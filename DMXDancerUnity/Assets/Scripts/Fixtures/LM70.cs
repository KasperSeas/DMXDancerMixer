using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LM70 : Fixture {

    [SerializeField] Transform _elbow;

    [SerializeField] [Range(0f, 1f)] private float _moveX;
    [SerializeField] [Range(0f, 1f)] private float _moveY;
    [SerializeField] private bool _active = true;
    [SerializeField] [Range(0f, 1f)] private float _masterControl;
    [SerializeField] [Range(0f, 1f)] private float _red;
    [SerializeField] [Range(0f, 1f)] private float _green;
    [SerializeField] [Range(0f, 1f)] private float _blue;
    [SerializeField] [Range(0f, 1f)] private float _white;
    [SerializeField] [Range(0f, 1f)] private float _XYMotorSpeed;

    // 136 -240 strobe
    // 241-255 ON

    public override void Initialize()
    {
        _channelCount = 9;
        base.Initialize();
        ActivateLight(_active);
    }

    public override void UpdateFixture()
    {

        //_moveX = Range(_elbow.localEulerAngles.y);
        //Debug.Log("raw: " + _elbow.localRotation.eulerAngles.z);

        //_moveY = Range(_elbow.localRotation.eulerAngles.z);
        //Debug.Log("x: "+_moveX);
        //Debug.Log("y: "+ _moveY);


        //MoveXAxis(_moveX);
        //MoveYAxis(_moveY);
        //MasterControl(_masterControl);
        //Red(_red);
        //Green(_green);
        //Blue(_blue);
        //White(_white);
        //Speed(_XYMotorSpeed);
        base.UpdateFixture();
    }

    private float Range(float value)
    {
        //Debug.Log("in " + value);

        value = Mathf.Abs(value);
        //Debug.Log("abs "+value);
        //if(value > 180f)
        //{
        //    value = 180f;
        //}
        return value / 180f;
    }

    public void ActivateLight(bool active)
    {
        if(active)
        {
            SetDmxValue(3, 254);
        }
        else
        {
            SetDmxValue(3, 0);
        }
        _dirty = true;
    }

    public void MasterControl(float value)
    {
        SetDmxValue(3, (byte)(value * byte.MaxValue));
        _dirty = true;
    }

    public void Dim(float value)
    {
        SetDmxValue(3, Range(value, 8, 134));
        _dirty = true;
    }

    public void Strobe(float value)
    {
        SetDmxValue(3, Range(value, 136, 239));
        _dirty = true;
    }


    public void MoveXAxis(float value)
    {
        SetDmxValue(1, (byte)(value * byte.MaxValue));
        _dirty = true;
    }

    public void MoveYAxis(float value)
    {
        SetDmxValue(2, (byte)(value * byte.MaxValue));
        _dirty = true;
    }

    public void Red(float value)
    {
        SetDmxValue(4, (byte)(value * byte.MaxValue));
        _dirty = true;
    }
    public void Green(float value)
    {
        SetDmxValue(5, (byte)(value * byte.MaxValue));
        _dirty = true;
    }
    public void Blue(float value)
    {
        SetDmxValue(6, (byte)(value * byte.MaxValue));
        _dirty = true;
    }
    public void White(float value)
    {
        SetDmxValue(7, (byte)(value * byte.MaxValue));
        _dirty = true;
    }

    public void Color(float red, float green, float blue)
    {
        Red(red);
        Green(green);
        Blue(blue);
    }


    public void Speed(float value)
    {
        SetDmxValue(8, (byte)(value * byte.MaxValue));
        _dirty = true;
    }
}
