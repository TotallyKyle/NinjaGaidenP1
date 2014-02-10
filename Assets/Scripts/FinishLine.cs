using UnityEngine;
using System.Collections;

public class FinishLine : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D collision) {
		GameData.timerData = 150;
		Application.LoadLevel("Boss Scene");
	}
}
