using UnityEngine;
using System.Collections;

public class Ryu : MonoBehaviour
{
    //Variable mechanics like running and jumping speed
    public float speed = 8;
    public float jumpSpeed = 7.74758027f;
    public Vector2 playerVelocity;

    //Input from User
    public float horizontalInput;
    public float verticalInput;

    //Conditions that Ryu is experiencing
    public bool climbing = false;
    public bool grounded = true;

    //Constants
    const int playerLAYER = 8;
    const int wallLAYER = 9;

    void Update()
    { // Every Frame
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        playerVelocity = rigidbody2D.velocity;
        /***Ryu's Horizontal Motion***/
        //Running
        playerVelocity.x = horizontalInput * speed;

        if (climbing)
            playerVelocity.x = 0;

        /***Ryu's Vertical Motion***/
        //Jumping
        if (Input.GetKeyDown(KeyCode.Space) ||
            Input.GetKeyDown(KeyCode.UpArrow) ||
            Input.GetKeyDown(KeyCode.W))
        {
            if (grounded)
                playerVelocity.y = jumpSpeed;
        }

        //Climbing
        if (climbing)
            playerVelocity.y = 0;

        rigidbody2D.velocity = playerVelocity;

        //Ignoring wall collisions when Ryu is grounded
        if (grounded)
            Physics2D.IgnoreLayerCollision(playerLAYER, wallLAYER, true);
        else
            Physics2D.IgnoreLayerCollision(playerLAYER, wallLAYER, false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground")
            grounded = true;
        else if (other.tag == "Wall")
        {
            climbing = true;
            playerVelocity.y = 0;
            playerVelocity.x = 0;
            rigidbody2D.velocity = playerVelocity;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Ground")
            grounded = false;
        else if (other.tag == "Wall")
            climbing = false;
    }
}


















