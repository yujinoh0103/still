using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImageToggle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private bool isPressed = false;
    Text textComponent; // 텍스트를 표시
    GameObject textBoxUI; // 텍스트 박스 UI
    Image obimage; // 이미지를 표시
    public Sprite newImage; // 활성화될 때 설정할 이미지
    public string[] message;
    public int message_num = 0;
    bool is_show_bg = false;

    private Sprite originalImage; // 원래 이미지 저장용 변수

    private void OnValidate() 
    {
        
    }

    private void Start()//모두 사용하는 Ui와 텍스트 자동입력
    {
        textComponent = GameObject.Find("Canvas").transform.Find("bg").transform.Find("text").transform.Find("Text_item").gameObject.GetComponent<Text>();
        textBoxUI = GameObject.Find("Canvas").transform.Find("bg").gameObject;
        obimage = GameObject.Find("Canvas").transform.Find("bg").transform.Find("objectimage").GetComponent<Image>();

        // 게임 시작 시 텍스트 박스 UI를 비활성화합니다.
        if (textBoxUI != null)
        {
            is_show_bg = false;
            textBoxUI.SetActive(is_show_bg);
        }

        // 초기 이미지를 저장합니다.
        if (obimage != null)
        {
            originalImage = obimage.sprite;
        }
    }

    private void Update()
    {
        // 마우스 왼쪽 버튼을 클릭했을 때
        if (Input.GetMouseButtonDown(0) && isPressed)
        {
            // 클릭 이벤트 처리
            isPressed = false;

            ResetText();


        }
        else if(Input.GetMouseButtonDown(0) && !isPressed && is_show_bg) //아이템을 이미 클릭했고 창이 켜져 있는 상태로 왼쪽 마우스 버튼 클릭
        {
            // 텍스트 내용 변경
           
            if (message_num >= message.Length) //다음 내용이 없으면 종료
            {
                message_num = 0;
                isPressed = true;

                ResetText();
            }
            else
            {
                textComponent.text = message[message_num]; //다음 내용이 있다면 넘어감
                message_num++;
            }
        }
    }

    void ResetText()
    {
        // 텍스트 박스 UI를 다시 비활성화합니다.
        if (textBoxUI != null)
        {
            is_show_bg = false;
            textBoxUI.SetActive(is_show_bg);
        }

        // 이미지를 원래 이미지로 복원합니다.
        if (obimage != null)
        {
            obimage.sprite = originalImage;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Mouse entered UI element");
        // 마우스가 UI 요소 위에 올라갔을 때 원하는 작업 수행
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Mouse exited UI element");
        // 마우스가 UI 요소를 벗어났을 때 원하는 작업 수행
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        // 텍스트 박스 UI를 활성화합니다.
        if (textBoxUI != null)
        {
            is_show_bg = !is_show_bg;
            textBoxUI.SetActive(is_show_bg);
        }

        // 이미지를 새 이미지로 설정합니다.
        if (obimage != null && newImage != null)
        {
            obimage.gameObject.SetActive(true);
            obimage.sprite = newImage;
        }
        else //만약 없다면 이미지 오브젝트 자체를 끔
        {
            obimage.gameObject.SetActive(false);
        }

        textComponent.text = message[0]; //맨 처음 문장 띄움
        message_num++;
    }
}
