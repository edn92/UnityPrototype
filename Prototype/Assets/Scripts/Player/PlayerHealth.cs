using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	public float currentHealth;
	public float maxHealth;
	private Image healthBar;
	
	public float currentStamina;
	public float maxStamina;
	public float staminaRechargeRate;
	public float staminaRechargeDelay;
	private float startStaminaRecharge;
	private Image staminaBar;

	private GameObject gameManager;
	// Use this for initialization
	void Start () {
		healthBar = GameObject.Find ("HealthBar").GetComponent<Image>();
		staminaBar = GameObject.Find ("StaminaBar").GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		healthBar.fillAmount = currentHealth / maxHealth;
		staminaBar.fillAmount = currentStamina / maxStamina;
		
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

	public void SetGameManager(GameObject gameManagerObject){
		gameManager = gameManagerObject;
	}
}
