using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class mini2_GameManager : MonoBehaviour
{
    public static mini2_GameManager instance; //싱글톤. 나이프 객체에서 함수 사용을 위함*

    public Text successText;
    public GameObject knife;
    public GameObject foot; // Foot 객체를 Inspector에서 지정

    public Sprite[] mini2_rope = new Sprite[5];
    private int clickCount;
    private int totalClick = 5;
    private bool isGameRunning = true;
    public GameObject Talk, Talk_text, Name_text;

    private void Awake()//*
    {
        instance = this;
    }

    private void Start()
    {
        successText.text = "남은 횟수: " + (totalClick - clickCount).ToString();
        Name_text.GetComponent<Text>().text = "현";
    }

    private void Update()
    {
    }

    

    public void OnKnifeClick()
    {
        clickCount++;
        if(clickCount >= 5)
            knife.SetActive(false);

        this.gameObject.GetComponent<SpriteRenderer>().sprite = mini2_rope[clickCount-1];
        switch (clickCount)
        {
            case 1:
                Talk.SetActive(true);
                Talk_text.GetComponent<Text>().text = "조금 더 세게 해야겠는걸.";
                break;
            case 5:
                Talk_text.GetComponent<Text>().text = "풀렸다!";
                break;
        }

        if (successText != null)
            successText.text = "남은 횟수: " + (totalClick-clickCount).ToString();

        if (clickCount >= totalClick)
        {
            EndGame();
        }
    }

   /* public void SetFootImage()
    {
        if (foot == null)
        {
            Debug.LogError("Foot GameObject is not assigned in the Inspector.");
            return;
        }

        if (clickCount == Mathf.RoundToInt(totalClick / 3f) * 2)
        {
            // i에 따라 숫자가 변화되며 이미지를 로드합니다.

            this.gameObject.GetComponent<SpriteRenderer>().sprite = mini2_rope[1];

        }
        else if (clickCount == Mathf.RoundToInt(totalClick / 3f))
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = mini2_rope[2];
        }
        else if (clickCount == 0)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = mini2_rope[3];
        }
    }*/

    private void EndGame()
    {
        Debug.Log(clickCount);
        if (successText != null && clickCount== totalClick)
        {
            successText.text = "Success!";
            
            if (!Tutorial_information.Tutorial_instance.clear_mini_tuto[0])
            {
                Tutorial_information.Tutorial_instance.move = true;
                Tutorial_information.Tutorial_instance.clear_mini_tuto[0] = true;
            }
          
            SceneManager.LoadScene("Tutorial_Game_Scene");
        }
        else
        {
            successText.text = "Fail";
        }

        if (knife != null)
            knife.GetComponent<SpriteRenderer>().sortingOrder = 0;

        this.gameObject.GetComponent<SpriteRenderer>().sprite = mini2_rope[4];
    }
}

