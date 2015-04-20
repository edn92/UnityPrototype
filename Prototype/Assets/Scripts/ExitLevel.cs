using UnityEngine;
using System.Collections;

public class ExitLevel : MonoBehaviour {
	public string levelName;
	private GameObject gameManager;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player"){
			StartCoroutine (ChangeLevel());
		}
	}

	public void SetLevelExit(string level){
		levelName = level;
	}

	public void SetGameManager(GameObject gameManagerObject){
		gameManager = gameManagerObject;
	}

	IEnumerator ChangeLevel(){
		gameManager.GetComponent<GameManager> ().ShowNotification ("Changing levels");
		yield return new WaitForSeconds(3f);

		Application.LoadLevel (levelName);
	}
}
