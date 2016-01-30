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
                    InterSceneVars.m_AmountOfPlayers = 2;
                    m_Manager.ActivateLevelSelection();
                }
                break;
            case ButtonType.TWOVSTWO:
                {
                    InterSceneVars.m_AmountOfPlayers = 4;
                    m_Manager.ActivateLevelSelection();
                }
                break;
            case ButtonType.LEVEL1:
                {
                    SceneManager.LoadScene("Level1");
                }
                break;
            case ButtonType.LEVEL2:
                {
                    SceneManager.LoadScene("Level2");
                }
                break;
            case ButtonType.LEVEL3:
                {
                    SceneManager.LoadScene("Level3");
                }
                break;
        }
    }
}
