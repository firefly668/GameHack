using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

   public AudioSource[] mic;
        

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("RondomDialogue", 10, 10);
    }

    // Update is called once per frame
    void Update()
    {
        RondomDialogue();
    }
    public void RondomDialogue()
    {
        int i = Random.Range(0, 2);
        Debug.Log(i);
        if (i==1)
        {
            mic[3].Play();
        }
        else
        {
            mic[4].Play();
        }

    }
}
