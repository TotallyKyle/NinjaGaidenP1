using UnityEngine;
using System.Collections;

public class StartScript : MonoBehaviour {
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Return)) {
			Application.LoadLevel("Prod Scene");
		} else if (Input.GetKeyDown(KeyCode.Space)) {
			Application.LoadLevel("Boss Scene");
		}
	}
}
