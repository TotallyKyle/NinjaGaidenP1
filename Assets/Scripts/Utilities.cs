using UnityEngine;
using System;
using System.Collections;

public class Utilities {

	private static float sPixelsPerUnit = 16f;

	public static float PixelsToUnits(float pixels) {
		return pixels / sPixelsPerUnit;
	}

	public static void PauseGame() {
		Time.timeScale = 0.0001f;
	}

	public static IEnumerator PauseGameFor(float seconds, Action onResume) {
		PauseGame();

		float end = Time.realtimeSinceStartup + seconds;

		while (Time.realtimeSinceStartup < end) {
			yield return 0;
		}

		ResumeGame();

		onResume();
	}

	public static void ResumeGame() {
		Time.timeScale = 1f;
	}

	public static void FreezeAllRigidbodies(bool freeze) {
		Rigidbody2D[] bodies = MonoBehaviour.FindObjectsOfType(typeof(Rigidbody2D)) as Rigidbody2D[];
		foreach (Rigidbody2D body in bodies) {
			if (freeze)
				body.Sleep();
			else
				body.WakeUp();
		}
	}

	public static void SetObjectsInLayerEnabled(int layer, bool enabled) {
		GameObject[] objects = MonoBehaviour.FindObjectsOfType(typeof(GameObject)) as GameObject[];
		foreach (GameObject object_ in objects) {
			if (object_.layer == layer) {
				if (object_.rigidbody2D != null) {
					if (enabled)
						object_.rigidbody2D.WakeUp();
					else
						object_.rigidbody2D.Sleep();
				}
			}
		}
	}
}
