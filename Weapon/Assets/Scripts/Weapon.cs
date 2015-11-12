using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour 
{
	//
	public float _comboTime = .5f;
	public float _comboTimer = 0f;
	//
	public int _combo = 0;
	public int _damage = 3;
	//
	public int[] _staminaUsage = new int[3];
	//
	private PlayerData _data;
	// Use this for initialization
	void Start () 
	{
		_data = GameObject.Find("Player").GetComponent<PlayerData>();
	}
	// Update is called once per frame
	void Update () 
	{
		//
		if ((Time.timeSinceLevelLoad >= _comboTimer) && (_combo != 0)) 
		{
			_combo = 0;
			_data.StartStaminaRegen();
		}
	}
	//
	public void Attack()
	{
		_data.StopStaminaRegen();
		if (_combo < 3) 
		{
			_combo++;
		} 
		else 
		{
			_combo = 1;
		}
		if(_data._stamina >= _staminaUsage[_combo-1])
		{
			_comboTimer = Time.timeSinceLevelLoad + _comboTime;
			_data._stamina -= _staminaUsage[_combo-1];
		}
		else
		{
			_combo = 0;
			_data.StartStaminaRegen();
		}
	}
}
