using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MovingEnemy : MonoBehaviour
{
    float speed;
    Vector2 direction;
    RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        // 초기 랜덤한 속도와 방향 설정
        speed = Random.Range(5f, 10f);
        direction = Random.insideUnitCircle.normalized;
    }

    void Update()
    {
   
        // 오브젝트를 움직입니다.
        Move();
        // 오브젝트가 화면 밖에 있는지 확인합니다.
        CheckBoundaries();
    }

    void Move()
    {
        // 현재 방향과 지정된 속도로 오브젝트를 움직입니다.
        rectTransform.anchoredPosition += direction * speed * Time.deltaTime*80;
    }

    void CheckBoundaries()
    {
        // 오브젝트가 화면 경계를 벗어났는지 확인합니다.
        if (!IsObjectInScreen())
        {
            // 방향을 90도로 변경합니다.
            direction = Quaternion.Euler(0, 0, 90) * direction;

            // 새로운 랜덤 속도를 설정합니다.
            speed = Random.Range(10f, 15f);
        }
    }

    bool IsObjectInScreen()
    {
    
        // 오브젝트가 화면 경계 내에 있는지 확인합니다.
        return rectTransform.anchoredPosition.x > -835 && rectTransform.anchoredPosition.x < 835 &&
               rectTransform.anchoredPosition.y > -300 && rectTransform.anchoredPosition.y < 400;
    }
}
