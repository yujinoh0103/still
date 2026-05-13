using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Titie_Code : MonoBehaviour
{
    public GameObject panel;
    public GameObject[] panel_child;

    private void Start()
    {
       
    }

    public void Exit_Button()
    {
        for (int i = 0; i < panel_child.Length; i++)
        {
            panel_child[i].SetActive(false);
        }
        panel.SetActive(false);
    }

    public void Start_Button()
    {
        panel.SetActive(true);
        panel_child[0].SetActive(true);
    }
    public void Start_Button_Yes()
    {
        SceneManager.LoadScene("Talk_Scene");
    }

    public void Save_Button()
    {
        panel.SetActive(true);
        panel_child[4].SetActive(true);
    }

    public void HowToPlay_Button()
    {
        panel.SetActive(true);
        panel_child[1].SetActive(true);
    }

    public void Ending_Button()
    {
        SceneManager.LoadScene("Ending_Scene");
    }

    public void Seting_Button()
    {
        panel.SetActive(true);
        panel_child[2].SetActive(true);
    }
    public void Game_Exit_Button()
    {
        panel.SetActive(true);
        panel_child[3].SetActive(true);
    }

    public void Game_Exit_Button_Yes()
    {
       Application.Quit();
    }
}

