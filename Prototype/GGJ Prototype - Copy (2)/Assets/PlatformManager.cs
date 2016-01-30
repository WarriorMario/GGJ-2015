using UnityEngine;
using System.Collections;

public class PlatformManager : MonoBehaviour {

	// Use this for initialization
	public void Init()
    {
        int num = transform.childCount;
        for(int i =0; i< num;++i)
        {
            transform.GetChild(i).GetComponent<Platform>().Init();
        }
    }
}
