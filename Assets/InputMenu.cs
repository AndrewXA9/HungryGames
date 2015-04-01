using UnityEngine;
using System.Collections;
//using System.Collections.Generic;

public class InputMenu : MonoBehaviour{

	private int height = 3;
	private int width = 8;
	
	private int maxWeaps = 8;
	
	private int slices = 8;
	
	private float padding = 0.002f;
	
	private string[] ContUrls;
	private WWW[] ContRequests;
	private string[] SponsUrls;
	private WWW[] SponsRequests;
	private string[] WeapUrls;
	private WWW[] WeapRequests;
	
	public GUISkin skin;
	
	public int display = 0;
	
	void Start (){
		
		Manager.manager = new Manager();
		ContUrls = new string[height*width];
		ContRequests = new WWW[height*width];
		SponsUrls = new string[maxWeaps];
		SponsRequests = new WWW[maxWeaps];
		WeapUrls = new string[maxWeaps];
		WeapRequests = new WWW[maxWeaps];
		
		
		for(int i=0;i<(height*width);i++){
			
			ContUrls[i] = "IMAGE URL";
			
			if(i<maxWeaps){
				SponsUrls[i] = "IMAGE URL";
				WeapUrls[i] = "IMAGE URL";
			}
		}
		
	}
	
	void Update(){
		if(Input.GetKeyDown(KeyCode.Space)){
			Texture2D img = (Texture2D)Resources.Load("guy");
			
			foreach(Contestant i in Manager.manager.contestants){
				i.image = img;
			}
		}
	}
	
