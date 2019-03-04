using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace CustomerControl
{
    public partial class BlinkLabel : LabelX
    {

        private Color _baseColor;
        private Color _blinkColor;
        private bool _textMove;
        private int _interval = 1000;
        private StringAlignment _stringAlignment;
       
        public BlinkLabel()
        {
            InitializeComponent();
            timer1.Tick+=timer1_Tick;
           
        }

      
       

        /// <summary>
        /// get or set label blink property
        /// </summary>
        [Description("label blink or not"), Category("Action")] 
        public bool Blink
        {
            get
            {
                return _blink;
            }
            set
            {
                _blink = value;
                if (_blink)
                {
                    StartBlink();
                }
                else
                {
                    StopBlink();
                }
            }
        }
        [Description("label back color"), Category("Appearance")] 
        public Color BlinkColor
        {
            get { return _blinkColor; }
            set
            {
                _baseColor = BackColor;
                _blinkColor = value;
            }
        }
        [Description("text move or not when blink"), Category("Appearance")] 
        public bool TextMove
        {
            get { return _textMove; }
            set
            {
                _stringAlignment = TextAlignment;
                _textMove = value;
            }
        }
        [Description("blink interval"), Category("Appearance")] 
        public int Interval
        {
            get { return _interval; }
            set
            {
                timer1.Interval = value;
                _interval = value;
            }
        }

       

        private void timer1_Tick(object sender, EventArgs e)
        {
            BackColor = BackColor == _blinkColor ? _baseColor : _blinkColor;
            if (_textMove)
            {
                TextAlignment = (StringAlignment)((Convert.ToInt32(TextAlignment) + 1) % 3);
            }
        }

        private bool _blink;


        
        private void StartBlink()
        {
          
           
            timer1.Enabled = true;
           
           
        }

        private void StopBlink()
        {
            timer1.Enabled = false;
            BackColor = _baseColor;
            TextAlignment = _stringAlignment;
           
        }
        
    }
}
