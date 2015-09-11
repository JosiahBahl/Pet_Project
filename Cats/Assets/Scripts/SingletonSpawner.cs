using UnityEngine;
using System.Collections;

public class SingletonSpawner : MonoBehaviour 
{
	public GameObject _object;

	private void Awake()
	{
		GameObject temp = GameObject.Find (_object.name);
		if(temp){}
		else
		{
			temp = (GameObject)Instantiate(_object);
			temp.name = _object.name;
		}
		Destroy(this.gameObject);
	}
}
