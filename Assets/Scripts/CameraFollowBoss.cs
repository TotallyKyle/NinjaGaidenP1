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
	private float mapHeight	= 176f / 8f;
	private float mapWidth	= 256f / 8f;

	private float cameraBoundX;
	private float cameraBoundY;
	
	void Awake() {
		cameraBoundY = mapHeight * 0.8f / 2f;
		Camera.main.orthographicSize = cameraBoundY;
		float aspectRatio = (float) Screen.width / (float) Screen.height;
		cameraBoundX = mapHeight * 0.8f * aspectRatio / 2f;
	}

	void Update () {
		Vector3 pos = transform.position;
        pos.y = Mathf.Max(poi.transform.position.y, cameraBoundY);
        pos.y = Mathf.Min(pos.y, mapHeight - cameraBoundY);
		pos.x = Mathf.Max(poi.transform.position.x, cameraBoundX);
		pos.x = Mathf.Min(pos.x, mapWidth - cameraBoundX);
		transform.position = pos;
	}
}
