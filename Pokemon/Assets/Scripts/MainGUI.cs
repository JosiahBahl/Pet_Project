using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainGUI : MonoBehaviour 
{
	/*------------------------------------------------------------------------*/
	//Singular
	public static MainGUI _mainGUI;
	/*------------------------------------------------------------------------*/
	//
	public Button _arrow;
	public Button _quit;
	public Button _mute;
	public Button _deleateData;
	public Button _yes;
	public Button _no;
	//
	public Image _panel;
	public Image _grayout;
	//
	public Text _messageText;
	//
	public GameObject _popUp;
	/*------------------------------------------------------------------------*/
	//Texture for the arrow button
	public Sprite _downArrow;
	public Sprite _upArrow;
	/*------------------------------------------------------------------------*/
	public bool _messageInUse = false;
	//Bools for if the controls are visible/invisible
	public bool _slideIn = true;
	public bool _slideOut = false;
	//If the controls are animating
	public bool _sliding = false;
	//Bool to trigger the pop up window for quiting
	public bool _quiting = false;
	//Bool to trigger the new user GUI
	public bool _new = false;
	//
	public bool _foundBattle = false;
	//
	public bool _deleatingData = false;
	// Use this for initialization
	void Awake () 
	{
		if(_mainGUI == null)
		{
			DontDestroyOnLoad(gameObject);
			_mainGUI = this;			
		}
		else if(_mainGUI != this)
		{
			Destroy(this);
		}
	}
	//Set screen resolution for GUi scaling
	public void Start()
	{
		_panel.rectTransform.transform.position = new Vector3 (0, ((_panel.rectTransform.transform.position.y)+_panel.rectTransform.rect.height),-10);
		_popUp.SetActive(false);
	}
	// Update is called once per frame
	void Update () {}
	//Animation for sliding in the control rect
	public IEnumerator SlideIn()
	{
		_sliding = true;
		float max = _panel.rectTransform.transform.position.y + (_panel.rectTransform.rect.height+1);
		Vector3 pos = _panel.rectTransform.transform.position;
		while(pos.y < max)
		{
			pos.y += 4;
			_panel.rectTransform.transform.position = pos;
			yield return null;
		}
		_sliding = false;
		_slideIn = true;
		_slideOut = false;
		_arrow.image.sprite = _downArrow;
		_grayout.enabled = false;
	}
	//Animation for sliding out the control rect
	public IEnumerator SlideOut()
	{
		_sliding = true;
		float min = _panel.rectTransform.transform.position.y - (_panel.rectTransform.rect.height+1);
		Vector3 pos = _panel.rectTransform.transform.position;
		while(pos.y > min)
		{
			pos.y -= 4;
			_panel.rectTransform.transform.position = pos;
			yield return null;
		}
		_sliding = false;
		_slideIn = false;
		_slideOut = true;
		_arrow.image.sprite = _upArrow;
	}
	//
	public void FoundBattle()
	{
		_foundBattle = true;
		_quiting = false;
		_deleatingData = false;
		_messageInUse = true;
		//
		_popUp.SetActive (true);
		_messageText.text = "You have spotted an unknown party, would you like to engage them in combat?";
		if (_slideOut) 
		{
			StartCoroutine (SlideIn ());
		}
	}
	//
	public void BattleTimeOut()
	{
		_foundBattle = false;
		_popUp.SetActive (false);
		_messageInUse = false;
	}
	//
	public void QuitClick()
	{
		if (!_messageInUse) 
		{
			_quiting = true;
			_messageInUse = true;
			_popUp.SetActive (true);
			_messageText.text = "Would you like to quit?";
		}
	}
	//
	public void DeleateSaveData()
	{
		if (!_new && !_messageInUse) 
		{
			_deleatingData = true;
			_messageInUse = true;
			_popUp.SetActive (true);
			_messageText.text = "Would you like to deleate your current save data?";
		}
	}
	//
	public void ArrowClick()
	{
		if (!_messageInUse) 
		{
			if (_slideIn && !_sliding) 
			{
				StartCoroutine (SlideOut ());
				_grayout.enabled = true;
			} 
			else if (_slideOut && !_sliding) 
			{
				StartCoroutine (SlideIn ());
			} 
			else{}
		}
	}
	//
	public void YesButton()
	{
		if(_quiting)
		{
			_quiting = false;
			Application.Quit();
		}
		if (_deleatingData) 
		{
			_deleatingData = false;
			ControlSystem._control.DeleateUserData();
			StartCoroutine(SlideIn());
		}
		if (_foundBattle) 
		{
			_deleateData.gameObject.SetActive(false);
			ControlSystem._control.EnterBattle ();
			_foundBattle = false;
		}
		_popUp.SetActive (false);
		_messageInUse = false;
	}
	//
	public void NoButton()
	{
		if (_foundBattle) 
		{
			ControlSystem._control._timeOut = true;
			_foundBattle = false;
		}
		_quiting = false;
		_deleatingData = false;
		_popUp.SetActive (false);
		_messageInUse = false;
	}
}
