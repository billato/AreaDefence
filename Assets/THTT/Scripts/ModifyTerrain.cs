//Modifly the terrain map texture base off height
//By David Harriosn

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TTHT;
using System.Linq;
[RequireComponent(typeof(TTHTS))]
public class ModifyTerrain : MonoBehaviour
{
    private TTHTS test ;
    
    private bool finishPaint = true;
    // Use this for initialization
    void Start () {
    
        test = GetComponent<TTHTS>();
        
		//tsiotas
		//Paint();
		StartCoroutine(StartPaintProcess());
    }

    void OnGUI()
    {
		
        //   Paint();         
    }

    public void Paint()
    {


        if (test.HeightTypes.Count > 0)
        {
            GUI.skin.button.imagePosition = ImagePosition.TextOnly;
            if (finishPaint)
            {
                if (GUI.Button(new Rect(Screen.width / 2 - 50, 10, 100, 50), "Paint"))
                {
                    //test.HeightTypes = HeightTypes;//store the heightType values in the thp so it can use that info
                    StartCoroutine(StartPaintProcess());
                }
            }
            else
                GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200), "Loading " + test.GetPercantageDone() + "%");
        }


	}

    IEnumerator StartPaintProcess()
    {

        finishPaint = false;
       yield return StartCoroutine(test.StartPaint());
        finishPaint = true;
    }
}
