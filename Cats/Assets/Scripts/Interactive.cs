using UnityEngine;
using System.Collections;

public class Interactive : MonoBehaviour 
{
    //
    public bool _active = false;
    //
    public int _index = 0;
    //
    public Vector3 _position;
    //
    public Vector3 _rotation;
    //
    public Usable _useableScript;
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(!_useableScript._inUse)
        {
            if(PlayerCameraControl.Index != _index)
            {
                HidePopup();
            }
            else
            {
                ShowPopup();
            }
        }
        else
        {
            if(Input.GetKeyDown("e"))
            {

            }
        }
	}

    public void ShowPopup()
    {
        GUIControl.InteractivePopup.SetActive(true);
        GUIControl.InteractivePopup.transform.position = _position;
        GUIControl.InteractivePopup.transform.rotation = Quaternion.Euler(_rotation);
        _active = true;
    }
    //
    public void HidePopup()
    {
        GUIControl.InteractivePopup.SetActive(false);
        _active = false;
    }
}
