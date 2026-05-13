using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debate_Button : MonoBehaviour
{
    public static int i = 0;//코드 내에서 토론 동안 계속 유지

    // Start is called before the first frame update
    public void Click_Button1()
    {
        Debug.Log("click_check_1");
        switch(i)
        {
            case 0:
                DontDestroy_Gamemanager.Dont_Destroy_Instance.debate_clear[i] = false;
                break;
            case 1:
                DontDestroy_Gamemanager.Dont_Destroy_Instance.debate_clear[i] = false;
                break;
            case 2:
                DontDestroy_Gamemanager.Dont_Destroy_Instance.debate_clear[i] = true;
                break;
            case 3:
                DontDestroy_Gamemanager.Dont_Destroy_Instance.debate_clear[i] = true;
                break;
        }
       
        i++;
    }

    public void Click_Button2()
    {
        Debug.Log("click_check_2");
        switch (i)
        {
            case 0:
                DontDestroy_Gamemanager.Dont_Destroy_Instance.debate_clear[i] = true;
                break;
            case 1:
                DontDestroy_Gamemanager.Dont_Destroy_Instance.debate_clear[i] = true;
                break;
            case 2:
                DontDestroy_Gamemanager.Dont_Destroy_Instance.debate_clear[i] = false;
                break;
            case 3:
                DontDestroy_Gamemanager.Dont_Destroy_Instance.debate_clear[i] = false;
                break;
        }

        i++;
    }
}
