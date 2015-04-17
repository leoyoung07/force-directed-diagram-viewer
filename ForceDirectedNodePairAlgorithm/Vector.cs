/*

Based on the Vector class in Bradley Smith's "A Force-Directed Diagram Layout Algorithm"
Released under the BSD license.
http://www.brad-smith.info


Modifications copyright © 2015 Michael Derenardi
Released under the BSD license.
http://www.ficfox.com

*/

using System;
using System.Drawing;


namespace ForceDirectedNodePairAlgorithm
{
    /// <summary>
    /// Represents a 2D vector.
    /// </summary>
    public sealed class Vector
    {
        #region Fields

        private double _x = 0;
        private double _y = 0;

        #endregion

        #region Properties

        /// <summary>
        /// Gets and sets the X component of the vector.
        /// </summary>
        public double X { get { return _x; } set { _x = value; } }
        /// <summary>
        /// Gets and sets the Y component of the vector.
        /// </summary>
        public double Y { get { return _y; } set { _y = value; } }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the Vector class using the specified values.
        /// </summary>
        /// <param name="direction">The direction of the new vector.</param>
        /// <param name="magnitude">The magnitude of the new vector.</param>
        public Vector(double magnitude, double direction)
        {
            X = magnitude * Math.Cos((Math.PI / 180.0) * direction);
            Y = magnitude * Math.Sin((Math.PI / 180.0) * direction);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a vector to this vector.
        /// </summary>
        /// <param name="value">The vector to add.</param>
        public void Add(Vector value)
        {
            X += value.X;
            Y += value.Y;
        }

        /// <summary>
        /// Clears the vector components.
        /// </summary>
        public void Clear()
        {
            X = 0;
            Y = 0;
        }

        /// <summary>
        /// Multiplies this vector with another vector.
        /// </summary>
        /// <param name="value">The vector that will be multiplied with this vector.</param>
        public void Multiply(Vector value)
        {
            X *= value.X;
            Y *= value.Y;
        }

        /// <summary>
        /// Multiplies this vector with a value.
        /// </summary>
        /// <param name="value">The value that will be multiplied with this vector.</param>
        public void Multiply(double value)
        {
            X *= value;
            Y *= value;
        }

        /// <summary>
        /// Returns the vector as a point.
        /// </summary>
        /// <returns>Returns the vector as a System.Drawing.Point.</returns>
        public Point ToPoint()
        {
            return new Point((int)X, (int)Y);
        }
    
        #endregion
    }
}
