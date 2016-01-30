using UnityEngine;
using System.Collections;

public class RitualBlock : MonoBehaviour
{
    public int m_HP;
    public GameObject[] m_Models;
    public void TakeDamage()
    {
        --m_HP;
        Destroy(m_Models[m_HP]);
        if(m_HP == 0)
        {
            Destroy(gameObject);
        }
    }
}
