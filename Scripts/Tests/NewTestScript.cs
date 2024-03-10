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
    public void NoiseGeneration()  // White box test on Noise's GenerateNoiseMap() method. Achieves branch coverage by hitting every line. Tests if a noise map is being generated properly.
    {
        int mapSize = 10;

        float[,] myNoiseMap = Noise.GenerateNoiseMap(mapSize, mapSize, 0, 25f, 4, 0.5f, 2f, Vector2.zero);

        for (int y = 0; y < mapSize; y++)
        {
            for (int x = 0; x < mapSize; x++)
            {
                Assert.AreNotEqual(null, myNoiseMap[x, y], "Map was not properly generated.");
            }
        }
    }

    [Test]
    public void SpawnPosition()  // White box test on PlayerSpawner's SelectSpawnLocation() method. Achieves statement coverage (as there are no branches) by hitting every line. Ensures that the values generated are within the spawn zone.
    {
        // Get spawn location
        Vector3 spawnLoc = PlayerSpawner.SelectSpawnLocation();

        Assert.LessOrEqual(spawnLoc.x, 200f, "Player spawn out of bounds");
        Assert.LessOrEqual(spawnLoc.z, 200f, "Player spawn out of bounds");
        Assert.GreaterOrEqual(spawnLoc.x, -200f, "Player spawn out of bounds");
        Assert.GreaterOrEqual(spawnLoc.z, -200f, "Player spawn out of bounds");
    }

    [Test]
    public void MapGeneratorValidation()  // White box test on MapGenerator's OnValidate() method. Achieves branch coverage by testing every possibility in the method.
    {
        // Get script from actual game object (to modify values not set in script)
        MapGenerator scriptComponent = mapGenObject.GetComponent<MapGenerator>();

        // Attempt to put levelOfDetail out of range ([0, 6])
        scriptComponent.levelOfDetail = 7;
        
        // Attempt to take octaves out of range ([0,+])
        scriptComponent.octaves = -3;

        // Attempt to take lacunarity out of range ([1,+])
        scriptComponent.lacunarity = -2;

        // Use reflection to get onvalidate method from MapGenerator
        MethodInfo onValidateMethod = typeof(MapGenerator).GetMethod("OnValidate", BindingFlags.NonPublic | BindingFlags.Instance);

        // Invoke OnValidate
        onValidateMethod.Invoke(scriptComponent, null);

        // Check if value was allowed to exit range
        Assert.AreNotEqual(7, scriptComponent.levelOfDetail, "Level of detail should not be able to exit range [0, 6]");
        Assert.AreNotEqual(-3, scriptComponent.octaves, "Octaves exited range of > 0");
        Assert.AreNotEqual(-2, scriptComponent.lacunarity, "Lacunarity left range of > 1");

        // Attempt to put levelOfDetail out of range in other direction ([0, 6])
        scriptComponent.levelOfDetail = -2;

        // Invoke OnValidate
        onValidateMethod.Invoke(scriptComponent, null);

        // Check if value was allowed to exit range
        Assert.AreNotEqual(-2, scriptComponent.levelOfDetail, "Level of detail should not be able to exit range [0, 6]");
    }

    [Test]
    public void MeshGenerationCount()  // White box test on MeshGenerator's GenerateTerrainMesh() method. Achieves branch coverage by executing every line (including conditional) and testing proper generation.
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
        Assert.AreNotEqual(null, newMesh.uvs, "UVs did not generate");
    }

    [Test]
    public void FalloffSize()  // White box test on FalloffGenerator's GenerateFalloffMap() method. Achieves statement coverage (There are no conditionals) by forcing every line in the method to execute and verifying proper execution.
    {
        // Set desired map size
        int mapSize = 10;

        // Generate map
        float[,] falloffMap = FalloffGenerator.GenerateFalloffMap(mapSize);

        // Check if size matches
        Assert.AreEqual(mapSize, falloffMap.GetLength(0), "Map does not match expected size");

        // Set desired map size
        mapSize = 100;

        // Generate map
        falloffMap = FalloffGenerator.GenerateFalloffMap(mapSize);

        // Check if size matches
        Assert.AreEqual(mapSize, falloffMap.GetLength(1), "Map does not match expected size");
    }

    [Test]
    public void ColorMatching()  // Black box acceptance test. Tests to make sure that my color map is properly following the color designated to the given region.
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
    public void FalloffMapOutput()  // Black box acceptance test. Tests to make sure the falloff map is correctly falling off towards the edges, with the center intact.
    {
        int mapSize = 10;

        float[,] falloffMap = FalloffGenerator.GenerateFalloffMap(mapSize);

        Assert.AreEqual(0, falloffMap[mapSize / 2, mapSize / 2], "Center of falloff map falls off");
        Assert.AreEqual(1, falloffMap[0, 0], "Edge of map does not fall off");
    }

    [Test]
    public void MeshGenerationSuccess()  // Black box acceptance test. Makes sure mesh is generated when calling GenerateTerrainMesh().
    {
        // Set params
        float[,] heightMap = mapGenerator.GetNoiseMap();
        float heightMultiplier = 1f;
        AnimationCurve heightCurve = AnimationCurve.Linear(0, 0, 1, 1);
        int levelOfDetail = 0;

        // Generate mesh
        MeshData newMesh = MeshGenerator.GenerateTerrainMesh(heightMap, heightMultiplier, heightCurve, levelOfDetail);

        // Check that mesh was created
        Assert.AreNotEqual(null, newMesh.ToString(), "Mesh was not generated correctly");
    }

    [Test]
    public void SeedChangesMap()  // Black box acceptance test. Makes sure that changing seed produces different maps.
    {
        // Generate two maps and save their height map
        mapGenerator.seed = 0;
        mapGenerator.GenerateMap();
        float[,] heightMap0 = mapGenerator.GetNoiseMap();

        mapGenerator.seed = 100;
        mapGenerator.GenerateMap();
        float[,] heightMap1 = mapGenerator.GetNoiseMap();

        // Check for difference between maps
        Assert.AreNotEqual(heightMap0, heightMap1, "Changing seed did not change height map");
    }

    [Test]
    public void FalloffGeneratorIsFollowed()  // Black box acceptance test. Makes sure that enabling and disabling falloff map actually changes world generation.
    {
        // Turn on falloff map
        mapGenerator.useFalloff = true;
        // Generate map
        mapGenerator.GenerateMap();
        // Save resulting map
        Color[] colorMap0 = mapGenerator.GetColorMap();

        // Turn off falloff map
        mapGenerator.useFalloff = false;
        // Generate map
        mapGenerator.GenerateMap();
        // Save resulting map
        Color[] colorMap1 = mapGenerator.GetColorMap();

        // Compare maps
        Assert.AreNotEqual(colorMap0, colorMap1, "Map does not follow falloff");
    }

    [Test]
    public void NoiseScaleAffectsGen()  // Black box acceptance test. Make sure that different noise scales generate the world differently.
    {
        mapGenerator.noiseScale = 0f;
        mapGenerator.GenerateMap();
        float[,] heightMap0 = mapGenerator.GetNoiseMap();

        mapGenerator.noiseScale = 1000f;
        mapGenerator.GenerateMap();
        float[,] heightMap1 = mapGenerator.GetNoiseMap();

        // Check that the maps generated differently
        Assert.AreNotEqual(heightMap0, heightMap1, "Changing noise scale did not affect world gen");
    }

    [Test]
    public void ValidateMeshAndColorMapConsistency()  // Integration test, bottom-up. Tests MeshGenerator and MapGenerator to ensure that the mesh generated by MeshGenerator and the color map generated by MapGenerator are consistent with one-another.
    {
        // Get generated height map
        float[,] heightMap = mapGenerator.GetNoiseMap();

        // Set parameters for mesh generation
        float heightMultiplier = 10f;
        AnimationCurve heightCurve = AnimationCurve.Linear(0, 0, 1, 1);
        int levelOfDetail = 0;

        // Generate mesh
        MeshData generatedMeshData = MeshGenerator.GenerateTerrainMesh(heightMap, heightMultiplier, heightCurve, levelOfDetail);

        // Get the generated color map
        Color[] generatedColorMap = mapGenerator.GetColorMap();

        // Check if the number of vertices in the mesh matches the number of colors in the color map
        Assert.AreEqual(generatedMeshData.vertices.Length, generatedColorMap.Length, "Number of vertices in mesh does not match number of colors in color map");
    }
}
