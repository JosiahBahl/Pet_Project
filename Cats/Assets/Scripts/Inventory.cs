using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour 
{
	public List<Item> _items = new List<Item>();
	//
	public Image[] _images;
	//
	public Image _selectBorder;
	//
	private Text _displayText;
	//
	public float _weight = 0;
	//
	public GameObject _storage;
	public GameObject _popup;
	//
	public int _index = -1;
	public int _imageRot = 10;
	//
	private Vector2 _mousePosition;
	//
	public bool _active = false;
	public bool _combining = false;
	//
	public Consumable _consume;
	public Consumable _consumed;
	// Use this for initialization
	void Start () 
	{
		AddItem ("Paper");
		AddItem ("Ink");
	}
	// Update is called once per frame
	void Update () 
	{
		_mousePosition = Input.mousePosition;
		if(_active)
		{
			for(int i = 0; i < _images.Length; i++)
			{
				//For on hover
				if(OnHover(_mousePosition, _images[i].rectTransform) && i != _index)
			   	{
					_images[i].rectTransform.localEulerAngles = new Vector3(0, 0,
					                                                        Mathf.Lerp(_images[i].rectTransform.localEulerAngles.z, _imageRot, Time.deltaTime));
				}
				else if(_images[i].rectTransform.localEulerAngles.z != 0 && i != _index)
				{
					_images[i].rectTransform.localEulerAngles = new Vector3(0, 0,
					                                                        Mathf.Lerp(_images[i].rectTransform.localEulerAngles.z, 0, Time.deltaTime*2));
				}
				else{}
				//For on Click
				if(OnClick(_mousePosition, _images[i].rectTransform) && i != _index)
				{
					if(_index != -1)
					{
						_images[_index].rectTransform.localEulerAngles = Vector3.zero;
					}
					else
					{
						_selectBorder.gameObject.SetActive(true);
						_displayText.text = "Press E to use, Press C to combine with another item, Press D to drop the item on the ground, Click the item again to deselect.";
					}

					_images[i].rectTransform.localEulerAngles = Vector3.zero;
					_index = i;
					_selectBorder.rectTransform.localPosition = new Vector3(_images[i].rectTransform.localPosition.x, _images[i].rectTransform.localPosition.y-100, 0);
				}
				else if(OnClick(_mousePosition, _images[i].rectTransform) && i == _index)
				{
					_index = -1;
					_selectBorder.gameObject.SetActive(false);
					_displayText.text = "Select an inventory item to display options";
				}
				else{}
				//For options
				if(_index != -1)
				{
					if(Input.GetKeyDown("e"))
					{
						if(_items[_index].GetType() == typeof(Consumable))
						{
							_items[_index].Use();
							DeleteItem(_items[_index]);
						}
					}
					else if(Input.GetKeyDown("c"))
					{
						if(_items[_index].GetType() == typeof(Consumable))
						{
							_consume = (Consumable)_items[_index];
							_combining = true;
						}
					}
					else if(Input.GetKeyDown("D"))
					{

					}
					else{}
				}
			}
		}
	}
	//
	public void Load(GameObject area)
	{
		_images = new Image[_items.Count];
		_popup.SetActive(true);
		_active = true;
		_displayText = _popup.GetComponentInChildren<Text>();
		_displayText.text = "Select an inventory item to display options";
		//
		int x = 50;
		int y = -50;
		for(int i = 0; i < _items.Count; i++)
		{
			GameObject o = new GameObject();
			o.transform.parent = area.transform;
			o.name = _items[i]._name;
			//
			RectTransform r = o.AddComponent<RectTransform>();
			//
			r.anchorMax = new Vector2(0, 1);
			r.anchorMin = new Vector2(0, 1);
			r.pivot = new Vector2(.5f, .5f);
			//
			r.localPosition = new Vector3(x,y,0);
			r.localScale = new Vector3(1,1,1);
			r.sizeDelta = new Vector2(100,100);
			x += 100;
			//
			_images[i] = o.AddComponent<Image>();
			_images[i].sprite = _items[i]._image;
		}
	}
	//
	public void AddItem(string name)
	{
		GameObject o = Instantiate((GameObject)Resources.Load("Items/"+name));
		//
		MeshRenderer m = o.GetComponent<MeshRenderer>();
		m.enabled = false;
		//
		Rigidbody r = o.GetComponent<Rigidbody>();
		r.isKinematic = true;
		//
		Collider c = o.GetComponent<Collider>();
		c.enabled = false;
		//
		o.transform.parent = _storage.transform;
		_items.Add(o.GetComponent<Item>());
	}
	//
	public void DeleteItem(Item item)
	{

	}
	//
	public void DropItem(Item item)
	{

	}
	//
	public void Unload(GameObject area)
	{
		_index = -1;
		_popup.SetActive(false);
		_active = false;
		for(int i = 0; i < _images.Length; i++)
		{
			Destroy(_images[i].gameObject);
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
	//
	public bool OnClick(Vector3 mouse, RectTransform rect)
	{
		if(OnHover(mouse, rect) && Input.GetKeyDown("mouse 0"))
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	//
	public void SortInventory()
	{
		//
		int x = 50;
		int y = -50;
		RectTransform r;
		for(int i = 0; i < _images.Length; i++)
		{
			r = _images[i].rectTransform;
			//
			r.localPosition = new Vector3(x,y,0);
			x += 100;
		}
	}
}
