using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UseItem
{
    private int AttackTime;
    private int ShieldTime;



    // Start is called before the first frame update
    void Setup()
    {

        AttackTime = 0;
        ShieldTime = 0;
    }

    void UseTool()
    {
        SaveData.Instance.health = 20;
    }

    void UseBullet()
    {
        SaveData.Instance.attack = 10;
    }

    void UseShield()
    {
        SaveData.Instance.shield = 30;
    }
}
