using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour {
	public GameObject player;
	public GameObject kunai;
	public List<GameObject> items;

	private float jumpCoolDown = 5.0f;
	private float liveCooldown = -1.0f;
	private float shootKunaiCooldown = 2.5f;
	private float shootOtherCooldown = 2.5f;

	// Update is called once per frame
	void Update () {
		if (GameObject.FindObjectOfType<GameManager>().BossLife <= 0) {
			GameObject.FindObjectOfType<GameManager>().Score += 2000;
			player.GetComponent<PlayerScript> ().EndGame(0);
		}

		transform.position = new Vector3 (player.transform.position.x + 8, transform.position.y, 0);


		if (jumpCoolDown >= 0.0f) {
			jumpCoolDown -= Time.deltaTime;
		} else {
			jumpCoolDown = Random.Range(4.0f, 6.0f);
			GetComponent<Rigidbody2D> ().AddForce (Vector2.up * 300);
		}
		if (liveCooldown >= 0.0f) {
			liveCooldown -= Time.deltaTime;
		}

		if (shootKunaiCooldown >= 0.0f) {
			shootKunaiCooldown -= Time.deltaTime;
		} else {
			shootKunaiCooldown = Random.Range(1.0f, 3.0f);
			GameObject newKunai = Instantiate(kunai, new Vector3(transform.position.x, transform.position.y) , Quaternion.identity);
			newKunai.GetComponent<kunaiAimedScript> ().player = gameObject;
		}

		if (shootOtherCooldown >= 0.0f) {
			shootOtherCooldown -= Time.deltaTime;
		} else {
			shootOtherCooldown = Random.Range(2.5f, 5.0f);
			int item = Random.Range (0, items.Count);
			GameObject newItem = Instantiate(items[item], new Vector3(transform.position.x, transform.position.y) , Quaternion.identity);
		}
	}


	private void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Shuriken") {
			if (liveCooldown <= 0.0f) {
				GameObject.FindObjectOfType<GameManager>().Score += 150;
				GameObject.FindObjectOfType<GameManager>().BossLife -= 1;
				liveCooldown = 1.5f;
			}

		}
	}
}
