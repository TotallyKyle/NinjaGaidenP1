using UnityEngine;
using System.Collections;

public class BossAnimationController : AnimationController<Boss> {

    private const int ANIM_IDLE = 0;
    private const int ANIM_CHARGING_STAGE_1 = 1;
    private const int ANIM_CHARGING_STAGE_2 = 2;
    private const int ANIM_HEAD_TAIL_PROJECTILE = 3;

    public override void UpdateAnimationState() {

        if (controlled.chargingStageOne) {
            setDisplayedAnimation(ANIM_CHARGING_STAGE_1);
        } else if (controlled.chargingStageTwo) {
            setDisplayedAnimation(ANIM_CHARGING_STAGE_2);
        } else if (controlled.bodyPartsAttack) {
            setDisplayedAnimation(ANIM_HEAD_TAIL_PROJECTILE);
        } else if (controlled.idle)
            setDisplayedAnimation(ANIM_IDLE);
    }
}

