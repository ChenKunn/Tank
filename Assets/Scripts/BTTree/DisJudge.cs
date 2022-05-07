using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
public class DisJudge : Conditional
{
    public Rigidbody player;
    public Rigidbody AI;
    private Transform EndPos;
    private Transform StartPos;
    public override void OnStart()
    {
        EndPos = AI.GetComponent<Transform>() ;
    }
    public override TaskStatus OnUpdate()
    {
        StartPos = SaveData.Instance.player;
        float dis = GetDis();
        EndPos.LookAt(StartPos);
        if(dis <= 10f){
            Debug.Log(StartPos.position);
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
    private float GetDis()
    {
        float Dis = Vector3.Distance(EndPos.position, StartPos.position);
        return Dis;
    }
}
