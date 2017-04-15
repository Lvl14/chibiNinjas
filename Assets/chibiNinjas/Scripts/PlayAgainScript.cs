using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgainScript : MonoBehaviour {
	public TextMesh scoreLabel;

	// Use this for initialization
	void Start () {
		scoreLabel.text = PlayerPrefs.GetInt ("maxScore").ToString();
	}

	public void LoadLevel(){
		Debug.Log ("Trying to load scene 1");
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}

}
