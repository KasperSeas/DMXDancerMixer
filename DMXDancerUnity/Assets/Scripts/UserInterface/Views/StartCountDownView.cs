using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartCountDownView : View {


    [SerializeField] private TextMeshPro _countdown;


    public override void StartView()
    {
        base.StartView();
        _countdown.text = "";
        StartCoroutine(CountDown());
    }

    private IEnumerator CountDown()
    {
        _countdown.text = "3";
        yield return new WaitForSeconds(1f);
        _countdown.text = "2";
        yield return new WaitForSeconds(1f);
        _countdown.text = "1";
        yield return new WaitForSeconds(1f);
        _app.OnStartCountDownFinished();
    }


}
