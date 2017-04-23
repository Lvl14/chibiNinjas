using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabScript : MonoBehaviour {

	public bool isGrababble = true;

	private void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "GrabStop") {
			isGrababble = false;
		}
	}

}
