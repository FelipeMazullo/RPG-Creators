using UnityEngine;
using System.Collections;

public class ScientistController : Interactable_Controller {
	private float MESSAGE_WIDTH = 100f, MESSAGE_HEIGHT = 70f;
	public GameObject itemToGive;
	private Item itemScript;
	private GameObject itemInstance;

	// Use this for initialization
	public override void Start () {
		//Base class "start" function is also executed.
		base.Start();
		itemScript = (Item) itemToGive.GetComponent<Item>();
		//New stage of dialogue when the quest in completed.
		if (GameManager.Instance.keys.TestChamberEastClear) {
			dialogueStage = 9;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Interact (GameObject interactObj) {
		if (interactObj == this.gameObject) {
			state = State.Interacting;
			dialogueStage++;

			//Initial dialogue.
			if (dialogueStage < 7) {
				Debug.Log ("Talked!");
				if (dialogueStage == 2 || dialogueStage == 6) {
					state = State.Idle;
					pCon.state = PlayerOverworldController.State.Idle;
					if (dialogueStage == 6) {
						Destroy (itemInstance);
						Debug.Log ("Destroyed!");
					}
				}
				//Gives an item to the player.
				if (dialogueStage == 5) {
					GameManager.Instance.inventory.Add (itemScript);
					itemInstance = pCon.ShowItem (itemToGive);
				}
			} else {
				//Loops indefinitely in the final piece of dialogue 
				//(spider quest still incomplete).
				if (dialogueStage == 8) {
					state = State.Idle;
					pCon.state = PlayerOverworldController.State.Idle;
					dialogueStage = 6;
				}
				//Loops indefinitely in the final piece of dialogue 
				//(spider quest still incomplete).
				if (dialogueStage == 11) {
					state = State.Idle;
					pCon.state = PlayerOverworldController.State.Idle;
					dialogueStage = 9;
				}
			}
		}
	}

	void OnGUI () {
		if (dialogueStage == 1) {
			GUI.Box(new Rect (0, Screen.height - MESSAGE_HEIGHT, Screen.width, MESSAGE_HEIGHT), 
			        "The day looks beautiful today, doesn't it?");
		} else if (dialogueStage == 3) {
			GUI.Box(new Rect (0, Screen.height - MESSAGE_HEIGHT, Screen.width, MESSAGE_HEIGHT), 
			        "Uh? So you wanted to know where you are supposed to go?");
		} else if (dialogueStage == 4) {
			GUI.Box(new Rect (0, Screen.height - MESSAGE_HEIGHT, Screen.width, MESSAGE_HEIGHT), 
			        "Take this key and go on ahead to the next room. There are lots of spiders to get rid of!");
		} else if (dialogueStage == 5) {
			GUI.Box(new Rect (0, Screen.height - MESSAGE_HEIGHT, Screen.width, MESSAGE_HEIGHT), 
			        itemScript.name + "\n" + itemScript.description);
		} else if (dialogueStage == 7) {
			GUI.Box(new Rect (0, Screen.height - MESSAGE_HEIGHT, Screen.width, MESSAGE_HEIGHT), 
			        "What are you waiting for? Good luck with the spiders!");
		} else if (dialogueStage == 10) {
			GUI.Box(new Rect (0, Screen.height - MESSAGE_HEIGHT, Screen.width, MESSAGE_HEIGHT), 
			        "So you got rid of the spiders? Splendid!");
		}
	}
}
