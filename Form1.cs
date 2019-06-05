//#define My_Debug

using MoleShooterFinal.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoleShooterFinal
{
    public partial class MoleShooter : Form
    {
      //  Bitmap myBitmap;

        const int SplatNum = 3;
        const int FrameNum = 10;

        bool splat = false;
        int _gameFrame = 0;
        int _splatTime = 0;

        int _hits = 0;
        int _misses = 0;
        int _totalShots = 0;
        double _averageHits = 0.0;
        int _skill = 0;

#if My_Debug
        int _cursX = 0;
        int _cursY = 0;
#endif
        CBird _bird;
        CSign _sign;
        CSplat _splat;
        CScoreFrame _scorefr;

        public bool Close1 { get; set; }

        public MoleShooter()
        {
            InitializeComponent();

            Bitmap b = new Bitmap(Resources.Site);
            this.Cursor = CustomCursor.CreateCursor(b, b.Height / 2, b.Width / 2);

            _scorefr = new CScoreFrame() { Left = 15, Top = 15 };
            _sign = new CSign() { Left = 740, Top = 15 };
            _bird = new CBird() { Left = 10, Top = 200};
            _splat = new CSplat();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void timerGameLoop_Tick(object sender, EventArgs e)
        {
            if (_gameFrame >= FrameNum)
            {
                UpdateBird();
                _gameFrame = 0;
            }
            if (splat)
            {
                if (_splatTime >= SplatNum)
                {
                    splat = false;
                    _splatTime = 0;
                    UpdateBird();
                }
                _splatTime++;
            }
            this.Refresh();
            _gameFrame++;

            this.Refresh();
        }
        private void UpdateBird()
        {
            Random rnd = new Random();
            _bird.Update(
                rnd.Next(Resources.flyingbird.Width, this.Width - Resources.flyingbird.Width),
                rnd.Next(this.Height / 2, this.Height - Resources.flyingbird.Height * 2)
                );
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            DoubleBuffered = true;
            Graphics dc = e.Graphics;
            if (splat == true)
            {
                _splat.DrawImage(dc);
            }
            else
            {
                _bird.DrawImage(dc);
            }

            _scorefr.DrawImage(dc);
            _sign.DrawImage(dc);

#if My_Debug
            TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.EndEllipsis;
            Font _font = new System.Drawing.Font("Stencil",12,FontStyle.Regular);
            TextRenderer.DrawText(dc, "X= " + _cursX.ToString() + ":"+"Y= "+ _cursY.ToString(), _font,
                new Rectangle(0, 0,120,20), SystemColors.ControlText, flags);
#endif
            
                TextFormatFlags flags = TextFormatFlags.Left;
                Font _font = new System.Drawing.Font("Stencil", 12, FontStyle.Regular);

                TextRenderer.DrawText(e.Graphics, "Shots: " + _totalShots.ToString(), _font, new Rectangle(139, 43, 297, 43), SystemColors.ControlText, flags);
                TextRenderer.DrawText(e.Graphics, "Hits: " + _hits.ToString(), _font, new Rectangle(139, 63, 297, 43), SystemColors.ControlText, flags);
                TextRenderer.DrawText(e.Graphics, "Misses: " + _misses.ToString(), _font, new Rectangle(139, 83, 297, 43), SystemColors.ControlText, flags);
                TextRenderer.DrawText(e.Graphics, "AVG: " + _averageHits.ToString("F0") + "%", _font, new Rectangle(139, 103, 297, 43), SystemColors.ControlText, flags);
                TextRenderer.DrawText(e.Graphics, "SKILL: " + _skill.ToString(), _font, new Rectangle(139, 133, 297, 43), SystemColors.ControlText, flags);

                base.OnPaint(e);

        } 

        private void MoleShooter_MouseMove(object sender, MouseEventArgs e)
        {
#if My_Debug
            _cursX = e.X;
            _cursY = e.Y;
#endif
          ///  this.Refresh();
        }


        private void MoleShooter_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.X > 725 && e.X < 945 && e.Y > 24 && e.Y < 49) /// start
            {
                timerGameLoop.Start();
                label1.Visible = false;
            }
            ////////////////////////
           else if (e.X > 735 && e.X < 947 && e.Y > 61 && e.Y < 86) //stop
            {
                timerGameLoop.Stop();
                if (_averageHits <= 20.0)
                {
                    // label1.Visible = false;
                    label1.Visible = true;
                    label1.Text = "Beginner";
                }
                else if (_averageHits > 20.0 && _averageHits <= 50.0)
                {
                    // label1.Visible = false;
                    label1.Visible = true;
                    label1.Text = "Fan";
                }
                else if (_averageHits > 50.0 && _averageHits <= 100.0)
                {
                    // label1.Visible = false;
                    label1.Visible = true;
                    label1.Text = "Professional";
                }
                else
                {
                    label1.Visible = true;
                    label1.Text = "Very Good!";
                }
            }
            /////////////////////////////
            else if (e.X > 730 && e.X < 938 && e.Y > 94 && e.Y < 118)  /// reset
            {
                timerGameLoop.Stop();
                timerGameLoop.Enabled = true;
                label1.Visible = false;
                _hits = 0;
                _misses = 0;
                _totalShots = 0;
                _averageHits = 0;
            }
            /////////////////////////////////////
            else if (e.X > 720 && e.X < 941 && e.Y > 125 && e.Y < 149) ///rating
            {
                Form3 form3 = new Form3();
                this.Hide();
                if(form3.ShowDialog()==DialogResult.OK)
                {
                    this.Visible = true;
                }
                else
                    this.Visible = true;
            }
            ////////////////////////////////////
            else if (e.X > 737 && e.X < 947 && e.Y > 170 && e.Y < 183)  ///quit
            {
                timerGameLoop.Stop();
                DialogResult dr = MessageBox.Show("    Are you sure?    ", "Exit", MessageBoxButtons.YesNo);
               // timerGameLoop.Stop();

                if (dr == DialogResult.Yes)
                {
                    Close1 = true;
                    this.Close();

                }
                else if (dr == DialogResult.No)
                {
                    Close1 = false;
                }

            }
            ////////////////////////////////////////
           else
            {
                if(_bird.Hit(e.X, e.Y))
                {
                    splat = true;
                    _splat.Left = _bird.Left - Resources.Splat.Width / 3;
                    _splat.Top = _bird.Top - Resources.Splat.Height / 3;
                    _hits++;
                }
                else
                {
                    _misses++;
                }
                
                _totalShots = _misses + _hits;
               _averageHits = (double) _hits / (double)_totalShots * 100.0;
            }
            FireGun();
        }

        private void FireGun()
        {
            SoundPlayer simpleSound = new SoundPlayer(Resources.GunShot);
            simpleSound.Play();
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

   
    }
}
