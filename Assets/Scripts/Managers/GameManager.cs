﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int m_NumRoundsToWin = 5;        
    public float m_StartDelay = 3f;         
    public float m_EndDelay = 3f;           
    public CameraControl m_CameraControl;   
    public Text m_MessageText;              
    public GameObject m_TankPrefab;     
    public GameObject m_TankEnemyPrefab;    
    public TankManager[] m_Tanks;

    public TankEnemyManger[] m_TankEnemys;       


    private int m_RoundNumber;              
    private WaitForSeconds m_StartWait;     
    private WaitForSeconds m_EndWait;       
    private TankManager m_RoundWinner;
    private TankManager m_GameWinner;
    private TankEnemyManger m_EnemyRoundWinner;
    private TankEnemyManger m_EnemyGameWinner;
    private float ShieldTime;
    private float AttackTime;
    private SqlAccess sql;
    private string playid;
    const float k_MaxDepenetrationVelocity = float.PositiveInfinity;

    private void Start()
    {
        // This line fixes a change to the physics engine.
        Physics.defaultMaxDepenetrationVelocity = k_MaxDepenetrationVelocity;
        
        m_StartWait = new WaitForSeconds(m_StartDelay);
        m_EndWait = new WaitForSeconds(m_EndDelay);
        sql = new SqlAccess();

        ShieldTime = 10f;
        AttackTime = 10f;
        playid = SaveData.Instance.playerid;

        SpawnAllTanks();
        SetCameraTargets();

        StartCoroutine(GameLoop());
    }

    private void Update()
    {
        if(m_Tanks[0] != null)
        {
            SaveData.Instance.player = m_Tanks[0].m_Instance.GetComponent<Transform>();
        }
        if(SaveData.Instance.health > 0)
        {
            if (m_Tanks[0] != null)
            {
                m_Tanks[0].AddHealth();
                SaveData.Instance.health = 0;
            }
        }

        if(SaveData.Instance.shield > 0)
        {
            if(m_Tanks[0] != null)
            {
                if (ShieldTime == 10f)
                {
                    //加护盾
                    m_Tanks[0].AddShield();
                } 

                if (ShieldTime <= 0f)
                {
                    //撤护盾
                    m_Tanks[0].CancleShield();
                    ShieldTime = 10f;
                    SaveData.Instance.shield = 0;
                }
                ShieldTime -= Time.deltaTime;
            }
        }

        if(SaveData.Instance.attack > 0)
        {
            if (m_Tanks[0] != null)
            {
                if (AttackTime == 10f)
                {
                    //加攻击
                    m_Tanks[0].AddAttack();
                }
                if(AttackTime <= 0f)
                {
                    //减攻击
                    m_Tanks[0].CancleAttack();
                    AttackTime = 10f;
                    SaveData.Instance.attack = 0;
                }

               AttackTime -= Time.deltaTime;
            }
        }

        if(SaveData.Instance.AIAddHp > 0)
        {
            if(m_TankEnemys[0] != null)
            {
                m_TankEnemys[0].AddHp();
                SaveData.Instance.AIAddHp = 0;
            }
        }
        SaveData.Instance.playerdie = OneTankLeft();
        SaveData.Instance.AIHealth = m_TankEnemys[0].GetHP();
    }

    private void SpawnAllTanks()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].m_Instance =
                Instantiate(m_TankPrefab, m_Tanks[i].m_SpawnPoint.position, m_Tanks[i].m_SpawnPoint.rotation) as GameObject;
            SaveData.Instance.player = m_Tanks[i].m_Instance.GetComponent<Transform>();
            m_Tanks[i].m_PlayerNumber = i + 1;
            m_Tanks[i].Setup();
        }

        for (int i = 0; i < m_TankEnemys.Length; i++)
        {
            m_TankEnemys[i].m_Instance = 
                Instantiate(m_TankEnemyPrefab, m_TankEnemys[i].m_SpawnPoint.position, m_TankEnemys[i].m_SpawnPoint.rotation) as GameObject;
            m_TankEnemys[i].m_EnemyNumber = i + 1;
            m_TankEnemys[i].Setup(m_Tanks[0].m_Instance);
        }
    }


    private void SetCameraTargets()
    {
        Transform[] targets = new Transform[m_Tanks.Length + m_TankEnemys.Length];

        for (int i = 0; i < m_Tanks.Length; i++)
        {
            targets[i] = m_Tanks[i].m_Instance.transform;
        }

        for (int i = 0; i < m_TankEnemys.Length; i++)
        {
            targets[m_Tanks.Length + i] = m_TankEnemys[i].m_Instance.transform;
        }

        m_CameraControl.m_Targets = targets;
    }


    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

        if (m_GameWinner != null || m_EnemyGameWinner != null)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            StartCoroutine(GameLoop());
        }
    }


    private IEnumerator RoundStarting()
    {
        ResetAllTanks();
        DisableTankControl();

        m_CameraControl.SetStartPositionAndSize();
        m_RoundNumber++;
        m_MessageText.text = "ROUND " + m_RoundNumber;
        SaveData.Instance.ToolNum = 2;
        yield return m_StartWait;
    }


    private IEnumerator RoundPlaying()
    {
        EnableTankControl();


        m_MessageText.text = string.Empty;

        while(!OneTankLeft())
        {
            yield return null;
        }
        
    }


    private IEnumerator RoundEnding()
    {
        DisableTankControl();

        m_RoundWinner = null;
        bool RoundWinnerType = GetRoundWinnerType();

        if(RoundWinnerType == false)
            m_RoundWinner = GetRoundWinner();
        else
           m_EnemyRoundWinner = GetEnemyRoundWinner();


        if(m_RoundWinner != null)
        {
            m_RoundWinner.m_Wins++;
            AddMoney();
        }

        if(m_EnemyRoundWinner != null)
            m_EnemyRoundWinner.m_Wins++;

        m_GameWinner = GetGameWinner();
        m_EnemyRoundWinner = GetEnemyGameWinner();

        string message = EndMessage();

        //Debug.Log(message);
        m_MessageText.text = message;
        
        yield return m_EndWait;
    }


    private bool OneTankLeft()
    {
        int numTanksLeft = 0;

        for (int i = 0; i < m_Tanks.Length; i++)
        {
            if (m_Tanks[i].m_Instance.activeSelf)
                numTanksLeft++;
        }

        for (int i = 0; i < m_TankEnemys.Length; i++)
        {
            if(m_TankEnemys[i].m_Instance.activeSelf)
                numTanksLeft++;
        }

        return numTanksLeft <= 1;
    }

    private bool GetRoundWinnerType()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            if (m_Tanks[i].m_Instance.activeSelf)
                return false;
        }

        for (int i = 0; i< m_TankEnemys.Length; i++)
        {
            if (m_TankEnemys[i].m_Instance.activeSelf)
                return true;
        }

        return false;
    }

    private TankManager GetRoundWinner()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            if (m_Tanks[i].m_Instance.activeSelf)
                return m_Tanks[i];
        }
        return null;
    }

    private TankEnemyManger GetEnemyRoundWinner()
    {
        for (int i = 0; i < m_TankEnemys.Length; i++)
        {
            if (m_TankEnemys[i].m_Instance.activeSelf)
                return m_TankEnemys[i];
        }
        return null;
    }
    
    private TankManager GetGameWinner()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            if (m_Tanks[i].m_Wins == m_NumRoundsToWin)
                return m_Tanks[i];
        }

        return null;
    }

    private TankEnemyManger GetEnemyGameWinner()
    {
        for (int i = 0; i < m_TankEnemys.Length; i++)
        {
            if (m_TankEnemys[i].m_Wins == m_NumRoundsToWin)
                return m_TankEnemys[i];
        }

        return null;
    }

    private string EndMessage()
    {
        string message = "DRAW!";

        if (m_RoundWinner != null)
            message = m_RoundWinner.m_ColoredPlayerText + " WINS THE ROUND!";

        message += "\n\n\n\n";

        for (int i = 0; i < m_Tanks.Length; i++)
        {
            message += m_Tanks[i].m_ColoredPlayerText + ": " + m_Tanks[i].m_Wins + " WINS\n";
        }

        for (int i = 0; i < m_TankEnemys.Length; i++)
        {
            message += m_TankEnemys[i].m_ColoredPlayerText + " : " + m_TankEnemys[i].m_Wins + " WINS\n";
        }

        if (m_GameWinner != null)
            message = m_GameWinner.m_ColoredPlayerText + " WINS THE GAME!";

        if (m_EnemyGameWinner != null)
            message = m_EnemyGameWinner.m_ColoredPlayerText + " WINS THE GAME!";
        
        return message;
    }

    private void ResetAllTanks()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].Reset();
        }

        for (int i = 0; i < m_TankEnemys.Length; i++)
        {
            m_TankEnemys[i].Reset();
        }
    }


    private void EnableTankControl()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].EnableControl();
        }

        for (int i = 0; i < m_TankEnemys.Length; i++)
        {
            m_TankEnemys[i].EnableControl();
        }
    }


    private void DisableTankControl()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].DisableControl();
        }

        for (int i = 0; i < m_TankEnemys.Length; i++)
        {
            m_TankEnemys[i].DisableControl();
        }
    }

    private void AddMoney()
    {
        int money;
        int.TryParse(sql.GetNumByID(playid, "2"), out money);
        //Debug.Log(money);
        money += 10;
        sql.UpdateItemByID(money.ToString(), playid, "2");
    }
}