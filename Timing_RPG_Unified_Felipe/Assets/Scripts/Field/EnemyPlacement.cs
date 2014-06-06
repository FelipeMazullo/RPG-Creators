using UnityEngine;
using System.Collections;

//Responsible for placing the enemies in the overworld view.
public class EnemyPlacement : MonoBehaviour {

	void Awake () {
		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy")) {
			//Places enemies which were not already defeated.
			if (GameManager.Instance.enemyID.Contains (obj.GetComponent<FieldEnemy>().ID)) {
				obj.SetActive (false);
			}
		}
	}
	
	//Placement itself still needs to be done, 
	//according to the necessities of each specific scene.

}
