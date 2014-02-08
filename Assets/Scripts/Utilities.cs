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
}
