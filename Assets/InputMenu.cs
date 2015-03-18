using UnityEngine;
using System.Collections;

public class InputMenu : MonoBehaviour{

	private int height = 3;
	private int width = 8;
	
	private int slices = 8;
	
	public float padding = 0.001f;
	
	private string[] urls;
	private WWW[] requests;
	
	public GUISkin skin;
	
	void Start (){
		
		Manager.manager = new Manager();
		urls = new string[height*width];
		requests = new WWW[height*width];
		
		
		for(int i=0;i<(height*width);i++){
			bool sex = false;
			if(i%2 == 0){
				sex = true;
			}
			Manager.manager.contestants[i] = new Contestant("Contestant "+(i+1).ToString(),new Texture2D(0,0),((i/2)+1),sex,0.5f,0.5f,0.5f);
			urls[i] = "IMAGE URL";
		}
		
	}
	
	void Update(){
		if(Input.GetKeyDown(KeyCode.Space)){
			
		}
	}
	
	void OnGUI (){
		GUI.skin = skin;
	
		int numba = 0;
		float bWidth = (Screen.width/(float)width);
		float bHeight = (Screen.height/(float)height);
		
		//contestants
		for (int i=0; i<height; i++) {
			for (int j=0; j<width; j++) {
				
				GUI.skin.label.alignment = TextAnchor.MiddleCenter;
				GUI.skin.textField.alignment = TextAnchor.MiddleCenter;
				
				//Make current draw area
				float offsetX = -(Screen.width*padding);
				float offsetY = Screen.width*padding;
				float padX = Screen.width*padding;
				float padY = Screen.width*padding;
				if(numba%2 == 0){
					offsetX = -offsetX;
				}
				Rect bounds = new Rect((bWidth*(float)j)+(offsetX/2f),(bHeight*(float)i)+(offsetY),bWidth-(padX),bHeight-(padY*2f));
				
				Contestant cont = Manager.manager.contestants[numba];
				GUI.Box(bounds,"");
				
				GUI.skin.label.fontSize = GUI.skin.textField.fontSize = GUI.skin.textArea.fontSize = (int)((bHeight/slices)/2);
				GUI.skin.button.fontSize = GUI.skin.textField.fontSize = GUI.skin.textArea.fontSize = (int)((bHeight/slices)/3);
				
				string bDisp;
				
				//District
				GUI.Label(new Rect(bounds.x,bounds.y,bounds.width/4f,bounds.height/slices),cont.district.ToString()+":");
				
				//Name
				Fit(cont.name,(bounds.width/4f)*3,GUI.skin.textField);
				cont.name = GUI.TextField(new Rect(bounds.x+(bounds.width/4f),bounds.y,(bounds.width/4f)*3f,bounds.height/slices),cont.name);
				GUI.skin.textField.fontSize = (int)((bHeight/slices)/2);
				
				//URL and Image
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
					GUI.DrawTexture(new Rect(bounds.x,bounds.y+(bounds.height/slices),bounds.width,bounds.height/slices*3f),cont.image,ScaleMode.ScaleToFit);
					if(GUI.Button(new Rect(bounds.x+bounds.width-(bounds.height/slices),bounds.y+((bounds.height/slices)*3f),(bounds.height/slices),(bounds.height/slices)),"New")){
						Texture2D.Destroy(cont.image);
						cont.image = new Texture2D(0,0);
					}
				}
				
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
				
				float prevStr = cont.strength;
				float prevFri = cont.friendliness;
				float prevInt = cont.intelligence;
				float diff = 0f;
				
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
	}
	
	public void Fit(string input,float maxSize,GUIStyle element){
		if(element.CalcSize(new GUIContent(input)).x > maxSize){
			
			element.fontSize -= 1;
			
			if(element.fontSize <=1){
				return;
			}
			
			Fit(input,maxSize,element);
			
		}
	}
	
	
	
}








