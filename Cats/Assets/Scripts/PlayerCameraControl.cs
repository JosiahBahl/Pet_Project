using UnityEngine;
using System.Collections;

public class PlayerCameraControl : MonoBehaviour 
{
	//
	public float _rotationSpeed = 2f;
	//
	public Transform _player;
	//
	public Vector3 _offset;
	//
	private float x = 0f;
	private float y = 0f;
	// Use this for initialization
	private void Start () 
	{
	}
	
	// Update is called once per frame
	private void FixedUpdate () 
	{
		if (!DataControl.Controller) 
		{
			x += Input.GetAxis ("Mouse X") * _rotationSpeed;
			y += Input.GetAxis ("Mouse Y") * _rotationSpeed;
		} 
		else 
		{
			x += Input.GetAxis ("JoystickMouseX") * _rotationSpeed;
			y += Input.GetAxis ("JoystickMouseY") * _rotationSpeed;
			print (Input.GetAxis ("JoystickMouseX"));
		}
		
		//y = ClampAngle(y, yMinLimit, yMaxLimit);
		Quaternion rotation = Quaternion.Euler(y, x, 0f);
		transform.rotation = rotation;
		Vector3 position = rotation * new Vector3(0f, 3, -3) + _player.position;

		transform.position = position;
		transform.LookAt(_player.position);
	}
}
