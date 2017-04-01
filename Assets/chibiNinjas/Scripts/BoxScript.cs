using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour {
	public GameObject  prize;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Shuriken") {
			GameObject newPrize = Instantiate(prize, transform.position, Quaternion.identity);
			GameObject.FindObjectOfType<GameManager>().Score += 5;
			Destroy (gameObject);
			Destroy (col.gameObject);
		}
	}

}
