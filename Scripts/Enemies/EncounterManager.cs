using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private float slideSpeed;
    
    private Transform[] enemies;
    private bool sliding;

    // Start is called before the first frame update
    void Start()
    {
        enemies = gameObject.GetComponentsInChildren<Transform>(true);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(sliding && transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, slideSpeed * Time.deltaTime);
        }
        if(transform.position == targetPosition)
        {
            sliding = false;
        }
    }

    public void SetEncounterActive(bool active)
    {
        gameObject.SetActive(active);
        foreach(Transform transform in enemies)
        {
            transform.gameObject.SetActive(active);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(targetPosition, 1f);
    }

    public void SlideToTarget()
    {
        sliding = true;
    }
}
