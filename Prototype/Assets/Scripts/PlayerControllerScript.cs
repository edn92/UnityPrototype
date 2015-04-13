using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerControllerScript : MonoBehaviour {

	public float moveForce = 10f;
	public float maxSpeed = 1f;
	public float sprintStaminaDrain = 2f;
	public float jumpForce = 700f;
	public float doubleJumpForce = 350f;
	public float doubleJumpStaminaDrain = 590f; //roughly 10% of 100 stamina -9.95101
	public float jumps = 0;

	public Transform groundCheck;
	public float groundRadius = 0.2f;
	public LayerMask whatisGround;
	public bool grounded = false;

	/*used for flipping player sprite
	private bool facing = true;

	 */

	private GameObject gameManager;
	//Animator anim;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update(){
		
		float translation = Input.GetAxis ("Horizontal");
		if(translation != 0){
			GetComponent<Rigidbody2D>().velocity = new Vector2(translation * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
		}
		
		if (Input.GetKey (KeyCode.LeftShift) && gameManager.GetComponent<GameManager> ().GetStamina () > 0) {
			gameManager.GetComponent<GameManager>().UpdateStamina (-sprintStaminaDrain);
			gameManager.GetComponent<GameManager>().StartStaminaDelay ();
			Debug.Log ("Sprinting");
		} 

		if (grounded && Input.GetKeyDown (KeyCode.W)) {
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, jumpForce));
			jumps++;
			Debug.Log("jumping");
		}

		if (!grounded && Input.GetKeyDown (KeyCode.W) && jumps > 0
		            && gameManager.GetComponent<GameManager>().GetStamina() > 10) {
			gameManager.GetComponent<GameManager>().UpdateStamina (-doubleJumpStaminaDrain);
			gameManager.GetComponent<GameManager>().StartStaminaDelay ();
			GetComponent<Rigidbody2D>().velocity = new Vector2(0, 5f);
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, doubleJumpForce));
			jumps = 0;
		}
		/*if (Input.GetKey (KeyCode.LeftShift) && currentStamina > 0) {
			UpdateStamina (staminaDrainRate);
			startStaminaRecharge = Time.time + staminaRechargeDelay;
		} else if (Time.time > startStaminaRecharge){
			UpdateStamina (staminaRechargeRate);
		}*/
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
