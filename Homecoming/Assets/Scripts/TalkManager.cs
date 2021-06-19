using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class TalkManager : MonoBehaviour
{
    public static TalkManager instance = null;

    public Text ChatBox;
    //让玩家输入自己的话
    public InputField InputDown;
    public bool sentable;
    //TODO:开始对话时修改此值
    public int currentNPCnumber;
    public int UpPos;
    public int DownPos;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        ChatBox.text = "";
        InputDown.readOnly = false;
        sentable = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Return) && sentable && InputDown.text.Length!=0)
        {
            TalkSend();
        }
    }
    public void Change(string response)
    {
        ChatBox.transform.position = InputDown.transform.position;
        ChatBox.text = InputDown.text;
        InputDown.text = "";
        //此时InputUp.readOnly应该是true
        ChatBox.gameObject.transform.DOLocalMoveY(UpPos, 1.5f, false).OnComplete(() =>
        {
            ChatBox.DOText(ChatBox.text + "\n" + response,0.8f);
            //已经交换过了，此时的InputDown就是用户接下来要输入的inputfield，将其设置为可输入
            InputDown.readOnly = false;
            sentable = true;
        });
    }

    public void TalkSend()
    {
        InputDown.readOnly = true;
        sentable = false;
        PostManager.instance.SendPost(currentNPCnumber, InputDown.text);
    }
}
