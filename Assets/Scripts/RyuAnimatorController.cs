using UnityEngine;
using System.Collections;

public class RyuAnimatorController : MonoBehaviour
{
    public bool grounded = true;

    private Animator animator;
    const int RIGHT = 1;
    const int LEFT = 0;

    // Use this for initialization
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");

        if (horizontal > 0)
            animator.SetInteger("Direction", RIGHT);
        else if (horizontal < 0)
            animator.SetInteger("Direction", LEFT);

        if (horizontal == 0)
            animator.SetInteger("Motion", 0);
        else
            animator.SetInteger("Motion", 1);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        animator.SetInteger("Jump", 0);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        animator.SetInteger("Jump", 1);
    }
}
