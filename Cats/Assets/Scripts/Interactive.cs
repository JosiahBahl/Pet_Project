using UnityEngine;
using System.Collections;

public class Interactive : MonoBehaviour 
{
    //
    public bool _active = false;
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
        if(!_useableScript._inUse)
        {
            if (!_active)
            {
                if (PlayerCameraControl.Index == _index)
                {
                    ShowPopup();
                }
            }
            else
            {
                if (PlayerCameraControl.Index != _index)
                {
                    HidePopup();
                }
                if (Input.GetKeyDown("e"))
                {
                    _useableScript.Use();
                }
            }
        }
        else
        {
            if (Input.GetKeyDown("e"))
            {
                _useableScript.UnUse();
            }
        }
	}

    public void ShowPopup()
    {
        GUIControl.InteractivePopup.SetActive(true);
        GUIControl.InteractivePopup.transform.localPosition = _popupPosition;
        GUIControl.InteractivePopup.transform.localEulerAngles = _popupRotation;
        _active = true;
    }
    //
    public void HidePopup()
    {
        GUIControl.InteractivePopup.SetActive(false);
        _active = false;
    }
}
