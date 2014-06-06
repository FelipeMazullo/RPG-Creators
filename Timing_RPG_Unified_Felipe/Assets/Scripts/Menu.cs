using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Menu {
	public string title;
	float left;
	float top;
	public float width;
	public float height;
	public GUIStyle font;
	public int numberMenuOptions;
	public Dictionary <int,string> menus;//KEEPS ALL THE MENU CATEGORIES
	public Texture cursor;
	public int cursorPosition;
	public bool active,move;

	public Menu ( string title,float left, float top,int fontsize, float width, float height, Texture cursor)
	{
		this.move = true;
		this.title = title;
		this.cursor = cursor;
		this.left = left;
		this.top = top;
		this.width = width;
		this.height = height;
		menus = new Dictionary<int,string> ();
		font = new GUIStyle();
		font.fontSize = fontsize;
		cursorPosition = 1;
		this.active = false;
		numberMenuOptions = 0;
	}
	public void AddMenu(int pos, string name){
		menus.Add(pos, name);
		numberMenuOptions = menus.Count;

	}

	public void CursorMenuMovement(){
		if(active&&move){
			//KEEPS TRACK OF THE CURSOR MOVEMENT IN THE MENU
			if (Input.GetKeyDown (KeyCode.UpArrow)) {
				if(cursorPosition>1)
					cursorPosition--;
				else
					if(cursorPosition==1)
						cursorPosition = numberMenuOptions;
			}
			if (Input.GetKeyDown(KeyCode.DownArrow)) {
				if(cursorPosition<numberMenuOptions)
					cursorPosition++;
				else
					if(cursorPosition==numberMenuOptions)
						cursorPosition = 1;
			}
		}
	}


	public void SetActive(bool active){
		this.active = active;

	}

	public void DoGUI(){//DRAWS THE MENU

			GUI.Label (new Rect (left + (width / 4), top, width, this.font.fontSize), this.title, this.font);
			foreach (KeyValuePair<int,string>menu in this.menus) {
				GUI.Button (new Rect (left, top + menu.Key * this.font.fontSize, width, this.font.fontSize), menu.Value, this.font);
			
		    }
			GUI.DrawTexture (new Rect (left - 32, top + cursorPosition * this.font.fontSize, 32, 32), cursor);//DRAWS THE CURSOR
		
	}



}
