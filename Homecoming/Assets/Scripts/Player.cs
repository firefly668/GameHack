using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Player : MonoBehaviour
{
    //地图左侧墙壁 因觉得要用左侧进场 所以先不激活左侧墙壁
    //等角色通过后再激活 防止角色走出地图
    public GameObject leftWall;
    public Collider2D c2D;
    public Sprite sprite;
    public Animator animator;
    public float moveSpeed;
    //对话中按移动键也不能走动
    public bool moveable;
    //是否在NPC附近，按下E键应不应该弹出对话框
    public bool talkable;
    public GameObject Talk;
    //新游戏开始 播放开场动画 禁止玩家进行任何游戏内操作
    public bool isStartGameAni = true;

    AnimatorStateInfo ani;
    public Image thinkBubble;
    public Text thinkText;
    public string thinkstr;
    private float targetAlpha;
    private float currentAlpha;
    public NPC currentNPC;
    public bool talking;
    Quaternion last_q= Quaternion.Euler(0, 180, 0); 
    // Start is called before the first frame update
    void Start()
    {
        c2D = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        //开始游戏 播放开场动画
        PlayNewGame();
        //Debug.Log(isStartGameAni);
        leftWall.SetActive(false);
        targetAlpha = 0;
        currentAlpha = 0;
        thinkBubble.color = new Color(255, 255, 255, 0);
        talking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStartGameAni == false)
        {
            if (moveable)
            {
                float horizon = Input.GetAxis("Horizontal");
                
               
                //Debug.Log(horizon);

                if (horizon > 0.5f)
                {
                    animator.Play("playerRight");
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    transform.Translate(Vector3.left * horizon * moveSpeed * Time.deltaTime);
                    
                    last_q = transform.rotation;
                    //Debug.Log("应该播放向右动画");
                }
                else if (horizon < -0.5f)
                {
                    animator.Play("playerLeft");
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    transform.Translate(Vector3.right * horizon * moveSpeed * Time.deltaTime);
                   
                    last_q = transform.rotation;

                    //Debug.Log("应该播放向左动画");
                }
                else
                {
                    animator.Play("PlayerIdle");
                    transform.rotation = last_q;
                    //Debug.Log("应该播放待机动画");

                }

               
            }
            if (Input.GetKeyDown(KeyCode.E) && talkable)
            {
                //TODO:清理对话框
                BeginTalk();
            }
        }
        //检测开成场动画是否播放完成
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime>=1.0f)
        {
            ////开场动画播放完毕 可以控制角色
            //isStartGameAni = false;
            ////开场动画播放完毕 激活左侧墙壁
            //leftWall.SetActive(true);
        }
        //Debug.Log(isStartGameAni + "   " + moveable + "   " + talkable);
        if (Input.GetKeyDown(KeyCode.T))
        {
            BeginThink("111");
        }
        if (Input.GetKeyDown(KeyCode.Q) && currentAlpha == 1)
        {
            EndThink();
        }
        if (targetAlpha != currentAlpha)
        {
            //Debug.Log(currentAlpha);
            if (Mathf.Abs(targetAlpha - currentAlpha) > 0.1)
                currentAlpha += (targetAlpha - currentAlpha) * Time.deltaTime * 2;
            else currentAlpha = targetAlpha;
            
            thinkBubble.color = new Color(255, 255, 255, currentAlpha);
        }
        else
        {
            if (currentAlpha == 0)
            {
                if(thinkBubble.IsActive())
                    thinkBubble.gameObject.SetActive(false);
            }
            else
            {
                if (!thinkText.IsActive())
                {
                    thinkText.gameObject.SetActive(true);
                    thinkText.DOText(thinkstr, 0.8f, false);
                }
            }
        }
    }
    public void BeginTalk()
    {
        talkable = false;
        moveable = false;
        Talk.SetActive(true);
        TalkManager.instance.currentNPCnumber = currentNPC.Number;
        currentNPC.talked = true;
        talking = true;
    }

    public void EndTalk()
    {
        talkable = true;
        moveable = true;
        Talk.SetActive(false);
        talking = false;
    }
    public void PlayNewGame()
    {
        animator.Play("playerAni");
        transform.rotation = Quaternion.Euler(0, 180, 0);
        //isStartGameAni = false;
        transform.Translate(Vector3.left * 5);
        Invoke("InvokePlayGame", 1f);
    }
    public void InvokePlayGame()
    {
        //开场动画播放完毕 可以控制角色
        isStartGameAni = false;
        //开场动画播放完毕 激活左侧墙壁
        leftWall.SetActive(true);
    }

    public void BeginThink(string str)
    {
        thinkstr = str;
        targetAlpha = 1;
        thinkText.text = "";
        thinkBubble.gameObject.SetActive(true);
    }

    public void EndThink()
    {
        thinkText.gameObject.SetActive(false);
        targetAlpha = 0;
    }
}
