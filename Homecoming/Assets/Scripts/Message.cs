using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message
{
    public int number;
    public int length;
    public double top_p;
    public double temperature;
    public string prompt;

    public Message(int number,int length,double top_p,double temperature,string prompt)
    {
        this.number = number;
        this.length = length;
        this.top_p = top_p;
        this.temperature = temperature;
        this.prompt = prompt;
    }
    public Message() { }
}
