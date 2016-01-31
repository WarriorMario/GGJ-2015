using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class ButtonScript : MonoBehaviour
{

    public enum ButtonType { START, INFO, QUIT, ONEVSONE, TWOVSTWO, LEVEL1, LEVEL2, LEVEL3 }
    public ButtonType m_MyType;
    public ButtonManager m_Manager;

    public ButtonScript m_Left;
    public ButtonScript m_Right;
    public ButtonScript m_Up;
    public ButtonScript m_Down;

    private static float m_Timer;
    private float m_TimerLimit;

    // Use this for initialization
    void Start()
    {
        m_TimerLimit = m_Manager.m_TimerLimit;
    }


    public void Active()
    {
        if (Input.GetButtonDown("Pickup0"))
        {
            OnSubmit();
        }

        if (Input.GetAxis("Vertical0") > 0 && m_Timer < 0)
        {
            if(m_Up != null)
            {
                m_Timer = m_TimerLimit;
                m_Manager.ChangeActiveButton(m_Up);
            }
        }
        else if (Input.GetAxis("Vertical0") < 0 && m_Timer < 0)
        {
            if (m_Down != null)
            {
                m_Timer = m_TimerLimit;
                m_Manager.ChangeActiveButton(m_Down);
            }
        }
        else if (Input.GetAxis("Horizontal0") > 0 && m_Timer < 0)
        {
            if (m_Right != null)
            {
                m_Timer = m_TimerLimit;
                m_Manager.ChangeActiveButton(m_Right);
            }
        }
        else if (Input.GetAxis("Horizontal0") < 0 && m_Timer < 0)
        {
            if (m_Left != null)
            {
                m_Timer = m_TimerLimit;
                m_Manager.ChangeActiveButton(m_Left);
            }
        }
        m_Timer -= Time.deltaTime;
    }

    public void OnSubmit()
    {
        switch (m_MyType)
        {
            case ButtonType.START:
                {
                    m_Manager.ActivatePlayerSelection();
                }
                break;
            case ButtonType.INFO:
                {
                    m_Manager.m_InstructionImage.gameObject.SetActive(true);
                }
                break;
            case ButtonType.QUIT:
                {
                    Application.Quit();
                }
                break;
            case ButtonType.ONEVSONE:
                {
                    InterSceneVars.s_AmountOfPlayers = 2;
                    m_Manager.ActivateLevelSelection();
                }
                break;
            case ButtonType.TWOVSTWO:
                {
                    InterSceneVars.s_AmountOfPlayers = 4;
                    m_Manager.ActivateLevelSelection();
                }
                break;
            case ButtonType.LEVEL1:
                {
                    InterSceneVars.s_Level = "Level1";
                    SceneManager.LoadScene(InterSceneVars.s_Level);
                }
                break;
            case ButtonType.LEVEL2:
                {
                    InterSceneVars.s_Level = "Level2";
                    SceneManager.LoadScene(InterSceneVars.s_Level);
                }
                break;
            case ButtonType.LEVEL3:
                {
                    InterSceneVars.s_Level = "Level3";
                    SceneManager.LoadScene(InterSceneVars.s_Level);
                }
                break;
        }
    }
}
