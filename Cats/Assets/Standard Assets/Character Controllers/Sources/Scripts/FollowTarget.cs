using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour 
{
	public Transform _target;
	//
	public Vector3 _offsetPosition;
	//
	public float _speed = .1f;
	//
	private Vector3 _lookTarget;
	// Use this for initialization
	void Start () 
	{
		transform.position = _target.position+_offsetPosition;
	}
	
	// Update is called once per frame
	void Update () 
	{
		_lookTarget = _target.position + _offsetPosition;
		transform.position = Vector3.MoveTowards (transform.position, _lookTarget, _speed);
	}
}
