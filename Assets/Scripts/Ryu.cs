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
    private const int LAYER_ITEMS = 14;

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

    public bool inWall = true;
    public bool climbing = false;
    public Transform wallCheckFront;
    public Transform wallCheckAbove;
    public float wallRadius = 0.2f;
    public LayerMask wallLayer;

    // Ground checking
    // ============================================

    public bool grounded = true;
    public Transform groundCheck;
    public Transform groundCheck2;
    public LayerMask groundLayer;

    // State
    // =====================================

    public bool running = false;
    public bool ascending = false;
    public bool facingRight = true;
    public bool crouching = false;
    public bool damaged = false;
    public bool invincible = false;

    public bool attacking = false;
    private int attackFrameCount = 0;

    public bool casting = false;
    private int castingFrameCount = 0;

    public GameObject sword;
    private SwordController swordController;

    // Items
    // =====================================

    public ItemScript item;

    // Game Data
    // =====================================
    public GameData gameData;

    void Start() {
        swordController = sword.GetComponent<SwordController>();
        gameData.scoreData = 0;
        gameData.timerData = 150;
        gameData.healthData = 16;
        gameData.spiritData = 0;
        gameData.livesData = 2;
    }

    void Update() {
        if (!damaged) {
            if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.RightAlt)) {
                // Can only jump when grounded
                if (grounded) {
                    jump(false);
                }
            } else if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.RightShift)) {
                // Can attack from any state except climbing
                if (!climbing) {
                    if (Input.GetKey(KeyCode.UpArrow) && !running && grounded && !casting) {
                        startCasting();
                    } else if (!attacking) {
                        startAttack();
                    }
                }
            }
        }

        grounded = Physics2D.OverlapArea(groundCheck.position, groundCheck2.position, groundLayer);

        ascending = rigidbody2D.velocity.y > 0;

        Physics2D.IgnoreLayerCollision(LAYER_PLAYER, LAYER_GROUND, ascending);

        inWall = Physics2D.OverlapCircle(wallCheckAbove.position, wallRadius, wallLayer);

        Physics2D.IgnoreLayerCollision(LAYER_PLAYER, LAYER_WALLS, grounded || (!grounded && inWall));

        climbing = !grounded && !inWall && Physics2D.OverlapCircle(wallCheckFront.position, wallRadius, wallLayer);

        swordController.onCrouchStateChanged(crouching);
    }

    void FixedUpdate() {
        if (!damaged) {
            if (climbing) {
                rigidbody2D.Sleep();
                handleWallJump();
            } else if (attacking) {
                handleAttack();
            } else if (casting) {
                handleCasting();
            } else {
                // Can only move horizontally if not climbing or attacking
                handleInput();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        switch (collision.gameObject.layer) {
            case LAYER_ENEMY:
                if (!invincible)
                    handleDamage(collision.gameObject);
                break;
            case LAYER_ITEMS:
                ItemScript freeItem = collision.gameObject.GetComponent<ItemScript>();
                if (!freeItem.isAutomatic()) {
                    item = freeItem;
                    item.transform.parent = transform;
                    item.pickUp();
                } else {
                    freeItem.pickUp();
                }
                break;
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
    }

    void OnTriggerEnter2D(Collider2D collider) {
        switch (collider.gameObject.tag) {
            case "Enemies":
                if (!invincible)
                    handleDamage(collider.gameObject);
                break;
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
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

    private void handleCasting() {
        if (castingFrameCount++ == 3) {
            casting = false;
            castingFrameCount = 0;
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
        rigidbody2D.WakeUp();
        Vector2 velocity = rigidbody2D.velocity;
        velocity.y = fromWall ? WALL_JUMP_SPEED : JUMP_SPEED;
        rigidbody2D.velocity = velocity;
    }

    private void startAttack() {
        attacking = true;
        attackFrameCount = 0;
        swordController.extendSword();
    }

    private void startCasting() {
        casting = true;
        castingFrameCount = 0;
        if (item != null) {
            item.deploy();
        }
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
        makeInvincible();
        Physics2D.IgnoreLayerCollision(LAYER_PLAYER, LAYER_ENEMY, true);
        Physics2D.IgnoreLayerCollision(LAYER_PLAYER, LAYER_ENEMY_PROJECTILES, true);

        //Decrease Health in Game Data
        gameData.healthData--;

        //Decreases Lives if You Die, Pauses Game if You Run Out of Lives
        if (gameData.healthData <= 0) {
            gameData.livesData--;
            gameData.healthData = 16;
        }

        if (gameData.livesData <= 0) {
            Time.timeScale = 0;
        }

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
        Invoke("makeVincible", 1.5f);
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
