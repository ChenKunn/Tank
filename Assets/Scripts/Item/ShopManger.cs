using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopManger : MonoBehaviour
{
    // Start is called before the first frame update

    public Button ToolDecButton;
    public Button ToolIncButton;
    public Text ToolText;

    public Button ShieldDecButton;
    public Button ShieldIncButton;
    public Text ShieldText;

    public Button BulletDecButton;
    public Button BulletIncButton;
    public Text BulletText;

    public Text MoneyText;
    public Text PriceText;
    public GameObject NoMoney;
    public Button BuyButton;
    public Button CloseButton;
    public SqlAccess sql;

    private string playerid;
    void Start()
    {
        sql = new SqlAccess();
        playerid = SaveData.Instance.playerid;

        ButtonAddListener();

        BuyButton.onClick.AddListener(()=> 
        {
            Buy();
        });

        CloseButton.onClick.AddListener(()=>
        {
            NoMoney.SetActive(false);
        });
    }

    // Update is called once per frame
    void Update()
    {
        MoneyText.text = sql.GetNumByID(playerid, "2");
        PriceText.text = GetPrice();
    }

    private void Buy()
    {
        int money;
        int.TryParse(MoneyText.text, out money);
        int price;
        int.TryParse(PriceText.text, out price);
        Debug.Log(money);
        Debug.Log(price);
        if(price <= money)
        {
            int NowMoney = money - price;
            NoMoney.SetActive(false);
            sql.UpdateItemByID(NowMoney.ToString(), playerid, "2");
            UpdateNum();
        }
        else
        {
            NoMoney.SetActive(true);
        }
    }
    private void ButtonAddListener()
    {
        //Î¬ÐÞ¹¤¾ß°´Å¥¼àÌý
        ToolIncButton.onClick.AddListener(() =>
        {
            ToolText.text = IncNum(ToolText.text);
        });
        ToolDecButton.onClick.AddListener(() =>
        {
            ToolText.text = DecNum(ToolText.text);
        });
        //»¤¶Ü°´Å¥¼àÌý
        ShieldIncButton.onClick.AddListener(() =>
        {
            ShieldText.text = IncNum(ShieldText.text);
        });

        ShieldDecButton.onClick.AddListener(() =>
        {
            ShieldText.text = DecNum(ShieldText.text);
        });
        //µ¯Ò©°´Å¥¼àÌý
        BulletIncButton.onClick.AddListener(()=>
        {
            BulletText.text = IncNum(BulletText.text);
        });
        BulletDecButton.onClick.AddListener(()=> 
        {
            BulletText.text = DecNum(BulletText.text);
        });
    }
    private string IncNum(string NowText)
    {
        int PreNum;
        int.TryParse(NowText, out PreNum);
        int NowNum = PreNum + 1;
        if (NowNum > 2) NowNum = 2;
        return NowNum.ToString();
    }
    private string DecNum(string NowText)
    {
        int PreNum;
        int.TryParse(NowText, out PreNum);
        int NowNum = PreNum - 1;
        if (NowNum < 0) NowNum = 0;
        return NowNum.ToString();
    }

    private string GetPrice()
    {
        int ToolPrice;
        int ShieldPrice;
        int BulletPrice;

        int.TryParse(sql.GetPrice("4"), out ToolPrice);
        int.TryParse(sql.GetPrice("3"), out ShieldPrice);
        int.TryParse(sql.GetPrice("1"), out BulletPrice);

        int ToolNum;
        int ShieldNum;
        int BulletNum;

        int.TryParse(ToolText.text, out ToolNum);
        int.TryParse(ShieldText.text, out ShieldNum);
        int.TryParse(BulletText.text ,out BulletNum);

        int price = ToolPrice * ToolNum + ShieldPrice * ShieldNum + BulletPrice * BulletNum;
        return price.ToString();
    }

    private void UpdateNum()
    {
        int ToolAddNum;
        int ShieldAddNum;
        int BulletAddNum;

        int.TryParse(ToolText.text, out ToolAddNum);
        int.TryParse(ShieldText.text, out ShieldAddNum);
        int.TryParse(BulletText.text, out BulletAddNum);
        int ToolNum;
        int ShieldNum;
        int BulletNum;

        int.TryParse(sql.GetNumByID(playerid, "4"), out ToolNum);
        int.TryParse(sql.GetNumByID(playerid, "3"), out ShieldNum);
        int.TryParse(sql.GetNumByID(playerid, "1"), out BulletNum);
        
        ToolNum += ToolAddNum;
        ShieldNum += ShieldAddNum;
        BulletNum += BulletAddNum;
        sql.UpdateItemByID(ToolNum.ToString(), playerid, "4");
        sql.UpdateItemByID(ShieldNum.ToString(), playerid, "3");
        sql.UpdateItemByID(BulletNum.ToString(), playerid, "1");
    }
}
