using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityScript : MonoBehaviour {
	public float floorPos = -1000000.0f;
	private float currvel = 0.0f;
	private float maxVel = -2.0f;
	// Use this for initialization
	void Start () {
		Vector3 pos = transform.position;
		pos.y -= 0.1f;
		RaycastHit2D hitGround = Physics2D.Raycast (pos, Vector2.down, 50.0f);
		if (hitGround.collider!=null){
			BoxCollider2D box = transform.GetComponent<BoxCollider2D> ();
			floorPos = hitGround.point.y + box.size.y / 2.0f * transform.lossyScale.y;
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		Rigidbody2D regidbody = GetComponent<Rigidbody2D> ();
		if (regidbody != null) {
			Vector2 vel = regidbody.velocity;
			currvel = vel.y;
			if (currvel < maxVel) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (vel.x, maxVel);
			} else if (transform.position.y <= floorPos) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (0.0f, 0.0f);
			}
		}
	}

	void LateUpdate () {
		Vector3 pos = new Vector3(transform.position.x, transform.position.y > floorPos ? transform.position.y : floorPos, 0.0f);
		transform.position = pos;
		pos.y -= 0.1f;
		RaycastHit2D hitGround = Physics2D.Raycast (pos, Vector2.down, 50.0f);
		if (hitGround.collider!=null){
			BoxCollider2D box = transform.GetComponent<BoxCollider2D> ();
			floorPos = hitGround.point.y + box.size.y / 2.0f * transform.lossyScale.y;
		}
	}
}
