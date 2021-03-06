﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LiveManager : MonoBehaviour {
	public bool isplayer;
	public Sprite live_ok;
	public Sprite live_ko;
	public int live_num;

	private SpriteRenderer renderer = new SpriteRenderer();

	// Use this for initialization
	void Start () {
		renderer = GetComponent<SpriteRenderer> ();
		if (renderer.sprite == null) {
			renderer.sprite = live_ok;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (isplayer) {
			if (GameObject.FindObjectOfType<GameManager> ().Life >= live_num && renderer.sprite != live_ok) {
				renderer.sprite = live_ok;
			} else if (GameObject.FindObjectOfType<GameManager> ().Life < live_num && renderer.sprite != live_ko) {
				renderer.sprite = live_ko;
			}
		} else {
			if (GameObject.FindObjectOfType<GameManager> ().BossLife >= live_num && renderer.sprite != live_ok) {
				renderer.sprite = live_ok;
			} else if (GameObject.FindObjectOfType<GameManager> ().BossLife  < live_num && renderer.sprite != live_ko) {
				renderer.sprite = live_ko;
			}
		}
	}
}
