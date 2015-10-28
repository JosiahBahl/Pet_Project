using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour 
{
	//
	public string _name = "";
	//
	public float _weight = 0;
	//
	public Sprite _image;
	//
	public MeshRenderer _mesh;
	//
	public Rigidbody _rigidbody;
	//
	public int _sellPrice = 0;
	// Use this for initialization
	void Start () 
	{
	
	}
	// Update is called once per frame
	void Update () 
	{
	
	}
	//
	public virtual void Use()
	{

	}
}
