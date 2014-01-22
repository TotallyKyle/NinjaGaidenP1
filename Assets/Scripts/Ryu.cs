using UnityEngine;
using System.Collections;

public class Ryu : MonoBehaviour {
	public float		speed = 8;
	public float		jumpSpeed = 12;
	public float		jumpAcc = 4;
	
	public bool			grounded = true;
	
	void Update () { // Every Frame
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		
		Vector2 vel = rigidbody2D.velocity;
		vel.x = h*speed;
		
		if (Input.GetKeyDown(KeyCode.Space) ||
		    Input.GetKeyDown(KeyCode.UpArrow) ||
		    Input.GetKeyDown(KeyCode.W) ) {
			if (grounded) vel.y = jumpSpeed;
		}
		if (Input.GetKey(KeyCode.Space) ||
		    Input.GetKey(KeyCode.UpArrow) ||
		    Input.GetKey(KeyCode.W) ) {
			if (!grounded) vel.y += jumpAcc * Time.deltaTime;
		}
		
		rigidbody2D.velocity = vel;
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		grounded = true;
	}
	void OnTriggerExit2D(Collider2D other) {
		grounded = false;
	}
}


















