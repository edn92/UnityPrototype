using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {
	public GameObject bulletPrefab;
	//public Transform target;

	public bool lockedOn;
	public float fireRate = 0.5f;
	private float nextFire;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
		if (lockedOn) {
			var target = GameObject.FindWithTag ("Player");

			var newRotation = Quaternion.LookRotation (target.transform.position - transform.position, Vector3.right);
			newRotation.x = 0.0f;
			newRotation.y = 0.0f;
			transform.rotation = Quaternion.Slerp (transform.rotation, newRotation, Time.deltaTime * 0.8f);

			/*Vector3 dir = target.position - transform.position;
			float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);*/
			//transform.rotation = Quaternion.Slerp (transform.rotation, angle, Time.deltaTime * 2);

			//Debug.Log (target.position);
			//Debug.Log (lookDir);
			//rotate turret to look at player
			if (Time.time > nextFire){
				nextFire = Time.time + fireRate;
				Instantiate(bulletPrefab, transform.localPosition, transform.rotation);
				//instantiate bullet
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player") {
			Debug.Log ("In range");
			lockedOn = true;
		}
	}

	void OnTriggerExit2D(Collider2D col){
		if (col.gameObject.tag == "Player") {
			Debug.Log ("Out of range");
			lockedOn = false;
		}
	}
}
