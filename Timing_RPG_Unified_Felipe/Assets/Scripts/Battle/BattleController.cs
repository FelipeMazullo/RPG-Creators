using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleController : MonoBehaviour {
	public List<GameObject> enemies;
	//List of the actions to be executed by the battle controller.
	//Sent by the battle entities (player or enemies).
	public List<Action> actionPool;
	public List<GameObject> playerChars;
	public GameObject curChar;

	//Constants.
	public const float ACTION_BAR_LENGTH = 100f;

	//Determines if battle actions are frozen.
	public bool frozen;
	//Indicates a successful run from battle.
	private bool gotAwaySafely;

	public animationStatus AS;

	//References.
	private BattleGUI BGUI;
	private BattleAnimator anim;
	PlayerController pCon;
	ActionTimer ActT;
	
	// Use this for initialization
	void Start () {
		frozen = false;
		gotAwaySafely = false;
		AS = animationStatus.NoAnimation;
		curChar = null;
		actionPool = new List<Action> ();

		//Generate the enemies and obtain a list of references to them.
		EnemyGenerator eg = (EnemyGenerator) (GameObject.FindGameObjectWithTag("EnemyGenerator")).GetComponent<EnemyGenerator>();
		enemies = eg.Generate();

		//Reference to the player.
		PlayerCharactersInstantiator pci = (PlayerCharactersInstantiator) (this.GetComponent<PlayerCharactersInstantiator>());
		playerChars = pci.GetCharacters();

		//Reference to the battle GUI.
		BGUI = (BattleGUI) (GameObject.FindGameObjectWithTag("BattleGUI")).GetComponent<BattleGUI>();

		//Reference to the player controller.
		pCon = (PlayerController) gameObject.GetComponent<PlayerController>();

		//Reference to the animator.
		anim = (BattleAnimator) (GameObject.FindGameObjectWithTag("BattleAnimator")).GetComponent<BattleAnimator>();

		//Reference to the action timer.
		ActT = (ActionTimer) (this.GetComponent<ActionTimer>());
	}

	// Update is called once per frame
	void Update () {
		if (!frozen) {
			Tick();
			if (actionPool.Count > 0) {
				//Freezes the battle controller in order to execute the received actions.
				Freeze();
				//Reorganized actions in the pool so that player actions 
				//have priority among the actions received in this tick.
				ReorderActions();
			}
		//Frozen: actions are taking place.
		} else {
			//Action pool not empty and action not in progress: All actions must be executed.
			if (actionPool.Count > 0 && (AS == animationStatus.NoAnimation) && (! (pCon.playerInput))) {
				//In case it's an action that will be taken by the player, activates player input.
				if (actionPool[0].description == actionNames.WaitingForInput) {
					pCon.playerInput = true;

					actionPool[0].subject.renderer.material.color = Color.red;
					curChar = actionPool[0].subject;
					actionPool.RemoveAt(0);
				//In case the action is ready, executes it.
				} else {
					AS = animationStatus.AnimationInProgress;
					StartCoroutine (ExecuteAction());
				}
			//Action pool empty and nothing (nor player input nor animation) is in progress:
			//can unfreeze for the next tick.
			} else if (actionPool.Count <= 0 && (! (pCon.playerInput)) && (AS == animationStatus.NoAnimation)) {
				Unfreeze();
			}

			//Current animation finished.
			if (AS == animationStatus.AnimationEnded) {
				StartCoroutine (TestEndBattle());
				AS = animationStatus.NoAnimation;
			}
		}
	}

	//Warns battle entities (player and enemies) that a tick has passed.
	private void Tick () {
		foreach (GameObject PC in playerChars) {
			PlayerCharacter pChar = (PlayerCharacter) PC.GetComponent<PlayerCharacter> ();
			pChar.ListenTick();
		}

		foreach (GameObject en in enemies) {
			EnemyCharacter eChar = (EnemyCharacter) en.GetComponent<EnemyCharacter> ();
			eChar.ListenTick();
		}
	}

	//When the battle controller is frozen, its ticks stop happening. In addition,
	//no actions from the pool is executed.
	public void Freeze () {
		frozen = true;
	}

	public void Unfreeze () {
		frozen = false;
	}

	private void ReorderActions() {
		Action[] oldActionPool;

		oldActionPool = new Action[actionPool.Count];
		actionPool.CopyTo (oldActionPool);

		//Fills the action pool first with the actions made by players.
		actionPool.Clear();
		foreach (Action act in oldActionPool) {
			if (act.subject != null) {
				if (act.subject.tag == "Player") {
					actionPool.Add (act);
				}
			}
		}

		//Then, adds the remaining actions.
		foreach (Action act in oldActionPool) {
			if (act.subject != null) {
				if (act.subject.tag != "Player") {
					actionPool.Add (act);
				}
			}
		}

		return;
	}

	private IEnumerator ExecuteAction () {
		GameObject PC = null;
		PlayerCharacter pChar = null;
		float damage;
		float timingBonusFactor;
		bool dodge;
		bool subjectDead;

		subjectDead = false;

		Action act = actionPool[0];

		actionPool.RemoveAt(0);

		//Case that should not exist: Action without subject.
		if (act.subject == null) {
			Debug.LogWarning ("Action without subject!");
			return true;
		}

		//Checks if the current act's subject has not disappeared (for instance, is dead).
		if (act.subject != null) {
			if (act.target != null) {
				Debug.Log("Subject " + act.subject.tag.ToString() + "; Target " + act.target.tag.ToString());
			} 
			
			if (act.subject.tag == "Player") {
				PC = act.subject;
			} else if (act.target.tag == "Player") {
				PC = act.target;
			}
			
			if (PC != null) {
				pChar = (PlayerCharacter) PC.GetComponent<PlayerCharacter>();
			}
			
			//Checks if the subject is dead.
			subjectDead = false;
			if (act.subject.tag == "Player") {
				if (pChar.status.Contains (statusConditionNames.Dead)) {
					subjectDead = true;
				}
			} else if (act.subject.tag == "Enemy") {
				EnemyCharacter eChar = (EnemyCharacter) act.subject.GetComponent<EnemyCharacter> ();
				if (eChar.status.Contains (statusConditionNames.Dead)) {
					subjectDead = true;
				}
			}

			/*if (act.description == actionNames.Run) {
				Debug.Log("Running...\n");
				//50% chance of running away.
				float rand = Random.value;
				if (rand <= 0.5f) {
					BGUI.successRun = true;
					gotAwaySafely = true;
				} else {
					BGUI.failedRun = true;
				}
			}*/
		}

		//Performs most actions, using the player's first button press to
		//potentially increase the action's efficiency or dodge an enemy's attack.
		if (act.description != actionNames.Run) {
			ActT.InitializeTimer (2f, TimerModes.FirstPress);
		//In the case of running from battle, uses the count of how many times the 
		//player pressed the button.
		} else {
			BGUI.running = true;
			ActT.InitializeTimer (1f, TimerModes.MostPresses);
			yield return new WaitForSeconds (1f);
			BGUI.running = false;
			if (ActT.timesPressed >= 5) {
				BGUI.successRun = true;
				gotAwaySafely = true;
			} else {
				BGUI.failedRun = true;
			}
			yield return new WaitForSeconds (1f);
			BGUI.failedRun = false;
		}
		//Animation for every action.
		yield return StartCoroutine (anim.Animate (act));
		
		//Performs the action (damage calculations).
		if (act.subject != null && act.description != actionNames.Run) {
			if (!subjectDead) {
				//Determine bonus provided by good timing.
				timingBonusFactor = 1f;
				if (act.subject.tag == "Player") {
					if (ActT.percentageWhenFirstPressed >= 0.4f && 
					    ActT.percentageWhenFirstPressed <= 0.5f) {
						Debug.Log ("Good timing!");
						timingBonusFactor = 1.5f;
					}
				}

				//Determine if the player was able to dodge the enemy attack.
				dodge = false;
				if (act.subject.tag == "Enemy") {
					if (ActT.percentageWhenFirstPressed >= 0.4f && 
					    ActT.percentageWhenFirstPressed <= 0.5f) {
						Debug.Log ("Attack dodged!");
						dodge = true;
					}
				}

				if (act.description == actionNames.Attack || act.description == actionNames.Super) {
					if (act.subject.tag == "Player") {
						EnemyCharacter eChar = (EnemyCharacter) act.target.GetComponent<EnemyCharacter> ();
						damage = pChar.baseStats[(int) BaseStatNames.Attack].AdjustedBaseValue - 
							eChar.baseStats[(int) BaseStatNames.Defense].AdjustedBaseValue;
						//Super strike does double damage.
						if (act.description == actionNames.Super) {
							damage = damage * 2;
						}
						damage = damage * timingBonusFactor;
						eChar.AdjustCurrentHP((-1f) * damage);
					} else if (act.subject.tag == "Enemy") {
						if (!dodge) {
							EnemyCharacter eChar = (EnemyCharacter) act.subject.GetComponent<EnemyCharacter> ();
							damage = eChar.baseStats[(int) BaseStatNames.Attack].AdjustedBaseValue
								- pChar.baseStats[(int) BaseStatNames.Defense].AdjustedBaseValue;
							pChar.AdjustCurrentHP ((-1f) * damage);
						}
					}
				}

				//Energy strike by the player.
				if (act.description == actionNames.Energy) {
					EnemyCharacter eChar = (EnemyCharacter) act.target.GetComponent<EnemyCharacter> ();
					damage = pChar.baseStats[(int) BaseStatNames.Willpower].AdjustedBaseValue - 
						eChar.baseStats[(int) BaseStatNames.Spirit].AdjustedBaseValue;
					damage = damage * timingBonusFactor;
					eChar.AdjustCurrentHP((-1f) * damage);
				}

			}
		} 

	}

	private IEnumerator TestEndBattle () {
		int numPlayerCharsDead = 0;
		int numEnemiesDead = 0;
		PlayerCharacter pChar;
		EnemyCharacter eChar;

		foreach (GameObject PC in playerChars) {
			pChar = (PlayerCharacter) PC.GetComponent<PlayerCharacter> ();
			if (pChar.energyStats[(int) EnergyStatNames.HP].BaseValue <= 0) {
				numPlayerCharsDead++;
			}
		}

		foreach (GameObject EC in enemies) {
			eChar = (EnemyCharacter) EC.GetComponent<EnemyCharacter> ();
			if (eChar.energyStats[(int) EnergyStatNames.HP].BaseValue <= 0) {
				numEnemiesDead++;
			}
		}

		//Player defeated.
		if (numPlayerCharsDead >= playerChars.Count) {
			GameManager.Instance.enemyDefeated = false;
			//Remove the enemy from the list of potentially defeated.
			GameManager.Instance.enemyID.RemoveAt(GameManager.Instance.enemyID.Count - 1);
			BGUI.gameOver = true;
			yield return StartCoroutine (EndBattleTimer());
		//Enemies defeated. 
		} else if (numEnemiesDead >= enemies.Count) {
			GameManager.Instance.enemyDefeated = true;
			//Saves the energy stats of the player characters in the GameManager.
			GameManager.Instance.UpdateAllEnergy(playerChars);
			BGUI.victory = true;
			yield return StartCoroutine (EndBattleTimer());
		//Ran from battle.
		} else if (gotAwaySafely) {
			GameManager.Instance.enemyDefeated = false;
			//Remove the enemy from the list of potentially defeated.
			GameManager.Instance.enemyID.RemoveAt(GameManager.Instance.enemyID.Count - 1);
			//Saves the energy stats of the player characters in the GameManager.
			GameManager.Instance.UpdateAllEnergy(playerChars);
			EndBattle();
			yield return null;
		}
	}

	private void EndBattle () {
		Application.LoadLevel (GameManager.Instance.lastScene);
	}

	private IEnumerator EndBattleTimer () {
		yield return new WaitForSeconds (1);
		Application.LoadLevel (GameManager.Instance.lastScene);
	}

}

public enum animationStatus {
	NoAnimation,
	AnimationInProgress,
	AnimationEnded
}
