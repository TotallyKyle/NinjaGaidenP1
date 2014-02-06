using UnityEngine;
using System.Collections;

public class BonusItem : ItemScript {

	public int scoreBonus = 500;

	public override bool isAutomatic() {
		return true;
	}

	public override void deploy() {
		GameObject.Find("Game HUD").GetComponent<GameData>().scoreData += scoreBonus;
	}

	public override void onPickedUp() {
		Destroy(this.gameObject);
	}
}
