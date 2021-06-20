using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using LitJson;
using System;
using Random = UnityEngine.Random;

public class PostManager : MonoBehaviour
{
    public static PostManager instance = null;

    int Number;
    public string[] connect;
    public string response;
    public GameObject[] NPCs;
    public int questioncount;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        questioncount = 0;
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
                string tempresponse = "response:" + temp.Split(new string[] {"response:","input:" }, StringSplitOptions.RemoveEmptyEntries)[1];
                if (tempresponse.Length == 0)
                    continue;
                connect[Number] += tempresponse;
                //connect[Number] = connect[Number].Substring(13, connect.Length-16);
                response = "";
                response += tempresponse.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries)[1];
                if ((Random.Range(0, 10) >= 2 && questioncount >= 4) || Random.Range(0,10)>=8)
                {
                    questioncount = 0;
                    string[] questions = NPCs[Number].GetComponent<NPC>().questions;
                    int tempNum = Random.Range(0, questions.Length);
                    connect[Number] += questions[tempNum];
                    response += questions[tempNum];
                }
                questioncount++;
                Debug.Log(response);
                TalkManager.instance.Change(true,response);
            }
        }
    }

    public string SendPost(int number,string str="爱吃肉")
    {
        if (Number != number)
        {
            questioncount = 0;
            Number = number;
        }
        connect[Number] += "input:" + str;
        StartCoroutine(Post());
        return response;
    }
}
