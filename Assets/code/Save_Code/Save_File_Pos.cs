using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Save_File_Pos : MonoBehaviour
{

    public int pos; //파일 번호
    GameObject Save_Name_Input;
    string Save_Name = null;
    bool Is_Show = false;

    public static string[] file_pos_name = new string[3]; //세이브파일 이름
    public static string[] file_name = new string[3]; //사용자 지정 세이브파일 이름
    string path;

    private void Start()
    {

        file_pos_name[0] = "database1.json";
        file_pos_name[1] = "database2.json";
        file_pos_name[2] = "database3.json";

        Save_Name_Input = gameObject.GetComponentInChildren<InputField>().gameObject;

        Is_Show = false;
        Save_Name_Input.SetActive(Is_Show);
    }

    private void Update()
    {
        Print_Name();
    }

    void Print_Name()
    {
        path = Path.Combine(Application.dataPath + "/Data/", file_pos_name[pos]);
       
        if (!File.Exists(path)) //데이터가 없다면 기본 이름 출력
        {
            gameObject.GetComponentInChildren<Text>().text = "빈 저장 슬롯 " + (pos+1);
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

    public void Return_Pos()
    { 
       Save.pos_num = pos;
    }

    public void Show_Inputfieid()
    {
        Is_Show = !Is_Show;
        Save_Name_Input.SetActive(Is_Show);
        GameManager_Game.Game_GameMananger.is_Save_Input = Is_Show;
    }

    [System.Serializable]
    public class SaveData
    {
        //불러올 내역

        public string[] file_name = new string[3]; //사용자 지정 세이브파일 이름
    }

}
