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
     * Variable mechanics like running and jumping speed
     */
    public const float SPEED		= 1.5f / 16f * 50f; 
	public const float SPEED_MED	= 1.0f / 16f * 50f;
	public const float SPEED_SLOW	= 0.5f / 16f * 50f;


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
	public Transform wallCheck;
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

	void Update() {
		/*
		 * Check for GetKeyDown here so that key events aren't missed in FixedUpdate
	 	 */
		bool jumpKey = Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.RightAlt);
		bool jumpKeyDown = Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.RightAlt);
		if (grounded && jumpKeyDown) {
			jump(false);
		} else if (climbing) {
			if (facingRight) {
				if ((jumpKeyDown && Input.GetKey(KeyCode.LeftArrow)) || 
				    (jumpKey && Input.GetKeyDown(KeyCode.LeftArrow))) {
					jump(true);
					flip();
				}
			} else if ((jumpKeyDown && Input.GetKey(KeyCode.RightArrow)) || 
			           (jumpKey && Input.GetKeyDown(KeyCode.RightArrow))) {
				jump(true);
				flip();
			}
		}
	}

    void FixedUpdate() {
		checkForGround();
		checkForWalls();
		if (climbing) {
			rigidbody2D.Sleep();
		} else {
			handleHorizontalInput();
		}
    }

	/*
	 * Check if we are on the ground or not by casting downward 
	 */
	private void checkForGround() {
		grounded = 
			Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
		// If grounded, ignore collisions with walls
		Physics2D.IgnoreLayerCollision(LAYER_PLAYER, LAYER_WALLS, grounded);
	}

	/*
	 * Check if we are climbing on a wall by casting to the side
	 */ 
	private void checkForWalls() {
		// We are never climbing if we are grounded
		// (Could change if we implement ladders)
		climbing = !grounded && 
			Physics2D.OverlapCircle(wallCheck.position, wallRadius, wallLayer);
	}

	/*
	 * Detect user input and adjust Ryu's horizontal velocity accordingly
	 */
	private void handleHorizontalInput() {
		float velocity = 0f;
		if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)) {
			if (!grounded) {
				running = false;
				if (facingRight)
					velocity = -1 * SPEED_SLOW;
				else
					velocity = -1 * SPEED;
			} else {
				running = true;
				velocity = -1 * SPEED;
				if (facingRight)
					flip();
			}
		} else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow)) {
			if (!grounded) {
				running = false;
				if (!facingRight)
					velocity = SPEED_SLOW;
				else
					velocity = SPEED;
			} else {
				running = true;
				velocity = SPEED;
				if (!facingRight)
					flip();
			}
		} else {
			running = false;
		}
		rigidbody2D.velocity = new Vector2(velocity, rigidbody2D.velocity.y);
	}

	private void jump(bool fromWall) {
		rigidbody2D.AddForce(new Vector2(0, 1500f));
	}

	private void flip() {
		facingRight = !facingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
}


















