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
    public void MapSize()  // White box test 1 : Ensure that mapChunkSize stays constant
    {
        Assert.AreEqual(241, MapGenerator.mapChunkSize, "Map chunk size should always remain constant");
    }

    [Test]
    public void LevelOfDetail()  // White box test 2 : Ensure that OnValidate() is properly clamping level of detail
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
    public void MeshGeneration()  // White box test 3 : Make sure mesh is generating correct number of vertices / triangles
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

/* VV TEST THIS TEST || TEST THIS TEST || TEST THIS TEST || TEST THIS TEST || TEST THIS TEST || TEST THIS TEST VV */
    [Test]
    public void DrawTexture()  // White box test 4 : Make sure DrawTexture() from MapDisplay properly draws the given texture
    {
        // Create test texture
        Texture2D texture = new Texture2D(10, 10);

        // Create blank MapDisplay to apply texture to
        MapDisplay display = new MapDisplay();

        // Call DrawTexture() with test texture
        display.DrawTexture(texture);

        // Make sure texture was properly applied
        Assert.AreEqual(texture, display.sharedMaterial.mainTexture, "Texture was not applied");
    }

    [Test]
    public void DrawMesh()  // White box test 5 : Make sure DrawMesh() from MapDisplay properly draws the mesh
    {
        // Create test texture & mesh
        Texture2D testTexture = new Texture2D(10, 10);
        MeshData testMesh = new MeshData(10, 10);

        // Create blank MapDisplay to apply mesh to
        MapDisplay display = new MapDisplay();

        // Call DrawMesh() with test mesh
        display.DrawTexture(testMesh, testTexture);

        // Make sure mesh was applied properly
        Assert.AreEqual(testMesh, display.sharedMesh.AcquireReadOnlyMeshData(), "Mesh data does not match data of applied mesh");
    }

    [Test]
    public void WB6()
    {
        
    }
  
    [Test]
    public void ColorMatching()  // Black box test 1 : Make sure edge of color map matches expected color based on noise map
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
    public void FalloffMap()  // Black box test 2 : Make sure edge of map falls off and center does not
    {
        int mapSize = 10;

        float[,] falloffMap = FalloffGenerator.GenerateFalloffMap(mapSize);

        Assert.AreEqual(0, falloffMap[mapSize / 2, mapSize / 2], "Center of falloff map falls off");
        Assert.AreEqual(1, falloffMap[0, 0], "Edge of map does not fall off");
    }

    [Test]
    public void BB3()
    {

    }

    [Test]
    public void BB4()
    {
        
    }

    [Test]
    public void BB5()
    {
        
    }

    [Test]
    public void WB6()
    {
        
    }

}
