using UnityEngine;
using System.Collections;

[System.Serializable]
public class RyuAnimation {
	public string name;
	public Sprite[] frames;
}

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
	public bool grounded = true;
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
	public bool climbing = false;
	public Transform wallCheck;
	public float wallRadius = 0.2f;

	/*
	 * Tells the collider what to consider walls
	 */
	public LayerMask wallLayer;

	// State
	// =====================================

	public bool facingRight = true;

	/*
	 * Array of all of Ryu's different state animations
	 */
	public RyuAnimation[] ryuAnimations;

	void Start() {
	}

	void Update() {
		bool jumpKey = Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.RightAlt);
		if (grounded && jumpKey) {
			// Animate jump
			rigidbody2D.AddForce(new Vector2(0, 1500f));
		}
	}

    void FixedUpdate() {
		/*
		 * Check if we are on the ground or not by casting downward 
		 */
		grounded = 
			Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

		// If grounded, ignore collisions with walls
		Physics2D.IgnoreLayerCollision(LAYER_PLAYER, LAYER_WALLS, grounded);

		/*
		 * Check if we are climbing on a wall by casting to the side
		 */ 
		climbing = !grounded && 
			Physics2D.OverlapCircle(wallCheck.position, wallRadius, wallLayer);
		if (climbing) {
			rigidbody2D.Sleep();
			// Animate climb
		} else {

			/*
		 	 * Handle horizontal movement
		 	 */
			float velocity = 0f;
			if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)) {
				if (!grounded) {
					if (facingRight)
						velocity = -1 * SPEED_SLOW;
					else
						velocity = -1 * SPEED;
				} else {
					velocity = -1 * SPEED;
					if (facingRight)
						flip();
				}
			} else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow)) {
				if (!grounded) {
					if (!facingRight)
						velocity = SPEED_SLOW;
					else
						velocity = SPEED;
				} else {
					velocity = SPEED;
					if (!facingRight)
						flip();
				}
			}
			// Animate idle or running
			rigidbody2D.velocity = new Vector2(velocity, rigidbody2D.velocity.y);
		}
    }

	private void flip() {
		facingRight = !facingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
}


















