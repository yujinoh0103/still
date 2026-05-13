using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEditor.PlayerSettings;

public class Timing : MonoBehaviour
{
    static public Timing instance;
    public GameObject hand;
    [Header("클리어 표시 오브젝트")]
    public Text clearText;
    [Header("화살표 오브젝트")]

    public GameObject arrow;
    [Header("초록색 체크 오브젝트")]
    public GameObject check_ber;

    Vector3 startPos;
    bool is_click = false, is_check = false, is_clear = false;
    int speed = 1;
    public bool isMoving = true; // 물체 움직임을 제어하는 변수

    private void Awake() //*
    {
        instance = this;
    }

    void Start()
    {
        startPos = transform.position;
        isMoving = true;
        hand.SetActive(true);

    }

    void Update()
    {
        if (!is_clear && isMoving)
        {
            Vector3 v = startPos;
            v.y += -4.0f * Mathf.Cos(Time.time * speed); // y축 이동
            transform.position = v;

            if (v.y >= 3.99)
            {
                isMoving = false;
                clearText.text = "Fail";
            }

            /*if (Input.GetMouseButtonDown(0))
            {
                isMoving = !isMoving;
            }*/
        }

        if (check_ber != null && arrow != null)
        {
            if (!isMoving)
            {
                float yPosition = arrow.transform.position.y;
                if (yPosition >= 2.5f && yPosition < 3.99f)
                {
                    int damage = (int)((yPosition - 2.5f) * 5);
                    clearText.text = "판정 성공\n데미지 : "+damage;
                }
                else if (yPosition >= 1.5f && yPosition < 2.5f)
                {
                    clearText.text = "판정 성공";
                }
                else if (yPosition >= -4f && yPosition < 1.5f)
                {
                    clearText.text = "판정 실패";
                }

            }
            else
            {
                clearText.text = "";
            }
        }
    }

    public void Button_Click()
    {
        if (is_check)
            is_click = true;
    }
}
