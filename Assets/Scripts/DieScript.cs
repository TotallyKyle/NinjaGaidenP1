using UnityEngine;
using System.Collections;

/**
 * Simple script that explodes on destroy
 */
public class DieScript : MonoBehaviour {

	private Vector3 offset = new Vector3(0f, 1f, 0f);

	public void die() {
		Instantiate(Resources.Load("Explosion"), transform.position + offset, Quaternion.identity);
	}
}
