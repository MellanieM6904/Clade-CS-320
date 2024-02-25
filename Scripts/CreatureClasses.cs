using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body: MonoBehavior {
    List<Vector3> vertices;
    List<int> triangles;
    List<Bone> bones;
}

public class Bone: MonoBehavior {
    int num;
    Vector3 position;
    new BoneWeight weight;
}

public class BoneWeight: MonoBehavior {
    int boneIndex;
    float weight;
}

public class Pattern: MonoBehavior {
    Material design;
    Texture texture;
    string colorA;
    string colorB;
}

public class Part: MonoBehavior {
    new Pattern color;
    List<Vector3> vertices;
    List<int> triangles;
    string partType;
    string partName;
}

public class Creature: MonoBehavior {
    new Pattern color;
    List<Part> parts;
    // implement stats class from Koby here
    new Body body;
}