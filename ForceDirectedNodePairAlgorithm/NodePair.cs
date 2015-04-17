/*

Copyright © 2015 Michael Derenardi
Released under the BSD license.
http://www.ficfox.com

*/

using System;

namespace ForceDirectedNodePairAlgorithm
{
    /// <summary>
    /// Manages a unique node interaction pair.
    /// </summary>
    public sealed class NodePair
    {
        #region Properties

        /// <summary>
        /// Gets and sets Node1.
        /// </summary>
        public Node Node1 { get; set; }
        /// <summary>
        /// Gets and sets Node2.
        /// </summary>
        public Node Node2 { get; set; }
        /// <summary>
        /// Gets and sets the kind of connection between Node1 and Node2.
        /// </summary>
        public NodeConnectionOption Connection { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the NodePair class which represents a unique node pairing.
        /// </summary>
        /// <param name="node1">Node1 of the pair.</param>
        /// <param name="node2">Node2 of the pair.</param>
        public NodePair(Node node1, Node node2)
        {
            Node1 = node1;
            Node2 = node2;
            Connection = NodeConnectionOption.None;
        }
    
        #endregion
    }
}
