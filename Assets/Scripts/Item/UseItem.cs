using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UseItem
{
    private int AttackTime;
    private int ShieldTime;

    public void UsebyID(int id)
    {
        if(id == 1)
        {
            UseBullet();
        }
        if(id == 3)
        {
            UseShield();
        }
        if(id == 4)
        {
            UseTool();
        }
    }
    private void UseTool()
    {
        SaveData.Instance.health = 20;

    }

    private void UseBullet()
    {
        SaveData.Instance.attack = 10;
    }

    private void UseShield()
    {
        SaveData.Instance.shield = 30;
    }
}
