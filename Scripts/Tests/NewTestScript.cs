using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Reflection;

public class NewTestScript
{
    private GameObject mapGenObject;
    private MapGenerator mapGenerator;
    [SetUp]
    public void SetUp()
    {
        mapGenObject = GameObject.Find("Map Generator");
        Assert.IsNotNull(mapGenObject, "Map Generator object not found");


        /* [FOR TESTING WORLD GEN METHODS] */
        // Set world gen params
        int seed = 0;
        int octaves = 4;
        float persistance = 2f;
        float noiseScale = 25f;
        float lacunarity = 2f;
        float meshHeightMultiplier = 10f;
        AnimationCurve meshHeightCurve = AnimationCurve.Linear(0, 0, 1, 1);
        Vector2 offset = Vector2.zero;

        // Create a new instance of MapGenerator or use an existing one
        mapGenerator = new GameObject().AddComponent<MapGenerator>(); // Create a MapGenerator as a component of a new GameObject
        mapGenerator.seed = seed;
        mapGenerator.octaves = octaves;
        mapGenerator.noiseScale = noiseScale;
        mapGenerator.persistance = persistance;
        mapGenerator.lacunarity = lacunarity;
        mapGenerator.meshHeightMultiplier = meshHeightMultiplier;
        mapGenerator.meshHeightCurve = meshHeightCurve;
        mapGenerator.offset = offset;
        mapGenerator.useFalloff = true;

        // Define terrain types to ensure matching
        mapGenerator.regions = new TerrainType[2];
        mapGenerator.regions[0] = new TerrainType();
        mapGenerator.regions[0].name = "Water";
        mapGenerator.regions[0].height = 0.3f;
        mapGenerator.regions[0].color = Color.blue;

        mapGenerator.regions[1] = new TerrainType();
        mapGenerator.regions[1].name = "Land";
        mapGenerator.regions[1].height = 1f;
        mapGenerator.regions[1].color = Color.green;

        // Generate map
        mapGenerator.GenerateMap();
    }



    [Test]
    public void mapSize()  // White box test 1 : Ensure that mapChunkSize stays constant
    {
        Assert.AreEqual(241, MapGenerator.mapChunkSize, "Map chunk size should always remain constant");
    }

    [Test]
    public void levelOfDetail()  // White box test 2 : Ensure that OnValidate() is properly clamping level of detail
    {
        // Get script from actual game object (to modify values not set in script)
        MapGenerator scriptComponent = mapGenObject.GetComponent<MapGenerator>();

        // Attempt to put levelOfDetail out of range ([0, 6])
        scriptComponent.levelOfDetail = 7;

        // Use reflection to get onvalidate method from MapGenerator
        MethodInfo onValidateMethod = typeof(MapGenerator).GetMethod("OnValidate", BindingFlags.NonPublic | BindingFlags.Instance);

        // Invoke OnValidate
        onValidateMethod.Invoke(scriptComponent, null);

        // Check if value was allowed to exit range
        Assert.AreNotEqual(7, scriptComponent.levelOfDetail, "Level of detail should not be able to exit range [0, 6]");
    }

    [Test]
    public void meshGeneration()  // White box test 3 : Make sure mesh is generating correct number of vertices / triangles
    {
        // Set params
        float[,] heightMap = mapGenerator.GetNoiseMap();
        float heightMultiplier = 10f;
        AnimationCurve heightCurve = AnimationCurve.Linear(0, 0, 1, 1);
        int levelOfDetail = 0;

        // Generate mesh
        MeshData newMesh = MeshGenerator.GenerateTerrainMesh(heightMap, heightMultiplier, heightCurve, levelOfDetail);

        // Validate mesh
        Assert.AreEqual(240, Mathf.Sqrt(newMesh.triangles.Length / 6), "Incorrect number of triangles");  // Should have 3 values per triangle, 2 triangles per square, 240 squares in each direction
        Assert.AreEqual(241f, Mathf.Sqrt(newMesh.vertices.Length), "Incorrect number of vertices");  // Map dimensions squared num of vertices
    }

  
    [Test]
    public void colorMatching()  // Black box test 1 : Make sure edge of color map matches expected color based on noise map
    {
        // Get color map
        Color[] colorMap = mapGenerator.GetColorMap();

        // Check color at edge of map
        float currentHeight = mapGenerator.GetNoiseMap()[0, 0];
        Color expectedColor = Color.blue;

        Color actualColor = colorMap[0];
        Assert.AreEqual(expectedColor, actualColor, "Color does not match expected");
    }

    [Test]
    public void falloffMap()  // Black box test 2 : Make sure edge of map falls off and center does not
    {
        int mapSize = 10;

        float[,] falloffMap = FalloffGenerator.GenerateFalloffMap(mapSize);

        Assert.AreEqual(0, falloffMap[mapSize / 2, mapSize / 2], "Center of falloff map falls off");
        Assert.AreEqual(1, falloffMap[0, 0], "Edge of map does not fall off");
    }

}
