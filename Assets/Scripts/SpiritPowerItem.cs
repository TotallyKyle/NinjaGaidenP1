using UnityEngine;
using System.Collections;

public class SpiritPowerItem : ItemScript {

	public int spiritPowerValue = 5;

	public override bool IsAutomatic() {
		return true;
	}

	public override void Deploy() {
        GameData.spiritData+= spiritPowerValue;
	}

	public override void OnPickedUp() {
		Destroy(this.gameObject);
	}
}
