using UnityEngine;
using System.Collections;

public class BattleAnimator : MonoBehaviour {
	private BattleController bc;

	// Use this for initialization
	void Start () {
		//Reference to the battle controller.
		bc = (BattleController) (GameObject.FindGameObjectWithTag("BattleController").GetComponent<BattleController> ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public IEnumerator Animate (Action act) {
		bool subjectDead;

		//Checks if the subject is dead.
		subjectDead = false;
		if (act.subject.tag == "Player") {
			PlayerCharacter pChar = (PlayerCharacter) act.subject.GetComponent<PlayerCharacter>();
			if (pChar.status.Contains (statusConditionNames.Dead)) {
				subjectDead = true;
			}
		} else if (act.subject.tag == "Enemy") {
			EnemyCharacter eChar = (EnemyCharacter) act.subject.GetComponent<EnemyCharacter> ();
			if (eChar.status.Contains (statusConditionNames.Dead)) {
				subjectDead = true;
			}
		}
		
		if (!subjectDead) {
			//Animates if the subject is present (i.e., not dead).
			if (act.subject != null) {
				if (act.description == actionNames.Attack || act.description == actionNames.Super) {
					//Attacking entity animation.
					if (act.subject.tag == "Player") {
						act.subject.transform.position += act.subject.transform.right * 2;
						yield return new WaitForSeconds(1);
						act.subject.transform.position -= act.subject.transform.right * 2;
						//StartCoroutine (PlayerAttack());
					} else if (act.subject.tag == "Enemy") {
						act.subject.transform.position -= act.subject.transform.right * 2;
						yield return new WaitForSeconds(1);
						act.subject.transform.position += act.subject.transform.right * 2;
						//StartCoroutine (EnemyAttack(act));
					}

					//Damaged entity animation.
					if (act.target != null) {
						if (act.target.tag == "Player") {
							act.target.transform.position -= act.target.transform.right * 2;
							yield return new WaitForSeconds(1);
							act.target.transform.position += act.target.transform.right * 2;
						} else if (act.target.tag == "Enemy") {
							act.target.transform.position += act.target.transform.right * 2;
							yield return new WaitForSeconds(1);
							act.target.transform.position -= act.target.transform.right * 2;
						}
					}
				} else if (act.description == actionNames.Energy) {
					//Attacking entity animation.
					act.subject.transform.position += act.subject.transform.up * 2;
					yield return new WaitForSeconds(1);
					act.subject.transform.position -= act.subject.transform.up * 2;
					//StartCoroutine (PlayerAttack());

					//Damaged entity (in this case, an enemy) animation.
					if (act.target != null) {
						act.target.transform.position += act.target.transform.right * 2;
						yield return new WaitForSeconds(1);
						if (act.target != null) {
							act.target.transform.position -= act.target.transform.right * 2;
						}
					}
				}
			}
		}

		bc.AS = animationStatus.AnimationEnded;
	}

	/*private IEnumerator PlayerAttack () {
		player.transform.position += player.transform.right * 2;
		yield return new WaitForSeconds(1);
		player.transform.position -= player.transform.right * 2;
	}

	private IEnumerator EnemyAttack (Action act) {
		act.subject.transform.position -= act.subject.transform.right * 2;
		yield return new WaitForSeconds(1);
		act.subject.transform.position += act.subject.transform.right * 2;
	}*/
}
