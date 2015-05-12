using UnityEngine;
using System.Collections;

public class EnemyStatus : MonoBehaviour {
	public float health = 100f;
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

	public void TakeDamage(float damage){
		health -= damage;
		if (health <= 0) {
			Destroy (gameObject);
			gameManager.GetComponent<GameManager>().UpdateScore (score);
		}
	}
}
