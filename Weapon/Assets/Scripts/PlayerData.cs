using UnityEngine;
using System.Collections;

public class PlayerData : MonoBehaviour 
{
	//
	public bool LockMovement = false;
	public bool Moving = false;
	public bool Grounded = false;
	public bool Blocking = false;
	public bool ByLadder = false;
	public bool Climing = false;
	public bool OnPlatform = false;
	private bool _regenerating = false;
	private bool _stopRegen = false;
	private bool _startRegen = false;
	//
	public int _health = 100;
	//
	public int _stamina = 100;
	//
	public float _staminaRegen = .5f;
	//
	private PlayerGUI _gui;
	// Use this for initialization
	void Start () 
	{
		_gui = GameObject.Find("PlayerUI").GetComponent<PlayerGUI>();
	}
	// Update is called once per frame
	void Update () 
	{
		_gui._staminaBar.value = _stamina;
		if(_stopRegen && _regenerating)
		{
			StopCoroutine("RegenStamina");
			_stopRegen = false;
		}
		if(_startRegen && !_regenerating)
		{
			StartCoroutine(RegenStamina(_staminaRegen));
			_startRegen = false;
		}
	}
	//
	public IEnumerator RegenStamina(float x)
	{
		_regenerating = true;
		while(_stamina < 100)
		{
			_stamina++;
			yield return new WaitForSeconds(x);
		}
		_regenerating = false;
		yield return null;
	}
	//
	public void StartStaminaRegen()
	{
		_startRegen = true;
	}
	//
	public void StopStaminaRegen()
	{
		_stopRegen = true;
	}
}
