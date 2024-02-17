using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];

        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];
        for (int i = 0; i < octaves; i++)
        {
            float offsetX = prng.Next(-100000, 100000) + offset.x;
            float offsetY = prng.Next(-100000, 100000) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        // Make sure scale is not 0 bc we can't divide by 0
        if (scale <= 0) scale = 0.0001f;

        // Set min and max to be adjusted as noise is generated
        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float midWidth = mapWidth / 2f;
        float midHeight = mapHeight / 2f;

        // Loop through each spot on 2D array to generate noise
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                // Set default values per octave
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                // Loop through each octave
                for (int i = 0; i < octaves; i++)
                {
                    float sampleX = (x - midWidth) / scale * frequency + octaveOffsets[i].x;
                    float sampleY = (y - midHeight) / scale * frequency + octaveOffsets[i].y;

                    // Set noiseMap perlin value
                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;  // Random value between -1 and 1
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance;  // Each octave will have less affect on map
                    frequency *= lacunarity;  // Each octave will be more detailed
                }  // End of octave loop

                // Adjust min/max noiseHeight as needed
                if (noiseHeight > maxNoiseHeight) maxNoiseHeight = noiseHeight;
                else if (noiseHeight < minNoiseHeight) minNoiseHeight = noiseHeight;

                noiseMap[x, y] = noiseHeight;

            }
        }

        // Normalize noise map
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);  // Returns between 1 and 0, 1 being maxNoiseHeight and 0 being minNoiseHeight

            }
        }

        // Return created map
        return noiseMap;
    }
}
