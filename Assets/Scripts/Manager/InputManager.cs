using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] GameEvent OnFireStatusChangedEvent;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnFireStatusChangedEvent.Raise(this, true);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            OnFireStatusChangedEvent.Raise(this, false);
        }
    }
}
