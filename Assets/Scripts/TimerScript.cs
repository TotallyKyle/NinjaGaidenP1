using UnityEngine;
using System.Collections;

public class TimerScript : MonoBehaviour {

	public GameData gameData;

	void Start () {
		gameData = GameObject.Find("Game HUD").GetComponent<GameData>();
		InvokeRepeating("checkTime", 1, 1);
	}

	private void checkTime() {
		GameData.timerData -= 1;
		if (GameData.timerData == 0) {
			GameObject.Find("Ryu").GetComponent<Ryu>().die();
		}
	}

	public void Stop() {
		CancelInvoke();
	}
}
