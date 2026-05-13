using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save_Scene : MonoBehaviour
{
    //획득 여부 검사/획득했다면 실행할 때마다 씬에서 제거
    //테스트시 이 스크립트가 존재하는 이전 씬에서 이동해와야 똑바로 작동함
    //테스트 씬에 존재하지 않아야 작동함.
    //튜토리얼 맵의 것은 Tutorial_information 쪽에 모두 몰아넣음
    static public Save_Scene Save_Scene_Item_Instance;

    private void Awake()
    {
        Save_Scene_Item_Instance = this;
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
