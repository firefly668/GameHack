using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bgroll : MonoBehaviour
{
    public int moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ChangeV", 14f, 14f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
    }
    public void ChangeV()
    {
        moveSpeed = -moveSpeed;
    }

    
}