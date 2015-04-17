/*

Based on the Demo application in Bradley Smith's "A Force-Directed Diagram Layout Algorithm"
Released under the BSD license.
http://www.brad-smith.info


Modifications copyright © 2015 Michael Derenardi
Released under the BSD license.
http://www.ficfox.com

*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForceDirectedDiagramViewer
{
    public partial class Form1 : Form
    {
        #region Fields

        private ForceDirectedAlgorithm.Diagram mDiagram = new ForceDirectedAlgorithm.Diagram();
        private ForceDirectedNodePairAlgorithm.DiagramGenerator npDiagram = new ForceDirectedNodePairAlgorithm.DiagramGenerator();
        private Int32 randomSeed = 0;

        #endregion

        #region Constructor

        public Form1()
        {
            InitializeComponent();

            comboBoxChoice.Text = "Structure";
        }

        #endregion

        #region Events

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            buttonGenerate.Enabled = false;
            
            mDiagram.Clear();
            npDiagram.Clear();
            
            if (comboBoxChoice.Text == "Peers") CreatePeers();
            if (comboBoxChoice.Text == "Snowflake") CreateSnowflake();
            if (comboBoxChoice.Text == "Structure") CreateStructure();

            // Create a random seed used by both algorithms so output can be visually compared by user.
            randomSeed = Convert.ToInt32(DateTime.Now.Ticks & 0x0000FFFF);

            UpdateViewer();

            buttonGenerate.Enabled = true;
            Cursor = Cursors.Default;
        }

        private void numericUpDownMinimumDisplacement_ValueChanged(object sender, EventArgs e)
        {
            UpdateViewer();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            mDiagram.Draw(e.Graphics, Rectangle.FromLTRB(20, 20, panelNodesDiagram.Width - 20, panelNodesDiagram.Height - 20));
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            npDiagram.Draw(e.Graphics, Rectangle.FromLTRB(20, 20, panelNodePairsDiagram.Width - 20, panelNodePairsDiagram.Height - 20));
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            UpdateViewer();
        }

        #endregion

        #region Private Methods

        private void CreatePeers()
        {
            mDiagram.Clear();
            npDiagram.Clear();

            //
            // Create nodes for the standard diagram.
            //
            ForceDirectedAlgorithm.Node count = new ForceDirectedAlgorithm.SpotNode();
            count.Label = "Dracula";

            ForceDirectedAlgorithm.Node harker = new ForceDirectedAlgorithm.SpotNode();
            harker.Label = "Harker";

            ForceDirectedAlgorithm.Node holmsford = new ForceDirectedAlgorithm.SpotNode();
            holmsford.Label = "Holmsford";

            ForceDirectedAlgorithm.Node lucy = new ForceDirectedAlgorithm.SpotNode();
            lucy.Label = "Lucy";

            ForceDirectedAlgorithm.Node mina = new ForceDirectedAlgorithm.SpotNode();
            mina.Label = "Mina";

            ForceDirectedAlgorithm.Node quincy = new ForceDirectedAlgorithm.SpotNode();
            quincy.Label = "Quincy";

            ForceDirectedAlgorithm.Node renfield = new ForceDirectedAlgorithm.SpotNode();
            renfield.Label = "Renfield";

            ForceDirectedAlgorithm.Node steward = new ForceDirectedAlgorithm.SpotNode();
            steward.Label = "Dr. Steward";

            ForceDirectedAlgorithm.Node vanhelsing = new ForceDirectedAlgorithm.SpotNode();
            vanhelsing.Label = "Van Helsing";

            ForceDirectedAlgorithm.Node weirdsisters = new ForceDirectedAlgorithm.SpotNode();
            weirdsisters.Label = "Weird Sisters";

            // Add nodes to diagram - order should match those in the other diagram.
            mDiagram.AddNode(count);
            mDiagram.AddNode(harker);
            mDiagram.AddNode(holmsford);
            mDiagram.AddNode(lucy);
            mDiagram.AddNode(mina);
            mDiagram.AddNode(quincy);
            mDiagram.AddNode(renfield);
            mDiagram.AddNode(steward);
            mDiagram.AddNode(vanhelsing);
            mDiagram.AddNode(weirdsisters);

            // Add node children
            count.AddChild(quincy);
            count.AddChild(renfield);
            count.AddChild(vanhelsing);
            count.AddChild(weirdsisters);

            harker.AddChild(mina);
            harker.AddChild(renfield);
            harker.AddChild(steward);
            harker.AddChild(weirdsisters);

            holmsford.AddChild(lucy);
            holmsford.AddChild(quincy);
            holmsford.AddChild(vanhelsing);

            lucy.AddChild(holmsford);
            lucy.AddChild(mina);
            lucy.AddChild(vanhelsing);

            mina.AddChild(harker);
            mina.AddChild(lucy);
            mina.AddChild(renfield);
            mina.AddChild(steward);
            mina.AddChild(vanhelsing);

            quincy.AddChild(count);
            quincy.AddChild(holmsford);
            quincy.AddChild(weirdsisters);

            renfield.AddChild(count);
            renfield.AddChild(harker);
            renfield.AddChild(mina);
            renfield.AddChild(steward);
            renfield.AddChild(vanhelsing);
            renfield.AddChild(weirdsisters);

            steward.AddChild(harker);
            steward.AddChild(mina);
            steward.AddChild(renfield);
            steward.AddChild(vanhelsing);

            vanhelsing.AddChild(count);
            vanhelsing.AddChild(holmsford);
            vanhelsing.AddChild(lucy);
            vanhelsing.AddChild(mina);
            vanhelsing.AddChild(renfield);
            vanhelsing.AddChild(steward);

            weirdsisters.AddChild(count);
            weirdsisters.AddChild(harker);
            weirdsisters.AddChild(quincy);
            weirdsisters.AddChild(renfield);


            //
            // Add nodes to node pair diagram
            //
            string[] npNodes = new string[] { "Dracula", "Harker", "Holmsford", "Lucy", "Mina", "Quincy", "Renfield", "Dr. Steward", "Van Helsing", "Weird Sisters" };
            npDiagram.AddNodes(npNodes);

            // Add peer connections between nodes.
            npDiagram.UpdateNodeConnections("Dracula", new string[] { "Quincy", "Renfield", "Van Helsing", "Weird Sisters" }, ForceDirectedNodePairAlgorithm.NodeConnectionOption.Peer);
            npDiagram.UpdateNodeConnections("Harker", new string[] { "Mina", "Renfield", "Dr. Steward", "Weird Sisters" }, ForceDirectedNodePairAlgorithm.NodeConnectionOption.Peer);
            npDiagram.UpdateNodeConnections("Holmsford", new string[] { "Lucy", "Quincy", "Van Helsing" }, ForceDirectedNodePairAlgorithm.NodeConnectionOption.Peer);
            npDiagram.UpdateNodeConnections("Lucy", new string[] { "Holmsford", "Mina", "Van Helsing" }, ForceDirectedNodePairAlgorithm.NodeConnectionOption.Peer);
            npDiagram.UpdateNodeConnections("Mina", new string[] { "Harker", "Lucy", "Renfield", "Dr. Steward", "Van Helsing" }, ForceDirectedNodePairAlgorithm.NodeConnectionOption.Peer);
            npDiagram.UpdateNodeConnections("Quincy", new string[] { "Dracula", "Holmsford", "Weird Sisters" }, ForceDirectedNodePairAlgorithm.NodeConnectionOption.Peer);
            npDiagram.UpdateNodeConnections("Renfield", new string[] { "Dracula", "Harker", "Mina", "Dr. Steward", "Van Helsing", "Weird Sisters" }, ForceDirectedNodePairAlgorithm.NodeConnectionOption.Peer);
            npDiagram.UpdateNodeConnections("Dr. Steward", new string[] { "Harker", "Mina", "Renfield", "Van Helsing" }, ForceDirectedNodePairAlgorithm.NodeConnectionOption.Peer);
            npDiagram.UpdateNodeConnections("Van Helsing", new string[] { "Dracula", "Holmsford", "Lucy", "Mina", "Renfield", "Dr. Steward" }, ForceDirectedNodePairAlgorithm.NodeConnectionOption.Peer);
            npDiagram.UpdateNodeConnections("Weird Sisters", new string[] { "Dracula", "Harker", "Quincy", "Renfield" }, ForceDirectedNodePairAlgorithm.NodeConnectionOption.Peer);
        }

        private void CreateSnowflake()
        {
            mDiagram.Clear();
            npDiagram.Clear();

            //
            // Create nodes for the standard diagram.
            //
            ForceDirectedAlgorithm.Node center = new ForceDirectedAlgorithm.SpotNode();
            center.Label = "Center";

            ForceDirectedAlgorithm.Node spoke1 = new ForceDirectedAlgorithm.SpotNode();
            spoke1.Label = "Spoke1";

            ForceDirectedAlgorithm.Node spoke2 = new ForceDirectedAlgorithm.SpotNode();
            spoke2.Label = "Spoke2";

            ForceDirectedAlgorithm.Node spoke3 = new ForceDirectedAlgorithm.SpotNode();
            spoke3.Label = "Spoke3";

            ForceDirectedAlgorithm.Node spoke4 = new ForceDirectedAlgorithm.SpotNode();
            spoke4.Label = "Spoke4";

            ForceDirectedAlgorithm.Node spoke5 = new ForceDirectedAlgorithm.SpotNode();
            spoke5.Label = "Spoke5";

            ForceDirectedAlgorithm.Node spoke6 = new ForceDirectedAlgorithm.SpotNode();
            spoke6.Label = "Spoke6";

            // Add nodes to diagram - order should match those in the other diagram.
            mDiagram.AddNode(center);
            mDiagram.AddNode(spoke1);
            mDiagram.AddNode(spoke2);
            mDiagram.AddNode(spoke3);
            mDiagram.AddNode(spoke4);
            mDiagram.AddNode(spoke5);
            mDiagram.AddNode(spoke6);

            // Add node children
            center.AddChild(spoke1);
            center.AddChild(spoke2);
            center.AddChild(spoke3);
            center.AddChild(spoke4);
            center.AddChild(spoke5);
            center.AddChild(spoke6);


            //
            // Add nodes to node pair diagram
            //
            string[] npNodes = new string[] { "Center", "Spoke1", "Spoke2", "Spoke3", "Spoke4", "Spoke5", "Spoke6" };
            npDiagram.AddNodes(npNodes);

            // Add parent-child connections between nodes.
            npDiagram.UpdateNodeConnections("Center", new string[] { "Spoke1", "Spoke2", "Spoke3", "Spoke4", "Spoke5", "Spoke6" },
                ForceDirectedNodePairAlgorithm.NodeConnectionOption.ParentChild);
        }

        private void CreateStructure()
        {
            mDiagram.Clear();
            npDiagram.Clear();

            //
            // Create nodes for the standard diagram.
            //
            ForceDirectedAlgorithm.Node[] mNodes = new ForceDirectedAlgorithm.SpotNode[34];
            for (int i = 0; i < 34; i++)
            {
                ForceDirectedAlgorithm.Node node = new ForceDirectedAlgorithm.SpotNode();
                node.Label = i.ToString();
                mNodes[i] = node;

                // Add nodes to diagram - order should match those in the other diagram.
                mDiagram.AddNode(node);
            }

            // Add node children
            mNodes[0].AddChild(mNodes[1]);
            mNodes[0].AddChild(mNodes[2]);
            mNodes[0].AddChild(mNodes[3]);
            mNodes[0].AddChild(mNodes[4]);
            mNodes[5].AddChild(mNodes[0]);
            mNodes[5].AddChild(mNodes[6]);
            mNodes[5].AddChild(mNodes[7]);
            mNodes[7].AddChild(mNodes[8]);
            mNodes[7].AddChild(mNodes[9]);
            mNodes[7].AddChild(mNodes[10]);
            mNodes[11].AddChild(mNodes[5]);
            mNodes[11].AddChild(mNodes[12]);
            mNodes[11].AddChild(mNodes[13]);
            mNodes[11].AddChild(mNodes[20]);
            mNodes[13].AddChild(mNodes[14]);
            mNodes[13].AddChild(mNodes[15]);
            mNodes[13].AddChild(mNodes[17]);
            mNodes[15].AddChild(mNodes[16]);
            mNodes[17].AddChild(mNodes[18]);
            mNodes[17].AddChild(mNodes[19]);
            mNodes[20].AddChild(mNodes[21]);
            mNodes[20].AddChild(mNodes[23]);
            mNodes[20].AddChild(mNodes[25]);
            mNodes[20].AddChild(mNodes[28]);
            mNodes[21].AddChild(mNodes[22]);
            mNodes[23].AddChild(mNodes[24]);
            mNodes[25].AddChild(mNodes[26]);
            mNodes[25].AddChild(mNodes[27]);
            mNodes[28].AddChild(mNodes[29]);
            mNodes[28].AddChild(mNodes[30]);
            mNodes[28].AddChild(mNodes[31]);
            mNodes[28].AddChild(mNodes[32]);
            mNodes[28].AddChild(mNodes[33]);

            //
            // Add nodes to node pair diagram
            //
            string[] npNodes = new string[34];
            for (int i = 0; i < 34; i++)
            {
                npNodes[i] = i.ToString();
            }
            npDiagram.AddNodes(npNodes);

            // Add parent-child connections between nodes.
            npDiagram.UpdateNodeConnections("0", new string[] { "1", "2", "3", "4" }, ForceDirectedNodePairAlgorithm.NodeConnectionOption.ParentChild);
            npDiagram.UpdateNodeConnections("5", new string[] { "0", "6", "7" }, ForceDirectedNodePairAlgorithm.NodeConnectionOption.ParentChild);
            npDiagram.UpdateNodeConnections("7", new string[] { "8", "9", "10" }, ForceDirectedNodePairAlgorithm.NodeConnectionOption.ParentChild);
            npDiagram.UpdateNodeConnections("11", new string[] { "5", "12", "13", "20" }, ForceDirectedNodePairAlgorithm.NodeConnectionOption.ParentChild);
            npDiagram.UpdateNodeConnections("13", new string[] { "14", "15", "17" }, ForceDirectedNodePairAlgorithm.NodeConnectionOption.ParentChild);
            npDiagram.UpdateNodeConnections("15", new string[] { "16" }, ForceDirectedNodePairAlgorithm.NodeConnectionOption.ParentChild);
            npDiagram.UpdateNodeConnections("17", new string[] { "18", "19" }, ForceDirectedNodePairAlgorithm.NodeConnectionOption.ParentChild);
            npDiagram.UpdateNodeConnections("20", new string[] { "21", "23", "25", "28" }, ForceDirectedNodePairAlgorithm.NodeConnectionOption.ParentChild);
            npDiagram.UpdateNodeConnections("21", new string[] { "22" }, ForceDirectedNodePairAlgorithm.NodeConnectionOption.ParentChild);
            npDiagram.UpdateNodeConnections("23", new string[] { "24" }, ForceDirectedNodePairAlgorithm.NodeConnectionOption.ParentChild);
            npDiagram.UpdateNodeConnections("25", new string[] { "26", "27" }, ForceDirectedNodePairAlgorithm.NodeConnectionOption.ParentChild);
            npDiagram.UpdateNodeConnections("28", new string[] { "29", "30", "31", "32", "33" }, ForceDirectedNodePairAlgorithm.NodeConnectionOption.ParentChild);
        }

        private void ShowStatistics()
        {
            labelNodes.Text = "Nodes Algorithm:  " + mDiagram.ElapsedGenerationMilliseconds.ToString() + " ms (" + mDiagram.ElapsedGenerationTicks.ToString() + " ticks)" +
                ",  Iterations: " + mDiagram.IterationsCount.ToString();

            labelNodePairs.Text = "Node Pairs Algorithm:  " + npDiagram.ElapsedGenerationMilliseconds.ToString() + " ms (" + npDiagram.ElapsedGenerationTicks.ToString() +
                " ticks),  Iterations: " + npDiagram.IterationsCount.ToString() + ",  Perfomance:  " +
                ((mDiagram.ElapsedGenerationTicks * 100) / npDiagram.ElapsedGenerationTicks).ToString() + "%";

            labelSeed.Text = "Seed: " + randomSeed.ToString();
        }

        private void UpdateViewer()
        {
            mDiagram.MinimumDisplacement = (Int32)numericUpDownMinimumDisplacement.Value;
            npDiagram.MinimumDisplacement = (Int32)numericUpDownMinimumDisplacement.Value;
            npDiagram.ConnectionLength = trackBar1.Value;

            mDiagram.Arrange(0.5f, trackBar1.Value, 500, randomSeed);
            npDiagram.Generate(500, randomSeed);

            labelConnectionLengthValue.Text = trackBar1.Value.ToString();
            ShowStatistics();

            panelNodesDiagram.Invalidate();
            panelNodePairsDiagram.Invalidate();
        }

        #endregion
    }
}
