using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameManager_Game : MonoBehaviour
{
    public static GameManager_Game Game_GameMananger;

    [Header("버튼 누를 때 뜨는 창 부모")]
    public GameObject panel;
    [Header("버튼 누를 때 뜨는 창들, 실행시 자동 채움")]
    public Transform[] panel_child;
    //0 - Esc(버튼 모음) 1 - inventory 2- map Image 3 - title 4 - howtoplay 5 - setting 6 - save 7- load
    public bool[] is_show = new bool[8]; //뜨는 창 bool. 숫자는 위와 동일

    [Header("아이템 설명 텍스트")]
    public GameObject inventory_text;
    [Header("현재 방 번호 텍스트")]
    public GameObject room_num_text;

    const int Player_Min = 1, Player_Max = 11;//위치
    int[] Player_Max_floor = { 11, 11, 8, 10 };//층수에 따른 최대 위치. 이후 Player_Max 대체 에정
    public int player_now_room_num; //현재 방 위치. DontDestroy_GameManager에서 받아옴
    public int player_now_floor_num; //현재 층 위치. DontDestroy_GameManager에서 받아옴

    public int enemy_now_num;
    public int enemy_num_min, enemy_num_max;

    float die_limit = 1, die_timer; //범인과 같은 방에 있을 때 사망하는 시간제한
    [Header("범인이 주위에 있을 때 뜨는 이미지/ 왼[0] 오[1]")]
    public Image[] near_enemy; //주위에 범인이 있을 때 붉은 색이 뜸(이미지)
    public bool is_maximum_start = false; //현재 범인이 끝방부터 돌아오는가?

    public bool is_sound = false;//소리를 냈는가?

    public bool is_Save_Input= false;//저장 중인지. 맞다면 지도 열기 등 작업 안 됨

    private void OnValidate()
    {

        Game_GameMananger = this;

    }
    private void Start()
    {
        Load_player_pos();
        panel_child = new Transform[8];
        int i = 0;
        foreach (Transform child in panel.transform)
        {
            panel_child[i] = (child.GetComponentInChildren<Transform>());
            i++;
        }

        // Eneny_Move_Start();
        //StartCoroutine(Eneny_Move()); //적 움직임 시작
    }



    // Update is called once per frame
    void Update()
    {
        Load_player_pos();
        room_num_text.GetComponent<Text>().text = (player_now_floor_num).ToString() + "층 " + (player_now_room_num).ToString() + "번 방";
        // Show_Enemy_Pos();

        //만약 적과 내 위치가 동일하다면(잡힘)
        if (enemy_now_num == player_now_room_num)
        {
            die_timer += Time.deltaTime;
            if (die_timer > die_limit)
            {
                panel.SetActive(true);
            }

        }
        //창 띄우기

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            is_show[0] = !is_show[0];
            panel_child[0].gameObject.SetActive(is_show[0]);

            for (int i = 1; i < panel_child.Length; i++)
            {
                is_show[i] = false;
                panel_child[i].gameObject.SetActive(is_show[i]);
            }
            panel.SetActive(is_show[0]);
        }

        if(!is_Save_Input) //이름 입력 중 창이 켜지지 않게끔 함
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                for (int i = 0; i < panel_child.Length; i++)
                {
                    if (i != 2) //예외 처리, 안 하면 창이 안 꺼짐
                    {
                        is_show[i] = false;
                        panel_child[i].gameObject.SetActive(is_show[i]);
                    }

                }

                is_show[2] = !is_show[2];
                panel_child[2].gameObject.SetActive(is_show[2]);
                panel.SetActive(is_show[2]);

            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                for (int i = 0; i < panel_child.Length; i++)
                {
                    if (i != 1) //예외 처리, 안 하면 창이 안 꺼짐
                    {
                        is_show[i] = false;
                        panel_child[i].gameObject.SetActive(is_show[i]);
                    }

                }

                is_show[1] = !is_show[1];
                panel_child[1].gameObject.SetActive(is_show[1]);
                panel.SetActive(is_show[1]);
                inventory_text.GetComponent<Text>().text = "아이템 이름\n\n아이템 정보";
            }
        }

       
    }

    void Load_player_pos() //DontDestroy_Gamemanager에서 층, 위치 불러옴
    {
        player_now_room_num = DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num;
        player_now_floor_num = DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_floor_num;
    }


    void Eneny_Move_Start() //첫 적 움직임 시작
    {

        enemy_num_min = Random.Range(Player_Min, 6);
        enemy_num_max = Random.Range(6, Player_Max);

        for (; enemy_num_min == player_now_room_num;) //현재 player의 위치와 같다면
        {
            enemy_num_min = Random.Range(Player_Min, 6);
        }

        enemy_now_num = enemy_num_min;

    }

    void Enemy_Move_Second_Start() //두번째 움직임 시작
    {
        is_maximum_start = !is_maximum_start;

        if (is_maximum_start) //true일 경우 현재 머무른 곳부터 min로 이동
        {
            enemy_num_max = enemy_now_num;

            enemy_num_min = Random.Range(Player_Min, 5);

            for (; enemy_num_min == enemy_num_max;)
            {
                enemy_num_min = Random.Range(Player_Min, 5);
            }
        }
        else //false일 경우 max로 이동 
        {
            enemy_num_min = enemy_now_num;
            enemy_num_max = Random.Range(5, Player_Max);

            for (; enemy_num_min == enemy_num_max;)
            {
                enemy_num_max = Random.Range(5, Player_Max);
            }

        }
    }

    IEnumerator Eneny_Move() //코루틴
    {
        //yield return new WaitForSeconds(Random.Range(5.0f, 10.0f)); //주기(자유 변경)
        yield return new WaitForSeconds(2);

        if (is_maximum_start) //is_maximum_start =true일 경우 현재 머무른 곳부터 min로 이동
        {
            enemy_now_num--;
            if (is_sound)
            {
                enemy_num_min = player_now_room_num;
                is_sound = false;
            }
            if (enemy_now_num <= enemy_num_min)
            {
                Enemy_Move_Second_Start();
            }
        }
        else //false일 경우 max로 이동 
        {

            enemy_now_num++;
            if (is_sound)
            {
                enemy_num_max = player_now_room_num;
                is_sound = false;
            }
            if (enemy_now_num >= enemy_num_max)
            {
                Enemy_Move_Second_Start();
            }

        }



        StartCoroutine(Eneny_Move());

    }

    void Show_Enemy_Pos()
    {
        switch (player_now_room_num - enemy_now_num)
        {
            case -2:
                near_enemy[1].GetComponent<Image>().color = new Color(1, 1, 1, 0.1f);
                near_enemy[0].GetComponent<Image>().color = new Color(1, 1, 1, 0.0f);
                break;

            case -1:
                near_enemy[1].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                near_enemy[0].GetComponent<Image>().color = new Color(1, 1, 1, 0.0f);

                break;

            case 0:
                near_enemy[0].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                near_enemy[1].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);

                break;

            case 1:
                near_enemy[0].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                near_enemy[1].GetComponent<Image>().color = new Color(1, 1, 1, 0.0f);

                break;

            case 2:
                near_enemy[0].GetComponent<Image>().color = new Color(1, 1, 1, 0.1f);
                near_enemy[1].GetComponent<Image>().color = new Color(1, 1, 1, 0.0f);

                break;

            default:
                near_enemy[0].GetComponent<Image>().color = new Color(1, 1, 1, 0.0f);
                near_enemy[1].GetComponent<Image>().color = new Color(1, 1, 1, 0.0f);
                break;
        }
    }

    public void Title_Button()
    {
        is_show[3] = !is_show[3];
        panel_child[3].gameObject.SetActive(is_show[3]);
    }

    public void Title_Button_Yes()
    {
        SceneManager.LoadScene("Title_Scene");
    }

    public void How_To_Play_Button()
    {
        is_show[4] = !is_show[4];
        panel_child[4].gameObject.SetActive(is_show[4]);
    }

    public void Setting_Button()
    {
        is_show[5] = !is_show[5];
        panel_child[5].gameObject.SetActive(is_show[5]);
    }

    public void Save_Button()
    {
        is_show[6] = !is_show[6];
        panel_child[6].gameObject.SetActive(is_show[6]);
    }

    public void Load_Button()
    {
        is_show[7] = !is_show[7];
        panel_child[7].gameObject.SetActive(is_show[7]);
    }

    //slot 스크립트에서 사용. button 연결용

    public int invrntory_floor;
    public void Floor_Invrntory_Button_1()
    {
        invrntory_floor = 1;
    }
    public void Floor_Invrntory_Button_2()
    {
        invrntory_floor = 2;

    }
    public void Floor_Invrntory_Button_3()
    {
        invrntory_floor = 3;

    }
    public void Floor_Invrntory_Button_4()
    {
        invrntory_floor = 4;

    }

 

}
