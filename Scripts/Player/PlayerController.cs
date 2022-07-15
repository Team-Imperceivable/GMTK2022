using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private FrameInputs _inputs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GatherInputs();

    }

    private void GatherInputs()
    {
        _inputs = new FrameInputs
        {
            mousePos = Input.mousePosition,
            select = Input.GetButtonDown("Fire1")
        };
    }
}
