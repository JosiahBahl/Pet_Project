using UnityEngine;
using System.Collections;

public class Settings : MonoBehaviour 
{
    //
    public AudioSource _music;
    //
    public bool _muted = false;
    //
	// Use this for initialization
	void Start () 
    {
      
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
    //
    public void setMusicVolume(float x)
    {
        _music.volume = x;
    }
    //
    public float getMusicVolume()
    {
        return _music.volume;
    }
}
