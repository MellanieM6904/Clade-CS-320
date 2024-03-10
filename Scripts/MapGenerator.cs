using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    // Enumeration for different drawing modes
    public enum DrawMode {NoiseMap, ColorMap, Mesh, FalloffMap}
    public DrawMode drawMode;

    // Factors for map generation
    public const int mapChunkSize = 241; // Size of the map chunk

    [Range(0, 6)]
    public int levelOfDetail; // Level of detail for the map

    public int seed, octaves;
    public float noiseScale, lacunarity, meshHeightMultiplier;
    public AnimationCurve meshHeightCurve;

    [Range(0, 1)]
    public float persistance; // Persistence of the noise
    public Vector2 offset; // Offset for noise generation
    public bool autoUpdate, useFalloff; // Flags for auto-updating and using falloff map
    public TerrainType[] regions; // Array of terrain types
    float[,] falloffMap; // Map for falloff effect

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Generate the falloff map
        falloffMap = FalloffGenerator.GenerateFalloffMap((mapChunkSize + mapChunkSize) / 2);
    }
    
    // Method to generate the map
    public void GenerateMap()
    {
        // Generate the noise map
        float[,] noiseMap = Noise.GenerateNoiseMap(mapChunkSize, mapChunkSize, seed, noiseScale, octaves, persistance, lacunarity, offset);  

        // Create a color map
        Color[] colorMap = new Color[mapChunkSize * mapChunkSize];

        // Loop through each location on the map
        for (int y = 0; y < mapChunkSize; y++)
        {
            for (int x = 0; x < mapChunkSize; x++)
            {
                // Apply falloff effect if enabled
                if (useFalloff)
                {
                    noiseMap[x, y] = Mathf.Clamp01(noiseMap[x, y] - falloffMap[x, y]);
                }
                
                float curHeight = noiseMap[x, y];  // Get current height of map location

                // Loop through each possible region
                for (int i = 0; i < regions.Length; i++)
                {
                    // Assign color based on height and region
                    if (curHeight <= regions[i].height)
                    {
                        colorMap[y * mapChunkSize + x] = regions[i].color;  
                        break;
                    }
                }
            }
        }

        // Get a reference to MapDisplay
        MapDisplay display = FindObjectOfType<MapDisplay>();  
        
        // Draw based on draw mode
        if (drawMode == DrawMode.NoiseMap) 
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));  
        else if (drawMode == DrawMode.ColorMap) 
            display.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, mapChunkSize, mapChunkSize));
        else if (drawMode == DrawMode.Mesh) 
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMultiplier, meshHeightCurve, levelOfDetail), TextureGenerator.TextureFromColorMap(colorMap, mapChunkSize, mapChunkSize));
        else if (drawMode == DrawMode.FalloffMap) 
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(FalloffGenerator.GenerateFalloffMap((mapChunkSize + mapChunkSize) / 2)));
    }

    // OnValidate is called when a value is changed in the editor
    private void OnValidate()
    {
        // Ensure parameters stay within valid ranges
        if (lacunarity < 1) lacunarity = 1;
        if (octaves < 0) octaves = 0;

        // Regenerate falloff map
        falloffMap = FalloffGenerator.GenerateFalloffMap(mapChunkSize);
    }
}

// Serializable struct for defining terrain types
[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color color;
}
