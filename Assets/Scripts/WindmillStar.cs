using UnityEngine;
using System.Collections;

public class WindmillStar : AnimationController<WindmillStar> {

	private const float SPEED			= 4f / 16f * 60f;
	private const float MAX_DISTANCE 	= 6f;

	private Ryu ryu;
	
	private Vector3 target;
	private float direction = 1f;

	protected override void Start () {
		base.Start();

		ryu = GameObject.Find("Ryu").GetComponent<Ryu>();

		direction = ryu.facingRight ? 1f : -1f;

		target = ryu.transform.position + new Vector3(direction * MAX_DISTANCE, 0f, 0f);
	}

	public override void UpdateAnimationState() {
	}

	void FixedUpdate() {

		// Position of the shuriken relative to Ryu
		Vector3 rel = transform.position - ryu.transform.position;
		
		if (Mathf.Abs(rel.x) < 0.2f && Mathf.Abs(rel.y) < 0.2f) {
			// If it reached Ryu, we are done
			Destroy(gameObject);
		}

		if (Mathf.Abs(Vector3.Distance(transform.position, target)) < 0.15f) {
			// If it reached the target, turn around and head towards new target
			direction *= -1;
			target = ryu.transform.position + new Vector3(direction * MAX_DISTANCE, 0f, 0f);
		}

		if (Mathf.Sign(rel.x) != direction) {
			// If it is headed towards Ryu, always update the target so it tries to go back to him
			target = ryu.transform.position + new Vector3(direction * MAX_DISTANCE, 0f, 0f);
		}

		Vector3 vel = target - transform.position;
		vel.Normalize();
		vel *= SPEED;
		rigidbody2D.velocity = vel;
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.layer == LayerMask.NameToLayer("Enemies") || 
		    collider.gameObject.layer == LayerMask.NameToLayer("Boss")) {
			EnemyScript enemy = collider.gameObject.GetComponent<EnemyScript>();
			if (enemy != null) {
				enemy.Die();
			}
		}
	}
}
