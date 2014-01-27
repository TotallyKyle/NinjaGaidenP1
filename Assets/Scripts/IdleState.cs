using UnityEngine;
using System.Collections;

public class IdleState : State {

	public IdleState(Ryu script) : base(script) {
	}

	public override void Start() {
		ryu.setMotion(STATE_IDLE);
		Physics2D.IgnoreLayerCollision(Ryu.LAYER_PLAYER, Ryu.LAYER_WALL, true);
	}

	public override void Update() {
		// Can change direction, run, or jump
		if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)) {
			ryu.faceLeft();
			ryu.run();
		} else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow)) {
			ryu.faceRight();
			ryu.run();
		} else if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.RightAlt)) {
			ryu.jump();
		}
	}
}
