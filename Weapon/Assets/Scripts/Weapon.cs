using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour 
{
	//
	public float _comboTime = .5f;
	public float _comboTimer = 0f;
	public float _animationTimer = 0f;
	public float _animationTime = 0f;
	//
	public int _combo = 0;
	public int _damage = 3;
	//
	public int[] _staminaUsage = new int[3];
	//
	private PlayerData _data;
	//
	public bool _attacking = false;
	public bool _contDown = false;
	// Use this for initialization
	void Start () 
	{
		_data = GameObject.Find("Player").GetComponent<PlayerData>();
	}
	// Update is called once per frame
	void Update () 
	{
		//
		if ((Time.timeSinceLevelLoad >= _comboTimer) && _contDown) 
		{
			_combo = 0;
			//
			_data.StartStaminaRegen();
            _data.GetAnimator().SetInteger("Combo", _combo);
            _data.Attacking = false;
			//
			_contDown = false;
		}
		//
		if((Time.timeSinceLevelLoad >= _animationTimer) && _attacking)
		{
			_comboTimer = Time.timeSinceLevelLoad + _comboTime;
			//
			_attacking = false;
			_contDown = true;
		}
		else{}
	}
	//
	public void Attack()
	{
		if((_combo > 0 && _data._stamina >= _staminaUsage[_combo-1]) && (Time.timeSinceLevelLoad >= _animationTimer))
		{
			_data.StopStaminaRegen();
	        _data.Attacking = true;
			//
			_contDown = false;
			_attacking = true;
			//
			if (_combo < 3) 
			{
				_combo++;
	            _data.GetAnimator().SetInteger("Combo", _combo);
				//
				_animationTime = _data.GetAnimator().GetCurrentAnimatorStateInfo(0).length;
				_animationTimer = _animationTime+Time.timeSinceLevelLoad;
			} 
			else 
			{
				_combo = 1;
	            _data.GetAnimator().SetInteger("Combo", _combo);
				//
				_animationTime = _data.GetAnimator().GetCurrentAnimatorStateInfo(0).length;
				_animationTimer = _animationTime+Time.timeSinceLevelLoad;
			}
			_data._stamina -= _staminaUsage[_combo-1];
		}
		else if((_combo == 0 && _data._stamina >= _staminaUsage[_combo]) && (Time.timeSinceLevelLoad >= _animationTimer))
		{
			_data.StopStaminaRegen();
			_data.Attacking = true;
			//
			_contDown = false;
			_attacking = true;
			//
			_combo++;
			_data.GetAnimator().SetInteger("Combo", _combo);
			//
			_animationTime = _data.GetAnimator().GetCurrentAnimatorStateInfo(0).length;
			_animationTimer = _animationTime+Time.timeSinceLevelLoad;
			//
			_data._stamina -= _staminaUsage[_combo-1];
		}
		else{}
	}
}
