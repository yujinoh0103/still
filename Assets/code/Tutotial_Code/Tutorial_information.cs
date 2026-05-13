using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_information : MonoBehaviour
{
    //튜토리얼의 모든 정보 포함(DontDestroy_Gamemanager와 동일한 역할)

    static public Tutorial_information Tutorial_instance;

    public bool[] clear_mini_tuto = new bool[3];//튜토리얼에서 생존자를 모두 구출하고 합류했는가?
                                                //현 - 에이든 - 예린&서연

    public bool move;//이동이 가능해졌는가?
    public bool is_frist_walk;//처음 나왔는가?

    public bool fade;//페이드 반복 안하게
    public bool Knife;//칼 안 생기게

    private void Awake()
    {
        Tutorial_instance = this; //자기 자신을 인스턴스로 넣고 씬에서 제거되지 않음
        DontDestroyOnLoad(gameObject);
    }
}
