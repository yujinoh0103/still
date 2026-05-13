using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.PlayerSettings;

public class Action_Drag_Script : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler //드래그 구현 용 
{
    Vector2 pos;
    bool is_act;
    GameObject GameMananger;
    public enum ACT //행동. Action_Base 사용
    {
        None,
        Attack,
        Defense,
        CounterAttack

    }
    [Header("행동 타입")]
    public ACT act_type;


    private void Awake()
    {
        GameMananger = GameObject.Find("GameManager");

    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData) //클릭 시작
    {
        if (!is_act)
            this.pos = transform.position; //원위치 복귀를 위해 현 위치 저장
    }

    void IDragHandler.OnDrag(PointerEventData eventData) //클릭 중
    {
        if (!is_act)
        {
            Vector2 current_pos = eventData.position; //이동하는 벡터 저장
            transform.position = current_pos; //저장된 벡터로 이동
        }

    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData) //클릭 종료
    {
        if (!is_act)
            transform.position = this.pos; //원위치 이동
    }

    //명시적 구현 3개. 안 하면 인터페이스 사용불가라고 하면서 오류남

    public void OnCollisionEnter2D(Collision2D other)
    {
        GameObject other_obj = other.gameObject;
       
        if (other.transform.CompareTag("Base")&&GameManager.Manager.ACT_TURN == other_obj.GetComponent<Action_Base>().number)
            //만약 베이스가 현재 결정 순번과 동일하다면
        {
            is_act = true;
            transform.position = other.transform.position; //베이스 위치로 좌표 변경
            Instantiate(transform.gameObject, pos, Quaternion.identity, transform); //이동

            switch (act_type)
            {
                case Action_Drag_Script.ACT.Attack:
                    GameMananger.GetComponent<Action_Script>().Attack_Act();
                    break;

                case Action_Drag_Script.ACT.Defense:
                    GameMananger.GetComponent<Action_Script>().Defense_Act();
                    break;

                case Action_Drag_Script.ACT.CounterAttack:
                    GameMananger.GetComponent<Action_Script>().Counterattack_Act();
                    break;
            }
            GameManager.Manager.ACT_TURN++;

        }
    }

    public void Reset_Action()
    {
        Transform[] delete = gameObject.GetComponentsInChildren<Transform>();

        if(delete.Length > 1)
        {
            for (int i = 1; i < delete.Length; i++)
            {
                Destroy(delete[i].gameObject);
            }

            transform.position = this.pos;
        }

        is_act = false;
    }
}


