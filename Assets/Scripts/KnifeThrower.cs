using UnityEngine;
using System.Collections;

public class KnifeThrower : EnemyScript, AnimationController<KnifeThrower>.AnimationListener
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
    public const float JUMP = 12f;
    public Vector2 vel;

    // State
    // =====================================
    public bool attackInvoked = false;
    public bool attacking = false;
    public bool running = true;
    public bool crouching = false;

    //Knife
    public Transform knifePrefab;

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
		GetComponent<KnifeThrowerAnimationController>().setAnimationListener(this);
    }

	void AnimationController<KnifeThrower>.AnimationListener.onAnimationRepeat(int animationIndex) {
		switch (animationIndex) {
		case KnifeThrowerAnimationController.ANIM_ATTACK:
		case KnifeThrowerAnimationController.ANIM_CROUCH_ATTACK:
			finishAttacking();
			break;
		}
	}

    void Update()
    {
		if (frozen) {
			vel = Vector2.zero;
			return;
		}

        //If goes off camera, destroy the object
        GameObject camera = GameObject.Find("Main Camera");
        float relativePosition = transform.position.x - camera.transform.position.x;
        if (Mathf.Abs(relativePosition) > 26 / 3)
            Destroy(transform.gameObject);
    }

    void FixedUpdate()
    {
        //Attacks every 2 seonds if attacking bool
        if (!attackInvoked)
        {
            Invoke("attack", 2);
            attackInvoked = true;
        }

        if (running)
            rigidbody2D.velocity = vel;
        else
            rigidbody2D.velocity = new Vector2(0f, 0f);
    }

    void attack()
    {
        //Set states
        attacking = true;
        running = false;
        //Random Crouching
        if (Random.value < .5)
            crouching = true;
        else
            crouching = false;


        //Face the direction Ryu is at
        GameObject player = GameObject.Find("Ryu");
        float relativePosition = player.transform.position.x - transform.position.x;
        if (relativePosition < 0 && vel.x == SPEED)
        {
            flip();
            vel.x = -SPEED;
        }
        else if (relativePosition > 0 && vel.x == -SPEED)
        {
            flip();
            vel.x = SPEED;
        }

        //Launch Projectile
        launchProjectile();
    }

    void finishAttacking()
    {
        //Set states
        running = true;
        attacking = false;
        crouching = false;

        //Set attacking bool to false so we invoke another attack command
        attackInvoked = false;
    }

    void launchProjectile()
    {
        float knifeHorizontalOffset, knifeVerticalOffset;

        if(vel.x > 0)
            knifeHorizontalOffset = 1;
        else
            knifeHorizontalOffset = -1;

        if (crouching)
            knifeVerticalOffset = 2.3f - 1.375f;
        else
            knifeVerticalOffset = 2.6f - 1.375f;

        Transform knife = Instantiate(knifePrefab, new Vector3(transform.position.x + knifeHorizontalOffset, transform.position.y + knifeVerticalOffset, 0), Quaternion.identity) as Transform;
        knife.parent = transform;
    }

    private void flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
