using UnityEngine;
using System.Collections;

public class ThrowingStar : AnimationController<ThrowingStar>
{
	
	// Constants
	// =============================================
	
	/*
     * Different speeds for different actions
     */
	public const float SPEED = 3f / 16f * 60f;

	public Vector2 vel;

	public AudioClip starClip;

	private static GameData sGameData;
	
	/*
     * Checks which direction the Knife Thrower threw the knife
     */
	protected override void Start() {
		base.Start();
		if (sGameData == null) {
			sGameData = GameObject.Find("Game HUD").GetComponent<GameData>();
		}
		AudioSource.PlayClipAtPoint(starClip, transform.position);
		Ryu ryu = (Ryu) GameObject.Find("Ryu").GetComponent<Ryu>();
		float speed = ryu.facingRight ? SPEED : -SPEED;
		vel = new Vector2(speed, 0f);
		rigidbody2D.velocity = vel;
	}
	
	void Update() {
		rigidbody2D.velocity = vel; 
		checkOffCamera();
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.layer == LayerMask.NameToLayer("Enemies")) {
			DieScript dieScript = collider.GetComponent<DieScript>();
			if (dieScript != null) {
				dieScript.die();
			}
			sGameData.scoreData += 100;
			Destroy(collider.gameObject);
			Destroy(transform.gameObject);
		}
	}
	
	//If goes off camera, destroy the object
	private void checkOffCamera() {
		GameObject camera = GameObject.Find("Main Camera");
		float relativePosition = transform.position.x - camera.transform.position.x;
		if (Mathf.Abs(relativePosition) > 26 / 3)
			Destroy(transform.gameObject);       
	}

	public override void UpdateAnimationState() {
	}
}

