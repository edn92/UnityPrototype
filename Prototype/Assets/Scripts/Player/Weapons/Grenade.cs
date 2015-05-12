using UnityEngine;
using System.Collections;

public class Grenade : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke ("Explode", 1.5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Explode(){
		Debug.Log ("Explode");
		Destroy (gameObject);
	}
}
