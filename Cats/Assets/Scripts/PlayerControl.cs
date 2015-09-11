using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour 
{
	//
	public float _vertAxis = 0f;
	public float _horiAxis = 0f;
	public float _translationSpeed = 3f;
	public float _rotationSpeed = 100f;
	//
	public PlayerCameraControl _cameraControl;
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
			_cameraControl.Recenter();
		}
		else if(_vertAxis < 0)
		{
			transform.Translate(0,0,(_vertAxis)*Time.deltaTime);
		}
		else{}
		//
		if(_horiAxis != 0)
		{
			transform.Rotate(0,(_horiAxis*(_rotationSpeed)*Time.deltaTime),0);
			_cameraControl.Recenter();
		}
		else{}
		//States
		if (_vertAxis == 0 && _horiAxis == 0) 
		{
			PlayerState.SetStopped (true);
		} 
		else 
		{
			PlayerState.SetStopped (false);
		}
	}
}
