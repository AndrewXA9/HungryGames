using UnityEngine;
using System.Collections;

public class Weapon{
	public Texture2D image;
	public string name;
	public float damage;
	
	public Weapon(Texture2D _image, string _name, float _damage){
		image = _image;
		name = _name;
		damage = _damage;
	}
	
}