	void OnGUI (){
		GUI.skin = skin;
	
		int numba = 0;
		float bWidth = (Screen.width/(float)width);
		float bHeight = ((Screen.height*0.95f)/(float)height);
		
		float padX = Screen.width*padding;
		float padY = Screen.width*padding;
		
		
		if(display == 0){
			//contestants
			
			for (int i=0; i<height; i++) {
				for (int j=0; j<width; j++) {
					
					GUI.skin.label.alignment = TextAnchor.MiddleCenter;
					GUI.skin.textField.alignment = TextAnchor.MiddleCenter;
					
					//Make current draw area
					float offsetX = -(Screen.width*padding);
					float offsetY = Screen.width*padding;
					
					if(numba%2 == 0){
						offsetX = -offsetX;
					}
					Rect bounds = new Rect((bWidth*(float)j)+(offsetX/2f),(bHeight*(float)i)+(offsetY),bWidth-(padX),bHeight-(padY*2f));
					
					Contestant cont = Manager.manager.contestants[numba];
					GUI.Box(bounds,"");
					
					GUI.skin.label.fontSize = GUI.skin.textField.fontSize = GUI.skin.textArea.fontSize = (int)((bHeight/slices)/2);
					GUI.skin.button.fontSize = (int)((bHeight/slices)/3);
					
					string bDisp;
					
					//District
					GUI.Label(new Rect(bounds.x,bounds.y,bounds.width/4f,bounds.height/slices),cont.district.ToString()+":");
					
					//Name
					Utility.Fit(cont.name,(bounds.width/4f)*3,GUI.skin.textField);
					cont.name = GUI.TextField(new Rect(bounds.x+(bounds.width/4f),bounds.y,(bounds.width/4f)*3f,bounds.height/slices),cont.name);
					GUI.skin.textField.fontSize = (int)((bHeight/slices)/2);
					
					//URL and Image
					Utility.ImageBox(ref cont.image,ref ContUrls[numba],ref ContRequests[numba],bounds,slices);
					/*
					if(cont.image.width == 0){
						urls[numba] = GUI.TextArea(new Rect(bounds.x,bounds.y+(bounds.height/slices),(bounds.width),(bounds.height/slices)*2f),urls[numba]);
						
						bDisp = "Download Image";
						if(requests[numba] != null){
							if(requests[numba].progress < 1f){
								bDisp = ((int)(requests[numba].progress*100f)).ToString()+"%";
							}
							else{
								if(string.IsNullOrEmpty(requests[numba].error)){
									requests[numba].LoadImageIntoTexture(cont.image);
								}
								else{
									urls[numba] = "Couldn't load image";
								}
								requests[numba].Dispose();
								requests[numba] = null;
							}
							
						}
						
						if(GUI.Button(new Rect(bounds.x,bounds.y+((bounds.height/slices)*3f),(bounds.width),(bounds.height/slices/2)),bDisp)){
							if(requests[numba] != null){
								requests[numba].Dispose();
								Debug.Log("Deleting old");
							}
							requests[numba] = new WWW(urls[numba]);
							Debug.Log("Requesting new");
						}
					}
					else{
						GUI.Box(new Rect(bounds.x,bounds.y+(bounds.height/slices),bounds.width,bounds.height/slices*3f),"");
						GUI.DrawTexture(new Rect(bounds.x,bounds.y+(bounds.height/slices),bounds.width,bounds.height/slices*3f),cont.image,ScaleMode.ScaleToFit);
						if(GUI.Button(new Rect(bounds.x+bounds.width-(bounds.height/slices),bounds.y+((bounds.height/slices)*3f),(bounds.height/slices),(bounds.height/slices)),"New")){
							Texture2D.Destroy(cont.image);
							cont.image = new Texture2D(0,0);
						}
					}*/
					
					GUI.skin.button.fontSize = (int)((bHeight/slices)/2);
					
					//Sex
					bDisp = "Female";
					if(cont.gender){
						bDisp = "Male";
					}
					if(GUI.Button(new Rect(bounds.x,bounds.y+((bounds.height/slices)*4),bounds.width,bounds.height/slices),bDisp)){
						cont.gender = !cont.gender;
					}
					
					GUI.skin.label.alignment = TextAnchor.UpperCenter;
					float sliderHeight = GUI.skin.horizontalSlider.CalcHeight(new GUIContent(),bounds.width);
					
					//Strength
					cont.strength = GUI.HorizontalSlider(new Rect(bounds.x,bounds.y+((bounds.height/slices)*6f)-sliderHeight,bounds.width,bounds.height/slices),cont.strength,0f,1f);
					GUI.Label(new Rect(bounds.x,bounds.y+((bounds.height/slices)*5f)-2f,bounds.width,bounds.height/slices),"Strength: "+((int)(cont.strength*10f)).ToString());
					
					//Friendliness
					cont.friendliness = GUI.HorizontalSlider(new Rect(bounds.x,bounds.y+((bounds.height/slices)*7f)-sliderHeight,bounds.width,bounds.height/slices),cont.friendliness,0f,1f);
					GUI.Label(new Rect(bounds.x,bounds.y+((bounds.height/slices)*6f)-2f,bounds.width,bounds.height/slices),"Friendliness: "+((int)(cont.friendliness*10f)).ToString());
					
					//Int
					cont.intelligence = GUI.HorizontalSlider(new Rect(bounds.x,bounds.y+((bounds.height/slices)*8f)-sliderHeight,bounds.width,bounds.height/slices),cont.intelligence,0f,1f);
					GUI.Label(new Rect(bounds.x,bounds.y+((bounds.height/slices)*7f)-2f,bounds.width,bounds.height/slices),"Intelligence: "+((int)(cont.intelligence*10f)).ToString());
					
					numba++;
				}	
			}
			
			//extra buttons
			Rect bottomBox = new Rect(padX,(Screen.height*0.95f)+padY,Screen.width-(padX*2f),(Screen.height*0.05f)-(padY*2f));
			if(GUI.Button(new Rect(bottomBox.x,bottomBox.y,(bottomBox.width/3f)-padX,bottomBox.height),"Reset")){
				Application.LoadLevel(Application.loadedLevel);
			}
			if(GUI.Button(new Rect(bottomBox.x+(bottomBox.width/3f),bottomBox.y,(bottomBox.width/3f)-padX,bottomBox.height),"GOOOOOOOOOOO")){
				this.enabled = false;
				gameObject.GetComponent<Simulate>().enabled = true;
			}
			if(GUI.Button(new Rect(bottomBox.x+((bottomBox.width/3f)*2),bottomBox.y,(bottomBox.width/3f)-padX,bottomBox.height),"Sponsors/Weapons")){
				display = 1;
			}
			
			
		}
		else if(display == 1){
			//secondary screen
			
			//sponsors
			for(int i=0;i<Manager.manager.sponsors.Count;i++){
			
				float offsetY = Screen.width*padding;
				
				Rect bounds = new Rect((Screen.width/2f)-((bWidth*(float)Manager.manager.sponsors.Count)/2f)+((bWidth*(float)i)),((bHeight*3f)/12f)*1f,bWidth-(padX),bHeight-(padY*2f));
				Sponsor spons = Manager.manager.sponsors[i];
				GUI.Box(bounds,"");
				
				GUI.skin.label.fontSize = GUI.skin.textField.fontSize = GUI.skin.textArea.fontSize = (int)((bHeight/slices)/2);
				GUI.skin.button.fontSize = (int)((bHeight/slices)/3);
				
				string bDisp;
				
				//Name
				Utility.Fit(spons.name,(bounds.width),GUI.skin.textField);
				spons.name = GUI.TextField(new Rect(bounds.x+(bounds.width/4f),bounds.y,(bounds.width/4f)*3f,bounds.height/slices),spons.name);
				GUI.skin.textField.fontSize = (int)((bHeight/slices)/2);
				
				//URL and Image
				Utility.ImageBox(ref spons.image,ref SponsUrls[i],ref SponsRequests[i],bounds,slices);
				
				GUI.skin.button.fontSize = (int)((bHeight/slices)/2);
				
				if(GUI.Button(new Rect(bounds.x,bounds.y+((bounds.height/slices)*6),bounds.width,bounds.height/slices),"Remove")){
					Manager.manager.sponsors.Remove(spons);
				}
				
			}
			
			//weapons
			for(int i=0;i<Manager.manager.weapons.Count;i++){
				
				float offsetY = Screen.width*padding;
				
				Rect bounds = new Rect((Screen.width/2f)-((bWidth*(float)Manager.manager.weapons.Count)/2f)+((bWidth*(float)i)),((bHeight*3f)/12f)*7f,bWidth-(padX),bHeight-(padY*2f));
				Weapon weap = Manager.manager.weapons[i];
				GUI.Box(bounds,"");
				
				GUI.skin.label.fontSize = GUI.skin.textField.fontSize = GUI.skin.textArea.fontSize = (int)((bHeight/slices)/2);
				GUI.skin.button.fontSize = (int)((bHeight/slices)/3);
				
				string bDisp;
				
				//Name
				Utility.Fit(weap.name,(bounds.width),GUI.skin.textField);
				weap.name = GUI.TextField(new Rect(bounds.x+(bounds.width/4f),bounds.y,(bounds.width/4f)*3f,bounds.height/slices),weap.name);
				GUI.skin.textField.fontSize = (int)((bHeight/slices)/2);
				
				//URL and Image
				Utility.ImageBox(ref weap.image,ref WeapUrls[i],ref WeapRequests[i],bounds,slices);
				
				GUI.skin.button.fontSize = (int)((bHeight/slices)/2);
				
				if(GUI.Button(new Rect(bounds.x,bounds.y+((bounds.height/slices)*6),bounds.width,bounds.height/slices),"Remove")){
					Manager.manager.weapons.Remove(weap);
				}
				
				GUI.skin.label.alignment = TextAnchor.UpperCenter;
				float sliderHeight = GUI.skin.horizontalSlider.CalcHeight(new GUIContent(),bounds.width);
				
				weap.damage = GUI.HorizontalSlider(new Rect(bounds.x,bounds.y+((bounds.height/slices)*5f)-sliderHeight,bounds.width,bounds.height/slices),weap.damage,0f,1f);
				GUI.Label(new Rect(bounds.x,bounds.y+((bounds.height/slices)*4f)-2f,bounds.width,bounds.height/slices),"Damage: "+((int)(weap.damage*10f)).ToString());
				
			}
			
			
			Rect bottomBox = new Rect(padX,(Screen.height*0.95f)+padY,Screen.width-(padX*2f),(Screen.height*0.05f)-(padY*2f));
			
			if(Manager.manager.sponsors.Count < maxWeaps){
				if(GUI.Button(new Rect(bottomBox.x,bottomBox.y,(bottomBox.width/3f)-padX,bottomBox.height),"Add Sponsor")){
					Manager.manager.sponsors.Add(new Sponsor(new Texture2D(0,0), "Sponsor "+(Manager.manager.weapons.Count+1).ToString()));
				}
			}
			if(Manager.manager.weapons.Count < maxWeaps){
				if(GUI.Button(new Rect(bottomBox.x+(bottomBox.width/3f),bottomBox.y,(bottomBox.width/3f)-padX,bottomBox.height),"Add Weapon")){
					Manager.manager.weapons.Add(new Weapon(new Texture2D(0,0), "Weapon "+(Manager.manager.weapons.Count+1).ToString(),0.5f));
				}
			}
			if(GUI.Button(new Rect(bottomBox.x+((bottomBox.width/3f)*2),bottomBox.y,(bottomBox.width/3f)-padX,bottomBox.height),"Contestants")){
				display = 0;
			}
			
		}
		
		
		
	}
	
	
}








