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
	public float floorPos = 0.0f;

    private void Start()
    {
        dead = false;
        GetComponent<AudioSource>().clip = auClip[0];
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
            } else if (col.tag == "Finish"){
				dead = true;
                GetComponent<AudioSource>().clip = auClip[1];
                GetComponent<AudioSource>().Play();
                Invoke("BackToMain", 1.5f);
			} else if (col.tag == "Stop"){
				canJumpPlatform = true;
				velocity = 0.00f;
			} else if (col.tag == "Floor"){
				canJumpFloor = true;
				falling = false;
				RaycastHit hit;
				if (Physics.Raycast(transform.position, transform.forward, out hit)){
					floorPos = hit.point.y;
				}
			}
        }
    }

	private void OnTriggerInside2D(Collider2D col)
	{
		if (col.tag == "Stop"){
			canJumpPlatform = true;
		} else if (col.tag == "Floor"){
			canJumpFloor = true;
		}
	}

	private void OnTriggerExit2D(Collider2D col){
		if (col.tag == "Stop"){
			canJumpPlatform = false;
			velocity = 0.03f;
		} else if (col.tag == "Floor"){
			canJumpFloor = false;
			falling = true;
		}
	}

    private void BackToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
}