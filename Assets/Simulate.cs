using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Simulate : MonoBehaviour {
	
	private int height = 4;
	private int width = 6;
	
	private int slices = 8;
	
	private float padding = 0.002f;
	
	public Texture2D X;
	
	public GUISkin skin;
	
	private string output = "";
	
	public Vector2 scrollPosition;
	
	private int displayMode = 0;
	
	void OnEnable() {
		
//		for(int i=0;i<10;i++){
//			output+=char.ConvertFromUtf32(Random.Range(65,65+24))+"\n";
//		}
		
	}
	
	void Update(){
		if(Input.GetKeyDown(KeyCode.Space)){
			//Manager.manager.contestants[Random.Range(0,24)].weapons.Add(Manager.manager.weapons[Random.Range(0,Manager.manager.weapons.Count)]);
			foreach(Contestant i in Manager.manager.contestants){
				i.weapons.Add(Manager.manager.weapons[Random.Range(0,Manager.manager.weapons.Count)]);
			}
			
		}
		//output+=char.ConvertFromUtf32(Random.Range(65,65+24))+"\n";
	}
	
	//private Vector2 scroll;
	
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
				
				Rect bounds = new Rect((bWidth*(float)j)+padX,(bHeight*(float)i)+padY,bWidth-(padX*2f),bHeight-(padY*2f));
				
				GUI.Box(bounds,"");
				
				GUI.skin.label.alignment = TextAnchor.MiddleCenter;
				
				//District
				GUI.Label(new Rect(bounds.x,bounds.y,bounds.width/4f,bounds.height/slices),cont.district.ToString()+":");
				
				//Name
				Utility.Fit(cont.name,(bounds.width/4f)*3,GUI.skin.label);
				GUI.Label(new Rect(bounds.x+(bounds.width/4f),bounds.y,(bounds.width/4f)*3f,bounds.height/slices),cont.name);
				GUI.skin.label.fontSize = (int)((bHeight/slices)/2);
				
				//Pic
				GUI.Box(new Rect(bounds.x,bounds.y+(bounds.height/slices),bounds.width,bounds.height/slices*3f),GUIContent.none);
				GUI.DrawTexture(new Rect(bounds.x,bounds.y+(bounds.height/slices),bounds.width,bounds.height/slices*3f),cont.image,ScaleMode.ScaleToFit);
				if(!cont.alive){
					GUI.DrawTexture(new Rect(bounds.x,bounds.y+(bounds.height/slices),bounds.width,bounds.height/slices*3f),X,ScaleMode.ScaleToFit);
				}
				
				//kills
				GUI.Label(new Rect(bounds.x,bounds.y+((bounds.height/slices)*4f),bounds.width-(bounds.height/slices),bounds.height/slices*1f),"Kills: "+Manager.manager.contestants[numba].kills.ToString());
				
				//weapons
				for(int k=0;k<Manager.manager.contestants[numba].weapons.Count;k++){
					GUI.Label(new Rect(bounds.x,bounds.y+((bounds.height/slices)*(5f+((float)k))),bounds.width-(bounds.height/slices),bounds.height/slices*1f),Manager.manager.contestants[numba].weapons[k].name);
					GUI.DrawTexture(new Rect(bounds.x+bounds.width-(bounds.height/slices),bounds.y+((bounds.height/slices)*(5f+((float)k))),(bounds.height/slices),bounds.height/slices),Manager.manager.contestants[numba].weapons[k].image,ScaleMode.ScaleToFit);
				}
				
				numba++;
			}
		}
		
		if(displayMode == 0){
		
			Rect boxo = new Rect((Screen.width/2f)+padX,padY,(Screen.width/2f)-(padX*2f),(Screen.height*0.95f)-(padY*2f));
			GUI.Box(new Rect(boxo.x,boxo.y,boxo.width,boxo.height),GUIContent.none);
			GUILayout.BeginArea(boxo);
				GUI.Box(new Rect(0f,0f,boxo.width,boxo.height),GUIContent.none);
				scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(boxo.width), GUILayout.Height(boxo.height));
					
				GUILayout.EndScrollView();
			GUILayout.EndArea();
			
		}
		else if(displayMode == 1){

			GUI.skin.textArea.alignment = TextAnchor.UpperLeft;
			Rect boxo = new Rect((Screen.width/16f)*9f,padY,(Screen.width/16f)*6f,(Screen.height*0.95f)-(padY*2f));
			GUILayout.BeginArea(boxo);
				GUI.Box(new Rect(0f,0f,boxo.width,boxo.height),GUIContent.none);
				scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(boxo.width), GUILayout.Height(boxo.height));
					GUILayout.TextArea(output);
				GUILayout.EndScrollView();
			GUILayout.EndArea();
		}
		
		
		
		
		
	}
	
	
}
