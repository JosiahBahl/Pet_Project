using UnityEngine;
using System.Collections;

public class ShipControls : Usable 
{
    //
    public float _horiAxis = 0f;
    public float _rotationSpeed = 2f;
    public float _rotation = 0f;
    public float _speed = 0;
    //
    public Vector2 _minMaxWheelRot;
    //
    public bool _anchored = true;
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        if(!_anchored)
        {
            transform.Translate(Vector3.right * Time.deltaTime * _speed);
        }
        if (_inUse)
        {
            _horiAxis = Input.GetAxis("Horizontal");
            _rotation += _horiAxis;
            _rotation = Mathf.Clamp(_rotation, _minMaxWheelRot.x, _minMaxWheelRot.y);
            //
            if (_horiAxis < -.1)
            {
                transform.Rotate(Vector3.up * (Time.deltaTime * (_rotation / _rotationSpeed)));
                
            }
            else if (_horiAxis > .1)
            {
                transform.Rotate(Vector3.up * (Time.deltaTime * (_rotation / _rotationSpeed)));
            }
            else 
            {
                _rotation = 0;
            }
        }
	}
    //
    public override void Use()
    {
        base.Use();
        PlayerControl.Controling = true;
    }
    //
    public override void UnUse()
    {
        base.UnUse();
        PlayerControl.Controling = false;
    }
    //
    public void DropAnchor()
    {
        Debug.Log("Drop");
        _anchored = true;
    }
    //
    public void RaiseAnchor()
    {
        Debug.Log("Raise");
        _anchored = false;
    }
}
