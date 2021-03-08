using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementRotation : MonoBehaviour
{

    public float speed;
    public bool randomize;
    public bool ticked;
    public float tickRatio = 0.25f;

    float rotation;

    public void Start()
    {
        if (randomize) {
            speed = Random.Range(-speed, speed);
        }
    }

    public void Update()
    {
        rotation += speed * Time.deltaTime;

        if (!ticked)
            transform.localEulerAngles = transform.localEulerAngles.WithZ(rotation);
        else {
            float ratio = 1f / tickRatio;
            float finalRot = (float)(Mathf.FloorToInt(rotation * ratio)) / ratio;
            transform.localEulerAngles = transform.localEulerAngles.WithZ(finalRot);
        }
    }

}