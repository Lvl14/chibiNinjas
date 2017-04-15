using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNinjaScript : MonoBehaviour {
	public int life = 5;
	public GameObject kunai;

	private bool dead;
	private float liveCooldown = -1.0f;
	private float shootKunaiCooldown = 5.0f;

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
		if (shootKunaiCooldown >= 0.0f) {
			shootKunaiCooldown -= Time.deltaTime;
		} else {
			shootKunaiCooldown = 3.0f;
			GameObject newKunai = Instantiate(kunai, new Vector3(transform.position.x, transform.position.y) , Quaternion.identity);
			newKunai.GetComponent<kunaiScript> ().player = gameObject;

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
				GameObject.FindObjectOfType<GameManager>().Score += 40;
				Destroy (gameObject);
			}
		} else {
			GameObject.FindObjectOfType<GameManager>().Score += 20;
			Destroy (gameObject);
		}
	}

}
