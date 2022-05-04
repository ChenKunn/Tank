using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManger : MonoBehaviour
{
    private UseItem use;
    private SqlAccess sql;
    private string playerid;
    // Start is called before the first frame update
    void Start()
    {
        use = new UseItem();
        sql = new SqlAccess();
        playerid = SaveData.Instance.playerid;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            use.UsebyID(1);
            Delete(1);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            use.UsebyID(3);
            Delete(3);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            use.UsebyID(4);
            Delete(4);
        }
    }
    private void Delete(int SelectItemID)
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
