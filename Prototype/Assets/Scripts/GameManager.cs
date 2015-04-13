using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {
	public GameObject playerPrefab;
	public GameObject healthPickupPrefab;
	private GameObject player;
	
	public float numHealthPickups = 1;

	public float currentHealth;
	public float maxHealth;
	private Image healthBar;

	public float currentStamina;
	public float maxStamina;
	public float staminaRechargeRate;
	public float staminaRechargeDelay;
	private float startStaminaRecharge;
	private Image staminaBar;
	// Use this for initialization

	private GameObject gameManager;
	void Start () {
		player = Instantiate (playerPrefab, new Vector2(0, 0), Quaternion.identity) as GameObject;
		player.GetComponent<PlayerControllerScript> ().SetGameManager (this.gameObject);

		healthBar = GameObject.Find ("HealthBar").GetComponent<Image>();
		staminaBar = GameObject.Find ("StaminaBar").GetComponent<Image> ();

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

		//UpdateStamina (staminaRechargeRate);
		/*if (Input.GetKey (KeyCode.LeftShift) && currentStamina > 0) {
			UpdateStamina (staminaDrainRate);
			startStaminaRecharge = Time.time + staminaRechargeDelay;
		} else if (Time.time > startStaminaRecharge){
			UpdateStamina (staminaRechargeRate);
		}*/

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
}
