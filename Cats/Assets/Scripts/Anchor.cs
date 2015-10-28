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
    public override void Use()
    {
        Debug.Log("Anchor Use");
        _shipControls.DropAnchor();
        base.Use();
    }
    //
    public override void UnUse()
    {
        _shipControls.RaiseAnchor();
        base.UnUse();
    }
}
