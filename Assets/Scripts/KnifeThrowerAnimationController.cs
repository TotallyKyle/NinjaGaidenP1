using UnityEngine;
using System.Collections;

public class KnifeThrowerAnimationController : AnimationController<KnifeThrower> {

	private const int ANIM_RUNNING			= 0;
	private const int ANIM_ATTACK			= 1;
	private const int ANIM_CROUCH_ATTACK	= 2;

	public override void UpdateAnimationState() {
		if (controlled.running)
			setDisplayedAnimation(ANIM_RUNNING);
        else if (controlled.attacking)
        {
            if (controlled.crouching)
                setDisplayedAnimation(ANIM_CROUCH_ATTACK);
            else
                setDisplayedAnimation(ANIM_ATTACK);
        }
	}
}

