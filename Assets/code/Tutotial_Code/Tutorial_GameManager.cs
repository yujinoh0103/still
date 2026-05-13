using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Tutorial_GameManager : MonoBehaviour
{

    [Header("버튼 누를 때 뜨는 창 부모")]
    public GameObject panel;
    [Header("버튼 누를 때 뜨는 창들, 실행시 자동 채움")]
    public Transform[] panel_child;
    //0 - Esc 1 - inventory 2- map 3 - save 4 - title 5 - howtoplay
    bool[] is_show = new bool[4]; //뜨는 창 bool. 숫자는 위와 동일

    [Header("아이템 설명 텍스트")]
    public GameObject inventory_text;
    [Header("현재 방 번호 텍스트")]
    public GameObject room_num_text;
    public int player_now_room_num; //현재 방 위치. DontDestroy_GameManager에서 받아옴
    public int player_now_floor_num; //현재 층 위치. DontDestroy_GameManager에서 받아옴
    [Header("방 이동 버튼 모은 오브젝트(Move)")]//
    public GameObject move_room;



    private void OnValidate()
    {
      
    }

  
    void Load_player_pos() //DontDestroy_Gamemanager에서 층, 위치 불러옴
    {
        player_now_room_num = DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num;
        player_now_floor_num = DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_floor_num;
    }
    private void Start()
    {
        move_room.SetActive(Tutorial_information.Tutorial_instance.move);
         panel_child = new Transform[6];
        int i = 0;
        foreach (Transform child in panel.transform)
        {
            panel_child[i] = (child.GetComponentInChildren<Transform>());
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Load_player_pos();
        room_num_text.GetComponent<Text>().text = (player_now_floor_num).ToString() + "층 " + (player_now_room_num).ToString() + "번 방";
        //창 띄우기

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            is_show[0] = !is_show[0];
            panel_child[0].gameObject.SetActive(is_show[0]);
            //panel_child[3].gameObject.SetActive(false);
            panel_child[4].gameObject.SetActive(false);
            panel_child[5].gameObject.SetActive(false);
            panel.gameObject.SetActive(is_show[0]);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            is_show[2] = !is_show[2];
            panel_child[2].gameObject.SetActive(is_show[2]);
            panel.gameObject.SetActive(is_show[2]);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            is_show[1] = !is_show[1];
            panel_child[1].gameObject.SetActive(is_show[1]);
            panel.gameObject.SetActive(is_show[1]);
            inventory_text.GetComponent<Text>().text = "아이템 이름\n\n아이템 정보";
        }
    }

  

    public void Save_Button()
    {

    }

    public void Title_Button()
    {
        panel_child[4].gameObject.SetActive(true);
    }

    public void Title_Button_Yes()
    {
        SceneManager.LoadScene("Title_Scene");
    }

    public void Title_Button_No()
    {
        panel_child[4].gameObject.SetActive(false);
    }

    public void How_To_Play_Button()
    {
        panel_child[5].gameObject.SetActive(true);
    }
    public void How_To_Play_Button_No()
    {
        panel_child[5].gameObject.SetActive(false);
    }

    //Tutorial_slot 스크립트에서 사용. button 연결용

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
