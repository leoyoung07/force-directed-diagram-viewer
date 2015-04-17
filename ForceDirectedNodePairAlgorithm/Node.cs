/*

Based on the Node class in Bradley Smith's "A Force-Directed Diagram Layout Algorithm"
Released under the BSD license.
http://www.brad-smith.info


Modifications copyright © 2015 Michael Derenardi
Released under the BSD license.
http://www.ficfox.com

*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;

namespace ForceDirectedNodePairAlgorithm
{
    /// <summary>
    /// Manages data for a node.
    /// </summary>
    public sealed class Node
    {
        #region Properties

        /// <summary>
        /// Gets and sets the node's inner color.
        /// </summary>
        public Color InnerColor { get; set; }
        /// <summary>
        /// Gets and sets the label for the node.
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// Gets and sets the label color.
        /// </summary>
        public Color LabelColor { get; set; }
        /// <summary>
        /// Gets and sets the node's outer color.
        /// </summary>
        public Color OuterColor { get; set; }
        /// <summary>
        /// Gets and sets the position for the node.
        /// </summary>
        public Point Position { get; set; }
        /// <summary>
        /// Gets and sets the velocity for the node.
        /// </summary>
        public Vector Velocity { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creats a new instance of the Node class.
        /// </summary>
        /// <param name="label">The label for the node.</param>
        public Node(string label)
        {
            InnerColor = Color.Black;
            Label = label;
            LabelColor = Color.Black;
            OuterColor = Color.Black;
            Position = new Point(0, 0);
            Velocity = new Vector(0, 0);
        }

        /// <summary>
        /// Creats a new instance of the Node class with the specified parameters.
        /// </summary>
        /// <param name="innerColor">The color of the node's inner circle.</param>
        /// <param name="label">The label for the node.</param>
        /// <param name="labelColor">The color of the label.</param>
        /// <param name="outerColor">The color of the node's outer circle.</param>
        public Node(string label, Color labelColor, Color outerColor, Color innerColor)
        {
            InnerColor = innerColor;
            Label = label;
            LabelColor = labelColor;
            OuterColor = outerColor;
            Position = new Point(0, 0);
            Velocity = new Vector(0, 0);
        }

        #endregion
    }
}
