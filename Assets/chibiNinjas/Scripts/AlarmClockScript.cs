using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlarmClockScript : MonoBehaviour {

	public float totalTime = 35.0f;
	public TextMesh text;
	public int clockLive = 70;
	public int clockLiveL = 5;
	public int clockLiveR = 5;

	private bool countDown = false;
	private bool broken = false;

	public GameObject clock;
	public Sprite clockB;
	public GameObject clockL;
	public GameObject clockR;

	void Update () {
		totalTime -= Time.deltaTime;
		int time = Mathf.FloorToInt (totalTime);

		if (!countDown && totalTime <= 30.5f) {
			text.transform.position = new Vector3 (475, text.transform.position.y, text.transform.position.z);
			countDown = true;
		}
		if (countDown && totalTime >= 0.0f && clockLive > 0) {
			text.text = time.ToString ();
		}
		if (clockLive <= 0) {
			GameObject.FindObjectOfType<GameManager> ().Score += time*50;
			AdvanceLevel ();
		}
		if (clockLive < 20 && !broken) {
			broken = true;
			clock.GetComponent<UnityEngine.UI.Image> ().sprite = clockB;
		}
		if (totalTime <= 0.0f) {
			ResetLevel ();
		}
	}
		
	public void tapOnLeft (){
		if (countDown) {
			clockLiveL--;
			if (clockLiveL <= 0) {
				Destroy (clockL);
			}
			clockLive -= 5;
			GameObject.FindObjectOfType<GameManager> ().Score+=100;
		}
	}
	public void tapOnRight (){
		if (countDown) {
			clockLiveR--;
			if (clockLiveR <= 0) {
				Destroy (clockR);
			}
			clockLive -= 5;
			GameObject.FindObjectOfType<GameManager> ().Score+=100;
		}
	}
	public void tapOnClock (){
		if (countDown) {
			clockLive--;
			GameObject.FindObjectOfType<GameManager> ().Score+=10;
		}
	}


	private void ResetLevel()
	{
		SaveMaxScore ();
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	private void AdvanceLevel()
	{
		int currentScore = GameObject.FindObjectOfType<GameManager> ().Score;
		GameObject.FindObjectOfType<GameManager> ().ScoreSession = currentScore;
		int lvlScore = currentScore - PlayerPrefs.GetInt ("AcumulatedScore" + (SceneManager.GetActiveScene ().buildIndex - 1));
		PlayerPrefs.SetInt ("AcumulatedScore" + SceneManager.GetActiveScene ().buildIndex, currentScore);
		PlayerPrefs.SetInt ("LevelScore" + SceneManager.GetActiveScene ().buildIndex, lvlScore);

		int maxScoreLvl = PlayerPrefs.GetInt ("maxScore" + SceneManager.GetActiveScene().buildIndex);

		if (lvlScore > maxScoreLvl) {
			PlayerPrefs.SetInt ("maxScore" + SceneManager.GetActiveScene().buildIndex, lvlScore);
			Debug.Log ("Max score for lvl " + SceneManager.GetActiveScene().buildIndex +" is: " + lvlScore);
		} else {
			Debug.Log ("Max score for lvl " + SceneManager.GetActiveScene().buildIndex +" is still: " + maxScoreLvl);
		}

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
	}

	private void SaveMaxScore () { 
		// when die get and update MAX score
		int maxScore = PlayerPrefs.GetInt ("maxScore");
		int currentScore = GameObject.FindObjectOfType<GameManager> ().Score;

		if (currentScore > maxScore) {
			PlayerPrefs.SetInt ("maxScore", currentScore);
			Debug.Log ("Max score is: " + currentScore);
		} else {
			Debug.Log ("Max score is still: " + maxScore);
		}
		// when die get and update MAX score on this lvl
		int maxScoreLvl = PlayerPrefs.GetInt ("maxScore" + SceneManager.GetActiveScene().buildIndex);

		int currentScoreTotal = GameObject.FindObjectOfType<GameManager> ().Score;

		int currentScoreLvlPrev = PlayerPrefs.GetInt ("AcumulatedScore" + (SceneManager.GetActiveScene().buildIndex-1));
		int currentScoreLvl = currentScoreTotal-currentScoreLvlPrev;
		if (currentScoreLvl > maxScoreLvl) {
			PlayerPrefs.SetInt ("maxScore" + SceneManager.GetActiveScene().buildIndex, currentScoreLvl);
			Debug.Log ("Max score for lvl " + SceneManager.GetActiveScene().buildIndex +" is: " + currentScoreLvl);
		} else {
			Debug.Log ("Max score for lvl " + SceneManager.GetActiveScene().buildIndex +" is still: " + maxScoreLvl);
		}
	}

}
