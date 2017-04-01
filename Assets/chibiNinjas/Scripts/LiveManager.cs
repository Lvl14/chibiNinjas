using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveManager : MonoBehaviour {
	public GameObject player;
	public Sprite live_ok;
	public Sprite live_ko;
	public int live_num;

	private SpriteRenderer renderer;

	// Use this for initialization
	void Start () {
		renderer = GetComponent<SpriteRenderer> ();
		if (renderer.sprite == null) {
			renderer.sprite = live_ok;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (player.GetComponent<PlayerScript> ().life >= live_num && renderer.sprite != live_ok) {
			renderer.sprite = live_ok;
		} else if (player.GetComponent<PlayerScript> ().life < live_num && renderer.sprite != live_ko) {
			renderer.sprite = live_ko;
		}
	}
}
