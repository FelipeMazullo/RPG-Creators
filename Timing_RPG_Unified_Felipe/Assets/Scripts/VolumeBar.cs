//------------------------------------------------------------------------------
// <auto-generated>
//     O código foi gerado por uma ferramenta.
//     Versão de Tempo de Execução:4.0.30319.18449
//
//     As alterações ao arquivo poderão causar comportamento incorreto e serão perdidas se
//     o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;

public class VolumeBar{
	
	public float progressBarHeight;
	public float left,top,width,height;
	public Texture barTexture;
	public float volume;
	
	public VolumeBar (float left, float top,float width, float height, Texture texture, float volume){
			this.left = left;
			this.top = top;
			this.width = width;
			this.height = height;
			barTexture = texture;
			this.volume = volume;
	}
	public void DoGUI(){
			GUI.DrawTexture (new Rect(left,top,width,height*volume),barTexture);
	}
}


