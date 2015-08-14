using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	//
	public string _name = "";
	//
	public int _level = 1;
	//
	public GameObject[] _party = new GameObject[4];
	//
	public Stats[] _partyStats = new Stats[4];
	//
	public static Player _player;
	public Player()
	{

	}
	//
	void Awake()
	{
		if(_player == null)
		{
			DontDestroyOnLoad(gameObject);
			_player = this;
		}
		else if(_player != this)
		{
			Destroy(this.gameObject);
		}
	}
	//
	public void Intiate()
	{
		_party = GameObject.FindGameObjectsWithTag ("Contract");
		for(int i = 0; i < 4; i++)
		{
			_partyStats[i] = _party[i].GetComponent<Stats>();
		}
	}
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public override string ToString()
	{
		string temp = "Player Name: "+_name+"\nPlayer Level: "+_level;
		for (int i = 0; i < _partyStats.Length; i++) 
		{
			temp += "\nContract "+(i+1)+": " + _partyStats[i]._name;
		}
		return temp;
	}

	public void AddContract(string member)
	{
		//
		GameObject newMember = (GameObject)Resources.Load ("Contracts/" + member);
		//
		bool emptySlot = false;
		//
		int index = 0;
		for(int i = 0; i < _partyStats.Length; i++)
		{
			if(_partyStats[i]._name == "PlaceHolder")
			{
				emptySlot = true;
				index = i;
				break;
			}
			else{}
		}
		if (emptySlot) 
		{
			Destroy(_party[index]);
			_party[index] = (GameObject)Instantiate(newMember);
			_partyStats[index] = _party[index].GetComponent<Stats>();
			_partyStats[index]._id = GetID();
			_party[index].transform.parent = this.transform;
			_party[index].name = _partyStats[index]._id;
		} 
		else {}
	}
	//
	public void ReplaceContract(GameObject replaced, GameObject added)
	{
		for(int i = 0; i < _party.Length; i++)
		{
			if(_party[i] == replaced)
			{
				Destroy(replaced);
				_party[i] = added;
				_partyStats[i] = added.GetComponent<Stats>();
				break;
			}
			else{}
		}
	}
	//
	public GameObject GetContract(int i)
	{
		return _party [i];
	}
	//
	public GameObject GetContract(string id)
	{
		GameObject temp = new GameObject();
		for(int i = 0; i < _party.Length; i++)
		{
			if(_partyStats[i]._id == id)
			{
				temp = _party[i];
				break;
			}
			else{}
		}
		return temp;
	}
	//
	public bool isContract(string id)
	{
		bool temp = false;
		for(int i = 0; i < _party.Length; i++)
		{
			if(_partyStats[i]._id == id)
			{
				temp = true;
				break;
			}
			else{}
		}
		return temp;
	}
	//
	public void ClearContracts()
	{
		//
		GameObject newMember = (GameObject)Resources.Load ("Contracts/PlaceHolder");
		for(int i = 0; i < _party.Length; i++)
		{
			Destroy(_party[i]);
			_party[i] = (GameObject)Instantiate(newMember);
			_partyStats[i] = _party[i].GetComponent<Stats>();
			_party[i].transform.parent = this.transform;
			_party[i].name = _partyStats[i]._name;
		}
	}
	//
	public string GetID()
	{
		System.Random _ran = new System.Random ();
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
	public int GetGreatestLevel()
	{
		int greatestLevel = 1;
		for(int i = 0; i < _partyStats.Length; i++)
		{
			if(_partyStats[i]._name != "PlaceHolder")
			{
				if(_partyStats[i]._level > greatestLevel)
				{
					greatestLevel = _partyStats[i]._level;
				}
			}
			else{}
		}
		return greatestLevel;
	}
	//
	public int GetLowestLevel()
	{
		int lowestLevel = 1;
		for(int i = 0; i < _partyStats.Length; i++)
		{
			if(_partyStats[i]._name != "PlaceHolder")
			{
				if(_partyStats[i]._level < lowestLevel)
				{
					lowestLevel = _partyStats[i]._level;
				}
			}
			else{}
		}
		return lowestLevel;
	}
	//
	public int GetMedianLevel()
	{
		int medianLevel = 1;
		int[] levels = new int[4];
		int amount = 0;
		for(int i = 0; i < _partyStats.Length; i++)
		{
			if(_partyStats[i]._name != "PlaceHolder")
			{
				levels[amount] = _partyStats[i]._level;
				amount++;
			}
			else{}
		}
		medianLevel = (levels [0] + levels [1] + levels [2] + levels [3]) / amount;
		return medianLevel;
	}
}
