using UnityEngine;
using System.Collections;

public class ProjectileSlot : MonoBehaviour
{
    public GameObject m_ProjectilePrefab;

    GameObject m_Projectile;
    public bool Filled
    {
        get { return m_Projectile != null; }
    }

    public void Fill()
    {
        if (!Filled)
        {
            m_Projectile = Instantiate(m_ProjectilePrefab, transform.position, transform.rotation) as GameObject;
            m_Projectile.transform.parent = transform;
        }
    }
    public void Fire(Vector3 target)
    {
        if (Filled)
        {
            GameObject projectile = Instantiate(m_ProjectilePrefab, transform.position, transform.rotation) as GameObject;
            projectile.GetComponent<ProjectileController>().Fire(target);
            Destroy(m_Projectile);
        }
    }
}
