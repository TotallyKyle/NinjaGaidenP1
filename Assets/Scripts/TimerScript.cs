using UnityEngine;
using System.Collections;

public class TimerScript : MonoBehaviour {

	public GameData gameData;
	public AudioClip tickClip;

	void Start () {
		gameData = GameObject.Find("Game HUD").GetComponent<GameData>();
		InvokeRepeating("checkTime", 1, 1);
	}

	private void checkTime() {
		if (GameData.timerData == 0) {
			GameObject.Find("Ryu").GetComponent<Ryu>().die();
		} else {
			GameData.timerData--;
			if (GameData.timerData <= 10) {
				AudioSource.PlayClipAtPoint(tickClip, transform.position);
			}
		}
	}

	public void Stop() {
		CancelInvoke();
	}
}
