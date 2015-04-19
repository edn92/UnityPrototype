using UnityEngine;
using System.Collections;

public class LaunchScreenManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton("Cancel")){
			Application.LoadLevel ("LevelOne");
		}
	}
}
