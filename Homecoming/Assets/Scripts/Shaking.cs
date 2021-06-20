using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Shaking : MonoBehaviour
{
    public float currenttime;
    public float targettime;
    public Vector3 Upv;
    public Vector3 Downv;
    public Vector3 Currentv;
    private void Start()
    {
        Currentv = transform.position;
        Upv = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
        Upv = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
        currenttime = 0;
    }
    void Update()
    {
        if (currenttime < targettime)
        {
            currenttime += Time.deltaTime;
            return;
        }
        currenttime = 0;
        this.transform.DOShakePosition(0.2f, new Vector3(0, 0.15f, 0));
    }
}
