using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MyGrid
{

    public Transform StartPos;
    public LayerMask WallMask;
    private Vector2 GridSize;
    private float GridRadius;
    public float Dis;
    Node[,] OneGrid;
    public List<Node> FindPath;
    private float GridDiameter;
    private int GridX, GridY;

    public void SetUp()
    {
        GridSize.x = 100;
        GridSize.y = 100;
        GridRadius = 1f;
        GridDiameter = GridRadius * 2;

        GridX = Mathf.RoundToInt(GridSize.x / GridDiameter);
        GridY = Mathf.RoundToInt(GridSize.y / GridDiameter);

        CreateGrid(); 
    }

    private void CreateGrid()
    {
        OneGrid = new Node[GridX, GridY];
        Vector3 BottomLeft = StartPos.position - Vector3.right * GridSize.x / 2 - Vector3.forward * GridSize.y / 2;

        for (int i = 0; i < GridX; i++)
            for (int j = 0; j < GridY; j++)
            {
                Vector3 worldPoint = BottomLeft + Vector3.right * (i * GridDiameter + GridRadius);
                worldPoint += Vector3.forward * (j * GridDiameter + GridRadius);
                bool IsOneWall = true;
                if(Physics.CheckSphere(worldPoint, GridRadius, WallMask))
                {
                    IsOneWall = false;
                    
                }
                OneGrid[i, j] = new Node(IsOneWall, worldPoint, i, j);
            }
    }
    public Node ToGridPos(Vector3 _WorldPos)
    {
        float XPoint = (_WorldPos.x + GridSize.x / 2) / GridSize.x;
        float YPoint = (_WorldPos.z + GridSize.y / 2) / GridSize.y;
    
        XPoint = Mathf.Clamp01(XPoint);
        YPoint = Mathf.Clamp01(YPoint); 

        int x = Mathf.RoundToInt((GridX - 1) * XPoint);
        int y = Mathf.RoundToInt((GridY - 1) * YPoint);

        return OneGrid[x, y];
    }

    public List<Node> GetVisitNode(Node _StartNode)
    {
        List<Node> VisitNodes = new List<Node>();
        int[] px = {0, -1, 0, 1};
        int[] py = {1, 0, -1, 0};
        int vx;
        int vy;
        for (int i = 0; i < 4; i++){
            vx = _StartNode.x + px[i];
            vy = _StartNode.y + py[i];
            if(vx >= 0 && vx < GridX && vy >= 0 && vy < GridY)
            {
                VisitNodes.Add(OneGrid[vx, vy]);
            }
        }
        return VisitNodes;
    }
/*
    private void OnDrawGizmos()
    {
        foreach(Node node in FindPath)
        {
            Gizmos.DrawCube(node.Pos, Vector3.one * (GridDiameter - Dis));
        }
    }
*/
/*
    private void OnDrawGizmos()
    {
        Debug.Log("Hello");
        Gizmos.DrawCube(transform.position, new Vector3(GridSize.x, 1, GridSize.y));

        if(OneGrid != null){
            foreach(Node node in OneGrid)
            {
                Debug.Log("It's Cool!");
                if(node.IsWall)
                {
                    Gizmos.color = Color.blue;
                }else
                {
                    Gizmos.color = Color.red;
                }
                
                //if(FindPath != null)
                //{
                //    Gizmos.color = Color.red;
                //}

                Gizmos.DrawCube(node.Pos, Vector3.one * (GridDiameter - Dis));
            }
        }
    }
*/
}
