using UnityEngine;
using System.Collections;

public class Anchor : Usable
{
    public ShipControls _shipControls;
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
    //
    public void Use()
    {
        _shipControls.DropAnchor();
        base.Use();
    }
    //
    public void Unuse()
    {
        base.UnUse();
        _shipControls.RaiseAnchor();
    }
}
