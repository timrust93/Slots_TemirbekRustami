using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip _itemAppearSound;
    [SerializeField] private AudioClip _itemSuccessSound;

    public AudioSource efXSource;
    public static SoundManager instance = null;

    public Dictionary<SoundName, AudioClip> easySoundBase;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            MakeEasySoundBase();
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        GameObject go = new GameObject();
        AudioSource source = go.AddComponent<AudioSource>();
        source.clip = clip;
        source.Play();
        GameObject.Destroy(go, clip.length);
    }

    public void PlaySoundByType(SoundName name)
    {
        AudioClip clip = easySoundBase[name];
        if (clip != null)
            PlaySound(clip);
    }


    private void MakeEasySoundBase()
    {
        easySoundBase = new Dictionary<SoundName, AudioClip>();
        easySoundBase.Add(SoundName.ItemAppear, _itemAppearSound);
        easySoundBase.Add(SoundName.ItemSuccess, _itemSuccessSound);
    }
}


public enum SoundName
{
    ItemAppear,
    ItemSuccess
}
