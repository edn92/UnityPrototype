using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerControllerScript : MonoBehaviour {
	public float maxSpeed = 10f;
	public float sprintSpeed = 15f;
	public float sprintStaminaDrain = 2f;
	public float jumpForce = 700f;
	public float doubleJumpForce = 350f;
	public float doubleJumpStaminaDrain = 590f; //roughly 10% of 100 stamina -9.95101
	private float jumps = 0;

	public Transform groundCheck;
	public float groundRadius = 0.2f;
	public LayerMask whatisGround;
	public bool grounded = false;

	public bool facingRight = true;
	
	private PlayerWeapons playerWeapons;
	private PlayerHealth playerHealth;

	private GameObject gameManager;
	//Animator anim;
	private Rigidbody2D rgb2d;
	private Animator animator;
	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator> ();
		rgb2d = GetComponent<Rigidbody2D> ();
		//not really used atm
		playerWeapons = GetComponent<PlayerWeapons> ();
		playerWeapons.SetGameManager (gameManager);

		playerHealth = GetComponent<PlayerHealth> ();
		playerHealth.SetGameManager (gameManager);
	}
	
	// Update is called once per frame
	void Update(){
		//sideways movement
		float translation = Input.GetAxis ("Horizontal");
		if (translation < 0 && facingRight) {
			Flip();

		} else if (translation > 0 && !facingRight) {
			Flip();
		}

		if (translation == 0) {
			animator.SetInteger ("State", 0);
		}

		if (translation != 0 && Input.GetKey (KeyCode.LeftShift) && 
		        GetComponent<PlayerHealth> ().GetStamina () > 0 && grounded) {
			animator.SetInteger ("State", 2);
			playerHealth.UpdateStamina (-sprintStaminaDrain);
			playerHealth.StartStaminaDelay ();
			rgb2d.velocity = new Vector2 (translation * sprintSpeed, rgb2d.velocity.y);
			//Debug.Log (translation * maxSpeed);
		} else if (translation != 0) {
			animator.SetInteger ("State", 1);
			rgb2d.velocity = new Vector2 (translation * maxSpeed, rgb2d.velocity.y);
		}

		//jumping controls
		if (grounded && Input.GetKeyDown (KeyCode.W)) {
			animator.SetInteger ("State", 2);
			rgb2d.AddForce (new Vector2 (0, jumpForce));
			jumps++;
		}

		if (!grounded && Input.GetKeyDown (KeyCode.W) && jumps > 0
		    && GetComponent<PlayerHealth> ().GetStamina() > 10) {
			playerHealth.UpdateStamina (-doubleJumpStaminaDrain);
			playerHealth.StartStaminaDelay ();
			rgb2d.velocity = new Vector2(0, 5f);
			rgb2d.AddForce (new Vector2 (0, doubleJumpForce));
			jumps = 0;
		}
	}

	void FixedUpdate(){
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatisGround);
	}

	public void SetGameManager(GameObject gameManagerObject){
		gameManager = gameManagerObject;
	}

	void Flip(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public bool Facing(){
		return facingRight;
	}
}
