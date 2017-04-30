using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
	private bool dead;
	private bool canJumpFloor = false;
	private bool canJumpPlatform = false;
	private bool canSuperJump = false;
	private bool jumpDiagonalRight = false;
	private bool jumpDiagonalLeft = false;
	private float timeStunt = -1.0f;
	private float shootCooldown = -1.0f;
	private float liveCooldown = -1.0f;
	private float grabCooldown = -1.0f;
	private GameObject grabbed = null;
	private int floorTransition = 0;
	private int stopRTransition = 0;
	private int stopLTransition = 0;


	public bool breakStopped = false;
	public float velocity = 0.00f;
	public Vector2 mouseClickedData;
	public Vector3 startPoint;
	public GameObject shuriken;

    private void Start()
    {
		grabbed = null;
        dead = false;
		startPoint = transform.position;
    }

    private void Update()
    {
		transform.position = new Vector3 (transform.position.x + (timeStunt <= 0.0f ? velocity : -velocity), transform.position.y, 0);

		if (grabbed != null) {
			grabbed.transform.position = new Vector3 (grabbed.transform.position.x + velocity, grabbed.transform.position.y, 0);
		}

		if (timeStunt <= 0.0f) {
	        if (Input.GetMouseButtonDown(0) && !dead){
	            //RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
				Vector3 mouseClicked = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				bool isBesidePlayer = mouseClicked.x <= transform.position.x;

				if (isBesidePlayer && (canJumpFloor || canJumpPlatform || jumpDiagonalLeft || jumpDiagonalRight)) {
					Jump ();
				} else if (!isBesidePlayer && shootCooldown <= 0.0f) {
					// Disparo
					float dX = mouseClicked.x - transform.position.x;
					float dY = mouseClicked.y - transform.position.y;

					float deltaSum = Mathf.Sqrt (dX * dX + dY * dY);

					float deltaX = dX / deltaSum;
					float deltaY = dY / deltaSum;


					mouseClickedData = new Vector2 (deltaX, deltaY);
					shootCooldown = 0.5f;

					GameObject newShuriken = Instantiate(shuriken, new Vector3(transform.position.x, transform.position.y) , Quaternion.identity);
					newShuriken.GetComponent<shurikenScript> ().direction = mouseClickedData;
					newShuriken.GetComponent<shurikenScript> ().player = gameObject;
				}
	        }
		} 
		if (!dead) {
			if (GameObject.FindObjectOfType<GameManager> ().Life <= 0) {
				dead = true;
			}
			if (shootCooldown >= 0.0f) {
				shootCooldown -= Time.deltaTime;
			}
			if (timeStunt >= 0.0f) {
				timeStunt -= Time.deltaTime;
			}
			if (liveCooldown >= 0.0f) {
				liveCooldown -= Time.deltaTime;
			}
			if (grabCooldown >= 0.0f) {
				grabCooldown -= Time.deltaTime;
			}
		} 
		if (dead) {
			Invoke("ResetLevel", 1.5f);
		}
    }

    private void Jump()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		if (canSuperJump) {
			GetComponent<Rigidbody2D> ().AddForce (Vector2.up * 900);
		} else if (jumpDiagonalRight){
			GetComponent<Rigidbody2D> ().AddForce (new Vector2(0.3f,1.0f) * 400);
		}else if (jumpDiagonalLeft){
			GetComponent<Rigidbody2D> ().AddForce (new Vector2(-0.3f,1.0f) * 400);
		}else {
			GetComponent<Rigidbody2D> ().AddForce (Vector2.up * 300);
		}
		if (!canJumpPlatform) {
			JumpRestrict ();
		}
    }

	private void JumpRestrict(){
		canJumpFloor = false;
		canJumpPlatform = false;
		canSuperJump = false;
		jumpDiagonalRight = false;
		jumpDiagonalLeft = false;
		floorTransition = 0;
		stopRTransition = 0;
		stopLTransition = 0;

	}


	private void OnTriggerEnter2D(Collider2D col)
	{

		if (!dead) {
			if (col.tag == "Score") {
				GameObject.FindObjectOfType<GameManager> ().Score++;
				Destroy (col.gameObject);
			} 
			if (col.tag == "Finish") {
				Invoke("AdvanceLevel", 1.5f);
			} 
			if (col.tag == "Jumper") {
				canSuperJump = true;
			} 
			if (col.tag == "Reset") {
				GameObject.FindObjectOfType<GameManager> ().Life = 0;
			} 
			if (col.tag == "Stop") {
				canJumpPlatform = true;
				velocity = 0.00f;
				transform.position = new Vector3 (col.transform.position.x - col.GetComponent<BoxCollider2D>().size.x/2 - GetComponent<BoxCollider2D>().size.x/2, transform.position.y, 0);
			} 
			if (col.tag == "StopRight") {
				GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				stopRTransition++;
				jumpDiagonalLeft = true;
				velocity = 0.00f;
				transform.position = new Vector3 (col.transform.position.x - col.GetComponent<BoxCollider2D>().size.x/2 - GetComponent<BoxCollider2D>().size.x/2, transform.position.y, 0);
			}
			if (col.tag == "StopLeft") {
				stopLTransition++;
				GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				jumpDiagonalRight = true;
				velocity = 0.00f;
				transform.position = new Vector3 (col.transform.position.x + col.GetComponent<BoxCollider2D>().size.x/2, transform.position.y, 0);
			}
			if (col.tag == "StopGrab" && grabbed != null) {
				canJumpPlatform = true;
				velocity = 0.00f;
				transform.position = new Vector3 (col.transform.position.x - col.GetComponent<BoxCollider2D>().size.x/2 - GetComponent<BoxCollider2D>().size.x/2, transform.position.y, 0);
				grabbed = null;
			} 
			if (col.tag == "Grab" && grabCooldown < 0.0f) {
				grabbed = col.gameObject;
			} 
			if (col.tag == "Break") {
				breakStopped = true;
				velocity = 0.00f;
				transform.position = new Vector3 (col.transform.position.x - col.GetComponent<BoxCollider2D>().size.x/2 - GetComponent<BoxCollider2D>().size.x/2, transform.position.y, 0);
			} 
			if (col.tag == "Floor") {
				GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				canJumpFloor = true;
				velocity = 0.03f;
				++floorTransition;
			}
			if (col.tag == "Roof") {
				GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0.0f);
			}
			if (col.tag == "kunai") {
				GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
				GetComponent<Rigidbody2D> ().AddForce (Vector2.up * 50);
				timeStunt = 1.0f;
				if (liveCooldown <= 0.0f) {
					GameObject.FindObjectOfType<GameManager>().Life -= 2;
					liveCooldown = 0.5f;
				}

			}
			if (col.tag == "Enemy") {
				GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
				GetComponent<Rigidbody2D> ().AddForce (Vector2.up * 100);
				timeStunt = 1.0f;
				if (liveCooldown <= 0.0f) {
					GameObject.FindObjectOfType<GameManager>().Life -= 1;
					liveCooldown = 0.5f;
				}
			}
			if (col.tag == "Live" && GameObject.FindObjectOfType<GameManager>().Life < 7) {
				GameObject.FindObjectOfType<GameManager>().Life += 1;
			}
		}
	}

	private void OnTriggerExit2D(Collider2D col){
		if (col.tag == "Stop" || col.tag == "Grab"){
			canJumpPlatform = false;
			velocity = 0.03f;
		} 

		if (col.tag == "StopRight") {
			stopRTransition--;
			if (stopRTransition == 0) {
				canJumpPlatform = false;
				jumpDiagonalLeft = false;
			}
		}
		if (col.tag == "StopLeft") {
			stopLTransition--;
			if (stopLTransition == 0) {
				canJumpPlatform = false;
				jumpDiagonalRight = false;
			}
		}


		if (col.tag == "Grab" || col.tag == "StopGrab"){
			grabCooldown = 3.0f;
		} 

		if (col.tag == "Break"){
			velocity = 0.03f;
			breakStopped = false;
		} 
		if (col.tag == "Floor"){
			--floorTransition;
			if (floorTransition == 0) {
				canJumpFloor = false;
			}
		}
		if (col.tag == "Jumper") {
			canSuperJump = false;
		} 
	}
		
	private void BackToMain()
	{
		SceneManager.LoadScene("MainMenu");
	}

	private void ResetLevel()
	{
		SaveMaxScore ();
		PlayerPrefs.SetInt ("currentLevel", SceneManager.GetActiveScene ().buildIndex);
		SceneManager.LoadScene (0);
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