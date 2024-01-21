using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

namespace Test
{
    public class BlockTest : ITraversalProvider
    {
        public HashSet<GraphNode> blockedNodes = new HashSet<GraphNode>();
        public bool CanTraverse(Path path, GraphNode node)
        {
            return DefaultITraversalProvider.CanTraverse(path, node) && !blockedNodes.Contains(node);
        }

        public uint GetTraversalCost(Path path, GraphNode node)
        {
            return DefaultITraversalProvider.GetTraversalCost(path, node);
        }
    }
}
