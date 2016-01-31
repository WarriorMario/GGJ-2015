using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SkullScript : MonoBehaviour {

    public GameObject m_WinnerSkull;
    public GameObject m_LoserSkull;

    public GameObject m_StartPosition1;
    public GameObject m_StartPosition2;

    bool m_Started = false;
    bool m_FallDown = false;
    bool m_Waiting = false;
    public float m_DistanceLimit;
    float m_Distance;
    public float m_Speed;
    public float m_TimerLimit;
    private float m_Timer;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(m_FallDown)
        {
            if(m_Started)
            {
                if(InterSceneVars.s_WinningTeam == 0)
                {
                    m_WinnerSkull.transform.position = m_StartPosition1.transform.position;
                    m_LoserSkull.transform.position = m_StartPosition2.transform.position;
                }
                else
                {
                    m_WinnerSkull.transform.position = m_StartPosition2.transform.position;
                    m_LoserSkull.transform.position = m_StartPosition1.transform.position;
                }
                m_Started = false;
            }
            float multiplier = m_Speed * Time.deltaTime;
            transform.position += Vector3.down * multiplier;
            m_Distance += multiplier;
            if(m_Distance > m_DistanceLimit)
            {
                m_Waiting = true;
                m_FallDown = false;
            }            
        }
        else if(m_Waiting)
        {
            m_Timer += Time.deltaTime;
            if(m_Timer > m_TimerLimit)
                SceneManager.LoadScene("VictoryScreen");
        }
	}

    public void StartFalling()
    {
        m_Started = true;
        m_FallDown = true;
    }
}
