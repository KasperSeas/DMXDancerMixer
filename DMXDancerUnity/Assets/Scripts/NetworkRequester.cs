using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkRequester : MonoBehaviour {

    public FaceDataJSON currentFaceData;
    public bool getFaceData = true;

    public BodyDataJSON currentBodyData;
    public bool getBodyData = true;

    private void Update()
    {
        if(getFaceData)
        {
            GetFaceData();
        }

        if(getBodyData)
        {
            GetBodyData();
        }
    }

    private void GetFaceData()
    {
        StartCoroutine(RequestFaceData( (faceData) => {
            currentFaceData = faceData;
        }));
    }

    private IEnumerator RequestFaceData(System.Action<FaceDataJSON> callback)
    {
        using(UnityWebRequest www = UnityWebRequest.Get("https://s3.amazonaws.com/deeplens-sagemaker-bodyposes/models/faceData.json"))
        {
            yield return www.SendWebRequest();

            if(www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                callback(GetFaceDataResponse.CreateFromJSON(www.downloadHandler.text).faceData[0]);
                // Show results as text
                //Debug.Log(www.downloadHandler.text);
                // Or retrieve results as binary data
                //byte[] results = www.downloadHandler.data;
            }
        }
    }

    private void GetBodyData()
    {
        StartCoroutine(RequestBodyData((bodyData) => {
            currentBodyData = bodyData;
        }));
    }

    private IEnumerator RequestBodyData(System.Action<BodyDataJSON> callback)
    {
        using(UnityWebRequest www = UnityWebRequest.Get("https://s3.amazonaws.com/deeplens-sagemaker-bodyposes/models/bodyData.json"))
        {
            yield return www.SendWebRequest();

            if(www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                callback(GetBodyDataResponse.CreateFromJSON(www.downloadHandler.text).bodyData[0]);
                // Show results as text
                //Debug.Log(www.downloadHandler.text);
                // Or retrieve results as binary data
                //byte[] results = www.downloadHandler.data;
            }
        }
    }

}

[System.Serializable]
public class BodyDataJSON {
    public float pitch;
    public float yaw;
    public float roll;
}

[System.Serializable]
public class GetBodyDataResponse {
    public BodyDataJSON[] bodyData;

    public static GetBodyDataResponse CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<GetBodyDataResponse>(jsonString);
    }
}

[System.Serializable]
public class FaceDataJSON {
    public int isSmiling;
}

[System.Serializable]
public class GetFaceDataResponse {
    public FaceDataJSON[] faceData;

    public static GetFaceDataResponse CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<GetFaceDataResponse>(jsonString);
    }
}
