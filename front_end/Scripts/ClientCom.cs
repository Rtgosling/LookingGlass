using System;
using UnityEngine;
using UnityEngine.UI;// for ui changing
using System.Collections;
using System.Collections.Generic;

public class ClientCom : MonoBehaviour
{


    void Start()
    {
        string url = "http://192.168.1.10:8000/AINT350/command.txt"; //IP for server will go in url
        WWW www = new WWW(url);
        StartCoroutine(WaitForRequest(www));
    }

    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;
        // check for errors
        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.text);
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }
}