using UnityEngine;
using System.Collections;

public class WindmillStar : AnimationController<WindmillStar> {

	private const float SPEED 			= 3f / 16f * 60f;
	private const float MAX_DISTANCE 	= 7f;

	private Ryu ryu;
	
	private float direction = 1f;

	protected override void Start () {
		base.Start();
		ryu = GameObject.Find("Ryu").GetComponent<Ryu>();
		direction = ryu.facingRight ? 1f : -1f;
	}

	public override void UpdateAnimationState() {

		float dX = transform.position.x - ryu.transform.position.x;

		
		Vector2 velocity = rigidbody2D.velocity;

		if (Mathf.Abs(dX) >= MAX_DISTANCE) {
			direction *= -1;
		}
		
		velocity.x = direction * SPEED;

		if ((Mathf.Sign(dX) != Mathf.Sign(rigidbody2D.velocity.x))) {
			velocity.y = 0.8f * ryu.rigidbody2D.velocity.y;
		}
		rigidbody2D.velocity = velocity;
	}


	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.layer == LayerMask.NameToLayer("Enemies")) {
			EnemyScript enemy = collider.gameObject.GetComponent<EnemyScript>();
			if (enemy != null) {
				enemy.Die();
			}
		}
	}
}
