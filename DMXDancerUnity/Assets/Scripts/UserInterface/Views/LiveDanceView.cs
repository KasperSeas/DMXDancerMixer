using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LiveDanceView : View {

    [SerializeField] private TextMeshPro _label;
    [SerializeField] private TextMeshPro _countdown;

    private Color _labelColor = Color.white;
    private float _fadeTime = 0f;

    public override void StartView()
    {
        base.StartView();
        _label.text = "Dance!";
        _countdown.text = "";
        StartCoroutine(DanceTimer());
    }


    private IEnumerator DanceTimer()
    {
        _labelColor.a = 1f;
        _fadeTime = 1f;
        while(_fadeTime > 0)
        {
            _fadeTime -= Time.deltaTime;
            _labelColor.a = _fadeTime;
            _label.color = _labelColor;
            yield return null;
        }
        //_labelColor.a = 0f;
        _label.color = _labelColor;

        yield return new WaitForSeconds(8f);
        //_label.text = "Ending...";
        //_labelColor.a = 1f;
        //_label.color = _labelColor;

        //yield return CountDown();
        //_app.OnLiveDanceFinished();
    }


    private IEnumerator CountDown()
    {
        _countdown.text = "3";
        yield return new WaitForSeconds(1f);
        _countdown.text = "2";
        yield return new WaitForSeconds(1f);
        _countdown.text = "1";
        yield return new WaitForSeconds(1f);
    }

}
