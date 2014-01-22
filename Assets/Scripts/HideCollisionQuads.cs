using UnityEngine;
using System.Collections;

public class HideCollisionQuads : MonoBehaviour {
	
	void Awake() {
		foreach (Transform t in transform) {
			Destroy(t.gameObject);
		}
	}
	
}
