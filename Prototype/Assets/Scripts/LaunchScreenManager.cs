using UnityEngine;
using System.Collections;

public class LaunchScreenManager : MonoBehaviour {
	//attached to buttons
	public void StartGame(){
		Application.LoadLevel ("LevelOne");
	}

	public void DiplayHelp(){
		Debug.Log ("Help");
	}

	public void ExitGame(){
		Debug.Log ("Exiting game");
	}
}
