/*

Based on the Diagram class in Bradley Smith's "A Force-Directed Diagram Layout Algorithm"
Released under the BSD license.
http://www.brad-smith.info


Modifications copyright © 2015 Michael Derenardi
Released under the BSD license.
http://www.ficfox.com

*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace ForceDirectedNodePairAlgorithm
{
    /// <summary>
    /// Generates and draws a force directed diagram.
    /// </summary>
    public sealed class DiagramGenerator
    {
        #region Fields

        // Due to fewer iterations, sub-optimal dispersal occurs more often than with the standard algorithm.
        // The following recommended values compensate somewhat for that:
        private const double _attraction = 0.1;
        private double _connectionLength = 5;
        private const double _connectionLengthMax = 100;
        private const double _connectionLengthMin = 1;
        private const double _damping = 0.7;
        private Int32 _minimumDisplacement = 20;
        private const double _repulsion = 10000;
        
        private bool _isGeneratingDiagram = false;
        private const Int32 _iterationsMax = 500;
        private string _nodeLabelFont = "Arial";
        private Int32 _nodeLabelFontSize = 10;
        private Int32 _nodeSize = 8;
        public List<NodePair> _nodePairs = new List<NodePair>();
        private List<Node> _nodes = new List<Node>();
        private Color _connectionParentChildColor = Color.Gray;
        private Color _connectionPeerColor = Color.Black;

        #endregion

        #region Properties

        /// <summary>
        /// Gets and sets the node color for parent-child connections.
        /// </summary>
        public Color ConnectionParentChildColor { get { return _connectionParentChildColor; } set { _connectionParentChildColor = value; } }
        /// <summary>
        /// Gets and sets the node color for peer connections.
        /// </summary>
        public Color ConnectionPeerColor { get { return _connectionPeerColor; } set { _connectionPeerColor = value; } }
        /// <summary>
        /// Gets and sets the length of the connection between nodes.
        /// </summary>
        public double ConnectionLength
        {
            get { return _connectionLength; }
            set
            {
                _connectionLength = value;

                if (_connectionLength < _connectionLengthMin) _connectionLength = _connectionLengthMin;
                if (_connectionLength > _connectionLengthMax) _connectionLength = _connectionLengthMax;
            }
        }
        /// <summary>
        /// Gets the elapsed time for diagram generation in milliseconds.
        /// </summary>
        public Int64 ElapsedGenerationMilliseconds { get; internal set; }
        /// <summary>
        /// Gets the elapsed time for diagram generation in ticks.
        /// </summary>
        public Int64 ElapsedGenerationTicks { get; internal set; }
        /// <summary>
        /// Gets the total iterations for diagram generation.
        /// </summary>
        public Int32 IterationsCount { get; internal set; }
        /// <summary>
        /// Gets and sets the minimum threshold for the combined displacement of all nodes during diagram generation. When
        /// the internal value drops below this threshold, generation stops.
        /// </summary>
        public Int32 MinimumDisplacement { get { return _minimumDisplacement; } set { _minimumDisplacement = value; } }
        /// <summary>
        /// Gets and sets the node label font.
        /// </summary>
        public string NodeLabelFont { get { return _nodeLabelFont; } set { _nodeLabelFont = value; } }
        /// <summary>
        /// Gets and sets the node label font size.
        /// </summary>
        public Int32 NodeLabelFontSize { get { return _nodeLabelFontSize; } set { _nodeLabelFontSize = value; } }
        /// <summary>
        /// Gets and sets the maximum width and height for the node.
        /// </summary>
        public Int32 NodeSize { get { return _nodeSize; } set { _nodeSize = value; } }
        /// <summary>
        /// Gets and sets a value indicating whether node labels are shown.
        /// </summary>
        public bool ShowLabels { get; set; }
        
        #endregion

        #region Events

        /// <summary>
        /// Creates a new instance of the DiagramGenerator class.
        /// </summary>
        public DiagramGenerator()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a uniquely named node to the diagram.
        /// </summary>
        /// <param name="label">The label for the node.</param>
        public void AddNode(string label)
        {
            if (GetNodeIndex(label) != -1) throw new Exception("Label '" + label + "' already exists.");

            Node node = new Node(label); 
            
            for (int i = 0; i < _nodes.Count; i++)
            {
                _nodePairs.Add(new NodePair(node, _nodes[i]));
            }
            _nodes.Add(node);            
        }

        /// <summary>
        /// Adds uniquely named nodes to the diagram.
        /// </summary>
        /// <param name="values">A string array containing the node labels. Duplicates are ignored.</param>
        public void AddNodes(string[] values)
        {
            for (Int32 i = 0; i < values.Length; i++)
            {
                AddNode(values[i]);
            }
        }

        /// <summary>
        /// Calculates the angle between two points.
        /// </summary>
        /// <param name="value1">The first point.</param>
        /// <param name="value2">The second point.</param>
        /// <returns>The angle in degrees.</returns>
        private double CalculateAngle(Point value1, Point value2)
        {
            Point half = new Point(value1.X + ((value2.X - value1.X) / 2), value1.Y + ((value2.Y - value1.Y) / 2));

            double xDifference = half.X - value1.X;
            double yDifference = half.Y - value1.Y;

            if (xDifference == 0) xDifference = 0.001;
            if (yDifference == 0) yDifference = 0.001;

            double angle;
            if (Math.Abs(xDifference) > Math.Abs(yDifference))
            {
                angle = Math.Tanh(yDifference / xDifference) * (180.0 / Math.PI);
                if (xDifference < 0 && yDifference != 0) angle += 180;
            }
            else
            {
                angle = Math.Tanh(xDifference / yDifference) * (180.0 / Math.PI);
                if (yDifference < 0 && xDifference != 0) angle += 180;
                angle = (180 - (angle + 90));
            }

            return angle;
        }

        /// <summary>
        /// Calculates the logical bounds of the diagram. This is used to center and scale the diagram when drawing.
        /// </summary>
        /// <returns>A System.Drawing.Rectangle that fits exactly around every node in the diagram.</returns>
        private Rectangle CalculateDiagramBounds()
        {
            Int32 xMin = Int32.MaxValue;
            Int32 yMin = Int32.MaxValue;
            Int32 xMax = Int32.MinValue;
            Int32 yMax = Int32.MinValue;

            foreach (Node node in _nodes)
            {
                if (node.Position.X < xMin) xMin = Convert.ToInt32(node.Position.X);
                if (node.Position.X > xMax) xMax = Convert.ToInt32(node.Position.X);
                if (node.Position.Y < yMin) yMin = Convert.ToInt32(node.Position.Y);
                if (node.Position.Y > yMax) yMax = Convert.ToInt32(node.Position.Y);
            }

            return Rectangle.FromLTRB(xMin, yMin, xMax, yMax);
        }

        /// <summary>
        /// Calculates the distance between two points.
        /// </summary>
        /// <param name="value1">The first point.</param>
        /// <param name="value2">The second point.</param>
        /// <returns>The distance value as a Double.</returns>
        private double CalculateDistance(Point value1, Point value2)
        {
            double xDistance = (value1.X - value2.X);
            double yDistance = (value1.Y - value2.Y);

            return Math.Sqrt(Math.Pow(xDistance, 2) + Math.Pow(yDistance, 2));
        }

        /// <summary>
        /// Clears the diagram data and image.
        /// </summary>
        public void Clear()
        {
            _isGeneratingDiagram = false;
            _nodePairs.Clear();
            _nodes.Clear();
        }

        /// <summary>
        /// Draws the diagram to the canvas.
        /// </summary>
        public void Draw(Graphics canvas, Rectangle bounds)
        {
            if (_isGeneratingDiagram) return;
            if (canvas == null) throw new Exception("Canvas is null.");

            // Adjust bounds in case that a label is at the bottom of the canvas.
            bounds.Height -= _nodeLabelFontSize;

            Point center = new Point(bounds.X + (bounds.Width / 2), bounds.Y + (bounds.Height / 2));

            // determine the scaling factor
            Rectangle diagramBounds = CalculateDiagramBounds();
            double scale = 1;

            if (diagramBounds.Width > diagramBounds.Height)
            {
                if (diagramBounds.Width != 0) scale = (double)Math.Min(bounds.Width, bounds.Height) / (double)diagramBounds.Width;
            }
            else
            {
                if (diagramBounds.Height != 0) scale = (double)Math.Min(bounds.Width, bounds.Height) / (double)diagramBounds.Height;
            }

            // draw all of the connectors first
            for (int i = 0; i < _nodePairs.Count; i++)
            {
                if (_nodePairs[i].Connection != NodeConnectionOption.None)
                {
                    Point source = ScalePoint(_nodePairs[i].Node1.Position, scale);
                    //source = new Point(source.X + bounds.Left, source.Y + bounds.Top);

                    Point destination = ScalePoint(_nodePairs[i].Node2.Position, scale);
                    //destination = new Point(destination.X + bounds.X, destination.Y + bounds.Y);

                    if (_nodePairs[i].Connection == NodeConnectionOption.ParentChild) canvas.DrawLine(new Pen(_connectionParentChildColor), center + (Size)source, center + (Size)destination);
                    if (_nodePairs[i].Connection == NodeConnectionOption.Peer) canvas.DrawLine(new Pen(_connectionPeerColor), center + (Size)source, center + (Size)destination);
                }
            }

            // then draw all of the nodes
            foreach (Node node in _nodes)
            {
                Point source = ScalePoint(node.Position, scale);
                Size nodeSize = new Size(_nodeSize, _nodeSize);
                Rectangle nodeBounds = new Rectangle(center.X + source.X - (nodeSize.Width / 2), center.Y + source.Y - (nodeSize.Height / 2), nodeSize.Width, nodeSize.Height);
                
                canvas.FillEllipse( new SolidBrush(node.InnerColor), nodeBounds);
                canvas.DrawEllipse(new Pen(node.OuterColor), nodeBounds);

                // Draw the node's label.
                canvas.DrawString(node.Label, new Font(_nodeLabelFont, _nodeLabelFontSize), new SolidBrush(node.LabelColor),
                    new PointF(center.X + source.X - (nodeSize.Width / 2) + 10, center.Y + source.Y - (nodeSize.Height / 2)));
            }
        }

        /// <summary>
        /// Generates the diagram.
        /// </summary>
        /// <param name="maxIterations">Maximum number of iterations before the algorithm terminates.</param>
        /// <param name="seed">The seed value for the random number generator.</param>
        public void Generate(Int32 maxIterations, Int32 seed)
        {
            _isGeneratingDiagram = true;
            Int32 iterations = 0;
            Int32 stopCount = 0;

            Random rnd = new Random(seed);

            // random starting positions can be made deterministic by seeding System.Random with a constant
            for (int i = 0; i < _nodes.Count; i++)
            {
                _nodes[i].Position = new Point(rnd.Next(-50, 50), rnd.Next(-50, 50));
            }

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Reset();
            stopWatch.Start();
            
            // Calculate node positions and velocities in finite snapshots.
            while (true)
            {
                // Calculate forces.
                for (int i = 0; i < _nodePairs.Count; i++ )
                {
                    double proximity = Math.Max(CalculateDistance(_nodePairs[i].Node1.Position, _nodePairs[i].Node2.Position), 1);
                    double node1Angle = CalculateAngle(_nodePairs[i].Node1.Position, _nodePairs[i].Node2.Position);
                    double node2Angle = node1Angle + 180;
                    if (node2Angle > 360) node2Angle -= 360;
                    
                    //
                    // Determine repulsion for each node in pair.
                    //
                    // Coulomb's Law: F = k(Qq/r^2)
                    //
                    double repulsionForce = -(_repulsion / Math.Pow(proximity, 2));

                    _nodePairs[i].Node1.Velocity.Add(new Vector(repulsionForce, node1Angle));
                    _nodePairs[i].Node2.Velocity.Add(new Vector(repulsionForce, node2Angle));
                    
                    //
                    // Connected nodes? Determine attraction for each node in pair.
                    //
                    // Hooke's Law: F = -kx
                    //
                    if (_nodePairs[i].Connection != NodeConnectionOption.None)
                    {
                        double attractionForce = _attraction * Math.Max(proximity - _connectionLength, 0) * (Int32)_nodePairs[i].Connection;

                        _nodePairs[i].Node1.Velocity.Add(new Vector(attractionForce, node1Angle));
                        _nodePairs[i].Node2.Velocity.Add(new Vector(attractionForce, node2Angle));
                    }
                }

                // Update node positions and calculate total displacment.
                double totalDisplacement = 0;

                for (Int32 i = 0; i < _nodes.Count; i++)
                {
                    Node node = _nodes[i];

                    node.Velocity.Multiply(_damping);

                    // Calculate new position based on velocity.
                    Vector currentPosition = new Vector(CalculateDistance(Point.Empty, node.Position), CalculateAngle(Point.Empty, node.Position));
                    currentPosition.Add(node.Velocity);
                    Point newPosition = currentPosition.ToPoint();

                    // Calculate displacement.
                    totalDisplacement += CalculateDistance(node.Position, newPosition);

                    // Update current position.
                    node.Position = new Point(newPosition.X, newPosition.Y);

                    // Reset velocity to zero.
                    node.Velocity.Clear();
                }

                // Done?
                iterations++;
                if (totalDisplacement < MinimumDisplacement) stopCount++;
                if (stopCount > 15 || iterations > maxIterations) break;
            }

            stopWatch.Stop();
            ElapsedGenerationMilliseconds = stopWatch.ElapsedMilliseconds;
            ElapsedGenerationTicks = stopWatch.ElapsedTicks;
            IterationsCount = iterations;

            // Center the diagram around the origin
            Rectangle diagramBounds = CalculateDiagramBounds();
            Point midPoint = new Point(diagramBounds.X + (diagramBounds.Width / 2), diagramBounds.Y + (diagramBounds.Height / 2));

            foreach (Node node in _nodes)
            {
                node.Position -= (Size)midPoint;
            }

            _isGeneratingDiagram = false;
        }

        /// <summary>
        /// Returns the index of the node with the specified label. If not found, -1 is returned.
        /// </summary>
        /// <param name="label">The label to find.</param>
        /// <returns>The index of the node if found, otherwise -1.</returns>
        private Int32 GetNodeIndex(string label)
        {
            Int32 result = -1;
            for (Int32 i = 0; i < _nodes.Count; i++)
            {
                if (_nodes[i].Label == label)
                {
                    result = i;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Removes the specified node from the diagram.
        /// </summary>
        /// <param name="label">The label of the node.</param>
        public void RemoveNode(string label)
        {
            Int32 index = (GetNodeIndex(label));

            // Not found?
            if (index == -1) throw new Exception("Node label '" + label + "' not found."); ;

            // Remove from node pairs.
            Node node = _nodes[index];

            for (int i = _nodePairs.Count - 1; i >= 0 ; i--)
            {
                if (_nodePairs[i].Node1 == node || _nodePairs[i].Node2 == node) _nodePairs.RemoveAt(i);
            }

            _nodes.RemoveAt(index);
        }

        /// <summary>
        /// Applies a scaling factor to the point.
        /// </summary>
        /// <param name="point">The coordinates to scale.</param>
        /// <param name="scale">The scaling factor.</param>
        /// <returns>A System.Drawing.Point representing the scaled coordinates.</returns>
        public Point ScalePoint(Point point, double scale)
        {
            double newX = (double)point.X * scale;
            double newY = (double)point.Y * scale;

            return new Point(Convert.ToInt32(newX), Convert.ToInt32(newY));
        }

        /// <summary>
        /// Updates the connection between two existing nodes.
        /// </summary>
        /// <param name="primaryLabel">The label of the primary node.</param>
        /// <param name="secondaryLabel">The label of the secondary node.</param>
        /// <param name="connection">The kind of connection.</param>
        public void UpdateNodeConnection(string primaryLabel, string secondaryLabel, NodeConnectionOption connection)
        {
            if (GetNodeIndex(primaryLabel) == -1) throw new Exception("Primary label '" + primaryLabel + "' not found.");
            if (GetNodeIndex(secondaryLabel) == -1) throw new Exception("Secondary label '" + secondaryLabel + "' not found.");

            for (int i = 0; i < _nodePairs.Count; i++)
            {
                NodePair pair = _nodePairs[i];

                //
                // Nodes may be mirrored (node1 is node2 and node2 is node1) depending on the order in which nodes were originally added. Check for both variations.
                //
                if (pair.Node1.Label == primaryLabel && pair.Node2.Label == secondaryLabel || pair.Node1.Label == secondaryLabel && pair.Node2.Label == primaryLabel)
                {
                    pair.Connection = connection;
                    break;
                }
            }
        }

        /// <summary>
        ///  Updates the connection between the specified nodes.
        /// </summary>
        /// <param name="primaryLabel">The label of the primary node.</param>
        /// <param name="childLabel">The labels of the secondary nodes.</param>
        /// <param name="connection">The kind of connection.</param>
        public void UpdateNodeConnections(string primaryLabel, string[] secondaryLabels, NodeConnectionOption connection)
        {
            for (Int32 i = 0; i < secondaryLabels.Length; i++)
            {
                UpdateNodeConnection(primaryLabel, secondaryLabels[i], connection);
            }
        }

        #endregion
    }
}
