using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
public class HpJudge : Conditional
{
    public override TaskStatus OnUpdate()
    {
        if(SaveData.Instance.AIHealth <= 40){
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
