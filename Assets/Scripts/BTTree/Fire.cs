using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorDesigner.Runtime.Tasks;
public class Fire : Action
{
    public Rigidbody m_Shell;
    public Transform Firetransform;
    
    public override TaskStatus OnUpdate()
    {
        ShellIns shellIns = new ShellIns();
        shellIns.SetUp(m_Shell, Firetransform);
        if(!SaveData.Instance.playerdie) shellIns.GetRig();
        return TaskStatus.Success;
    }
}

public class ShellIns: MonoBehaviour
{
    private Rigidbody Shell;
    private Transform FireTransform;
    private float CurrentLaunchForce = 15f; 
    public void SetUp(Rigidbody rig, Transform tran)
    {
        Shell = rig;
        FireTransform = tran;
    }

    public  void  GetRig()
    {
        Rigidbody shell = Instantiate(Shell, FireTransform.position, FireTransform.rotation) as Rigidbody;
        shell.velocity = (CurrentLaunchForce) * FireTransform.forward;
    }

}
