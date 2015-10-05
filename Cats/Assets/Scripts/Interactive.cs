using UnityEngine;
using System.Collections;

public class Interactive : MonoBehaviour 
{
    public bool _active = false;
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (_active)
        {
            if(PlayerCameraControl.LookingAt != this.gameObject)
            {
                HidePopup();
            }
        }
	}

    public void ShowPopup()
    {
        GUIControl.InteractivePopup.SetActive(true);
        _active = true;
    }
    //
    public void HidePopup()
    {
        GUIControl.InteractivePopup.SetActive(false);
        _active = false;
    }
}
