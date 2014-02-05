using UnityEngine;
using System.Collections;

public class BoxerAnimationController : AnimationController<Boxer> {

	private const int ANIM_RUNNING			= 0;
	private const int ANIM_ATTACK			= 1;

	public override void UpdateAnimationState() {
		if (controlled.running)
			setDisplayedAnimation(ANIM_RUNNING);
        else if (controlled.attacking) {
            setDisplayedAnimation(ANIM_ATTACK);
        }
	}
}

