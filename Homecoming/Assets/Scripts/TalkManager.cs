using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class TalkManager : MonoBehaviour
{
    public static TalkManager instance = null;

    //显示玩家的话和NPC的回答
    public InputField InputUp;
    //让玩家输入自己的话
    public InputField InputDown;
    public bool inputable;
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

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Change(string response)
    {
        InputUp.text = "";
        //此时InputUp.readOnly应该是true
        InputDown.readOnly = true;
        InputUp.gameObject.transform.DOLocalMoveY(DownPos, 0.2f, false).OnComplete(() =>
        {
            InputDown.gameObject.transform.DOLocalMoveY(UpPos, 1.0f, false).OnComplete(() =>
            {
                //TODO:等待post返回再显示response
                InputDown.gameObject.transform.Find("Text").GetComponent<Text>().DOText(InputDown.text + "\n" + response, 1).OnComplete(()=> 
                {
                    SwitchInputField();
                    //已经交换过了，此时的InputDown就是用户接下来要输入的inputfield，将其设置为可输入
                    InputDown.readOnly = false;
                });
            });
        });
    }

    public void TalkSend()
    {
        PostManager.instance.SendPost(currentNPCnumber, InputDown.text);
    }
    public void SwitchInputField()
    {
        InputField temp = InputUp;
        InputUp = InputDown;
        InputDown = temp;
    }
}
