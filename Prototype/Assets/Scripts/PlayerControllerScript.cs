using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerControllerScript : MonoBehaviour {
	public GameObject bulletPrefab;
	public GameObject lobShotPrefab;

	public float maxSpeed = 10f;
	public float sprintSpeed = 15f;
	public float sprintStaminaDrain = 2f;
	public float jumpForce = 700f;
	public float doubleJumpForce = 350f;
	public float doubleJumpStaminaDrain = 590f; //roughly 10% of 100 stamina -9.95101
	private float jumps = 0;

	public float equippedWeapon;
	public float fireRate;
	private float nextFire;
	private float standardFireRate = 0.3f;
	private float machineFireRate = 0.05f;

	public Transform groundCheck;
	public float groundRadius = 0.2f;
	public LayerMask whatisGround;
	public bool grounded = false;

	/*used for flipping player sprite
	private bool facing = true;
	 */

	private GameObject gameManager;
	//Animator anim;
	private Rigidbody2D rgb2d;
	// Use this for initialization
	void Start () {
		equippedWeapon = 0;
		fireRate = standardFireRate;
		rgb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update(){
		//sideways movement
		float translation = Input.GetAxis ("Horizontal");
		if (translation != 0 && Input.GetKey (KeyCode.LeftShift) && 
		        gameManager.GetComponent<GameManager> ().GetStamina () > 0 && grounded) {
			gameManager.GetComponent<GameManager>().UpdateStamina (-sprintStaminaDrain);
			gameManager.GetComponent<GameManager>().StartStaminaDelay ();
			rgb2d.velocity = new Vector2 (translation * sprintSpeed, rgb2d.velocity.y);
			//Debug.Log (translation * maxSpeed);
		} else if (translation != 0) {
			rgb2d.velocity = new Vector2 (translation * maxSpeed, rgb2d.velocity.y);
		}
		//jumping controls
		if (grounded && Input.GetKeyDown (KeyCode.W)) {
			rgb2d.AddForce (new Vector2 (0, jumpForce));
			jumps++;
		}

		if (!grounded && Input.GetKeyDown (KeyCode.W) && jumps > 0
		            && gameManager.GetComponent<GameManager>().GetStamina() > 10) {
			gameManager.GetComponent<GameManager>().UpdateStamina (-doubleJumpStaminaDrain);
			gameManager.GetComponent<GameManager>().StartStaminaDelay ();
			rgb2d.velocity = new Vector2(0, 5f);
			rgb2d.AddForce (new Vector2 (0, doubleJumpForce));
			jumps = 0;
		}

		//shooting controls
		if (Input.GetButton ("Jump")){
			//flip transform.localRotation depending on which way player is facing
			/*Checks which weapon state we're in before firing. Fire rate will be updated for each weapon*/
			if (equippedWeapon == 0){
				if (Time.time > nextFire){
					nextFire = Time.time + standardFireRate;
					Instantiate (bulletPrefab, transform.localPosition, transform.localRotation);

				}
			} else if (equippedWeapon == 1 && gameManager.GetComponent<GameManager>().GetMachineGunAmmo() > 0){
				if (Time.time > nextFire){
					gameManager.GetComponent<GameManager>().UseMachineGunAmmo ();
					nextFire = Time.time + machineFireRate;
					Instantiate (lobShotPrefab, transform.localPosition, transform.localRotation);
				}
			}
		}

		//switching weapons
		if (Input.GetKeyDown (KeyCode.Alpha1)){
			equippedWeapon = 0;
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)){
			equippedWeapon = 1;
		}
	}

	void FixedUpdate(){
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatisGround);

		//float h = Input.GetAxis ("Horizontal");

		//anim.SetFloat("Speed", Mathf.Abs(h));
	}

	public void SetGameManager(GameObject gameManagerObject){
		gameManager = gameManagerObject;
	}
}
