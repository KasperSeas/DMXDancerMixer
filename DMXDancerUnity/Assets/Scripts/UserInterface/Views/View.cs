using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour {

    protected bool _activated = false;
    protected MainApplication _app;

    public void Initialize(MainApplication app)
    {
        this._app = app;
    }

    public virtual void StartView()
    {
        //if(_activated) { return; }
        _activated = true;
        gameObject.SetActive(true);
    }

    public virtual void EndView()
    {
        //if(!_activated) { return; }
        _activated = false;
        gameObject.SetActive(false);
    }
}
