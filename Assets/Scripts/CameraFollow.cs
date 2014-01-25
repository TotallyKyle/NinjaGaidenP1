using UnityEngine;
using System.Collections;

/**
 * Script that positions the main camera on a given point of 
 * interest (Ryu) with the correct offset to show the whole height of the map.
 */
public class CameraFollow : MonoBehaviour {
	
	// Point of Interest (i.e. Ryu for the Main Camera)
	public Transform 	poi;

	public Vector3		offset = new Vector3(0,12.625f,-5);
	
	void Update () {
		Vector3 offsetPos = poi.position + offset;
		transform.position = offsetPos;
	}
}
