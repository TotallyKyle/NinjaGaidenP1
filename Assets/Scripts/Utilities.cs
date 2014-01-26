using UnityEngine;
using System.Collections;

public class Utilities {

	private static float sPixelsPerUnit = 16f;

	public static float PixelsToUnits(float pixels) {
		return pixels / sPixelsPerUnit;
	}
}
