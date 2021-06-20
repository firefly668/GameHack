using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public Player player;
    public Bgroll bgroll;
    public float gametime;
    public float currenttime;
    public bool endbegin;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);
    }
    void Start()
    {
        endbegin = false;
        currenttime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (currenttime < gametime)
        {
            currenttime += Time.deltaTime;
            return;
        }
        if (!endbegin)
        {
            endbegin = true;
            if (player.talking)
            {
                player.currentNPC.EndGameTalk();
                return;
            }
            else
            {
                Debug.Log(1);
                bgroll.EndGame();
            }
        }
    }
}
