using UnityEngine;
using System.Collections;

//Responsible for placing the player in the overworld view.
public class PlayerPlacement : MonoBehaviour {
	private GameObject overworldPlayer;
	private GameObject[] warpPoints;
	private WarpPointController wpCon;

	void Awake() {

	}

	// Use this for initialization
	void Start () {
		//Coming from the battle to the overworld view.
		if (GameManager.Instance.lastScene == "Battle" || GameManager.Instance.lastScene == "TitleScreen") {
			overworldPlayer = GameObject.FindGameObjectWithTag("Player");
			overworldPlayer.transform.position = GameManager.Instance.playerPos;
		//Transitioning between different scenes in overworld view.
		} else {
			overworldPlayer = GameObject.FindGameObjectWithTag("Player");
			warpPoints = GameObject.FindGameObjectsWithTag ("WarpPoint");
			//Gets the specific warp point where the player should appear.
			foreach (GameObject wp in warpPoints) {
				wpCon = (WarpPointController) wp.GetComponent<WarpPointController>();
				if (wpCon.ID == GameManager.Instance.wpSpawnID) {
					overworldPlayer.transform.position = wp.transform.Find ("SpawnPoint").position;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
