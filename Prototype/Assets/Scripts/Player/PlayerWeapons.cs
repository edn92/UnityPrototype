using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerWeapons : MonoBehaviour {
	public GameObject bulletPrefab;
	public GameObject machineBulletPrefab;

	public float equippedWeapon;
	public float fireRate;
	private float nextFire;
	private float standardFireRate = 0.3f;
	private float machineFireRate = 0.05f;

	private Image standardShotImage;
	public int machineGunAmmo;
	private Image machineGunImage;
	private Text machineGunText;

	private GameObject gameManager;
	// Use this for initialization
	void Start () {
		standardShotImage = GameObject.Find ("StandardBullet").GetComponent<Image> ();
		machineGunImage = GameObject.Find ("MachineBullet").GetComponent<Image> ();
		machineGunText = GameObject.Find ("MachineBulletText").GetComponent<Text> ();
		machineGunText.text = "" + machineGunAmmo;

		equippedWeapon = 0;
		fireRate = standardFireRate;
	}
	
	// Update is called once per frame
	void Update () {
		machineGunText.text = "" + machineGunAmmo;
		//shooting controls
		if (Input.GetButton ("Jump")){
			//flip transform.localRotation depending on which way player is facing
			/*Checks which weapon state we're in before firing. Fire rate will be updated for each weapon
			  Need to create an offset from where the bullet is instantiated attached to the player*/
			if (equippedWeapon == 0){
				if (Time.time > nextFire){
					nextFire = Time.time + standardFireRate;
					if (GetComponent<PlayerControllerScript>().Facing()){
						Instantiate (bulletPrefab, transform.localPosition, transform.localRotation);
					} else {
						Instantiate (bulletPrefab, transform.localPosition, Quaternion.Euler(0, 0, 180));
					}	
				}
			} else if (equippedWeapon == 1 && machineGunAmmo > 0){
				if (Time.time > nextFire){
					UseMachineGunAmmo();
					nextFire = Time.time + machineFireRate;
					if (GetComponent<PlayerControllerScript>().Facing()){
						Instantiate (machineBulletPrefab, transform.localPosition, transform.localRotation);
					} else {
						Instantiate (machineBulletPrefab, transform.localPosition, Quaternion.Euler(0, 0, 180));
					}
				}
			}
		}
		
		//switching weapons
		if (Input.GetKeyDown (KeyCode.Alpha1) && equippedWeapon != 0){
			equippedWeapon = 0;
			gameManager.GetComponent<GameManager>().ShowNotification ("Standard weapon equipped!");
		}
		if (Input.GetKeyDown (KeyCode.Alpha2) && equippedWeapon != 1){
			equippedWeapon = 1;
			gameManager.GetComponent<GameManager>().ShowNotification ("Special weapon equipped!");
		}
	}

	public void UseMachineGunAmmo(){
		machineGunAmmo--;
	}
	
	/*public int GetMachineGunAmmo(){
		return machineGunAmmo;
	}*/
	public void SetGameManager(GameObject gameManagerObject){
		gameManager = gameManagerObject;
	}
}
