using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;
using UnityEditor.PackageManager;
using Unity.VisualScripting;

public class InGame_Load : MonoBehaviour
{

    public static string[] file_pos_name = new string[3]; //세이브파일 이름
    public static string[] file_name = new string[3]; //사용자 지정 세이브파일 이름
    public static int pos_num; //현재 클릭한 파일 번호, 저장 시 사용(Save_File_Pos)

    public GameObject load_check_panel; //로드 여부 묻는 창
    public GameObject load_end_panel; //저장 완료 창
    public Button button_Yes; //로드 선택 버튼

    public int pos; //파일 번호, 타이틀 로드 시 사용
    string path;

    private void Start()
    {
        file_pos_name[0] = "database1.json";
        file_pos_name[1] = "database2.json";
        file_pos_name[2] = "database3.json";

    }

    private void Update()
    {
        Print_Name();
        button_Yes.onClick.AddListener(InGameJsonLoad);
    }

    void Print_Name()
    {
        path = Path.Combine(Application.dataPath + "/Data/", file_pos_name[pos]);

        if (!File.Exists(path)) //데이터가 없다면 기본 이름 출력
        {
            gameObject.GetComponentInChildren<Text>().text = "빈 저장 슬롯 " + (pos + 1);
        }
        else
        {
            SaveData Save = new SaveData();

            string load_json = File.ReadAllText(path);
            Save = JsonUtility.FromJson<SaveData>(load_json);

            if (Save.file_name[pos] != null) //세이브데이터에 이름이 있다면
                gameObject.GetComponentInChildren<Text>().text = Save.file_name[pos]; //이름 띄움
        }
    }

    [System.Serializable]
    public class SaveData
    {
        //저장할 내역들

        public string[] file_name = new string[3]; //사용자 지정 세이브파일 이름

        public bool[] dead_human = new bool[4]; //사망 인원
                                                //아이템 획득 여부 판별, 각 층마다 3개씩 획득(지금은 임시로 토론 대사변경용 하나만)
        public bool[] item_check = new bool[1];

        [Header("GameManager_Talk에서 사용.(Talk scene)/Talk_Files_Num는 여러 곳에서 참조")]
        public int Talk_Files_Num;//현재 대사 파일.
        public int Talk_Num;//현재 대사 줄

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
    }
    public void LoadButton() //인게임 Yes 클릭 시 창 띄움
    {
        path = Path.Combine(Application.dataPath + "/Data/", file_pos_name[pos]);
        load_check_panel.SetActive(true);
    }

    public void No_Load()
    {
        load_check_panel.SetActive(false);
    }

    public void End_Load()
    {
        load_end_panel.SetActive(false);
    }


    public void InGameJsonLoad()
    {
        SaveData save_data = new SaveData();

        if (!File.Exists(path)) //데이터가 없다면 무시
        {
            gameObject.GetComponentInChildren<Text>().text = "해당 슬롯에는 세이브파일이 존재하지 않습니다.";
        }
        else
        {
            string load_json = File.ReadAllText(path);
            save_data = JsonUtility.FromJson<SaveData>(load_json);

            if (save_data != null)
            {
                for (int i = 0; i < save_data.dead_human.Length; i++)
                {
                    DontDestroy_Gamemanager.Dont_Destroy_Instance.dead_human[i] = save_data.dead_human[i];
                }

                for (int i = 0; i < save_data.item_check.Length; i++)
                {
                    DontDestroy_Gamemanager.Dont_Destroy_Instance.item_check[i] = save_data.dead_human[i];
                }

                for (int i = 0; i < save_data.player_before_visit.Length; i++)
                {
                    DontDestroy_Gamemanager.Dont_Destroy_Instance.player_before_visit[i] = save_data.player_before_visit[i];
                }

                for (int i = 0; i < save_data.clear_mini.Length; i++)
                {
                    DontDestroy_Gamemanager.Dont_Destroy_Instance.clear_mini[i] = save_data.clear_mini[i];
                }

                for (int i = 0; i < save_data.click_mini.Length; i++)
                {
                    DontDestroy_Gamemanager.Dont_Destroy_Instance.click_mini[i] = save_data.click_mini[i];
                }

                for (int i = 0; i < save_data.debate_clear.Length; i++)
                {
                    DontDestroy_Gamemanager.Dont_Destroy_Instance.debate_clear[i] = save_data.debate_clear[i];
                }

                DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Files_Num = save_data.Talk_Files_Num;
                DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num = save_data.Talk_Num;
                DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num = save_data.player_now_room_num;
                DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_floor_num = save_data.player_now_floor_num;

            }
            /*load_check_panel.SetActive(false);
            load_end_panel.SetActive(true);*/
            SceneManager.LoadScene("Game_Scene");

        }


    }

}