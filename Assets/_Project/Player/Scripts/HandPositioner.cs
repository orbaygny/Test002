using System;
using UnityEngine;

public class HandPositioner : MonoBehaviour
{
    public static Action FrameIK;
    
    public void InitIK()
    {
        FrameIK?.Invoke();
    }
}
