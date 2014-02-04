using UnityEngine;
using System.Collections;

public class ThrowingStar : MonoBehaviour
{
	
	// Constants
	// =============================================
	
	/*
     * Layer indecies 
     */
	private const int LAYER_ENEMIES = 11;
	
	/*
     * Different speeds for different actions
     */
	public const float SPEED = 3f / 16f * 60f;

	public Ryu ryu;
	public Vector2 vel;
	
	/*
     * Checks which direction the Knife Thrower threw the knife
     */
	void Start()
	{
		ryu = (Ryu) GameObject.Find("Ryu").GetComponent<Ryu>();
		float speed = ryu.facingRight ? SPEED : -SPEED;
		vel = new Vector2(speed, 0f);
		rigidbody2D.velocity = vel;
	}
	
	void Update()
	{
		rigidbody2D.velocity = vel; 
		checkOffCamera();
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		Destroy(transform.gameObject);
	}
	
	//If goes off camera, destroy the object
	private void checkOffCamera(){
		GameObject camera = GameObject.Find("Main Camera");
		float relativePosition = transform.position.x - camera.transform.position.x;
		if (Mathf.Abs(relativePosition) > 26 / 3)
			Destroy(transform.gameObject);       
	}
}

