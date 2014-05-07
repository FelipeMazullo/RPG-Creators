using UnityEngine;
using System.Collections;

public class WarpPointController : MonoBehaviour {
	public string dest;

	void OnCollisionEnter2D (Collision2D col) {
		//Transports the player to the relevant scene.
		if (col.gameObject.tag == "Player") {
			//Clears the list which keeps track of the defeated enemies.
			GameManager.Instance.enemyID.Clear();
			Application.LoadLevel (dest);
		}
	}
}
