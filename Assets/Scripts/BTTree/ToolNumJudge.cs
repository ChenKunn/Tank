using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
public class ToolNumJudge : Conditional
{
    public override TaskStatus OnUpdate()
    {
        if(SaveData.Instance.ToolNum > 0){
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
