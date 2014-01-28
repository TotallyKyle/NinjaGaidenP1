using UnityEngine;
using System.Collections;

public class RyuAnimationController : AnimationController<Ryu> {

	private const int ANIM_IDLE		= 0;
	private const int ANIM_RUNNING	= 1;
	private const int ANIM_JUMPING	= 2;
	private const int ANIM_CLIMBING	= 3;

	public override void UpdateAnimationState() {
		if (controlled.climbing)
			setDisplayedAnimation(ANIM_CLIMBING);
		else if (!controlled.grounded)
			setDisplayedAnimation(ANIM_JUMPING);
		else if (controlled.running)
			setDisplayedAnimation(ANIM_RUNNING);
		else
			setDisplayedAnimation(ANIM_IDLE);
	}
}

