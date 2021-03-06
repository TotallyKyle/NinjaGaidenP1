using UnityEngine;
using System.Collections;

public class BonusItem : ItemScript {

	public int scoreBonus = 500;

	public override bool IsAutomatic() {
		return true;
	}

	public override void Deploy() {
		GameData.scoreData += scoreBonus;
	}

	public override void OnPickedUp() {
		Destroy(this.gameObject);
	}
}
