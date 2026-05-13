using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEngine.EventSystems;

//9스테이지 - 이동하는 오브젝트에 삽입

public class Drag : MonoBehaviour,IBeginDragHandler, IEndDragHandler, IDragHandler //드래그 구현 용 
{
    Vector2 pos;
    bool is_over ,is_clear; // 실패했는가, 성공했는가
    [Header("모든 체력 오브젝트")]
    public GameObject[] hp;
    [Header("클리어 오브젝트")]
    public GameObject clear;
    int i = 0;

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData) //클릭 시작
    {
        this.pos = transform.position; //원위치 복귀를 위해 현 위치 저장
        if(is_over)
        {
            foreach(GameObject j in hp)
            {
                j.SetActive(true);
                i = 0;
                is_over = false;
            }
        }
    }

    void IDragHandler.OnDrag(PointerEventData eventData) //클릭 중
    {
        Vector2 current_pos = eventData.position; //이동하는 벡터 저장
        transform.position = current_pos; //저장된 벡터로 이동
        if (is_over) //만약 실패했다면
            transform.position = this.pos; //원위치로

    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData) //클릭 종료
    {
        if(!is_clear)
            transform.position = this.pos; //원위치 이동
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Obstacle"))
        {
            
            hp[i].SetActive(false);
            i++;

            if(i >= hp.Length)
            {
                is_over = true;
            }
        }

        if (other.transform.CompareTag("Clear"))
        {
            clear.SetActive(true);
            is_clear = true;
        }
    }

}
