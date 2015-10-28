using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour 
{
	public List<Item> _items = new List<Item>();
	//
	public float _weight = 0;
	//
	public GameObject _storage;
	// Use this for initialization
	void Start () 
	{
	
	}
	// Update is called once per frame
	void Update () 
	{
	
	}
	//
	public void Init(GameObject area)
	{
		_items.Add(new Item());
		for(int i = 0; i < _items.Count; i++)
		{
			area.AddComponent<GridLayoutGroup>();
			GameObject o = new GameObject();
			o.transform.parent = area.transform;
			o.name = _items[i]._name;
			o.AddComponent<Image>();
		}

	}
}
