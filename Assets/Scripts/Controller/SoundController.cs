using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : Singleton<SoundController>
{
    private AudioSource AudioSource;

    public AudioClip EnemyDead;
    public AudioClip CollectItem;
    public AudioClip GameOver;
    public AudioClip GameStart;

    private void Awake()
    {
        
        AudioSource = GetComponent<AudioSource>();
    }
    public void PlayEnemyDead()
    {
        AudioSource.clip = EnemyDead;
        AudioSource.Play();
    }

    public void PlayCollectItem()
    {
        AudioSource.clip = CollectItem;
        AudioSource.Play();
    }

    public void PlayGameOver()
    {
        AudioSource.clip = GameOver;
        AudioSource.Play();
    }

    public void PlayGameStart()
    {
        AudioSource.clip = GameStart;
        AudioSource.Play();
    }

    public void PlaySound(AudioClip ac)
    {
        AudioSource.clip = ac;
        AudioSource.Play();
    }
}
