using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyCharacter : BattleEntity {

	public override void Awake () {
		//Base class "Awake" function is also executed.
		base.Awake();
		type = BattleEntityType.Enemy;
	}

	// Use this for initialization
	public override void Start () {
		//Base class "start" function is also executed.
		base.Start();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void ListenTick() {
		List<GameObject> playerCharsAlive;
		PlayerCharacter pChar;
		float rand;

		playerCharsAlive = new List<GameObject> ();
		Action act = new Action();

		if (!status.Contains(statusConditionNames.Dead)) {
			actionPoints += baseStats[(int) BaseStatNames.Speed].AdjustedBaseValue;
			//The action bar length determines when to take an action.
			if (actionPoints >= BattleController.ACTION_BAR_LENGTH) {
				
				actionPoints -= BattleController.ACTION_BAR_LENGTH;

				//Create vector with the player character who are alive
				//(and thus can be targetted by the enemies).
				foreach (GameObject PC in bc.playerChars) {
					pChar = (PlayerCharacter) PC.GetComponent<PlayerCharacter>();
					if (!pChar.status.Contains (statusConditionNames.Dead)) {
						playerCharsAlive.Add (PC);
					}
				}
				
				//Enemy action.
				//TO DO: Decide the action and target!
				act = new Action();
				act.subject = gameObject;
				//Temporary: chooses a target among the player characters randomly.
				//When 3 are alive.
				if (playerCharsAlive.Count == 3) {
					rand = Random.value;
					if (rand <= 0.33f) {
						act.target = bc.playerChars[0];
					} else if (rand <= 0.66f) {
						act.target = bc.playerChars[1];
					} else {
						act.target = bc.playerChars[2];
					}
				//When 2 are alive.
				} else if (playerCharsAlive.Count == 2) {
					rand = Random.value;
					if (rand <= 0.5f) {
						act.target = playerCharsAlive[0];
					} else {
						act.target = playerCharsAlive[1];
					}
				//Only one alive.
				} else {
					act.target = playerCharsAlive[0];
				}
				act.description = actionNames.Attack;
				
				//Sends the action to the action pool.
				SendAction(act);
			}
		}
	}
}
