using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
	public Sprite stone;

	public float surfaceValue = 0.25f;
    public int worldSize = 100;
	public float heightMultiplier = 25f;
	public int heightAddition = 25;
    public float caveFreq = 0.08f;
	public float terrainFreq = 0.04f;
    public float seed;
    public Texture2D noiseTexture;
    
    private void Start() {
		seed = Random.Range(-10000, 10000);
    	GenerateNoiseTexture();
		GenerateTerrain();
    }
    
    public void GenerateNoiseTexture() {
    	noiseTexture = new Texture2D(worldSize, worldSize);
    	
    	for (int x = 0; x < noiseTexture.width; x++)
    	{
    		for (int y = 0; y < noiseTexture.height; y++)
    		{
    			float v = Mathf.PerlinNoise((x + seed) * caveFreq, (y + seed) * caveFreq);
    			noiseTexture.SetPixel(x, y, new Color(v, v, v));
    		}
    	}
    	
    	noiseTexture.Apply();
    }

	public void GenerateTerrain() {
		for (int x = 0; x < worldSize; x++) {
			float height = Mathf.PerlinNoise((x + seed) * terrainFreq, seed * terrainFreq) * heightMultiplier + heightAddition;
			for (int y = 0; y < height; y++) {
				if (noiseTexture.GetPixel(x,y).r > surfaceValue) {
					GameObject newTile = new GameObject(name = "tile");
					newTile.transform.parent = this.transform;
					newTile.AddComponent<SpriteRenderer>();
					newTile.GetComponent<SpriteRenderer>().sprite = stone;
					newTile.transform.position = new Vector2(x + 0.5f, y + 0.5f);
				}
			
			}
		}
	}
}
