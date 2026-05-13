using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using static Save;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;
using UnityEditor.PackageManager;
using Unity.VisualScripting;

public class Save : MonoBehaviour
{

    public static string[] file_pos_name = new string[3]; //세이브파일 이름
    public static int pos_num; //현재 클릭한 파일 번호, 저장 시 사용(Save_File_Pos)
    public static string[] file_name = new string[3]; //사용자 지정 세이브파일 이름

    public int pos; //파일 번호, 타이틀 로드 시 사용
    public GameObject[] Save_Name_Inputfieid = new GameObject[3]; //이름 입력 칸, 띄우는 건 Save_File_pos에서 버튼 이용
    public string[] Save_Name = new string[3]; //저장 이름
    public bool[] Is_Show = new bool[3]; //이름 입력 칸 띄움

    public GameObject save_check_panel; //저장 여부 묻는 창
    public GameObject save_end_panel; //저장 완료 창
    public Button button_Yes; //세이브 버튼

    private void Start()
    {
        file_pos_name[0] = "database1.json";
        file_pos_name[1] = "database2.json";
        file_pos_name[2] = "database3.json";

        button_Yes.onClick.AddListener(Show_Inputfieid);
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

    string path;

    //데이터 무 : 이름 입력(Show_Inputfieid) -> 저장
    //데이터 유 : 저장 여부 확인 -> 이름 입력 -> 저장
    public void SaveButton() //버튼 클릭 시 저장(슬롯 버튼)
    {
        path = Path.Combine(Application.dataPath + "/Data/", file_pos_name[pos_num]);

        Debug.Log(pos_num);

        if (!File.Exists(path)) //데이터가 없다면 이름 입력
        {
            Show_Inputfieid();
        }
        else //있다면 덮어씌우는 창 띄우기
        {
            save_check_panel.SetActive(true);
        }
        
    }

    public void SaveButton_Yes() //버튼 클릭 시 저장
    {

        JsonSave(pos_num);
        save_check_panel.SetActive(false);
    }

    public void SaveButton_No() //창 끔
    {
        save_check_panel.SetActive(false);
    }

    public void Save_End_Button() //저장 완료, 창 끔
    {
        save_end_panel.SetActive(false);
    }

    public void JsonSave(int pos_num) //저장
    {
        SaveData save_data = new SaveData();

        for (int i = 0; i < save_data.file_name.Length; i++)
        {
            save_data.file_name[i] = file_name[i];
        }

        for (int i = 0; i < save_data.dead_human.Length; i++)
        {
            save_data.dead_human[i] = DontDestroy_Gamemanager.Dont_Destroy_Instance.dead_human[i];
        }

        for (int i = 0; i < save_data.item_check.Length; i++)
        {
            save_data.dead_human[i] = DontDestroy_Gamemanager.Dont_Destroy_Instance.item_check[i];
        }

        for (int i = 0; i < save_data.player_before_visit.Length; i++)
        {
            save_data.player_before_visit[i] = DontDestroy_Gamemanager.Dont_Destroy_Instance.player_before_visit[i];
        }

        for (int i = 0; i < save_data.clear_mini.Length; i++)
        {
            save_data.clear_mini[i] = DontDestroy_Gamemanager.Dont_Destroy_Instance.clear_mini[i];
        }

        for (int i = 0; i < save_data.click_mini.Length; i++)
        {
            save_data.click_mini[i] = DontDestroy_Gamemanager.Dont_Destroy_Instance.click_mini[i];
        }

        for (int i = 0; i < save_data.debate_clear.Length; i++)
        {
            save_data.debate_clear[i] = DontDestroy_Gamemanager.Dont_Destroy_Instance.debate_clear[i];
        }

        save_data.Talk_Files_Num = DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Files_Num;
        save_data.Talk_Num = DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num;
        save_data.player_now_room_num = DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num;
        save_data.player_now_floor_num = DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_floor_num;

        Debug.Log("num "+ pos_num);
        string json = JsonUtility.ToJson(save_data, true);
        File.WriteAllText(path, json);
        save_end_panel.SetActive(true);

    }

    public void Return_Save_name() //이름 넘김, 인풋 필드 옆의 확인 버튼 누르면 넘겨짐
    {
        Save_Name[pos_num] = Save_Name_Inputfieid[pos_num].GetComponentInChildren<Text>().text;
        if (Save_Name[pos_num] == null)
            Save_Name[pos_num] = "저장 슬롯 " + (pos_num + 1) + ": 저장됨";
        file_name[pos_num] = Save_Name[pos_num];

        if (file_name[pos_num] != null)
            gameObject.GetComponentInChildren<Text>().text = file_name[pos_num];

        Debug.Log(Save_Name);
        JsonSave(pos_num);
        Show_Inputfieid();
    }

    public void Show_Inputfieid()
    {
        save_check_panel.SetActive(false);
        Is_Show[pos_num] = !Is_Show[pos_num];
        Save_Name_Inputfieid[pos_num].SetActive(Is_Show[pos_num]);
        GameManager_Game.Game_GameMananger.is_Save_Input = Is_Show[pos_num];
    }


}

