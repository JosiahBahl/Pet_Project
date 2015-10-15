using UnityEngine;
using System.Collections;

public class Usable : MonoBehaviour 
{
    public bool _inUse = false;
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
        _inUse = true;
    }
    //
    public virtual void UnUse()
    {
        _inUse = false;
    }
}
