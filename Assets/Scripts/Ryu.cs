using UnityEngine;
using System.Collections;

public class Ryu : MonoBehaviour {

    // Constants
    // =============================================

    /*
     * Layer indecies 
     */
    private const int LAYER_PLAYER = 8;
    private const int LAYER_WALLS = 9;
    private const int LAYER_GROUND = 10;
    private const int LAYER_ENEMY = 11;
    private const int LAYER_ENEMY_PROJECTILES = 13;

    /*
     * Different speeds for different actions
     */
    public const float SPEED = 1.5f / 16f * 60f;
    public const float SPEED_MED = 1.0f / 16f * 60f;
    public const float SPEED_SLOW = 0.5f / 16f * 60f;
    public const float JUMP_SPEED = 18.6f;
    public const float WALL_JUMP_SPEED = 13;
    public const float INJURED_JUMP_SPEED = 13;

    // Wall checking
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
    public Collider2D feetCollider;
    public GameObject currentGround;

    public bool climbing = false;
    public bool facingRight = true;
    public bool inWall = true;
    public bool crouching = false;
    public bool damaged = false;
    public bool invincible = false;

    public bool attacking = false;
    private int attackFrameCount = 0;

    public GameObject sword;
    private SwordController swordController;

    private BoxCollider2D boxCollider;

    void Start() {
        swordController = sword.GetComponent<SwordController>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update() {
        /*
         * Check for GetKeyDown here so that key events aren't missed in FixedUpdate
         */
        if (!damaged) {
            if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.RightAlt)) {
                // Can only jump when grounded
                if (grounded) {
                    jump(false);
                }
            } else if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.RightShift)) {
                // Can attack from any state except climbing
                if (!climbing) {
                    startAttack();
                }
            }
        }

        checkForWalls();

        if (crouching) {
            swordController.onCrouchStateChanged(true);
            boxCollider.size = new Vector2(boxCollider.size.x, 1.3f);
            boxCollider.center = new Vector2(boxCollider.center.x, 0.85f);
        } else {
            swordController.onCrouchStateChanged(false);
            boxCollider.size = new Vector2(boxCollider.size.x, 1.7f);
            boxCollider.center = new Vector2(boxCollider.center.x, 1.05f);
        }
    }

    void FixedUpdate() {
        if (!damaged) {
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
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == LAYER_GROUND) {
            grounded = true;
            currentGround = collision.gameObject;
            Physics2D.IgnoreLayerCollision(LAYER_PLAYER, LAYER_WALLS, true);
        } else if (collision.gameObject.layer == LAYER_ENEMY) {
            if (!invincible)
                handleDamage(collision.gameObject);
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject == currentGround) {
            grounded = false;
            currentGround = null;
            Physics2D.IgnoreLayerCollision(LAYER_PLAYER, LAYER_WALLS, inWall);
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        switch (collider.gameObject.layer) {
            case LAYER_ENEMY:
                // TODO take hit
                break;
            case LAYER_GROUND:
                feetCollider.enabled = false;
                break;
        }
        switch (collider.gameObject.tag) {
            case "Enemies":
                if (!invincible)
                    handleDamage(collider.gameObject);
                break;
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        switch (collider.gameObject.layer) {
            case LAYER_GROUND:
                feetCollider.enabled = true;
                break;
        }
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
        if ((Input.GetKey(KeyCode.X) && Input.GetKey(KeyCode.LeftArrow)) ||
            (Input.GetKey(KeyCode.RightAlt) && Input.GetKey(KeyCode.LeftArrow))) {
            if (facingRight) {
                jump(true);
                flip();
            }
        } else if ((Input.GetKey(KeyCode.X) && Input.GetKey(KeyCode.RightArrow)) ||
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
        if (attackFrameCount++ == 3) {
            // Done attacking
            swordController.retractSword();
            attacking = false;
            attackFrameCount = 0;
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
        } else if (Input.GetKey(KeyCode.DownArrow)) {
            running = false;
            crouching = true;
        } else {
            running = false;
            crouching = false;
        }
        rigidbody2D.velocity = new Vector2(velocity, rigidbody2D.velocity.y);
    }

    private void jump(bool fromWall) {
        // TODO fix jump from wall velocity
        rigidbody2D.WakeUp();
        Vector2 velocity = rigidbody2D.velocity;
        velocity.y = fromWall ? JUMP_SPEED / 1f : JUMP_SPEED;
        rigidbody2D.velocity = velocity;
    }

    private void startAttack() {
        attacking = true;
        attackFrameCount = 0;
        swordController.extendSword();
    }

    private void flip() {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }


    private void handleDamage(GameObject damageSource) {
        //Set damage states and ignore physics
        damaged = true;
        Physics2D.IgnoreLayerCollision(LAYER_PLAYER, LAYER_ENEMY, true);
        Physics2D.IgnoreLayerCollision(LAYER_PLAYER, LAYER_ENEMY_PROJECTILES, true);

        //Direction where damage source came from
        //Sets recoil appropriately
        float relativePosition = transform.position.x - damageSource.transform.position.x;
        Vector2 vel = new Vector2(0f, 0f);
        if (relativePosition < 0) {
            vel.x = -SPEED_SLOW;
        } else {
            vel.x = SPEED_SLOW;
        }
        vel.y = INJURED_JUMP_SPEED;
        rigidbody2D.velocity = vel;

        //Invoke a function that executes to restore player states
        Invoke("postDamageHandler", .5f);
        Invoke("makeVincible", 2);
    }

    private void postDamageHandler() {
        damaged = false;
        Physics2D.IgnoreLayerCollision(LAYER_PLAYER, LAYER_ENEMY, false);
        Physics2D.IgnoreLayerCollision(LAYER_PLAYER, LAYER_ENEMY_PROJECTILES, false);
    }

    private void makeInvincible() {
        invincible = true;
    }

    private void makeVincible() {
        invincible = false;
    }
}