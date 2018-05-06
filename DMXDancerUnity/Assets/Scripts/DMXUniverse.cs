using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMXUniverse {

    private byte[] _channels = new byte[512];
    private List<Fixture> _fixtures = new List<Fixture>();
    private int _currentIndex = 0;
    private Coroutine _dataWriter;
    private WaitForSeconds _wait = new WaitForSeconds(0.1f);

    public void AddFixture(Fixture fixture)
    {
        fixture.SetUniverseIndex(_currentIndex);
        _currentIndex += fixture.ChannelCount;
        _fixtures.Add(fixture);
    }

    public void StartDataWriter()
    {
        if(_dataWriter != null)
        {
            MonoAccessor.instance.StopCoroutine(_dataWriter);
        }
        _dataWriter = MonoAccessor.instance.StartCoroutine( WriteData() );
    }

    private IEnumerator WriteData()
    {
        while(true)
        {
            OpenDMX.WriteData();
            yield return _wait;
        }
    }

    public void EndDataWriter()
    {
        if(_dataWriter != null)
        {
            MonoAccessor.instance.StopCoroutine(_dataWriter);
        }
    }

}
