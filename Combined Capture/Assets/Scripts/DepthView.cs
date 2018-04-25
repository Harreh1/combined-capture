using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;
using System;

public class DepthView : MonoBehaviour
{

    public bool DepthActive { get; private set; }

    public int DepthWidth { get; private set; }
    public int DepthHeight { get; private set; }

    private KinectSensor sensor;
    private DepthFrameReader reader;
    private Texture2D texture;
    private byte[] data;
    private ushort[] rawData;
    private int averageDepth;

    public GameObject depthPlane;

    private GameObject bodyPos;
    private GameObject circlePos;

    private Renderer rend;

    private SpriteRenderer srend;

    public static List<Vector3> circlePositions;

    // Use this for initialization
    void Start()
    {
        DepthActive = true  ;
        averageDepth = 0;
        ShowAndStart();
        rend = GetComponent<Renderer>();
        srend = GetComponent<SpriteRenderer>();
        circlePositions = new List<Vector3>();
        
    }


    // Update is called once per frame
    void Update()
    {

        if (DepthActive == true && reader != null)
        {
            var frame = reader.AcquireLatestFrame();

            if (frame != null)
            {
                frame.CopyFrameDataToArray(rawData);

                frame.Dispose();
                frame = null;
                int index = 0;
                if (averageDepth == 0)
                {
                    int totalDepth = 0;
                    //int[] modeValues = new int[217088];
                    foreach (var dp in rawData)
                    {
                        byte intensity = (byte)(dp >> 8);
                        //modeValues[index] = intensity;
                        totalDepth += intensity;
                        index++;
                    }
                    averageDepth = totalDepth / index;
                    Debug.Log("This should only appear once");
                   
                }

                index = 0;
                int[,] detectionFrame = new int[513, 425];
                foreach (var dp in rawData)
                {
                    byte intensity = (byte)(dp >> 8);
                    if (intensity < averageDepth)
                    {
                        data[index++] = intensity;
                        data[index++] = 100;
                        data[index++] = 0;
                        data[index++] = 255;
                        detectionFrame[index / 4 % 512, index / 4 / 512] = 1;
                    }
                    else
                    {
                        data[index++] = 0;
                        data[index++] = 0;
                        data[index++] = 0;
                        data[index++] = 0;
                        detectionFrame[index / 4 % 512, index / 4 / 512] = 0;
                    }
                }
                //texture.LoadRawTextureData(data);
                //texture.Apply();
                //depthPlane.GetComponent<MeshRenderer>().material.mainTexture = texture;
                detectionFrame = FloodFill(detectionFrame);
                GameObject[] detections;
                detections = GameObject.FindGameObjectsWithTag("detection");
                foreach (GameObject d in detections)
                {
                    Destroy(d);
                }
                for (int k = 0; k < detectionFrame.GetLength(1); k++)
                {
                    for (int l = 0; l < detectionFrame.GetLength(0); l++)
                    {
                        if (detectionFrame[l, k] == 1)
                        {
                            if (true)
                            {
                                bodyPos = GameObject.CreatePrimitive(PrimitiveType.Cube);
                                bodyPos.tag = "detection";
                                bodyPos.layer = 9;
                                bodyPos.GetComponent<Renderer>().material.color = Color.red;
                                //bodyPos.GetComponent<MeshRenderer>().enabled = false;
                                double xPercent = (double)l / 513.0;
                                float xFloat = (float)xPercent * 28542 * 2;
                                double yPercent = (double)k / 425.0;
                                float yFloat = (float)yPercent * -16309*2;
                                bodyPos.transform.localScale += new Vector3(800f, 800f, 3f);
                                bodyPos.transform.position = new Vector3(1 * (57250f - xFloat), 1 * (-500f + yFloat), 0);
                                l += 10;
                            }
                        }
                        else if (detectionFrame[l, k] == 0)
                        {
                            if (true)
                            {
                                
                                circlePos = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                                circlePos.GetComponent<Renderer>().material.color = Color.blue;
                                //circlePos.GetComponent<MeshRenderer>().enabled = false;
                                circlePos.tag = "detection";
                                double xPercent = (double)l / 513.0;
                                float xFloat = (float)xPercent * 28542 * 2;
                                double yPercent = (double)k / 425.0;
                                float yFloat = (float)yPercent * -16309 * 2;
                                circlePos.transform.localScale += new Vector3(500f, 500f, 3);
                                circlePos.transform.position = new Vector3(1 * (57250f - xFloat), 1 * (-500f + yFloat), 0);
                                circlePositions.Add(new Vector3(1 * (57250f - xFloat), 1 * (-500f + yFloat), 0));
                                l += 10;
                            }
                        }
                    }
                    k += 10;
                }
                /**
                bodyPos = GameObject.CreatePrimitive(PrimitiveType.Cube);
                bodyPos.transform.position = new Vector3(-5.9f, -4.5f, 0);
                Light bodyLight = bodyPos.AddComponent<Light>();
                bodyLight.color = Color.blue;
                bodyLight.type = LightType.Point; **/



                //bodyPos.transform.
                // 512 x 424 dimensions
                // Total pixels is 217088
                // Data is 868352 elements long (4 for evert pixel)

            }
        }
    }

