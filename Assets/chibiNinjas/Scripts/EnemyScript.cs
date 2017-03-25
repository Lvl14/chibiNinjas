﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
	private bool falling = true;
	public float floorPos = -1000000.0f;
	private bool dead;
	private int life = 5;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 pos = new Vector3(transform.position.x, falling ? transform.position.y : floorPos, 0);
		transform.position = pos;
		RaycastHit2D hitGround = Physics2D.Raycast (pos, Vector2.down, 3.5f);
		if (hitGround!=null){
			CircleCollider2D circle = transform.GetComponent<CircleCollider2D> ();
			floorPos = hitGround.point.y + circle.radius;
		}
		if (life <= 0) {
			dead = true;
		}
	}


	private void OnTriggerEnter2D(Collider2D col)
	{

		if (!dead) {
			if (col.tag == "Floor") {
				falling = false;
			}
			if (col.tag == "Player") {
				life--;
			}
			if (col.tag == "Shuriken") {
				GameObject.FindObjectOfType<GameManager>().Score += 20;
				Destroy (gameObject);
				Destroy (col.gameObject);
			}
		} else {
			GameObject.FindObjectOfType<GameManager>().Score += 10;
			Destroy (gameObject);
		}
	}
}
