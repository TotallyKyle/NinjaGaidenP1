using UnityEngine;
using System.Collections;

public class Boss : EnemyScript {

    // Constants
    // =============================================
    public const int MAX_BOSS_HEALTH = 32;
    public Transform headPrefab;
    public Transform tailPrefab;
    public Transform projectilePrefab;
    public Transform lightningHorizontalPrefab;
    public Transform lightningVerticalPrefab;

    /*
     * Layer indecies 
     */
    private const int LAYER_ENEMIES = 11;

    /*
     * Different speeds for different actions
     */
    public const float SPEED = .5f / 16f * 60f;
    public Vector2 vel = new Vector2(0, 0);

    // State
    // =====================================
    public bool movementComplete = false;
    public bool idle = true;
    public bool chargingStageOne = false;
    public bool chargingStageTwo = false;
    public bool bodyPartsAttack = false;
    public bool projectilesAttack = false;
    public int bodyProjectilesAlive = 0;
    public int lightningCasted = 0;
    public int bulletsFlourished = 0;
    public float timeBetweenAttacks = 0;
    public int movement = 0;
    public int currentHealth;

    void Start() {
        //Sets Score and Boss Health
        score = 10000;
        currentHealth = MAX_BOSS_HEALTH;
    }

    void Update() {
        GameData.enemyHealthData = currentHealth;
    }

    void FixedUpdate() {
        if (!movementComplete)
            MoveBoss();
        else {
            rigidbody2D.velocity = new Vector2(0, 0);
            HandleAttack();
        }
    }

    public override void Die() {
        currentHealth--;
        if (currentHealth <= 0) {
            base.Die();
        }
    }

    void MoveBoss() {
        if (movement == 0) {
            if (transform.position.y < 17) {
                vel.x = 0;
                vel.y = SPEED;
            } else {
                vel.x = -SPEED;
                vel.y = 0;
                movement++;
                movementComplete = true;
            }
        } else if (movement == 1) {
            if (transform.position.x > 7) {
                vel.x = -SPEED;
                vel.y = 0;
            } else {
                flip();
                vel.x = 0;
                vel.y = -SPEED;
                movement++;
                movementComplete = true;
            }
        } else if (movement == 2) {
            if (transform.position.y > 6) {
                vel.x = 0;
                vel.y = -SPEED;
            } else {
                vel.x = SPEED;
                vel.y = SPEED;
                movement++;
                movementComplete = true;
            }
        } else if (movement == 3) {
            if (transform.position.y < 10)
                vel.y = SPEED;
            else
                vel.y = 0;
            if (transform.position.x < 16)
                vel.x = SPEED;
            else
                vel.x = 0;
            if (transform.position.y >= 10 && transform.position.x >= 16) {
                vel.x = SPEED;
                vel.y = -SPEED;
                movement++;
                movementComplete = true;
            }
        } else if (movement == 4) {
            if (transform.position.y > 6)
                vel.y = -SPEED;
            else
                vel.y = 0;
            if (transform.position.x < 25)
                vel.x = SPEED;
            else
                vel.x = 0;
            if (transform.position.y <= 6 && transform.position.x >= 25) {
                flip();
                vel.x = 0;
                vel.y = SPEED;
                movement = 0;
                movementComplete = true;
            }
        }
        rigidbody2D.velocity = vel;
    }

    void HandleAttack() {
        switch (movement) {
            case 0:
                ChargeAttackHorizontal();
                break;
            case 1:
                BodyAttack();
                break;
            case 2:
                ChargeAttackVertical();
                break;
            case 3:
                BodyAttack();
                break;
            case 4:
                ProjectileFlurry();
                break;
        }
    }

    void ChargeAttackHorizontal() {
        chargingStageOne = true;
        if (transform.childCount == 0) {
            if (lightningCasted == 5) {
                movementComplete = false;
                chargingStageOne = false;
                lightningCasted = 0;
            } else {
                if (Time.time - timeBetweenAttacks > .5) {
                    Transform lightning = Instantiate(lightningHorizontalPrefab, new Vector3(16, 6 + 3.25f * lightningCasted, 0), Quaternion.identity) as Transform;
                    lightning.parent = transform;
                    lightning.Rotate(new Vector3(0, 0, 270));
                    timeBetweenAttacks = Time.time;
                    lightningCasted++;
                }
            }

        } else {
            if (Time.time - timeBetweenAttacks > 1.5) {
                foreach (Transform child in transform) {
                    Destroy(child.gameObject);
                }
                timeBetweenAttacks = Time.time;
            }
        }
    }

