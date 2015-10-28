using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonControl : MonoBehaviour 
{
	private Inventory _inventory;
	//
	public static bool Opened = false;
	//
	public int _index = 0;
	//
	public GameObject _background;
	public GameObject _buttonRow;
	public GameObject _content;
	//
	public GameObject[] _buttons;
	//
	public RectTransform[] _buttonRects;
	//
	public Image[] _buttonImages;
	//
	public Color[] _colors = new Color[3];
	//
	public bool[] _active;
	//
	public Vector3 _mousePosition;
	// Use this for initialization
	void Start () 
	{
		//_buttons = _buttonRow.GetComponentsInChildren<GameObject>();
		_buttonRects = new RectTransform[_buttons.Length];
		_buttonImages = new Image[_buttons.Length];
		_active = new bool[_buttons.Length];
		for(int i = 0; i < _buttons.Length; i++)
		{
			_buttonRects[i] = _buttons[i].GetComponent<RectTransform>();
			_buttonImages[i] = _buttons[i].GetComponent<Image>();
			_buttonImages[i].color = _colors[0];
			_active[i] = false;
		}
		_inventory = gameObject.GetComponent<Inventory>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		_mousePosition = Input.mousePosition;
		//
		if(Input.GetKeyDown("q") && !Opened)
		{
			Open ();
		}
		else if(Input.GetKeyDown("q") && Opened)
		{
			Close ();
		}
		else{}
		//
		if(Opened)
		{
			if(!DataControl.Controller)
			{
				for(int i = 0; i < _buttons.Length; i++)
				{
					if(OnHover(_mousePosition, _buttonRects[i]) && !_active[i])
					{
						_buttonImages[i].color = _colors[1];
						if(Input.GetKeyDown("mouse 0"))
						{
							_active[_index] = false;
							_index = i;
							_active[i] = true;
							switch(i)
							{
								case 0:
									_inventory.Init(_content);
									break;
								default:
									break;
							}
						}

					}
					else if(!_active[i])
					{
						_buttonImages[i].color = _colors[0];
					}
					else
					{
						_buttonImages[i].color = _colors[2];
					}
				}
	  	 	}
		   	else
		   	{

			}
		}
	}
	//
	public void Open()
	{
		Opened = true;
		_index = 0;
		_background.SetActive(true);
		_buttonRow.SetActive(true);
		_content.SetActive(true);
		for(int i = 0; i < _buttons.Length; i++)
		{
			_buttonImages[i].color = _colors[0];
			_active[i] = false;
		}
		_active[0] = true;
		_buttonImages[0].color = _colors[2];
		PlayerControl.Controling = true;
		_inventory.Init(_content);
	}
	//
	public void Close()
	{
		Opened = false;
		_background.SetActive(false);
		_buttonRow.SetActive(false);
		_content.SetActive(false);
		PlayerControl.Controling = false;
	}
	//
	public bool OnHover(Vector3 mouse, RectTransform rect)
	{
		Vector3[] corners = new Vector3[4];
		rect.GetWorldCorners(corners);
		if(mouse.x >= corners[0].x && mouse.x <= corners[2].x && mouse.y >= corners[0].y && mouse.y <= corners[2].y)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
