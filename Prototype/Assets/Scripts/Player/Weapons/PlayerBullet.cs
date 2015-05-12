using UnityEngine;
using System.Collections;
using System.Linq;

public class PlayerBullet : MonoBehaviour {
	public float speed;
	
	private Rigidbody2D rgb2d;
	// Use this for initialization
	void Start () {
		rgb2d = GetComponent<Rigidbody2D> ();
		//rgb2d.velocity = new Vector2(10, 0);
	}
	
	// Update is called once per frame
	void Update () {
		rgb2d.AddForce(transform.right * speed);
	}

	void OnTriggerEnter2D(Collider2D col){
		string[] tags = new string[]{"Turret", "Fence", "Breakable"};
		if (col.gameObject.tag == "Ground") {
			Destroy (gameObject);
		}
		if (tags.Contains (col.gameObject.tag)){
			col.GetComponent<EnemyStatus>().TakeDamage (20);
			Destroy (gameObject);
		}
	}

	/*public void SetVelocity(int vel){
		rgb2d.velocity = new Vector2 (vel, 0);
	}*/
}
