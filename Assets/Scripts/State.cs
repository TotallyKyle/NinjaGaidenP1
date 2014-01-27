using UnityEngine;
using System.Collections;

/**
 * Abstract class that represents the state of Ryu
 */
public class State {

	/*
	 * State names
	 */
	public const string STATE_IDLE_LEFT		= "Idle Left";
	public const string STATE_IDLE_RIGHT	= "Idle Right";
	public const string STATE_RUN_LEFT		= "Run Left";
	public const string STATE_RUN_RIGHT		= "Run Right";
	public const string STATE_JUMP_LEFT		= "Jump Left";
	public const string STATE_JUMP_RIGHT	= "Jump Right";
	public const string STATE_HANG_LEFT		= "Hang Left";
	public const string STATE_HANG_RIGHT	= "Hang Right";
	
	/*
	 * Condition names
	 */
	public const string MOTION		= "Motion";
	public const string DIRECTION	= "Direction";

	/*
	 * Condition values
	 */
	public const int STATE_IDLE		= 0;
	public const int STATE_RUN		= 1;
	public const int STATE_JUMP		= 2;
	public const int STATE_CLIMB	= 3;
	
	public Ryu ryu;

	public State(Ryu ryuScript) {
		ryu = ryuScript;
	}

	/*
	 * Called before the state is set as Ryu's new state.
	 * At this point, Ryu still has his previous state.
	 */
	public virtual void Start() {
	}

	/*
	 * Called each frame when mScript updates
	 */
	public virtual void Update() {
	}
}

