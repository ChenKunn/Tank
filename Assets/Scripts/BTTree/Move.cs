using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
public class Move : Action
{
    public MyGrid grid;
    private float TankSpeed = 8f;
    
    public Rigidbody TankEnemy;
    private int Num = 0;
    private Transform StartPos;
    private Transform TargetPos;

    private Transform NowPos;
    public override  void OnStart()
    {
        StartPos = GetComponent<Transform>();
        TargetPos = SaveData.Instance.player;
        grid.SetUp(); 
    }

    public override TaskStatus OnUpdate()
    {

        if(TargetPos != null){
            StartPos = GetComponent<Transform>();
            TargetPos = SaveData.Instance.player;
            FindPath(StartPos.position, TargetPos.position); 
            if(Num < grid.FindPath.Count)
            {
                MoveToTarget();
                if(GetDis() < 0.1f) Num++;
            }
        }
        return TaskStatus.Running;
    }
    private void MoveToTarget()
    {
        Debug.Log("Hello");
        Vector3 StartPosition = TankEnemy.position;
        Vector3 TargetPosition = grid.FindPath[Num].Pos;
        float x = Mathf.MoveTowards(StartPosition.x, TargetPosition.x, TankSpeed * Time.deltaTime);
        float z = Mathf.MoveTowards(StartPosition.z, TargetPosition.z, TankSpeed * Time.deltaTime);

        Vector3 pos = new Vector3(x, 0, z) - TankEnemy.position;
        Quaternion rotation = Quaternion.LookRotation(pos);
        
        TankEnemy.rotation = rotation;
        TankEnemy.position = new Vector3(x, 0, z);
    }

    private float GetDis()
    {
        float Dis = Vector3.Distance(TankEnemy.position, grid.FindPath[Num].Pos);
        return Dis;
    }
    void FindPath(Vector3 _StartPos, Vector3 _TargetPos)
    {
        //Debug.Log("Hello");
        //Debug.LogWarning(_StartPos);
        //Debug.LogWarning(_TargetPos);
        Node StartNode = grid.ToGridPos(_StartPos);
        Node TargetNode = grid.ToGridPos(_TargetPos);
        //Debug.LogWarning(TargetNode.Pos);
        List<Node> OpenList = new List<Node>();
        HashSet<Node> CLosedList = new HashSet<Node>();
        OpenList.Add(StartNode);
        while(OpenList.Count > 0)
        {
            Node NowNode = OpenList[0];

            for (int i = 1; i < OpenList.Count; i++)
            {
                if(OpenList[i].FCost < NowNode.FCost || OpenList[i].FCost == NowNode.FCost && OpenList[i].HCost < NowNode.HCost)
                {
                    NowNode = OpenList[i];
                }
            }

            OpenList.Remove(NowNode);
            CLosedList.Add(NowNode);

            if(NowNode == TargetNode){
                GetFinalPath(StartNode, TargetNode);
            }

            foreach(Node VisitNode in grid.GetVisitNode(NowNode))
            {
                if(!VisitNode.IsWall || CLosedList.Contains(VisitNode))
                {
                    continue;
                }
                int Cost = NowNode.GCost + GetDis(NowNode, VisitNode);
                
                if(Cost < VisitNode.GCost || !OpenList.Contains(VisitNode)){
                    VisitNode.GCost = Cost;
                    VisitNode.HCost = GetDis(NowNode, VisitNode);
                    VisitNode.Parent = NowNode;

                    if(!OpenList.Contains(VisitNode))
                    {
                        OpenList.Add(VisitNode);
                    }
                }
            }
        }
    }

    void GetFinalPath(Node _StartNode, Node _TargetNode)
    {
        List<Node> FinalPath = new List<Node>();
        Node NowNode = _TargetNode;

        while(NowNode != _StartNode)
        {
            FinalPath.Add(NowNode);
            NowNode = NowNode.Parent;
        }
        FinalPath.Reverse();

        grid.FindPath = FinalPath;
    }
    
    int GetDis(Node _ANode, Node _BNode)
    {
        int dx = Mathf.Abs(_ANode.x - _BNode.x);
        int dy = Mathf.Abs(_ANode.y - _BNode.y);

        return dx + dy;
    }
}
