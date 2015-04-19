using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {
	private GameObject player;
	public GameObject playerPrefab;
	public GameObject healthPickupPrefab;

	public GameObject levelExitPrefab;
	public string exitToLevel;

	private Text notificationText;
	private Image pauseIcon;
	private bool isPaused;
	// Use this for initialization

	private GameObject gameManager;
	void Start () {
		player = Instantiate (playerPrefab, new Vector2(0, 0), Quaternion.identity) as GameObject;
		player.GetComponent<PlayerControllerScript> ().SetGameManager (this.gameObject);

		notificationText = GameObject.Find ("GameNotifications").GetComponent<Text> ();
		notificationText.enabled = false;

		pauseIcon = GameObject.Find ("PauseIcon").GetComponent<Image> ();
		pauseIcon.enabled = false;

		var pickupSpawns = GameObject.FindGameObjectsWithTag ("Pickup");
		foreach (var spots in pickupSpawns){
			GameObject healthPickupClone = Instantiate (healthPickupPrefab, spots.transform.position, spots.transform.rotation) as GameObject;
			healthPickupClone.GetComponent<HealthPickup>().SetGameManager (this.gameObject);
		}

		GameObject exit = GameObject.FindGameObjectWithTag ("LevelExit") as GameObject;
		exit = Instantiate (levelExitPrefab, exit.transform.position, exit.transform.rotation) as GameObject;
		exit.GetComponent<ExitLevel> ().SetGameManager (this.gameObject);
		exit.GetComponent<ExitLevel> ().SetLevelExit ("LevelTwo");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.P)) {
			PauseGame ();
		}
	}

	public void ShowNotification(string notify){
		StartCoroutine (Notify (notify));
	}

	public void PauseGame(){
        if(!isPaused){
            Time.timeScale = 0;
            isPaused = true;
            pauseIcon.enabled = true;
        } else {
            Time.timeScale = 1;
            isPaused = false;
            pauseIcon.enabled = false;
        }
	}

	IEnumerator Notify(string notify){
		notificationText.enabled = true;
		notificationText.text = notify;
		yield return new WaitForSeconds(1.5f);
		notificationText.enabled = false;
	}
}
