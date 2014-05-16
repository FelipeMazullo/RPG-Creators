using UnityEngine;
using System.Collections;

public class PlayerCollisionDetection : MonoBehaviour {
	private float MESSAGE_WIDTH = 100f, MESSAGE_HEIGHT = 70f;
	public GUIStyle MESSAGE_STYLE;
	public bool startBattle;

	private Animator battleTransitionAnimator;

	// Use this for initialization
	void Start () {
		startBattle = false;
		battleTransitionAnimator = (Animator) GameObject.Find ("BattleTransition").GetComponent<Animator>();
	}

	//Enter battle with an enemy in the field.
	void OnCollisionEnter2D (Collision2D col) {
		if (!startBattle) {
			if (col.gameObject.tag == "Enemy") {
				GameManager.Instance.enemyID.Add (col.gameObject.GetComponent<FieldEnemy>().ID);
				//player.state = paused;
				//Time.timeScale = 0f;
				startBattle = true;
				//Start battle transition: The transition takes place
				//when the animation is in fact finished, 
				battleTransitionAnimator.SetBool ("Active", true);
				//StartCoroutine (ChangeToBattle());
			} 
		}
	}

	public void ChangeToBattle() {
		//yield return new WaitForSeconds(2);
		GameManager.Instance.playerPos = transform.position;
		GameManager.Instance.lastScene = Application.loadedLevelName;
		Application.LoadLevel("Battle");
		//yield return null;
	}

	void OnGUI () {
		if (startBattle == true) {
			GUI.Label(new Rect((Screen.width / 2f) - (MESSAGE_WIDTH / 2f), 
			                      (Screen.height / 2f) - (MESSAGE_HEIGHT / 2f), 
			                      MESSAGE_WIDTH, MESSAGE_HEIGHT), 
			             		  "Battle Start!", MESSAGE_STYLE);
		}
	}



}
