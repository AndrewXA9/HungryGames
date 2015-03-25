using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Manager{

	public static Manager manager;
	
	public Contestant[] contestants;
	public List<Sponsor> sponsors;
	public List<Weapon> weapons;
	
	public Manager(){
		contestants = new Contestant[24];
		sponsors = new List<Sponsor>();
		weapons = new List<Weapon>();
		
		for(int i=0;i<(contestants.Length);i++){
			bool sex = false;
			if(i%2 == 0){
				sex = true;
			}
			contestants[i] = new Contestant("Contestant "+(i+1).ToString(),new Texture2D(0,0),((i/2)+1),sex,0.5f,0.5f,0.5f);
		}
		
	}
	
}
