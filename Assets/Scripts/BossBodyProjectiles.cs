using UnityEngine;
using System.Collections;

public class BossBodyProjectiles : EnemyScript {

    // Constants
    // =============================================
    public Boss bossScript;
    public Transform target;

    /*
     * Different speeds for different actions
     */
    public const float SPEED = 1f / 16f * 60f;
    public Vector2 vel = new Vector2(0, 0);

    // State
    // =====================================

    void Start() {
        bossScript = GameObject.Find("Boss").GetComponent<Boss>();
        target = GameObject.Find("Ryu").GetComponent<Transform>();
        //Face Ryu
        float relativePosition = target.position.x - transform.position.x;
        vel = new Vector2(0f, 0f);
        if (relativePosition < 0) {
            vel.x = -SPEED;
            rigidbody2D.velocity = vel;
        } else {
            flip();
            vel.x = SPEED;
            rigidbody2D.velocity = vel;
        }
        
    }

    void Update() {
    }

    void FixedUpdate() {
        float distanceX = target.position.x - transform.position.x;
        float distanceY = target.position.y - transform.position.y;
        if (distanceX > 0 && vel.x < 0) {
            vel.x = SPEED;
            flip();
        } else if(distanceX < 0 && vel.x > 0){
            flip();
            vel.x = -SPEED;
        } else if (distanceY < 0 && vel.y > 0) {
            vel.y = -SPEED;
        } else if (distanceY > 0 && vel.y < 0) {
            vel.y = SPEED;
        } else if (Mathf.Abs(distanceX) < .5) {
            vel.x = 0;
        } else if (Mathf.Abs(distanceY) < .5) {
            vel.y = 0;
        } else {
            if (vel.x > 0) {
                vel.x = SPEED;
            } else {
                vel.x = -SPEED;
            }
            if (vel.y > 0) {
                vel.y = SPEED;
            } else {
                vel.y = -SPEED;
            }
        }
        rigidbody2D.velocity = vel;
    }

    public override void Die() {
        base.Die();
        bossScript.bodyProjectilesAlive--;
    }

    private void flip() {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
