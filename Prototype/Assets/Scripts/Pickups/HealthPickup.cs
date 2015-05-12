using UnityEngine;
using System.Collections;

public class HealthPickup : MonoBehaviour {
	private float addHealth;
	//public int score;
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
			addHealth = Random.Range (15, 20);
			col.GetComponent<PlayerHealth>().UpdateHealth (addHealth);
			gameManager.GetComponent<GameManager> ().ShowNotification ("Found " + addHealth + "HP!");
			Destroy (gameObject);
		}
	}
}
