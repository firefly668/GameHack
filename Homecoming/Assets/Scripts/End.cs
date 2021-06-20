using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class End : MonoBehaviour
{
    public Image panel;
    public Text summary;
    public float currentPanelAlpha;
    public float currentSumAlpha;
    public string summarystr;
    public bool end;
    private void OnEnable()
    {
        currentPanelAlpha = 0;
        currentSumAlpha = 0;
        panel.color = new Color(0, 0, 0, 0);
        summary.color = new Color(255, 255, 255, 0);
        summary.gameObject.SetActive(true);
        end = false;
        for (int i = 0; i < SoundManager.instance.mic.Length; i++)
        {
            SoundManager.instance.mic[i].mute = true;
        }
        SoundManager.instance.mic[8].mute = false;
        SoundManager.instance.mic[8].Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(1 - currentPanelAlpha) > 0.1)
            currentPanelAlpha += (1 - currentPanelAlpha) * Time.deltaTime / 2;
        else currentPanelAlpha = 1;
        panel.color = new Color(0, 0, 0, currentPanelAlpha);
        if(currentPanelAlpha == 1)
        {
            if (Mathf.Abs(1 - currentSumAlpha) > 0.1)
                currentSumAlpha += (1 - currentSumAlpha) * Time.deltaTime;
            else currentSumAlpha = 1;
            summary.color = new Color(255, 255, 255, currentSumAlpha);
            if(currentSumAlpha == 1)
            {
                end = true;
            }
        }
        if (end && Input.anyKey)
        {
            SceneManager.LoadScene("StartScence");
        }
    }
}
