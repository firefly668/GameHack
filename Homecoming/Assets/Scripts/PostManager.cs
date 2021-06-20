using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using LitJson;
public class PostManager : MonoBehaviour
{
    public static PostManager instance = null;

    int Number;
    public string[] connect;
    public string response;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
    IEnumerator Post()
    {
        response = "";
        Message message = new Message();
        //message.AddField("Content-Type", "application/json");
        message.number = 1;
        message.length = 250;
        message.top_p = 0.8;
        message.temperature = 0.9;
        message.prompt = connect[Number];
        string jsondata = JsonMapper.ToJson(message);
        byte[] data = Encoding.UTF8.GetBytes(jsondata);
        while (response.Length == 0)
        {
            Debug.Log(1);
            UnityWebRequest test = new UnityWebRequest("https://618.rct.ai/z", UnityWebRequest.kHttpVerbPOST);
            test.uploadHandler = new UploadHandlerRaw(data);
            test.downloadHandler = new DownloadHandlerBuffer();
            test.SetRequestHeader("Content-Type", "application/json");
            yield return test.SendWebRequest();
            if (test.error != null)
                Debug.Log(test.error);
            else if(!GameManager.instance.endbegin)
            {
                string temp = test.downloadHandler.text;
                Debug.Log(temp);
                //connect[Number] = test.downloadHandler.text;
                Debug.Log(temp.Length);
                string tempresponse = Regex.Match(temp, "response:.*?。|\\? ").Value;
                if (tempresponse.Length == 0)
                    continue;
                connect[Number] += tempresponse;
                //connect[Number] = connect[Number].Substring(13, connect.Length-16);
                response = "";
                response += Regex.Match(tempresponse, ":.*?。|\\?");
                Debug.Log(response);
                TalkManager.instance.Change(true,response);
            }
        }
    }

    public string SendPost(int number,string str="爱吃肉")
    {
        Number = number;
        connect[Number] += "input:" + str;
        StartCoroutine(Post());
        return response;
    }
}
