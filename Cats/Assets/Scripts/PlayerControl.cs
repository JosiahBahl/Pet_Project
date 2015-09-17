using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour 
{
	//
	public float _vertAxis = 0f;
	public float _horiAxis = 0f;
	public float _jumpAxis = 0f;
	public float _translationSpeed = 3f;
	public float _rotationSpeed = 100f;
	public float _jumpSpeed = 40f;
	public float _jumpHeight = 10f;
	//
	public bool _jumping = false;
	//
	private RaycastHit _hit;
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
		_jumpAxis = Input.GetAxis("Jump");
		//
		if(_vertAxis > 0)
		{
			transform.Translate(0,0,(_vertAxis*_translationSpeed)*Time.deltaTime);
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
		}
		else{}
		//
		if(_jumpAxis > 0 && !_jumping)
		{
			rigidbody.velocity = Vector3.zero;
			rigidbody.AddForce(new Vector3(0,_jumpHeight*_jumpSpeed,0));
			_jumping = true;
		}
		//States
		if (_vertAxis == 0 && _horiAxis == 0) 
		{
			PlayerState.SetStopped (true);
		} 
		else 
		{
			PlayerState.SetStopped (false);
		}
		//Debug.DrawLine(transform.position, new Vector3(0,-.3f,0), Color.red, Time.deltaTime);
		//
		if(_jumping && rigidbody.velocity.y < 0)
		{
			if(Physics.Raycast(transform.position, Vector3.down, out _hit, .7f))
		   	{
				if(_hit.collider.tag == "ground")
				{
					rigidbody.velocity = Vector3.zero;
					_jumping = false;
				}
			}
		}
	}
}
