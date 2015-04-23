using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour {
	public float speed;
	
	private Rigidbody2D rgb2d;
	// Use this for initialization
	void Start () {
		rgb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		rgb2d.AddForce(transform.right * speed);
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Ground") {
			Destroy (gameObject);
		}
		if (col.gameObject.tag == "Obstacle") {
			col.GetComponent<EnemyHealth>().TakeDamage (20);
			Destroy (gameObject);
		}
	}
}
