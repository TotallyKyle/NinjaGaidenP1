﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof(BoxCollider2D))]
[RequireComponent (typeof(SpriteRenderer))]
public class FireWheel : MonoBehaviour {

	private GameObject mainCamera;

	void Awake() {
		mainCamera = GameObject.Find("Main Camera");
	}

	void Update () {
		float relativePosition = transform.position.x - mainCamera.transform.position.x;
		if (Mathf.Abs(relativePosition) > (26f / 3f)) {
			Destroy(transform.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.layer == LayerMask.NameToLayer("Enemies") ||
		    collider.gameObject.layer == LayerMask.NameToLayer("Boss")) {
			EnemyScript enemy = collider.GetComponent<EnemyScript>();
			if (enemy != null) {
				enemy.Die();
			}
		}
	}
}
