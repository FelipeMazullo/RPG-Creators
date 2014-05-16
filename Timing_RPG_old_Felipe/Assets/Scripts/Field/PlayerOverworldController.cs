using UnityEngine;
using System.Collections;


public enum Facing {
	Up,
	Down,
	Left,
	Right
}

public class PlayerOverworldController : MonoBehaviour {
	private const float INTERACTION_RADIUS = 0.5f;

	private float maxSpeed = 10f;
	private Facing side = Facing.Right;
	private Vector3 originalLocalScale;
	private GameObject currentlyInteracting;

	public enum State {
		Idle,
		Moving,
		Interacting,
		Paused
	}
	public State state;
	private GameObject itemObj;

	//References.
	private Animator anim;
	private GameObject interactionDetector, receivedItemLocation;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		currentlyInteracting = null;
		//Getting the interaction detector.
		Transform[] transfs = transform.GetComponentsInChildren<Transform>();
		foreach (Transform t in transfs) {
			if (t.gameObject.name == "InteractionDetector") {
				interactionDetector = t.gameObject;
			}
			if (t.gameObject.name == "ReceivedItemLocation") {
				receivedItemLocation = t.gameObject;
			}
		}
		state = State.Idle;
		originalLocalScale = transform.localScale;
	}

	void Update () {
		if ((state != State.Interacting)&&(state!=State.Paused)) {//MOVES CHARACTER
			MoveRotation ();
			if (Input.GetKeyDown(KeyCode.Space)) {
				TryInteract();
			}
		} else if (state == State.Interacting) {
			//Gets the next interaction until the state of the player
			//is changed (so that it can move again).
			if (Input.GetKeyDown (KeyCode.Space)) {
				TryInteract();
				/*Interactable_Controller inCon = currentlyInteracting.GetComponent<Interactable_Controller>();
				inCon.Interact();*/
			}
		}
	}

	//Checks if there is anything the player can interact with. 
	//If so, acts accordingly.
	/*private void TryInteract () {
		Collider2D col;
		Interactable_Controller inCon;

		col = Physics2D.OverlapCircle (interactionDetector.transform.position, INTERACTION_RADIUS);
		if (col != null) {
			//Interacts with NPCs.
			if (col.gameObject.tag == "NPC") {
				state = State.Interacting;
				Debug.Log ("Hey!");
				inCon = col.gameObject.GetComponent<Interactable_Controller>();
				currentlyInteracting = col.gameObject;
				inCon.Interact();
			//Interacting with items, such as chests.
			} else if (col.gameObject.tag == "Item") {
				Debug.Log ("Item here!");
			}
		}
	}*/
	private void TryInteract () {
		Collider2D col;
		
		col = Physics2D.OverlapCircle (interactionDetector.transform.position, INTERACTION_RADIUS);
		if (col != null) {
			//Interacts with NPCs.
			if (col.gameObject.tag == "NPC") {
				Debug.Log ("Hey!");
				state = State.Interacting;
				currentlyInteracting = col.gameObject;
				EventManager.Instance.Interact (currentlyInteracting);
			//Interacting with items, such as chests.
			} else if (col.gameObject.tag == "OverworldItem") {
				Debug.Log ("Item here!");
				state = State.Interacting;
				currentlyInteracting = col.gameObject;
				EventManager.Instance.Interact (currentlyInteracting);
			}
		}
	}

	private void MoveRotation () {
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		if (moveHorizontal == 0 && moveVertical == 0) {
			state = State.Idle;
		} else {
			state = State.Moving;
		}
		
		if (moveVertical > 0) {
			if (side != Facing.Up) {
				transform.localScale = originalLocalScale;
				transform.rotation = Quaternion.Euler (0, 0, 90f);
			}
			side = Facing.Up;
			//cacheTransform.rotation = Quaternion.Slerp (cacheTransform.rotation, Quaternion.LookRotation (target.position - cacheTransform.position), rotationSpeed * Time.deltaTime);
		} else if (moveVertical < 0) {
			if (side != Facing.Down) {
				transform.localScale = originalLocalScale;
				transform.rotation = Quaternion.Euler (0, 0, -90f);
			}
			side = Facing.Down;
			//transform.rotation = new Vector3 (0, 0, -90f);
		} else if (moveHorizontal > 0) {
			if (side != Facing.Right) {
				transform.localScale = originalLocalScale;
				transform.rotation = Quaternion.Euler (0, 0, 0);
			}
			side = Facing.Right;
			//transform.rotation.SetLookRotation (new Vector3(0, 0, 0));
			//transform.rotation = new Vector3 (0, 0, 0);
		} else if (moveHorizontal < 0) {
			if (side != Facing.Left) {
				transform.rotation = Quaternion.Euler (0, 0, 0);
				transform.localScale = originalLocalScale;
				Flip();
			}
			side = Facing.Left;
			//transform.rotation.SetLookRotation (new Vector3(0, 0, 0));
			//transform.rotation = new Vector3 (0, 0, 0);
		}
	}

	//Responsible for physical actions of the player's "rigidbody", such as changing velocity.
	void FixedUpdate () {
		//MOVES CHARACTER
		if ((state != State.Interacting)&&(state!=State.Paused)) {
			float moveHorizontal = Input.GetAxis("Horizontal");
			float moveVertical = Input.GetAxis("Vertical");

			rigidbody2D.velocity = new Vector2 (moveHorizontal * maxSpeed, moveVertical * maxSpeed);

			anim.SetFloat ("Speed", rigidbody2D.velocity.magnitude);
		}
	}

	//Flips the sprite to the left.
	private void Flip () {
		Vector3 scaleAux;

		scaleAux = transform.localScale;
		scaleAux.x = scaleAux.x * (-1f);
		transform.localScale = scaleAux;
	}

	public GameObject ShowItem (GameObject item) {
		/*//Gets the sprite of the designated item.
		Sprite[] textures = Resources.LoadAll<Sprite> ("Sprites");
		string[] names = new string[textures.Length];

		for(int i = 0; i < names.Length; i++) {
			names[i] = textures[i].name;
		}
		
		Sprite itemSprite = textures[System.Array.IndexOf(names, item.texture)];

		//SpriteRenderer spriteR = new SpriteRenderer();
		SpriteRenderer spriteR = (SpriteRenderer) itemObj.AddComponent ("SpriteRenderer");
		spriteR.sprite = itemSprite;*/
		Vector3 itemPos = receivedItemLocation.transform.position;
		/*Vector3 itemPos = transform.position + new Vector3 (transform.position.x, 
		                                                    transform.position.y - ((GetComponent<SpriteRenderer>().sprite.rect.height) / 2.0f),
		                                                    transform.position.z);*/
		//GameObject inst = (GameObject) Instantiate (itemObj, itemPos, Quaternion.identity);
		return (GameObject) Instantiate (item, itemPos, Quaternion.identity);
	}
	public void setState(string s){
		if(s.Equals("Paused"))
			state = State.Paused;
		if(s.Equals("Idle"))
			state = State.Idle;
		if(s.Equals("Moving"))
			state = State.Moving;
		if(s.Equals("Interacting"))
			state = State.Interacting;
	}
	public string getState(){
		if(state == State.Paused)
			return "Paused";
		if(state == State.Idle)
			return "Idle";
		if(state == State.Moving)
			return "Moving";
		if(state == State.Interacting)
			return "Interacting";
		return "";
	}

}



