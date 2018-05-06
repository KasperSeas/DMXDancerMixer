using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DMXManager: MonoBehaviour {

    [SerializeField] private NetworkRequester _networker;

    private bool _lightOn = false;
    private byte xAxis = 0;

    [SerializeField] private LM70 _movingHead1;
    [SerializeField] private LM70 _movingHead2;
    [SerializeField] private LM70 _movingHead3;
    [SerializeField] private LM70 _movingHead4;


    [SerializeField] private List<Fixture> _fixtures = new List<Fixture>();
    private DMXUniverse _universe = new DMXUniverse();

    private float _lastLeftYaw;
    private float _strobeLeftDelay = 0f;
    [SerializeField] [Range(0f, 1f)] private float _smileLevel;
    private float _level;
    [SerializeField] private float _smileThreshold = 0.7f;



    [SerializeField] [Range(-270f, 270f)] private float _leftPitch;
    [SerializeField] [Range(-90f, 90f)] private float _leftYaw;
    private float _lastRightPitch;
    private float _strobeRightDelay = 0f;
    [SerializeField] [Range(-270f, 270f)] private float _rightPitch;
    [SerializeField] [Range(-90f, 90f)] private float _rightYaw;
    private float minPitch = -270f;
    private float maxPitch = 270;
    private float minYaw = -90f;
    private float maxYaw = 90;
    [SerializeField] private float _pitchStrobeThreshold = 0.7f;
    [SerializeField] private float _yawStrobeThreshold = 30f;

    [SerializeField] private Color _themeColor1 = Color.white;
    [SerializeField] private Color _themeColor2 = Color.white;



    public void Start()
    {
        try
        {
            OpenDMX.Start();                                            //find and connect to devive (first found if multiple)
            if(OpenDMX.status == FT_STATUS.FT_DEVICE_NOT_FOUND)       //update status
                Debug.Log("No Enttec USB Device Found");
            else if(OpenDMX.status == FT_STATUS.FT_OK)
                Debug.Log("Found DMX on USB");
            else
                Debug.Log(OpenDMX.status);
        }
        catch(Exception exp)
        {
            Debug.Log(exp);
            Debug.Log("Error Connecting to Enttec USB Device");

        }

        for(int i = 0; i < _fixtures.Count; i++)
        {
            _fixtures[i].Initialize();
            _universe.AddFixture(_fixtures[i]);
        }

        _universe.StartDataWriter();
    }

    public void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            _smileLevel += 0.01f;
        }
        if(Input.GetKey(KeyCode.Z))
        {
            _smileLevel -= 0.01f;

        }
        //if(_networker.getBodyData)
        //{
        //    Debug.Log("Pitch: "+_networker.currentBodyData.pitch);
        //    _leftYaw = _networker.currentBodyData.pitch;
        //}

        //_movingHead1.MoveXAxis(Range(_leftPitch, minPitch, maxPitch));

        //_smileLevel = _smileLevel * 90f;

        _level = 90f -(_smileLevel * 90f);

        _movingHead1.MoveYAxis(Range(_level, minYaw, maxYaw));
        _movingHead2.MoveYAxis(Range(_level, minYaw, maxYaw));

        _movingHead3.MoveYAxis(Range(_level, minYaw, maxYaw));
        _movingHead4.MoveYAxis(Range(_level, minYaw, maxYaw));

        if(_smileLevel > _smileThreshold)
        {
            _movingHead1.Strobe(1f);
            _movingHead2.Strobe(1f);
            _movingHead3.Strobe(1f);
            _movingHead4.Strobe(1f);

            _strobeLeftDelay = 0.7f;
        }
        else
        {
            _strobeLeftDelay -= Time.deltaTime;
            if(_strobeLeftDelay <= 0f)
            {
                _movingHead1.ActivateLight(true);
                _movingHead2.ActivateLight(true);
                _movingHead3.ActivateLight(true);
                _movingHead4.ActivateLight(true);

            }
        }
        _lastLeftYaw = _level;

        //if(Mathf.Abs(_level - _lastLeftYaw) > _pitchStrobeThreshold)
        //{
        //    _movingHead1.Strobe(1f);
        //    _movingHead2.Strobe(1f);
        //    _movingHead3.Strobe(1f);
        //    _movingHead4.Strobe(1f);

        //    _strobeLeftDelay = 0.7f;
        //}
        //else
        //{
        //    _strobeLeftDelay -= Time.deltaTime;
        //    if(_strobeLeftDelay <= 0f)
        //    {
        //        _movingHead1.ActivateLight(true);
        //        _movingHead2.ActivateLight(true);
        //        _movingHead3.ActivateLight(true);
        //        _movingHead4.ActivateLight(true);

        //    }
        //}
        //_lastLeftYaw = _level;



        //_movingHead2.MoveXAxis(Range(_rightPitch, minPitch, maxPitch));
        //if(Mathf.Abs(_rightPitch - _lastRightPitch) > _pitchStrobeThreshold)
        //{
        //    _movingHead2.Strobe(1f);
        //    _strobeRightDelay = 0.7f;
        //}
        //else
        //{
        //    _strobeRightDelay -= Time.deltaTime;
        //    if(_strobeRightDelay <= 0f)
        //    {
        //        _movingHead2.ActivateLight(true);
        //    }
        //}
        //_lastRightPitch = _rightPitch;





        for(int i = 0; i < _fixtures.Count; i++)
        {
            _fixtures[i].UpdateFixture();
        }
    }


    public void SetColorTheme()
    {
        _movingHead1.Color(_themeColor2.r, _themeColor2.g, _themeColor2.b);
        _movingHead2.Color(_themeColor1.r, _themeColor1.g, _themeColor1.b);
        _movingHead3.Color(_themeColor1.r, _themeColor1.g, _themeColor1.b);
        _movingHead4.Color(_themeColor2.r, _themeColor2.g, _themeColor2.b);

    }


    private void OnApplicationQuit()
    {
        _universe.EndDataWriter();
        OpenDMX.End();
    }

    private float Range(float value, float min, float max)
    {
        return (value - min) / (max - min);
    }
}
