using UnityEngine;
using System.Collections;

public class RitualBlock : MonoBehaviour
{
    public GameObject[] m_Models;

    public void TakeDamage()
    {
        bool taken = false;
        Vector3 previousPosition = Vector3.zero;
        for (int model = 0; model < m_Models.Length ; model++)
        {
            if (taken)
            {
                Vector3 tempPosition = m_Models[model].transform.position;
                m_Models[model].transform.position = previousPosition;
                previousPosition = tempPosition;
            }
            else if (m_Models[model] != null)
            {
                previousPosition = m_Models[model].transform.position;
                Destroy(m_Models[model]);
                taken = true;
            }
        }
        if(!taken)
        {
            bool parentNotFound = true;
            GameObject parent = transform.parent.gameObject;
            while (parent && parentNotFound)
            {
                Platform platform = parent.GetComponent<Platform>();
                if (platform)
                {
                    platform.DestroyBlock(transform.gameObject);
                    break;
                }
                parent = parent.transform.parent.gameObject;
            }
        }
    }
}
