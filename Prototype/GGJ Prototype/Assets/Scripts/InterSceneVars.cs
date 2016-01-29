using UnityEngine;
using System.Collections;

public class InterSceneVars : MonoBehaviour {

    public static int s_AmountOfPlayers;

    private static bool s_Initialized;

    public void Start()
    {
        if(!s_Initialized)
        {
            s_Initialized = true;
        }
    }
}
