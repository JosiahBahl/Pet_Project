using UnityEngine;
using System.Collections;

public class Singleton : MonoBehaviour 
{
	//
	void Awake () 
	{
		DontDestroyOnLoad(gameObject);
	}

	public void OnLevelLoaded()
	{
		if (Application.loadedLevel == 2) 
		{
			GameObject _gameobject = GameObject.Find (this.gameObject.name);
			if(_gameobject == null)
			{
							
			}
			else
			{
				Destroy(this.gameObject);
			}
		}
	}
}
