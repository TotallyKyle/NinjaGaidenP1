using UnityEngine;
using System.Collections;

public class ItemContainerScript : MonoBehaviour {

	public Transform item;

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.layer == LayerMask.NameToLayer("Sword")) {
			Instantiate(item, transform.position, Quaternion.identity);
			Destroy(this.gameObject);
		}
	}
}
