using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	//Signals that we are waiting for the player input.
	public bool playerInput;
	//Active during target selection.
	public bool selectingTarget;
	public bool selectingSkill;

	private GameObject selectedTarget;

	//Active when the player starts inputting his command.
	private bool startInput;

	private Action nextAct;
	private Action previousAct;

	private PlayerCharacter pChar;

	private BattleController bc;

	private BattleGUI BGUI;
	
	// Use this for initialization
	void Start () {
		playerInput = false;
		selectingTarget = false;
		selectingSkill = false;
		selectedTarget = null;
		startInput = true;
		nextAct = new Action();

		//Reference to the battle controller.
		bc = (BattleController) (GameObject.FindGameObjectWithTag("BattleController").GetComponent<BattleController> ());
		//Reference to the battle GUI.
		BGUI = (BattleGUI) (GameObject.FindGameObjectWithTag("BattleGUI")).GetComponent<BattleGUI>();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerInput) {
			if (startInput) {
				previousAct = nextAct;
				startInput = false;
			}
			if (!selectingTarget && !selectingSkill) {
				if (Input.GetKeyDown(KeyCode.A)) {
					//EnemyHealth eh = (EnemyHealth)target.GetComponent("EnemyHealth");
					//eh.AdjustCurrentHealth(-50);

					nextTarget(1);

					selectingTarget = true;
					nextAct = new Action();
					nextAct.description = actionNames.Attack;
				} else if (Input.GetKeyDown(KeyCode.S)) {
					nextAct = new Action();
					selectingSkill = true;
				} else if (Input.GetKeyDown(KeyCode.R)) {
					nextAct = new Action();
					nextAct.subject = bc.curChar;
					nextAct.target = null;
					nextAct.description = actionNames.Run;

					playerInput = false;
					startInput = true;

					//Sends the action to the action pool.
					SendActionToBeginning (nextAct);
					//Not the active character anymore.
					bc.curChar.renderer.material.color = Color.green;
				}
			} else if (selectingSkill) {
				if (!BGUI.insufficientKP) {
					if (Input.GetKeyDown(KeyCode.S)) {
						pChar = (PlayerCharacter) bc.curChar.GetComponent<PlayerCharacter>();
						if (pChar.energyStats[(int) EnergyStatNames.KP].BaseValue - 15f < 0) {
							//Shows message stating it's not possible to make that action.
							BGUI.insufficientKP = true;
						} else {
							nextAct.description = actionNames.Super;

							selectingTarget = true;

							selectingSkill = false;
							nextTarget(1);
						}
					} else if (Input.GetKeyDown(KeyCode.E)) {
						nextAct.description = actionNames.Energy;

						selectingTarget = true;

						selectingSkill = false;
						nextTarget(1);
					}
				}
			} else if (selectingTarget) {
				if (Input.GetKeyDown(KeyCode.DownArrow)) {
					nextTarget(1);
				} else if (Input.GetKeyDown(KeyCode.UpArrow)) {
					nextTarget(-1);
				} else if (Input.GetKeyDown(KeyCode.Space)) {
					//Spend KP in the case of a super attack.
					if (nextAct.description == actionNames.Super) {
						pChar = (PlayerCharacter) bc.curChar.GetComponent<PlayerCharacter>();
						pChar.AdjustCurrentKP (-15f);
					}

					selectedTarget.renderer.material.color = Color.blue;
					nextAct.subject = bc.curChar; 
					nextAct.target = selectedTarget;

					//Sends the action to the first slot of the action pool
					//(action occurs right after player selects it).
					SendActionToBeginning (nextAct);

					playerInput = false;
					startInput = true;
					selectingTarget = false;

					//Not the active character anymore.
					bc.curChar.renderer.material.color = Color.green;
				}
			}
		}
	}

/*	private Action CreateAction () {
		//Gets the input in the "Update()" function, by using "yield"
		//and an IEnumerator function.
		playerInput = true;
		WaitInput();
		return nextAct;
	}

	IEnumerator WaitInput () {
		while (true) {
			yield return new WaitForFixedUpdate();
			if (!playerInput) {
				break;
			}
		}
	}*/

	//Adds the action to the beginning of the "actionPool" list.
	private void SendActionToBeginning (Action act) {
		bc.actionPool.Insert(0, act);
	}

	private void nextTarget (int increment) {
		int index;
		EnemyCharacter eChar;

		//First target in the list.
		if (selectedTarget == null) {
			selectedTarget = bc.enemies[0];
			selectedTarget.renderer.material.color = Color.red;
		} else {
			index = bc.enemies.IndexOf(selectedTarget);

			//Deselects current target...
			selectedTarget.renderer.material.color = Color.blue;

			//...and selects the next one (which is not dead).
			if (increment == 1) {
				selectedTarget = null;
				while (selectedTarget == null) {
					if (index < bc.enemies.Count - 1) {
						eChar = (EnemyCharacter) bc.enemies[index + 1].GetComponent<EnemyCharacter> ();
					} else {
						eChar = (EnemyCharacter) bc.enemies[0].GetComponent<EnemyCharacter> ();
					}
					if (!(eChar.status.Contains(statusConditionNames.Dead))) {
						if (index < bc.enemies.Count - 1) {
							selectedTarget = bc.enemies[index + 1];
						} else {
							selectedTarget = bc.enemies[0];	
						}
					} else {
						index++;
						if (index > bc.enemies.Count - 1) {
							index = 0;
						}
					}
				}
			} else if (increment == -1) {
				selectedTarget = null;
				while (selectedTarget == null) {
					if (index > 0) {
						eChar = (EnemyCharacter) bc.enemies[index - 1].GetComponent<EnemyCharacter> ();
					} else {
						eChar = (EnemyCharacter) bc.enemies[bc.enemies.Count - 1].GetComponent<EnemyCharacter> ();
					}
					if (!(eChar.status.Contains(statusConditionNames.Dead))) {
						if (index > 0) {
							selectedTarget = bc.enemies[index - 1];
						} else {
							selectedTarget = bc.enemies[bc.enemies.Count - 1];	
						}
					} else {
						index--;
						if (index < 0) {
							index = bc.enemies.Count - 1;
						}
					}
				}
			}
			selectedTarget.renderer.material.color = Color.red;
		}
	}

}
