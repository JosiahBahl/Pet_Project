using UnityEngine;
using System.Collections;

public class SpecialAttack : MonoBehaviour 
{
	//
	public string _name = "";
	public string _id = "";
	public string[] _type = new string[3];
	public int _damage = 0;
	//
	public int _rechargeTime = 0;
	public int _currentTime = 0;
	//
	public int[] _stats = new int[4];
	//
	public GameObject _effect;
	//
	public Animation _animation;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	//
	public bool Contains(string x)
	{
		bool temp = false;
		foreach(string t in _type)
		{
			if(t == x.ToLower())
			{
				temp = true;
				break;
			}
		}
		return temp;
	}
}
