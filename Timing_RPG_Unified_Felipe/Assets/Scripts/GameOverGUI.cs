using UnityEngine;
using System.Collections;

public class GameOverGUI : MonoBehaviour {


	public Texture cursor;
	public Texture background;
	float left = Screen.width/2;
	float top = Screen.height/2;
	int fontsize = 14;
	float width = 600;
	float height = 400;
	Menu gameoverMenu;

	// Use this for initialization
	void Start () {
		gameoverMenu = new Menu ("",left,top,fontsize, width, height,cursor);
		gameoverMenu.AddMenu(1,"LOAD GAME");
		gameoverMenu.AddMenu(2,"QUIT GAME");
		gameoverMenu.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
		gameoverMenu.CursorMenuMovement();
		MenuSelection();
	}

	public void MenuSelection(){
		if (Input.GetKeyDown (KeyCode.Space)) {
			if(gameoverMenu.cursorPosition==1){//LOAD GAME

			}
			else
			if(gameoverMenu.cursorPosition==2){//QUIT GAME
				Application.Quit();
			}
		}
	}

	void OnGUI(){
		GUI.DrawTexture(new Rect(0,0,background.width,background.height),background);//DRAWS THE BACKGROUND IMAGE
		if(gameoverMenu.active){
			gameoverMenu.DoGUI();
		}
	}
}
