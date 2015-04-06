using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Contestant{
	
	public string name;
	public Texture2D image;
	public int district;
	public bool gender;
	
	public float strength;
	public float friendliness;
	public float intelligence;
	
	public float stamina;
	public List<Weapon> weapons;
	public Dictionary<Contestant,float> relationships;
	
	public int kills;
	
	public bool alive;
	
	public Contestant(string _name,Texture2D _image,int _district,bool _gender,float _strength,float _friendliness,float _intelligence){
		name = _name;
		image = _image;
		district = _district;
		gender = _gender;
		strength = _strength;
		friendliness = _friendliness;
		intelligence = _intelligence;
		
		stamina = 0.5f;
		relationships = new Dictionary<Contestant, float>();
		
		weapons = new List<Weapon>();
		relationships = new Dictionary<Contestant, float>();
		
		kills = 0;
		
		alive = true;
		
	}
	
}
