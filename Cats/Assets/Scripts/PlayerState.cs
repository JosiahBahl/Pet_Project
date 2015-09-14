using UnityEngine;
using System.Collections;

public class PlayerState : MonoBehaviour 
{
	public static bool[] _states = new bool[5];
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	//
	public static bool isStopped()
	{
		return _states [0];
	}
	//
	public static void SetStopped(bool x)
	{
		_states [0] = x;
	}
}
