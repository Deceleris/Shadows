using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetRotation : MonoBehaviour
{
    void Start()
    {
        transform.localEulerAngles = Vector3.zero;
    }

    void OnEnable()
    {
        transform.localEulerAngles = Vector3.zero;
    }

    public void Reset()
    {
        transform.localEulerAngles = Vector3.zero;
    }
}