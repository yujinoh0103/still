using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Click_Object : MonoBehaviour
{

    GameObject show_human_image; //왼쪽 이미지만 사용
    GameObject show_human_image_no; //오른쪽 이미지. 혹시 모를 경우를 위해 끄는 용도로만 사용

    GameObject Talk; // 텍스트 박스 UI
    GameObject Item_Talk_Panel; //모아놓는 곳

    Text Talk_Text; // 텍스트 뜨는 것
    Text Name_Text; //이름 UI

    public string[] message;
    public int message_num = 0;
    bool is_show_message = false;
    bool is_pressed_button = false;

    GameObject choice_background; // 선택지 UI
    Text button_1_text;
    Text button_2_text;

    bool is_no_click = false;
    void Start()
    {
        Talk = GameObject.Find("Canvas").transform.Find("Item_Talk_Panel").transform.Find("Talk").gameObject;
        Item_Talk_Panel = GameObject.Find("Canvas").transform.Find("Item_Talk_Panel").gameObject;

        show_human_image = GameObject.Find("Canvas").transform.Find("Item_Talk_Panel").transform.Find("Human_Image").transform.Find("image_lift").gameObject;
        show_human_image_no = GameObject.Find("Canvas").transform.Find("Item_Talk_Panel").transform.Find("Human_Image").transform.Find("image_lift").gameObject;

        Name_Text = GameObject.Find("Canvas").transform.Find("Item_Talk_Panel").transform.Find("Talk").transform.Find("Name").transform.Find("Name_Text").GetComponent<Text>();
        Talk_Text = GameObject.Find("Canvas").transform.Find("Item_Talk_Panel").transform.Find("Talk").transform.Find("Talk_Text").GetComponent<Text>();

        Hide_Human_image();


    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0) && is_show_message && !is_no_click) //창이 켜져 있는 상태로 왼쪽 마우스 버튼 클릭.                                                                                                       
        {
            if (is_pressed_button) //is_pressed_button->OnMouseDown과 동시클릭되어서 넣은 구문. 클릭 한 번을 막아줌. 넣지 않으면 창이 켜지자마자 꺼짐
            {
                Debug.Log(message.Length);
                if (message_num + 1 >= message.Length) //다음 내용이 없으면 클릭 시 종료
                {
                    Debug.Log("reset");
                    message_num = 0;
                    Reset_Text();
                }
                else
                {
                    message_num++;
                    Talk_item(); //다음 내용이 있다면 넘어감
                }
            }
            else //동시클릭 된 경우. 아무 일도 일어나지 않음
            {
                Debug.Log("OnMouseDown");
                is_pressed_button = true;
            }


        }


    }

    void Reset_Text()
    {
        // 텍스트 박스 UI 둘 다 비활성화
        if (Talk != null)
        {
            is_show_message = false;
            is_pressed_button = false;
            Item_Talk_Panel.SetActive(is_show_message);
            Hide_Human_image();
        }
    }

    private void OnMouseDown()
    {
        if (!is_show_message && !EventSystem.current.IsPointerOverGameObject()) //EventSystem.current.IsPointerOverGameObject()는 레이캐스트 타겟이 체크된 UI 클릭했는지 확인
        {
            is_show_message = true;
            Item_Talk_Panel.SetActive(is_show_message);
            Talk_item();
        }

    }

    void Talk_item()
    {

        if (message[message_num].StartsWith("!")) //만약 대사 첫 줄에 !가 있다면 사람 대사
        {

            Talk_Text.text = message[message_num].Substring(1, message[message_num].Length - 1);
            Image_Change_Default();

        }
        else
        {
            Name_Text.text = "";
            Talk_Text.text = message[message_num];
            Hide_Human_image();
        }
    }

    void Hide_Human_image() //사람 이미지 숨김
    {
        show_human_image.SetActive(false);
        show_human_image_no.SetActive(false);
    }

    void Image_Change_Default()
    {

        switch (DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_floor_num)
        {
            case 1:
                Name_Text.text = "현";
                break;
            case 2:
                Name_Text.text = "서연";
                break;
            case 3:
                Name_Text.text = "에이든";
                break;
            case 4:
                Name_Text.text = "예린";
                break;

        }

        switch (Name_Text.text)
        {
            case "현":
                show_human_image.SetActive(true);
                show_human_image.GetComponent<Image>().sprite = DontDestroy_Gamemanager.Dont_Destroy_Instance.Human_Mamber_image[0].Human_Face_image[0];
                show_human_image.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                break;

            case "에이든":
                show_human_image.SetActive(true);
                show_human_image.GetComponent<Image>().sprite = DontDestroy_Gamemanager.Dont_Destroy_Instance.Human_Mamber_image[1].Human_Face_image[0];
                show_human_image.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                break;

            case "예린":
                show_human_image.SetActive(true);
                show_human_image.GetComponent<Image>().sprite = DontDestroy_Gamemanager.Dont_Destroy_Instance.Human_Mamber_image[2].Human_Face_image[0];
                show_human_image.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                break;

            case "서연":
                show_human_image.SetActive(true);
                show_human_image.GetComponent<Image>().sprite = DontDestroy_Gamemanager.Dont_Destroy_Instance.Human_Mamber_image[3].Human_Face_image[0];
                show_human_image.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                break;


            default:
                Hide_Human_image();
                break;
        }


    }
}
