using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour 
{
	//
	public bool _melee = false;
	public bool _mage = false;
	public bool _defender = false;
	public bool _healer = false;
	public bool _thief = false;
	//
	public Stats _target = new Stats ();
	public Stats _user;
	// Use this for initialization
	void Start () 
	{
		_user = this.GetComponent<Stats> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	//
	public string State()
	{
		string text = "No target found.";
		if (_melee) 
		{
			FindTarget ("Melee");
			text = _user._name+" attacked "+_target._name+" for "+_target.TakeDamage(_user)+" damage.";
		} 
		else if (_mage) 
		{
			FindTarget ("LowestResistance");
		} 
		else if (_defender) 
		{
			FindTarget ("AllyLowestDefence");
		} 
		else if (_healer) 
		{
			FindTarget ("AllyLowestHealth");
		} 
		else if (_thief) 
		{
			FindTarget ("LowestHealth");
		} 
		else 
		{

		}
		return text;
	}
	//
	public bool FindTarget(string variable)
	{
		bool found = false;
		switch (variable) 
		{
			case "Melee":
			{
				for(int i = 0; i < 4; i++)
				{
					if(BattleSystem._battleSystem._partyStats[i]._name != "Placeholder")
					{
						if(BattleSystem._battleSystem._partyStats[i]._type == "melee")
						{
							_target = BattleSystem._battleSystem._partyStats[i];
							found = true;
							break;
						}
						else{}
					}
				}
				break;
			}
			case "AllyLowestHealth":
			{
				for(int i = 0; i < 4; i++)
				{
					if(BattleSystem._battleSystem._partyStats[i]._name != "Placeholder")
					{
						if((BattleSystem._battleSystem._partyStats[i]._currentHealth <= (BattleSystem._battleSystem._partyStats[i]._health*.75f)) 
					   	&& (BattleSystem._battleSystem._partyStats[i]._currentHealth< _target._health))
						{
							_target = BattleSystem._battleSystem._partyStats[i];
							found = true;
						}
						else{}
					}
				}
				break;
			}
			case "LowestHealth":
			{
				for(int i = 0; i < 4; i++)
				{
					if(BattleSystem._battleSystem._partyStats[i]._name != "Placeholder")
					{
						if(BattleSystem._battleSystem._partyStats[i]._currentHealth < _target._health)
						{
							_target = BattleSystem._battleSystem._partyStats[i];
							found = true;
						}
						else if (BattleSystem._battleSystem._partyStats[i]._currentHealth == _target._health)
						{
							if(BattleSystem._battleSystem._partyStats[i]._defence <= _target._defence)
							{
								_target = BattleSystem._battleSystem._partyStats[i];
								found = true;
							}
							else{}
						}
						else{}
					}
				}
				break;
			}
			default:
			{
				break;
			}
		}
		return found;
	}
}
