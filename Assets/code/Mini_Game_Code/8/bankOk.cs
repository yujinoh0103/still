using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bankOk : MonoBehaviour
{
    public GameObject okButton;
    public GameObject bankInside;
    public Text first;
    public Text second;
    public Text third;
    public Text fourth;

    [Header("회전속도 조절")]
    [SerializeField]
    [Range(1f, 100f)]
    float rotateSpeed = 50f; // 인스펙터 창에서 회전속도를 조절할 수 있다.

    public void okGood()
    {
        StartCoroutine("Fade");
    }

    IEnumerator Fade()
    {
        float f;

        for (f = 20f; f > 0; f -= 1f) // 초단위로 손잡이의 각도 조절
        {
            okButton.transform.Rotate(0, 0, Time.deltaTime * rotateSpeed, Space.Self);
            yield return null;
        }

        if (f == 0 && first.text == "0" && second.text == "0" && third.text == "0" && fourth.text == "0")
        {
            for (f = 15f; f > 0; f -= 1f) // 정답일 경우 손잡이가 더 돌아가면서
            {
                okButton.transform.Rotate(0, 0, Time.deltaTime * rotateSpeed, Space.Self);
                yield return null;
            }
            bankInside.SetActive(true); // 금고 내부 활성화
            okButton.SetActive(false); // 금고 손잡이 비활성화
        }
        else if (f == 0)
        {
            for (f = 20f; f > 0; f -= 1f) // 정답이 아닐 경우 손잡이가 다시 올라가도록 함
            {
                okButton.transform.Rotate(0, 0, -Time.deltaTime * rotateSpeed, Space.Self);
                yield return null;
            }
        }

        okButton.transform.localEulerAngles = new Vector3(0, 0, 0); // 각도가 틀어지는 오류를 예방
    }
}
