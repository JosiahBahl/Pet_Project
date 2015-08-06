using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntroScreen : MonoBehaviour 
{
	//
	public Sprite[] _screenTexture;
	//
	public Image _fadeImage;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine (SlideShow ());
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	public IEnumerator SlideShow()
	{
		for (int i = 0; i < _screenTexture.Length; i++) 
		{
			_fadeImage.sprite = _screenTexture [i];
			_fadeImage.CrossFadeAlpha(1f, 2f, false);
			yield return new WaitForSeconds (2);
			_fadeImage.CrossFadeAlpha (0f, 2f, true);
			yield return new WaitForSeconds (2);
		}
		Application.LoadLevel (2);
	}
}
