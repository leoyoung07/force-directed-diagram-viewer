using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForceDirectedNodePairAlgorithm
{
    public enum NodeConnectionOption
    {
        /// <summary>
        /// No connection exists between the nodes.
        /// </summary>
        None = 0,
        /// <summary>
        /// Represents a parent-child connection between the nodes.
        /// </summary>
        ParentChild = 1,
        /// <summary>
        /// Represents dual parent-child connections between the nodes. Each node is the
        /// parent and the child of the other node.
        /// </summary>
        Peer = 2
    }
}
