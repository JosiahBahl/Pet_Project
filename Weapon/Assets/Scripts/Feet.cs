using UnityEngine;
using System.Collections;

public class Feet : MonoBehaviour 
{
	private PlayerController _playerScript;
	// Use this for initialization
	void Start () 
	{
		_playerScript = transform.parent.GetComponent<PlayerController>();
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
			_playerScript.Grounded = true;
		}
        if(c.tag == "Platform")
        {
            _playerScript.OnPlatform = true;
        }
        else
        {
            _playerScript.OnPlatform = false;
        }
	}
}
