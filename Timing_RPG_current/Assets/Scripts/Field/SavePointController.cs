using UnityEngine;
using System.Collections;
using System.Reflection;

public class SavePointController : Interactable_Controller {
	private float MESSAGE_WIDTH = 100f, MESSAGE_HEIGHT = 70f, MESSAGE_OFFSET = 30f, OPTION_WIDTH = 30f, LINE_HEIGHT = 30f;

	// Use this for initialization
	public override void Start () {
		//Base class "start" function is also executed.
		base.Start();
		numberChoices = 2;
	}

	public override void Interact (GameObject interactObj) {
		if (interactObj == this.gameObject) {
			dialogueStage++;

			//Asks whether the player wants to save.
			if (dialogueStage == 1) {
				interactionChoice = true;
				cursorPosition = 1;
			} else if (dialogueStage == 2) {
				//Yes, save game.
				if (cursorPosition == 1) {
					SaveGameData();
					dialogueStage = 3;
				//No, don't save game.
				} else {
					pCon.state = PlayerOverworldController.State.Idle;
					dialogueStage = 0;
				}
			//"Game saved" message.
			} else if (dialogueStage >= 4) {
				pCon.state = PlayerOverworldController.State.Idle;
				dialogueStage = 0;
			}
		}
	}


	//Saved data is accessed by going to "Start" (Windows); typing "regedit"; and then going
	//into HKEY_CURRENT_USER -> Software -> company name -> project name
	//(the last two can both be found under Edit->Project Settings -> Player)
	private void SaveGameData () {
		PropertyInfo[] properties;
		bool curKey;

		//Deletes previously saved data.
		PlayerPrefs.DeleteAll();

		//Save game keys.
		properties  = typeof (GameKeys).GetProperties();
		foreach (PropertyInfo prop in properties) {
			curKey = (bool) prop.GetValue (GameManager.Instance.keys, null); 
			if (curKey) {
				PlayerPrefs.SetInt (prop.Name, 1);
			} else {
				PlayerPrefs.SetInt (prop.Name, 0);
			}
		}

		//test.
		PlayerPrefs.SetString ("Character Name", "Dan");

		Debug.Log ("Data saved!");
	}

	void OnGUI () {
		if (dialogueStage == 1) {
			GUI.Box(new Rect (0, Screen.height - MESSAGE_HEIGHT, Screen.width, MESSAGE_HEIGHT), 
			        "Do you want to save the game?");
			GUI.Button (new Rect (MESSAGE_OFFSET , Screen.height - MESSAGE_HEIGHT + MESSAGE_OFFSET, OPTION_WIDTH, LINE_HEIGHT), "Yes");
			GUI.Button (new Rect (MESSAGE_OFFSET * 3 , Screen.height - MESSAGE_HEIGHT + MESSAGE_OFFSET, OPTION_WIDTH, LINE_HEIGHT), "No");
			GUI.DrawTexture(new Rect(MESSAGE_OFFSET-32 + (cursorPosition - 1) * MESSAGE_OFFSET * 2, 
			                         Screen.height - MESSAGE_HEIGHT + MESSAGE_OFFSET,32,32),cursor);//DRAWS THE CURSOR
		} else if (dialogueStage == 3) {
			GUI.Box(new Rect (0, Screen.height - MESSAGE_HEIGHT, Screen.width, MESSAGE_HEIGHT), 
			        "Game data saved!");
		}

	}
}
