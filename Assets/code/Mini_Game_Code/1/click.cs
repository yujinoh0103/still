using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class click : MonoBehaviour
{
    //1스테이지

    //public GameObject max_clrcle;
    public GameObject clear;
    public GameObject click_text;
    public GameObject Talk, Talk_text,Name_text;
    int click_num = 35; //클리어에 필요한 클릭 횟수 
    int score = 0;
    public GameObject Button;
    public Sprite[] Button_image;

    // Update is called once per frame
    void Update()
    {
        click_text.GetComponent<Text>().text = score.ToString();
        Name_text.GetComponent<Text>().text = "현";
        switch (score)
        {
            case 1:
                Talk.SetActive(true);
                Talk_text.GetComponent<Text>().text = "윽, 이렇게 더 하면 될 것 같아.";
                break;
            case 10:
                Talk_text.GetComponent<Text>().text = "얼마나 힘을 줘야 하는 거지?";
                break;
            case 20:
                Talk_text.GetComponent<Text>().text = "곧 풀릴 것 같은데...";
                Button.GetComponent<Image>().sprite = Button_image[0];
                break;
        }
    }       

    public void click_button()
    {
        score++;

        if (score >= click_num)
        {
            Button.GetComponent<Image>().sprite = Button_image[1];
            Talk_text.GetComponent<Text>().text = "풀렸다!";
            clear.SetActive(true); //클리어 오브젝트 보이게 함
            DontDestroy_Gamemanager.Dont_Destroy_Instance.is_talk_start = true;
            SceneManager.LoadScene("Tutorial_Game_Scene");
        }
    }

    public void _click()
    {


        /*if (hit.transform.CompareTag("C")) //일반 클릭
        {
            score++;

            if (score >= 10)
            {
                clear.SetActive(true);
            }
        }
        if (hit.transform.CompareTag("click_circle"))  //원형 클릭
        {
            GameObject circle = hit.transform.gameObject;
            float size = circle.transform.localScale.x;
            if (max_clrcle.transform.localScale.x <= size)
            {
                clear.SetActive(true);
            }
        }*/

    } //무시
}