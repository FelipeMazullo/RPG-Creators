using UnityEngine;
using System.Collections;

public class HealthBar : EnergyBar {

	public override void OnGUI () {
		if (!BE.status.Contains (statusConditionNames.Dead)) { 
			if (type == EnergyBarType.Player) {
				GUI.Box (new Rect (10, 10, barLength, 20), 
				         BE.energyStats[(int) EnergyStatNames.HP].BaseValue + "/" + BE.baseStats[(int) BaseStatNames.MaxHP].AdjustedBaseValue);
			} else if (type == EnergyBarType.Enemy) {
				GUI.Box (new Rect (280, 40, barLength, 20), 
				         BE.energyStats[(int) EnergyStatNames.HP].BaseValue + "/" + BE.baseStats[(int) BaseStatNames.MaxHP].AdjustedBaseValue);
			}
		}
	}

	public override void AdjustLength () {
		barLength = (Screen.width/2) * (BE.energyStats[(int) EnergyStatNames.HP].BaseValue / 
	                                      BE.baseStats[(int) BaseStatNames.MaxHP].BaseValue);
	}
}

