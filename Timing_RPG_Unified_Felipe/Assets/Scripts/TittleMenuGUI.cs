using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TittleMenuGUI : MonoBehaviour {

	public enum SelectedMenu{
		PressStart,
		MainMenu,
			NewGame,
			LoadGame,
			Options,
				 Volume,
					MusicVolume,
					SFXVolume,
				 ScreenMode,
				 PlayerControls,
			ExitGame
	}
	SelectedMenu select;
	float left = Screen.width/2-300;
	float top = Screen.height/2-100;
	public Texture cursor;
	public Texture background;
	public Texture volumeBarTexture;
	int fontsize = 14;
	float width = 600;
	float height = 400;
	Menu menu1,mainMenu,optionsMenu,loadgameMenu,exitgameMenu;//main menu--------
	Menu volumeMenu,screenmodeMenu;//options menu-----
	VolumeBar musicVolumeBar,sfxVolumeBar;

	// Use this for initialization
	void Start () {
		menu1 = new Menu ("PRESS START",left,top,fontsize, width, height,cursor);
		mainMenu = new Menu ("MAIN MENU",left,top,fontsize, width, height,cursor);
		mainMenu.AddMenu(1,"NEW GAME");
		mainMenu.AddMenu(2,"LOAD GAME");
		mainMenu.AddMenu(3,"OPTIONS");
		mainMenu.AddMenu(4,"EXIT GAME");
		optionsMenu = new Menu ("OPTIONS MENU", left, top, fontsize, width, height,cursor);
		optionsMenu.AddMenu(1,"VOLUME");
		optionsMenu.AddMenu(2,"SCREEN MODE");
		optionsMenu.AddMenu(3,"PLAYER CONTROLS");
		volumeMenu = new Menu ("VOLUME MENU", left, top, fontsize, width, height,cursor);
		volumeMenu.AddMenu (1,"MUSIC VOLUME");
		volumeMenu.AddMenu (2,"SFX VOLUME");
		loadgameMenu = new Menu("LOAD GAME", left, top, fontsize, width, height,cursor);
		exitgameMenu = new Menu ("EXIT GAME", left, top, fontsize, width, height, cursor);
		screenmodeMenu = new Menu ("SCREEN MODE", left, top, fontsize, width, height, cursor);
		screenmodeMenu.AddMenu (1, "FULL SCREEN");
		screenmodeMenu.AddMenu (2, "WINDOW MODE");
		select = SelectedMenu.PressStart;
		musicVolumeBar = new VolumeBar (left+width/4,top+32,30,200,volumeBarTexture,GameManager.Instance.MusicVolume);
		sfxVolumeBar = new VolumeBar (left+width/4+32,top+32,30,200,volumeBarTexture,GameManager.Instance.SfxVolume);
	
    }
	
	// Update is called once per frame
	void Update () {
		mainMenu.CursorMenuMovement ();
		optionsMenu.CursorMenuMovement ();
		volumeMenu.CursorMenuMovement ();
		screenmodeMenu.CursorMenuMovement ();
		loadgameMenu.CursorMenuMovement ();
		exitgameMenu.CursorMenuMovement ();
		MenuSelection ();
	}

	public void MenuSelection(){//CHECKS WHICH CATEGORY IS SELECTED IN THE MENU
		switch (select)
		{
		case SelectedMenu.PressStart:
			menu1.SetActive (true);
			if (Input.anyKeyDown) {
				select = SelectedMenu.MainMenu;
				menu1.SetActive (false);
			}
			break;
		case SelectedMenu.MainMenu:
			mainMenu.SetActive (true);
			if (Input.GetKeyDown (KeyCode.LeftShift)) {
				select = SelectedMenu.PressStart;
				mainMenu.SetActive(false);
			}
			else
			if(Input.GetKeyDown (KeyCode.Space)){
				if(mainMenu.cursorPosition==3){
					select = SelectedMenu.Options;
					mainMenu.SetActive(false);
					optionsMenu.cursorPosition=1;
				}
				else
				if(mainMenu.cursorPosition==1){
					select = SelectedMenu.NewGame;
					mainMenu.SetActive(false);
				}
				else
				if(mainMenu.cursorPosition==4){
					select = SelectedMenu.ExitGame;
					mainMenu.SetActive(false);
				}
			}
			break;
		case SelectedMenu.NewGame:
			Application.LoadLevel("TestChamberWest");
			break;
		case SelectedMenu.LoadGame:
			loadgameMenu.SetActive(true);
			if (Input.GetKeyDown (KeyCode.LeftShift)) {
				select = SelectedMenu.MainMenu;
				loadgameMenu.SetActive(false);	
				mainMenu.cursorPosition=2;
			}
			break;
		case SelectedMenu.Options:
			optionsMenu.SetActive(true);
			if (Input.GetKeyDown (KeyCode.LeftShift)) {
				select = SelectedMenu.MainMenu;
				optionsMenu.SetActive(false);	
				mainMenu.cursorPosition=3;
			}
			else
			if(Input.GetKeyDown (KeyCode.Space)){
				if(optionsMenu.cursorPosition==1){
					select = SelectedMenu.Volume;
					optionsMenu.SetActive(false);
				}
				else
				if(optionsMenu.cursorPosition==2){
					select = SelectedMenu.ScreenMode;
					optionsMenu.SetActive(false);
				}
				else
				if(optionsMenu.cursorPosition==3){
					select = SelectedMenu.PlayerControls;
					optionsMenu.SetActive(false);
				}
			}
			break;
		
		case SelectedMenu.Volume:
			volumeMenu.SetActive(true);
			if (Input.GetKeyDown (KeyCode.LeftShift)) {
				select = SelectedMenu.Options;
				volumeMenu.SetActive(false);
				optionsMenu.cursorPosition=1;
			}
			else
			if (Input.GetKeyDown (KeyCode.Space)) {
				volumeMenu.move = false;
				if(volumeMenu.cursorPosition==1){
					select = SelectedMenu.MusicVolume;
				}
				else
				if(volumeMenu.cursorPosition==2){
					select = SelectedMenu.SFXVolume;
				}

			}
			break;
		case SelectedMenu.MusicVolume:
			if (Input.GetKeyDown (KeyCode.LeftArrow)){
				if(GameManager.Instance.MusicVolume>0f)
					GameManager.Instance.MusicVolume-=0.1f;
				Debug.Log("DIMINUI MUSIC VOLUME"+GameManager.Instance.MusicVolume);
				
			}
			if (Input.GetKeyDown (KeyCode.RightArrow)){
				if(GameManager.Instance.MusicVolume<1f)
					GameManager.Instance.MusicVolume+=0.1f;;
				Debug.Log("AUMENTA MUSIC VOLUME"+GameManager.Instance.MusicVolume);
			}
			if (Input.GetKeyDown (KeyCode.LeftShift)) {
				volumeMenu.move = true;
				select = SelectedMenu.Volume;
			}
			break;
		case SelectedMenu.SFXVolume:
			if (Input.GetKeyDown (KeyCode.LeftArrow)){
				if(GameManager.Instance.SfxVolume>0f)
					GameManager.Instance.SfxVolume-=0.1f;
				Debug.Log("DIMINUI SFX VOLUME"+GameManager.Instance.SfxVolume);
				
			}
			if (Input.GetKeyDown (KeyCode.RightArrow)){
				if(GameManager.Instance.SfxVolume<1f)
				GameManager.Instance.SfxVolume+=0.1f;
				Debug.Log("AUMENTA SFX VOLUME"+GameManager.Instance.SfxVolume);
			}
			if (Input.GetKeyDown (KeyCode.LeftShift)) {
				volumeMenu.move = true;
				select = SelectedMenu.Volume;
			}
			break;
		case SelectedMenu.ScreenMode:
			screenmodeMenu.SetActive(true);
			if (Input.GetKeyDown (KeyCode.LeftShift)) {
				select = SelectedMenu.Options;
				screenmodeMenu.SetActive(false);
				optionsMenu.cursorPosition=2;
			}
			else
			if (Input.GetKeyDown (KeyCode.Space)) {
				if(screenmodeMenu.cursorPosition==1){
					Screen.fullScreen = true;
				}
				else
				if(screenmodeMenu.cursorPosition==2){
					Screen.fullScreen = false;
				}
				
			}
			break;
		case SelectedMenu.PlayerControls:
			if (Input.GetKeyDown (KeyCode.LeftShift)) {
				select = SelectedMenu.Options;
				optionsMenu.cursorPosition=3;
			}
			break;
		case SelectedMenu.ExitGame:
			Application.Quit();
			break;
		default:
			break;
		}

	}

	//DRAWS MENU
	void OnGUI(){
		GUI.DrawTexture(new Rect(0,0,background.width,background.height),background);//DRAWS THE BACKGROUND IMAGE

		if(mainMenu.active){//MAIN MENU----------------------
			mainMenu.DoGUI();

		}
		else
		if (optionsMenu.active) {//OPTIONS MENU----------------------
			optionsMenu.DoGUI();
		}
		else
		if (volumeMenu.active) {//OPTIONS>VOLUME MENU----------------------
			volumeMenu.DoGUI();
			musicVolumeBar.volume = GameManager.Instance.MusicVolume;
			sfxVolumeBar.volume = GameManager.Instance.SfxVolume;
			musicVolumeBar.DoGUI();
			sfxVolumeBar.DoGUI();
		}
		else
		if(screenmodeMenu.active){//OPTIONS>SCREEN MODE MENU------------
			screenmodeMenu.DoGUI();
		}
		else
		if(menu1.active){//TITLE SCREEN-----------------------
			GUI.Label(new Rect(left+(width/4)-30, top+50, width, menu1.font.fontSize),menu1.title,menu1.font);
		}
	}

}
