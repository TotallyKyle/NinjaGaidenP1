using UnityEngine;
using System.Collections;

[System.Serializable]
public class AnimationControllerInfo {
	public string name;
	public Sprite[] sprites;
}

public abstract class AnimationController<T> : MonoBehaviour where T : MonoBehaviour {

	public interface AnimationListener {
		void onAnimationRepeat(int animationIndex);
	}

	private int frameCount = 0;
	private int animationIndex = 0;
	private AnimationListener listener;
	private SpriteRenderer spriteRenderer;

	/*
	 * Set these in Unity
	 */
	public T controlled;
	public AnimationControllerInfo[] animationInfo;
	
	/*
	 * Here is where subclasses should use the state 
	 * of a referenced object to determine which animation to play
	 */
	public abstract void UpdateAnimationState();

	protected virtual void Start() {
		if (controlled != null)
			spriteRenderer = controlled.GetComponent<SpriteRenderer>();
		else
			spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Update() {
		UpdateAnimationState();
	}

	void FixedUpdate() {
		Sprite[] anim = animationInfo[animationIndex].sprites;
		spriteRenderer.sprite = anim[frameCount++];
		frameCount %= anim.Length;
		if (frameCount == 0 && listener != null) {
			listener.onAnimationRepeat(animationIndex);
		}
	}

	public void setDisplayedAnimation(int animationIndex) {
		if (this.animationIndex != animationIndex) {
			this.animationIndex = animationIndex;
			frameCount = 0;
		}
	}

	public void setAnimationListener(AnimationListener listener) {
		this.listener = listener;
	}
}
