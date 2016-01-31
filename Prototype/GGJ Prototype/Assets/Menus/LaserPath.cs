using UnityEngine;
using System.Collections;

public class LaserPath : MonoBehaviour {

    public GameObject[] m_PathPoints;
    public LineRenderer m_LineRenderer;
	// Use this for initialization
	void Start () {
        Vector3[] positions = new Vector3[m_PathPoints.Length ];
        for (int i = 0; i < m_PathPoints.Length; i++)
        {
            positions[i] = m_PathPoints[i].transform.position;
            positions[i] += new Vector3(0f, transform.position.y, 0f);
        }

        m_LineRenderer.SetPositions(positions);
	}

    void FixedUpdate()
    {
        for (int i = 0; i < m_PathPoints.Length; i++)
        {
            if (m_PathPoints[i] != null && !m_PathPoints[i].GetComponent<Rigidbody>().useGravity)
                return;
        }

        Destroy(gameObject);
    }
}

/*
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LaserPath : MonoBehaviour {

    public List<GameObject> m_PathPoints;
    public LineRenderer m_LineRenderer;

    List<GameObject> m_ActivePoints = new List<GameObject>();

	void Start ()
    {
        m_ActivePoints = m_PathPoints;
        UpdateLasers();
	}

    void Update()
    {
        UpdateLasers();
    }

    public void UpdateLasers()
    {
        for (int i = m_PathPoints.Count -1 ; i >= 0; i--)
        {
            if (m_PathPoints[i] == null || m_PathPoints[i].GetComponent<Rigidbody>().useGravity)
                m_ActivePoints.RemoveAt(i);
        }
        if (m_ActivePoints.Count == 1)
        {
            Destroy(gameObject);
            return;
        }

        Vector3[] positions = new Vector3[m_PathPoints.Count];
        for (int i = 0; i < m_PathPoints.Count; i++)
        {
            if (i < m_ActivePoints.Count)
            {
                positions[i] = m_ActivePoints[i].transform.position;
                positions[i] += Vector3.up;
            }
            else
                positions[i] = m_ActivePoints[m_ActivePoints.Count - 1].transform.position;
        }

        m_LineRenderer.SetPositions(positions);
    }
}
*/