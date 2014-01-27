using UnityEngine;
using System.Collections;

public class RunState : State {
	
	public RunState(Ryu script) : base(script) {
	}

	public override void Start() {
		ryu.setMotion(STATE_RUN);
		Physics2D.IgnoreLayerCollision(Ryu.LAYER_PLAYER, Ryu.LAYER_WALL, true);
	}

	public override void Update() {
		// Can change direction, idle, or jump
		if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.RightAlt)) {
			ryu.jump();
		} else if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)) {
			ryu.faceLeft();
			setRunSpeed(-1.5f / 16f * 60f);
		} else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow)) {
			ryu.faceRight();
			setRunSpeed(1.5f / 16f * 60f);
		} else {
			setRunSpeed(0f);
			ryu.idle();
		}
	}

	private void setRunSpeed(float speed) {
		Vector2 velocity = ryu.rigidbody2D.velocity;
		velocity.x = speed;
		ryu.rigidbody2D.velocity = velocity;
	}
}
