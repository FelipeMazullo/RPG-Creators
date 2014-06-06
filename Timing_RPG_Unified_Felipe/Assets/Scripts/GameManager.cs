using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;

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
	//ID of the warp point where the player should spawn in the current scene.
	public int wpSpawnID;
	public List<int> openChests;
	//Not used any more.
	public bool enemyDefeated;

	//Stores the event keys.
	public GameKeys keys;

	private EnergyStat[] es;
	private GameObject overworldPlayer;

	//References.
	private BattleController bc;

	public float MusicVolume=1.0f;//MAX VOLUME
	public float SfxVolume=1.0f;//MAX VOLUME
	//Guarantees this will be always a singleton only - can't use the constructor!
	protected GameManager () {}

	void Awake () {
		int curKey;

		playerPos = new Vector3 (0.5f, 0f, 0f);
		lastScene = "TitleScreen";
		wpSpawnID = 1;
		inventory = new List<Item> ();
		enemyID = new List<int> ();
		openChests = new List<int> ();
		enemyDefeated = false;
		//Initialize game keys (properties of the "GameKeys" class) from the saved data.
		keys = new GameKeys();
		PropertyInfo[] properties  = typeof (GameKeys).GetProperties();
		foreach (PropertyInfo prop in properties) {
			curKey = PlayerPrefs.GetInt (prop.Name);
			if (curKey == 1) {
				prop.SetValue (keys, true, null);
			} else {
				prop.SetValue (keys, false, null);
			}
		}

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
