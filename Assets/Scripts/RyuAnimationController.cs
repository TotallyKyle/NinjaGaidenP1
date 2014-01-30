using UnityEngine;
using System.Collections;

public class RyuAnimationController : AnimationController<Ryu> {

	private const int ANIM_IDLE				= 0;
	private const int ANIM_RUNNING			= 1;
	private const int ANIM_JUMPING			= 2;
	private const int ANIM_CLIMBING			= 3;
	private const int ANIM_CROUCHING		= 4;
	private const int ANIM_ATTACK			= 5;
	private const int ANIM_CROUCH_ATTACK	= 6;

	public override void UpdateAnimationState() {
		if (controlled.climbing)
			setDisplayedAnimation(ANIM_CLIMBING);
		else if (controlled.attacking) {
			if (controlled.crouching)
				setDisplayedAnimation(ANIM_CROUCH_ATTACK);
			else
				setDisplayedAnimation(ANIM_ATTACK);
		} else if (!controlled.grounded)
			setDisplayedAnimation(ANIM_JUMPING);
		else if (controlled.running)
			setDisplayedAnimation(ANIM_RUNNING);
		else if (controlled.crouching)
			setDisplayedAnimation(ANIM_CROUCHING);
		else
			setDisplayedAnimation(ANIM_IDLE);
	}
}

