using UnityEngine;
using System.Collections;

public class SpiritPowerItem : ItemScript {

	public int spiritPowerValue = 5;

	public override bool isAutomatic() {
		return true;
	}

	public override void deploy() {
		// TODO increment spirit power by value amount
        GameObject.Find("Game HUD").GetComponent<GameData>().spiritData+= spiritPowerValue;
	}

	public override void onPickedUp() {
		Destroy(this.gameObject);
	}
}
