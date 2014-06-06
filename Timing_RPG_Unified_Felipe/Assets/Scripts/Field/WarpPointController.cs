using UnityEngine;
using System.Collections;

public class WarpPointController : MonoBehaviour {
	public string dest;
	public int ID, dest_ID;

	void OnCollisionEnter2D (Collision2D col) {
		//Transports the player to the relevant scene.
		if (col.gameObject.tag == "Player") {
			//Clears the list which keeps track of the defeated enemies.
			GameManager.Instance.enemyID.Clear();
			//Saves reference to the last scene.
			GameManager.Instance.lastScene = Application.loadedLevelName;
			//Sets the ID of the destination spawn point (next scene).
			GameManager.Instance.wpSpawnID = dest_ID;
			Application.LoadLevel (dest);
		}
	}
}
