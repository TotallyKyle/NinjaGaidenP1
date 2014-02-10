using UnityEngine;
using System.Collections;

public class BossExplosionAnimationController : 
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
