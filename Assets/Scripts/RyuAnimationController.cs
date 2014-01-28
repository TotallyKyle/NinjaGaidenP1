using UnityEngine;
using System.Collections;

public class RyuAnimationController : AnimationController<Ryu> {

	private const int ANIM_IDLE			= 0;
	private const int ANIM_RUNNING		= 1;
	private const int ANIM_JUMPING		= 2;
	private const int ANIM_CLIMBING		= 3;
	private const int ANIM_CROUCHING	= 4;

	public override void UpdateAnimationState() {
		if (controlled.climbing)
			setDisplayedAnimation(ANIM_CLIMBING);
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

