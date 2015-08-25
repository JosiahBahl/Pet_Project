using UnityEngine;
using System.Collections;
using System.Threading;

public class BattleSystem : MonoBehaviour 
{
	//
	public GameObject _indicator;
	public GameObject _cameraSpawn;
	public GameObject _contractStatHolder;
	public GameObject _enemyStatHolder;
	//
	public Stats[] _partyStats;
	public Stats[] _enemyStats;
	public EnemyAI[] _enemyAI;
	//
	public GameObject[] _contractSpawns = new GameObject[4];
	public GameObject[] _enemySpawn = new GameObject[4];
	public GameObject[] _enemys;
	public GameObject[] _contracts;
	//
	public static BattleSystem _battleSystem;
	//
	private BattleGUI _battleGUI;
	//
	public Action[] _userActions;
	//
	private Rect _touchRect = new Rect (0, 25, 1024, 518);
	//
	public float _xResolution;
	public float _yResolution;
	//
	public int _selectIndex = 0;
	public int _targetIndex = 0;
	public int _turn = 0;
	public int _amountOfContracts = 0;
	public int _amountOfEnemys = 0;
	//
	public Texture _blueArrow;
	public Texture _redArrow;
	//
	public bool _attacking = false;
	public bool _stopUpdate = false;
	public bool _won = false;
	public static bool _endingBattle = false;
	//
	public Thread _lootThread;
	// Use this for initialization
	public void Awake()
	{
		_battleGUI = this.GetComponent<BattleGUI> ();
		_battleSystem = this;
	}
	//
	void Start () 
	{
		//Set player contract spawn
		_partyStats = Player._player._partyStats;
		_contracts = Player._player._party;
		for(int i = 0; i < _partyStats.Length; i++)
		{
			if(_partyStats[i]._name != "PlaceHolder")
			{
				_contracts[_amountOfContracts].transform.position = _contractSpawns[_amountOfContracts].transform.position;
				_contracts[_amountOfContracts].transform.rotation = new Quaternion(0,0,0,0);
				_amountOfContracts++;
			}
			else{}
		}
		//
		GameObject _enemy = (GameObject)Resources.Load ("Enemys/Enemy");
		int amount = Random.Range (1, _amountOfContracts);
		//Instatiate Enemys
        for (int i = 0; i < amount; i++) 
  		{
			Instantiate(_enemy);
			_amountOfEnemys++;
			_enemyStatHolder.AddComponent<Stats>();
			_enemyStatHolder.AddComponent<EnemyAI>();
		}
		//Get all enemys
		_enemys = GameObject.FindGameObjectsWithTag ("Enemy");
		_enemyAI = _enemyStatHolder.GetComponents<EnemyAI>();
		_enemyStats = _enemyStatHolder.GetComponents<Stats>();
		int greatestLevel = Player._player.GetGreatestLevel ();
		int median = Player._player.GetMedianLevel ();
		//Set enemy IDs, spawns and get stats
		for (int i = 0; i < _amountOfEnemys; i++) 
		{
			_enemys[i].name = GetID(i);
			_enemys[i].transform.position = _enemySpawn[i].transform.position;
			_enemyStats[i] = _enemys[i].GetComponent<Stats>();
			_enemyStats[i]._id = _enemys[i].name;
			_enemyStats[i]._name = _enemyStats[i]._name+"_"+(1+i);
			_enemyAI[i] = _enemys[i].GetComponent<EnemyAI>();
			_enemyStats[i].LevelUp(Random.Range(median, greatestLevel));

		}
		_userActions = new Action[_amountOfContracts];
		for(int i = 0; i < _userActions.Length; i++)
		{
			_userActions[i] = new Action();
		}
		//
		ControlSystem._control._camera.transform.position = _cameraSpawn.transform.position;
		ControlSystem._control._camera.transform.rotation = _cameraSpawn.transform.rotation;
		//
		_xResolution = Screen.width/1024f;
		_yResolution = Screen.height/768f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(_endingBattle && !_stopUpdate)
		{
			_stopUpdate = true;
			_battleGUI.LootScreen(_enemyStats, _partyStats);
		}
	}
	//
	void OnGUI()
	{
		GUI.matrix = Matrix4x4.TRS(new Vector3(1,1,1), Quaternion.identity, new Vector3(_xResolution, _yResolution, 1));
		if(MainGUI._mainGUI._slideIn && !_attacking && !_endingBattle)
		{
			if((Input.GetMouseButtonDown(0)) && (_touchRect.Contains(Event.current.mousePosition))) 
			{
				RaycastHit hit = new RaycastHit();
				Ray ray = ControlSystem._control._camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
				
				if(Physics.Raycast(ray, out hit))
				{
					if(_userActions[_selectIndex]._type == "")
					{
						if(hit.transform.tag == "Contract")
						{
							SetIndicator(hit.transform.gameObject);
						}
					}
					else if(_userActions[_selectIndex]._type == "attack")
					{
						if(hit.transform.tag == "Enemy")
						{
							SetTarget(hit.transform.gameObject, "enemy");
						}
					}
					else if(_userActions[_selectIndex]._type == "defend")
					{
						if(hit.transform.tag == "Contract")
						{
							SetTarget(hit.transform.gameObject, "contract");
						}
					}
					else if(_userActions[_selectIndex]._type == "special")
					{
						if(hit.transform.tag == "Enemy" && _partyStats[_selectIndex]._spAttack.Contains("damage"))
						{
							SetTarget(hit.transform.gameObject, "enemy");
						}
						else if(hit.transform.tag == "Contract" && _partyStats[_selectIndex]._spAttack.Contains("heal"))
						{
							SetTarget(hit.transform.gameObject, "contract");
						}
						else{}
					}
				}
			}
		}
	}
	public void SetIndicator(int i)
	{
		_indicator.renderer.material.mainTexture = _blueArrow;
		_indicator.transform.position = new Vector3(_contractSpawns[i].transform.position.x, 2.5f, _contractSpawns[i].transform.position.z);
		_selectIndex = i;
		_battleGUI.UpdateStats(i);
	}
	//
	public void SetIndicator(GameObject x)
	{
		for (int i = 0; i < 4; i++) 
		{
			if(x.name == _partyStats[i]._id)
			{
				if(!_userActions[i]._finished)
				{
					_indicator.renderer.material.mainTexture = _blueArrow;
					_indicator.transform.position = new Vector3(x.transform.position.x, 2.5f, x.transform.position.z);
					_selectIndex = i;
					_battleGUI.UpdateStats(i);
				}
				else{}
			}
			else{}
		}
	}
	public void SetTarget(int x, string type)
	{
		if (type == "enemy") 
		{
			_indicator.renderer.material.mainTexture = _redArrow;
			_indicator.transform.position = new Vector3 (_enemys[x].transform.position.x, 2.5f, _enemys[x].transform.position.z);
			_targetIndex = x;
		} 
		else if (type == "contract") 
		{
			if(x != _selectIndex)
			{
				_indicator.renderer.material.mainTexture = _redArrow;
				_indicator.transform.position = new Vector3 (_contractSpawns[x].transform.position.x, 2.5f, _contractSpawns[x].transform.position.z);
				_targetIndex = x;
			}
			else
			{
				if((x+1) <= _amountOfContracts)
				{
					x++;
				}
				else
				{
					x = 0;
				}
				_indicator.renderer.material.mainTexture = _redArrow;
				_indicator.transform.position = new Vector3 (_contractSpawns[x].transform.position.x, 2.5f, _contractSpawns[x].transform.position.z);
				_targetIndex = x;
			}
		}
	}
	//
	public void SetTarget(GameObject x, string type)
	{
		if (type == "enemy") 
		{
			for(int i = 0; i < _enemys.Length; i++)
			{
				if(x.name == _enemys[i].name)
				{
					_targetIndex = i;
					_indicator.renderer.material.mainTexture = _redArrow;
					_indicator.transform.position = new Vector3(x.transform.position.x, 2.5f, x.transform.position.z);
				}
			}
		}
		else if (type == "contract") 
		{
			for(int i = 0; i < _partyStats.Length; i++)
			{
				if((x.name == _partyStats[i]._id) && (i != _selectIndex))
				{
					_targetIndex = i;
					_indicator.renderer.material.mainTexture = _redArrow;
					_indicator.transform.position = new Vector3(x.transform.position.x, 2.5f, x.transform.position.z);
				}
			}
		}
	}
	//
	public void EndTurn ()
	{
		_turn++;
		_targetIndex = 0;
		if (_turn == _amountOfContracts) 
		{
			_attacking = true;
			_indicator.SetActive(false);
			_selectIndex = 0;
			_turn = 0;
			_battleGUI.DisableCommands();
			StartCoroutine(Orders());
		} 
		else 
		{
			for(int i = 0; i < _amountOfContracts; i++)
			{
				if(!_userActions[i]._finished)
				{
					SetIndicator(i);
				}
			}
		}
	}
	//
	public IEnumerator Orders()
	{
		Stats target;
		Stats[] targets = _enemyStats;
		Stats user;
		for(int i = 0; i < _amountOfContracts; i++)
		{
			switch(_userActions[i]._type)
			{
				case "attack":
					target = getEnemyStats(_userActions[i]._target);
					user = getPlayerStats(_userActions[i]._user);
					_battleGUI.setBattleText(user._name+" attacked "+target._name+" for "+target.TakeDamage(user)+" damage");
					yield return new WaitForSeconds(2f);
					if(target._currentHealth <= 0)
					{
						target.Feint();
						_battleGUI.setBattleText(target._name+" has fallen.");
					}
					break;
				case "defend":
					target = getPlayerStats(_userActions[i]._target);
					user = getPlayerStats(_userActions[i]._user);
					target.Shielded(user);
					_battleGUI.setBattleText(user._name+" defended "+target._name);
					break;
				case "special":
					user = getPlayerStats(_userActions[i]._user);
					if(!user._spAttack._name.Contains("group"))
					{
						if(!user._spAttack._name.Contains("heal"))
						{
							targets[0] = getEnemyStats(_userActions[i]._target);
						}
						else
						{
							targets[0] = getPlayerStats(_userActions[i]._target);
						}
					}
					else
					{
						if(user._spAttack._name.Contains("heal"))
						{
							for(int x = 0; x < _amountOfEnemys; x++)
							{
								targets[x] = _partyStats[x];
							}
						}
					}
					_battleGUI.setBattleText(user.UseSpecial(targets));
					yield return new WaitForSeconds(2f);
					for(int x = 0; x < targets.Length; x++)
					{
						if(targets[x]._currentHealth <= 0)
						{
							targets[x].Feint();
							_battleGUI.setBattleText(". "+targets[x]._name+" has fallen.");
							yield return new WaitForSeconds(2f);
						}
					}
					break;
				default:
					break;
			}
			if(EnemysAllDead())
			{
				break;
			}
		}
		if (!EnemysAllDead ()) 
		{
			_battleGUI.setBattleText ("Enemy turn.");
			yield return new WaitForSeconds (2f);
			for (int i = 0; i < _amountOfEnemys; i++) 
			{
				if(_enemyStats[i]._alive)
				{
					_battleGUI.setBattleText (_enemyAI [i].State ());
					yield return new WaitForSeconds (2f);
					if(ContractsAllDead())
					{
						break;
					}
				}
				else{}
			}
			_attacking = false;
			_battleGUI.setBattleText ("Your turn.");
			yield return null;
		} 
		else 
		{
			_attacking = false;
			_battleGUI.setBattleText("All enemys defeated, you have won the battle.");
			_endingBattle = true;
			_won = true;
			_indicator.gameObject.SetActive(false);
			StopCoroutine(Orders());
		}
		if (ContractsAllDead ()) 
		{
			_battleGUI.setBattleText("All your contracts have fallen in battle, you have lost.");
			_endingBattle = true;
			_indicator.gameObject.SetActive(false);
			StopCoroutine(Orders());
		}
		for (int i = 0; i < _userActions.Length; i++) 
		{
			_partyStats[i]._defended = false;
			_partyStats[i].ResetStat("defence");
			_partyStats[i].ResetStat("resistance");
			if(_partyStats[i]._spAttack != null)
			{
				if(_partyStats[i]._spAttack._currentTime < _partyStats[i]._spAttack._rechargeTime)
				{
					_partyStats[i]._spAttack._currentTime++;
				}
			}
			_userActions [i] = new Action ();
		}
		_indicator.SetActive (true);
		SetIndicator (_contracts [0]);
	}
	//
	public Stats getEnemyStats(GameObject x)
	{
		Stats temp = null;
		for(int i = 0; i < 4; i++)
		{
			if(x.name == _enemyStats[i]._id)
			{
				temp = _enemyStats[i];
				break;
			}
		}
		return temp;
	}
	//
	public Stats getPlayerStats(GameObject x)
	{
		Stats temp = null;
		for(int i = 0; i < 4; i++)
		{
			if(x.name == _partyStats[i]._id)
			{
				temp = _partyStats[i];
				break;
			}
		}
		return temp;
	}
	//
	public string GetID(int x)
	{
		System.Random _ran = new System.Random (x);
		byte[] _array = new byte[10];
		string _return = "";
		_ran.NextBytes (_array);
		for (int i = 0; i < _array.Length; i++) 
		{
			_return += _array[i];
		}
		return _return;
	}
	//
	public bool EnemysAllDead()
	{
		int cont = 0;
		for (int i = 0; i < _amountOfEnemys; i++) 
		{
			if(_enemyStats[i]._alive)
			{
				break;
			}
			else
			{
				cont++;
			}
		}
		return (cont==_amountOfEnemys)?true:false;
	}
	//
	public bool ContractsAllDead()
	{
		int cont = 0;
		for (int i = 0; i < _amountOfContracts; i++) 
		{
			if(_partyStats[i]._alive)
			{
				break;
			}
			else
			{
				cont++;
			}
		}
		return (cont==_amountOfContracts)?true:false;
	}
	//
	public void EndBattle()
	{
		_stopUpdate = false;
		_endingBattle = false;
		Application.LoadLevel(2);
	}
}
