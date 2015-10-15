using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour 
{
    public Transform _cameraRotation;
	//
	public float _vertAxis = 0f;
	public float _horiAxis = 0f;
	public float _jumpAxis = 0f;
	public float _translationSpeed = 3f;
	public float _jumpSpeed = 40f;
	public float _jumpHeight = 10f;
    public float _rotationXSpeed = 2f;
    public float _rotationYSpeed = 2f;
    private float x = 0f;
    private float y = 0f;
    //
    public Vector2 _minMaxY;
	//
	public bool _jumping = false;
    public static bool Controling = false;
	//
	private RaycastHit _hit;
    //
    public PlayerCameraControl _cameraScript;
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
    private void FixedUpdate()
    {
        //Camera Control
        if (!DataControl.Controller)
        {
            x = Input.GetAxis("Mouse X") * _rotationXSpeed;
            y -= Input.GetAxis("Mouse Y") * _rotationYSpeed;
            y = Mathf.Clamp(y, _minMaxY.x, _minMaxY.y);
        }
        else
        {
            x += Input.GetAxis("JoystickMouseX") * _rotationXSpeed;
            y -= Input.GetAxis("JoystickMouseY") * _rotationYSpeed;
            y = Mathf.Clamp(y, _minMaxY.x, _minMaxY.y);
        }
        _cameraRotation.localEulerAngles = new Vector3(y, _cameraRotation.rotation.y, 0f);
        transform.Rotate(0, x, 0);
        //-----------------------------------------------------------------------------------
        //Player Control
        _vertAxis = Input.GetAxis("Vertical");
        _horiAxis = Input.GetAxis("Horizontal");
        _jumpAxis = Input.GetAxis("Jump");
        //
        if (!Controling)
        {
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
            //
            if (_jumpAxis > 0 && !_jumping)
            {
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                GetComponent<Rigidbody>().AddForce(new Vector3(0, _jumpHeight * _jumpSpeed, 0));
                _jumping = true;
            }
            //States
            if (_vertAxis == 0 && _horiAxis == 0)
            {
                PlayerState.SetStopped(true);
            }
            else
            {
                PlayerState.SetStopped(false);
            }
        }
        //Debug.DrawLine(transform.position, new Vector3(0,-.3f,0), Color.red, Time.deltaTime);
        //
        if (_jumping && GetComponent<Rigidbody>().velocity.y < 0)
        {
            if (Physics.Raycast(transform.position, Vector3.down, out _hit, .7f))
            {
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                _jumping = false;
            }
        }
        //-----------------------------------------------------------------------------------
    }
    //
    private void OnTriggerEnter(Collider x)
    {
        InteractiveTrigger temp = x.gameObject.GetComponent<InteractiveTrigger>();
        if (temp != null)
        {
            _cameraScript.AddInteraction(temp._interactiveObject, temp._interactiveObject.GetComponent<Interactive>());
        }
    }
    //
    private void OnTriggerExit(Collider x)
    {
         InteractiveTrigger temp = x.gameObject.GetComponent<InteractiveTrigger>();
         if (temp != null)
         {
             if (PlayerCameraControl.ObjectsIn.Contains(temp._interactiveObject))
             {
                 _cameraScript.RemoveInteraction(temp._interactiveObject);
             }
         }
    }
}
