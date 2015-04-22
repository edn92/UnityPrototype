using UnityEngine;
using System.Collections;

public class ElectricFence : MonoBehaviour {
	private bool inContact;
	private float nextZap;
	public float zapRate = 0.1f;
	public float health = 100f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerStay2D(Collider2D col){
		if (col.gameObject.tag == "Player"){
			if (Time.time > nextZap){
				nextZap = Time.time + zapRate;
				col.GetComponent<PlayerHealth>().TakeDamage (1);
			}
		}
	}
}
