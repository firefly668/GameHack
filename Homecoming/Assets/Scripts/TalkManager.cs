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
    public Bgroll bgroll;
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
    public void Change(bool flag ,string response)
    {
        ChatBox.transform.position = InputDown.transform.position;
        if (flag)
        {
            ChatBox.text = InputDown.text + "\n";
            InputDown.text = "";
        }
        else
        {
            ChatBox.text = "";
        }
        //此时InputUp.readOnly应该是true
        ChatBox.gameObject.transform.DOLocalMoveY(UpPos, 1.5f, false).OnComplete(() =>
        {
            ChatBox.DOText(ChatBox.text + response,0.8f).OnComplete(() =>
            {
                InputDown.readOnly = false;
                sentable = true;
                if (!flag)
                {
                    bgroll.EndGame();
                }
            });
        });
    }

    public void TalkSend()
    {
        InputDown.readOnly = true;
        sentable = false;
        PostManager.instance.SendPost(currentNPCnumber, InputDown.text);
    }
}
