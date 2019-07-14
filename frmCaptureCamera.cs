using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;

using System.ServiceModel;

namespace CaptureCamera
{
    public partial class frmCaptureCamera : Form
    {
        private VideoCapture _capture = null;
        private bool _captureInProgress;
        private Mat _frame;
        public frmCaptureCamera()
        {
            InitializeComponent();
            CvInvoke.UseOpenCL = false;
            try
            {
                _capture = new VideoCapture();
                _capture.ImageGrabbed += ProcessFrame;
            }
            catch (NullReferenceException excpt)
            {
                MessageBox.Show(excpt.Message);
            }
            _frame = new Mat();
            
        }
    
    private void ProcessFrame(object sender, EventArgs arg)
    {
        if (_capture != null && _capture.Ptr != IntPtr.Zero)
        {
            _capture.Retrieve(_frame, 0);

            
            captureImageBox.Image = _frame;
            
        }
    }

    private void btnProcess_Click(object sender, EventArgs e)
        {
            if (_capture != null)
            {
                if (_captureInProgress)
                {  //stop the capture
                    this.btnProcess.Text = "Start Capture";
                    _capture.Pause();
                }
                else
                {
                    //start the capture
                    btnProcess.Text = "Stop";
                    _capture.Start();
                }

                _captureInProgress = !_captureInProgress;
            }

        }

        private void btnCaptureFrame_Click(object sender, EventArgs e)
        {
            capturedFrame.Image = _frame;
        }
    }
}
