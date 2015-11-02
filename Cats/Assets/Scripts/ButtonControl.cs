using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ButtonControl : MonoBehaviour 
{
	//
	public class MainButton
	{
		//
		public GameObject _object;
		public RectTransform _rect;
		public Image _image;
		public bool _active;
		//
		public MainButton(GameObject obj, RectTransform rect, Image image, bool active)
		{
			_object = obj;
			_rect = rect;
			_image = image;
			_active = active;
		}
		//
		public void setImage(Image image)
		{
			_image = image;
		}
		//
		public void setColor(Color color)
		{
			_image.color = color;
		}
		//
		public void setActive(bool x)
		{
			_active = x;
		}
		//
		public string Print()
		{
			string x = _object.name +", "+_rect.rect+", "+_image.color+", "+_active;
			return x;
		}
	}
	private Inventory _inventory;
	//
	public bool _opened = false;
	//
	public int _index = 0;
	//
	public GameObject _background;
	public GameObject _buttonRow;
	public GameObject _content;
	//
	public List<MainButton> _buttons = new List<MainButton>();
	//
	public Color[] _colors = new Color[3];
	//
	public Vector3 _mousePosition;
	// Use this for initialization
	void Start () 
	{
		Image[] o = _buttonRow.GetComponentsInChildren<Image>();
		for(int i = 0; i < o.Length; i++)
		{
			_buttons.Add(new MainButton(o[i].gameObject, o[i].GetComponent<RectTransform>(), o[i].GetComponent<Image>(), false));
		}
		_inventory = gameObject.GetComponent<Inventory>();
		//
		_background.SetActive(false);
		_buttonRow.SetActive(false);
		_content.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		_mousePosition = Input.mousePosition;
		//
		if(Input.GetKeyDown("q") && !_opened)
		{
			Open ();
		}
		else if(Input.GetKeyDown("q") && _opened)
		{
			Close ();
		}
		else{}
		//
		if(_opened)
		{
			if(!DataControl.Controller)
			{
				for(int i = 0; i < _buttons.Count; i++)
				{
					if(OnHover(_mousePosition, _buttons[i]._rect) && !_buttons[i]._active)
					{
						_buttons[i].setColor(_colors[1]);
						if(Input.GetKeyDown("mouse 0"))
						{
							_buttons[_index].setActive(false);
							_index = i;
							_buttons[i].setActive(true);
							switch(i)
							{
								case 0:
									_inventory.Load(_content);
									break;
								default:
									break;
							}
						}

					}
					else if(!_buttons[i]._active)
					{
						_buttons[i].setColor(_colors[0]);
					}
					else
					{
						_buttons[i].setColor(_colors[2]);
					}
				}
	  	 	}
		   	else
		   	{

			}
		}
		if(Input.GetKeyDown("p"))
		{
			for(int i = 0; i < _buttons.Count; i++)
			{
				Debug.Log (_buttons[i].Print());
			}
		}
	}
	//
	public void Open()
	{
		_opened = true;
		_index = 0;
		_background.SetActive(true);
		_buttonRow.SetActive(true);
		_content.SetActive(true);
		for(int i = 0; i < _buttons.Count; i++)
		{
			_buttons[i].setColor(_colors[0]);
			_buttons[i].setActive(false);
		}
		_buttons[0].setActive(true);
		_buttons[0].setColor(_colors[2]);
		PlayerControl.LockAllMovement = true;
		_inventory.Load(_content);
	}
	//
	public void Close()
	{
		_opened = false;
		_background.SetActive(false);
		_buttonRow.SetActive(false);
		_content.SetActive(false);
		PlayerControl.LockAllMovement = false;
		if(_buttons[0]._active)
		{
			_inventory.Unload(_content);
		}
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
