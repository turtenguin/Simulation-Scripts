using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif

public class GazeAdjustProcessor : InputProcessor<Quaternion>
{

#if UNITY_EDITOR
    static GazeAdjustProcessor()
    {
        Initialize();
    }
#endif

    [RuntimeInitializeOnLoadMethod]
    static void Initialize()
    {
        InputSystem.RegisterProcessor<GazeAdjustProcessor>();
    }

    public override Quaternion Process(Quaternion value, InputControl control)
    {
        Quaternion returnVal = value;
        Vector3 eulers = value.eulerAngles;
        returnVal.eulerAngles = new Vector3(-(eulers.x + 180), eulers.y, eulers.z);
        return returnVal;
    }
}
