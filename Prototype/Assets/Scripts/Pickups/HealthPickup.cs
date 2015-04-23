﻿using UnityEngine;
using System.Collections;

public class HealthPickup : MonoBehaviour {
	public float addHealth = 20f;
	private GameObject player;
	private GameObject gameManager;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetGameManager(GameObject gameManagerObject){
		gameManager = gameManagerObject;
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player") {
			col.GetComponent<PlayerHealth>().UpdateHealth (addHealth);
			gameManager.GetComponent<GameManager> ().ShowNotification ("Health picked up!");
			Destroy (gameObject);
		}
	}
}