using UnityEngine;
using System.Collections;

public class PlayerCharacter : BattleEntity {
	public characterNames charName;

	public override void Awake () {
		//Base class "Awake" function is also executed.
		base.Awake();
		type = BattleEntityType.Player;
	}

	// Use this for initialization
	public override void Start () {
		//Base class "start" function is also executed.
		base.Start();
		//Reference to the ki bar.
		KPBar = (KiBar) gameObject.GetComponent<KiBar>();
	}

	public void Initialization (characterNames cn) {
		charName = cn;
		//Initialize energy stats with the values saved in the game manager (persistent object).
		energyStats[(int) EnergyStatNames.HP].BaseValue = GameManager.Instance.GetEnergy(charName, EnergyStatNames.HP);
		energyStats[(int) EnergyStatNames.KP].BaseValue = GameManager.Instance.GetEnergy(charName, EnergyStatNames.KP);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void ListenTick() {
		Action act;

		if (!status.Contains(statusConditionNames.Dead)) {
			actionPoints += baseStats[(int) BaseStatNames.Speed].AdjustedBaseValue;
			
			//The action bar length determines when to take an action.
			if (actionPoints >= BattleController.ACTION_BAR_LENGTH) {
				bc.Freeze();

				actionPoints -= BattleController.ACTION_BAR_LENGTH;

				act = new Action();
				act.subject = gameObject;
				act.description = actionNames.WaitingForInput;
				SendAction(act);

			}
		}
	}
}

public enum characterNames {
	A,
	S,
	D
}
