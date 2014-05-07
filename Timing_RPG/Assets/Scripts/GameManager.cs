using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

//This class is primarily responsible for communication between
//different scenes (levels). Variables which should not be 
//destroyed when changing scenes are managed here.
public class GameManager : Singleton<GameManager> {
	public const int NUM_PLAYER_CHARACTERS = 3;

	public Vector3 playerPos;
	public EnergyStat[,] playerEnergy;
	//The name of the last scene the player was in (used to return there after a battle).
	public string lastScene; 
	public List<Item> inventory;
	//List of the IDs of the field enemies that were defeated in the current scene.
	//Temporarily contains IDs of potentially destroyed enemies when entering a battle.
	public List<int> enemyID;
	//Not used any more.
	public bool enemyDefeated;

	//Stores the event keys.
	public GameKeys keys;

	private EnergyStat[] es;
	private GameObject overworldPlayer;

	//References.
	private BattleController bc;

	//Guarantees this will be always a singleton only - can't use the constructor!
	protected GameManager () {}

	void Awake () {
		playerPos = new Vector3 (0, 2f, 0);
		lastScene = Application.loadedLevelName;
		inventory = new List<Item> ();
		enemyID = new List<int> ();
		enemyDefeated = false;
		//!!!Make this general!!!
		keys = new GameKeys();
		keys.TestChamberEastClear = false;

		playerEnergy = new EnergyStat[NUM_PLAYER_CHARACTERS, Enum.GetValues(typeof (EnergyStatNames)).Length];
		for (int i = 0; i < NUM_PLAYER_CHARACTERS; i++) {
			for (int j = 0; j < Enum.GetValues(typeof (EnergyStatNames)).Length; j++) {
				playerEnergy[i, j] = new EnergyStat();
				playerEnergy[i, j].Name = ((EnergyStatNames)j).ToString();
			}
		}
		for (int i = 0; i < NUM_PLAYER_CHARACTERS; i++) {
			//playerEnergy[i] = new EnergyStat[Enum.GetValues(typeof (EnergyStatNames)).Length]; 
			playerEnergy[i, (int) EnergyStatNames.HP].BaseValue = 100f;
			playerEnergy[i, (int) EnergyStatNames.KP].BaseValue = 25f;
		}
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public float GetEnergy (characterNames charN, EnergyStatNames es) {
		return (playerEnergy[(int) charN, (int) es].BaseValue);
	}

	//Saves the character's energy stats.
	public void UpdateAllEnergy (List<GameObject> playerChars) {
		PlayerCharacter pChar;

		foreach (characterNames cn in Enum.GetValues (typeof (characterNames))) {
			pChar = (PlayerCharacter) (playerChars[(int) cn].GetComponent<PlayerCharacter>());
			playerEnergy[(int) cn, (int) EnergyStatNames.HP].BaseValue = pChar.energyStats[(int) EnergyStatNames.HP].BaseValue;
			playerEnergy[(int) cn, (int) EnergyStatNames.KP].BaseValue = pChar.energyStats[(int) EnergyStatNames.KP].BaseValue;
		}
	}
	
	//Initialization specific to each level.
	void OnLevelWasLoaded (int level) {
		//Going to field view.
		if (keys.TestChamberEastClear) {
			Debug.Log ("Yes!");
		} else {
			Debug.Log ("No!");
		}
		if (level >= 0 && level <= 1) {
			//Reference to the player.
			overworldPlayer = GameObject.FindGameObjectWithTag("Player");
			overworldPlayer.transform.position = playerPos;
		} else {
			//Reference to the battle controller.
			bc = (BattleController) (GameObject.FindGameObjectWithTag("BattleController").GetComponent<BattleController> ());
		}
	}
}
