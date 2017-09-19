using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticRotationScript : MonoBehaviour {

	public float rotationVelocity = 60.0f;
	public Vector3 rotationDirection = Vector3.up;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		transform.RotateAround (transform.position, rotationDirection, Time.deltaTime*rotationVelocity);
	}
}
