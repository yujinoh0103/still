using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//클릭 시 강제로 넘어가는 일회용 미니게임 오브젝트 제거
public class MiniGame_Click : MonoBehaviour
{
    public int mini_num;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DontDestroy_Gamemanager.Dont_Destroy_Instance.click_mini[mini_num]) //다시 게임 씬으로 돌아올 때 복구되므로 제거
            Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        DontDestroy_Gamemanager.Dont_Destroy_Instance.click_mini[mini_num] = true;
    }
}
