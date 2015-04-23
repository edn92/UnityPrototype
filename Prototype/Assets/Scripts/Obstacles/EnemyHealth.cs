using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
	public float health = 100f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void TakeDamage(float damage){
		health -= damage;
		if (health <= 0) {
			Destroy (gameObject);
		}
	}
}
