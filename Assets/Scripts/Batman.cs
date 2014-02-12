using UnityEngine;
using System.Collections;

public class Batman : EnemyScript
{

    // Constants
    // =============================================

    /*
     * Layer indecies 
     */
    private const int LAYER_ENEMIES = 11;

    /*
     * Different speeds for different actions
     */
    public const float SPEED = .5f / 16f * 60f;
    public Vector2 vel;

    /*
     * Checks which direction Ryu is then changes the anim to be running in that direction
     */
    void Start()
    {
        GameObject player = GameObject.Find("Ryu");
        float relativePosition = player.transform.position.x - transform.position.x;
        vel = new Vector2(0f, 0f);
        if (relativePosition < 0)
        {
            flip();
            vel.x = -SPEED;
            rigidbody2D.velocity = vel;
        }
        else
        {
            vel.x = SPEED;
            rigidbody2D.velocity = vel;
        }
    }

    void Update()
    {
		if (frozen) {
			vel = Vector2.zero;
			return;
		}

        //Constantly update velocity
        rigidbody2D.velocity = vel;

        //If goes off camera, destroy the object
        GameObject camera = GameObject.Find("Main Camera");
        float relativePosition = transform.position.x - camera.transform.position.x;
        if (Mathf.Abs(relativePosition) > 26 / 3)
            Destroy(transform.gameObject);
    }

	bool wallEntered = false;

    void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.layer == LayerMask.NameToLayer("Enemy Patrol Walls")) {
			if (!wallEntered) {
				wallEntered = true;
				flip();
			}
		}
    }

	void OnTriggerExit2D(Collider2D collider) {
		if (collider.gameObject.layer == LayerMask.NameToLayer("Enemy Patrol Walls")) {
			wallEntered = false;
		}
	}

    private void flip()
	{
		vel.x = -vel.x;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
