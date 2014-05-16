using UnityEngine;
using System.Collections;

public class Interactable_Controller : MonoBehaviour {
	public enum State {
		Idle,
		Interacting
	}
	public State state;
	public int dialogueStage;

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
		pCon = (PlayerOverworldController) (GameObject.FindGameObjectWithTag ("Player")).GetComponent<PlayerOverworldController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void WalkAround() {

	}

	//Interaction chain will depend on the specific NPC.
	public virtual void Interact (GameObject obj) {
		Debug.LogWarning ("\"Interact\" called from a base class NPC controller!");
	}
}
