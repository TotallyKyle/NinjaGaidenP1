using UnityEngine;
using System.Collections;

public class BossProjectiles : MonoBehaviour {

    // Constants
    // =============================================

    /*
     * Different speeds for different actions
     */
    public const float SPEED = 1.5f / 16f * 60f;
    public Vector2 vel = new Vector2(0, 0);

    // State
    // =====================================

    void Start() {
        Transform target = GameObject.Find("Boss").transform;
        float relativePositionX = transform.position.x - target.position.x;
        float relativePositionY = transform.position.y - target.position.y;
        float degrees = Mathf.Atan(relativePositionY / relativePositionX);
        if (relativePositionX < 0)
            vel.x = Mathf.Cos(degrees) * -SPEED;
        else
            vel.x = Mathf.Cos(degrees) * SPEED;
        if (relativePositionY < 0)
            vel.y = Mathf.Sin(degrees) * SPEED;
        else
            vel.y = Mathf.Sin(degrees) * -SPEED;
    }

    void Update() {
    }

    void FixedUpdate() {
        rigidbody2D.velocity = vel;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        Destroy(gameObject);
    }
}
