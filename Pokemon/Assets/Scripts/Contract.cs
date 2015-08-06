using UnityEngine;
using System.Collections;

public class Contract : MonoBehaviour 
{
	//
	public GameObject _model;
	//
	public Stats _stats = new Stats();
	//
	public Contract()
	{

	}
	//
	public Contract(string name)
	{
		_model = (GameObject)Resources.Load ("Contracts/" + name);
		_stats = _model.GetComponent<Stats> ();
	}
	// Use this for initialization
	void Start () 
	{
		_model = (GameObject)Resources.Load ("Contracts/PlaceHolder");
		_stats = _model.GetComponent<Stats> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
