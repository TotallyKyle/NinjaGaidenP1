using UnityEngine;
using System.Collections;

public class RyuAnimationController : AnimationController<Ryu> {

	public const int ANIM_IDLE			= 0;
	public const int ANIM_RUNNING		= 1;
	public const int ANIM_JUMPING		= 2;
	public const int ANIM_CLIMBING		= 3;
	public const int ANIM_CROUCHING		= 4;
	public const int ANIM_ATTACK		= 5;
	public const int ANIM_CROUCH_ATTACK	= 6;
	public const int ANIM_CASTING		= 7;

	public override void UpdateAnimationState() {
		if (controlled.climbing)
			setDisplayedAnimation(ANIM_CLIMBING);
		else if (controlled.attacking) {
			if (controlled.crouching)
				setDisplayedAnimation(ANIM_CROUCH_ATTACK);
			else
				setDisplayedAnimation(ANIM_ATTACK);
		} else if (controlled.casting)
			setDisplayedAnimation(ANIM_CASTING);
		else if (!controlled.grounded)
			setDisplayedAnimation(ANIM_JUMPING);
		else if (controlled.running)
			setDisplayedAnimation(ANIM_RUNNING);
		else if (controlled.crouching)
			setDisplayedAnimation(ANIM_CROUCHING);
		else
			setDisplayedAnimation(ANIM_IDLE);
	}
}

