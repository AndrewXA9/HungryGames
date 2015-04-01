using UnityEngine;
using System.Collections;

public class Utility{

	public static void Fit(string input,float maxSize,GUIStyle element){
		if(element.CalcSize(new GUIContent(input)).x > maxSize){
			
			element.fontSize -= 1;
			
			if(element.fontSize <=1){
				return;
			}
			
			Fit(input,maxSize,element);
			
		}
	}
	
	public static void ImageBox(ref Texture2D image,ref string url,ref WWW request,Rect bounds,int slices){
		if(image.width == 0){
			url = GUI.TextArea(new Rect(bounds.x,bounds.y+(bounds.height/slices),(bounds.width),(bounds.height/slices)*2f),url);
			
			string tDisp = "Download Image";
			if(request != null){
				if(request.progress < 1f){
					tDisp = ((int)(request.progress*100f)).ToString()+"%";
				}
				else{
					if(string.IsNullOrEmpty(request.error) && request.isDone){
						request.LoadImageIntoTexture(image);
					}
					else{
						url = "Couldn't load image";
					}
					request.Dispose();
					request = null;
				}
				
			}
			
			if(GUI.Button(new Rect(bounds.x,bounds.y+((bounds.height/slices)*3f),(bounds.width),(bounds.height/slices/2)),tDisp)){
				if(request != null){
					request.Dispose();
					Debug.Log("Deleting old");
				}
				request = new WWW(url);
				Debug.Log("Requesting new");
			}
		}
		else{
			GUI.Box(new Rect(bounds.x,bounds.y+(bounds.height/slices),bounds.width,bounds.height/slices*3f),"");
			GUI.DrawTexture(new Rect(bounds.x,bounds.y+(bounds.height/slices),bounds.width,bounds.height/slices*3f),image,ScaleMode.ScaleToFit);
			if(GUI.Button(new Rect(bounds.x+bounds.width-(bounds.height/slices),bounds.y+((bounds.height/slices)*3f),(bounds.height/slices),(bounds.height/slices)),"New")){
				Texture2D.Destroy(image);
				image = new Texture2D(0,0);
			}
		}
	}
	
}
