using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;
using System;

public class DepthViewTest : MonoBehaviour
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

    public GameObject bodyObject;

    public GameObject captureObject;

    public Material circleMaterial;
    public Material cubeMaterial;

    private GameObject bodyPos;
    private GameObject circlePos;

    private Renderer rend;

    private SpriteRenderer srend;

    public static List<Vector3> circlePositions;


    // Use this for initialization
    void Start()
    {
        DepthActive = true;
        averageDepth = 0;
        ShowAndStart();
        rend = GetComponent<Renderer>();
        srend = GetComponent<SpriteRenderer>();
        circlePositions = new List<Vector3>();       
    }


    // Update is called once per frame
    void Update()
    {
        circlePositions.Clear();
        if (DepthActive == true && reader != null)
        {
            var frame = reader.AcquireLatestFrame();

            if (frame != null)
            {
                frame.CopyFrameDataToArray(rawData);

                frame.Dispose();
                frame = null;
                int index = 0;
                int index2 = 0;
                if (averageDepth == 0)
                {
                    int totalDepth = 0;
                    //int[] modeValues = new int[217088];
                    foreach (var dp in rawData)
                    {
                        byte intensity = (byte)(dp >> 4);
                        if (intensity != 0)
                        {
                            totalDepth += intensity;
                            index++;
                        }
                        
                    }
                    averageDepth = totalDepth / index;
                    Debug.Log("This should only appear once");         
                }
                
                index = 0;
                index2 = 0;
                int[,] detectionFrame = new int[129, 107];
                foreach (var dp in rawData)
                {
                    byte intensity = (byte)(dp >> 4);
                    // 70 with 125cm difference
                    if (intensity !=0 && intensity < 70)
                    {
                        /**
                        data[index++] = intensity;
                        data[index++] = 100;
                        data[index++] = 0;
                        data[index++] = 255;
                        **/
                        detectionFrame[index2 / 4 % 128, index2 / 2048] = 1;
                    }
                    else
                    {
                        /**
                        data[index++] = 0;
                        data[index++] = 0;
                        data[index++] = 0;
                        data[index++] = 0;
                        **/
                        detectionFrame[index2 / 4 % 128, index2 / 2048] = 0;
                    }
                    index2++;
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
                            if (l%2 == 0 && k%2 ==0)
                            {

                                bodyPos = Instantiate(bodyObject);
                                /**
                                bodyPos = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube));
                                bodyPos.tag = "detection";
                                bodyPos.layer = 9;
                                bodyPos.GetComponent<Renderer>().material = circleMaterial;
                                **/
                                //bodyPos.GetComponent<MeshRenderer>().enabled = false;
                                double xPercent = (double)l / 129.0;
                                float xFloat = (float)xPercent * 29.5f;
                                double yPercent = (double)k / 107.0;
                                float yFloat = (float)yPercent * -17f;
                                bodyPos.transform.localScale -= new Vector3(0.53f, 0.655f, 0.5f);
                                bodyPos.transform.position = new Vector3(1 * (14.3f - xFloat), 1 * (8.2f + yFloat), 0);
                                //l += 10;
                            }
                        }
                        else if (detectionFrame[l, k] == 0)
                        {
                            if (l % 2 == 0 && k % 2 == 0)
                            {
                                double xPercent = (double)l / 129.0;
                                float xFloat = (float)xPercent * 29.5f;
                                double yPercent = (double)k / 107.0;
                                float yFloat = (float)yPercent * -17f;
                                float xPos = 1 * (14.3f - xFloat);
                                float yPos = 1 * (8.2f + yFloat);
                                //if (l % 3 == 0 && k % 3 == 0)
                                //{
                                /**
                                circlePos = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere));
                                circlePos.GetComponent<Renderer>().material = cubeMaterial;
                                //circlePos.GetComponent<MeshRenderer>().enabled = false;
                                circlePos.tag = "detection";
                                **/
                                circlePos = Instantiate(captureObject);
                                circlePos.transform.position = new Vector3(xPos, yPos, 0);
                                circlePos.transform.localScale -= new Vector3(0.54f, 0.655f, 0.5f);
                                //}
                                circlePositions.Add(new Vector3(xPos, yPos, 0));
                                
                            }
                        }
                        l += 1;
                    }
                   k += 1;
                }

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

    // Flood fill algorithm based from Karim Oumghars solution at https://simpledevcode.wordpress.com/2015/12/29/flood-fill-algorithm-using-c-net/

    private int[,] FloodFill(int[,] originalPixels)
    {
        Stack<Point> pixels = new Stack<Point>();
        int[,] newPixels = new int[originalPixels.GetLength(0), originalPixels.GetLength(1)];
        Point startingPos = new Point(-1, -1);
        
        pixels.Push(new Point(1, 1));
        pixels.Push(new Point(1, 25));
        pixels.Push(new Point(1, 50));
        pixels.Push(new Point(1, 105));
        pixels.Push(new Point(1, 10));
        pixels.Push(new Point(1, 20));
        pixels.Push(new Point(1, 30));
        pixels.Push(new Point(1, 40));
        pixels.Push(new Point(1, 60));
        pixels.Push(new Point(1, 70));
        pixels.Push(new Point(1, 80));
        pixels.Push(new Point(1, 90));
        pixels.Push(new Point(1, 100));
        pixels.Push(new Point(127, 1));
        pixels.Push(new Point(127, 25));
        pixels.Push(new Point(127, 50));
        pixels.Push(new Point(127, 105));
        pixels.Push(new Point(127, 10));
        pixels.Push(new Point(127, 20));
        pixels.Push(new Point(127, 30));
        pixels.Push(new Point(127, 40));
        pixels.Push(new Point(127, 60));
        pixels.Push(new Point(127, 70));
        pixels.Push(new Point(127, 80));
        pixels.Push(new Point(127,90));
        pixels.Push(new Point(127, 100));
        pixels.Push(new Point(10, 1));
        pixels.Push(new Point(20, 1));
        pixels.Push(new Point(30, 1));
        pixels.Push(new Point(40, 1));
        pixels.Push(new Point(50, 1));
        pixels.Push(new Point(60, 1));
        pixels.Push(new Point(70, 1));
        pixels.Push(new Point(80, 1));
        pixels.Push(new Point(90, 1));
        pixels.Push(new Point(100, 1));
        pixels.Push(new Point(110, 1));
        pixels.Push(new Point(120, 1));
        pixels.Push(new Point(10, 105));
        pixels.Push(new Point(20, 105));
        pixels.Push(new Point(30, 105));
        pixels.Push(new Point(40, 105));
        pixels.Push(new Point(50, 105));
        pixels.Push(new Point(60, 105));
        pixels.Push(new Point(70, 105));
        pixels.Push(new Point(80, 105));
        pixels.Push(new Point(90, 105));
        pixels.Push(new Point(100, 105));
        pixels.Push(new Point(110, 105));
        pixels.Push(new Point(120, 105));
        while (pixels.Count > 0)
        {
            Point a = pixels.Pop();
            if (a.X < originalPixels.GetLength(0) && a.X >= 0 &&
                    a.Y < originalPixels.GetLength(1) && a.Y >= 0)//make sure we stay within bounds
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

