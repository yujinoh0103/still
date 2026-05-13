using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Action_Base : MonoBehaviour
{

    public int number; //현재 리스트 수와 일치하면 삽입
    GameObject GameMananger;

    private void Awake()
    {
        GameMananger = GameObject.Find("GameManager");

    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if ( GameManager.Manager.ACT_TURN == number)
        {
           
        }

    }
}
