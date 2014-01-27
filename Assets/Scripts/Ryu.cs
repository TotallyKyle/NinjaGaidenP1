using UnityEngine;
using System.Collections;

public class Ryu : MonoBehaviour
{
    /*
     * Variable mechanics like running and jumping speed
     */
    public float mSpeed = 1.5f / 16f * 60f; 
	public float mSlowSpeed = 0.5f / 16f * 60f;
    public float jumpSpeed = 8.92142857f;

    //Conditions that Ryu is experiencing
    public bool climbing = false;
    public bool grounded = true;

    //Constants
    const int playerLAYER = 8;
    const int wallLAYER = 9;

	private Animator mAnimator;

	/*
	 * true if the player is on some ground, false otherwise
	 */
	public bool mGrounded = true;
	public Transform mGroundCheck;
	public float mGroundRadius = 0.2f;

	/*
	 * Tells the collider what to consider ground
	 */
	public LayerMask mGroundLayer;

	public bool mFacingRight = true;

	void Start() {
		mAnimator = GetComponent<Animator>();
	}

	void Update() {
		bool jumpKey = Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.RightAlt);
		if (mGrounded && jumpKey) {
			mAnimator.SetBool("Grounded", false);
			rigidbody2D.AddForce(new Vector2(0, 1000f));
		}
	}

    void FixedUpdate() {
		/*
		 * Check if we are on the ground or not by casting downward 
		 */
		mGrounded = Physics2D.OverlapCircle(mGroundCheck.position, mGroundRadius, mGroundLayer);
		mAnimator.SetBool("Grounded", mGrounded);

		/*
		 * Handle horizontal movement
		 */
		float velocity = 0f;
		if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)) {
			if (!mGrounded) {
				if (mFacingRight)
					velocity = -1 * mSlowSpeed;
				else
					velocity = -1 * mSpeed;
			} else {
				velocity = -1 * mSpeed;
				if (mFacingRight)
					flip();
			}
		} else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow)) {
			if (!mGrounded) {
				if (!mFacingRight)
					velocity = mSlowSpeed;
				else
					velocity = mSpeed;
			} else {
				velocity = mSpeed;
				if (!mFacingRight)
					flip();
			}
		}
		mAnimator.SetFloat("Speed", Mathf.Abs(velocity));
		rigidbody2D.velocity = new Vector2(velocity, rigidbody2D.velocity.y);

        //Ignoring wall collisions when Ryu is grounded
        if (grounded)
            Physics2D.IgnoreLayerCollision(playerLAYER, wallLAYER, true);
        else
            Physics2D.IgnoreLayerCollision(playerLAYER, wallLAYER, false);
    }

	private void flip() {
		mFacingRight = !mFacingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground")
            grounded = true;
        else if (other.tag == "Wall")
        {
            climbing = true;
            rigidbody2D.Sleep();
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Ground")
            grounded = false;
    }
}


















