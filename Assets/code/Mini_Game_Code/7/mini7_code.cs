using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mini7_code : MonoBehaviour, IPointerClickHandler
{
    public InputField chack_InputField;
    public Text chack_text;
    public GameObject picture;
    public GameObject phone;
    public GameObject password;

    [Header("휴대폰 이미지(꺼짐/켜짐")]
    public Sprite[] phone_image;
    bool is_on_phone = false;
    bool is_on_password = false;

    void Start()
    {
        picture.SetActive(false); // 사진(갤러리)을 비활성화 한다.
        password.SetActive(false); // 비밀번호를 비활성화 한다.
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(!is_on_password)
            is_on_phone = !is_on_phone; // 마우스키를 누르면 휴대폰 이미지를 변경한다
        else
            password.SetActive(true);
    }
    void Update()
    {
        if (!is_on_phone)
        {
            phone.GetComponent<Image>().sprite = phone_image[0]; // 휴대폰 이미지를 변경한다(끔)
        }
        else
        {
            phone.GetComponent<Image>().sprite = phone_image[1]; // 휴대폰 이미지를 변경한다(켬)
            is_on_password = true; //다음 클릭 시 패스워드 창을 띄우기 위함
            
        }

        /*if (chack_InputField.text == "985617") // 플레이어가 정답을 맞추면 사진(갤러리)을 활성화 한다.
        {
            picture.SetActive(true);
        }*/

        if (chack_text.text == "123456") // 플레이어가 정답을 맞추면 사진(갤러리)을 활성화 한다.
        {
            picture.SetActive(true);
        }

    }
}
