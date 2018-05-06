using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoAccessor : MonoBehaviour {

    public static MonoAccessor instance;

    public MonoAccessor()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
	
}
