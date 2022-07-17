using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] private List<AudioClip> music;
    
    private List<AudioClip> playlist;
    private AudioSource source;
    private int currIndex;

    // Start is called before the first frame update
    private void Start()
    {
        if (source == null)
        {
            source = gameObject.GetComponent<AudioSource>();
            Shuffle();
            playlist = music;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if(!source.isPlaying)
        {
            source.clip = playlist[currIndex];
            currIndex++;
            source.Play();
            if (currIndex >= playlist.Count)
                SwapPlaylist();
        }
    }

    private void SwapPlaylist()
    {
        playlist = music;
        StartCoroutine(Shuffle());
    }

    private IEnumerator Shuffle()
    {
        List<AudioClip> newPlaylist = new List<AudioClip>();
        while(music.Count > 0)
        {
            AudioClip selected = music[Random.Range(0, music.Count)];
            music.Remove(selected);
            newPlaylist.Add(selected);
            yield return null;
        }
        music = newPlaylist;
    }
}
