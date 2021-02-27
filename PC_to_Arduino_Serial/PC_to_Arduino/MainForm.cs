using System;
using System.IO.Ports;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PC_to_Arduino
{
	public partial class MainForm : Form
	{
		static SerialPort _serialPort;
		public byte []Buff = new byte[2];
		
		public MainForm()
		{
			InitializeComponent();
            _serialPort = new SerialPort();
            _serialPort.PortName = "COM5";//Set your board COM
            _serialPort.BaudRate = 9600;
            _serialPort.Open();			
		}
		
		
		
		void Timer1Tick(object sender, EventArgs e)
		{
                string a = _serialPort.ReadExisting();
                textBox1.Text+=a;			
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			Buff[0] = 1; //Th1 
			Buff[1] = 0; //Th2
			_serialPort.Write(Buff,0,2);			
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			Buff[0] = 0;//Th1
			Buff[1] = 0;//Th2
			_serialPort.Write(Buff,0,2);
			
		}

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
