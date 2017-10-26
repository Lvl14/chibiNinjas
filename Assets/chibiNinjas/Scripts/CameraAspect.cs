using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraAspect : MonoBehaviour 
{
	public GameObject player;

	private float x_difference = 9.0f;
	private float y_difference = 1.0f;
	private float z_difference = -12.5f;

	void Start () {
		Camera.main.aspect = 16/10f;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		Time.timeScale = 1;
		cameraFunc ();
	}

	//public void Update() {
	//	cameraFunc ();
	//}

	//public void FixedUpdate() {
	//	cameraFunc ();
	//}

	private void cameraFunc(){
		transform.position = new Vector3(player.transform.position.x + x_difference,player.transform.position.y + y_difference,player.transform.position.z + z_difference);
		Vector3 position = transform.position;
		if (transform.position.x >= GameObject.FindObjectOfType<GameManager> ().MaxCameraX[SceneManager.GetActiveScene().buildIndex]) {
			position.x = GameObject.FindObjectOfType<GameManager> ().MaxCameraX[SceneManager.GetActiveScene().buildIndex];
		}

		if (transform.position.y >= GameObject.FindObjectOfType<GameManager> ().MaxCameraY [SceneManager.GetActiveScene ().buildIndex]) {
			position.y = GameObject.FindObjectOfType<GameManager> ().MaxCameraY [SceneManager.GetActiveScene ().buildIndex];
		} else if (transform.position.y <= GameObject.FindObjectOfType<GameManager> ().MinCameraY [SceneManager.GetActiveScene ().buildIndex]) {
			position.y = GameObject.FindObjectOfType<GameManager> ().MinCameraY [SceneManager.GetActiveScene ().buildIndex];
		} else{
			position.y = player.transform.position.y + 1.0f;
		}
		transform.position = position;
	}
	

	void LateUpdate() {
		cameraFunc ();
	}
}
