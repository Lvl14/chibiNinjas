using UnityEngine;


public class ObjectMove : MonoBehaviour 
{
	public GameObject player;
	public float offsetX;
	public float offsetY;

	void Update () {
		if (player != null) {
			transform.position = new Vector3 (player.transform.position.x + offsetX, player.transform.position.y + offsetY, player.transform.position.z);
		}
	}
}
