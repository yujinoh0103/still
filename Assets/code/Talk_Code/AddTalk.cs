using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTalk : MonoBehaviour
{

    //추가 대사가 나올 경우

    public int Max;

    public void AddTalkNum()
    {
        if(Max < gameObject.GetComponent<Talk_Obj_Click_Object>().click_obj_num) //Max보다 작은 경우 다음 대사 출력
            gameObject.GetComponent<Talk_Obj_Click_Object>().click_obj_num++;
    }
 
}
