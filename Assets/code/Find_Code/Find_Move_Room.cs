using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Find_Move_Room : MonoBehaviour
{
    //Find의 메인 카메라에 삽입

    int room_min = 0, room_max = 1,player_pos; 
    //모든 방은 7개(임시로 완성된 방까지만 입력)
    public GameObject[] move_room_button =new GameObject[2];
    //버튼 오브젝트 [0] = left [1] = right
    public GameObject[] move_room_list = new GameObject[7];
    //방은 UI 묶음으로 작성

    // Start is called before the first frame update
    void Start()
    {
        Move_Room_image();
    }

    // Update is called once per frame
    void Update()
    {
        Show_Move_Button();
    }

    //화살표 띄움
    void Show_Move_Button()
   {
        move_room_button[0].SetActive(true);
        move_room_button[1].SetActive(true);

        if (player_pos <= room_min)
       {
           move_room_button[0].SetActive(false);
       }
       else if (player_pos >= room_max)
       {
           move_room_button[1].SetActive(false);
       }

   }

    //이미지 변경(방 이동)
    void Move_Room_image() 
    {
        for (int i =0; i<move_room_list.Length && move_room_list[i] != null; i++) //list만큼 있고 이번 방이 null이 아닌 동안 반복
        {
            move_room_list[i].SetActive(false);
        }
        move_room_list[player_pos].SetActive(true);
    }

    //버튼

    public void Move_Button_Left()
    {
        player_pos--;
        Move_Room_image();
    }
    public void Move_Button_Right()
    {
        player_pos++;
        Move_Room_image();
    }

}
