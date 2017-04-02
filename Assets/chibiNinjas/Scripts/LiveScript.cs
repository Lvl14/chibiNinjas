using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveScript : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	private void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Player") {
			GameObject.FindObjectOfType<GameManager>().Score += 1;
			Destroy (gameObject);
		}
	}
}
