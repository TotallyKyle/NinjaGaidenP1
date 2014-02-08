using UnityEngine;
using System.Collections;

[System.Serializable]
public class AnimationControllerInfo {
	public string name;
	public Sprite[] sprites;
	public float animationTime = 1f / 20f;
}

public abstract class AnimationController<T> : MonoBehaviour where T : MonoBehaviour {

	public interface AnimationListener {
		void onAnimationRepeat(int animationIndex);
	}

	private float timer = 0f;
	private int spriteIndex = 0;
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

		timer += Time.deltaTime;

		AnimationControllerInfo info = animationInfo[animationIndex];

		if (timer >= info.animationTime) {
			timer = 0;

			spriteRenderer.sprite = info.sprites[spriteIndex++];

			spriteIndex %= info.sprites.Length;

			if (spriteIndex == 0 && listener != null) {
				listener.onAnimationRepeat(animationIndex);
			}
		}
	}

	void FixedUpdate() {

	}

	public void setDisplayedAnimation(int animationIndex) {
		if (this.animationIndex != animationIndex) {
			this.animationIndex = animationIndex;
			spriteIndex = 0;
			timer = 0f;
		}
	}

	public void setAnimationListener(AnimationListener listener) {
		this.listener = listener;
	}
}
