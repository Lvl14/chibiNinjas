using UnityEngine;


public class ObjectMove : MonoBehaviour 
{
	public float height = 1.5f;
	public float velocity = 0.01f;

	private float maxPos;
	private float minPos;
	private bool up;

	void Start(){
		up = true;
		minPos = transform.position.y;
		maxPos = transform.position.y + height;
	}

	void Update () {
		if (up) {
			transform.position = new Vector3 (transform.position.x, transform.position.y + velocity, transform.position.z);
			if (transform.position.y >= maxPos) {
				up = false;
			}
		} else {
			transform.position = new Vector3 (transform.position.x, transform.position.y - velocity, transform.position.z);
			if (transform.position.y <= minPos) {
				up = true;
			}
		}
	}
}
