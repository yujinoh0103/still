using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGame_Button : MonoBehaviour
{

    public int player_now_room_num; //현재 방 위치. DontDestroy_GameManager에서 받아옴
    public int player_now_floor_num; //현재 층 위치. DontDestroy_GameManager에서 받아옴
    public GameObject minigame_button;

    private void Update()
    {
        Show();
    }

    void Load_player_pos() //DontDestroy_Gamemanager에서 층, 위치 불러옴
    {
        player_now_room_num = DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num;
        player_now_floor_num = DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_floor_num;
    }
    public void MiniGame_Scene_Start()
    {
        Load_player_pos();
        switch (player_now_floor_num) //층수
        {
            case 1:
                switch (player_now_room_num) //방 번호
                {
                    case 7: //잠금화면 해제, 7번
                        SceneManager.LoadScene("mini7_Scene");
                        break;
                    case 9: //전선 연결, 6번
                        SceneManager.LoadScene("electricty_6");
                        break;

                    default:
                        
                        break;
                }
                break;

            case 2:
                switch (player_now_room_num)
                {
                
                    default:
                        break;
                }
                break;

            case 3:
                switch (player_now_room_num)
                {

                    case 6: //금고, 8번
                        SceneManager.LoadScene("mini8_Scene");
                        break;

                    default:
                     break;
                }
                break;

            case 4:
                switch (player_now_room_num)
                {

                    default:
                        break;
                }
                break;

            default:
                break;
        }
    
}

  /*  public void MiniGame_Scene_Open() 
    {
        if (!DontDestroy_Gamemanager.Dont_Destroy_Instance.clear_mini[3])
            SceneManager.LoadScene("");
    }*/
    public void Show()
    {
        Load_player_pos();
        switch (player_now_floor_num)
        {
            case 1:
                switch(player_now_room_num)
                {
                    case 7:
                    case 9:
                        minigame_button.SetActive(true);
                        break;

                    default:
                        minigame_button.SetActive(false);
                        break;
                }
                break;

            case 2:
                switch (player_now_room_num)
                {
                  

                    default:
                        minigame_button.SetActive(false);
                        break;
                }
                break;

            case 3:
                switch (player_now_room_num)
                {
                    case 6:
                        minigame_button.SetActive(true);
                        break;

                    default:
                        minigame_button.SetActive(false);
                        break;
                }
                break;

            case 4:
                switch (player_now_room_num)
                {
                    default:
                        minigame_button.SetActive(false);
                        break;
                }
                break;
        }
    }
   
}
