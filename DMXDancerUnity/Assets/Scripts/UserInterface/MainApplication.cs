using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainApplication : MonoBehaviour {

    [SerializeField] private DMXManager _dmxManager;
    [SerializeField] private NetworkRequester _networker;
    enum AppState {
        Standby, // Waits for a gesture to start
        Info,
        SelectTheme, // Waits for a gesture choose theme
        StartCountDown, // Countdown to start recognizing the dance
        LiveDance, // Reocrds dance
        Finish // stops recording and output to next station
    }

    private static AppState _appState = AppState.Standby;

    [SerializeField] private View _standByView;
    [SerializeField] private View _infoView;
    [SerializeField] private View _selectedThemeView;
    [SerializeField] private View _startCountdownView;
    [SerializeField] private View _liveDanceView;
    [SerializeField] private View _finishView;

    [SerializeField] private View[] _views;

    public void Start()
    {
        for(int i = 0; i < _views.Length; i++)
        {
            _views[i].Initialize(this);
        }
        ResetApp();
    }

    public void ResetApp()
    {
        _appState = AppState.Standby;
        for(int i = 0; i < _views.Length; i++)
        {
            _views[i].EndView();
        }
        _standByView.StartView();
        _networker.getFaceData = true;
        //_networker.getBodyData = false;

    }

    public void StartApp() // Move out of Standby to ChooseTheme
    {
        _appState = AppState.StartCountDown;
        TransitionScenes(_standByView, _startCountdownView);
        _dmxManager.SetColorTheme();

        //_networker.getFaceData = false;

    }

    public void OnReceivedSelectedTheme(int choosenThemeID)
    {
        _appState = AppState.StartCountDown;
        TransitionScenes(_selectedThemeView, _startCountdownView);
        _dmxManager.SetColorTheme();
        //_networker.getBodyData = true;
    }

    public void OnStartCountDownFinished()
    {                                   
        _appState = AppState.LiveDance;
        TransitionScenes(_startCountdownView, _liveDanceView);
    }

    public void OnLiveDanceFinished()
    {
        _appState = AppState.Finish;
        TransitionScenes(_liveDanceView, _finishView);
    }

    private void TransitionScenes(View previous, View next)
    {
        previous.EndView();
        next.StartView();
    }


    private void Update() // DEBUG OVERRIDES
    {


        if(Input.GetKeyDown(KeyCode.Q))
        {
            ResetApp();
        }
        if(Input.GetKey(KeyCode.W) || _networker.currentFaceData.isSmiling >= 1)
        {
            StartApp();
        }
        if(Input.GetKey(KeyCode.E))
        {
            OnReceivedSelectedTheme(2);
        }
       
    }

}
