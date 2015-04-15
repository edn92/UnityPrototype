using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {
	public GameObject playerPrefab;
	public GameObject healthPickupPrefab;
	private GameObject player;

	public float currentHealth;
	public float maxHealth;
	private Image healthBar;

	public float currentStamina;
	public float maxStamina;
	public float staminaRechargeRate;
	public float staminaRechargeDelay;
	private float startStaminaRecharge;
	private Image staminaBar;

	private Image standardShotImage;
	public int machineGunAmmo;
	private Image machineGunImage;
	private Text machineGunText;

	private Image pauseIcon;
	private bool isPaused;
	// Use this for initialization

	private GameObject gameManager;
	void Start () {
		player = Instantiate (playerPrefab, new Vector2(0, 0), Quaternion.identity) as GameObject;
		player.GetComponent<PlayerControllerScript> ().SetGameManager (this.gameObject);

		healthBar = GameObject.Find ("HealthBar").GetComponent<Image>();
		staminaBar = GameObject.Find ("StaminaBar").GetComponent<Image> ();
		pauseIcon = GameObject.Find ("PauseIcon").GetComponent<Image> ();
		pauseIcon.enabled = false;

		standardShotImage = GameObject.Find ("StandardBullet").GetComponent<Image> ();
		machineGunImage = GameObject.Find ("MachineBullet").GetComponent<Image> ();
		machineGunText = GameObject.Find ("MachineBulletText").GetComponent<Text> ();

		var pickupSpawns = GameObject.FindGameObjectsWithTag ("Pickup");
		foreach (var spots in pickupSpawns){
			GameObject healthPickupClone = Instantiate (healthPickupPrefab, spots.transform.position, spots.transform.rotation) as GameObject;
			healthPickupClone.GetComponent<HealthPickup>().SetGameManager (this.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		healthBar.fillAmount = currentHealth / maxHealth;
		staminaBar.fillAmount = currentStamina / maxStamina;

		machineGunText.text = "" + machineGunAmmo;

		if (Time.time > startStaminaRecharge) {
			UpdateStamina (staminaRechargeRate);
		}

		if (currentStamina < 0.01) {
			currentStamina = 0;
		}

		if (currentStamina > maxStamina) {
			currentStamina = maxStamina;
		}

		if (currentHealth > maxHealth) {
			currentHealth = maxHealth;
		}

		if (Input.GetKeyDown (KeyCode.P)) {
			PauseGame ();
		}
	}

	public void UpdateHealth(float health){
		currentHealth += health;
	}

	public void UpdateStamina(float stamina){
		currentStamina += stamina * Time.deltaTime;
	}

	public float GetStamina(){
		return currentStamina;
	}

	public void StartStaminaDelay(){
		startStaminaRecharge = Time.time + staminaRechargeDelay;
	}

	public void UseMachineGunAmmo(){
		machineGunAmmo--;
	}

	public int GetMachineGunAmmo(){
		return machineGunAmmo;
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
}
