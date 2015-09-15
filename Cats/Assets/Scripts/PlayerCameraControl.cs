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
	//
	private Vector3 _lookTarget;
	// Use this for initialization
	private void Start () 
	{
		transform.position = _player.position+_offsetPosition;
	}
	
	// Update is called once per frame
	private void FixedUpdate () 
	{
		transform.LookAt(_player.position);
		transform.RotateAround(_player.position, Vector3.up, Input.GetAxis("Mouse X"));
		transform.RotateAround(_player.position, Vector3.right, Input.GetAxis("Mouse Y"));
	}
}
