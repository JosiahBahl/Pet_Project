using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Inventory : MonoBehaviour 
{
	public float _xResolution;
	public float _yResolution;
	//
	public float _speed = 5f;
	/*------------------------------------------------------------------------*/
	//
	public GameObject[] _contracts = new GameObject[4];
	//
	public GameObject[] _spawnPoints = new GameObject[4];
	//
	public GameObject _current;
	/*------------------------------------------------------------------------*/
	//
	public int _index = 0;
	/*------------------------------------------------------------------------*/
	//
	private Rect _touchRect = new Rect(0,250,1024,426);
	/*------------------------------------------------------------------------*/
	//
	public Camera _camera;
	/*------------------------------------------------------------------------*/
	//
	public bool _rotating = false;
	/*------------------------------------------------------------------------*/
	//
	public Stats[] _stats = new Stats[4];
	//
	public Text _name;
	public Text _level;
	public Text _health;
	public Text _class;
	public Text _defence;
	public Text _resistance;
	public Text _damage;
	public Text _magicDamage;
	// Use this for initialization
	void Start () 
	{
		_xResolution = Screen.width/1024f;
		_yResolution = Screen.height/768f;
		_camera = ControlSystem._control._camera.GetComponent<Camera> ();
		for (int i = 0; i < 4; i++) 
		{
			if(ControlSystem._control._player._party.Length != 0 && ControlSystem._control._player._party[i])
			{
				_contracts[i] = ControlSystem._control._player._party[i];
				_stats[i] = ControlSystem._control._player._partyStats[i];
				_contracts[i].transform.position = _spawnPoints[i].transform.position;
			}
		}
		if (ControlSystem._control._player._party.Length != 0) 
		{
			_current = _contracts [0];
			SetToForground (_contracts [0].name);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (MainGUI._mainGUI._slideIn) 
		{
			if (Input.GetMouseButtonUp (0) && _rotating)
			{
				_rotating = false;
			}
			if (_rotating) 
			{
				_contracts [_index].transform.Rotate (new Vector3 (0, (-1 * (_speed * Input.GetAxis ("Mouse X"))), 0));
			}
		}
	}
	//
	public void SetToForground(string name)
	{
		int x = 0;
		for (int i = 0; i < 4; i++) 
		{
			if(_contracts[i].name == name)
			{
				x = i;
				break;
			}
			else{}
		}
		_index = x;
		_contracts[x].transform.position = new Vector3(0,0,3);
		_current = _contracts[x];
		_name.text = "Name: "+_stats [_index]._name;
		_level.text = "Level: "+_stats [_index]._level;
		_health.text = "Health: "+_stats [_index]._health;
		_class.text = "Class: "+_stats [_index]._class;
		_defence.text = "Defence: "+_stats [_index]._defence;
		_resistance.text = "Resistance: "+_stats [_index]._resistance;
		_damage.text = "Damage: "+_stats [_index]._damage;
		_magicDamage.text = "Magic Damage: "+_stats [_index]._magicDamage;
	}
	//
	public void SetToBackground()
	{
		_contracts [_index].transform.position = _spawnPoints [_index].transform.position;
		_contracts[_index].transform.rotation = new Quaternion(0,0,0,0);
	}
	//
	void OnGUI()
	{
		//Scaling for GUI elements
		GUI.matrix = Matrix4x4.TRS(new Vector3(1,1,1), Quaternion.identity, new Vector3(_xResolution, _yResolution, 1));
		//
		if (!MainGUI._mainGUI._new) 
		{
			if (MainGUI._mainGUI._slideIn) 
			{
				//GUI.Box (_touchRect,"");
				if ((Input.GetMouseButtonDown (0)) && (_touchRect.Contains (Event.current.mousePosition))) 
				{
					RaycastHit hit = new RaycastHit ();
					Ray ray = _camera.ScreenPointToRay (Input.mousePosition);
					if (Physics.Raycast (ray, out hit)) 
					{
						if (_current.name != hit.transform.name) 
						{
							SetToBackground ();
							SetToForground (hit.transform.name);
						} 
						else 
						{
							_rotating = true;
						}
					}
				}
			}
		}
	}
}
