//Demostrate Terrain Height Texture Tool on what it can do.
//By David Harriosn

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TTHT;
using System.Threading;

[RequireComponent(typeof(TTHTS))]
public class Demo : MonoBehaviour
{
    //terrain size variables
    public GameObject Huge;
    public GameObject Big;
    public GameObject Medium;
    public GameObject Small;
    public enum TerrainSize { Huge, Big, Medium, Small };
    public TerrainSize Size = TerrainSize.Small;
    //paint variables
    private TTHTS test;
    public Texture2D[] TerrainTextures;
    //camera
    public CameraControl Cam;
    public GameObject CamO;

    //GUI var
    private bool changeTerrain = false;
    private Vector2 scrollPosition;
    private Vector2 scrollHeights;
    public GUISkin skin;
    private bool pickTex = false;
    private int HeightTexVal;

    private int selGridInt = -1;
    private string[] selStrings = new string[] { "Huge(2,000x2,000)", "Big(1,000x1,000)", "Medium(500x500)", "Small(100x100)" };
    private bool finishPaint = true;
    private float secsToFinish = 0;
    //private ThreadStart childref = new ThreadStart(ThreadCreationProgram.CallToChildThread);
    //Thread childThread;
    // Use this for initialization
    void Start()
    {
        
        //Debug.Log("In Main: Creating the Child thread");
        // childThread = new Thread(childref);
        //childThread.Start();

        test = GetComponent<TTHTS>();
        ChangeTerrains();
    }
    //change terrains
    private void ChangeTerrains()
    {

        switch (Size)
        {
            case TerrainSize.Huge:
                CamO.gameObject.transform.position = new Vector3(1200, 600, 80);
                test.terrain = Huge;
                break;
            case TerrainSize.Big:
                CamO.gameObject.transform.position = new Vector3(450, 600, 2);
                test.terrain = Big;
                break;
            case TerrainSize.Medium:
                CamO.gameObject.transform.position = new Vector3(250, 500, -10);
                test.terrain = Medium;
                break;
            default:
                CamO.gameObject.transform.position = new Vector3(50, 200, -10);
                test.terrain = Small;
                break;
        }
        ActiveTerrain();


    }
    //to deactive all the terrains where not using and active the one we want to use
    private void ActiveTerrain()
    {
        switch (Size)
        {
            case TerrainSize.Huge:
                Huge.SetActive(true);
                Big.SetActive(false);
                Medium.SetActive(false);
                Small.SetActive(false);
                break;
            case TerrainSize.Big:
                Huge.SetActive(false);
                Big.SetActive(true);
                Medium.SetActive(false);
                Small.SetActive(false);
                break;
            case TerrainSize.Medium:
                Huge.SetActive(false);
                Big.SetActive(false);
                Medium.SetActive(true);
                Small.SetActive(false);
                break;
            default:
                Huge.SetActive(false);
                Big.SetActive(false);
                Medium.SetActive(false);
                Small.SetActive(true);
                break;
        }
    }



