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
	public Vector2 _maxMinX;
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
		if (!_playerScript.Moving)
		{
			Centered = false;
		}
		else{}
		if(Centering)
		{
			if (_playerPosition.position.x > (this.transform.position.x+_maxMinX.y))
			{
				this.transform.Translate (Vector3.right * Time.deltaTime * _speed);
			}
			else if (_playerPosition.position.x < (this.transform.position.x-_maxMinX.y))
			{
				this.transform.Translate (Vector3.left * Time.deltaTime * _speed);
			}
			else
			{
				Centering = false;
				Centered = true;
			}
		}
		else
		{
			if((_playerPosition.position.x < (this.transform.position.x - _maxMinX.x)) || 
			   (_playerPosition.position.x > (this.transform.position.x + _maxMinX.x)))
			{
				Centering = true;
			}
			else{}
		}
	}
}
