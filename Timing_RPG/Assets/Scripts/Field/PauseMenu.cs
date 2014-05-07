using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	PlayerOverworldController player;
	float left = Screen.width/2;
	float top = Screen.height/2;
	float width = 120;
	float height = 200;
	bool buttonMenu;
	bool menuON;
	bool mainSelected;//IF IT'S ON THE MAIN MENU
	bool optionSelected;//IF IT'S ON THE OPTION MENU
	GUIStyle font;
	public Texture cursor;
	int numberMenuOptions = 3; // CHANGE HERE HOW MANY MENU OPTIONS WE HAVE
	int cursorPosition;

	// Use this for initialization
	void Start () {
		buttonMenu = false;
		player = GetComponent<PlayerOverworldController>();
		font = new GUIStyle();
		font.fontSize = 14;
		cursorPosition = 1;
	}
	
	// Update is called once per frame
	void Update () {

		pause();
		cursorMenuMovement ();
		//menuSelection ();

	}


	public void menuSelection(){//CHECKS WHICH OPTION IS SELECTED IN THE MENU
		if(menuON&&Input.GetKeyDown (KeyCode.Return)){
			if(cursorPosition==2){
				optionSelected = true;
				mainSelected = false;

			}
		}
	}

	public void cursorMenuMovement(){
		//KEEPS TRACK OF THE CURSOR MOVEMENT IN THE MENU
		if (menuON&&Input.GetKeyDown (KeyCode.UpArrow)) {
			if(cursorPosition>1)
				cursorPosition--;
			else
				if(cursorPosition==1)
					cursorPosition = numberMenuOptions;
		}
		if (menuON&&Input.GetKeyDown(KeyCode.DownArrow)) {
			if(cursorPosition<numberMenuOptions)
				cursorPosition++;
			else
				if(cursorPosition==numberMenuOptions)
					cursorPosition = 1;
		}
	
	}

	public void pause(){
		//VERIFIES IF THE PAUSE BUTTON IS PRESSED, IF IT IS THEN IT PAUSES THE GAME
		if(Input.GetKeyDown(KeyCode.Escape)){
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
				menuON = false;
				
			}
		}
	
	}
	

	//SHOWS IN-GAME PAUSE MENU
	void OnGUI(){
		if(buttonMenu&&mainSelected){
			menuON = true;
			GUI.Box (new Rect(left, top, width, height),"");//DRAWS THE TEXT BOX
			GUI.Label(new Rect(left+(width/4), top, width, font.fontSize),"MAIN MENU",font);
			GUI.Label(new Rect(left, top+font.fontSize, width, font.fontSize),"RESUME GAME",font);//POSITION = 1
			GUI.Label(new Rect(left, top+2*font.fontSize, width, font.fontSize),"OPTIONS",font);//POSITION = 2
			GUI.Label(new Rect(left, top+3*font.fontSize, width, font.fontSize),"QUIT GAME",font);//POSITION = 3
			GUI.DrawTexture(new Rect(left-32,top+ cursorPosition*font.fontSize,32,32),cursor);//DRAWS THE CURSOR
		}
	}
}
