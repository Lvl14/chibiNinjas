using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
	private bool dead;
	private bool canJumpFloor = false;
	private bool canJumpPlatform = false;
	private float timeStunt = -1.0f;
	private float shootCooldown = -1.0f;
	private float liveCooldown = -1.0f;

	public AudioClip[] auClip;
	public float velocity = 0.00f;
	public Vector2 mouseClickedData;
	public Vector3 startPoint;
	public GameObject shuriken;

    private void Start()
    {
        dead = false;
        GetComponent<AudioSource>().clip = auClip[0];
		startPoint = transform.position;
    }

    private void Update()
    {
		transform.position = new Vector3 (transform.position.x + (timeStunt <= 0.0f ? velocity : -velocity), transform.position.y, 0);

		if (timeStunt <= 0.0f) {
	        if (Input.GetMouseButtonDown(0) && !dead){
	            //RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
				Vector3 mouseClicked = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				bool isBesidePlayer = mouseClicked.x <= transform.position.x;

				if (isBesidePlayer && (canJumpFloor || canJumpPlatform)) {
					Jump ();
				} else if (!isBesidePlayer && shootCooldown <= 0.0f) {
					// Disparo
					float dX = mouseClicked.x - transform.position.x;
					float dY = mouseClicked.y - transform.position.y;

					float deltaSum = Mathf.Sqrt (dX * dX + dY * dY);

					float deltaX = dX / deltaSum;
					float deltaY = dY / deltaSum;


					mouseClickedData = new Vector2 (deltaX, deltaY);
					shootCooldown = 1.0f;

					GameObject newShuriken = Instantiate(shuriken, new Vector3(transform.position.x, transform.position.y) , Quaternion.identity);
					newShuriken.GetComponent<shurikenScript> ().direction = mouseClickedData;
					newShuriken.GetComponent<shurikenScript> ().player = gameObject;
				}
	        }
		} 

		if (GameObject.FindObjectOfType<GameManager>().Life <= 0) {
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
    }

    private void Jump()
    {
        GetComponent<AudioSource>().Play();
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * 300);
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
			if (col.tag == "Reset") {
				dead = true;
				GetComponent<AudioSource> ().clip = auClip [1];
				GetComponent<AudioSource> ().Play ();
				Invoke ("ResetLevel", 1.5f);
			} 
			if (col.tag == "Stop") {
				canJumpPlatform = true;
				velocity = 0.00f;
				transform.position = new Vector3 (col.transform.position.x - col.GetComponent<BoxCollider2D>().size.x/2 - GetComponent<CircleCollider2D>().radius, transform.position.y, 0);
			} 
			if (col.tag == "Floor") {
				canJumpFloor = true;
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
		} else {
			GetComponent<AudioSource>().clip = auClip[1];
			GetComponent<AudioSource>().Play();
			Invoke("ResetLevel", 1.5f);
		}
	}
		
	private void OnTriggerExit2D(Collider2D col){
		if (col.tag == "Stop"){
			canJumpPlatform = false;
			velocity = 0.03f;
		} 
		if (col.tag == "Floor"){
			canJumpFloor = false;
		}
	}
		
	private void BackToMain()
	{
		SceneManager.LoadScene("MainMenu");
	}

	private void ResetLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	private void AdvanceLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
	}
}