    void ChargeAttackVertical() {
        chargingStageTwo = true;
        if (transform.childCount == 0) {
            if (lightningCasted == 5) {
                movementComplete = false;
                chargingStageTwo = false;
                lightningCasted = 0;
            } else {
                if (Time.time - timeBetweenAttacks > .5) {
                    Transform lightning = Instantiate(lightningVerticalPrefab, new Vector3(6 + 5 * lightningCasted, 11, 0), Quaternion.identity) as Transform;
                    lightning.parent = transform;
                    timeBetweenAttacks = Time.time;
                    lightningCasted++;
                }
            }

        } else {
            if (Time.time - timeBetweenAttacks > 1.5) {
                foreach (Transform child in transform) {
                    Destroy(child.gameObject);
                }
                timeBetweenAttacks = Time.time;
            }
        }

    }

    void BodyAttack() {
        if (bodyPartsAttack && bodyProjectilesAlive == 0) {
            movementComplete = false;
            bodyPartsAttack = false;
        } else if (transform.childCount < 1) {
            bodyProjectilesAlive = 2;
            bodyPartsAttack = true;

            if (movement == 1) {
                Transform head = Instantiate(headPrefab, new Vector3(transform.position.x - 2.5f, 25, 0), Quaternion.identity) as Transform;
                head.parent = transform;
                Transform tail = Instantiate(tailPrefab, new Vector3(transform.position.x - 2, 2, 0), Quaternion.identity) as Transform;
                tail.parent = transform;
            } else if (movement == 3) {
                Transform head = Instantiate(headPrefab, new Vector3(transform.position.x + 2.5f, 25, 0), Quaternion.identity) as Transform;
                head.parent = transform;
                Transform tail = Instantiate(tailPrefab, new Vector3(transform.position.x + 2, 2, 0), Quaternion.identity) as Transform;
                tail.parent = transform;
            }
        }
    }

    void ProjectileFlurry() {
        if (transform.childCount == 0) {
            if (bulletsFlourished == 5) {
                movementComplete = false;
                bulletsFlourished = 0;
            } else {
                bulletsFlourished++;
                Transform bullet = Instantiate(projectilePrefab, new Vector3(transform.position.x - 5 + bulletsFlourished, transform.position.y + bulletsFlourished, 0), Quaternion.identity) as Transform;
                bullet.parent = transform;  
                bullet = Instantiate(projectilePrefab, new Vector3(transform.position.x + bulletsFlourished, transform.position.y + 5 - bulletsFlourished, 0), Quaternion.identity) as Transform;
                bullet.parent = transform;
                bullet = Instantiate(projectilePrefab, new Vector3(transform.position.x + 5 - bulletsFlourished, transform.position.y - bulletsFlourished, 0), Quaternion.identity) as Transform;
                bullet.parent = transform;
                bullet = Instantiate(projectilePrefab, new Vector3(transform.position.x - bulletsFlourished, transform.position.y - 5 + bulletsFlourished, 0), Quaternion.identity) as Transform;
                bullet.parent = transform;
                bullet = Instantiate(projectilePrefab, new Vector3(transform.position.x + 2.5f - bulletsFlourished, transform.position.y + 2.5f - bulletsFlourished, 0), Quaternion.identity) as Transform;
                bullet.parent = transform;
                bullet = Instantiate(projectilePrefab, new Vector3(transform.position.x + 2.5f - bulletsFlourished, transform.position.y - 2.5f + bulletsFlourished, 0), Quaternion.identity) as Transform;
                bullet.parent = transform;
                bullet = Instantiate(projectilePrefab, new Vector3(transform.position.x - 2.5f - bulletsFlourished, transform.position.y - 2.5f + bulletsFlourished, 0), Quaternion.identity) as Transform;
                bullet.parent = transform;
                bullet = Instantiate(projectilePrefab, new Vector3(transform.position.x - 2.5f + bulletsFlourished, transform.position.y + 2.5f + bulletsFlourished, 0), Quaternion.identity) as Transform;
                bullet.parent = transform;
                timeBetweenAttacks = Time.time;
            }
        } else {
            if (Time.time - timeBetweenAttacks > 3) {
                foreach (Transform child in transform) {
                    Destroy(child.gameObject);
                }
            }
        }
    }

    private void flip() {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
