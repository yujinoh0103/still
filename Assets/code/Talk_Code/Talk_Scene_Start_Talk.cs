using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talk_Scene_Start_Talk : MonoBehaviour
{
    //토크 씬에서만 바로 출력되게 하기 위한 코드

    private void Awake()
    {
        gameObject.GetComponent<GameManager_Talk>().Talk_Start();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
