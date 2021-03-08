using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class Parallax : MonoBehaviour
{
	// ======================================================== VARIABLES

	[Header("References")]

	[Header("Settings")]
	public float globalDistanceFactor = 1;
	public float globalDistanceRotFactor = 1;
	public float rotationSpeed = 2;

	[Header ("Elements")]
	public List<ParalalxElement> paralaxElements = new List<ParalalxElement> ();

	[System.Serializable]
	public class ParalalxElement
	{
		public GameObject pivot;
		public float distance;
		public float rotationAmplitude;
		[HideInInspector] public Vector2 startPosition;
	}

	// Bookmarkers
	float enableTime;
	Vector2 rotDirection;

        
	// ======================================================== PARRALLAX

	public void Start()
	{
		foreach (ParalalxElement element in paralaxElements) {
			element.startPosition = element.pivot.transform.position;
		}

		enableTime = Time.time;
	}

	void Update () 
    {
		// Parallax
		Vector2 mousePos = Mouse.current.position.ReadValue();
        float x = (mousePos.x / Screen.width) * 2 - 1;
        float y = (mousePos.y / Screen.height) * 2 - 1;
        Vector2 direction = new Vector2(x, y);

        float t = (Time.time - enableTime) * rotationSpeed;
        float xf = Mathf.Cos(t) * 2;
        float yf = Mathf.Sin(t * 2) / 2;
        rotDirection = new Vector2(xf, yf);

        foreach (ParalalxElement element in paralaxElements) {
            element.pivot.transform.position =
                element.startPosition +
                direction * element.distance * globalDistanceFactor +
                rotDirection * element.rotationAmplitude * globalDistanceRotFactor;
        }
    }
}