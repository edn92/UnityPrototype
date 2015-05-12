using UnityEngine;
using System.Collections;

public class MachineGunAmmoPickup : MonoBehaviour {
	//public int addAmmo;
	public int score;
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
			int ammoAmount = Random.Range (20, 30);
			col.GetComponent<PlayerWeapons>().AddAmmo (1, ammoAmount);
			//col.GetComponent<PlayerHealth>().UpdateHealth (addAmmo);
			gameManager.GetComponent<GameManager> ().ShowNotification ("Found Maching Gun ammo!");
			gameManager.GetComponent<GameManager> ().UpdateScore (score);
			Destroy (gameObject);
		}
	}
}
