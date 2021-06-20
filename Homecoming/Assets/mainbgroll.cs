using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainbgroll : MonoBehaviour
{
    public float moveSpeed;
    public Vector3 startPos;
    public End end;
    private void Start()
    {
        
            startPos = new Vector3(-1954f, 0f, 0f);
        

    }
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        //Debug.Log(transform.position);
        if (transform.localPosition.x > 0)
        {
            transform.localPosition = startPos;
        }
    }
  

}


