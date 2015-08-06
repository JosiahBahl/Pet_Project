using UnityEngine;
using System.Collections;

public class Action
{
	//
	public int _step = 1;
	//
	public string _type = "";
	//
	public GameObject _target;
	public GameObject _user;
	//
	public bool _finished = false;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void SetActionType(string type)
	{
		switch(type)
		{
			case "attack":
				_type = "attack";
				_step++;
				break;
			case "defend":
				_type = "defend";
				_step++;
				break;
			case "special":
				_type = "special";
				_step++;
				break;
			default:
				break;
		}
	}
	//
	public void Back()
	{
		_type = "";
		_step--;
	}
	//
	public void IssueCommand()
	{
		_step++;
		_finished = true;
	}
}
