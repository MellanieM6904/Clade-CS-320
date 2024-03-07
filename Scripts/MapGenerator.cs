using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode {NoiseMap, ColorMap, Mesh, FalloffMap}
    public DrawMode drawMode;

    // Factors for a generation
    public const int mapChunkSize = 241;

    [Range(0, 6)]
    public int levelOfDetail;

    public int seed, octaves;
    public float noiseScale, lacunarity, meshHeightMultiplier;
    public AnimationCurve meshHeightCurve;

    [Range(0, 1)]
    public float persistance;
    public Vector2 offset;
    public bool autoUpdate, useFalloff;
    public TerrainType[] regions;
    float[,] falloffMap;

    private Color[] colorMap;
    private float[,] noiseMap;

    private void Awake()
    {
        falloffMap = FalloffGenerator.GenerateFalloffMap((mapChunkSize + mapChunkSize) /2);
    }
    // End of generation factors

    public void GenerateMap()
    {
        noiseMap = Noise.GenerateNoiseMap(mapChunkSize, mapChunkSize, seed, noiseScale, octaves, persistance, lacunarity, offset);  // Calls our generate method from our Noise class

        colorMap = new Color[mapChunkSize * mapChunkSize];
        // Loop through each location on map
        for (int y = 0; y < mapChunkSize; y++)
        {
            for (int x = 0; x < mapChunkSize; x++)
            {
                if (useFalloff)
                {
                    noiseMap[x, y] = Mathf.Clamp01(noiseMap[x, y] - falloffMap[x, y]);
                }
                float curHeight = noiseMap[x, y];  // Get current height of map location

                // Loop through each possible region
                for (int i = 0; i < regions.Length; i++)
                {
                    if (curHeight <= regions[i].height)  // If map location is in current region
                    {
                        colorMap[y * mapChunkSize + x] = regions[i].color;  // Assigns region color to current map location
                        break;
                    }
                }

            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();  // Gets a reference to our MapDisplay script on our plane object
        if (drawMode == DrawMode.NoiseMap) display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));  // Calls our Draw method (Puts the noise map on our plane)
        else if (drawMode == DrawMode.ColorMap) display.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, mapChunkSize, mapChunkSize));
        else if (drawMode == DrawMode.Mesh) display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMultiplier, meshHeightCurve, levelOfDetail), TextureGenerator.TextureFromColorMap(colorMap, mapChunkSize, mapChunkSize));
        else if (drawMode == DrawMode.FalloffMap) display.DrawTexture(TextureGenerator.TextureFromHeightMap(FalloffGenerator.GenerateFalloffMap((mapChunkSize + mapChunkSize) / 2)));
    }

    public Color[] GetColorMap()
    {
        return colorMap;
    }

    public float[,] GetNoiseMap()
    {
        return noiseMap;
    }

    private void OnValidate() // Makes sure map elements stay in possible range when adjusting in the editor
    {
        if (lacunarity < 1) lacunarity = 1;
        if (octaves < 0) octaves = 0;
        if (levelOfDetail < 0) levelOfDetail = 0;
        if (levelOfDetail > 6) levelOfDetail = 6;

        falloffMap = FalloffGenerator.GenerateFalloffMap(mapChunkSize);
    }
}


[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color color;
}