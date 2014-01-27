using UnityEngine;
using System.Collections;

public class Ryu : MonoBehaviour {

    //Constants
    public const int LAYER_PLAYER	= 8;
	public const int LAYER_WALL		= 9;
	public const string TAG_GROUND	= "Ground";
	public const string TAG_WALL	= "Wall";
		
	private static State sIdleState;
	private static State sRunState;
	private static State sJumpState;
	private static State sClimbState;

	private State mState;
	private Animator mAnimator;

	public void idle() {
		sIdleState.Start();
		mState = sIdleState;
	}

	public bool isIdle() {
		return mState == sIdleState;
	}

	public void run() {
		sRunState.Start();
		mState = sRunState;
	}

	public bool isRunning() {
		return mState == sRunState;
	}

	public void jump() {
		sJumpState.Start();
		mState = sJumpState;
	}

	public bool isJumping() {
		return mState == sJumpState;
	}

	public void climb() {
		sClimbState.Start();
		mState = sClimbState;
	}

	public bool isClimbing() {
		return mState == sClimbState;
	}

	public void faceLeft() {
		mAnimator.SetInteger(State.DIRECTION, 0);
	}
	
	public bool isFacingLeft() {
		return mAnimator.GetInteger(State.DIRECTION) == 0;
	}
	
	public void faceRight() {
		mAnimator.SetInteger(State.DIRECTION, 1);
	}
	
	public bool isFacingRight() {
		return mAnimator.GetInteger(State.DIRECTION) == 1;
	}
	
	public void setMotion(int state) {
		mAnimator.SetInteger(State.MOTION, state);
	}

	void Awake() {
		mAnimator = GetComponent<Animator>();
		sIdleState = new IdleState(this);
		sRunState = new RunState(this);
		sJumpState = new JumpState(this);
		sClimbState = new ClimbState(this);
		mState = sIdleState;
	}

	void Start() {
		mState.Start();
	}

	void FixedUpdate() {
		mState.Update();
	}

	void OnTriggerEnter2D(Collider2D other) {
		switch (other.tag) {
		case TAG_WALL:
			rigidbody2D.Sleep();
			climb();
			break;
		case TAG_GROUND:
			if (Input.GetAxis("Horizontal") == 0) {
				idle();
			} else {
				run();
			}
			break;
		}
    }

    void OnTriggerExit2D(Collider2D other) {
		switch (other.tag) {
		case TAG_WALL:
			rigidbody2D.WakeUp();
			break;
		}
    }
}


















