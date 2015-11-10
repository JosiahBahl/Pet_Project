using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerGUI : MonoBehaviour 
{
	public Text _vertActionText;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	//
	public void setVertText(string x)
	{
		_vertActionText.text = x;
	}
}
