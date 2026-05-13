using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.PlayerSettings;

//3스테이지 - 각 퍼즐 객체에 삽입
public class puzzle : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler //드래그 구현 용 
{
    [Header("블럭 번호")] //코드 인스펙터 창에 보임
    public int number;
    Vector2 pos;
    GameObject game_manager;
    bool is_puzzle = false; //퍼즐이 맞는가?

    void Awake()
    {
        game_manager = GameObject.Find("GameManager");
    }
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData) //클릭 시작
    {
        if(!is_puzzle)
            this.pos = transform.position; //원위치 복귀를 위해 현 위치 저장
    }

    void IDragHandler.OnDrag(PointerEventData eventData) //클릭 중
    {
        if (!is_puzzle)
        {
            Vector2 current_pos = eventData.position; //이동하는 벡터 저장
            transform.position = current_pos; //저장된 벡터로 이동
        }
        
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData) //클릭 종료
    {
        if (!is_puzzle)
            transform.position = this.pos; //원위치 이동
    }

    //명시적 구현 3개. 안 하면 인터페이스 사용불가라고 하면서 오류남

    public void OnCollisionEnter2D(Collision2D other)
    {

        GameObject other_obj = other.gameObject;
        if(other_obj.GetComponent<puzzle_base>().number == this.number) //만약 퍼즐 베이스의 number와 이곳의 number가 같다면
        {
            transform.position = other.transform.position; //퍼즐 베이스 위치로 좌표 변경
            this.game_manager.GetComponent<puzzle_score>().puzzle_right_num++; // 맞춘 퍼즐 개수(puzzle_score 내부 변수)
            is_puzzle =true;
            

        }
    }
}


