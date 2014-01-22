using UnityEngine;
using System.Collections;

public class RyuAnimatorController : MonoBehaviour
{

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
}
