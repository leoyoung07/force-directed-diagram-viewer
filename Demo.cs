// A Force-Directed Diagram Layout Algorithm
// Bradley Smith - 2010/07/01

// uncomment the following line to animate the iterations of the force-directed algorithm:
//#define ANIMATE

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace ForceDirected
{

    public partial class Demo : Form
    {

        Diagram mDiagram;
        private TrackBar trackBar1;
        private Button button1;
        private NumericUpDown numericUpDown1;
        Random mRandom;

        public Demo()
        {
            this.InitializeComponent();

            mDiagram = new Diagram();
            mRandom = new Random();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // draw with anti-aliasing and a 12 pixel border
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            mDiagram.Draw(e.Graphics, Rectangle.FromLTRB(12, 12, ClientSize.Width - 12, ClientSize.Height - 12));
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            // redraw on resize
            Invalidate();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            mDiagram.Clear();

            /*
            // create a basic, random diagram that is between 2 and 4 levels deep 
            // and has between 1 and 10 leaf nodes per branch
            Node node = new SpotNode(Color.Black);
            mDiagram.AddNode(node);

            for (int i = 0; i < mRandom.Next(1, 10); i++)
            {
                Node child = new SpotNode(Color.Navy);
                node.AddChild(child);

                for (int j = 0; j < mRandom.Next(0, 10); j++)
                {
                    Node grandchild = new SpotNode(Color.Blue);
                    child.AddChild(grandchild);

                    for (int k = 0; k < mRandom.Next(0, 10); k++)
                    {
                        Node descendant = new SpotNode(Color.CornflowerBlue);
                        grandchild.AddChild(descendant);
                    }
                }
            } */

            // create nodes
            Node count = new SpotNode();
            count.Name = "Dracula";

            Node harker = new SpotNode();
            harker.Name = "Harker";
            
            Node holmsford = new SpotNode();
            holmsford.Name = "Holmsford";
            
            Node lucy = new SpotNode();
            lucy.Name = "Lucy";
            
            Node mina = new SpotNode();
            mina.Name = "Mina";
            
            Node quincy = new SpotNode();
            quincy.Name = "Quincy";
            
            Node renfield = new SpotNode();
            renfield.Name = "Renfield";
            
            Node steward = new SpotNode();
            steward.Name = "Dr. Steward";
            
            Node vanhelsing = new SpotNode();
            vanhelsing.Name = "Van Helsing";
            
            Node weirdsisters = new SpotNode();
            weirdsisters.Name = "Weird Sisters";

            // Define node relationships
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
/*
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
            vanhelsing.AddChild(quincy);
            vanhelsing.AddChild(holmsford);
            vanhelsing.AddChild(mina);
            vanhelsing.AddChild(lucy);
            vanhelsing.AddChild(steward);
            vanhelsing.AddChild(renfield);

            weirdsisters.AddChild(count);
            weirdsisters.AddChild(harker);
            weirdsisters.AddChild(quincy);
            weirdsisters.AddChild(renfield);
*/
            // Add nodes to diagram
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

            // run the force-directed algorithm (async)
            Cursor = Cursors.WaitCursor;
            btnGenerate.Enabled = false;
            mDiagram.Arrange(0.5f, trackBar1.Value, 500, false);
            //Thread bg = new Thread(mDiagram.Arrange);
            //bg.IsBackground = true;
            //bg.Start();

            Graphics g = CreateGraphics();

#if ANIMATE
			while (bg.IsAlive) {
				Invalidate();
				Application.DoEvents();
				Thread.Sleep(20);
			}
#else
            //bg.Join();
#endif

            btnGenerate.Enabled = true;
            Cursor = Cursors.Default;

            Invalidate();
        }
        /*
        private void InitializeComponent()
        {
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(986, 43);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar1.Size = new System.Drawing.Size(45, 475);
            this.trackBar1.TabIndex = 0;
            this.trackBar1.Value = 5;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // Demo
            // 
            this.ClientSize = new System.Drawing.Size(1043, 547);
            this.Controls.Add(this.trackBar1);
            this.Name = "Demo";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        */
        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            mDiagram.Arrange(0.5f, trackBar1.Value, 500, false);
            //Thread bg = new Thread(mDiagram.Arrange);
            //bg.IsBackground = true;
            //bg.Start();

            Graphics g = CreateGraphics();

            //bg.Join();
            btnGenerate.Enabled = true;
            Cursor = Cursors.Default;

            Invalidate();
        }

        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1119, 746);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(972, 749);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 1;
            // 
            // Demo
            // 
            this.ClientSize = new System.Drawing.Size(1224, 781);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.button1);
            this.Name = "Demo";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }
        /*
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Demo
            // 
            this.ClientSize = new System.Drawing.Size(740, 560);
            this.Name = "Demo";
            this.ResumeLayout(false);

        } */
    }
}
