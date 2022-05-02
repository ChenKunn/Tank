using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int x;
    public int y;

    public bool IsWall;
    public Vector3 Pos;

    public Node Parent;
    
    public int GCost;
    public int HCost;
    public int FCost {get {return GCost + HCost;}}

    public Node(bool _IsWall, Vector3 _Pos, int _x, int _y)
    {
        IsWall = _IsWall;
        Pos = _Pos;
        x = _x;
        y = _y;
    }
}
