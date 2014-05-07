using UnityEngine;
using System.Collections;

public class PlayerCollisionDetection : MonoBehaviour {
	private float MESSAGE_WIDTH = 100f, MESSAGE_HEIGHT = 70f;
	public GUIStyle MESSAGE_STYLE;
	public bool startBattle;
	private BoxCollider2D playerCol;

	//Enter battle with an enemy in the field.
	void OnCollisionEnter2D (Collision2D col) {
		if (!startBattle) {
			if (col.gameObject.tag == "Enemy") {
				GameManager.Instance.enemyID.Add (col.gameObject.GetComponent<FieldEnemy>().ID);
				startBattle = true;
				StartCoroutine (ChangeToBattle());
			} 
		}
	}

	IEnumerator ChangeToBattle() {
		yield return new WaitForSeconds(2);
		GameManager.Instance.playerPos = transform.position;
		GameManager.Instance.lastScene = Application.loadedLevelName;
		Application.LoadLevel("Battle");
	}

	void OnGUI () {
		if (startBattle == true) {
			GUI.Label(new Rect((Screen.width / 2f) - (MESSAGE_WIDTH / 2f), 
			                      (Screen.height / 2f) - (MESSAGE_HEIGHT / 2f), 
			                      MESSAGE_WIDTH, MESSAGE_HEIGHT), 
			             		  "Battle Start!", MESSAGE_STYLE);
		}
	}

	// Use this for initialization
	void Start () {
		startBattle = false;
	}
	
	// Update is called once per frame
	void Update () {

	}
}
