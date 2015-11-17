using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	private PlayerData _data;
    //
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
	public float _climgSpeed = 4;
    public float _rotationSpeed = 4;
    //
    public Vector3 _velocity = new Vector3(0, 0, 0);
	//
	private Ladder _ladder;
	//
	public Weapon _weapon;
	// Use this for initialization
	void Start () 
    {
        _rigidbody = this.GetComponent<Rigidbody>();
		_data = this.GetComponent<PlayerData>();
	}
	// Update is called once per frame
	void Update () 
    {
	
	}
    //
    void FixedUpdate()
    {
		if(!_data.LockMovement)
        {
			if(_movingLeft && _data.Grounded)
			{
				_direction = -1;
			}
			//
			if(_direction != 0)
			{
				_data.Moving = true;
			}
			else
			{
				_data.Moving = false;
			}
			//
			_velocity = new Vector3(_direction * _speed, _rigidbody.velocity.y, 0);
			//
			_rigidbody.velocity = _velocity;
            //
        }
		//
		if(!_data.Climing && _climingDone)
		{
			_climingDone = false;
			_data.LockMovement = false;
			_rigidbody.useGravity = true;
			gameObject.layer = 8;
		}
		if(_dropping && _data.Grounded)
        {
            _dropping = false;
            gameObject.layer = 8;
        }
    }
	//
	public void MoveUp()
	{
		if(!_data.LockMovement && _data.Grounded)
		{
			if (_data.ByLadder && !_data.Climing) 
			{
				_data.Climing = true;
				_data.LockMovement = true;
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
		if (!_data.LockMovement && _data.Grounded)
        {
			if (_data.ByLadder && !_data.Climing)
            {
				_data.Climing = true;
				_data.LockMovement = true;
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
			else if(_data.OnPlatform)
            {
				_data.Grounded = false;
                _dropping = true;
                gameObject.layer = 9;
            }
        }
        else { }
    }
	//
	public void Jump()
	{
		if(_data.Grounded && !_data.LockMovement)
		{
			_rigidbody.AddForce(Vector3.up*_jumpHeight);
			_data.Grounded = false;
		}
	}
	//
	public void MoveLeft(bool x)
	{
		if(!_data.LockMovement)
		{
			if(!_movingLeft && _data.Grounded)
			{

				StartCoroutine(Rotate(180));
			}
			else if(_movingLeft && !x)
			{
				_movingLeft = false;
				_direction = 0;
                _data.GetAnimator().SetFloat("Direction", _direction);
			}
			else{}
		}
		else{}
	}
	//
	public void MoveRight(bool x)
	{
		if(!_data.LockMovement)
		{
			if(!_movingRight && !_movingLeft && x && _data.Grounded)
			{
				_movingRight = true;
				_direction = 1;
                _data.GetAnimator().SetFloat("Direction", _direction);
                StartCoroutine(Rotate(0));
			}
			else if(_movingRight && !x)
			{
				_movingRight = false;
				_direction = 0;
                _data.GetAnimator().SetFloat("Direction", _direction);
			}
			else{}
		}
		else{}
	}
	//
	public void setBlocking(bool x)
	{
		if(!_data.LockMovement)
		{
			_data.Blocking = x;
		}
		else{}
	}
	//
	public void Attack()
	{
		if(!_data.LockMovement)
		{
			if(_weapon != null)
			{
				_weapon.Attack();
			}
		}
		else{}
	}
	//
	public void OnTriggerEnter(Collider c)
	{
		if(!_data.Climing)
		{
			if(c.gameObject.tag == "LadderUp")
			{
				_data.ByLadder = true;
				_ladder = c.transform.parent.GetComponent<Ladder>();
				_ladder._up = true;
			}
			else if(c.gameObject.tag == "LadderDown")
			{
				_data.ByLadder = true;
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
			_data.ByLadder = false;
			_ladder = null;
		}
		else
		{
			
		}
	}
    //
    public void OnCollisionEnter(Collision c)
    {
		if((c.gameObject.tag == "Ground" || c.gameObject.tag == "Platform")&& !_data.Grounded)
        {
            _direction = 0;
        }
    }
	//
	public IEnumerator Climb(Ladder ladder, bool up)
	{
		float positionY = transform.position.y;
		while(_data.Climing)
		{
			if(up)
			{
				if(transform.position.y < positionY+ladder._height)
				{
					transform.Translate(Vector3.up*Time.deltaTime*_climgSpeed);
				}
				else
				{
					_data.Climing = false;
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
					_data.Climing = false;
				}
			}
			yield return null;
		}
		_climingDone = true;
		yield return null;
	}
    public IEnumerator Rotate(int amount)
    {
        Quaternion rotation = transform.rotation;
        for (float t = 0f; t < 1f; t+= Time.deltaTime*_rotationSpeed)
        {
            transform.rotation = Quaternion.Lerp(rotation, Quaternion.Euler(0, amount, 0), t);
            yield return null;
        }
        yield return null;
    }
}
