﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityScript : MonoBehaviour {
	private bool falling = true;
	public float floorPos = -1000000.0f;
	public float currvel = 0.0f;
	private float maxVel = -2.0f;
	// Use this for initialization
	void Start () {
		Vector3 pos = transform.position;
		transform.position = pos;
		RaycastHit2D hitGround = Physics2D.Raycast (pos, Vector2.down, 5.0f);
		if (hitGround!=null){
			CircleCollider2D circle = transform.GetComponent<CircleCollider2D> ();
			if (circle != null) {
				floorPos = hitGround.point.y + circle.radius;
			} else {
				BoxCollider2D box = transform.GetComponent<BoxCollider2D> ();
				floorPos = hitGround.point.y + box.size.y/2;
			}
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		Rigidbody2D regidbody = GetComponent<Rigidbody2D> ();
		if (regidbody != null) {
			Vector2 vel = regidbody.velocity;
			currvel = vel.y;
			if (currvel < maxVel) {
				GetComponent<Rigidbody2D>().velocity =  new Vector2(vel.x, maxVel);
			}
		}
	}

	void Update () {
		Vector3 pos = new Vector3(transform.position.x, falling ? transform.position.y : floorPos, 0);
		transform.position = pos;
		RaycastHit2D hitGround = Physics2D.Raycast (pos, Vector2.down, 5.0f);
		if (hitGround!=null){
			CircleCollider2D circle = transform.GetComponent<CircleCollider2D> ();
			if (circle != null) {
				floorPos = hitGround.point.y + circle.radius;
			} else {
				BoxCollider2D box = transform.GetComponent<BoxCollider2D> ();
				floorPos = hitGround.point.y + box.size.y/2;
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Floor") {
			falling = false;
		}
	}
	private void OnTriggerExit2D(Collider2D col){
		if (col.tag == "Floor") {
			falling = true;
		}
	}
}
