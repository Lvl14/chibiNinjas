using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraAspect : MonoBehaviour 
{
	public GameObject player;

	void Start () {
		Camera.main.aspect = 16/10f;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		Time.timeScale = 1;
	}

	public void Update() {
		cameraFunc ();
	}

	public void FixedUpdate() {
		cameraFunc ();
	}

	private void cameraFunc(){
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
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
	}
}
