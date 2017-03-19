using UnityEngine;


public class CameraAspect : MonoBehaviour 
{
	public Vector3 offset;

	void Start ()
	{
		Camera.main.aspect = 16/10f;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		Time.timeScale = 1;
	}
	private void Update()
	{
		//transform.position = new Vector3(transform.position.x + velocity, transform.position.y , 0);
	}
	

	void LateUpdate()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
}
