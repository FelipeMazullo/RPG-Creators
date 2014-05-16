using UnityEngine;
using System.Collections;

public class ChestController : Interactable_Controller {
	public ChestState cState;
	public int ID;
	public Sprite openSprite, closedSprite;
	public GameObject containedItem;
	private Item itemScript;
	private GameObject itemInstance;
	private SpriteRenderer spriteR;

	//temp.
	private float MESSAGE_WIDTH = 100f, MESSAGE_HEIGHT = 70f;

	public override void Start () {
		//Base class "start" function is also executed.
		base.Start();
		itemScript = (Item) containedItem.GetComponent<Item>();
		spriteR = GetComponent<SpriteRenderer>();
	}

	public override void Interact (GameObject interactObj) {
		if (interactObj == this.gameObject) {
			state = State.Interacting;
			dialogueStage++;

			//Closed: Interaction possible.
			if (cState == ChestState.Closed) {
				if (dialogueStage == 1) {
					GameManager.Instance.inventory.Add (itemScript);
					itemInstance = pCon.ShowItem (containedItem);
					spriteR.sprite = openSprite;
					GameManager.Instance.openChests.Add (ID);
				} else if (dialogueStage == 2) {
					pCon.state = PlayerOverworldController.State.Idle;
					Destroy (itemInstance);
					dialogueStage = 0;
					cState = ChestState.Open;
				}
			//Open: No interaction.
			} else {
				if (dialogueStage == 2) {
					pCon.state = PlayerOverworldController.State.Idle;
					dialogueStage = 0;
				}
			}
		}
	}

	void OnGUI () {
		if (dialogueStage == 1 && cState == ChestState.Closed) {
			GUI.Box(new Rect (0, Screen.height - MESSAGE_HEIGHT, Screen.width, MESSAGE_HEIGHT), 
			        itemScript.name + "\n" + itemScript.description);
		} else if (dialogueStage == 1 && cState == ChestState.Open) {
			GUI.Box(new Rect (0, Screen.height - MESSAGE_HEIGHT, Screen.width, MESSAGE_HEIGHT), 
			        "Nothing more here...");
		}
	}
}

public enum ChestState {
	Closed,
	Open
}
