using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fadein : MonoBehaviour
{
    float color_num = 1;
    GameObject Story_Talk_Panel; //Talk_GameManager 있는 오브젝트

    private void Awake()
    {
        if (Tutorial_information.Tutorial_instance.fade)
            Destroy(gameObject);
    }

    private void Start()
    {
        Story_Talk_Panel = GameObject.Find("Story_Canvas").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("fadein", 0.2f);
    }

    void fadein()
    {
        Color fade = new Color(0,0,0, color_num);
        gameObject.GetComponent<Image>().color = fade;
        color_num -= 0.05f;
       
        if (color_num < 0)
        {
            if (!Tutorial_information.Tutorial_instance.fade)
            {
                Tutorial_information.Tutorial_instance.fade = true;
                Story_Talk_Panel.GetComponent<GameManager_Talk>().Talk_Start(2);
            }
            Destroy(gameObject);
        }
    }
}
