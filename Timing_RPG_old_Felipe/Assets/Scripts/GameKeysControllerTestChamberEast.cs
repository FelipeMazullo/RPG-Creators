using UnityEngine;
using System.Collections;

public class GameKeysControllerTestChamberEast : MonoBehaviour {

	// Use this for initialization
	void Start () {
		/*foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy")) {
			if (obj.activeSelf) {
				return;
			}
		}
		GameManager.Instance.keys.TestChamberEastClear = true;*/
		//"All enemies defeated" event.
		if (GameObject.FindGameObjectsWithTag("Enemy").Length <= 0) {
			GameManager.Instance.keys.TestChamberEastClear = true;
		}
	}
	

}
