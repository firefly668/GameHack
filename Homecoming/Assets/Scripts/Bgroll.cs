using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Bgroll : MonoBehaviour
{
    public float moveSpeed;
    public Vector3 startPos;
    public End end;
    private void Start()
    {
        startPos = new Vector3(-85, -0.8f, 0);
    }
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        //Debug.Log(transform.position);
        if(transform.position.x > 35)
        {
            transform.position = startPos;
        }
    }
    public void EndGame()
    {
        DOTween.To(() => moveSpeed, x => moveSpeed = x, 0, 8).OnComplete(() => 
        {
            end.gameObject.SetActive(true);
        });
    }
}