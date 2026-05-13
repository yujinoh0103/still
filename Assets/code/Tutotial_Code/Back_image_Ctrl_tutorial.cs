using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//게임 배경들은 모두 카메라 밑의 스프라이트 오브젝트로 사용하므로 인스펙터에서 pixels per unit 조절 필요
public class Back_image_Ctrl_tutorial : MonoBehaviour
{
    public int player_now_room_num; //현재 방 위치. DontDestroy_GameManager에서 받아옴
    public int player_now_floor_num; //현재 층 위치. DontDestroy_GameManager에서 받아옴

    static int FLOOR = 5; //층수(0은 이미지 없음)
    static int FLOOR_ROOM = 12; //방 개수(3층은 8개지만 편의상 가장 큰 방에 맞춰 전부 동일하게 처리, 0은 이미지 없음)
    [SerializeField]
    public ROOM_MUN[] floor_image = new ROOM_MUN[FLOOR]; //띄울 이미지 2차원 배열(인스펙터 창에 보이게 하는 용도)
    //방 순서는 Game_Move와 동일
    [Serializable]
    public class ROOM_MUN
    {
        [SerializeField]
        public Sprite[] floor_room_image = new Sprite[FLOOR_ROOM]; //띄울 방 이미지 배열
    }

    [Header("방 물품 클릭 오브젝트")]
    public GameObject[] click_livingroom;
    public Sprite[] floor1_livingroom_image = new Sprite[4];//각각 2개의 이미지. 화살표 이동용
    public bool livingroom_move; //false가 기본 방 이미지 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Load_player_pos();
        Load_Image();
    }

    void Load_player_pos() //DontDestroy_Gamemanager에서 층, 위치 불러옴
    {
        player_now_room_num = DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num;
        player_now_floor_num = DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_floor_num;
    }

    void Load_Image()
    {
        if (floor_image[player_now_floor_num].floor_room_image[player_now_room_num] != null)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = floor_image[player_now_floor_num].floor_room_image[player_now_room_num];
            livingroom_move = false;
        }

    }

    public void Load_Image_livingroom_lift()
    {
        livingroom_move = false;
    }

    public void Load_Image_livingroom_right()
    {
        livingroom_move = true;
    }
}
