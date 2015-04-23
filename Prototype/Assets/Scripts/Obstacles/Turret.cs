using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {
	public GameObject bulletPrefab;
	public float health = 100f;
	public float fireRate = 0.5f;
	private float nextFire;

	private GameObject lockOnSystem;
	// Use this for initialization
	void Start () {
		lockOnSystem = transform.Find ("LockOnSystem").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (lockOnSystem.GetComponent<TurretLockOn> ().GetLockOn ()) {
			var target = GameObject.FindWithTag ("Player");

			var newRotation = Quaternion.LookRotation (target.transform.position - transform.position, Vector3.right);
			newRotation.x = 0.0f;
			newRotation.y = 0.0f;
			transform.rotation = Quaternion.Slerp (transform.rotation, newRotation, Time.deltaTime * 0.8f);
			//rotate turret to look at player
			if (Time.time > nextFire){
				nextFire = Time.time + fireRate;
				Instantiate(bulletPrefab, transform.localPosition, transform.localRotation);
			}
		}
	}

	public void TakeDamage(float damage){
		health -= damage;
		if (health <= 0) {
			Destroy (gameObject);
		}
	}
}
