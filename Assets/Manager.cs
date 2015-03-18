using UnityEngine;
using System.Collections;

public class Manager{

	public static Manager manager;
	
	public Contestant[] contestants;
	
	public Manager(){
		contestants = new Contestant[24];
	}
	
}
