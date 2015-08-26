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
	//Easier access to stats
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
	// Use this for initialization
	public void Awake()
	{
		_battleGUI = this.GetComponent<BattleGUI> ();
		_battleSystem = this;
	}
	//
	void Start () 
	{
		//Grab contract stats and contract game objects
		_partyStats = Player._player._partyStats;
		_contracts = Player._player._party;
		//Position contracts, count how many real contracts we have
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
		//--------------------------------------------------------------
		//Put enemy spawn code here, GPS and such.
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
		//--------------------------------------------------------------
		//Get all enemys, get their AI, get their Stats
		_enemys = GameObject.FindGameObjectsWithTag ("Enemy");
		_enemyAI = _enemyStatHolder.GetComponents<EnemyAI>();
		_enemyStats = _enemyStatHolder.GetComponents<Stats>();
		//Find the greatest level of the player and the median level.
		int greatestLevel = Player._player.GetGreatestLevel ();
		int median = Player._player.GetMedianLevel ();
		//Set enemy IDs, spawns and get stats
		for (int i = 0; i < _amountOfEnemys; i++) 
		{
			_enemys[i].name = GetID(i);
			_enemys[i].transform.position = _enemySpawn[i].transform.position;
			_enemyStats[i] = _enemys[i].GetComponent<Stats>();
			_enemyStats[i]._id = _enemys[i].name;
			_enemyAI[i] = _enemys[i].GetComponent<EnemyAI>();
			//Pick a random level between the median and greatest level.
			_enemyStats[i].LevelUp(Random.Range(median, greatestLevel));
		}
		//Create array for contract actions.
		_userActions = new Action[_amountOfContracts];
		for(int i = 0; i < _userActions.Length; i++)
		{
			_userActions[i] = new Action();
		}
		//Set camera position
		ControlSystem._control._camera.transform.position = _cameraSpawn.transform.position;
		ControlSystem._control._camera.transform.rotation = _cameraSpawn.transform.rotation;
		//
		_xResolution = Screen.width/1024f;
		_yResolution = Screen.height/768f;
		//Set stage for time of day.
		ControlSystem._control.SetStage();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//When the battle ends, start the loot screen.
		if(_endingBattle && !_stopUpdate)
		{
			_stopUpdate = true;
			_battleGUI.LootScreen(_enemyStats, _partyStats);
		}
	}
	//
	void OnGUI()
	{
		//Scaleing GUI.
		GUI.matrix = Matrix4x4.TRS(new Vector3(1,1,1), Quaternion.identity, new Vector3(_xResolution, _yResolution, 1));
		if(MainGUI._mainGUI._slideIn && !_attacking && !_endingBattle)
		{
			//Raycast for selecting enemies and contracts
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
	//Set the indicator to a contract based on array index	
	public void SetIndicator(int i)
	{
		if(!_userActions[i]._finished)
		{
			_indicator.renderer.material.mainTexture = _blueArrow;
			_indicator.transform.position = new Vector3(_contractSpawns[i].transform.position.x, 2.5f, _contractSpawns[i].transform.position.z);
			_selectIndex = i;
			_battleGUI.UpdateStats(i);
		}
		else{}
	}
	//Set the indicator based on game obejct
	public void SetIndicator(GameObject x)
	{
		for (int i = 0; i < 4; i++) 
		{
			if(x.name == _partyStats[i]._id)
			{
				//Make sure the contract you selected has not already taken its turn.
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
	//Set the indicator image, then set it to either the enemy or contract based on array index.
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
			//Make sure it does not target the current contract your on
			if(x != _selectIndex)
			{
				_indicator.renderer.material.mainTexture = _redArrow;
				_indicator.transform.position = new Vector3 (_contractSpawns[x].transform.position.x, 2.5f, _contractSpawns[x].transform.position.z);
				_targetIndex = x;
			}
			else
			{
				//If it is targeting the current contract your on, move it to another.
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
	//Set target based on game object
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
				//Make sure they cannot select the current contract they are on.
				if((x.name == _partyStats[i]._id) && (i != _selectIndex))
				{
					_targetIndex = i;
					_indicator.renderer.material.mainTexture = _redArrow;
					_indicator.transform.position = new Vector3(x.transform.position.x, 2.5f, x.transform.position.z);
				}
			}
		}
	}
	//End the current contracts turn, if  it is the last contract that finishes, start the attacking sequence.
	public void EndTurn ()
	{
		_turn++;
		_targetIndex = 0;
		//If it is your last turn, start attacking sequence.
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
	//Attacking sequence
	public IEnumerator Orders()
	{
		//Target of attack/heal
		Stats target;
		//Targets of group attack/heal
		Stats[] targets = _enemyStats;
		//current contract
		Stats user;
		for(int i = 0; i < _amountOfContracts; i++)
		{
			//Based on contracts action type
			switch(_userActions[i]._type)
			{
				//Attack the target with and attack, check to see if the target has fallen.
				case "attack":
					target = getEnemyStats(_userActions[i]._target);
					user = getContractStats(_userActions[i]._user);
					_battleGUI.setBattleText(user._name+" attacked "+target._name+" for "+target.TakeDamage(user)+" damage.");
					yield return new WaitForSeconds(2f);
					if(target._currentHealth <= 0)
					{
						target.Feint();
						_battleGUI.AppendBattleText(". "+target._name+" has fallen.");
					}
					break;
				//Defend the target
				case "defend":
					target = getContractStats(_userActions[i]._target);
					user = getContractStats(_userActions[i]._user);
					target.Shielded(user);
					_battleGUI.setBattleText(user._name+" defended "+target._name+".");
					break;
				//Set the user and target based on if the special is group or single attack/heal.
				case "special":
					user = getContractStats(_userActions[i]._user);
					if(!user._spAttack._name.Contains("group"))
					{
						if(!user._spAttack._name.Contains("heal"))
						{
							targets[0] = getEnemyStats(_userActions[i]._target);
						}
						else
						{
							targets[0] = getContractStats(_userActions[i]._target);
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
							_battleGUI.AppendBattleText(targets[x]._name+" has fallen.");
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
	//Get the enemy stats based on game object
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
	//Get contract stats based on game object
	public Stats getContractStats(GameObject x)
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
	//Get a random string to use as an enemy ID
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
	//Check to see if all the enemys are dead.
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
	//Check to see if all your contracts are dead.
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
	//End the battle, load main meneu.
	public void EndBattle()
	{
		_stopUpdate = false;
		_endingBattle = false;
		Application.LoadLevel(2);
	}
}