    void Update()
    {
        if (selGridInt != -1)
        {
            switch (selGridInt)
            {
                case 0:
                    Size = TerrainSize.Huge;
                    break;
                case 1:
                    Size = TerrainSize.Big;
                    break;
                case 2:
                    Size = TerrainSize.Medium;
                    break;
                case 3:
                    Size = TerrainSize.Small;
                    break;
            }
            selGridInt = -1;
            ChangeTerrains();
            changeTerrain = false;
        }
       


    }
    void OnGUI()
    {
        GUI.skin = skin;

        GUI.skin.button.imagePosition = ImagePosition.ImageAbove;
        if (finishPaint)
        {
            if (secsToFinish > 0)
            {
                GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200), "Paint Time:  " + secsToFinish +" Secs");
            }
            if (!changeTerrain)
            {
                //to turn on  and off the two scroll windows
                if (pickTex)
                    CycleTextures();
                else
                {
                    if (GUI.Button(new Rect(500, 500, 100, 100), "Change Size"))
                    {
                        changeTerrain = true;
                    }
                    TextureHeights();
                    Paint();
                }

            }
            else
                ButtonsTerrainChange();
        }
        else
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200), "Loading " + test.GetPercantageDone() + "%");
    }

    //GUI to change the terrain size
    private void ButtonsTerrainChange()
    {

        selGridInt = GUI.SelectionGrid(new Rect(25, 25, 300, 200), selGridInt, selStrings, 2);

    }
    //the scroll window that allows you to change the values of the height types
    public void TextureHeights()
    {
        //show the values of the heightTypes and allow to adjust the height
        scrollHeights = GUILayout.BeginScrollView(scrollHeights, GUILayout.Width(250), GUILayout.Height(500));
        for (int x = 0; x < test.HeightTypes.Count; x++)
        {
            if (GUILayout.Button(new GUIContent("Select", test.HeightTypes[x].texture), GUILayout.Width(100), GUILayout.Height(100)))
            {
                pickTex = true;
                HeightTexVal = x;

            }


            if (x == 0)
            {
                //HeightTypes[x].Height = GUILayout.HorizontalSlider(HeightTypes[x].Height, 0.0f, 0.0f);
                GUILayout.Label("Ground");
            }
            else if (x == test.HeightTypes.Count - 1)
            {
                test.HeightTypes[x].StartHeight = GUILayout.HorizontalSlider(test.HeightTypes[x].StartHeight, test.HeightTypes[x - 1].StartHeight, Terrain.activeTerrain.terrainData.size.y);
                GUILayout.Label("Top: " + test.HeightTypes[x].StartHeight.ToString());
            }
            else
            {
                test.HeightTypes[x].StartHeight = GUILayout.HorizontalSlider(test.HeightTypes[x].StartHeight, test.HeightTypes[x - 1].StartHeight, test.HeightTypes[x + 1].StartHeight);
                GUILayout.Label(test.HeightTypes[x].StartHeight.ToString());
            }
        }
        GUILayout.EndScrollView();
        //add and remove HeightTypes
        if (test.HeightTypes.Count < 8)
        {
            if (GUILayout.Button("Add New Height Texuture", GUILayout.Height(40)))
            {
                test.HeightTypes.Add(new HeightType());
                if (test.HeightTypes.Count > 1)
                    test.HeightTypes[test.HeightTypes.Count - 1].StartHeight = test.HeightTypes[test.HeightTypes.Count - 2].StartHeight;

            }
        }
        if (test.HeightTypes.Count > 0)
        {
            if (GUILayout.Button("Remove", GUILayout.Height(40)))
                test.HeightTypes.Remove(test.HeightTypes[test.HeightTypes.Count - 1]);
        }

    }
    //the texuture picker
    public void CycleTextures()
    {
        scrollPosition = GUI.BeginScrollView(new Rect(10, 30, 220, 210), scrollPosition, new Rect(0, 0, 180, 105 * (TerrainTextures.Length / 2)));
        GUILayout.BeginVertical();
        int tCount = 0;
        for (int x = 0; x < TerrainTextures.Length; x++)
        {

            if (tCount == 0)
            {
                GUILayout.BeginHorizontal();
            }
            tCount++;

            if (GUILayout.Button(TerrainTextures[x], GUILayout.Width(100), GUILayout.Height(100)))
            {
                pickTex = false;
                test.HeightTypes[HeightTexVal].texture = TerrainTextures[x];
            }
            if (tCount == 2)
            {
                GUILayout.EndHorizontal();
                tCount = 0;
            }
        }
        GUILayout.EndVertical();
        GUI.EndScrollView();
    }
    // paint the terrain
    public void Paint()
    {
        if (test.HeightTypes.Count > 0)
        {
            GUI.skin.button.imagePosition = ImagePosition.TextOnly;
            if (finishPaint)
            {
                if (GUI.Button(new Rect(Screen.width / 2 - 50, 10, 100, 50), "Paint"))
                {
                    StartCoroutine(StartPaintProcess());
                }
            }
        }
    }

    IEnumerator StartPaintProcess()
    {
        finishPaint = false;
        ThreadCreationProgram d = new ThreadCreationProgram();
      
        ThreadStart childref = new ThreadStart(d.CallToChildThread);
        Debug.Log("In Main: Creating the Child thread");
        Thread childThread = new Thread(childref);
        childThread.Start();
        
        //secsToFinish = 0;
       
        //yield return new WaitForSeconds(3);
        yield return StartCoroutine(test.StartPaint());
        childThread.Abort();
        secsToFinish = d.counter;
       // Debug.Log(secsToFinish);
        finishPaint = true;
    }
    //change the speed of the camera
    public void CameraControls()
    {
        GUI.Label(new Rect(Screen.width - 200, 10, 200, 100), "MoveSpeed:    " + Cam.MoveSpeed.ToString());

        Cam.MoveSpeed = GUI.HorizontalSlider(new Rect(Screen.width - 200, 110, 200, 100), Cam.MoveSpeed, 5, 100);

        GUI.Label(new Rect(Screen.width - 200, 170, 200, 100), "ScrollSpeed:    " + Cam.ScrollSpeed.ToString());

        Cam.ScrollSpeed = GUI.HorizontalSlider(new Rect(Screen.width - 200, 270, 200, 100), Cam.ScrollSpeed, 5, 100);
        //Cam.ScrollSpeed;
        //Cam.MoveSpeed;

    }

    class ThreadCreationProgram
    {
        public float counter = 0;
        public void CallToChildThread()
        {
           
                Debug.Log("Child thread starts");
            // do some work, like counting to 10

            while (true)
            {
                Thread.Sleep(100);

                counter += 0.1f;
                
            }

           
               
            
        }
    }
}


