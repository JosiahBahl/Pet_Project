using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stats : MonoBehaviour 
{
	//
	public string _name = "PlaceHolder";
	public string _class = "PlaceHolder";
	public string _id = "";
	public string _type = "";
	//
	public int _health = 1;
	public int _currentHealth = 1;
	//
	public int _defence = 1;
	public int _currentDefence = 1;
	public int _resistance = 1;
	public int _currentResistance = 1;
	//
	public int _damage = 1;
	public int _currentDamage = 1;
	public int _magicDamage = 1;
	public int _currentMDamage = 1;

	public int _level = 1;
	public int _exp = 0;
	public int _expDrop = 20;
	//
	public SpecialAttack _spAttack;
	//
	public int[] _statGrowth = new int[5];
	//
	public bool _alive = true;
	public bool _defended = false;
	//
	public Stats _defendStats;
	// Use this for initialization
	void Start () 
	{
		ResetCurrentStats ();
	}
	// Update is called once per frame
	void Update () 
	{
	
	}
	//
	public Stats()
	{
		_name = "Placeholder";
		_class = "Placeholder";
		_health = 1;
		_defence = 1;
		_resistance = 1;
		_damage = 1;
		_magicDamage = 1;
		_level = 1;
		_exp = 1;
		_spAttack = null;
		_id = "Placeholder";
		for(int i = 0; i < 5; i++)
		{
			_statGrowth[i] = 1;
		}
	}
	//Set Stats to the given stats
	public void setStats(Stats x)
	{
		_name = x._name;
		_class = x._class;
		_health = x._health;
		_defence = x._defence;
		_resistance = x._resistance;
		_damage = x._damage;
		_magicDamage = x._magicDamage;
		_level = x._level;
		_exp = x._exp;
		_spAttack = x._spAttack;
		_id = x._id;
		for(int i = 0; i < 5; i++)
		{
			_statGrowth[i] = x._statGrowth[i];
		}
	}
	//Check the contracts exp to see if you need to level it up.
	public void CheckLevel()
	{
		if(_exp > 100)
		{
			_exp -= 100;
			LevelUp();
		}
		else{}
	}
	//Level up the contract, Reset its stats back to the default.
	public void LevelUp()
	{
		_level++;
		_health += _statGrowth[0];
		_defence += _statGrowth[1];
		_resistance += _statGrowth[2];
		_damage += _statGrowth[3];
		_magicDamage += _statGrowth[4];
		ResetCurrentStats ();
	}
	//Level up the contract by a certain amount of levels.
	public void LevelUp(int level)
	{
		for (int i = 0; i < level; i++) 
		{
			_level++;
			_health += _statGrowth [0];
			_defence += _statGrowth [1];
			_resistance += _statGrowth [2];
			_damage += _statGrowth [3];
			_magicDamage += _statGrowth [4];
		}
		ResetCurrentStats ();
	}
	//Take damage from a special attack
	public int TakeDamage(SpecialAttack sp)
	{
		int damage = 0;
		if (sp.Contains ("melee")) 
		{
			damage = (sp._damage - _defence);
			if (damage > 0) 
			{
				_currentHealth -= damage;
			} 
			else 
			{
				damage = 0;
			}
		} 
		else if (sp.Contains ("magic")) 
		{
			damage = (sp._damage - _resistance);
			_currentHealth -= damage;
		}
		return damage;
	}
	//Take damage from a normal attack
	public int TakeDamage(Stats x)
	{
		int damage = 0;
		switch (x._type) 
		{
		case "melee":
			damage = (x._damage-_defence);
			if(damage > 0)
			{
				_currentHealth -= damage;
			}
			else
			{
				damage = 0;
			}
			break;
		case "magic":
			damage = (x._magicDamage-_resistance);
			_currentHealth -= damage;
			break;
		default:
			break;
		}
		return damage;
	}
	//Function for when a contract is being defended by another.
	public void Shielded(Stats x)
	{
		_defended = true;
		_defendStats = x;
		//------------------
		int extraDefence = (int)Mathf.Ceil((.5f * x._defence));
		int extraResistance = (int)Mathf.Ceil((.5f * x._resistance));
		//------------------
		_currentDefence += extraDefence;
		_currentResistance += extraResistance;
	}
	//Use the contracts special, returns a string for output text.
	public string UseSpecial(Stats[] targets)
	{
		_spAttack._currentTime = 0;
		string returnString = "";
		if(!_spAttack.Contains("group"))
		{
			if(_spAttack.Contains("damage"))
			{
				returnString = _name + " used " + _spAttack._name + " to attack " + targets[0]._name + " for " + targets[0].TakeDamage(_spAttack) + " damage.";
			}
			else if(_spAttack.Contains("heal"))
			{
				returnString = _name + " used " + _spAttack._name + " to heal " + targets[0]._name + " for " + targets[0].TakeDamage(_spAttack) + " health.";
			}
		}
		else
		{
			if(_spAttack.Contains("damage"))
			{
				for(int i = 0; i < targets.Length; i++)
				{
					returnString += _name + " used " + _spAttack._name + " to attack " + targets[i]._name + " for " + targets[i].TakeDamage(_spAttack) + " damage.";
				}
			}
			else if(_spAttack.Contains("heal"))
			{
				for(int i = 0; i < targets.Length; i++)
				{
					returnString += _name + " used " + _spAttack._name + " to heal " + targets[i]._name + " for " + targets[i].TakeDamage(_spAttack) + " health.";
				}
			}
		}
		return returnString;
	}
	//Function for when the health of the contract is below or equal to 0
	public void Feint()
	{
		this.gameObject.SetActive (false);
		_alive = false;
	}
	//Reset stats to the max amount.
	public void ResetCurrentStats()
	{
		_currentHealth = _health;
		_currentDefence = _defence;
		_currentResistance = _resistance;
		_currentDamage = _damage;
		_currentMDamage = _magicDamage;
	}
	//Reset stat based on string
	public void ResetStat(string stat)
	{
		switch (stat) 
		{
			case "health":
			{
				_currentHealth = _health;
				break;
			}
			case "defence":
			{
				_currentDefence = _defence;
				break;
			}
			case "resistance":
			{
				_currentResistance = _resistance;
				break;
			}
			default:
			{
				break;
			}
		}
	}
	//Find a special attack thats name is string param, create it, set it to the contract, set the current special to the new one.
	public void SetSpecial(string name)
	{
		if(_spAttack != null)
		{
			Destroy(_spAttack.gameObject);
			_spAttack = null;
		}
		Instantiate(Resources.Load("Specials/" + name));
		GameObject special = GameObject.Find (name+"(Clone)");
		special.transform.parent = this.gameObject.transform;
		_spAttack = special.GetComponent<SpecialAttack> ();
		special.name = _spAttack._name;
	}
}
