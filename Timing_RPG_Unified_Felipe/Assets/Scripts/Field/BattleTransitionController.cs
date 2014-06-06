using UnityEngine;
using System.Collections;

public class BattleTransitionController : MonoBehaviour {
	private PlayerCollisionDetection pCol;

	// Use this for initialization
	void Start () {
		pCol = (PlayerCollisionDetection) GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerCollisionDetection>();
	}

	public void ChangeToBattle() {
		pCol.ChangeToBattle();
	}
}
