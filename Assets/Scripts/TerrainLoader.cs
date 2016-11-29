using UnityEngine;
using System.Collections;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class TerrainLoader : MonoBehaviour {

	// Member variables.
	//----------------------------------------------------------------------------------------------
	private Terrain    m_terrain      = null;
	private float[ , ] m_heightValues = null;
	//private int        m_resolution   = 0;
	GameObject tmpTerrain;

	// Use this for initialization
	private void Start()
	{
		/*
		tmpTerrain = GameObject.Find("TerrainDynamic");
		m_terrain = (Terrain) tmpTerrain.GetComponent(typeof (Terrain));


		//LoadTerrain ("/heightMaps/_ALL_20height.raw", m_terrain.terrainData);
		LoadTerrain ("/heightMaps/2.raw", m_terrain.terrainData);


		//Build NavMesh
		NavMeshBuilder.BuildNavMesh();

*/
	}


	void LoadTerrain(string aFileName, TerrainData aTerrain)
	{
		aFileName = Application.dataPath + aFileName;
		int h = aTerrain.heightmapHeight;
		int w = aTerrain.heightmapWidth;
		float[,] data = new float[h, w];

		using (var file = System.IO.File.OpenRead (aFileName))
		using (var reader = new System.IO.BinaryReader(file))
		{
			
			for (int y = 0; y < h; y++)
			{
				for (int x = 0; x < w; x++)
				{
					
					float v = (float)reader.ReadUInt16() / 0xFFFF;
					data[y, x] = v;
				}
			}
		}
		aTerrain.SetHeights(0, 0, data);


	}








	private void LoadHeightmap( string filename )
	{
		// Load heightmap.
		Texture2D heightmap = ( Texture2D ) Resources.Load( "heightMaps/" + filename );

		// Acquire an array of colour values.
		Color[] values = heightmap.GetPixels();

		// Run through array and read height values.
		int index = 0;
		for ( int z = 0; z < heightmap.height; z++ )
		{
			for ( int x = 0; x < heightmap.width; x++ )
			{
				m_heightValues[ z, x ] = values[ index ].r;
				index++;
			}
		}

		// Now set terrain heights.
		m_terrain.terrainData.SetHeights( 0, 0, m_heightValues );
	}


}
