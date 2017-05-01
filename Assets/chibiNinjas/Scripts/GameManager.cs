using UnityEngine;

public class GameManager : MonoBehaviour
{
	public GameObject objects;

	public TextMesh scoreLabel;
	public static int score;
	public static int sessionScore = 0;
	public static int life;
	public static int bosslife;
	public int Score
	{
		set
		{
			score = value;
			scoreLabel.text = Score.ToString();
		}
		get
		{
			return score;
		}
	}
	public int Life
	{
		set
		{
			life = value;
		}
		get
		{
			return life;
		}
	}

	public int BossLife
	{
		set
		{
			bosslife = value;
		}
		get
		{
			return bosslife;
		}
	}


	public int ScoreSession
	{
		set
		{
			sessionScore = value;
		}
		get
		{
			return sessionScore;
		}
	}

	void Start (){
		if (life <= 0) {
			life = 7;
			score = sessionScore;
		}
		if (bosslife <= 0) {
			bosslife = 7;
		}
		scoreLabel.text = Score.ToString ();
	}
}
