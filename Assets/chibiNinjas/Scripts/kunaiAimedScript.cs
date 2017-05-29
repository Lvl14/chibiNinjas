using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kunaiAimedScript : MonoBehaviour {

	public float velocity;
	public GameObject player;
	public GameObject playerToAim;

	// Update is called once per frame
	void Update () {
		if (player == null) {
			Destroy (gameObject);
		}
		float dist = (transform.position - player.transform.position).magnitude;

		if (dist > 20.0f) {
			Destroy (gameObject);
		}
		Vector2 direction = Vector2.left;
		if (dist <= 15.0f) {
			float dX = playerToAim.transform.position.x - transform.position.x;
			float dY = playerToAim.transform.position.y - transform.position.y;

			float deltaSum = Mathf.Sqrt (dX * dX + dY * dY);

			float deltaX = dX / deltaSum;
			float deltaY = dY / deltaSum;

			direction = new Vector2 (deltaX, deltaY);
		}

		Vector3 pos = transform.position;
		transform.position = new Vector3(pos.x + velocity*direction.x, pos.y+ velocity*direction.y);

	}

	private void OnTriggerEnter2D(Collider2D col){
		if (col.tag != "Enemy" && col.tag != "FinalBoss" && col.tag != "Live" && col.tag != "Score") {
			Destroy (gameObject);
		}
	}

}
