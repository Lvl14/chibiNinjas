using UnityEngine;

public class GameManager : MonoBehaviour
{
	public GameObject objects;

	public TextMesh scoreLabel;
	public static int score;
	public static int sessionScore = 0;
	public static int life;
	public static int bosslife;
	public static float[] maxCameraX = 	new float[]	{0.0f,	63.5f,	0.0f, 	76.5f,	49.0f,	129.0f,	700.0f};
	public static float[] maxCameraY = 	new float[]	{0.0f,	8.0f,	0.0f,	12.0f,	-4.29f,	120.0f,	100.0f};
	public static float[] minCameraY = 	new float[]	{0.0f,	6.0f,	0.0f,	8.0f,	-20.0f,	77.0f,	6.5f};


	public float[] MaxCameraX {
		get{
			return maxCameraX;
		}
	}	

	public float[] MaxCameraY {
		get{
			return maxCameraY;
		}
	}

	public float[] MinCameraY {
		get{
			return minCameraY;
		}
	}

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
