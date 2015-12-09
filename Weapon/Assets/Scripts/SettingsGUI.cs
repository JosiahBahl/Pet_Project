using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettingsGUI : MonoBehaviour
{
    //
    public GameObject _settingsObj;
    //
    private bool _opened = false;
    //
    public Settings _settings;
    //
    public Slider _musicVolume;
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    //
    public void Load()
    {
        if (!_opened)
        {
            //
            _opened = true;
            //
            _settingsObj.SetActive(true);
            //
            _musicVolume.value = _settings.getMusicVolume();
        }
    }
    //
    public void Unload()
    {
        if (_opened)
        {
            //
            _opened = false;
            //
            _settingsObj.SetActive(false);
        }
    }
    //
    public void UpdateMusicVolume()
    {
        _settings.setMusicVolume(_musicVolume.value);
    }
}
