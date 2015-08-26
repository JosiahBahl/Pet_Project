using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Threading;

public class BattleGUI : MonoBehaviour 
{
	//Output text for displaying information
	public Text _outputText;
	//Current contract stats
	public Text _name;
	public Text _class;
	public Text _level;
	public Text _health;
	public Text _defence;
	public Text _res;
	public Text _damage;
	public Text _mDamage;
	//Name of the contract in the loot menue
	public Text _lootName;
	//
	public int _selectIndex = 0;
	private int _expToGive = 0;
	//True when giving exp to a contract
	public bool _givingExp = false;
	//Controls for the battle
	public Button _attack;
	public Button _defend;
	public Button _special;
	public Button _back;
	public Button _issue;
	public Button _next;
	//Exp bar to show effect
	public Slider _expBar;
	//Current contract stats
	public Stats _current;
	//
	public GameObject _battleStats;
	public GameObject _lootScreen;
	// Use this for initialization
	void Awake () {}
	//
	public void Start()
	{
		//Turn off GUI elements
		_back.gameObject.SetActive(false);
		_issue.gameObject.SetActive(false);
		_lootScreen.SetActive(false);
		//Set stats to first contract
		UpdateStats(0);
		//Set Battle text
		setBattleText ("Your Turn");
	}
	// Update is called once per frame
	void Update () {}
	//Set battle text, overwrites current text
	public void setBattleText(string x)
	{
		_outputText.text = x;
	}
	//Appends battle text to current text
	public void AppendBattleText(string x)
	{
		_outputText.text += x;
	}
	//Update the current contract
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
		//if we are not at the loot screen, don't want to commands to show up.
		if(!BattleSystem._endingBattle)
		{
			EnableCommands();
		}
		_selectIndex = x;
	}
	//Function call for Attack button
	public void Attack()
	{
		//If the contract has yet to do and action and were not currently attacking the nemey, set the contract action.
		if (BattleSystem._battleSystem._userActions [_selectIndex]._type == "" && !BattleSystem._battleSystem._attacking) 
		{
			BattleSystem._battleSystem._userActions [_selectIndex].SetActionType("attack");
			Forward();
			BattleSystem._battleSystem.SetTarget(0, "enemy");
		}
	}
	//Function call for Defend button
	public void Defend()
	{
		//If the contract has yet to do and action and were not currently attacking the nemey, set the contract action.
		if (BattleSystem._battleSystem._userActions [_selectIndex]._type == "" && !BattleSystem._battleSystem._attacking) 
		{
			BattleSystem._battleSystem._userActions [_selectIndex].SetActionType("defend");
			Forward();
			BattleSystem._battleSystem.SetTarget(0, "contract");
		}
	}
	//Function call for the Special button
	public void Special()
	{
		//If the contract has yet to do and action and were not currently attacking the nemey, set the contract action.
		if (BattleSystem._battleSystem._userActions [_selectIndex]._type == "" && !BattleSystem._battleSystem._attacking) 
		{
			BattleSystem._battleSystem._userActions [_selectIndex].SetActionType("special");
			Forward();
			//Check to see if the special attack is a heal or damage based attack, set the indicator arrow based on that.
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
	//Function call for Forward Button.
	public void Forward()
	{
		//If we have attacked, defended or used a special
		if (BattleSystem._battleSystem._userActions [_selectIndex]._step == 2) 
		{
			DisableCommands();
			_back.gameObject.SetActive(true);
			_issue.gameObject.SetActive(true);
			BattleSystem._battleSystem._userActions [_selectIndex]._user = BattleSystem._battleSystem._contracts[_selectIndex];
		}
	}
	//Function call for Back Button
	public void Back()
	{
		EnableCommands();
		_back.gameObject.SetActive(false);
		_issue.gameObject.SetActive(false);
		BattleSystem._battleSystem._userActions [_selectIndex].Back ();
		BattleSystem._battleSystem.SetIndicator (_selectIndex);
	}
	//End the turn for the current contract
	public void IssueCommand()
	{
		EnableCommands();
		_back.gameObject.SetActive(false);
		_issue.gameObject.SetActive(false);
		//If we are attacking set the target to the enemy we selected
		if (BattleSystem._battleSystem._userActions [_selectIndex]._type == "attack") 
		{
			BattleSystem._battleSystem._userActions [_selectIndex]._target = BattleSystem._battleSystem._enemys [BattleSystem._battleSystem._targetIndex];
		}
		//If we are defending set the target to the contract we selected
		else if (BattleSystem._battleSystem._userActions [_selectIndex]._type == "defend") 
		{
			BattleSystem._battleSystem._userActions [_selectIndex]._target = BattleSystem._battleSystem._contracts [BattleSystem._battleSystem._targetIndex];
		} 
		//If we used a special decide if it is a heal or damage special, then set the target based on that.
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
	//Enables the loot screen and start distributing exp.
	public void LootScreen(Stats[] enemys, Stats[] contracts)
	{
		DisableCommands();
		_lootScreen.SetActive(true);
		_next.gameObject.SetActive(false);
		_expToGive = 0;
		_selectIndex = 0;
		//Calculate exp
		for(int i = 0; i < BattleSystem._battleSystem._amountOfEnemys; i++)
		{
			_expToGive += enemys[i]._expDrop*enemys[i]._level;
		}
		_expToGive = _expToGive / BattleSystem._battleSystem._amountOfContracts;
		NextContract();
	}
	//Goes to the next contract can gives it exp
	public void NextContract()
	{
		bool _watcher = true;
		_next.gameObject.SetActive(true);
		while(_watcher)
		{
			if(BattleSystem._battleSystem._partyStats[_selectIndex]._name != "PlaceHolder")
			{
				UpdateStats(_selectIndex);
				StartCoroutine(CalculateExp(BattleSystem._battleSystem._partyStats[_selectIndex]));
				_watcher = false;
				_selectIndex++;
				break;
			}
			else if(_selectIndex < 3)
			{
				_selectIndex++;
			}
			else
			{
				BattleSystem._battleSystem.EndBattle();
				break;
			}
		}
	}
	//Exp bar animation and gives exp, levels up contract too.
	public IEnumerator CalculateExp(Stats contract)
	{
		_lootName.text = contract._name;
		_expBar.value = contract._exp;
		_givingExp = true;
		for(int e = 0; e < _expToGive; e++)
		{
			_expBar.value++;
			if(_expBar.value == 100)
			{
				contract.LevelUp();
				contract.ResetCurrentStats();
				_expBar.value = 0;
			}
			yield return new WaitForSeconds(.1f);
		}
		contract._exp = (int)_expBar.value;
		_givingExp = false;
	}
	//Disables the command buttons
	public void DisableCommands()
	{
		_attack.gameObject.SetActive(false);
		_defend.gameObject.SetActive(false);
		_special.gameObject.SetActive(false);
	}
	/*
	 * Enables the command buttons, makes sure not to enable Special button if there is no special
	 * ,makes sure to not enable the defend when there is only one contract.
	 */
	public void EnableCommands()
	{
		_attack.gameObject.SetActive(true);
		if(_current._spAttack != null)
		{
			if(_current._spAttack._currentTime != _current._spAttack._rechargeTime)
			{
				_special.gameObject.SetActive(false);
			}
			else
			{
				_special.gameObject.SetActive(true);
			}
		}
		else
		{
			_special.gameObject.SetActive(false);
		}		
		if(BattleSystem._battleSystem._amountOfContracts == 1)
		{
			_defend.gameObject.SetActive(false);
		}
	}
}
