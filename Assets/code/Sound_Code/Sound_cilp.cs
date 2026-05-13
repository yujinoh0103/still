using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_cilp : MonoBehaviour
{

    public AudioSource Audio;
    public int min = 0; //재생할 초 수
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Audio != null)
        {
            //Debug.Log(Audio.time);
            if(Audio.time>=min)
            {
                Audio.time = 0f;
                Audio.Play();
            }
        }
    }
}
