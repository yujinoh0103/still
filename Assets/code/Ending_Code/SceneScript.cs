using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private float pulseSpeed = 0.1f;
    private float maxScale = 1.05f;
    private float minScale = 1.0f;
    public bool isOpen;
    public string nextSceneName; // 씬 이름을 public 변수로 설정
    public RawImage childImage; // "Image" 타입의 변수명 수정

    private bool isPulsing = false;
    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void Update()
    {
        if (!isOpen) // "isOpen"이 false일 때 자식 이미지를 활성화, 엔딩 클리어 못 한 상태
        {
            if (childImage != null)
            {
                childImage.enabled = true; // 자식 이미지를 활성화
            }
        }

        else
        {
            if (childImage != null)
            {
                childImage.enabled = false; // 자식 이미지를 비활성화하여 숨김
            }
        }

        if (isPulsing&&isOpen)
        {
            float scale = Mathf.PingPong(Time.time * pulseSpeed, maxScale - minScale) + minScale;
            transform.localScale = originalScale * scale;
        }

        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPulsing = true;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPulsing = false;
        transform.localScale = originalScale;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isOpen)
        {
            SceneManager.LoadScene(nextSceneName); // nextSceneName 변수를 사용하여 씬 이동
        }
    }
}
