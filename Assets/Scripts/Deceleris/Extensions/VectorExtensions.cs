using System.Collections;
using System.Collections.Generic;using UnityEngine;

public static class VectorExtensions
{
    // -------------------------------------------------- VECTOR 3

    public static Vector3 AddX(this Vector3 from, float x) { from.x += x; return from; }
    public static Vector3 AddY(this Vector3 from, float y) { from.y += y; return from; }
    public static Vector3 AddZ(this Vector3 from, float z) { from.z += z; return from; }
    public static Vector3 WithX(this Vector3 from, float x) { return new Vector3(x, from.y, from.z); }
    public static Vector3 WithY(this Vector3 from, float y) { return new Vector3(from.x, y, from.z); }
    public static Vector3 WithZ(this Vector3 from, float z) { return new Vector3(from.x, from.y, z); }

    public static Vector3Int ToInt(this Vector3 from)
    {
        return new Vector3Int(
            Mathf.RoundToInt(from.x - .5f),
            Mathf.RoundToInt(from.y - .5f),
            Mathf.RoundToInt(from.z - .5f)
        );
    }

    public static Vector2Int ToV2I(this Vector3 from)
    {
        return new Vector2Int(
            Mathf.RoundToInt(from.x - .5f),
            Mathf.RoundToInt(from.y - .5f)
        );
    }

    // -------------------------------------------------- VECTOR 2

    public static Vector2 AddX(this Vector2 from, float x) { from.x += x; return from; }
    public static Vector2 AddY(this Vector2 from, float y) { from.y += y; return from; }

    public static Vector3Int ToV3I(this Vector2Int from, int z = 0) { return new Vector3Int(from.x, from.y, z); }

    // -------------------------------------------------- VECTOR 4

    public static Color WithA(this Color from, float a) { return new Color(from.r, from.g, from.b, a); }


}