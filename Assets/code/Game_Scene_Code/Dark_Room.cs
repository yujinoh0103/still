using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dark_Room : MonoBehaviour
{
    [Header("카메라 밑에 있는 이미지(배경)")]
    public GameObject Background_image; //거실 어둡게 하는용.컬러만 변경(UI로 처리하면 클릭 씹힘 처리 때문)
    [Header("전기 미니게임 입장 버튼")]
    public GameObject electricty; //검은 것 위에 오브젝트를 올려야 하므로 따로 처리
    [Header("클릭 시 대사")]
    public GameObject Talk_Dark; //어두워지면 무조건 한 대사만 나오도록 처리
    bool livingroom_move = false;

    public Color[] back_color = new Color[2];

    // Start is called before the first frame update
    void Start()
    {
        back_color[0] = Color.white;
        back_color[1] = new Color(60/255f, 60/255f, 60 / 255f);
    }

    // Update is called once per frame
    void Update()
    {
        Dark_Room_Image();
    }
    //1층 방 어둡게 하는 용도
    void Dark_Room_Image()
    {

        if (DontDestroy_Gamemanager.Dont_Destroy_Instance.clear_mini[2] && DontDestroy_Gamemanager.Dont_Destroy_Instance.clear_mini[3]) //모든 전기퍼즐 성공
        {
            Destroy(gameObject);
            Background_image.GetComponent<SpriteRenderer>().color = back_color[0];
        }
        else
        {
            if (DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_floor_num == 1) //1층
            {
                switch (DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num) //거실
                {
                    case 1: //101호
                    case 2: //101호
                    case 5: //101호
                    case 6: //101호
                    case 9: //101호 거실
                        if (DontDestroy_Gamemanager.Dont_Destroy_Instance.clear_mini[2])
                        {
                            Talk_Dark.SetActive(false);
                            electricty.SetActive(false);
                            Background_image.GetComponent<SpriteRenderer>().color = back_color[0];
                        }
                        else
                        {
                            Talk_Dark.SetActive(true);
                            Background_image.GetComponent<SpriteRenderer>().color = back_color[1];
                        }
                        break;

                    case 3: //102호
                    case 4: //102호
                    case 7: //102호
                    case 8: //102호
                    case 11: //102호 거실
                        if (DontDestroy_Gamemanager.Dont_Destroy_Instance.clear_mini[3])
                        {
                            Talk_Dark.SetActive(false);
                            electricty.SetActive(false);
                            Background_image.GetComponent<SpriteRenderer>().color = back_color[0];
                        }
                        else
                        {
                            Talk_Dark.SetActive(true);
                            Background_image.GetComponent<SpriteRenderer>().color = back_color[1];
                        }
                        break;
                    default:
                       Background_image.GetComponent<SpriteRenderer>().color = back_color[0];
                        Talk_Dark.SetActive(false);
                        electricty.SetActive(false);
                        break;
                }

            }
            else
            {
                Background_image.GetComponent<SpriteRenderer>().color = back_color[0];
                Talk_Dark.SetActive(false);
                electricty.SetActive(false);
            }

        }

    }

    //좌우 이동 화살표 버튼에 삽입. flase-전기 미니게임 안뜸
    public void Load_Image_livingroom_lift()
    {
        livingroom_move = false;
        electricty.SetActive(livingroom_move);
        Talk_Dark.GetComponent<BoxCollider2D>().size = new Vector2(1920, 1080);
        Talk_Dark.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
    }

    public void Load_Image_livingroom_right()
    {
        livingroom_move = true;
        electricty.SetActive(livingroom_move);
        Talk_Dark.GetComponent<BoxCollider2D>().size = new Vector2(1920, 790);
        Talk_Dark.GetComponent<BoxCollider2D>().offset = new Vector2(0, -150);
    }
}
