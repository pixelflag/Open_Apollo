using UnityEngine;

public class Sound : MonoBehaviour
{
    public static Sound I;

    public AudioClip Ok;
    public AudioClip Launch;
    public AudioClip Clash;
    public AudioClip Randing;

    public AudioClip BGM_Finish;
    public AudioClip BGM_Spare;

    private void Awake()
    {
        I = this;
        Initialize();
    }

    private AudioSource bgmAudioSource;
    private AudioSource seAudioSource;

    public float bgmVolume = 0.5f;
    public float seVolume = 1.0f;

    public void Initialize()
    {
        bgmAudioSource = gameObject.AddComponent<AudioSource>();
        seAudioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlayBgm(BgmType type, bool isLoop)
    {
        bgmAudioSource.clip = GetAudioClip(type);
        bgmAudioSource.volume = bgmVolume;
        bgmAudioSource.loop = isLoop;
        bgmAudioSource.Play();

        AudioClip GetAudioClip(BgmType type)
        {
            switch (type)
            {
                case BgmType.Space: return BGM_Spare;
                case BgmType.Finish: return BGM_Finish;
            }
            throw new System.Exception("invalid type. : " + type);
        }
    }

    public void StopBgm()
    {
        bgmAudioSource.Stop();
    }

    public void PlaySE(SeType type, float volume)
    {
        seAudioSource.clip = GetAudioClip(type);
        seAudioSource.volume = volume * seVolume;
        seAudioSource.loop = false;
        seAudioSource.Play();

        AudioClip GetAudioClip(SeType type)
        {
            switch (type)
            {
                case SeType.Ok: return Ok;
                case SeType.Launch: return Launch;
                case SeType.Clash: return Clash;
                case SeType.Randing: return Randing;
            }
            throw new System.Exception("invalid type. : " + type);
        }
    }
}

public enum BgmType
{
    Space,
    Finish,
}

public enum SeType
{
    Ok,
    Launch,
    Clash,
    Randing,
}