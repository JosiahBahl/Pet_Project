using UnityEngine;
using System.Collections;

abstract public class Usable : MonoBehaviour 
{
    public bool _inUse = false;
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
