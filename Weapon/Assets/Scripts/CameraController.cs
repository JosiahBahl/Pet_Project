using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	private Transform _playerPosition;
	//
	private PlayerController _playerScript;
	//
	public int _speed = 6;
	//
	public float _heightDistance = 4.75f;
	//
	private Vector3 _position;
	//
	public Vector2 _maxMinX;
	public Vector4 _maxMinY;
	//
	public bool Centered = false;
	public bool Centering = false;
	// Use this for initialization
	void Start () 
	{
		_playerPosition = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
		_playerScript = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Centered)
		{
			this.transform.position = new Vector3(_playerPosition.position.x, _playerPosition.position.y+_heightDistance, this.transform.position.z);
		}
		else{}
		if (!_playerScript.Moving && _playerScript.Grounded)
		{
			Centered = false;
		}
		else{}
		if(Centering)
		{
			_position = new Vector3(_playerPosition.position.x, _playerPosition.position.y+_heightDistance, transform.position.z);
			if (_playerPosition.position.x > (this.transform.position.x+_maxMinX.y))
			{
				transform.position = Vector3.MoveTowards(transform.position, _position, Time.deltaTime*_speed);
			}
			if (_playerPosition.position.x < (this.transform.position.x-_maxMinX.y))
			{
				transform.position = Vector3.MoveTowards(transform.position, _position, Time.deltaTime*_speed);
			}
			if (_playerPosition.position.y > (this.transform.position.y+_maxMinY.w))
			{
				transform.position = Vector3.MoveTowards(transform.position, _position, Time.deltaTime*_speed);
			}
			if (_playerPosition.position.y < (this.transform.position.y-_maxMinY.y))
			{
				transform.position = Vector3.MoveTowards(transform.position, _position, Time.deltaTime*_speed);
			}
			if(PlayerInMinBounds())
			{
				Centering = false;
				Centered = true;
			}
		}
		else
		{
			if(_playerScript.Climing)
			{
				this.transform.position = new Vector3(_playerPosition.position.x, _playerPosition.position.y+_heightDistance, this.transform.position.z);
			}
			else
			{
				if(!PlayerInMaxBounds())
				{
					Centering = true;
				}
				else{}
			}
		}
	}
	//
	public bool PlayerInMaxBounds()
	{
		bool temp = false;
		if (_playerPosition.position.x > (this.transform.position.x+_maxMinX.x))
		{
			temp = false;
		}
		else if (_playerPosition.position.x < (this.transform.position.x-_maxMinX.x))
		{
			temp = false;
		}
		else if (_playerPosition.position.y > (this.transform.position.y+_maxMinY.z))
		{
			temp = false;
		}
		else if (_playerPosition.position.y < (this.transform.position.y-_maxMinY.x))
		{
			temp = false;
		}
		else
		{
			temp = true;
		}
		return temp;
	}
	//
	public bool PlayerInMinBounds()
	{
		bool temp = false;
		if (_playerPosition.position.x > (this.transform.position.x+_maxMinX.y))
		{
			temp = false;
		}
		else if (_playerPosition.position.x < (this.transform.position.x-_maxMinX.y))
		{
			temp = false;
		}
		else if (_playerPosition.position.y > (this.transform.position.y+_maxMinY.w))
		{
			temp = false;
		}
		else if (_playerPosition.position.y < (this.transform.position.y-_maxMinY.y))
		{
			temp = false;
		}
		else
		{
			temp = true;
		}
		return temp;
	}
}
