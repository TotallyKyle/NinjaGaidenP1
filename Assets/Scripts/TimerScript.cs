using UnityEngine;
using System.Collections;

public class TimerScript : MonoBehaviour {

	public GameData gameData;

	void Start () {
		gameData = GameObject.Find("Game HUD").GetComponent<GameData>();
		InvokeRepeating("checkTime", 1, 1);
	}

	private void checkTime() {
		gameData.timerData -= 1;
		if (gameData.timerData == 0) {
			// TODO kill Ryu, freeze everything, and reload scene
		}
	}
}
