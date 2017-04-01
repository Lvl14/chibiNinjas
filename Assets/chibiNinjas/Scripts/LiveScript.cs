using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveScript : MonoBehaviour {
	private bool falling = true;
	private float floorPos = -1000000.0f;

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
	}

	private void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Floor") {
			falling = false;
		}
		if (col.tag == "Player") {
			GameObject.FindObjectOfType<GameManager>().Score += 1;
			Destroy (gameObject);
		}
	}
}
