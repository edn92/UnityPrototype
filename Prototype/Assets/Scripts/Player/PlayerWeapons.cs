using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerWeapons : MonoBehaviour {
	public GameObject bulletPrefab;
	public GameObject machineBulletPrefab;

	public float equippedWeapon;
	private float nextFire;
	private float standardFireRate = 0.3f;
	private float machineFireRate = 0.05f;

	public int machineGunAmmo;
	//private RectTransform activeIndicator;
	private Text machineGunText;

	private GameObject gameManager;
	private GameObject weaponPoint;
	// Use this for initialization
	void Start () {
		weaponPoint = transform.Find ("WeaponPoint").gameObject;

		machineGunText = GameObject.Find ("MachineBulletText").GetComponent<Text> ();
		machineGunText.text = "" + machineGunAmmo;

		//activeIndicator = GameObject.Find ("ActiveIndicator").GetComponent<RectTransform>();

		equippedWeapon = 0;
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
						GameObject  bullet = Instantiate (bulletPrefab, weaponPoint.transform.position, transform.localRotation) as GameObject;
						bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(20, 0);
					} else {
						GameObject bullet = Instantiate (bulletPrefab, weaponPoint.transform.position, Quaternion.Euler(0, 0, 180)) as GameObject;
						bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-20, 0);
					}	
				}
			} else if (equippedWeapon == 1 && machineGunAmmo > 0){
				if (Time.time > nextFire){
					machineGunAmmo--;
					nextFire = Time.time + machineFireRate;
					if (GetComponent<PlayerControllerScript>().Facing()){
						GameObject bullet = Instantiate (machineBulletPrefab, weaponPoint.transform.position, transform.localRotation) as GameObject;
						bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(20, 0);
					} else {
						GameObject bullet = Instantiate (machineBulletPrefab, weaponPoint.transform.position, Quaternion.Euler(0, 0, 180)) as GameObject;
						bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-20, 0);
					}
				}
			}
		}

		if (Input.GetKeyUp (KeyCode.G)) {
			Debug.Log ("throwing grenade");
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

	public void AddAmmo(int weaponType, int amount){
		switch (weaponType) {
		case 1:
			machineGunAmmo += amount;
			break;
		}
	}

	public void SetGameManager(GameObject gameManagerObject){
		gameManager = gameManagerObject;
	}
}
