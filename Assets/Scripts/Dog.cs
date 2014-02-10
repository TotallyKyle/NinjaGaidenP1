using UnityEngine;
using System.Collections;

public class Dog : EnemyScript {

    // Constants
    // =============================================

    /*
     * Layer indecies 
     */
    private const int LAYER_ENEMIES = 11;

    /*
     * Different speeds for different actions
     */
    public const float SPEED = 1.75f / 16f * 60f;
    public const float JUMP = 9f;
    public Vector2 vel;

	public bool grounded = true;
	public BoxCollider2D feetCollider;

    /*
     * Checks which direction Ryu is then changes the anim to be running in that direction
     */
    void Start() {
        GameObject player = GameObject.Find("Ryu");
        float relativePosition = player.transform.position.x - transform.position.x;
        vel = new Vector2(0f, JUMP);
        if (relativePosition < 0) {
            flip();
            vel.x = -SPEED;
            rigidbody2D.velocity = vel;
        } else {
            vel.x = SPEED;
            rigidbody2D.velocity = vel;
        }
    }

    void Update() {
		if (frozen) {
			vel = Vector2.zero;
			return;
		}

		if (grounded) {
			vel.y = JUMP;
			rigidbody2D.velocity = vel;
			grounded = false;
		}

		feetCollider.enabled = rigidbody2D.velocity.y <= 0;

        //If goes off camera, destroy the object
        GameObject camera = GameObject.Find("Main Camera");
        float relativePosition = transform.position.x - camera.transform.position.x;
        if (Mathf.Abs(relativePosition) > 26 / 3)
            Destroy(transform.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider) {
		if (collider.name != "Ryu") {
			flip();
			vel.x = -vel.x;
			rigidbody2D.velocity = vel;
        }
    }

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) {
			if (!grounded)
				grounded = true;
		}
	}

	void OnCollisionExit2D(Collision2D collision) {
		if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) {
			if (grounded)
				grounded = false;
		}
	}

	private void flip() {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
