using UnityEngine;
using System.Collections;

public class KiBar : EnergyBar {

	public override void OnGUI () {
		if (!BE.status.Contains (statusConditionNames.Dead)) { 
			if (type == EnergyBarType.Player) {
				GUI.Box (new Rect (10, 40, barLength, 20), 
				         BE.energyStats[(int) EnergyStatNames.KP].BaseValue + "/" + BE.baseStats[(int) BaseStatNames.MaxKP].AdjustedBaseValue);
			}
		}
	}
	
	public override void AdjustLength () {
		barLength = (Screen.width/4) * (BE.energyStats[(int) EnergyStatNames.KP].BaseValue / 
		                                BE.baseStats[(int) BaseStatNames.MaxKP].BaseValue);
	}
}
