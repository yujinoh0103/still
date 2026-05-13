using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Back_GameScene : MonoBehaviour
{
    public void Back_Scene() //미니게임에서 메인 스테이지로 복귀
    {
        SceneManager.LoadScene("Game_Scene");
    }

    public void Tutorial_Back_Scene() //미니게임에서 튜토리얼 메인 스테이지로 복귀
    {
        SceneManager.LoadScene("Tutorial_Game_Scene");
    }
}
