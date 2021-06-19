using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Collider2D c2D;
    public Sprite sprite;
    public Animator animator;
    public float moveSpeed;
    //对话中按移动键也不能走动
    public bool moveable;
    //是否在NPC附近，按下E键应不应该弹出对话框
    public bool talkable;
    public GameObject Talk;
    // Start is called before the first frame update
    void Start()
    {
        c2D = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moveable)
        {
            float horizon = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * horizon * moveSpeed * Time.deltaTime);
        }
        if(Input.GetKeyDown(KeyCode.E) && talkable)
        {
            //TODO:清理对话框
            BeginTalk();
        }
    }
    public void BeginTalk()
    {
        talkable = false;
        moveable = false;
        Talk.SetActive(true);
    }

    public void EndTalk()
    {
        talkable = true;
        moveable = true;
        Talk.SetActive(false);
    }
}
