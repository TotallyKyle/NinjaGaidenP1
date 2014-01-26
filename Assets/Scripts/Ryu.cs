using UnityEngine;
using System.Collections;

public class Ryu : MonoBehaviour
{
    //Variable mechanics like running and jumping speed
    public float runSpeed = 5.625f;
    public float jumpSpeed = 7.74758027f;
    public Vector2 playerVelocity;
    public Vector3 playerPosition;

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
        playerPosition = transform.position;
        playerVelocity = rigidbody2D.velocity;

        /***Ryu's Horizontal Motion***/
        //Running
        if (horizontalInput > 0)
            playerVelocity.x = runSpeed;
        else if (horizontalInput < 0)
            playerVelocity.x = -runSpeed;
        else
            playerVelocity.x = 0;

        if (climbing)
            playerVelocity.x = 0;

        /***Ryu's Vertical Motion***/
        //Jumping
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (grounded)
                playerVelocity.y = jumpSpeed;
        }

        //Climbing
        if (climbing)
            playerVelocity.y = 0;

        //Jumping off Wall
        AnimatorStateInfo state = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        if (state.IsName("Hang Right"))
        {
            if (Input.GetKey(KeyCode.Z) && horizontalInput < 0)
            {
                rigidbody2D.WakeUp();
                climbing = false;
                playerVelocity.y = jumpSpeed;
                playerVelocity.x = -runSpeed;
            }
        }
        else if (state.IsName("Hang Left"))
        {
            if (Input.GetKey(KeyCode.Z) && horizontalInput > 0)
            {
                rigidbody2D.WakeUp();
                climbing = false;
                playerVelocity.y = jumpSpeed;
                playerVelocity.x = runSpeed;
            }
        }

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
            rigidbody2D.Sleep();
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Ground")
            grounded = false;
    }
}


















