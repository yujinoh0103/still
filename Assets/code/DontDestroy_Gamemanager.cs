using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DontDestroy_Gamemanager;

public class DontDestroy_Gamemanager : MonoBehaviour
{
    //게임 최종본에는 게임 내에 하나만 넣을 것
    //전체적으로 게임 내에서 사용하는 변수를 넣음 (인벤토리는 오브젝트 자식으로 설정됨)

    public static DontDestroy_Gamemanager Dont_Destroy_Instance;

    public bool[] dead_human = new bool[4]; //사망 인원
    //현 - 에이든 - 예린 - 서연

    [Header("GameManager_Talk 사용.(Talk scene)")]
    //아이템 획득 여부 판별, 각 층마다 3개씩 획득(지금은 임시로 토론 대사변경용 하나만)
    public bool[] item_check = new bool[1];

    [Header("GameManager_Talk에서 사용.(Talk scene)/Talk_Files_Num는 여러 곳에서 참조")]
    public int Talk_Files_Num = 0;//현재 대사 파일.
    public string[] talk_name;//텍스트 파일
    public int Talk_Num = 0;//현재 대사 줄

    /*부울 변수. 이것이 활성화되면 GameManager_Talk에서 받아서 Talk_Start 실행.
     * GameManager_Talk가 없는 씬에서 실행 시 사용
     *만약 특정 번호의 대사를 출력하고 싶다면 따로 Talk_Files_Num을 변경 
     */
    public bool is_talk_start = false;
    public bool is_talk_start_sign = false;

    static int HUMAN_MAMBER = 7; //사람 명수
    static int HUMAN_FACE = 1; //사람 표정 갯수
    [SerializeField]
    public Human_Face[] Human_Mamber_image = new Human_Face[HUMAN_MAMBER]; //띄울 이미지 2차원 배열(인스펙터 창에 보이게 하는 용도)
    //캐릭터 순서는 GameManager_Talk의 Image_Change_Default() 스위치문과 동일
    [Serializable]
    public class Human_Face
    {
        [SerializeField]
        public Sprite[] Human_Face_image = new Sprite[HUMAN_FACE]; //띄울 캐릭터 얼굴 이미지 배열
    }
    [Header("배경 이미지 삽입")]
    public Sprite[] background_array; //배경
    [Header("Cg 이미지 삽입")]
    public Sprite[] cg_array; //배경
    [Header("노래 삽입")]
    public AudioClip[] sound_array;


    [Header("GameManager_Game, MiniGame_Button에서 사용. (Game_scene)")]
    public int player_now_room_num; //현재 방 위치
    public int player_now_floor_num; //몇번째 층인가?
    public bool[] player_before_visit = new bool[2]; //방문 이벤트 체크.
    //[0]=202호 작은방 확인 여부
    //[1]=202호 작은방 입장 여부

    [Header("Ending_Check에서 사용 (Ending_scene)")]
    public bool[] Ending_Open = new bool[6]; //본 엔딩 체크 변수. 봤으면 True로 변환

    [Header("MiniGame_Button에서 사용 (Game_scene)")]
    public bool[] clear_mini = new bool[10];//몇 개의 미니게임을 성공했는가? 각 순서는 스토리기획서와 동일(방 순서와는 무관)
    public bool[] click_mini = new bool[10];//몇 개의 미니게임을 클릭했는가? 각 순서는 스토리기획서와 동일(방 순서와는 무관/MiniGame_Click에서 사용)
    //2,3은 전기 퍼즐(각 방 따로)

    [Header("n_Last_Debate 사용 (Debate_scene)")]
    public bool[] debate_clear = new bool[4];//몇 개의 추리를 성공했는가?

    private void OnValidate()
    {
        talk_name = new string[20];
        talk_name[1] = @"\1_Start.txt";
        talk_name[2] = @"\2_Wakeup.txt";
        talk_name[3] = @"\3_hallway.txt";
        talk_name[4] = @"\4_Find_she.txt";
        talk_name[5] = @"\5_Find_he.txt";
        talk_name[6] = @"\6_Tutorial_End.txt";
        talk_name[7] = @"\7_Move_Floor.txt";
        talk_name[8] = @"\8_floor_1_click.txt";
        talk_name[9] = @"\9_floor_2_click.txt";
        talk_name[10] = @"\10_floor_3_click.txt";
        talk_name[11] = @"\11_floor_4_click.txt";
        talk_name[18] = @"\mini_game.txt";
        talk_name[19] = @"\n_Last_Debate.txt";
    }

    private void Awake()
    {
        if(Dont_Destroy_Instance == null) //아무것도 없다면
        {
            Dont_Destroy_Instance = this; //자기 자신을 인스턴스로 넣고 씬에서 제거되지 않음
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if(Dont_Destroy_Instance != this) //이미 인스턴스가 있고 자신이 아니라면
            {
                Destroy(gameObject); //자기 자신 제거
            }
        }

       
    }
    void Start()
    {
        player_now_room_num = 2;
        player_now_floor_num = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

}

