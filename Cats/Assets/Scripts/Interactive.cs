using UnityEngine;
using System.Collections;

public class Interactive : MonoBehaviour 
{
    //
    public bool _active = false;
    private bool _buttonUp = true;
    //
    public int _index = 0;
    //
    public Usable _useableScript;
    //
    public Vector3 _popupPosition;
    public Vector3 _popupRotation;
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        //
        if (PlayerCameraControl.Index == _index && _active)
        {
            ShowPopup();
            if(!_useableScript._inUse)
            {
                if (Input.GetKeyDown("e"))
                {
                    _useableScript.Use();
                    _buttonUp = false;
                }
            }
        }
        else if (PlayerCameraControl.Index != _index && _active)
        {
            HidePopup();
        }
        else { }
        if (_active && _useableScript._inUse && _buttonUp)
        {
            if (Input.GetKeyDown("e"))
            {
                _useableScript.UnUse();
            }
        }
        if(!_buttonUp && Input.GetKeyUp("e"))
        {
            _buttonUp = true;
        }
	}

    public void ShowPopup()
    {
        GUIControl.InteractivePopup.SetActive(true);
        GUIControl.InteractivePopup.transform.localPosition = _popupPosition;
        GUIControl.InteractivePopup.transform.localEulerAngles = _popupRotation;
    }
    //
    public void HidePopup()
    {
        GUIControl.InteractivePopup.SetActive(false);
    }
}
