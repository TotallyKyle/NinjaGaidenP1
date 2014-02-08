using UnityEngine;
using System.Collections;

public class Utilities {

	private static float sPixelsPerUnit = 16f;

	public static float PixelsToUnits(float pixels) {
		return pixels / sPixelsPerUnit;
	}

	public static void PauseGame() {
		Time.timeScale = 0;
	}

	public static void ResumeGame() {
		Time.timeScale = 1;
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
