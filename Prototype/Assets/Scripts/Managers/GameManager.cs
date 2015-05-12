using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {
	//prefab declarations - player, pickups, enemies and obstacles
	public GameObject playerPrefab;

	public GameObject healthPickupPrefab;
	public GameObject machineGunAmmoPrefab;

	public GameObject turretPrefab;
	public GameObject boxPrefab;

	//Exit level stuff
	public GameObject levelExitPrefab;
	public string exitToLevel;

	private Text notificationText;
	private Text scoreText;
	private int totalScore = 0;
	private Image pauseIcon;
	private bool isPaused;
	// Use this for initialization

	private GameObject gameManager;
	void Start () {
		GameObject player = GameObject.FindGameObjectWithTag ("PlayerSpawn") as GameObject;
		player = Instantiate (playerPrefab, player.transform.position, player.transform.rotation) as GameObject;
		player.GetComponent<PlayerControllerScript>().SetGameManager(this.gameObject);

		SetupUI ();
		SpawnHealthPickups ();
		SpawnRandomPickup ();
		SpawnEnemies ();

		GameObject exit = GameObject.FindGameObjectWithTag ("LevelExit") as GameObject;
		exit = Instantiate (levelExitPrefab, exit.transform.position, exit.transform.rotation) as GameObject;
		exit.GetComponent<ExitLevel> ().SetGameManager (this.gameObject);
		exit.GetComponent<ExitLevel> ().SetLevelExit (exitToLevel);
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

	public void UpdateScore(int score){
		totalScore += score;
		scoreText.text = "Score " + totalScore;
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

	public void GameOver(){
		//temporary
		Time.timeScale = 0;

		//bring up gameover screen
	}

	private void SetupUI(){
		notificationText = GameObject.Find ("GameNotifications").GetComponent<Text> ();
		
		scoreText = GameObject.Find ("ScoreText").GetComponent<Text> ();
		scoreText.text = "Score: " + totalScore;
		
		pauseIcon = GameObject.Find ("PauseIcon").GetComponent<Image> ();
	}

	private void SpawnHealthPickups(){
		var healthSpawns = GameObject.FindGameObjectsWithTag ("HealthPickup");
		foreach (var spots in healthSpawns) {
			GameObject healthPickupClone = Instantiate (healthPickupPrefab, 
			                                            spots.transform.position, spots.transform.rotation) as GameObject;
			healthPickupClone.GetComponent<HealthPickup>().SetGameManager (this.gameObject);
		}
	}

	private void SpawnRandomPickup(){
		var pickupSpawns = GameObject.FindGameObjectsWithTag ("Pickup");
		foreach (var spots in pickupSpawns){
			//Random.range max is exclusive not inclusive
			int i = Random.Range(1, 2);

			switch(i){
			case 0:
				GameObject healthPickupClone = Instantiate (healthPickupPrefab, 
                												spots.transform.position, spots.transform.rotation) as GameObject;
				healthPickupClone.GetComponent<HealthPickup>().SetGameManager (this.gameObject);
				break;
			case 1:
				GameObject machineGunAmmoClone = Instantiate (machineGunAmmoPrefab, 
				                                                spots.transform.position, spots.transform.rotation) as GameObject;
				machineGunAmmoClone.GetComponent<MachineGunAmmoPickup>().SetGameManager (this.gameObject);
				break;
			}
		}
	}

	private void SpawnEnemies(){
		//obstacles
		var turretSpawns = GameObject.FindGameObjectsWithTag ("Turret");
		foreach (var spots in turretSpawns) {
			GameObject turretClone = Instantiate (turretPrefab, spots.transform.position, spots.transform.rotation) as GameObject;
			turretClone.GetComponent<EnemyStatus>().SetGameManager (this.gameObject);
		}
		
		var boxSpawns = GameObject.FindGameObjectsWithTag ("Breakable");
		foreach (var spots in boxSpawns) {
			GameObject boxClone = Instantiate (boxPrefab, spots.transform.position, spots.transform.rotation) as GameObject;
			boxClone.GetComponent<EnemyStatus>().SetGameManager (this.gameObject);
		}
		//enemy
	}

	IEnumerator Notify(string notify){
		notificationText.enabled = true;
		notificationText.text = notify;
		yield return new WaitForSeconds(1.5f);
		notificationText.enabled = false;
	}
}
