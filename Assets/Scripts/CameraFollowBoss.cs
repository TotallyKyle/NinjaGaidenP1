using UnityEngine;
using System.Collections;

/**
 * Script that positions the main camera on a given point of 
 * interest (Ryu) with the correct offset to show the whole height of the map.
 */
public class CameraFollowBoss : MonoBehaviour {
	
	// Point of Interest (i.e. Ryu for the Main Camera)
	public Transform 	poi;

	// Map dimensions in units
	private float mapHeight	= Utilities.PixelsToUnits(352);
	private float mapWidth	= Utilities.PixelsToUnits(512);

	private float cameraBound;
	
	void Awake() {
		Camera.main.orthographicSize = mapWidth / 2f;
		float aspectRatio = (float) Screen.width / (float) Screen.height;
		cameraBound = mapHeight * aspectRatio / 2f;
	}

	void Update () {
		Vector3 pos = transform.position;
        pos.y = Mathf.Max(poi.transform.position.y, cameraBound);
        pos.y = Mathf.Min(pos.y, mapHeight - cameraBound);
		pos.x = Mathf.Max(poi.transform.position.x, cameraBound);
		pos.x = Mathf.Min(pos.x, mapWidth - cameraBound);
		transform.position = pos;
	}
}
