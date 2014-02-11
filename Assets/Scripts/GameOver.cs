using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	void Start() {
		Invoke("Restart", 5f);
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Return)) {
			CancelInvoke();
			Restart();
		}
	}

	void Restart() {
		Application.LoadLevel("Start Scene");
	}
}
