using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PauseMenu : MonoBehaviour {

	PlayerOverworldController player;
	float left = Screen.width/2-300;
	float top = Screen.height/2-300;
	float width = 600;
	float height = 400;
	bool buttonMenu;//Tells me if the menu button was pressed
	bool menuON;
	bool mainSelected;//IF IT'S ON THE MAIN MENU
	bool optionsSelected;//IF IT'S ON THE OPTION MENU
	GUIStyle font;
	public Texture cursor;
	int numberMenuOptions;
	int cursorPosition;
	int menuLayer;//1 = Main menu, 2 = Inside a category, 3 = Inside a subcategory, and so on
	Dictionary <int,string> menus;//KEEPS ALL THE MENU CATEGORIES

	// Use this for initialization
	void Start () {
		buttonMenu = false;
		player = GetComponent<PlayerOverworldController>();
		font = new GUIStyle();
		font.fontSize = 14;
		cursorPosition = 1;
		menus = new Dictionary<int,string> ();
		menus.Add (1,"RESUME GAME");
		menus.Add (2,"STATUS");
		menus.Add (3,"OPTIONS");
		menus.Add (4,"QUIT GAME");
		numberMenuOptions = menus.Count;
		menuLayer = 1;
	}
	
	// Update is called once per frame
	void Update () {

		pause();
		cursorMenuMovement ();
		menuSelection ();

	}


	public void menuSelection(){//CHECKS WHICH OPTION IS SELECTED IN THE MENU
		if(mainSelected&&Input.GetKeyDown (KeyCode.Space)){
			if(cursorPosition==3){
				optionsSelected = true;
				mainSelected = false;
				menuLayer++;
				cursorPosition=1;
			}
		}
		if (menuLayer == 2 && Input.GetKeyDown (KeyCode.LeftShift)) {//RETURNING FROM SECOND MENU LAYER
			if(optionsSelected){
				menuLayer--;
				optionsSelected = false;
				mainSelected = true;
				cursorPosition=3;
			}
		}
	}

	public void cursorMenuMovement(){
		//KEEPS TRACK OF THE CURSOR MOVEMENT IN THE MAIN MENU
		if (mainSelected&&Input.GetKeyDown (KeyCode.UpArrow)) {
			if(cursorPosition>1)
				cursorPosition--;
			else
				if(cursorPosition==1)
					cursorPosition = numberMenuOptions;
		}
		if (mainSelected&&Input.GetKeyDown(KeyCode.DownArrow)) {
			if(cursorPosition<numberMenuOptions)
				cursorPosition++;
			else
				if(cursorPosition==numberMenuOptions)
					cursorPosition = 1;
		}
	
	}

	public void pause(){
		//VERIFIES IF THE PAUSE BUTTON IS PRESSED, IF IT IS THEN IT PAUSES THE GAME
		if(Input.GetKeyDown(KeyCode.Return)){
			if(buttonMenu == false){
				buttonMenu = true;
				mainSelected = true;
				Time.timeScale = 0.0f;
				player.setState("Paused");
				
				
			}
			else{//UNPAUSES THE GAME AND RETURNS TO IDLE STATE
				buttonMenu = false;
				mainSelected = false;
				player.setState("Idle");
				Time.timeScale = 1.0f;

				
			}
		}
	
	}
	

	//SHOWS IN-GAME PAUSE MENU
	void OnGUI(){
		if(buttonMenu && mainSelected){//MAIN MENU----------------------
			GUI.Box (new Rect(left, top, width, height),"");//DRAWS THE MAIN MENU BOX
			GUI.Label(new Rect(left+(width/4), top, width, font.fontSize),"MAIN MENU",font);
			foreach(KeyValuePair<int,string>menu in menus){
				GUI.Button(new Rect(left, top + menu.Key*font.fontSize, width, font.fontSize),menu.Value,font);

			}
			GUI.DrawTexture(new Rect(left-32,top+ cursorPosition*font.fontSize,32,32),cursor);//DRAWS THE CURSOR
		}
		if (buttonMenu && optionsSelected) {//OPTIONS MENU----------------------
			GUI.Box (new Rect(left, top, width, height),"");//DRAWS THE MAIN MENU BOX
			GUI.Label(new Rect(left+(width/4), top, width, font.fontSize),"OPTIONS MENU",font);
		}
	}
}
