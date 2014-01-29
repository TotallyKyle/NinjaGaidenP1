using UnityEngine;
using System.Collections;

public class BatmanAnimationController : AnimationController<Batman>
{

    private const int ANIM = 0;

    public override void UpdateAnimationState()
    {
        setDisplayedAnimation(ANIM);
    }
}

