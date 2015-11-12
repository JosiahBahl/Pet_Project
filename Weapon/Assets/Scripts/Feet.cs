using UnityEngine;
using System.Collections;

public class Feet : MonoBehaviour 
{
	private PlayerData _data;
	// Use this for initialization
	void Start () 
	{
		_data = transform.parent.GetComponent<PlayerData>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	//
	public void OnTriggerEnter(Collider c)
	{
        if ((c.tag == "Ground" || c.tag == "Platform"))
		{
			_data.Grounded = true;
		}
        if(c.tag == "Platform")
        {
			_data.OnPlatform = true;
        }
        else
        {
			_data.OnPlatform = false;
        }
	}
}
