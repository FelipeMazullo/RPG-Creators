using UnityEngine;
using System.Collections;

public class Interactable_Controller : MonoBehaviour {
	public enum State {
		Idle,
		Interacting
	}
	public State state;
	public int dialogueStage;
	public bool interactionChoice;
	protected int cursorPosition;
	protected int numberChoices;
	public Texture cursor;

	//References.
	protected PlayerOverworldController pCon;

	void OnEnable () {
		EventManager.onInteract += Interact;
	}

	void OnDisable () {
		EventManager.onInteract -= Interact;
	}

	// Use this for initialization
	public virtual void Start () {
		state = State.Idle;
		dialogueStage = 0;
		interactionChoice = false;
		cursorPosition = 1;
		pCon = (PlayerOverworldController) (GameObject.FindGameObjectWithTag ("Player")).GetComponent<PlayerOverworldController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (interactionChoice) {
			CursorMovement();
		}
	}

	private void WalkAround() {

	}

	//Interaction chain will depend on the specific NPC.
	public virtual void Interact (GameObject obj) {
		Debug.LogWarning ("\"Interact\" called from a base class interactable controller!");
	}

	//For choosing options when interacting.
	public void CursorMovement(){
		//KEEPS TRACK OF THE CURSOR MOVEMENT IN THE MAIN MENU
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			if(cursorPosition>1)
				cursorPosition--;
			else
				if(cursorPosition==1)
					cursorPosition = numberChoices;
		}
		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			if(cursorPosition<numberChoices)
				cursorPosition++;
			else
				if(cursorPosition==numberChoices)
					cursorPosition = 1;
		}
		
	}

	public virtual void ChoiceSelection () {
		Debug.LogWarning ("\"choiceSelection\" called from a base class interactable controller!");
	}
}
