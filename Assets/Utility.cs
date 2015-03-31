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
	
}
