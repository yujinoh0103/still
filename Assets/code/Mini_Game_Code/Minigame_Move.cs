/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Minigame_Move : MonoBehaviour
{
    string[] mini_name = new string[10];
    [Header("미니게임 번호 입력")]
    public int mini_num;
    // Start is called before the first frame update
    void Start()
    {
        mini_name[5] = "Cat_MiniGame";
        mini_name[6] = "electricty_6";
        mini_name[7] = "mini7_Scene";
        mini_name[8] = "mini8_Scene";
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Click_Object>().message_num+1 >= gameObject.GetComponent<Click_Object>().message.Length)
        {
            SceneManager.LoadScene(mini_name[mini_num]);
        }
    }
}*/
