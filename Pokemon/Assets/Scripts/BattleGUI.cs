using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Threading;

public class BattleGUI : MonoBehaviour 
{
	//
	public Text _outputText;
	//
	public Text _name;
	public Text _class;
	public Text _level;
	public Text _health;
	public Text _defence;
	public Text _res;
	public Text _damage;
	public Text _mDamage;
	//
	public Text _lootName;
	//
	public int _selectIndex = 0;
	//
	public bool _givingExp = false;
	//
	public Button _attack;
	public Button _defend;
	public Button _special;
	public Button _back;
	public Button _issue;
	public Button _next;
	//
	public Slider _expBar;
	//
	public Stats _current;
	//
	public GameObject _battleStats;
	public GameObject _lootScreen;
	//Wait Handle for puasing the flow of logic until the user does somthing
	public EventWaitHandle _nextContract = new EventWaitHandle(false, EventResetMode.AutoReset);
	// Use this for initialization
	void Awake () 
	{

	}
	//
	public void Start()
	{
		_back.gameObject.SetActive(false);
		_issue.gameObject.SetActive(false);
		UpdateStats(0);
		setBattleText ("Your Turn");
	}
	// Update is called once per frame
	void Update () 
	{
	
	}
	//
	public void setBattleText(string x)
	{
		_outputText.text = x;
	}
	//
	public void AppendBattleText(string x)
	{
		_outputText.text += x;
	}
	//
	public void UpdateStats(int x)
	{
		_current = BattleSystem._battleSystem._partyStats [x];
		_name.text = "Name: "+_current._name;
		_class.text = "Class: "+_current._class;
		_level.text = "Level: "+_current._level;
		_health.text = "Health: "+_current._currentHealth;
		_defence.text = "Defence: "+_current._currentDefence;
		_res.text = "Resistance "+_current._currentResistance;
		_damage.text = "Damage: "+_current._currentDamage;
		_mDamage.text = "Magic Damage: "+_current._currentMDamage;
		//
		if (_current._spAttack != null) 
		{
			if (_current._spAttack._currentTime != _current._spAttack._rechargeTime) 
			{
				_special.gameObject.SetActive (false);
			} 
			else 
			{
				_special.gameObject.SetActive (true);
			}
		} 
		else 
		{
			_special.gameObject.SetActive (false);
		}		
		if(BattleSystem._battleSystem._amountOfContracts == 1)
		{
			_defend.gameObject.SetActive(false);
		}
		_selectIndex = x;
	}
	//
	public void Attack()
	{
		if (BattleSystem._battleSystem._userActions [_selectIndex]._type == "" && !BattleSystem._battleSystem._attacking) 
		{
			BattleSystem._battleSystem._userActions [_selectIndex].SetActionType("attack");
			Forward();
			BattleSystem._battleSystem.SetTarget(0, "enemy");
		}
	}
	//
	public void Defend()
	{
		if (BattleSystem._battleSystem._userActions [_selectIndex]._type == "" && !BattleSystem._battleSystem._attacking) 
		{
			BattleSystem._battleSystem._userActions [_selectIndex].SetActionType("defend");
			Forward();
			BattleSystem._battleSystem.SetTarget(0, "contract");
		}
	}
	//
	public void Special()
	{
		if (BattleSystem._battleSystem._userActions [_selectIndex]._type == "" && !BattleSystem._battleSystem._attacking) 
		{
			BattleSystem._battleSystem._userActions [_selectIndex].SetActionType("special");
			Forward();
			if(!_current._spAttack.Contains("group"))
			{
				if(_current._spAttack.Contains("damage"))
				{
					BattleSystem._battleSystem.SetTarget(0, "enemy");
				}
				else if(_current._spAttack.Contains ("heal"))
				{
					BattleSystem._battleSystem.SetTarget(0, "contract");
				}
			}
			else
			{
				IssueCommand();
			}
		}
	}
	//
	public void Forward()
	{
		if (BattleSystem._battleSystem._userActions [_selectIndex]._step == 2) 
		{
			_attack.gameObject.SetActive(false);
			_defend.gameObject.SetActive(false);
			_special.gameObject.SetActive(false);
			_back.gameObject.SetActive(true);
			_issue.gameObject.SetActive(true);
			BattleSystem._battleSystem._userActions [_selectIndex]._user = BattleSystem._battleSystem._contracts[_selectIndex];
		}
	}
	//
	public void Back()
	{
		_attack.gameObject.SetActive(true);
		_defend.gameObject.SetActive(true);
		_special.gameObject.SetActive(true);
		_back.gameObject.SetActive(false);
		_issue.gameObject.SetActive(false);
		BattleSystem._battleSystem._userActions [_selectIndex].Back ();
		BattleSystem._battleSystem.SetIndicator (_selectIndex);
	}
	//
	public void IssueCommand()
	{
		_attack.gameObject.SetActive(true);
		_defend.gameObject.SetActive(true);
		_special.gameObject.SetActive(true);
		_back.gameObject.SetActive(false);
		_issue.gameObject.SetActive(false);
		if (BattleSystem._battleSystem._userActions [_selectIndex]._type == "attack") 
		{
			BattleSystem._battleSystem._userActions [_selectIndex]._target = BattleSystem._battleSystem._enemys [BattleSystem._battleSystem._targetIndex];
		} 
		else if (BattleSystem._battleSystem._userActions [_selectIndex]._type == "defend") 
		{
			BattleSystem._battleSystem._userActions [_selectIndex]._target = BattleSystem._battleSystem._contracts [BattleSystem._battleSystem._targetIndex];
		} 
		else if(BattleSystem._battleSystem._userActions [_selectIndex]._type == "special")
		{
			if(_current._spAttack.Contains("damage"))
			{
				BattleSystem._battleSystem._userActions [_selectIndex]._target = BattleSystem._battleSystem._enemys [BattleSystem._battleSystem._targetIndex];
			}
			else
			{
				BattleSystem._battleSystem._userActions [_selectIndex]._target = BattleSystem._battleSystem._contracts [BattleSystem._battleSystem._targetIndex];
			}
		}
		BattleSystem._battleSystem._userActions [_selectIndex]._finished = true;
		BattleSystem._battleSystem.EndTurn ();
	}
	//
	public void LootScreen(Stats[] enemys, Stats[] contracts)
	{
		_battleStats.SetActive(false);
		_lootScreen.SetActive(true);
		int exp = 0;
		for(int i = 0; i < enemys.Length;i++)
		{
			exp += enemys[i]._expDrop*enemys[i]._level;
		}
		exp = exp / contracts.Length;
		for(int i = 0; i < contracts.Length; i++)
		{
			if(contracts[i]._name != "PlaceHolder")
			{
				StartCoroutine(CalculateExp(contracts[i], exp));
				_nextContract.WaitOne();
			}
		}
		_next.gameObject.SetActive(false);
		return;
	}
	//
	public IEnumerator CalculateExp(Stats contract, int exp)
	{
		_lootName.text = contract._name;
		_expBar.value = contract._exp;
		_givingExp = true;
		_next.gameObject.SetActive(false);
		for(int e = 0; e < exp; e++)
		{
			_expBar.value++;
			if(_expBar.value == 100)
			{
				contract.LevelUp();
				_expBar.value = 0;
			}
			yield return new WaitForSeconds(.1f);
		}
		_next.gameObject.SetActive(true);
		_givingExp = false;
	}
	//
	public void NextContract()
	{
		_nextContract.Set();
	}
}
