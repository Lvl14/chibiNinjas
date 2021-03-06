﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shurikenScript : MonoBehaviour {

	public Vector2 direction;
	public float velocity;
	public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		transform.position = new Vector3(pos.x + direction.x * velocity, pos.y+direction.y * velocity);

		float dist = (transform.position - player.transform.position).magnitude;
		if (dist > 20.0f) {
			Destroy (gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Break") {
			PlayerScript scr = player.GetComponent<PlayerScript> ();
			if (scr.breakStopped) {
				scr.velocity = 0.03f;
			}
		}

		if (col.tag != "Player" && col.tag != "Live" && col.tag != "Score" && col.tag != "StopLeft" && col.tag != "CameraStopMoving" && col.tag != "CameraStopMovingYUp" && col.tag != "CameraStopMovingYDown") {
			Destroy (gameObject);
		}
	}

}
