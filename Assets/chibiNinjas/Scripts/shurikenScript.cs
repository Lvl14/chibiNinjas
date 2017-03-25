using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shurikenScript : MonoBehaviour {

	public Vector2 start;
	public Vector2 direction;
	public float velocity;
	public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;

		transform.RotateAround (pos, Vector3.forward, Time.deltaTime*1000);
		transform.position = new Vector3(pos.x + direction.x * velocity, pos.y+direction.y * velocity);

		float dist = (transform.position - player.transform.position).magnitude;
		if (dist > 15.0f) {
			Destroy (gameObject);
		}
	}
}
