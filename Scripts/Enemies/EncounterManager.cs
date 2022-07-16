using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    Transform[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        enemies = gameObject.GetComponentsInChildren<Transform>(true);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetEncounterActive(bool active)
    {
        gameObject.SetActive(active);
        foreach(Transform transform in enemies)
        {
            transform.gameObject.SetActive(active);
        }
    }
}
