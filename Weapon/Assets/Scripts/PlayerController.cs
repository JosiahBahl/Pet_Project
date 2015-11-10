using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
    //
    public bool LockMovement = false;
    public bool Moving = false;
    public bool Grounded = false;
	public bool Blocking = false;
	public bool ByLadder = false;
	private bool _movingLeft = false;
	private bool _movingRight = false;
    //
    private Rigidbody _rigidbody;
    //
    public float _speed = 3f;
    public float _direction = 0f;
    public float _jumpHeight = 350f;
	public float _comboTime = .5f;
	public float _comboTimer = 0f;
    //
    public Vector3 _velocity = new Vector3(0, 0, 0);
	//
	private RaycastHit _hit;
	private float _distToGround = .5f;
	//
	public int _combo = 0;
	// Use this for initialization
	void Start () 
    {
        _rigidbody = this.gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
    //
    void FixedUpdate()
    {
        if(!LockMovement)
        {
			//
			if(_direction != 0)
			{
				Moving = true;
			}
			else
			{
				Moving = false;
			}
			#if UNITY_IPHONE || UNITY_ANDROID

			#endif
            #if UNITY_EDITOR || UNITY_STANDALONE
			//
			//_direction = Input.GetAxis("Horizontal");
			//
            if(Input.GetKeyDown("space"))
            {
				Jump ();
            }
            #endif
			//
			_velocity = new Vector3(_direction * _speed, _rigidbody.velocity.y, 0);
			//
			_rigidbody.velocity = _velocity;
        }
		//
		if(Physics.Raycast(transform.position, Vector3.down, out _hit, _distToGround))
		{
			if(_hit.collider.gameObject.tag == "Ground")
			{
				Grounded = true;
			}
		}
		else
		{
			Grounded = false;
		}
		//
		if ((Time.timeSinceLevelLoad >= _comboTimer) && (_combo != 0)) 
		{
			_combo = 0;
		}
    }
	//
	public void MoveUp()
	{
		if (ByLadder) 
		{

		} 
		else 
		{
			Jump ();
		}
	}
	//
	public void Jump()
	{
		if(Grounded)
		{
			_rigidbody.AddForce(Vector3.up*_jumpHeight);
			Grounded = false;
		}
	}
	//
	public void MoveLeft(bool x)
	{
		if(!_movingRight && !_movingLeft && x && Grounded)
		{
			_movingLeft = true;
			_direction = -1;
		}
		else if(_movingLeft && !x)
		{
			_movingLeft = false;
			_direction = 0;
		}
		else{}
	}
	//
	public void MoveRight(bool x)
	{
		if(!_movingRight && !_movingLeft && x && Grounded)
		{
			_movingRight = true;
			_direction = 1;
		}
		else if(_movingRight && !x)
		{
			_movingRight = false;
			_direction = 0;
		}
		else{}
	}
	//
	public void setBlocking(bool x)
	{
		Blocking = x;
	}
	//
	public void Attack()
	{
		if (_combo < 3) 
		{
			_comboTimer = Time.timeSinceLevelLoad + _comboTime;
			_combo++;
		} 
		else 
		{
			_comboTimer = Time.timeSinceLevelLoad + _comboTime;
			_combo = 1;
		}
	}
}
