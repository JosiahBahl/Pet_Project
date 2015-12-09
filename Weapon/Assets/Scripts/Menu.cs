using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Menu : MonoBehaviour 
{
    //
    public bool Opened = false;
    //
    public GameObject _settings;
    public GameObject _equipment;
    //
    public SettingsGUI _settingsGUI;
    //
    public Text _menutext;
	// Use this for initialization
	void Start () 
    {
        this.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
    //
    public void Open()
    {
        Opened = true;
        this.gameObject.SetActive(true);
        //
        _settingsGUI.Load();
        //
        _menutext.text = "X";
    }
    //
    public void Close()
    {
        Opened = false;
        this.gameObject.SetActive(false);
        //
        _settingsGUI.Unload();
        //
        _menutext.text = "Menu";
    }
    //
    public void Clear()
    {

    }
    //
    public void OnClick()
    {
        if(!Opened)
        {
            Open();
        }
        else
        {
            Close();
        }
    }
}
