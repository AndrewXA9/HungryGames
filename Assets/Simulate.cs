﻿using UnityEngine;
using System.Collections;

public class Simulate : MonoBehaviour {
	
	private int height = 4;
	private int width = 6;
	
	private int slices = 8;
	
	private float padding = 0.002f;
	
	public Texture2D X;
	
	public GUISkin skin;
	
	void Go() {
		
	}
	
	void Update(){
		if(Input.GetKeyDown(KeyCode.Space)){
			Manager.manager.contestants[Random.Range(0,24)].alive = false;
			
		}
	}
	
	void OnGUI() {
		
		GUI.skin.label.alignment = TextAnchor.MiddleCenter;
		
		GUI.skin = skin;
		
		float bWidth = ((Screen.width/2f)/(float)width);
		float bHeight = (Screen.height/(float)height);
		
		float padX = Screen.width*padding;
		float padY = Screen.width*padding;
		
		GUI.skin.label.fontSize = GUI.skin.textField.fontSize = GUI.skin.textArea.fontSize = (int)((bHeight/slices)/2);
		GUI.skin.button.fontSize = (int)((bHeight/slices)/2);
		
		int numba = 0;
		
		for(int i=0;i<height;i++){
			for(int j=0;j<width;j++){
				
				Contestant cont = Manager.manager.contestants[numba];
				
				Rect bounds = new Rect((bWidth*(float)j),(bHeight*(float)i),bWidth-(padX),bHeight-(padY*2f));
				
				GUI.Box(bounds,"");
				
				//District
				GUI.Label(new Rect(bounds.x,bounds.y,bounds.width/4f,bounds.height/slices),cont.district.ToString()+":");
				
				//Name
				Utility.Fit(cont.name,(bounds.width/4f)*3,GUI.skin.label);
				GUI.Label(new Rect(bounds.x+(bounds.width/4f),bounds.y,(bounds.width/4f)*3f,bounds.height/slices),cont.name);
				GUI.skin.label.fontSize = (int)((bHeight/slices)/2);
				
				//Pic
				GUI.DrawTexture(new Rect(bounds.x,bounds.y+(bounds.height/slices),bounds.width,bounds.height/slices*3f),cont.image,ScaleMode.ScaleToFit);
				if(!cont.alive){
					GUI.DrawTexture(new Rect(bounds.x,bounds.y+(bounds.height/slices),bounds.width,bounds.height/slices*3f),X,ScaleMode.ScaleToFit);
				}
				
				
				numba++;
			}
		}
		
		
	}
	
	
}
