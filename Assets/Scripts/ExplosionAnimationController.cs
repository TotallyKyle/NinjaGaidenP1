using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class ExplosionAnimationController : 
AnimationController<MonoBehaviour>, AnimationController<MonoBehaviour>.AnimationListener {

	public AudioClip explosionClip;

	protected override void Start() {
		base.Start();
		setAnimationListener(this);
		AudioSource.PlayClipAtPoint(explosionClip, transform.position, 1.0f);
	}

	public override void UpdateAnimationState() {
	}

	void AnimationController<MonoBehaviour>.AnimationListener.onAnimationRepeat(int animationIndex) {
		Destroy(gameObject);
	}
}
