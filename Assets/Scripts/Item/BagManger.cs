using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagManger : MonoBehaviour
{
    // Start is called before the first frame update
    public Text ToolNum;
    public Text ShieldNum;
    public Text BulletNum;
    public Text MoneyNum;
    public Button ToolButton;
    public Button ShieldButton;
    public Button BulletButton;
    public Button MoneyButton;
    public Button UseButton;
    public Button DeleteButton;


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
        ToolNum.text = sql.GetNumByID(playerid, "4");
        ShieldNum.text = sql.GetNumByID(playerid, "3");
        MoneyNum.text = sql.GetNumByID(playerid, "2");
        BulletNum.text = sql.GetNumByID(playerid, "1");
    }
}
