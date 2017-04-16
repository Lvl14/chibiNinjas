using UnityEngine;

public class GameManager : MonoBehaviour
{
	public GameObject objects;

	public TextMesh scoreLabel;
	public static int score;
	public static int sessionScore;
	public static int life;
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

	void Start (){
		if (life <= 0) {
			life = 7;
			score = 0;
			sessionScore = 0;
		}
		scoreLabel.text = Score.ToString ();
	}
}
