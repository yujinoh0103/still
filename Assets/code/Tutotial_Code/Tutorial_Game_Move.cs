using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_Game_Move : MonoBehaviour
{
    //Game_scene과 동일.
    /*방 이동 버튼 순서는 방 숫자대로 정렬 (거실은 예외. 우선적으로 맨 앞)
     * 
     *방 숫자는 차례대로 왼쪽 위->오른쪽 위 -> 왼쪽 아래 -> 오른쪽 아래 순으로 매김. 방 호수와는 무관함(4층 발코니도 방 취급)
      나머지는 방 이후에 n01호(거실 1) -> 복도 -> n02호(거실 2)
     *이동 가능 구역
      1층 불가
      2층 201호 작은방(2),거실(9),복도(10), 현 깨어남
      3층 301호 욕실/화장실(1),거실(7),복도(8), 예린/서연 구출
      4층 402호 주방(4),복도(10),거실(11), 에이든 구출
     */

    [Header("방 이동 버튼 모은 오브젝트")]//Next_Room_Button
    public GameObject move_room;
    [Header("방 이동 버튼")]
    public Transform[] move_room_button;
    // move_room_button[0]은 텍스트. 이후부터 5개 버튼, [6]은 창 닫기 (자동채움. 확인용 public)
    int player_now_floor_num; //몇번째 층인가?(DontDestroy_Gamemanager에서 받아옴)
    int[] button_move = new int[5];
    //버튼 누를 시 이동하는 위치 
    bool is_show;

    public int now_floor;

    private bool is_pressed_button = false; //버튼을 눌렀는가?
    public Text Talk_Text; // 텍스트 뜨는 것
    Text Name_Text; //이름 UI
    GameObject Talk; // 텍스트 박스 UI
    GameObject Item_Talk_Panel; //모아놓는 곳
    public string[] message;
    public int message_num = 0;
    bool is_show_bg = false;

    GameObject Story_Talk_Panel; //Talk_GameManager 있는 오브젝트

    private void OnValidate()
    {
        for (int i = 0; i <= 6; i++)
        {
            move_room_button[i] = move_room.transform.GetChild(i);
        }


    }

    // Start is called before the first frame update
    void Start()
    {
        show_all_button();

        Talk_Text = GameObject.Find("Canvas").transform.Find("Item_Talk_Panel").transform.Find("Talk").transform.Find("Talk_Text").GetComponent<Text>();
        Talk = GameObject.Find("Canvas").transform.Find("Item_Talk_Panel").transform.Find("Talk").gameObject;
        Name_Text = GameObject.Find("Canvas").transform.Find("Item_Talk_Panel").transform.Find("Talk").transform.Find("Name").transform.Find("Name_Text").GetComponent<Text>();
        Item_Talk_Panel = GameObject.Find("Canvas").transform.Find("Item_Talk_Panel").gameObject;

        Story_Talk_Panel = GameObject.Find("Find_Stroy").transform.Find("Story_Canvas").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && is_pressed_button && is_show_bg) //아이템을 이미 클릭했고 창이 켜져 있는 상태로 왼쪽 마우스 버튼 클릭
        {
            // 텍스트 내용 변경
            if (message_num+1 >= message.Length) //다음 내용이 없으면 종료
            {
                message_num = 0;
                Reset_Text();
            }
            else
            {
                message_num++;
                Talk_Text.text = message[message_num]; //다음 내용이 있다면 넘어감
            }
        }
    }
    void Reset_Text()
    {
        // 텍스트 박스 UI 둘 다 비활성화
        if (Talk != null)
        {
            is_pressed_button = false;
            is_show_bg = false;
            Talk.SetActive(is_show_bg);
            Item_Talk_Panel.SetActive(is_show_bg);
        }
    }

    public void show_button()
    {
        is_show = !is_show;
        move_room.SetActive(is_show);
    }

    public void show_button_move() //Move버튼 창 켜졌을 시 작동 안하게 처리
    {
        if (!is_show)
        {
            is_show = !is_show;
            move_room.SetActive(is_show);
            show_all_button();
        }

    }

    void Room_Lock()
    {
        is_pressed_button = true;
        message = new string[1];
        Name_Text.text = "현";
        message[0] = "여기엔 아무도 없네. 다른 방도 더 찾아보자.";
        is_show_bg = true;
        Talk_Text.text = message[0];
        Talk.SetActive(is_show_bg);
        Item_Talk_Panel.SetActive(is_show_bg);

    }
    void Hallway()//복도 나옴
    {
        if (!Tutorial_information.Tutorial_instance.is_frist_walk)
        {
            Story_Talk_Panel.GetComponent<GameManager_Talk>().Talk_Start(3);
            Tutorial_information.Tutorial_instance.is_frist_walk = true;
        }

    }

    void Find_She()//3층 1번방,301호 화장실 - 예린, 서연 발견
    {
        if (!Tutorial_information.Tutorial_instance.clear_mini_tuto[2])
        {
            Story_Talk_Panel.GetComponent<GameManager_Talk>().Talk_Start(4);
            Tutorial_information.Tutorial_instance.clear_mini_tuto[2] = true;
        }
           
    }

    void Find_He()//4층 4번방,402호 주방 - 에이든 발견
    {
        if (!Tutorial_information.Tutorial_instance.clear_mini_tuto[1])
        {
            Story_Talk_Panel.GetComponent<GameManager_Talk>().Talk_Start(5);
            Tutorial_information.Tutorial_instance.clear_mini_tuto[1] = true;
        }
           
    }

    void Find_All()//2층 2번방, 작은방-전체 발견 후 본 게임 시작
    {
        //배열이 모두 true인지 판정
        if(Array.IndexOf(Tutorial_information.Tutorial_instance.clear_mini_tuto,false)==-1)
        {
            Story_Talk_Panel.GetComponent<GameManager_Talk>().Talk_Start(6);
            Destroy(Tutorial_information.Tutorial_instance.gameObject);//튜토리얼 인포메이션 삭제
        }
           
    }



    void show_all_button() //버튼 누를때마다 호출. 카메라 이동, 이동 버튼 띄움
    {
        Move_Camera();
        player_now_floor_num = DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_floor_num;
        switch (player_now_floor_num)
        {
            case 2:
                Map_Button_player_now_floor_2();
                break;
            case 3:
                Map_Button_player_now_floor_3();
                break;
            case 4:
                Map_Button_player_now_floor_4();
                break;
        }
    }

    void Map_Button_player_now_floor_2()   //방 몇 개를 이동할 수 있는가에 따른 버튼 띄우기(2층)
    {
        int i = 0;
        for (; i < player_now_floor_move_2() + 1; i++)
        {
            move_room_button[i].gameObject.SetActive(true);
        }
        for (; i < move_room_button.Length - 1; i++)
        {
            move_room_button[i].gameObject.SetActive(false);
        }
        Move_Button_text();
    }
    void Map_Button_player_now_floor_3()   //방 몇 개를 이동할 수 있는가에 따른 버튼 띄우기(3층)
    {
        int i = 0;
        for (; i < player_now_floor_move_3() + 1; i++)
        {
            move_room_button[i].gameObject.SetActive(true);
        }
        for (; i < move_room_button.Length - 1; i++)
        {
            move_room_button[i].gameObject.SetActive(false);
        }
        Move_Button_text();
    }
    void Map_Button_player_now_floor_4()   //방 몇 개를 이동할 수 있는가에 따른 버튼 띄우기(4층)
    {
        int i = 0;
        for (; i < player_now_floor_move_4() + 1; i++)
        {
            move_room_button[i].gameObject.SetActive(true);
        }
        for (; i < move_room_button.Length - 1; i++)
        {
            move_room_button[i].gameObject.SetActive(false);
        }
        Move_Button_text();
    }

    void Move_Camera() //방 이동
    {
        Camera.main.transform.position = new Vector3((DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num - 1) * 2000,
            (DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_floor_num - 1) * 1100, -10);
        //x축-방 번호 y-층 번호
    }

    void Move_Button_text()
    {
        switch (player_now_floor_num)
        { 
            case 2:
                switch (DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num) //방 번호(1번~11번까지 존재)
                {
                    case 1: //201호 주방
                        move_room_button[1].GetComponentInChildren<Text>().text = "201호 거실";//2층 9번방
                        move_room_button[2].GetComponentInChildren<Text>().text = "201호 작은방";//2층 2번방 
                        break;
                    case 2://201호 작은방
                        move_room_button[1].GetComponentInChildren<Text>().text = "201호 거실";//2층 9번방
                        move_room_button[2].GetComponentInChildren<Text>().text = "201호 주방"; //2층 1번방
                        break;
                    case 3://202호 작은방
                        move_room_button[1].GetComponentInChildren<Text>().text = "202호 거실";//2층 11번방
                        move_room_button[2].GetComponentInChildren<Text>().text = "202호 주방"; //2층 4번방
                        break;
                    case 4://202호 주방
                        move_room_button[1].GetComponentInChildren<Text>().text = "202호 거실";//2층 11번방
                        move_room_button[2].GetComponentInChildren<Text>().text = "202호 작은방"; //2층 3번방
                        break;
                    case 5://201호 큰방
                        move_room_button[1].GetComponentInChildren<Text>().text = "201호 거실";//2층 9번방
                        break;
                    case 6://201호 화장실
                        move_room_button[1].GetComponentInChildren<Text>().text = "201호 거실";//2층 9번방
                        break;
                    case 7: //202호 화장실
                        move_room_button[1].GetComponentInChildren<Text>().text = "202호 거실";//2층 11번방
                        break;
                    case 8: //202호 큰방
                        move_room_button[1].GetComponentInChildren<Text>().text = "202호 거실";//2층 11번방
                        break;
                    case 9: //201호 거실
                        move_room_button[1].GetComponentInChildren<Text>().text = "201호 주방"; //2층 1번방
                        move_room_button[2].GetComponentInChildren<Text>().text = "201호 작은방";//2층 2번방
                        move_room_button[3].GetComponentInChildren<Text>().text = "201호 큰방"; //2층 5번방
                        move_room_button[4].GetComponentInChildren<Text>().text = "201호 화장실"; //2층 6번방
                        move_room_button[5].GetComponentInChildren<Text>().text = "복도";//2층 10번방
                        break;
                    case 10: //복도
                        move_room_button[1].GetComponentInChildren<Text>().text = "201호 거실";//2층 9번방
                        move_room_button[2].GetComponentInChildren<Text>().text = "202호 거실";//2층 11번방
                        move_room_button[3].GetComponentInChildren<Text>().text = "1층";//1층 10번방
                        move_room_button[4].GetComponentInChildren<Text>().text = "3층";//3층 8번방
                        break;
                    case 11://202호 거실
                        move_room_button[1].GetComponentInChildren<Text>().text = "202호 작은방"; //2층 3번방
                        move_room_button[2].GetComponentInChildren<Text>().text = "202호 주방"; //2층 4번방
                        move_room_button[3].GetComponentInChildren<Text>().text = "202호 화장실"; //2층 7번방
                        move_room_button[4].GetComponentInChildren<Text>().text = "202호 큰방"; //2층 8번방
                        move_room_button[5].GetComponentInChildren<Text>().text = "복도";//2층 10번방
                        break;
                    default:
                        return;

                }
                break;
            case 3:
                switch (DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num) //방 번호(1번~9번까지 존재)
                {
                    case 1: //301호 욕실&화장실
                        move_room_button[1].GetComponentInChildren<Text>().text = "301호 거실";//3층 7번방
                        move_room_button[2].GetComponentInChildren<Text>().text = "301호 큰방";//3층 3번방 
                        break;
                    case 2://302호 욕실&화장실
                        move_room_button[1].GetComponentInChildren<Text>().text = "302호 거실";//3층 9번방
                        move_room_button[2].GetComponentInChildren<Text>().text = "302호 큰방";//3층 6번방 
                        break;
                    case 3://301호 큰방
                        move_room_button[1].GetComponentInChildren<Text>().text = "301호 거실";//3층 7번방
                        break;
                    case 4://301호 주방
                        move_room_button[1].GetComponentInChildren<Text>().text = "301호 거실";//3층 7번방
                        break;
                    case 5://302호 주방
                        move_room_button[1].GetComponentInChildren<Text>().text = "302호 거실";//3층 9번방
                        break;
                    case 6://302호 큰방
                        move_room_button[1].GetComponentInChildren<Text>().text = "302호 거실";//3층 9번방
                        break;
                    case 7:
                        //301호 거실
                        move_room_button[1].GetComponentInChildren<Text>().text = "301호 욕실&화장실"; //3층 1번방
                        move_room_button[2].GetComponentInChildren<Text>().text = "301호 큰방"; //3층 3번방
                        move_room_button[3].GetComponentInChildren<Text>().text = "301호 주방";//3층 4번방
                        move_room_button[4].GetComponentInChildren<Text>().text = "복도";//3층 8번방
                        break;
                    case 8:
                        //복도
                        move_room_button[1].GetComponentInChildren<Text>().text = "301호 거실";//3층 7번방
                        move_room_button[2].GetComponentInChildren<Text>().text = "302호 거실";//3층 9번방
                        move_room_button[3].GetComponentInChildren<Text>().text = "2층";//2층 10번방
                        move_room_button[4].GetComponentInChildren<Text>().text = "4층";//4층 10번방
                        break;

                    case 9:
                        //302호 거실
                        move_room_button[1].GetComponentInChildren<Text>().text = "302호 욕실&화장실"; //3층 2번방
                        move_room_button[2].GetComponentInChildren<Text>().text = "302호 주방"; //3층 5번방
                        move_room_button[3].GetComponentInChildren<Text>().text = "302호 큰방";//3층 6번방
                        move_room_button[4].GetComponentInChildren<Text>().text = "복도";//3층 8번방
                        break;

                    default:
                        return;

                }
                break;
            case 4:
                switch (DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num) //방 번호(1번~11번까지 존재)
                {
                    case 1: //401호 발코니
                        move_room_button[1].GetComponentInChildren<Text>().text = "401호 거실";//4층 9번방
                        break;
                    case 2://401호 화장실
                        move_room_button[1].GetComponentInChildren<Text>().text = "401호 거실";//4층 9번방
                        break;
                    case 3://401호 주방
                        move_room_button[1].GetComponentInChildren<Text>().text = "401호 거실";//4층 9번방
                        break;
                    case 4://402호 주방
                        move_room_button[1].GetComponentInChildren<Text>().text = "402호 거실";//4층 11번방
                        break;
                    case 5://402호 화장실
                        move_room_button[1].GetComponentInChildren<Text>().text = "402호 거실";//4층 11번방
                        break;
                    case 6://402호 발코니
                        move_room_button[1].GetComponentInChildren<Text>().text = "402호 거실";//4층 11번방
                        break;
                    case 7://401호 큰방
                        move_room_button[1].GetComponentInChildren<Text>().text = "401호 거실";//4층 9번방
                        break;
                    case 8://402호 큰방
                        move_room_button[1].GetComponentInChildren<Text>().text = "402호 거실";//4층 11번방
                        break;
                    case 9: //401호 거실
                        move_room_button[1].GetComponentInChildren<Text>().text = "401호 발코니"; //4층 1번방
                        move_room_button[2].GetComponentInChildren<Text>().text = "401호 화장실";//4층 2번방
                        move_room_button[3].GetComponentInChildren<Text>().text = "401호 주방"; //4층 3번방
                        move_room_button[4].GetComponentInChildren<Text>().text = "401호 큰방"; //4층 7번방
                        move_room_button[5].GetComponentInChildren<Text>().text = "복도";//4층 10번방
                        break;
                    case 10: //복도
                        move_room_button[1].GetComponentInChildren<Text>().text = "401호 거실";//4층 9번방
                        move_room_button[2].GetComponentInChildren<Text>().text = "402호 거실";//4층 11번방
                        move_room_button[3].GetComponentInChildren<Text>().text = "3층";//3층 8번방
                        break;
                    case 11://402호 거실
                        move_room_button[1].GetComponentInChildren<Text>().text = "402호 주방"; //4층 4번방
                        move_room_button[2].GetComponentInChildren<Text>().text = "402호 화장실";//4층 5번방
                        move_room_button[3].GetComponentInChildren<Text>().text = "402호 발코니"; //4층 6번방
                        move_room_button[4].GetComponentInChildren<Text>().text = "402호 큰방"; //4층 8번방
                        move_room_button[5].GetComponentInChildren<Text>().text = "복도";//4층 10번방
                        break;
                    default:
                        return;

                }
                break;
        }

    }


    public void Button1_Move() //버튼 클릭 시 방 이동
    {
        switch (player_now_floor_num)
        {
            case 2:
                //2층 현재 방 번호(1번~ 8번까지 방, 9 로비 1,11 로비2 ,10번 복도)/2,9,10
                switch (DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num)
                {
                    case 1:
                        DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num = 9;
                        break;
                    case 2:
                        DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num = 9;
                        break;
                    case 5:
                        DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num = 9;
                        break;
                    case 6:
                        DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num = 9;
                        break;
                    case 10:
                        DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num = 9;
                        break;
                    default:
                        Room_Lock();
                        break;

                }
                break;
            case 3:
                //3층 현재 방 번호(1번~ 6번까지 방, 7 로비 1,9 로비2 ,8번 복도)/1,7,8
                switch (DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num)
                {
                    case 1:
                        DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num = 7;
                        break;
                    case 3:
                        DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num = 7;
                        break;
                    case 4:
                        DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num = 7;
                        break;
                    case 7:
                        DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num = 1;
                        Find_She();
                        break;
                    case 8:
                        DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num = 7;
                        break;
                    default:
                        Room_Lock();
                        break;
                }
                break;
            case 4:
                //4층 현재 방 번호(1번~ 8번까지 방, 9 로비 1,11 로비2 ,10번 복도)/4,10,11
                switch (DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num)
                {
                    case 4:
                        DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num = 11;
                        break;
                    case 5:
                        DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num = 11;
                        break;
                    case 6:
                        DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num = 11;
                        break;
                    case 8:
                        DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num = 11;
                        break;
                    case 11:
                        DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num = 4;
                        Find_He();
                        break;
                    default:
                        Room_Lock();
                        break;

                }
                break;
        }

        show_all_button();

    }

    public void Button2_Move()
    {
        switch (player_now_floor_num)
        {
            case 2:
                //2층 현재 방 번호(1번~ 8번까지 방, 9 로비 1,11 로비2 ,10번 복도)/2,9,10
                switch (DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num)
                {
                    case 1:
                        DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num = 2;
                        Find_All();
                        break;
                    case 9:
                        DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num = 2;
                        Find_All();
                        break;
                    default:
                        Room_Lock();
                        break;

                }
                break;
            case 3:
                //3층 현재 방 번호(1번~ 6번까지 방, 7 로비 1,9 로비2 ,8번 복도)/1,7,8
                Room_Lock();
                break;
            case 4:
                //4층 현재 방 번호(1번~ 8번까지 방, 9 로비 1,11 로비2 ,10번 복도)/4,10,11
                switch (DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num)
                {

                    case 10:
                        DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num = 11;
                        break;
                    default:
                        Room_Lock();
                        break;

                }
                break;
        }

        show_all_button();
    }

    public void Button3_Move()
    {
        switch (player_now_floor_num)
        {
           
            case 2:
                //2층 현재 방 번호(1번~ 8번까지 방, 9 로비 1,11 로비2 ,10번 복도)/2,9,10
                switch (DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num)
                {

                    case 10:
                        is_pressed_button = true;
                        Name_Text.text = "현";
                        message = new string[2];
                        message[0] = "1층엔 피해자들을 가두지 않았을 거야.";
                        message[1] = "다른 층 먼저 찾아보자.";
                        is_show_bg = true;
                        Talk_Text.text = message[message_num];
                        Talk.SetActive(is_show_bg);
                        Item_Talk_Panel.SetActive(is_show_bg);
                        //2층에서는 1층으로 내려갈 수 없음.
                        break;

                    default:
                        Room_Lock();
                        break;

                }
                break;
            case 3:
                //3층 현재 방 번호(1번~ 6번까지 방, 7 로비 1,9 로비2 ,8번 복도)/1,7,8
                switch (DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num)
                {

                    case 8:
                        DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_floor_num = 2;
                        DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num = 10;
                        break;

                    default:
                        Room_Lock();
                        break;
                }
                break;

            case 4:
                //4층 현재 방 번호(1번~ 8번까지 방, 9 로비 1,11 로비2 ,10번 복도)/4,10,11
                switch (DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num)
                {

                    case 10:
                        DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_floor_num = 3;
                        DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num = 8;
                        break;

                    default:
                        Room_Lock();
                        break;
                }
                break;
        }
        

        show_all_button();
    }

    public void Button4_Move()
    {
        switch (player_now_floor_num)
        {
            case 2:
                //2층 현재 방 번호(1번~ 8번까지 방, 9 로비 1,11 로비2 ,10번 복도)/2,9,10
                switch (DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num)
                {
                    case 10:
                        DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_floor_num = 3;
                        DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num = 8;
                        break;
                    default:
                        Room_Lock();
                        break;
                }
                break;

            case 3:
                //3층 현재 방 번호(1번~ 6번까지 방, 7 로비 1,9 로비2 ,8번 복도)/1,7,8
                switch (DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num)
                {
                    case 7:
                        DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num = 8;
                        break;
                    case 8:
                        DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_floor_num = 4;
                        DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num = 10;
                        break;
                    case 9:
                        DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num = 8;
                        break;
                    default:
                        Room_Lock();
                        break;

                }
                break;
            case 4:
                //4층 현재 방 번호(1번~ 8번까지 방, 9 로비 1,11 로비2 ,10번 복도)/4,10,11
                Room_Lock();
                break;
        }

        show_all_button();
    }

    public void Button5_Move()
    {
        switch (player_now_floor_num)
        {
        
            case 2:
                //2층 현재 방 번호(1번~ 8번까지 방, 9 로비 1,11 로비2 ,10번 복도)/2,9,10
                switch (DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num)
                {

                    case 9:
                        DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num = 10;
                        Hallway();
                        break;
                    case 11:
                        DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num = 10;
                        break;
                    default:
                        Room_Lock();
                        break;

                }
                break;
            case 3:
                break;
            case 4:
                //4층 현재 방 번호(1번~ 8번까지 방, 9 로비 1,11 로비2 ,10번 복도)/4,10,11
                switch (DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num)
                {

                    case 9:
                        DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num = 10;
                        break;
                    case 11:
                        DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num = 10;
                        break;
                    default:
                        Room_Lock();
                        break;

                }
                break;
        }

        show_all_button();
    }


    public int player_now_floor_move_2() //버튼을 띄우는 개수
    {
        switch (DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num) //방 번호(1번~11번까지 존재)
        {
            case 1: //방 1
                return 2;
            case 2:
                return 2;
            case 3:
                return 2;
            case 4:
                return 2;
            case 5:
                return 1;
            case 6:
                return 1;
            case 7:
                return 1;
            case 8:
                return 1;
            case 9: //로비 1
                return 5;
            case 10: //복도
                return 4;
            case 11: //로비 2
                return 5;
            default:
                return 0;

        }

    }

    public int player_now_floor_move_3() //버튼을 띄우는 개수
    {
        switch (DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num) //방 번호(1번~9번까지 존재)
        {
            case 1: //방 1
                return 2;
            case 2:
                return 2;
            case 3:
                return 1;
            case 4:
                return 1;
            case 5:
                return 1;
            case 6:
                return 1;
            case 7: //로비 1
                return 4;
            case 8:
                return 4;
            case 9: //로비 2
                return 4;
            default:
                return 0;

        }

    }

    public int player_now_floor_move_4() //버튼을 띄우는 개수
    {
        switch (DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num) //방 번호(1번~11번까지 존재)
        {
            case 1: //방 1
                return 1;
            case 2:
                return 1;
            case 3:
                return 1;
            case 4:
                return 1;
            case 5:
                return 1;
            case 6:
                return 1;
            case 7:
                return 1;
            case 8:
                return 1;
            case 9: //로비 1
                return 5;
            case 10: //복도
                return 3;
            case 11: //로비 2
                return 5;
            default:
                return 0;


        }

    }
}
