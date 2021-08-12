using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static bool playJumpSound;
    public static bool playAttackSound;

    public const int jump = 0;
    public const int attack = 1;

    public AudioClip jumpSound;
    public AudioClip attackSound;

    private static AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        playJumpSound = false;
        playAttackSound = false;

        audioSource = GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update()
    {
        if (playJumpSound)
        {
            PlaySound(jump);
            playJumpSound = false;
        }

        if (playAttackSound)
        {
            PlaySound(attack);
            playAttackSound = false;
        }
    }

    public void PlaySound(int sound)
    {
        Debug.Log("Sound started!");
        switch (sound)
        {
            case jump:
                audioSource.PlayOneShot(jumpSound);
                break;

            case attack:
                audioSource.PlayOneShot(attackSound);
                break;
        }
    }
}
