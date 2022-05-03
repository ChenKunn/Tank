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
    //选中道具的提示信息
    public Text ItemMessage;

    private SqlAccess sql;
    private string playerid;
    private int SelectItemID;
    void Start()
    {
        sql = new SqlAccess();
        playerid = SaveData.Instance.playerid;

        SelectItemID = 0;
        //添加道具按钮监听
        AddItemListener();
        //添加使用按钮监听
        UseButton.onClick.AddListener(() =>
        {
            Use();
        });
        //添加删除按钮监听
        DeleteButton.onClick.AddListener(() =>
        {
            Delete();
        });
    }

    // Update is called once per frame
    void Update()
    {
        //更新背包中的道具数量
        ToolNum.text = sql.GetNumByID(playerid, "4");
        ShieldNum.text = sql.GetNumByID(playerid, "3");
        MoneyNum.text = sql.GetNumByID(playerid, "2");
        BulletNum.text = sql.GetNumByID(playerid, "1");
        if(SelectItemID != 0)
        {
            ShowText();
        }
    }
    //显示提示信息
    private void ShowText()
    {
        string itemid = SelectItemID.ToString();
        string text;
        if(SelectItemID != 2)
        {
            text = sql.SelectFromItems("itemname", itemid) + ": " + sql.SelectFromItems("itemtime", itemid) + "s内" + sql.SelectFromItems("itemmessage", itemid) + sql.SelectFromItems("itemability", itemid) + "点";
        }
        else
        {
            text = sql.SelectFromItems("itemname", itemid) + ": " + sql.SelectFromItems("itemmessage", itemid); ;
        }
        ItemMessage.text = text;
    }
    
    private void AddItemListener()
    {
        //点击金币
        MoneyButton.onClick.AddListener(()=>
        {
            SelectItemID = 2;
        });
        //点击维修工具
        ToolButton.onClick.AddListener(() =>
        {
            SelectItemID = 4;
        });
        //点击护盾
        ShieldButton.onClick.AddListener(() =>
        {
            SelectItemID = 3;
        });
        //点击弹药
        BulletButton.onClick.AddListener(() =>
        {
            SelectItemID = 1;
        });
    }
    private void Use()
    {
        /*
            ItemUse(SelectItemID);
         */
        Delete();
    }

    private void Delete()
    {
        int PreNum = 0;
        int NowNum = 0;
        int.TryParse(sql.GetNumByID(playerid, SelectItemID.ToString()), out PreNum);
        NowNum = PreNum - 1;
        if (NowNum >= 0)
        {
            sql.UpdateItemByID(NowNum.ToString(), playerid, SelectItemID.ToString());
        }
    }
}
