using UnityEngine;
using System.Collections;

public class LoginScreen : MonoBehaviour 
{
	//Resolution
	public float _rWidth = 1024;
	public float _rHeight = 768;
	//Matrix translation
	private Vector3 _translation = new Vector3(1, 1, 1);
	//Matrix rotation
	private Quaternion _rotation = Quaternion.identity;
	//Matrix scale
	private Vector3 _scale;
	//Gui styles
	public GUIStyle _loginScreenStyle;
	public GUIStyle _textFieldStyle;
	public GUIStyle _textGeneralStyle;
	public GUIStyle _headerStyle;
	public GUIStyle _buttonStyle;
	public GUIStyle _popupStyle;
	public GUIStyle _exitButtonStyle;
	//strings for holding username, password and the url's
	private string _username = "";
	private string _password = "";
	private string _debug = "";
	private string _url = "http://www.zimmerspineuniversity.com/mss/vrsim/applicationauth.php";
	private string _newUserUrl = "http://www.zimmerspineuniversity.com";
	//Rect for the login window
	private Rect _loginGroupRect = new Rect(257, 134, 500, 500);
	//Backgrounds
	public Texture2D _mainBackground;
	public Texture2D _background;
	//Master script to talk to
	private Master _master;
	//Bools
	private bool _noInternet = false;
	private bool _unknownUser = false;
	// Use this for initialization
	void Start () 
	{
		_master = GameObject.Find("Master").GetComponent<Master>();
	}
	// Update is called once per frame
	void Update () 
	{
	
	}
	//GUI
	void OnGUI()
	{
		//Scaleing matrix stuff
		_scale = new Vector3(Screen.width/_rWidth, Screen.height/_rHeight, 1);
		Matrix4x4 matrix = Matrix4x4.TRS(_translation, _rotation, _scale);
		GUI.matrix = matrix;
		//Main background
		//GUI.DrawTexture(new Rect(0, 0, 1024, 768), _mainBackground);
		//Debug for errors
		//GUI.Label(new Rect (0, 0, 1024, 768), _debug);
		/*
		 * Need different paths for phones/tablets and the websim,
		 * Phones/tablets need to ask for user information/new user/guest in the simulation while the websim will already have that information
	 	*/
		#if UNITY_IPHONE || UNITY_ANDROID
			GUI.BeginGroup(_loginGroupRect);
			GUI.DrawTexture(new Rect(0, 0, 500, 500), _background);
			GUI.Label(new Rect(0, 0, 500, 200), "Please Login using your username and password then click 'Login'. If you are a new user please enter the username and password you would like to use, then click 'New User'. If you want to use the simulation as a guest simply click 'Skip'.", _headerStyle);

			GUI.Label(new Rect(10, 120, 100, 20), "Username: ", _textGeneralStyle);
			_username = GUI.TextField(new Rect(100, 115, 200, 35), _username, _textFieldStyle);

			GUI.Label(new Rect(10, 200, 100, 20), "Password: ", _textGeneralStyle);
			_password = GUI.TextField(new Rect(100, 195, 200, 35), _password, _textFieldStyle);

			if(GUI.Button(new Rect(400, 400, 58, 58), "Login", _buttonStyle))
			{
				PlayerPrefs.SetString("Username", _username);
				PlayerPrefs.SetString("Password", _password);
				//print (PlayerPrefs.GetString("Password"));
				PlayerPrefs.Save();
				Master.Guest = false;
				Master.User = true;
				CreateForm();
			}
			if(GUI.Button(new Rect(20, 320, 88, 58), "Skip", _buttonStyle))
			{
				Master.Guest = true;
				Master.User = false;
				this.GetComponent<MainScreen>().enabled = true;
				this.GetComponent<LoginScreen>().enabled = false;
			}
			if(GUI.Button(new Rect(20, 400, 88, 58), "New User", _buttonStyle))
			{
				Application.OpenURL(_newUserUrl);
			}
		
			if(_noInternet)
			{
				GUI.Box(new Rect(100, 100, 300, 200), "Could not connect to the internet, please connect to a wifi network then try logging in again.", _popupStyle);
				if(GUI.Button(new Rect(120, 220, 58, 58), "", _exitButtonStyle))
		 	  	{
					_noInternet = false;
				}
			}
			else if(_unknownUser)
			{
				GUI.Box(new Rect(100, 100, 300, 200), "Could not find combination of username and password.", _popupStyle);
				if(GUI.Button(new Rect(120, 220, 58, 58), "", _exitButtonStyle))
				{
					_unknownUser = false;
				}
			}
		GUI.EndGroup();
		#endif
		#if UNITY_WEBPLAYER
			if(!this.GetComponent<MainScreen>().hasChosen())
			{
				this.GetComponent<MainScreen>().enabled = true;
				Master.Guest = false;
				Master.User = true;
				this.GetComponent<LoginScreen>().enabled = false;
			}
			else{}
		#endif
	}
	//Create WWWform to get data from server url
	private void CreateForm()
	{	
		WWWForm form = new WWWForm();

		form.AddField("user", PlayerPrefs.GetString("Username"));
		form.AddField("pass", PlayerPrefs.GetString("Password"));
		form.AddField("action", "authenticate");
		#if UNITY_WEBPLAYER
		form.AddField("Resume", PlayerPrefs.GetString("Resume"));
		form.AddField("Mode", PlayerPrefs.GetString("Mode"));
		form.AddField("CurrentStep", PlayerPrefs.GetInt("CurrentStep"));
		form.AddField("GreatestStep", PlayerPrefs.GetInt("GreatestStep"));

		for(int i = 0; i < _master.getTotalLength(); i++)
		{
			form.AddField("Passed" + i, PlayerPrefs.GetInt("" + i));
		}
		#endif
		WWW www = new WWW(_url, form);
		//_debug += "968474575";
		StartCoroutine(WaitForRequest(www));			
	}
	//Wait for data then check if the user was able to login/no internet/not a user
	IEnumerator WaitForRequest(WWW www)
	{	
		//_debug += "hjgfhjdfhjfd";
		yield return www;
		// check for errors
		if (www.error == null)
		{	
			_debug += "\n no error";
			_noInternet = false;
			if(www.text.Contains("sucess"))
			{
				_unknownUser = false;
				if(!this.GetComponent<MainScreen>().hasChosen())
				{
					this.GetComponent<MainScreen>().enabled = true;
					this.GetComponent<LoginScreen>().enabled = false;
				}
				else{}
			}
			else
			{
				_unknownUser = true;
			}
		}
		else 
		{
			_debug += "\n" + www.error;
			_noInternet = true;
		}
	}
}
