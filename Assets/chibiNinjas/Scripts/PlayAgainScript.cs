using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgainScript : MonoBehaviour {
	public TextMesh scoreLabel;
	public TextMesh scoreLabel1;
	public TextMesh scoreLabel2;
	public TextMesh scoreLabel3;
	public TextMesh scoreLabel1Boss;
	public TextMesh scoreLabel2Boss;
	public TextMesh scoreLabel3Boss;

	// Use this for initialization
	void Start () {
		scoreLabel.text = PlayerPrefs.GetInt ("maxScore").ToString();
		scoreLabel1.text = PlayerPrefs.GetInt ("maxScore1").ToString();
		scoreLabel2.text = PlayerPrefs.GetInt ("maxScore3").ToString();
		scoreLabel3.text = PlayerPrefs.GetInt ("maxScore5").ToString();
		scoreLabel1Boss.text = PlayerPrefs.GetInt ("maxScore2").ToString();
		scoreLabel2Boss.text = PlayerPrefs.GetInt ("maxScore4").ToString();
		scoreLabel3Boss.text = PlayerPrefs.GetInt ("maxScore6").ToString();
	}

	public void LoadLevel(){
		Debug.Log ("Trying to load scene 1");
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
		PlayerPrefs.SetInt ("AcumulatedScore0", 0);
		PlayerPrefs.SetInt ("LevelScore0", 0);
	}

	public void ResetScores(){
		PlayerPrefs.SetInt ("maxScore",0);
		PlayerPrefs.SetInt ("maxScore1",0);
		PlayerPrefs.SetInt ("maxScore2",0);
		PlayerPrefs.SetInt ("maxScore3",0);
		PlayerPrefs.SetInt ("maxScore4",0);
		PlayerPrefs.SetInt ("maxScore5",0);
		PlayerPrefs.SetInt ("maxScore6",0);


		scoreLabel.text = PlayerPrefs.GetInt ("maxScore").ToString();
		scoreLabel1.text = PlayerPrefs.GetInt ("maxScore1").ToString();
		scoreLabel2.text = PlayerPrefs.GetInt ("maxScore3").ToString();
		scoreLabel3.text = PlayerPrefs.GetInt ("maxScore5").ToString();
		scoreLabel1Boss.text = PlayerPrefs.GetInt ("maxScore2").ToString();
		scoreLabel2Boss.text = PlayerPrefs.GetInt ("maxScore4").ToString();
		scoreLabel3Boss.text = PlayerPrefs.GetInt ("maxScore6").ToString();

	}

}
