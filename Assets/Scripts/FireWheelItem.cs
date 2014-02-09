using UnityEngine;
using System.Collections;

public class FireWheelItem : ItemScript {

	private Object fireWheel;

	public override bool IsAutomatic() {
		return true;
	}

	public override void Deploy() {
		// TODO instantiate fire wheel
	}

	public override void OnPickedUp() {
		// TODO set power up sprite in game data
		Destroy(this.gameObject);
	}
}
