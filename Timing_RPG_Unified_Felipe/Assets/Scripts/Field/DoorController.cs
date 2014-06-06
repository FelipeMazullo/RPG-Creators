using UnityEngine;
using System.Collections;

public class DoorController : Interactable_Controller {
	private float MESSAGE_WIDTH = 100f, MESSAGE_HEIGHT = 70f;

	public override void Interact (GameObject interactObj) {
		if (interactObj == this.gameObject) {
			dialogueStage++;
			//Decide interacion: Can the door be opened?
			if (dialogueStage == 1) {
				state = State.Interacting;
				foreach (Item it in GameManager.Instance.inventory) {
					//Player has the appropriate key.
					if (it.name == "First Key") {
						dialogueStage = 4;
						//Subtract the key from the inventory.
						//maybe...
						return;
					}
				}
				//The player doesn't have it.
				dialogueStage = 2;
			//Interaction ended without door being opened.
			} else if (dialogueStage == 3) {
				state = State.Idle;
				pCon.state = PlayerOverworldController.State.Idle;
				dialogueStage = 0;
			//Finishes the current interaction and destroys the door's box collider,
			//making further interaction with the door impossible.
			} else if (dialogueStage == 5) {
				state = State.Idle;
				pCon.state = PlayerOverworldController.State.Idle;
				Destroy (GetComponent<BoxCollider2D>());
			}
		}
	}

	void OnGUI () {
		if (dialogueStage == 2) {
			GUI.Box(new Rect (0, Screen.height - MESSAGE_HEIGHT, Screen.width, MESSAGE_HEIGHT), 
			        "The door is locked! Where could the key be?");
		} else if (dialogueStage == 4) {
			GUI.Box(new Rect (0, Screen.height - MESSAGE_HEIGHT, Screen.width, MESSAGE_HEIGHT), 
			        "The key opened the door!");
		}
	}
}
