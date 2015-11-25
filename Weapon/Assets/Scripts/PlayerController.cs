using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	//Player data var for accessing the states of the player
	private PlayerData _data;
    //Keep track of internal states
	private bool _climingDone = false;
	private bool _movingLeft = false;
	private bool _movingRight = false;
    private bool _dropping = false;
    //Rigid body for movement
    private Rigidbody _rigidbody;
    //vars for movement speeds
    public float _speed = 3f;
    public float _jumpHeight = 350f;
	public float _climgSpeed = 4;
    public float _rotationSpeed = 4;
	//Direction of travles, -1 left|0 idle|1 right
	public int _direction = 0;
    //Velocty of player
    public Vector3 _velocity = new Vector3(0, 0, 0);
	//Ladder var for if were near one
	private Ladder _ladder;
	//Weapon var for our current equiped weapon
	public Weapon _weapon;
	//Grab rigidbody and player data
	void Start () 
    {
        _rigidbody = this.GetComponent<Rigidbody>();
		_data = this.GetComponent<PlayerData>();
	}
    //Using Fixed update for physics
    void FixedUpdate()
    {
		//If the player can move
		if(!_data.LockMovement)
        {
			//Check to see if we are moving
			if(_direction != 0)
			{
				_data.Moving = true;
                _data.GetAnimator().SetBool("Idle", false);
			}
			else
			{
				_data.Moving = false;
                _data.GetAnimator().SetBool("Idle", true);
			}
			//Assign velocity of player based on _direction and _speed
			_velocity = new Vector3(_direction * _speed, _rigidbody.velocity.y, 0);
			_rigidbody.velocity = _velocity;
            //Set animation based on _direction
            _data.GetAnimator().SetInteger("Direction", _direction);
        }
		/*
		/If we are done climing enable rigidbody gravity, 
		/change the current physics layer back to Player layer,
		/enable movement
		*/
		if(!_data.Climing && _climingDone)
		{
			_climingDone = false;
			_data.LockMovement = false;
			_rigidbody.useGravity = true;
			gameObject.layer = 8;
		}
		//If you hit the ground from falling
		if(_dropping && _data.Grounded)
        {
            _dropping = false;
            gameObject.layer = 8;
        }
    }
	//Move up function to be used for movement
	public void MoveUp()
	{
        if (!_data.LockMovement && _data.Grounded)
		{
			//If we are by a ladder and it goes up
            if (_data.ByLadder && !_data.Climing && _ladder._up) 
			{
				//Disable movement of the player and gravity of the rigidbody
				_data.Climing = true;
				_data.LockMovement = true;
				_rigidbody.useGravity = false;
				_movingLeft = false;
				_movingRight = false;
				//Position the palyer for climing the ladder
				transform.position = new Vector3(_ladder.transform.position.x, transform.position.y, transform.position.z);
				//Change the physics layer so we can pass through the ladder
				gameObject.layer = 9;
				//Make sure the velocity of the player is zero so they are not moving left or right while climing the ladder.
				_velocity = Vector3.zero;
				_rigidbody.velocity = _velocity;
				_direction = 0;
				//Start the climing of the ladder, up
				StartCoroutine(Climb(_ladder, true));
			} 
		}
		else{}
	}
    //Move down function used for movement
    public void MoveDown()
    {
		if (!_data.LockMovement && _data.Grounded)
        {
			//If we are by a ladder and it goes down
			if (_data.ByLadder && !_data.Climing && !_ladder._up)
            {
				//Disable movement of the player and gravity of the rigidbody
				_data.Climing = true;
				_data.LockMovement = true;
                _rigidbody.useGravity = false;
                _movingLeft = false;
                _movingRight = false;
				//Position the palyer for climing the ladder
                transform.position = new Vector3(_ladder.transform.position.x, transform.position.y, transform.position.z);
				//Change the physics layer so we can pass through the ladder
                gameObject.layer = 9;
				//Make sure the velocity of the player is zero so they are not moving left or right while climing the ladder.
                _velocity = Vector3.zero;
                _rigidbody.velocity = _velocity;
                _direction = 0;
				//Start the climing of the ladder, up
                StartCoroutine(Climb(_ladder, false));
            }
			//If we are on a platform that we can drop down from.
			else if(_data.OnPlatform)
            {
				_data.Grounded = false;
                _dropping = true;
                gameObject.layer = 9;
            }
        }
        else { }
    }
	//Jump function to be used for movement
	public void Jump()
	{
		if(_data.Grounded && !_data.LockMovement)
		{
			//Accelerate up
			_rigidbody.AddForce(Vector3.up*_jumpHeight);
			_data.Grounded = false;
		}
	}
	//Function to be used for movement
	//#param dirction is an int used for moving left or right, -1 left| 0 no direction| 1 right
	public void Move(int direction)
	{
		if(!_data.LockMovement)
		{
			//If we are not moving left and we want to go left, make sure we are ground so we can't change direction in the air
			if(!_movingLeft && _data.Grounded && direction == -1)
			{
				//Set direction to left, set bools, rotate the character to the left.
                _movingLeft = true;
                _movingRight = false;
				StartCoroutine(Rotate(180));
                _direction = -1;
			}
			//If we are not moving right and we want to go right,
			else if(!_movingRight && _data.Grounded && direction == 1)
			{
                _movingLeft = false;
                _movingRight = true;
                StartCoroutine(Rotate(0));
                _direction = 1;
			}
            else if (direction == 0)
            {
                _movingLeft = false;
                _movingRight = false;
                _direction = 0;
            }
            else { }
            if(direction == 2)
            {
                MoveUp();
            }
            else if (direction == -2)
            {
                MoveDown();
            }
            else { }
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
