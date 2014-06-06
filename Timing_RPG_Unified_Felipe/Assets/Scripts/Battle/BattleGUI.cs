using UnityEngine;
using System.Collections;

public class BattleGUI : MonoBehaviour {
	public bool gameOver;
	public bool victory;
	public bool running;
	public bool failedRun;
	public bool successRun;
	public bool insufficientKP;
	private bool showingFailedRun;
	private bool showingInsufficientKP;

	private float MESSAGE_WIDTH = 100f, MESSAGE_HEIGHT = 70f;
	public GUIStyle MESSAGE_STYLE;

	//references.
	PlayerController pCon;

	// Use this for initialization
	void Start () {
		gameOver = false;
		victory = false;
		running = false;
		failedRun = false;
		successRun = false;
		insufficientKP = false;
		showingFailedRun = false;
		showingInsufficientKP = false;

		//Reference to the player controller.
		pCon = (PlayerController) (GameObject.FindGameObjectWithTag("BattleController").GetComponent<PlayerController>());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {
		if (gameOver) {
			GUI.Label(new Rect((Screen.width / 2f) - (MESSAGE_WIDTH / 2f), 
			                   (Screen.height / 2f) - (MESSAGE_HEIGHT / 2f), 
			                   MESSAGE_WIDTH, MESSAGE_HEIGHT), 
			          "You are dead", MESSAGE_STYLE);
		} else if (victory) {
			GUI.Label(new Rect((Screen.width / 2f) - (MESSAGE_WIDTH / 2f), 
			                   (Screen.height / 2f) - (MESSAGE_HEIGHT / 2f), 
			                   MESSAGE_WIDTH, MESSAGE_HEIGHT), 
			          "Victory is ours!", MESSAGE_STYLE);
		} else if (pCon.playerInput && !pCon.selectingTarget && !pCon.selectingSkill) {
			GUI.Label(new Rect((Screen.width / 2f) - (MESSAGE_WIDTH / 2f), 
			                   (Screen.height / 2f) - (MESSAGE_HEIGHT / 2f), 
			                   MESSAGE_WIDTH, MESSAGE_HEIGHT), 
			          "Choose your action\nA: Attack\nS: Skill\nR: Run", MESSAGE_STYLE);
		} else if (insufficientKP) {
			GUI.Label(new Rect((Screen.width / 2f) - (MESSAGE_WIDTH / 2f), 
			                   (Screen.height / 2f) - (MESSAGE_HEIGHT / 2f), 
			                   MESSAGE_WIDTH, MESSAGE_HEIGHT), 
			          "Not enough ki points!", MESSAGE_STYLE);
			//Shows message for a limited time only.
			if (!showingInsufficientKP) {
				showingInsufficientKP = true;
				StartCoroutine(Wait2 (1f));
			}
		} else if (pCon.selectingSkill) {
			GUI.Label(new Rect((Screen.width / 2f) - (MESSAGE_WIDTH / 2f), 
			                   (Screen.height / 2f) - (MESSAGE_HEIGHT / 2f), 
			                   MESSAGE_WIDTH, MESSAGE_HEIGHT), 
			          "Choose the skill to use\nS: Super Strike\nE: Energy Strike", MESSAGE_STYLE);
		} else if (running) {
			GUI.Label(new Rect((Screen.width / 2f) - (MESSAGE_WIDTH / 2f), 
			                   (Screen.height / 2f) - (MESSAGE_HEIGHT / 2f), 
			                   MESSAGE_WIDTH, MESSAGE_HEIGHT), 
			          "Run for the hills!", MESSAGE_STYLE);
		} else if (failedRun) {
			GUI.Label(new Rect((Screen.width / 2f) - (MESSAGE_WIDTH / 2f), 
			                   (Screen.height / 2f) - (MESSAGE_HEIGHT / 2f), 
			                   MESSAGE_WIDTH, MESSAGE_HEIGHT), 
			          "Can't get away!", MESSAGE_STYLE);
			//Shows message for a limited time only.
			/*if (!showingFailedRun) {
				showingFailedRun = true;
				StartCoroutine(Wait (1f));
			}*/
		} else if (successRun) {
			GUI.Label(new Rect((Screen.width / 2f) - (MESSAGE_WIDTH / 2f), 
			                   (Screen.height / 2f) - (MESSAGE_HEIGHT / 2f), 
			                   MESSAGE_WIDTH, MESSAGE_HEIGHT), 
			          "Got away safely!", MESSAGE_STYLE);
		}
	}

	private IEnumerator Wait(float seconds) {
		yield return new WaitForSeconds(seconds);
		failedRun = false;
		showingFailedRun = false;
	}

	private IEnumerator Wait2(float seconds) {
		yield return new WaitForSeconds(seconds);
		insufficientKP = false;
		showingInsufficientKP = false;
	}
}
