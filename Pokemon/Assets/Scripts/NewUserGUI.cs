using UnityEngine;
using System.Collections;
using System.Threading;
using UnityEngine.UI;

public class NewUserGUI : MonoBehaviour 
{
	/*------------------------------------------------------------------------*/
	//
	public Text _outputText;
	//
	public InputField _inputText;
	/*------------------------------------------------------------------------*/
	//
	public Button _submit;
	public Button _yes;
	public Button _no;
	public Button _warrior;
	public Button _thief;
	public Button _wizard;
	//
	public GameObject _parent;
	public GameObject _container;
	/*------------------------------------------------------------------------*/
	//Wait Handle for puasing the flow of logic until the user does somthing
	public EventWaitHandle _enterHit = new EventWaitHandle(false, EventResetMode.AutoReset);
	/*------------------------------------------------------------------------*/
	//GUI bools for the diffierent steps of creating a user
	public bool _namePick = false;
	public bool _classPick = false;
	public bool _tutorial = false;
	//Bool to skip the text writing animation
	public bool _finishOutput = false;
	//Bool for waiting for user input
	public bool _waiting = false;
	/*------------------------------------------------------------------------*/
	//Rect for text display for new player
	private Rect _outputRect = new Rect(212,150,600,400);
	/*------------------------------------------------------------------------*/
	// Use this for initialization
	void Start () 
	{
		Instantiate((GameObject)Resources.Load ("NewUser"));
		_parent = GameObject.Find ("NewUser(Clone)");
		_container = _parent.transform.FindChild ("Container").gameObject;
		_outputText = _container.transform.FindChild ("OutputText").GetComponent<Text>();
		//
		_inputText = _container.transform.FindChild ("InputField").GetComponent<InputField>();
		//
		_submit = _container.transform.FindChild ("Submit").GetComponent<Button>();
		_submit.onClick.AddListener (() => {Submit();});
		//
		_warrior = _container.transform.FindChild ("Warrior").GetComponent<Button>();
		_warrior.onClick.AddListener (() => {Warrior();});
		_thief = _container.transform.FindChild ("Thief").GetComponent<Button>();
		_thief.onClick.AddListener (() => {Thief();});
		_wizard = _container.transform.FindChild ("Wizard").GetComponent<Button>();
		_wizard.onClick.AddListener (() => {Wizard();});
		_warrior.gameObject.SetActive (false);
		_thief.gameObject.SetActive (false);
		_wizard.gameObject.SetActive (false);
		//
		_yes = _container.transform.FindChild ("Yes").GetComponent<Button> ();
		_yes.onClick.AddListener(() =>{Yes();});
		_no = _container.transform.FindChild ("No").GetComponent<Button> ();
		_no.onClick.AddListener (() => {No ();});
		_yes.gameObject.SetActive (false);
		_no.gameObject.SetActive (false);
		//
		StartCoroutine (CreatePlayer());
	}
	// Update is called once per frame
	void Update () 
	{

	}
	//
	void OnGUI()
	{
		if((Input.GetMouseButtonDown(0)) && (_outputRect.Contains(Event.current.mousePosition))) 
		{
			_finishOutput = true;
		}
		if ((Input.GetMouseButtonUp(0)) && (_finishOutput)) 
		{
			_finishOutput = false;
		}
	}
	//Main function for creating player, stop the flow of logic when user input is required
	public IEnumerator CreatePlayer()
	{
		//Getting name
		StartCoroutine(TypeOut("New player detected.\nPlease type in a new user name, then press Submit."));
		Thread t = new Thread (() => GetName(ControlSystem._control._player));
		t.Start ();
		while (t.IsAlive) 
		{
			yield return null;
		}
		_inputText.gameObject.SetActive (false);
		_submit.gameObject.SetActive (false);
		_warrior.gameObject.SetActive (true);
		_thief.gameObject.SetActive (true);
		_wizard.gameObject.SetActive (true);
		//Getting starting contract
		StartCoroutine(TypeOut("Please select a starting party member."));
		t = new Thread (() => SelectMember(ControlSystem._control._player));
		t.Start ();
		while (t.IsAlive) 
		{
			yield return null;
		}
		_warrior.gameObject.SetActive (false);
		_thief.gameObject.SetActive (false);
		_wizard.gameObject.SetActive (false);
		_yes.gameObject.SetActive (true);
		_no.gameObject.SetActive (true);
		//All data needed has been gotten, create user.
		ControlSystem._control.CreateUser ();
		//Ask if they would like to do a tutorial to go over battle controls.
		StartCoroutine(TypeOut("Would you like a tutorial of a battle?"));
		t = new Thread (() => Tutorial ());
		t.Start ();
		while (t.IsAlive) 
		{
			yield return null;
		}
		Destroy (_parent.gameObject);
	}
	//Waits for user input then sets the name
	public void GetName(Player player)
	{
		_namePick = true;
		while (_namePick) 
		{
			_enterHit.WaitOne ();
			if(_inputText.text != "")
			{
				_namePick = false;
				player._name = _inputText.text;
			}
			else{}
		}
		return;
	}
	//Waits for user input then moves on.
	public void SelectMember(Player player)
	{
		_classPick = true;
		while (_classPick) 
		{

		}
		return;
	}
	//Waits for user input then proceeds
	public void Tutorial()
	{
		_tutorial = true;
		while (_tutorial) 
		{

		}
		return;
	}
	//Starts the user tutorial
	public void StartTutorial()
	{

	}
	//Does an animation of writing out the text
	public IEnumerator TypeOut(string x)
	{
		int length = x.Length;
		string writtenText = "";
		for(int i = 0; i < length; i++)
		{
			writtenText += x.Substring(i,1);
			_outputText.text = writtenText;
			if(_finishOutput)
			{
				_outputText.text = x;
				break;
			}
			yield return new WaitForSeconds(.05f);
		}
	}
	//
	public void Submit()
	{
		_enterHit.Set ();
	}
	//
	public void Warrior()
	{
		ControlSystem._control._player.AddContract("Warrior");
		_classPick = false;
	}
	//
	public void Thief()
	{
		ControlSystem._control._player.AddContract("Thief");
		_classPick = false;
	}
	//
	public void Wizard()
	{
		ControlSystem._control._player.AddContract("Wizard");
		_classPick = false;
	}
	//
	public void Yes()
	{
		StartTutorial();
		_tutorial = false;
	}
	//
	public void No()
	{
		MainGUI._mainGUI._new = false;
		ControlSystem._control.StartGame();
		_tutorial = false;
	}
}
