using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXHandler : MonoBehaviour
{
    [SerializeField] private AudioClip playerDamaged;
    [SerializeField] private AudioClip enemyDamaged;
    [SerializeField] private AudioClip enemyDeath;
    [SerializeField] private AudioClip click;

    private AudioSource source;

    private void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
    }
    
    public void PlayClick()
    {
        source.clip = click;
        source.Play();
    }

    public void PlayEnemyHit()
    {
        source.clip = enemyDamaged;
        source.Play();
    }

    public void PlayEnemyDeath()
    {
        source.clip = enemyDeath;
        source.Play();
    }

    public void PlayHit()
    {
        source.clip = playerDamaged;
        source.Play();
    }
}
