using UnityEngine;
using System.Collections;

public class Weapon{
	public Texture2D image;
	public string name;
	public int damage;
	
	public Weapon(Texture2D _image, string _name, int _damage){
		image = _image;
		name = _name;
		damage = _damage;
	}
	
}
