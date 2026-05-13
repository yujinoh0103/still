using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEditor.PlayerSettings;

public class timing : MonoBehaviour
{
    //2스테이지 - pin 오브젝트 내에 삽입(충돌 판정 때문)

    [Header("클리어 표시 오브젝트")]
    public GameObject clear;
    [Header("화살표 오브젝트")]
    public GameObject arrow;
    [Header("점수 표시 오브젝트")]
    public GameObject score;
    [Header("초록색 체크 오브젝트")]
    public GameObject check_ber;
    Vector3 pos;
    Vector3 check_ber_pos;
    int speed = 2;
    bool is_click = false, is_check = false, is_clear; //초록 위인가 체크용(버튼 클릭)
    float max = 970.8f, min = 525.0f; //판정 체크 바
    int num = 0;

    // Update is called once per frame
    void Update()
    {
        if(!is_clear) //종료 시 마지막 초록에 붙게 하기 위함
        {
            Vector3 v = pos;
            v.x += 550.0f * Mathf.Sin(Time.time * speed); // 좌우 이동의 최대치 및 반전 처리
                                                          //sin으로 반복 움직임
            transform.position = v;

        }

    }
    void Start()
    {
        pos = transform.position;
        this.check_ber_pos = check_ber.transform.position;
        
    }
    public void Button_Click()
    {
        if(is_check)
            is_click = true;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
    }

    public void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.transform.CompareTag("Check_Ber"))
        {
            is_check = true;
            if (is_click)
            {
                is_click = false;

                if (num == 2)
                {
                    is_clear = true;
                    clear.SetActive(is_clear);
                    speed = 0;
                    arrow.transform.position = new Vector3 (this.check_ber_pos.x,arrow.transform.position.y,arrow.transform.position.z);
                }
                
                else
                {
                    num++;
                    score.GetComponent<Text>().text = num.ToString("D1");
                    speed = Random.Range(2, 3);
                    this.check_ber_pos = new Vector3(Random.Range(min, max), check_ber_pos.y, check_ber_pos.z); //랜덤 위치 변경
                    check_ber.transform.position = check_ber_pos;

                }

            }

        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        is_check = false;
    }

}
