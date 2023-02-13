using UnityEngine;

namespace GeekBrains.Helpers
{
public class PerlinNoise : MonoBehaviour {

	[SerializeField] private Texture2D[] _terrainTextures;
	[SerializeField] private int _depth = 30;
	[SerializeField] private int _width = 1024;
	[SerializeField] private int _height = 1024;
	[SerializeField] private int _scale = 30;

	private void Start()
	{
		var terrain = Terrain.CreateTerrainGameObject(GenerationTerrain()).GetComponent<Terrain>();

		GenerationAlphamaps(terrain);
	}

	private TerrainData GenerationTerrain()
	{
		var result = new TerrainData
		{
			heightmapResolution = _width + 1,
			size = new Vector3(_width, _depth, _height),
		};

		result.SetHeights(0,0, GenerationHeights());
		result.splatPrototypes = GenerationSplatPrototype();
		return result;
	}


	private float[,] GenerationHeights()
	{
		float[,] heights = new float[_width,_height];

		for (var i = 0; i < _width; i++)
		{
			for (var j = 0; j < _height; j++)
			{
				heights[i, j] = CalculateHeight(i, j);
			}
		}

		return heights;
	}

	private float CalculateHeight(int i, int j)
	{
		float xCoord = (float)i / _width * _scale;
		float yCoord = (float)j / _height * _scale;

		return Mathf.PerlinNoise(xCoord, yCoord);
	}

	private SplatPrototype[] GenerationSplatPrototype()
	{
		if (_terrainTextures == null) return null;
		SplatPrototype[] splatPrototypes = new SplatPrototype[_terrainTextures.Length];
		for (var i = 0; i < _terrainTextures.Length; i++)
		{
			splatPrototypes[i] = new SplatPrototype
			{
				texture = _terrainTextures[i],
				tileSize = new Vector2(1, 1)
			};
		}
		return splatPrototypes;
	}

	private void GenerationAlphamaps(Terrain t)
	{
		var map = new float[t.terrainData.alphamapWidth, t.terrainData.alphamapHeight, 2];
		
		for (var y = 0; y < t.terrainData.alphamapHeight; y++)
		{
			for (var x = 0; x < t.terrainData.alphamapWidth; x++)
			{
				var normX = x * 1.0 / (t.terrainData.alphamapWidth - 1);
				var normY = y * 1.0 / (t.terrainData.alphamapHeight - 1);
				
				var angle = t.terrainData.GetSteepness((float)normX, (float)normY);
				
				var frac = angle / 90.0;
				map[x, y, 0] = (float)frac;
				map[x, y, 1] = 1 - (float)frac;
			}
		}

		t.terrainData.SetAlphamaps(0, 0, map);
	}
}
}
