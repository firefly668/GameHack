using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using LitJson;
public class test : MonoBehaviour
{
    string connect;
    IEnumerator TestPost()
    {
        Message message = new Message();
        //message.AddField("Content-Type", "application/json");
        message.number = 1;
        message.length = 250;
        message.top_p = 0.8;
        message.temperature = 0.9;
        message.prompt = connect;
        string jsondata = JsonMapper.ToJson(message);
        byte[] data = Encoding.UTF8.GetBytes(jsondata);
        UnityWebRequest test = new UnityWebRequest("https://618.rct.ai/z", UnityWebRequest.kHttpVerbPOST);
        test.uploadHandler = new UploadHandlerRaw(data);
        test.downloadHandler = new DownloadHandlerBuffer();
        test.SetRequestHeader("Content-Type", "application/json");
        yield return test.SendWebRequest();
        if (test.error != null)
            Debug.Log(test.error);
        else
        {
            Debug.Log(test.downloadHandler.text);
            connect = test.downloadHandler.text;
            Debug.Log(connect.Length);
            connect = connect.Substring(13, connect.Length-16);
            Debug.Log(connect);
        }
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            connect = "爱吃肉";
            StartCoroutine(TestPost());
        }
    }
    public string SendPost(string str="爱吃肉")
    {
        connect = str;
        StartCoroutine(TestPost());
        return connect;
    }
}
