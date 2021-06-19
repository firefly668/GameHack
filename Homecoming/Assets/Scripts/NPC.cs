using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class NPC : MonoBehaviour
{
    public Player player;
    public Animator animator;
    public Image thinkBubble;
    public Text thinkText;
    public string thinkstr;
    private float targetAlpha;
    private float currentAlpha;
    private float tick;
    private bool ticking;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            BeginThink("111");
        }

        if (targetAlpha != currentAlpha)
        {
            Debug.Log(currentAlpha);
            thinkBubble.gameObject.SetActive(true);
            if (Mathf.Abs(targetAlpha - currentAlpha) > 0.1)
                currentAlpha += (targetAlpha - currentAlpha) * Time.deltaTime * 2;
            else currentAlpha = targetAlpha;

            thinkBubble.color = new Color(255, 255, 255, currentAlpha);
        }
        else
        {
            if (currentAlpha == 0)
            {
                if (thinkBubble.IsActive())
                    thinkBubble.gameObject.SetActive(false);
            }
            else
            {
                if (!thinkText.IsActive())
                {
                    thinkText.gameObject.SetActive(true);
                    thinkText.DOText(thinkstr, 0.8f, false);
                    tick = 0;
                }
                else
                {
                    if (ticking)
                    {
                        tick += Time.deltaTime;
                        if (tick > 4.0f)
                        {
                            ticking = false;
                            EndThink();
                        }
                    }
                    else
                    {
                        ticking = true;
                        tick = 0;
                    }
                }
            }
        }
    }

    public void moveToPlayer()
    {
        Vector3 target = player.transform.position;
        Vector3 pointAt = target - transform.position;
        transform.Translate(pointAt);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player.talkable = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.talkable = false;
        }
    }

    public void BeginThink(string str)
    {
        thinkstr = str;
        targetAlpha = 1;
        thinkText.text = "";
    }

    public void EndThink()
    {
        thinkText.gameObject.SetActive(false);
        targetAlpha = 0;
    }
}
