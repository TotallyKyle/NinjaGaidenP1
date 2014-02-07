using UnityEngine;
using System.Collections;

public class Ryu : MonoBehaviour, AnimationController<Ryu>.AnimationListener {

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
    public const float JUMP_SPEED = 18.5f;
    public const float WALL_JUMP_SPEED = 13;
    public const float INJURED_JUMP_SPEED = 13;

    // Ground checking
    // ============================================

    public bool grounded = true;
    public Transform groundCheck;
    public Transform groundCheck2;
    public LayerMask groundLayer;

	public BoxCollider2D mainCollider;
	public BoxCollider2D feetCollider;
	private Vector2 jumpSize = new Vector2(1.2f, 1f);
	private Vector2 jumpCenter = Vector2.zero;
	private Vector2 standSize;
	private Vector2 standCenter;

    // State
    // =====================================

    public bool running = false;
	public bool climbing = false;
    public bool ascending = false;
    public bool facingRight = true;
    public bool crouching = false;
    public bool damaged = false;
	public bool invincible = false;
    public bool attacking = false;
    public bool casting = false;

    public GameObject sword;
    private SwordController swordController;

    // Items
    // =====================================

    public ItemScript item;

    // Game Data
    // =====================================
    public GameData gameData;

	// Sounds
	// =====================================
	public AudioClip jumpClip;
	public AudioClip hitClip;

    void Start() {
        swordController = sword.GetComponent<SwordController>();
        gameData.scoreData = 0;
        gameData.timerData = 150;
        gameData.healthData = 16;
        gameData.spiritData = 0;
        gameData.livesData = 2;

		RyuAnimationController anim = GetComponent<RyuAnimationController>();
		anim.setAnimationListener(this);

		standSize = mainCollider.size;
		standCenter = mainCollider.center;
    }

	void AnimationController<Ryu>.AnimationListener.onAnimationRepeat(int animationIndex) {
		switch (animationIndex) {
		case RyuAnimationController.ANIM_ATTACK:
		case RyuAnimationController.ANIM_CROUCH_ATTACK:
			if (attacking) {
				attacking = false;
				swordController.retractSword();
			}
			break;
		case RyuAnimationController.ANIM_CASTING:
			if (casting)
				casting = false;
			break;
		}
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

        //Ground Checking
		bool wasGrounded = grounded;
        grounded = Physics2D.OverlapArea(groundCheck.position, groundCheck2.position, groundLayer);
		if (!wasGrounded && grounded) {
			mainCollider.size = standSize;
			mainCollider.center = standCenter;
		} else if (wasGrounded && !grounded) {
			Physics2D.IgnoreLayerCollision(LAYER_PLAYER, LAYER_WALLS, true);
			mainCollider.size = jumpSize;
			mainCollider.center = jumpCenter;
			Physics2D.IgnoreLayerCollision(LAYER_PLAYER, LAYER_WALLS, false);
		}
	
        ascending = rigidbody2D.velocity.y > 0;
		feetCollider.enabled = !ascending;

        //Sword Crouch Checking
        swordController.onCrouchStateChanged(crouching);
    }

	private void revertPlayerToWallCollisions() {
		Physics2D.IgnoreLayerCollision(LAYER_PLAYER, LAYER_WALLS, false);
	}

    void FixedUpdate() {
		if (!damaged) {
			if (climbing) {
				rigidbody2D.Sleep();
				handleWallJump();
			} else if (!attacking && !casting) {
				// Can only move horizontally if not climbing or attacking or casting
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

	// Work around for OnTriggerEnter2D being called more than once
	private bool wallEntered = false;

    void OnTriggerEnter2D(Collider2D collider) {
        switch (collider.gameObject.tag) {
            case "Enemies":
                if (!invincible)
                    handleDamage(collider.gameObject);
                break;
        }
		if (collider.gameObject.layer == LayerMask.NameToLayer("Wall")) {
			if (!wallEntered) {
				wallEntered = true;
				if (!grounded && !climbing) {
					switch (collider.gameObject.tag) {
					case "Wall Right":
						if (rigidbody2D.velocity.x < 0) {
							if (facingRight) flip();
							climb();
						} else {
							climbing = false;
						}
						break;
					case "Wall Left":
						if (rigidbody2D.velocity.x > 0) {
							if (!facingRight) flip();
							climb();
						} else {
							climbing = false;
						}
						break;
					}
				}
			}
		}
    }

    void OnTriggerExit2D(Collider2D collider) {
		if (collider.gameObject.layer == LayerMask.NameToLayer("Wall")) {
			wallEntered = false;
			climbing = false;
		}
    }

    /*
     * Tests for the appropriate conditions to initiate a wall jump
     */
    private void handleWallJump() {
        if ((Input.GetKey(KeyCode.X) && Input.GetKey(KeyCode.LeftArrow)) ||
            (Input.GetKey(KeyCode.RightAlt) && Input.GetKey(KeyCode.LeftArrow))) {
			if (facingRight) {
				flip();
                jump(true);
				climbing = false;
            }
        } else if ((Input.GetKey(KeyCode.X) && Input.GetKey(KeyCode.RightArrow)) ||
                   (Input.GetKey(KeyCode.RightAlt) && Input.GetKey(KeyCode.RightArrow))) {
			if (!facingRight) {
				flip();
				jump(true);
				climbing = false;
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
		// Ensure physics
        rigidbody2D.WakeUp();

		// Set correct velocity
        Vector2 velocity = rigidbody2D.velocity;
        velocity.y = fromWall ? WALL_JUMP_SPEED : JUMP_SPEED;
        rigidbody2D.velocity = velocity;

		// Jump sound
		AudioSource.PlayClipAtPoint(jumpClip, transform.position);
    }

	private void climb() {
		rigidbody2D.Sleep();
		climbing = true;
		AudioSource.PlayClipAtPoint(jumpClip, transform.position);
	}

    private void startAttack() {
        attacking = true;
        swordController.extendSword();
    }

    private void startCasting() {
        casting = true;
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
		AudioSource.PlayClipAtPoint(hitClip, transform.position);
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
