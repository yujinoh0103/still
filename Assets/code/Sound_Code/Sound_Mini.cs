using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Mini : MonoBehaviour
{
    public static Sound_Mini instance;

    [Header("#BGM")]
    public AudioClip bgmClip;
    public float bgmVolume = 1f;
    private AudioSource bgmPlayer;

    [Header("#SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume = 1f;
    public int channels = 5;
    private AudioSource[] sfxPlayers;
    private int channelIndex;

    // 구간 재생용 변수
    private AudioSource audioSource;
    private float cutStartTime;
    private float cutEndTime;
    private bool isPlayingSection = false;

    public enum Sfx
    {
        click
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Init()
    {
        // BGM 플레이어 초기화
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;

        // SFX 플레이어 초기화
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];
        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;
            sfxPlayers[i].volume = sfxVolume;
        }

        // 구간 재생용 AudioSource 초기화 (BGM과 별개)
        GameObject sectionPlayerObj = new GameObject("SectionPlayer");
        sectionPlayerObj.transform.parent = transform;
        audioSource = sectionPlayerObj.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.volume = sfxVolume;  // 필요에 따라 조절
        if (bgmClip != null)
        {
            bgmPlayer.Play();
        }
    }

    private void Update()
    {
        if (isPlayingSection)
        {
            if (audioSource.time >= cutEndTime)
            {
                audioSource.Pause();
                isPlayingSection = false;
            }
        }
    }

    // 일반 SFX 재생
    public void PlaySfx(Sfx sfx)
    {
        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            int loopIndex = (i + channelIndex) % sfxPlayers.Length;
            if (sfxPlayers[loopIndex].isPlaying)
                continue;

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
            sfxPlayers[loopIndex].Play();
            break;
        }
    }

    // 지정 구간만 재생 (오디오 클립은 audioSource.clip에 직접 할당해야 함)
    public void PlaySection(float startTime, float endTime)
    {
        if (audioSource.clip == null)
        {
            Debug.LogWarning("audioSource.clip이 할당되어 있지 않습니다!");
            return;
        }

        cutStartTime = startTime;
        cutEndTime = endTime;
        audioSource.time = cutStartTime;
        audioSource.Play();
        isPlayingSection = true;
    }

    // 구간 재생용 클립 할당 함수
    public void SetSectionClip(AudioClip clip)
    {
        audioSource.clip = clip;
    }

    // int 인덱스로 직접 재생하는 함수 추가
    public void PlaySfxCustom(int clipIndex)
    {
        if (clipIndex < 0 || clipIndex >= sfxClips.Length)
        {
            Debug.LogWarning("PlaySfxCustom: clipIndex 범위 벗어남: " + clipIndex);
            return;
        }

        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            int loopIndex = (i + channelIndex) % sfxPlayers.Length;
            if (sfxPlayers[loopIndex].isPlaying)
                continue;

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[clipIndex];
            sfxPlayers[loopIndex].Play();
            break;
        }
    }

}
