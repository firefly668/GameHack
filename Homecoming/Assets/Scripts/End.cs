using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class End : MonoBehaviour
{
    public Image panel;
    public Text summary;
    public float currentAlpha;
    public string summarystr;
    public bool end;
    private void OnEnable()
    {
        currentAlpha = 0;
        panel.color = new Color(255, 255, 255, 0);
        summary.text = "";
        summary.gameObject.SetActive(false);
        end = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(1 - currentAlpha) > 0.1)
            currentAlpha += (1 - currentAlpha) * Time.deltaTime;
        else currentAlpha = 1;
        if(currentAlpha == 1)
        {
            summary.gameObject.SetActive(true);
            summary.DOText(summarystr, 1.5f).OnComplete(() => 
            {
                end = true;
            });
        }
        if (end && Input.anyKey)
        {
            //TODO:按键回到主菜单
        }
    }
}
