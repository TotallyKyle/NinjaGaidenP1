using UnityEngine;
using System.Collections;

public class ClimbState : State {

	public ClimbState(Ryu script) : base(script) {
	}

	public override void Start() {
		ryu.setMotion(STATE_CLIMB);
	}

	public override void Update() {
		bool jumpHold = Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.RightAlt);
		bool jumpTap = Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.RightAlt);
		if (ryu.isFacingLeft()) {
			if ((jumpHold && Input.GetKeyDown(KeyCode.RightArrow)) || 
			    (jumpTap && Input.GetKey(KeyCode.RightArrow))) {
				ryu.jump();
			}
		} else if (ryu.isFacingRight()) {
			if ((jumpHold && Input.GetKeyDown(KeyCode.LeftArrow)) ||
			    (jumpTap && Input.GetKey(KeyCode.LeftArrow))) {
				ryu.jump();
			}
		}
	}
}
