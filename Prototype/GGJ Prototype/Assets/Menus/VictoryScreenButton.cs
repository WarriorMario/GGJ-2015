using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class VictoryScreenButton : MonoBehaviour
{

    public enum VictoryButtonType { MAINMENU, RETRY }
    public VictoryButtonType m_MyType;

    public VictoryScreenButton m_Up;
    public VictoryScreenButton m_Down;

    public VictoryScreenButton m_FirstSelectedButton;
    private static VictoryScreenButton s_ActiveButton;

    public bool m_IsActive;

    private static float s_Timer = 0;
    public float m_TimerLimit;

    void Start()
    {
        s_Timer = 0;
        s_ActiveButton = m_FirstSelectedButton;
        s_ActiveButton.m_IsActive = true;
        s_ActiveButton.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_IsActive)
        {
            if (Input.GetButtonDown("Pickup0"))
            {
                OnSubmit();
            }

            if (Input.GetAxis("Vertical0") > 0 && s_Timer < 0)
            {
                s_Timer = m_TimerLimit;
                if (m_Up != null)
                {
                    s_ActiveButton.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    s_ActiveButton = m_Up;
                    s_ActiveButton.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                    s_ActiveButton.m_IsActive = true;
                    m_IsActive = false;
                }
            }
            else if (Input.GetAxis("Vertical0") < 0 && s_Timer < 0)
            {
                s_Timer = m_TimerLimit;
                if (m_Down != null)
                {
                    s_ActiveButton.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    s_ActiveButton = m_Down;
                    s_ActiveButton.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                    s_ActiveButton.m_IsActive = true;
                    m_IsActive = false;
                }
            }
            s_Timer -= Time.deltaTime;
        }
    }

    void OnSubmit()
    {
        switch (m_MyType)
        {
            case VictoryButtonType.MAINMENU:
                {
                    SceneManager.LoadScene("MainMenu");
                }
                break;
            case VictoryButtonType.RETRY:
                {
                    SceneManager.LoadScene(InterSceneVars.s_Level);
                }
                break;
        }
    }
}
