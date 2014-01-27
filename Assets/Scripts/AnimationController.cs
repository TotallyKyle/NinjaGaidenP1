using UnityEngine;
using System.Collections;

[System.Serializable]
public class AnimationControllerInfo {
	public string name;
	public Sprite[] sprites;
}

public abstract class AnimationController<T> : MonoBehaviour where T : MonoBehaviour {

	private int frameCount = 0;
	private int displayedAnimation = 0;
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

	void Start() {
		spriteRenderer = controlled.GetComponent<SpriteRenderer>();
	}

	void FixedUpdate() {
		UpdateAnimationState();
		Sprite[] anim = animationInfo[displayedAnimation].sprites;
		spriteRenderer.sprite = anim[frameCount++];
		frameCount %= anim.Length;
	}

	public void setDisplayedAnimation(int displayedAnimation) {
		if (this.displayedAnimation != displayedAnimation) {
			this.displayedAnimation = displayedAnimation;
			frameCount = 0;
		}
	}
}
