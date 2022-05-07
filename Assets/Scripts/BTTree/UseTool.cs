using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
public class UseTool : Action
{
    public override TaskStatus OnUpdate()
    {
        if(SaveData.Instance.ToolNum > 0)
        {
            SaveData.Instance.AIAddHp = 20;
            SaveData.Instance.ToolNum--;
        }
        return TaskStatus.Success;
    }
}
