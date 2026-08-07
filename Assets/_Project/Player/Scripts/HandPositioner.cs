using System;
using UnityEngine;

public class HandPositioner : MonoBehaviour
{
    public static Action FrameIK;
    public static Action SetGround;
    public void InitIK()
    {
        FrameIK?.Invoke();
    }
    public void SetHeight() 
    { 
        SetGround?.Invoke();
    }
}
