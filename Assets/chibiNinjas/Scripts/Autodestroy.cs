using UnityEngine;
using System.Collections;

public class Autodestroy : MonoBehaviour
{
	public float timeToDestroy = 10.0f;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		timeToDestroy -= Time.deltaTime;

		if (timeToDestroy < 0.0f) {
			Destroy (gameObject);
		}
	}
}

