using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    private float pulseSpeed = 0.1f;
    private float maxScale = 1.05f;
    private float minScale = 1.0f;
    private bool isPulsing = false;
    private Vector3 originalScale;

    // Example variable declarations (make sure to replace them with your actual variables)
    private bool isOpen = false;
    private SpriteRenderer childImage;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void Update()
    {

        if (isPulsing)
        {
            float scale = Mathf.PingPong(Time.time * pulseSpeed, maxScale - minScale) + minScale;
            transform.localScale = originalScale * scale;
        }
    }

    private void OnMouseDown()
    {
        mini2_GameManager.instance.OnKnifeClick();
    }

    // Correct method names for mouse enter and exit events
    private void OnMouseEnter()
    {
        isPulsing = true;
    }

    private void OnMouseExit()
    {
        isPulsing = false;
        transform.localScale = originalScale;
    }
}
