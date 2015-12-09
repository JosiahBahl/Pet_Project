using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerGUI : MonoBehaviour 
{
	public Slider _healthBar;
	public Slider _staminaBar;
    //
    public GameObject _joystick;
    public GameObject _attackButton;
    public GameObject _jumpButton;
    public GameObject _blockButton;
    public GameObject _useButton;
	// Use this for initialization
	void Start () 
	{
#if UNITY_IPHONE || UNITY_ANDROID
        _joystick.SetActive(true);
        _attackButton.SetActive(true);
        _jumpButton.SetActive(true);
        _blockButton.SetActive(true);
        _useButton.SetActive(true);
#endif
	}
	// Update is called once per frame
	void Update () 
	{
	
	}
}
