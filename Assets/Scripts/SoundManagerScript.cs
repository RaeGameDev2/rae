using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static SoundManagerScript instance;

    public enum SoundType
    {
        JUMP,
        ATTACK,
    }

    public AudioClip jumpSound;
    public AudioClip attackSound;
    public AudioClip music;

    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.volume = GameManager.instance.volume;
        Camera.main.GetComponent<AudioSource>().volume = GameManager.instance.volume;
        Camera.main.GetComponent<AudioSource>().clip = music;
    }

    public void PlaySound(SoundType type)
    {
        switch (type)
        {
            case SoundType.JUMP:
                audioSource.PlayOneShot(jumpSound);
                break;

            case SoundType.ATTACK:
                audioSource.PlayOneShot(attackSound);
                break;
        }
    }

    public void UpdateVolume()
    {
        Camera.main.GetComponent<AudioSource>().volume = GameManager.instance.volume;
        audioSource.volume = GameManager.instance.volume;
    }
}
