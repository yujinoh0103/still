using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

public class Tutorial_Knife : MonoBehaviour
{
    private void Awake()
    {
        if (Tutorial_information.Tutorial_instance.Knife)
            Destroy(gameObject);
        //Background_image_tuto에 있는 건 칼을 주운 뒤 방 배경 변경을 위해 어웨이크 부분만 사용하기 위해 삽입함
    }

    private void OnMouseDown()
    {
        Tutorial_information.Tutorial_instance.Knife = true;
        SceneManager.LoadScene("2_mini");
    }
}
