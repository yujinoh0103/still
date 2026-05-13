using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial_Next_Scene : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<ImageToggle>().message_num >= gameObject.GetComponent<ImageToggle>().message.Length)
        {
            DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_floor_num = 2;
            DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num = 2;
            DontDestroy_Gamemanager.Dont_Destroy_Instance.is_talk_start = true;
            SceneManager.LoadScene("Tutorial_Game_Scene");
        }
    }
}
