using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStart : MonoBehaviour
{
    [SerializeField] private GameObject Options;
    [SerializeField] private GameObject Credits;

    public void Start()
    {
        Options.SetActive(false);
        Credits.SetActive(false);
    }
}
