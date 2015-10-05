using UnityEngine;
using System.Collections;

public class PlayerCameraControl : MonoBehaviour 
{
    public static GameObject LookingAt;
    //
    private RaycastHit _hit;
    //
    public float _range = 2f;
    //
    private void FixedUpdate ()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * _range;
        Debug.DrawRay(transform.position, forward, Color.red);
        if(Physics.Raycast(transform.position, forward, out _hit, _range))
		{
            Interactive temp = _hit.transform.GetComponent<Interactive>();
            LookingAt = _hit.collider.gameObject;
            if(temp != null)
            {
                temp.ShowPopup();
            }
		}
    }
}
