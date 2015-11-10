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
	private bool _climingUp = false;
	private bool _climingDown = false;
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
			Climing = true;
			_rigidbody.useGravity = false;
			transform.position = new Vector3(_ladder.transform.position.x, transform.position.y, transform.position.z);
			gameObject.layer = 9;
			StartCoroutine(Climb(_ladder));
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
	//
	public void OnTriggerEnter(Collider c)
	{
		if(c.gameObject.tag == "LadderUp")
		{
			_gui.setVertText("Climb");
			ByLadder = true;
			_ladder = c.transform.parent.GetComponent<Ladder>();
			_ladder._up = true;
		}
		else if(c.gameObject.tag == "LadderDown")
		{
			_gui.setVertText("Descend");
			ByLadder = true;
			_ladder = c.transform.parent.GetComponent<Ladder>();
			_ladder._up = false;
		}
		else
		{
		
		}
	}
	//
	public void OnTriggerExit(Collider c)
	{
		if((c.gameObject.tag == "LadderUp") || (c.gameObject.tag == "LadderDown"))
		{
			_gui.setVertText("^");
			ByLadder = false;
			_ladder = null;
		}
		else
		{
			
		}
	}
	//
	public IEnumerator Climb(Ladder ladder)
	{
		float positionY = transform.position.y;
		while(Climing)
		{
			if(ladder._up)
			{
				if(transform.position.y < positionY+ladder._height)
				{
					transform.Translate(Vector3.up*Time.deltaTime);
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
					transform.Translate(Vector3.down*Time.deltaTime);
				}
				else
				{
					Climing = false;
				}
			}
			yield return null;
		}
		yield return null;
	}
}
