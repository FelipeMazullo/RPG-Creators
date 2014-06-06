using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System;
using System.Collections.Generic;

public class ScientistController : Interactable_Controller {
	private float MESSAGE_WIDTH = 100f, MESSAGE_HEIGHT = 70f;
	public GameObject itemToGive;
	public GameObject chestToActivate;
	private Item itemScript;
	private GameObject itemInstance;
	private GameObject chestInstance;
	string d4,d5;
	string[] d1;
	AudioSource itemSFX;
	public Dictionary <string,string> dialogues;//CONTAINS ALL THE DIALOGUES

	// Use this for initialization
	public override void Start () {
		//Base class "start" function is also executed.
		base.Start();
		itemScript = (Item) itemToGive.GetComponent<Item>();
		//New stage of dialogue when the quest in completed.
		if (GameManager.Instance.keys.TestChamberEastClear) {
			dialogueStage = 9;
		}
		dialogues = new Dictionary<string,string> ();
		DialoguesUpdate();//GETS DIALOGS FROM THE .XML FILE
		d1 = GetDialogues("1").Split('|');
		d4 = GetDialogues ("2");
		d5 = GetDialogues ("3");
		itemSFX = this.GetComponent<AudioSource> ();
		itemSFX.volume = GameManager.Instance.SfxVolume;


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
				if (dialogueStage == 2 || dialogueStage == 6) {
					state = State.Idle;
					pCon.state = PlayerOverworldController.State.Idle;
					if (dialogueStage == 6) {
						Destroy (itemInstance);
					}
				}
				//Gives an item to the player.
				if (dialogueStage == 5) {
					GameManager.Instance.inventory.Add (itemScript);
					itemInstance = pCon.ShowItem (itemToGive);
					itemSFX.Play ();
				}
			} else {
				//Loops indefinitely in the final piece of dialogue 
				//(spider quest still incomplete).
				if (dialogueStage == 8) {
					state = State.Idle;
					pCon.state = PlayerOverworldController.State.Idle;
					dialogueStage = 6;
				}
				//Makes a chest appear.
				if (dialogueStage == 10) {
					chestToActivate.GetComponent<SpriteRenderer>().enabled = true;
					chestToActivate.GetComponent<BoxCollider2D>().enabled = true;
					GameManager.Instance.keys.TestChamberWestChestAppears = true;
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
			        d1[0]/*"The day looks beautiful today, doesn't it?"*/);
		} else if (dialogueStage == 3) {
			GUI.Box(new Rect (0, Screen.height - MESSAGE_HEIGHT, Screen.width, MESSAGE_HEIGHT), 
			        d1[1]/* "Uh? So you wanted to know where you are supposed to go?"*/);
		} else if (dialogueStage == 4) {
			GUI.Box(new Rect (0, Screen.height - MESSAGE_HEIGHT, Screen.width, MESSAGE_HEIGHT), 
			        d1[2]/*"Take this key and go on ahead to the next room. There are lots of spiders to get rid of!"*/);
		} else if (dialogueStage == 5) {
			GUI.Box(new Rect (0, Screen.height - MESSAGE_HEIGHT, Screen.width, MESSAGE_HEIGHT), 
			        itemScript.name + "\n" + itemScript.description);
		} else if (dialogueStage == 7) {
			GUI.Box(new Rect (0, Screen.height - MESSAGE_HEIGHT, Screen.width, MESSAGE_HEIGHT), 
			        d4/*"What are you waiting for? Good luck with the spiders!"*/);
		} else if (dialogueStage == 10) {
			GUI.Box(new Rect (0, Screen.height - MESSAGE_HEIGHT, Screen.width, MESSAGE_HEIGHT), 
			        d5/*"So you got rid of the spiders? Splendid!"*/);
		}
	}

	public void DialoguesUpdate(){
		
		XmlReader reader = XmlReader.Create("C:\\Users\\Public\\Documents\\Unity Projects\\Timing_RPG_Unified\\Assets\\Resources\\Dialogs.xml");
		XmlDocument xml = new XmlDocument();
		xml.Load(reader);
		XmlNodeList dialogueList = xml.GetElementsByTagName("Dialogue"); // array of the dialog nodes.
		string number = "";
		foreach (XmlNode dialogueInfo in dialogueList) {
			XmlNodeList dialogueContent = dialogueInfo.ChildNodes;
			foreach (XmlNode dialogueItens in dialogueContent) { // dialog item nodes.
				
				
				if (dialogueItens.Name == "Number") {
					number = dialogueItens.InnerText;
					if(!dialogues.ContainsKey(number))
						dialogues.Add(number,"");
					//Debug.Log(number);
				}
				if (dialogueItens.Name == "Text") {
					dialogues[number]=dialogueItens.InnerText;
					//Debug.Log(dialogues[number]);
				}
			}
			
		}
		
		reader.Close ();
		
	}
	public string GetDialogues(string number){
		string dialogue = "";
		foreach (string key in dialogues.Keys) {
			if(number==key)
				dialogue = dialogues[key];
		}
		return dialogue;
	}
	

}
