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
		
	}
	
}
