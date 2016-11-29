using UnityEngine;
using System.Collections;

/// <summary>
///  Create a scene from scratch
/// </summary>
public class SceneFromScratch : MonoBehaviour {

	public Vector3 worldSize = new Vector3 (2000, 2000, 2000);

	public Terrain terrainMain;

	// Use this for initialization
	void Start () {

	

		int nRows = terrainMain.terrainData.heightmapWidth;
		int nCols = terrainMain.terrainData.heightmapHeight;

		float[,] heights = new float[nRows, nCols];
		for (int j = 0; j < nRows; j++) {
			for (int i = 0; i < nCols; i++) {
				heights [j, i] = 0;


			}
		}
		terrainMain.terrainData.SetHeights (0,0,heights);



		int[,] startPosition = new int[0,0];



		

		
	}



}