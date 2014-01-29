using UnityEngine;
using System.Collections;

public class Ryu : MonoBehaviour {

	// Constants
	// =============================================

	/*
	 * Layer indecies 
	 */
	private const int LAYER_PLAYER	= 8;
	private const int LAYER_WALLS	= 9;

    /*
     * Different speeds for different actions
     */
    public const float SPEED		= 1.5f / 16f * 60f; 
	public const float SPEED_MED	= 1.0f / 16f * 60f;
	public const float SPEED_SLOW	= 0.5f / 16f * 60f;
	public const float JUMP_SPEED	= 19.5f;


	// Ground raycasting
	// ============================================

	/*
	 * true if Ryu is on some ground, false otherwise
	 */
	public Transform groundCheck;
	public float groundRadius = 0.2f;

	/*
	 * Tells the collider what to consider ground
	 */
	public LayerMask groundLayer;


	// Wall raycasting
	// ============================================

	/*
	 * true if Ryu is on a wall, false otherwise
	 */
	public Transform wallCheckFront;
	public Transform wallCheckAbove;
	public float wallRadius = 0.2f;

	/*
	 * Tells the collider what to consider walls
	 */
	public LayerMask wallLayer;


	// State
	// =====================================

	public bool running = false;
	public bool grounded = true;
	public bool climbing = false;
	public bool facingRight = true;
	public bool inWall = true;
	public bool crouching = false;
	public bool attacking = false;
	private int attackFrameCount = 0;

	void Update() {
		/*
		 * Check for GetKeyDown here so that key events aren't missed in FixedUpdate
	 	 */
		if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.RightAlt)) {
			// Can only jump when grounded
			if (grounded) {
				jump(false);
			}
		} else if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.RightShift)) {
			// Can attack from any state except climbing
			if (!climbing) {
				attacking = true;
				// TODO attack
			}
		}
	}

    void FixedUpdate() {
		checkForGround();
		checkForWalls();
		if (climbing) {
			rigidbody2D.Sleep();
			handleWallJump();
		} else if (attacking) {
			handleAttack();
		} else {
			// Can only move horizontally if not climbing or attacking
			handleInput();
		}
    }

	/*
	 * Check if we are on the ground or not by casting downward 
	 */
	private void checkForGround() {
		grounded = 
			Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
		Physics2D.IgnoreLayerCollision(LAYER_PLAYER, LAYER_WALLS, grounded || inWall);
	}

	/*
	 * Check if we are climbing on a wall by casting to the side
	 */ 
	private void checkForWalls() {
		// We are never climbing if we are grounded
		// (Could change if we implement ladders)
		inWall = 
			Physics2D.OverlapCircle(wallCheckAbove.position, wallRadius, wallLayer);
		climbing = !grounded && !inWall &&
			Physics2D.OverlapCircle(wallCheckFront.position, wallRadius, wallLayer);
	}

	/*
	 * Tests for the appropriate conditions to initiate a wall jump
	 */
	private void handleWallJump() {
		if ((Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.LeftArrow)) || 
		    (Input.GetKey(KeyCode.RightAlt) && Input.GetKey(KeyCode.LeftArrow))) {
			if (facingRight) {
				jump(true);
				flip();
			}
		} else if ((Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.RightArrow)) || 
		           (Input.GetKey(KeyCode.RightAlt) && Input.GetKey(KeyCode.RightArrow))) {
			if (!facingRight) {
				jump(true);
				flip();
			}
		}
	}

	/*
	 * Handles keeping track of how long Ryu has been attacking
	 */
	private void handleAttack() {
		if (attackFrameCount == 1) {
			// TODO extend attacking collision box
		} else {
			// TODO retract collision box
			if (attackFrameCount == 3) {
				// Done attacking
				attacking = false;
				attackFrameCount = 0;
			}
		}
	}

	/*
	 * Detect user input and adjust Ryu's horizontal velocity accordingly
	 */
	private void handleInput() {
		float velocity = 0f;
		if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)) {
			if (!grounded) {
				running = false;
				crouching = false;
				velocity = -1 * (facingRight ? SPEED_SLOW : SPEED);
			} else {
				running = true;
				crouching = false;
				velocity = -1 * SPEED;
				if (facingRight) flip();
			}
		} else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow)) {
			if (!grounded) {
				running = false;
				crouching = false;
				velocity = facingRight ? SPEED : SPEED_SLOW;
			} else {
				running = true;
				crouching = false;
				velocity = SPEED;
				if (!facingRight) flip();
			}
		} else if (Input.GetKey(KeyCode.DownArrow)){
			running = false;
			crouching = true;
			// TODO make collision box smaller
		} else {
			running = false;
			crouching = false;
		}
		rigidbody2D.velocity = new Vector2(velocity, rigidbody2D.velocity.y);
	}

	private void jump(bool fromWall) {
		rigidbody2D.WakeUp();
		Vector2 velocity = rigidbody2D.velocity;
		velocity.y = fromWall ? JUMP_SPEED / 2f : JUMP_SPEED;
		rigidbody2D.velocity = velocity;
	}

	private void startAttack() {
		attacking = true;
		attackFrameCount = 0;
	}

	private void flip() {
		facingRight = !facingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
}


















