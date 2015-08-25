using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Threading;

public class ControlSystem : MonoBehaviour 
{
	//
	[System.Serializable]
	public class PlayerData
	{
		//
		public string _name = "";
		//
		public int _level = 1;
		//
		public StatsData[] _partyStats = new StatsData[4];
	}
	//
	[System.Serializable]
	public class StatsData
	{
		//
		public string _name = "";
		public string _class = "";
		public string _id = "";
		public string _type = "";
		public string _specialAttack = "";
		//
		public int _health = 1;
		//
		public int _defence = 1;
		public int _resistance = 1;
		//
		public int _damage = 1;
		public int _magicDamage = 1;
		//
		public int _level = 1;
		public int _exp = 0;
		//
		public int[] _statGrowth = new int[5];
	}
	//
	public static ControlSystem _control;
	//
	public GameObject _camera;
	public GameObject _inventory;
	//
	public bool _inBattle = false;
	public bool _lookingForBattle = false;
	public bool _running = false;
	public bool _timerTrigger = false;
	public bool _timeOut = false;
	//
	public Player _player;
	//
	public GameObject _playerObject;
	//
	public System.Timers.Timer _timer;
	public System.Timers.Timer _timeOutTimer;
	//
	private Thread _searchThread;
	//
	void Awake()
	{
		if(_control == null)
		{
			DontDestroyOnLoad(gameObject);
			_control = this;
			//
			_running = true;
			//
			_camera = GameObject.Find("Camera");
			_playerObject = GameObject.Find("Player");
			//
			_timer = new System.Timers.Timer(30000);
			_timer.AutoReset = true;
			_timer.Elapsed += OnTimerElapsed;
			//
			_timeOutTimer = new System.Timers.Timer(30000);
			_timeOutTimer.Elapsed += OnTimeOutElapsed;
			_timeOutTimer.AutoReset = true;
			//
			Load();
		}
		else if(_control != this)
		{
			Destroy(this.gameObject);
		}
	}
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown ("e")) 
		{
			EnterBattle();
		}
		if (_timeOut) 
		{
			_timeOut = false;
			MainGUI._mainGUI.BattleTimeOut();
			_lookingForBattle = false;
			_timerTrigger = false;
			_timeOutTimer.Stop ();
			InitateTimer();
			_timer.Start();
		}
	}
	//
	public bool getInbattle()
	{
		return _inBattle;
	}
	//
	public void setInBattle(bool x)
	{
		_inBattle = x;
	}
	//
	public void Save()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Open (Application.persistentDataPath + "/player.dat", FileMode.Open);
		PlayerData player = new PlayerData ();
		setPlayerData(player, _player);
		bf.Serialize (file, player);
		file.Close ();
	}
	//
	public void Load()
	{
		_player = _playerObject.GetComponent<Player> ();
		_player.Intiate();
		if(File.Exists(Application.persistentDataPath+"/player.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/player.dat", FileMode.Open);
			PlayerData player = (PlayerData)bf.Deserialize(file);
			setPlayer(_player, player);
			file.Close ();
			//_player.AddContract("Thief");
			StartGame();
		}
		else
		{
			MainGUI._mainGUI._new = true;
			_camera.AddComponent<NewUserGUI>();
		}
	}
	//
	public void StartGame()
	{
		_searchThread = new Thread(() => SearchForPlayer());
		StartCoroutine(SearchForBattle());
		//
		Instantiate (Resources.Load ("Inventory"));
		_inventory = GameObject.FindGameObjectWithTag ("Inventory");
		_inventory.name = "Inventory";
		//
		SetStage();
	}
	//
	public void CreateUser()
	{
		if (File.Exists (Application.persistentDataPath + "/player.dat")) 
		{
			File.Delete(Application.persistentDataPath + "/player.dat");
			File.Create (Application.persistentDataPath + "/player.dat").Close();
		} 
		else 
		{
			File.Create (Application.persistentDataPath + "/player.dat").Close();
		}
		Save ();
	}
	//
	public void DeleateUserData()
	{
		File.Delete(Application.persistentDataPath + "/player.dat");
		Destroy (_inventory);
		_timer.Stop ();
		_timeOutTimer.Stop ();
		_running = false;
		_player.ClearContracts ();
		MainGUI._mainGUI._new = true;
		_camera.AddComponent<NewUserGUI>();
	}
	//
	public IEnumerator SearchForBattle()
	{
		_timer.Start ();
		while (_running) 
		{
			if(_timerTrigger && !_lookingForBattle)
			{
				_lookingForBattle = true;
				MainGUI._mainGUI.FoundBattle();
				_timeOutTimer.Start ();
				_timer.Stop ();
			}
			yield return null;
		}
	}
	//
	public void SearchForPlayer()
	{
		while (_running) 
		{
			if(!_lookingForBattle)
			{

			}
		}
		return;
	}
	//
	public void EnterBattle()
	{
		//
		_inBattle = true;
		_timerTrigger = false;
		_timeOut = false;
		_lookingForBattle = false;
		//
		_timeOutTimer.Stop ();
		_timer.Stop ();
		//
		Application.LoadLevel (3);
		MainGUI._mainGUI._deleateData.gameObject.SetActive (false);
	}
	//
	public void OnLevelWasLoaded()
	{
		if(_control == this)
		{
			if(Application.loadedLevel == 2)
			{
				_camera = GameObject.Find("Camera");
				_camera.transform.position = new Vector3(0, 0, 0);
				_camera.transform.rotation = new Quaternion(0, 0, 0, 0);
				_timer = new System.Timers.Timer(30000);
				_timer.AutoReset = true;
				_timer.Elapsed += OnTimerElapsed;
				//
				_timeOutTimer = new System.Timers.Timer(30000);
				_timeOutTimer.Elapsed += OnTimeOutElapsed;
				_timeOutTimer.AutoReset = true;
				StartGame();
			}
		}
	}
	//
	public void OnTimerElapsed(System.Object source, System.Timers.ElapsedEventArgs e)
	{
		_timerTrigger = true;
	}
	//
	public void OnTimeOutElapsed(System.Object source, System.Timers.ElapsedEventArgs e)
	{
		_timeOut = true;
	}
	//
	public void InitateTimer()
	{
		//float _ran = Random.Range (1, 10);
		_timer.Interval = (30000);
	}
	//
	public void setPlayerData(PlayerData playerData, Player player)
	{
		playerData._name = player._name;
		playerData._level = player._level;
		setStatsData (playerData._partyStats, player._partyStats);
	}
	//
	public void setStatsData(StatsData[] statsData, Stats[] stats)
	{
		for (int i = 0; i < 4; i++) 
		{
			statsData[i] = new StatsData();
			statsData[i]._name = stats[i]._name;
			statsData[i]._class = stats[i]._class;
			statsData[i]._id = stats[i]._id;
			statsData[i]._type = stats[i]._type;
			if(stats[i]._spAttack != null)
			{
				statsData[i]._specialAttack = stats[i]._spAttack._id;
			}
			else
			{

			}
			//
			statsData[i]._health = stats[i]._health;
			//
			statsData[i]._resistance = stats[i]._resistance;
			statsData[i]._defence = stats[i]._defence;
			//
			statsData[i]._damage = stats[i]._damage;
			statsData[i]._magicDamage = stats[i]._magicDamage;
			//
			statsData[i]._level = stats[i]._level;
			statsData[i]._exp = stats[i]._exp;
			//
			for(int x = 0; x < 5; x++)
			{
				statsData[i]._statGrowth[x] = stats[i]._statGrowth[x];
			}
		}
	}
	//
	public void setPlayer(Player player, PlayerData playerData)
	{
		player._name = playerData._name;
		player._level = playerData._level;
		setContracts(player, playerData._partyStats);
		setStats (player, playerData._partyStats);
	}
	//
	public void setContracts(Player player, StatsData[] statsData)
	{
		for(int i = 0; i < 4; i++)
		{
			GameObject newMember = (GameObject)Resources.Load ("Contracts/" + statsData[i]._name);
			Destroy(player._party[i]);
			player._party[i] = (GameObject)Instantiate(newMember);
			player._party[i].transform.parent = player.transform;
			player._party[i].name = statsData[i]._id;
			player._partyStats[i] = _player._party[i].GetComponent<Stats>();
		}
	}
	//
	public void setStats(Player player, StatsData[] statsData)
	{
		for (int i = 0; i < 4; i++)
		{
			player._partyStats[i]._name = statsData[i]._name;
			player._partyStats[i]._class = statsData[i]._class;
			player._partyStats[i]._id = statsData[i]._id;
			player._partyStats[i]._type = statsData[i]._type;
			//
			player._partyStats[i]._health = statsData[i]._health;
			//
			player._partyStats[i]._resistance = statsData[i]._resistance;
			player._partyStats[i]._defence = statsData[i]._defence;
			//
			player._partyStats[i]._damage = statsData[i]._damage;
			player._partyStats[i]._magicDamage = statsData[i]._magicDamage;
			//
			player._partyStats[i]._level = statsData[i]._level;
			player._partyStats[i]._exp = statsData[i]._exp;
			//
			for(int x = 0; x < 5; x++)
			{
				player._partyStats[i]._statGrowth[x] = statsData[i]._statGrowth[x];
			}
			//
			if(statsData[i]._specialAttack != "")
			{
				player._partyStats[i].SetSpecial(statsData[i]._specialAttack);
			}
			else{}
		}
	}
	//
	public void SetStage()
	{
		System.DateTime time = System.DateTime.Now;
		if((time.Hour > 18) || (time.Hour < 6))
		{
			RenderSettings.skybox = (Material)Resources.Load("SkyBox/MoonShine Skybox");
			Light light = _camera.GetComponentInChildren<Light>();
			light.intensity = .2f;
		}
		else
		{
			RenderSettings.skybox = (Material)Resources.Load("SkyBox/Sunny2 Skybox");
			Light light = _camera.GetComponentInChildren<Light>();
			light.intensity = .5f;
		}
	}
	//
	public void OnApplicationPause()
	{
		
	}
	//
	public void OnApplicationQuit()
	{
		_timer.Close ();
		_timeOutTimer.Close ();
		Resources.UnloadUnusedAssets ();
		_running = false;
		Save ();
	}

}
