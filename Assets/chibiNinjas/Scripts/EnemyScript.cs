using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
	public int life = 5;

	private bool dead;
	private float liveCooldown = -1.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (life <= 0) {
			dead = true;
		}
		if (liveCooldown >= 0.0f) {
			liveCooldown -= Time.deltaTime;
		}
	}


	private void OnTriggerEnter2D(Collider2D col)
	{

		if (!dead) {
			if (col.tag == "Player") {
				if (liveCooldown <= 0.0f) {
					--life;
					liveCooldown = 0.5f;
				}
			}
			if (col.tag == "Shuriken") {
				GameObject.FindObjectOfType<GameManager>().Score += 20;
				Destroy (gameObject);
			}
		} else {
			GameObject.FindObjectOfType<GameManager>().Score += 10;
			Destroy (gameObject);
		}
	}
}
