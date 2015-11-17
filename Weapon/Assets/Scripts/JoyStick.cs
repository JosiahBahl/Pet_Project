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
	// Use this for initialization
	void Start () 
    {
        _image = this.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        _mousePosition = Input.mousePosition;
	    if(Dragging)
        {
            if (InBounds(_top))
            {
                _top.rectTransform.anchoredPosition = new Vector2((_top.rectTransform.sizeDelta.x - _mousePosition.x) * -1, (_top.rectTransform.sizeDelta.y - _mousePosition.y) * -1);
            }
            else 
            {
                _top.rectTransform.anchoredPosition = new Vector2(_top.rectTransform.anchoredPosition.x-1, _top.rectTransform.anchoredPosition.y-1);
            }
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
    //
    public bool InBounds(Image x)
    {
        bool temp = false;
        if ((_top.rectTransform.anchoredPosition.x + _top.rectTransform.sizeDelta.x) < (_image.rectTransform.sizeDelta.x - _top.rectTransform.sizeDelta.x)
            && (_top.rectTransform.anchoredPosition.y + _top.rectTransform.sizeDelta.y) < (_image.rectTransform.sizeDelta.y - _top.rectTransform.sizeDelta.y))
        {
            temp = true;
        }
        else { }
        return temp;
    }
}
