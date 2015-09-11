using UnityEngine;
using System.Collections;

public class PlayerCameraControl : MonoBehaviour 
{
	//
	public float _rotationSpeed = 2f;
	//
	public Transform _player;
	//
	public Vector3 _offsetPosition;
	public Vector3 _offsetRotation;
	private Vector3 _moveOffset;
	//
	private bool _moved = false;
	// Use this for initialization
	private void Start () 
	{
		transform.position = new Vector3 (_player.position.x+_offsetPosition.x, _player.position.y+_offsetPosition.y, _player.position.z+_offsetPosition.z);
		transform.eulerAngles = new Vector3 (_player.rotation.x+_offsetRotation.x, _player.rotation.y+_offsetRotation.y, _player.rotation.z+_offsetRotation.z);
		_moveOffset = transform.position;
	}
	
	// Update is called once per frame
	private void FixedUpdate () 
	{
		if (PlayerState.isStopped ()) 
		{
			_moveOffset = Quaternion.AngleAxis (Input.GetAxis("Mouse X") * _rotationSpeed, Vector3.up) * _moveOffset;
			transform.position = _player.position + _moveOffset; 
			transform.LookAt(_player.position);
			_moved = true;
		}
	}
	//
	public void Recenter()
	{
		if (_moved) 
		{
			transform.position = new Vector3 (_player.position.x+_offsetPosition.x, _player.position.y+_offsetPosition.y, _player.position.z+_offsetPosition.z);
			transform.eulerAngles = new Vector3 (_player.rotation.x+_offsetRotation.x, _player.rotation.y+_offsetRotation.y, _player.rotation.z+_offsetRotation.z);
			_moveOffset = transform.position;
			_moved = false;
		}
	}
}
