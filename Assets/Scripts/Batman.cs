using UnityEngine;
using System.Collections;

public class Batman : MonoBehaviour
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
        //Constantly update velocity
        rigidbody2D.velocity = vel;

        //If goes off camera, destroy the object
        GameObject camera = GameObject.Find("Main Camera");
        float relativePosition = transform.position.x - camera.transform.position.x;
        if (Mathf.Abs(relativePosition) > 26 / 3)
            Destroy(transform.gameObject);
    }

    private void flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
