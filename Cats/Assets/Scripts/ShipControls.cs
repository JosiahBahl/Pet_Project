using UnityEngine;
using System.Collections;

public class ShipControls : Usable 
{
    //
    public float _vertAxis = 0f;
    public float _horiAxis = 0f;
    public float _translationSpeed = 3f;
    public float _rotationXSpeed = 2f;
    public float _rotationYSpeed = 2f;
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        if (_inUse)
        {
            //-----------------------------------------------------------------------------------
            //Player Control
            _vertAxis = Input.GetAxis("Vertical");
            _horiAxis = Input.GetAxis("Horizontal");
            if (_vertAxis > 0)
            {
                transform.Translate(0, 0, (_vertAxis * _translationSpeed) * Time.deltaTime);
            }
            else if (_vertAxis < 0)
            {
                transform.Translate(0, 0, (_vertAxis) * Time.deltaTime);
            }
            else { }
            //
            if (_horiAxis != 0)
            {
                transform.Translate((_horiAxis * _translationSpeed) * Time.deltaTime, 0, 0);
            }
            else { }
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
}
