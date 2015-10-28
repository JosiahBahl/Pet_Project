using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCameraControl : MonoBehaviour 
{
    //
    public static List<GameObject> ObjectsIn = new List<GameObject>();
    //
    private RaycastHit _hit;
    //
    public float _range = 2f;
    //
    public static List<Interactive> Interactions = new List<Interactive>();
    //
    public bool _canInteract = false;
    //
    public static int Index = 0;
    //
    private void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            for (int i = 0; i < ObjectsIn.Count; i++)
            {
                Debug.Log(ObjectsIn[i]);
                Debug.Log(Interactions[i]);
            }
        }
    }
    //
    private void FixedUpdate ()
    {
        if (_canInteract)
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward) * _range;
            Debug.DrawRay(transform.position, forward, Color.red);
            if (Physics.Raycast(transform.position, forward, out _hit, _range))
            {
                if (ObjectsIn.Contains(_hit.collider.gameObject))
                {
                    PlayerCameraControl.Index = ObjectsIn.IndexOf(_hit.collider.gameObject);
                }
                else
                {
                    PlayerCameraControl.Index = -2;
                }
            }
            else
            {
                PlayerCameraControl.Index = -2;
            }
        }
    }
    //
    public void AddInteraction(GameObject x, Interactive y)
    {
        if (!ObjectsIn.Contains(x))
        {
            Interactions.Add(y);
            y._active = true;
            y._index = Interactions.Count - 1;
            ObjectsIn.Add(x);
            _canInteract = true;
        }
    }
    //
    public void RemoveInteraction(GameObject x)
    {
        int i = ObjectsIn.IndexOf(x);
        ObjectsIn.RemoveAt(i);
        Interactions[i]._index = -1;
        Interactions[i]._active = false;
        Interactions.RemoveAt(i);
        if (ObjectsIn.Count == 0)
        {
            _canInteract = false;
        }
    }
}
