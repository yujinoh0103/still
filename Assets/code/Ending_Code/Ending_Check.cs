using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending_Check : MonoBehaviour
{
    public SceneScript[] Ending_Script;
    public GameObject Canvas;
    private void OnValidate()
    {
        Ending_Script = Canvas.GetComponentsInChildren<SceneScript>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i =0;i<Ending_Script.Length;i++)
        {
            Ending_Script[i].isOpen = DontDestroy_Gamemanager.Dont_Destroy_Instance.Ending_Open[i];
        }

    }
}
