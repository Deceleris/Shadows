using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class MiscExtensions
{

    public static T Random<T>(this List<T> from)
    {
        return from[UnityEngine.Random.Range(0, from.Count)];
    }

    public static void ScaleWidth(this Image from, float value, float max)
    {
        from.transform.localScale = new Vector3(value / max, 1, 1);
    }

    public static void ClearChilds (this Transform from)
    {
        for (int i = from.childCount - 1; i > 0; i--) {
            GameObject.DestroyImmediate(from.GetChild(i).gameObject);
        }
    }
}