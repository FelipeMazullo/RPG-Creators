using UnityEngine;
using System.Collections;

public class ChestManager : MonoBehaviour {
	private GameObject[] chests;
	private ChestController cCon;
	private SpriteRenderer cSprite;

	// Use this for initialization
	void Start () {
		//Correct state for the chests in the scene.
		chests = GameObject.FindGameObjectsWithTag("OverworldItem");
		foreach (GameObject c in chests) {
			cCon = c.GetComponent<ChestController>();
			if (cCon != null) {
				cSprite = c.GetComponent<SpriteRenderer>();
				if (GameManager.Instance.openChests.Contains(cCon.ID)) {
					cSprite.sprite = cCon.openSprite;
					cCon.cState = ChestState.Open;
				} else {
					cSprite.sprite = cCon.closedSprite;
					cCon.cState = ChestState.Closed;
				}
				//Quest completed made chest appear.
				if (Application.loadedLevelName == "TestChamberWest") {
					if (GameManager.Instance.keys.TestChamberWestChestAppears) {
						cSprite.enabled = true;
						cCon.GetComponent<BoxCollider2D>().enabled = true;
					}
				}
			}
		}
	}

}
