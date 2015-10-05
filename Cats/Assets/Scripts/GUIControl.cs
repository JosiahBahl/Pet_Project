﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIControl : MonoBehaviour 
{
    public static GameObject InteractivePopup;
    public static Text InteractiveText;
	// Use this for initialization
	void Start () 
	{
        InteractivePopup = GameObject.Find("InteractivePopup");
        InteractiveText = InteractivePopup.GetComponentInChildren<Text>();
        InteractivePopup.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
