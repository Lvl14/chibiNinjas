using UnityEngine;
using System.Collections;

public class Autodestroy : MonoBehaviour
{
	public float timeToDestroy = 10.0f;
	private bool activated = true;

	public Autodestroy[] activateOnDestroy;
	private string[] activateOnDestroyTexts;

	// Use this for initialization
	void Start ()
	{		
		if (activateOnDestroy != null) {
			activateOnDestroyTexts = new string[activateOnDestroy.Length];
			for (int i = 0; i < activateOnDestroy.Length; i++) {
				Autodestroy item = activateOnDestroy [i];
				TextMesh tm = item.GetComponent<TextMesh> ();
				if (tm != null) {
					activateOnDestroyTexts [i] = tm.text;
					tm.text = "";
				}
				SpriteRenderer sr = item.GetComponent<SpriteRenderer> ();
				if (sr != null) {
					sr.enabled = false;
				}
			}
		}
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (activated) {
			timeToDestroy -= Time.deltaTime;

			if (timeToDestroy < 0.0f) {
				Destroy (gameObject);
			}
		}
	}

	void OnDestroy() {
		if (activateOnDestroy != null) {
			for (int i = 0; i < activateOnDestroy.Length; i++) {
				Autodestroy item = activateOnDestroy [i];
				TextMesh tm = item.GetComponent<TextMesh> ();
				if (tm != null) {
					tm.text = activateOnDestroyTexts [i];
				}
				item.activated = true;
				SpriteRenderer sr = item.GetComponent<SpriteRenderer> ();
				if (sr != null) {
					sr.enabled = true;
				}
			}
		}
	}
}

