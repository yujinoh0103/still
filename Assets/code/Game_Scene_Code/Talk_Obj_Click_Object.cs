using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Talk_Obj_Click_Object : MonoBehaviour
{
    GameObject Story_Canvas; //Talk_GameManager 있는 오브젝트
    [Header("floot_click.text 참조. 아이템 클릭 시 나오는 대사 번호")]
    public int click_obj_num;//아이템 아이디 번호
    public int line_num = 0; //실제 줄 번호
    public int now_floor = 0;
    string[] talk; //텍스트 내용 저장
    bool Is_Text_Save; //텍스트 저장 완료
    int MAX;

    void Start()
    {
       

    }

    void Update()
    {

    }



    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject()) //EventSystem.current.IsPointerOverGameObject()는 레이캐스트 타겟이 체크된 UI 클릭했는지 확인
        {
            Story_Canvas.GetComponent<GameManager_Talk>().Talk_Start_item(now_floor+7, line_num );
            //1층 8번, 2층 9번, 3층 10번, 4층 11번
        }

    }

    void OnBecameVisible()//카메라에 보일 때 1회 실행
    {
        Save_text_all();
    }


    void Save_text_all()
    {
       
        if (!Is_Text_Save)
        {
            now_floor = DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_floor_num;
            Story_Canvas = GameObject.Find("Find_Stroy").transform.Find("Story_Canvas").gameObject;
            string path = Application.streamingAssetsPath + DontDestroy_Gamemanager.Dont_Destroy_Instance.talk_name[now_floor + 7];
            this.MAX = System.IO.File.ReadAllLines(path).Length; //현재 파일의 전체 줄 수
            talk = new string[MAX];
            talk = System.IO.File.ReadAllLines(path); //파일 내용

            while (line_num + 1 < this.MAX)
            {
                if (talk[line_num + 1].StartsWith("("))
                {
                    //Debug.Log(line_num);
                    int item_num = int.Parse(talk[line_num + 1].Substring(1, talk[line_num + 1].Length - 1)); //맨 앞 기호 제거;
                    if (item_num == click_obj_num)
                    {
                        line_num++;
                        break; //line_num은 위에서 대입
                    }
                    else
                    {
                        line_num++;
                    }
                }
                else
                {
                    line_num++;
                }
            }
            Is_Text_Save = true;

        }
    }

}
