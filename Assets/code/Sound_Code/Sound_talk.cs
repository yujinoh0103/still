using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_talk : MonoBehaviour
{
    public AudioSource Audio;
    public int min;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void Audio_cut() //오디오 길이 자름
    {
        if (Audio != null)
        {
            Debug.Log(Audio.time);
            if (Audio.time >= min)
            {
                Audio.time = 0f;
                Audio.Play();
            }
        }
    }
}