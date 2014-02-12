using UnityEngine;
using System.Collections;

public class Boxer : EnemyScript {

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
    public const float SPEED_FAST = 2f / 16f * 60f;
    public const float JUMP = 9f;
    public const float HIGH_JUMP = 15f;
    public Vector2 vel;

    // State
    // =====================================
    public bool attackInvoked = false;
    public bool attacking = false;
    public bool running = true;
    private bool ryuStationary;
    private float previousFrameRyuPosition;

    void Start() {
        //Checks which direction Ryu is then changes the anim to be running in that direction
        GameObject player = GameObject.Find("Ryu");
        float relativePosition = player.transform.position.x - transform.position.x;
        vel = new Vector2(0f, 0f);
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
            GetComponent<BoxerAnimationController>().animate = false;
            vel = Vector2.zero;
            return;
        } else {
            GetComponent<BoxerAnimationController>().animate = true;
        }

        //If goes off camera, destroy the object
        GameObject camera = GameObject.Find("Main Camera");
        float relativePosition = transform.position.x - camera.transform.position.x;
        if (Mathf.Abs(relativePosition) > 26 / 3)
            Destroy(transform.gameObject);
    }

    void FixedUpdate() {
        if (frozen) {
            GetComponent<BoxerAnimationController>().animate = false;
            vel = Vector2.zero;
            return;
        } else {
            GetComponent<BoxerAnimationController>().animate = true;
        }

        //Checks which direction Ryu is then changes the anim to be running in that direction
        GameObject player = GameObject.Find("Ryu");
        float relativePosition = player.transform.position.x - transform.position.x;
        if (player.transform.position.x == previousFrameRyuPosition)
            ryuStationary = true;
        else
            ryuStationary = false;

        //Attacks every 2 seonds if within a close enough range
        if (!attackInvoked && Mathf.Abs(relativePosition) < 3) {
            Invoke("attack", .75f);
            attackInvoked = true;
        }

        vel = rigidbody2D.velocity;
        if (ryuStationary && Mathf.Abs(relativePosition) < .2) {
            vel.x = 0;
        } else if (attacking) {
            BoxCollider2D collider = (BoxCollider2D)transform.gameObject.GetComponent(typeof(BoxCollider2D));
            collider.size = new Vector2(1.5f, 2);
            if (Mathf.Abs(relativePosition) > .8) {
                if (relativePosition < 0 && vel.x > 0) {
                    flip();
                    vel.x = -SPEED_FAST;
                } else if (relativePosition > 0 && vel.x < 0) {
                    flip();
                    vel.x = SPEED_FAST;
                } else {
                    if (vel.x > 0)
                        vel.x = SPEED_FAST;
                    else
                        vel.x = -SPEED_FAST;
                }
            }
        } else {
            BoxCollider2D collider = (BoxCollider2D)transform.gameObject.GetComponent(typeof(BoxCollider2D));
            collider.size = new Vector2(.875f, 2);
            if (Mathf.Abs(relativePosition) > .8) {
                if (relativePosition < 0 && vel.x > 0) {
                    flip();
                    vel.x = -SPEED;
                } else if (relativePosition > 0 && vel.x < 0) {
                    flip();
                    vel.x = SPEED;
                } else {
                    if (vel.x > 0)
                        vel.x = SPEED;
                    else
                        vel.x = -SPEED;
                }
            }
        }
        rigidbody2D.velocity = vel;
        previousFrameRyuPosition = player.transform.position.x;
    }

    void attack() {
        //Set states
        attacking = true;
        running = false;

        vel = rigidbody2D.velocity;
        vel.y = JUMP;
        rigidbody2D.velocity = vel;
        //Invoke a finish attack command
        Invoke("finishAttacking", .4f);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "Ground") {
            vel = rigidbody2D.velocity;
            vel.y = HIGH_JUMP;
            rigidbody2D.velocity = vel;
        }
    }

    void finishAttacking() {
        //Set states
        running = true;
        attacking = false;

        //Set attacking bool to false so we invoke another attack command
        attackInvoked = false;
    }

    private void flip() {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
