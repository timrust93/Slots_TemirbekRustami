using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FingerMover : MonoBehaviour
{
    private bool _isMovementAllowed;

    public void SetMoveAllowed(bool isTrue)
    {
        _isMovementAllowed = isTrue;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isMovementAllowed)
        {
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
