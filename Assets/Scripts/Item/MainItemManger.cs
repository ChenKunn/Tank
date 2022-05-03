using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainItemManger : MonoBehaviour
{
    // Start is called before the first frame update
    public Text BulletNum;
    public Text ShieldNum;
    public Text ToolNum;

    private SqlAccess sql;
    private string playerid;
    void Start()
    {
        sql = new SqlAccess();
        playerid = SaveData.Instance.playerid;
    }

    // Update is called once per frame
    void Update()
    {
        BulletNum.text = sql.GetNumByID(playerid, "1");
        ShieldNum.text = sql.GetNumByID(playerid, "3");
        ToolNum.text = sql.GetNumByID(playerid, "4");
    }
}
