using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JoyStick : MonoBehaviour 
{
    //
    public bool Dragging = false;
    public bool Released = false;
    //
    public Image _top;
    private Image _image;
    //
    public Vector2 _mousePosition;
	public Vector4 _bounds;
	public Vector4 _dampeningBounds;
	//
	public PlayerController _movementScript;
	// Use this for initialization
	void Start () 
    {
        _image = this.GetComponent<Image>();
		_bounds = new Vector4(((_image.rectTransform.sizeDelta.x - (_top.rectTransform.sizeDelta.x*2))*-1), ((_image.rectTransform.sizeDelta.y - (_top.rectTransform.sizeDelta.y*2))*-1), (_image.rectTransform.sizeDelta.x - (_top.rectTransform.sizeDelta.x*2)), (_image.rectTransform.sizeDelta.y - (_top.rectTransform.sizeDelta.y*2)));
		_movementScript = GameObject.Find("Player").GetComponent<PlayerController>();
		_dampeningBounds = new Vector4(Mathf.Ceil(_bounds.x/4), Mathf.Ceil(_bounds.y/4), Mathf.Ceil(_bounds.z/4), Mathf.Ceil(_bounds.w/4));
	}
	
	// Update is called once per frame
	void Update () 
    {
        _mousePosition = Input.mousePosition;
	    if(Dragging)
        {
			_top.rectTransform.anchoredPosition = new Vector2(Mathf.Clamp((_top.rectTransform.sizeDelta.x - _mousePosition.x) * -1,_bounds.x,_bounds.z), 
			                                                  Mathf.Clamp((_top.rectTransform.sizeDelta.y - _mousePosition.y) * -1, _bounds.y, _bounds.w));
        }
		else{}
		//
		if(Released)
		{
			_top.rectTransform.anchoredPosition = Vector2.zero;
		}
		else{}
		if(_top.rectTransform.anchoredPosition.x > _dampeningBounds.x)
		{
			_movementScript.MoveLeft(true);
		}
		else
		{
			_movementScript.MoveLeft(false);
		}
		if(_top.rectTransform.anchoredPosition.x < _dampeningBounds.z)
		{

		}
	}
    //
    public void setDragging(bool x)
    {
        Dragging = x;
    }
    //
    public void setReleased(bool x)
    {
        Released = x;
    }
}