    public void ShowAndStart()
    {
        sensor = KinectSensor.GetDefault();

        if (sensor != null)
        {
            reader = sensor.DepthFrameSource.OpenReader();

            var frameDesc = sensor.DepthFrameSource.FrameDescription;
            DepthWidth = frameDesc.Width;
            DepthHeight = frameDesc.Height;

            texture = new Texture2D(DepthWidth, DepthHeight, TextureFormat.BGRA32, false);
            data = new byte[frameDesc.LengthInPixels * 4];
            rawData = new ushort[frameDesc.LengthInPixels];

            if (sensor.IsOpen == false)
            {
                sensor.Open();

                depthPlane.GetComponent<MeshRenderer>().enabled = true;
                DepthActive = true;
            }
        }
    }

    private int[,] FloodFill(int[,] originalPixels)
    {
        Stack<Point> pixels = new Stack<Point>();
        int[,] newPixels = new int[originalPixels.GetLength(0), originalPixels.GetLength(1)];
        Point startingPos = new Point(-1, -1);
        int[] visitedPoints;

        for (int k = 0; k < originalPixels.GetLength(1); k++)
        {
            for (int l = 0; l < originalPixels.GetLength(0); l++)
            {
                if (originalPixels[l, k] == 0)
                {
                    startingPos = new Point(l, k);
                }
            }
        }
        if (startingPos.X != -1)
        {
            pixels.Push(startingPos);
        }
        while (pixels.Count > 0)
        {
            Point a = pixels.Pop();
            if (a.X < originalPixels.GetLength(0) && a.X > 0 &&
                    a.Y < originalPixels.GetLength(1) && a.Y > 0)//make sure we stay within bounds
            {

                if (originalPixels[a.X, a.Y] == 0)
                {
                    originalPixels[a.X, a.Y] = 2;
                    pixels.Push(new Point(a.X - 1, a.Y));
                    pixels.Push(new Point(a.X + 1, a.Y));
                    pixels.Push(new Point(a.X, a.Y - 1));
                    pixels.Push(new Point(a.X, a.Y + 1));
                }
            }
        }
        return originalPixels;
    }

    public void HideAndStop()
    {
        depthPlane.GetComponent<MeshRenderer>().enabled = false;
        CloseKinect();
        DepthActive = false;
    }

    private void CloseKinect()
    {
        if (reader != null)
        {
            reader.Dispose();
            reader = null;
        }
        if (sensor != null)
        {
            if (sensor.IsOpen)
            {
                sensor.Close();
            }
            sensor = null;
        }
    }

    void OnApplicationQuit()
    {
        CloseKinect();
    }

    public void ToggleSensor()
    {
        if (DepthActive == false)
        {
            ShowAndStart();
        }
        else
        {
            HideAndStop();
        }
    }

    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}

