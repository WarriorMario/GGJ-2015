using UnityEngine;
using System.Collections.Generic;

public class Ritual : MonoBehaviour {

    List<Transform> m_RitualCubes;
    List<Transform> m_RitualPartCubes;
    // Use this for initialization
    void Awake ()
    {
        m_RitualCubes = new List<Transform>();
        m_RitualPartCubes = new List<Transform>();
	    // Get all ritual blocks
        // Get all normal blocks
        foreach(Transform t in transform)
        {
            if(t.name.StartsWith("RitualCube"))
            {
                m_RitualCubes.Add(t);
            }
            else if(t.name.StartsWith("RitualPartCube"))
            {
                m_RitualPartCubes.Add(t);
            }
        }
	}

    public void OnBlockDestroy()
    {
        bool destroyedPlatform = true;
        foreach(Transform t in m_RitualCubes)
        {
            if(t!=null)
            {
                if(t.GetComponent<Rigidbody>().useGravity == true)
                {
                    continue;
                }
                destroyedPlatform = false;
                break;
            }
            
        }
        if(destroyedPlatform)
        {
            foreach(Transform t in m_RitualPartCubes)
            {
                if(t==null)
                {
                    continue;
                }
                // Destroy the block
                t.GetComponent<Rigidbody>().useGravity = true;
                t.GetChild(0).GetComponent<BlockHover>().enabled = false;
                t.GetComponent<Rigidbody>().isKinematic = false;
                Destroy(t.gameObject, 10.0f);
            }
        }
    }
}
