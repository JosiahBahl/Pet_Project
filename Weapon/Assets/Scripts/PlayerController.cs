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
	public bool Climing = false;
    public bool OnPlatform = false;
	private bool _climingDone = false;
	private bool _movingLeft = false;
	private bool _movingRight = false;
    private bool _dropping = false;
    //
    private Rigidbody _rigidbody;
    //
    public float _speed = 3f;
    public float _direction = 0f;
    public float _jumpHeight = 350f;
	public float _comboTime = .5f;
	public float _comboTimer = 0f;
	public float _climgSpeed = 4;
    //
    public Vector3 _velocity = new Vector3(0, 0, 0);
	//
	private RaycastHit _hit;
	private float _distToGround = 1f;
	//
	public int _combo = 0;
	//
	private PlayerGUI _gui;
	//
	private Ladder _ladder;
	// Use this for initialization
	void Start () 
    {
        _rigidbody = this.gameObject.GetComponent<Rigidbody>();
		_gui = GameObject.Find("PlayerUI").GetComponent<PlayerGUI>();
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
			//
			_velocity = new Vector3(_direction * _speed, _rigidbody.velocity.y, 0);
			//
			_rigidbody.velocity = _velocity;
        }
		//
		if ((Time.timeSinceLevelLoad >= _comboTimer) && (_combo != 0)) 
		{
			_combo = 0;
		}
		if(!Climing && _climingDone)
		{
			_climingDone = false;
			LockMovement = false;
			_rigidbody.useGravity = true;
			gameObject.layer = 8;
		}
        if(_dropping && Grounded)
        {
            _dropping = false;
            gameObject.layer = 8;
        }
    }
	//
	public void MoveUp()
	{
		if(!LockMovement && Grounded)
		{
			if (ByLadder && !Climing) 
			{
				Climing = true;
				LockMovement = true;
				_rigidbody.useGravity = false;
				_movingLeft = false;
				_movingRight = false;
				//
				transform.position = new Vector3(_ladder.transform.position.x, transform.position.y, transform.position.z);
				//
				gameObject.layer = 9;
				//
				_velocity = Vector3.zero;
				_rigidbody.velocity = _velocity;
				_direction = 0;
				//
				StartCoroutine(Climb(_ladder, true));
			} 
		}
		else{}
	}
    //
    public void MoveDown()
    {
        if (!LockMovement && Grounded)
        {
            if (ByLadder && !Climing)
            {
                Climing = true;
                LockMovement = true;
                _rigidbody.useGravity = false;
                _movingLeft = false;
                _movingRight = false;
                //
                transform.position = new Vector3(_ladder.transform.position.x, transform.position.y, transform.position.z);
                //
                gameObject.layer = 9;
                //
                _velocity = Vector3.zero;
                _rigidbody.velocity = _velocity;
                _direction = 0;
                //
                StartCoroutine(Climb(_ladder, false));
            }
            else if(OnPlatform)
            {
                Grounded = false;
                _dropping = true;
                gameObject.layer = 9;
            }
        }
        else { }
    }
	//
	public void Jump()
	{
		if(Grounded && !LockMovement)
		{
			_rigidbody.AddForce(Vector3.up*_jumpHeight);
			Grounded = false;
		}
	}
	//
	public void MoveLeft(bool x)
	{
		if(!LockMovement)
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
		else{}
	}
	//
	public void MoveRight(bool x)
	{
		if(!LockMovement)
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
		else{}
	}
	//
	public void setBlocking(bool x)
	{
		if(!LockMovement)
		{
			Blocking = x;
		}
		else{}
	}
	//
	public void Attack()
	{
		if(!LockMovement)
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
		else{}
	}
	//
	public void OnTriggerEnter(Collider c)
	{
		if(!Climing)
		{
			if(c.gameObject.tag == "LadderUp")
			{
				ByLadder = true;
				_ladder = c.transform.parent.GetComponent<Ladder>();
				_ladder._up = true;
			}
			else if(c.gameObject.tag == "LadderDown")
			{
				ByLadder = true;
				_ladder = c.transform.parent.GetComponent<Ladder>();
				_ladder._up = false;
			}
			else
			{
			
			}
		}
	}
	//
	public void OnTriggerExit(Collider c)
	{
		if((c.gameObject.tag == "LadderUp") || (c.gameObject.tag == "LadderDown"))
		{
			ByLadder = false;
			_ladder = null;
		}
		else
		{
			
		}
	}
    //
    public void OnCollisionEnter(Collision c)
    {
        if((c.gameObject.tag == "Ground" || c.gameObject.tag == "Platform")&& !Grounded)
        {
            _direction = 0;
        }
    }
	//
	public IEnumerator Climb(Ladder ladder, bool up)
	{
		float positionY = transform.position.y;
		while(Climing)
		{
			if(up)
			{
				if(transform.position.y < positionY+ladder._height)
				{
					transform.Translate(Vector3.up*Time.deltaTime*_climgSpeed);
				}
				else
				{
					Climing = false;
				}
	  	 	}
			else
			{
				if(transform.position.y > positionY-ladder._height)
				{
					transform.Translate(Vector3.down*Time.deltaTime*_climgSpeed);
				}
				else
				{
					Climing = false;
				}
			}
			yield return null;
		}
		_climingDone = true;
		yield return null;
	}
}
