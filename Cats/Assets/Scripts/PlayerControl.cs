using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour 
{
	//
	public float _vertAxis = 0f;
	public float _horiAxis = 0f;
	public float _translationSpeed = 2f;
	public float _rotationSpeed = 2f;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	private void FixedUpdate () 
	{
		//
		_vertAxis = Input.GetAxis("Vertical");
		_horiAxis = Input.GetAxis("Horizontal");
		//
		if(_vertAxis > 0)
		{
			transform.Translate(0,0,(_vertAxis*_translationSpeed)*Time.deltaTime);
		}
		else if(_vertAxis < 0)
		{
			transform.Translate(0,0,(_vertAxis*(_translationSpeed/2))*Time.deltaTime);
		}
		else{}
		//
		if(_horiAxis != 0)
		{
			transform.Rotate(0,(_horiAxis*(_rotationSpeed)*Time.deltaTime),0);
		}
		else{}
	}
}
