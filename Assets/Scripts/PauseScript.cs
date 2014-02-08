using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {

	public AudioSource music;
	public AudioClip pauseClip;

	private bool paused = false;

	void Update () {
		if (Input.GetKeyDown(KeyCode.Return)) {
			if (paused) {
				music.enabled = true;
				Utilities.ResumeGame();
				paused = false;
			} else {
				music.enabled = false;
				AudioSource.PlayClipAtPoint(pauseClip, transform.position);
				Utilities.PauseGame();
				paused = true;
			}
		}
	}
}
