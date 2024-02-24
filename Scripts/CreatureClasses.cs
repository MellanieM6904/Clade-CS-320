using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body() {
    List<Vector3> vertices;
    List<int> triangles;
    List<Bone> bones;
}

public class Bone {
    int num;
    Vector3 position;
    BoneWeight weight;
}

public class BoneWeight {
    int boneIndex;
    float weight;
}