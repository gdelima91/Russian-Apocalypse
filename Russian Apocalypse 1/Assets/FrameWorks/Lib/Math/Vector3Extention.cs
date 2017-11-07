using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Extention {

    public static Vector3 RandomPos(this Vector3 vector3,float from,float to)
    {
        return new Vector3(Random.Range(from, to), Random.Range(from, to), Random.Range(from, to));
    }

    public static Vector3 RandomPosXZ(this Vector3 vector3, float from, float to)
    {
        return new Vector3(Random.Range(from, to), 0, Random.Range(from, to));
    }
}
