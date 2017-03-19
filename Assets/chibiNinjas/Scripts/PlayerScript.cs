using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
	private bool dead;
	private bool canJumpFloor = false;
	private bool canJumpPlatform = false;
	private bool falling = true;
	public AudioClip[] auClip;
    public GameObject fire;
	public float velocity = 0.03f;
	public float floorPos = -1000000.0f;

	public Vector3 startPoint;
    private void Start()
    {
        dead = false;
        GetComponent<AudioSource>().clip = auClip[0];
		startPoint = transform.position;
    }

    private void Update()
    {
		
		transform.position = new Vector3(transform.position.x + velocity, falling ? transform.position.y : floorPos, 0);

        if (Input.GetMouseButtonDown(0) && !dead)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

			if (hit.collider == null && (canJumpFloor || canJumpPlatform)){
                Jump();
            }
        }
		RaycastHit2D hitGravity = Physics2D.Raycast(transform.position, -Vector2.up);
		if (hitGravity.collider!=null){
			floorPos = hitGravity.point.y;
		}

    }

    private void Jump()
    {
        fire.SetActive(true);
        GetComponent<AudioSource>().Play();
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * 200);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
		
        if (!dead){
            if (col.tag == "Score"){
                GameObject.FindObjectOfType<GameManager>().Score++;
                Destroy(col.gameObject);
			} 
			if (col.tag == "Finish"){
				dead = true;
				GetComponent<AudioSource>().clip = auClip[1];
				GetComponent<AudioSource>().Play();
				Invoke("BackToMain", 1.5f);
			} 
			if (col.tag == "Reset"){
				dead = true;
				GetComponent<AudioSource>().clip = auClip[1];
				GetComponent<AudioSource>().Play();
				Invoke("ResetLevel", 1.5f);
			} 
			if (col.tag == "Stop"){
				canJumpPlatform = true;
				velocity = 0.00f;
			} 
			if (col.tag == "Floor"){
				canJumpFloor = true;
				falling = false;
			}
        }
	}
		
	private void OnTriggerExit2D(Collider2D col){
		if (col.tag == "Stop"){
			canJumpPlatform = false;
			velocity = 0.03f;
		} 
		if (col.tag == "Floor"){
			falling = true;
			canJumpFloor = false;
		}
	}

	private void BackToMain()
	{
		SceneManager.LoadScene("MainMenu");
	}

	private void ResetLevel()
	{
		SceneManager.LoadScene("Game");
	}
}