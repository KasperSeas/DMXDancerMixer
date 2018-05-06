using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fixture: MonoBehaviour {

    protected int _channelCount;
    protected byte[] _channels;
    protected bool _dirty = false;
    private int _universeStartIndex = 0;

    public int ChannelCount
    {
        get { return _channelCount; }
    }

    

    public virtual void Initialize()
    {
        _channels = new byte[_channelCount];
    }

    public void SetUniverseIndex(int universeStartIndex)
    {
        this._universeStartIndex = universeStartIndex;
    }

    public virtual void UpdateFixture()
    {
        if(_dirty)
        {
            //OpenDMX.WriteData();
            _dirty = false;
        }
    }


    protected void SetDmxValue(int channel, byte value)
    {
        if(channel - 1 >= 0 && channel - 1 < _channels.Length)
        {
            _channels[channel - 1] = value;
            OpenDMX.SetDmxValue(_universeStartIndex+channel, value);
            
        }
    }

    public static byte Range(float value, byte min, byte max)
    {
        return (byte)Clamp((byte)(min + ((max - min) * value)), min, max);
    }

    public static byte Clamp(byte value, byte min, byte max)
    {
        return (value < min) ? min : (value > max) ? max : value;
    }
}


