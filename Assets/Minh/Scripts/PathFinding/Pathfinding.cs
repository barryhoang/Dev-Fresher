using System;
using System.Collections.Generic;
using UnityEngine;

namespace Minh

{
    public class Pathfinding : MonoBehaviour
    {
        private Grid _grid;
        public Transform StartPosition;
        public Transform TargetPosition;

        private void Awake()
        {
            _grid = GetComponent<Grid>();
        }

        private void Update()
        {
            FindPath(StartPosition.position, TargetPosition.position);
         
        }

        private void FindPath(Vector3 a_StartPos, Vector3 a_TargetPos)
        {
            Node StartNode = _grid.NodeFromWorldPosition(a_StartPos);
            Node TargetNode = _grid.NodeFromWorldPosition(a_TargetPos);
            
            List<Node> OpenList=new List<Node>();
            HashSet<Node>ClosedList=new HashSet<Node>();
            OpenList.Add(StartNode);
            while (OpenList.Count > 0)
            {
                Node CurrentNode = OpenList[0];
                for (int i = 1; i < OpenList.Count; i++)
                {
                    if (OpenList[i].FCost < CurrentNode.FCost || OpenList[i].FCost == CurrentNode.FCost &&
                        OpenList[i].hCost < CurrentNode.hCost)
                    {
                        CurrentNode = OpenList[i];
                    }

                    OpenList.Remove(CurrentNode);
                    ClosedList.Add(CurrentNode);
                }

                if (CurrentNode == TargetNode)
                {
                    GetFinalPath(StartNode, TargetNode);
                }

                foreach (Node NeighboreNode in _grid.GetNeighboringNodes(CurrentNode))
                {
                    
                }
            }
        }
        private void GetFinalPath(Node a_StartingNode, Node a_EndNode)
        {
            List<Node> FinalPath=new List<Node>();
            Node CurrentNode = a_EndNode;
            while (CurrentNode != a_StartingNode)
            {
                FinalPath.Add(CurrentNode);
                CurrentNode = CurrentNode.Parent;
            }
            FinalPath.Reverse();
            _grid.FinalPath = FinalPath;
        }
    }
   
}