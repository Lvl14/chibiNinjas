﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kunaiScript : MonoBehaviour {

	public float velocity;
	public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (player == null) {
			Destroy (gameObject);
		}
		Vector3 pos = transform.position;
		transform.position = new Vector3(pos.x + velocity, pos.y);

		float dist = (transform.position - player.transform.position).magnitude;
		if (dist > 20.0f) {
			Destroy (gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D col){
		if (col.tag != "Enemy" && col.tag != "Live" && col.tag != "Score" && col.tag != "CameraStopMoving" && col.tag != "CameraStopMovingYUp" && col.tag != "CameraStopMovingYDown") {
			Destroy (gameObject);
		}
	}

}
