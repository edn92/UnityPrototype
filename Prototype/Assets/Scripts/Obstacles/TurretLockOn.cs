using UnityEngine;
using System.Collections;

public class TurretLockOn : MonoBehaviour {
	public bool lockedOn;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player") {
			lockedOn = true;
		}
	}
	
	void OnTriggerExit2D(Collider2D col){
		if (col.gameObject.tag == "Player") {
			lockedOn = false;
		}
	}

	public bool GetLockOn(){
		return lockedOn;
	}
}
