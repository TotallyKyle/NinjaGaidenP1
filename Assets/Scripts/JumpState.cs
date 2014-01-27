using UnityEngine;
using System.Collections;

public class JumpState : State {

	private bool mFromWall;
	private float mJumpSpeed = 15f;

	public JumpState(Ryu script) : base(script) {
	}

	public override void Start() {
		// Different vertical speed if jumping off a wall
		mFromWall = ryu.isClimbing();

		ryu.setMotion(STATE_JUMP);
		Vector2 velocity = ryu.rigidbody2D.velocity;
		velocity.y = mFromWall ? mJumpSpeed / 2f : mJumpSpeed;
		ryu.rigidbody2D.velocity = velocity;

		// We don't want to ignore walls so we can start a climb if needed
		Physics2D.IgnoreLayerCollision(Ryu.LAYER_PLAYER, Ryu.LAYER_WALL, false);
	}

	public override void Update() {
		if (Input.GetKey(KeyCode.LeftArrow)) {
			if (ryu.isFacingRight()) {
				if (mFromWall) {
					ryu.faceLeft();
					setHorizontalSpeed(-1.5f / 16f * 60f);
				} else {
					setHorizontalSpeed(-0.5f / 16f * 60f);
				}
			} else {
				setHorizontalSpeed(-1.5f / 16f * 60f);
			}
		} else if (Input.GetKey(KeyCode.RightArrow)) {
			if (ryu.isFacingLeft()) {
				if (mFromWall) {
					ryu.faceRight();
					setHorizontalSpeed(1.5f / 16f * 60f);
				} else {
					setHorizontalSpeed(0.5f / 16f * 60f);
				}
			} else {
				setHorizontalSpeed(1.5f / 16f * 60f);
			}
		} else {
			setHorizontalSpeed(0f);
		}
	}

	private void setHorizontalSpeed(float speed) {
		Vector2 velocity = ryu.rigidbody2D.velocity;
		velocity.x = speed;
		ryu.rigidbody2D.velocity = velocity;
	}
}
