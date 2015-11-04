using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
    //
    public bool LockMovement = false;
    public bool Moving = false;
    public bool Jumping = false;
    //
    private Rigidbody _rigidbody;
    //
    public float _speed = 3f;
    public float _direction = 0f;
    public float _jumpHeight = 350f;
    //
    public Vector3 _velocity = new Vector3(0, 0, 0);
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
            _direction = Input.GetAxis("Horizontal");
            #if UNITY_EDITOR || UNITY_STANDALONE
            if(_direction != 0)
            {
                Moving = true;
            }
            else
            {
                Moving = false;
            }
            _velocity = new Vector3(_direction * _speed, _rigidbody.velocity.y, 0);
            //
            if(Input.GetKeyDown("space") && !Jumping)
            {
                 _rigidbody.AddForce(Vector3.up*_jumpHeight);
                 Jumping = true;
            }
            //
            _rigidbody.velocity = _velocity;
            #endif
        }
    }
}
