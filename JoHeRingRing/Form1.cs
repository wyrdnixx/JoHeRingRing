using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JoHeRingRing
{
    public partial class Form1 : Form
    {

        private Params _param;
        private Timer updateTimer;
        private Timer alertTimer;
        private Timer checkRingStatusTimer;
        private string checkRingStatusFile;
        //private string[] RemoteStationsList;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitProgramm();

            updateTimer = new Timer();
            updateTimer.Enabled = true;
            updateTimer.Interval = 5000;
            updateTimer.Tick += new EventHandler(UpdateTimerTick);

            alertTimer = new Timer();
            alertTimer.Interval = 500;
            alertTimer.Tick += new EventHandler(pictureBlink);


            checkRingStatusTimer = new Timer();
            checkRingStatusTimer.Interval = 5000;
            checkRingStatusTimer.Tick += new EventHandler(checkRingStatus);


            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);



        }


        void UpdateTimerTick(object sender, EventArgs e)
        {

            string StationFileResult = CheckStationFile(_param.StationFile);
            if (StationFileResult == null)
            {
                UpdateStationFile(_param.StationFile);
            } else if (StationFileResult == "Call Accepted") {
                /// TODO
            }
            else
            {
                Console.WriteLine("Inhalt gefunden: " + StationFileResult);
                alertMessage(StationFileResult);

                UpdateStationFile(_param.StationFile);
            }


            //RemoteStationsList = getStationFiles();
            createButtons(getStationFiles());

        }
        public void createButtons(string[] _StationFiles)
        {

            foreach (var button in this.Controls.OfType<Button>())
            {
                
                    this.Controls.Remove(button);
               
            }


        

            int top = 60;
            int left = 70;
            
            foreach (string File in _StationFiles)
            {

                if (File != _param.StationFile)
                {

                    Button newButton = new Button();
                    newButton.Text = Path.GetFileName(File);


                    newButton.Size = new Size(350, 50);
                    newButton.Left = left;
                    newButton.Top = top;
                    newButton.Click += (s, e) => { RingRemoteStation(File); };
                    this.Controls.Add(newButton);
                    top += newButton.Height + 2;
                }
            }
            
        }


        private void RingRemoteStation(string _station)
        {

            string[] x  = Path.GetFileName(_param.StationFile).Split('.');
            string message = x[0];

            
            Console.WriteLine("Button clicked: " + _station);

            try
            {
                using (StreamWriter outputFile = new StreamWriter(_station))
            {
                
                    outputFile.WriteLine(message);
                    lblAlertInfo.Text = "Ringing " + Path.GetFileName(_station).Split('.')[0];
                    lblAlertInfo.Visible = true;

                    checkRingStatusFile = _station;
                    checkRingStatusTimer.Start();


                }
            }catch (Exception e)
            {
               MessageBox.Show("Error ringing: " + e.Message);
                lblAlertInfo.Text = "Ringing failed";
            }

            
        }

        private void alertMessage(string _message)
        {
            Console.WriteLine("Ringing: " + _message);

           // Bring Form to front
            this.Activate();

            try
            {
                SoundPlayer simpleSound = new SoundPlayer(@"C:\Windows\media\Alarm07.wav");
                simpleSound.Play();
            } catch (Exception e)
            {
                Console.WriteLine("Error playing sound: " + e.Message);
            }

           

            alertTimer.Enabled = true;
            alertTimer.Start();

            
            lblAlertInfo.Text = "Alert: " + _message;
            lblAlertInfo.Visible = true;
        }

        private void pictureBlink(object sender, EventArgs e)
        {

            this.pbRinger.Visible = true;

            if (this.pbRinger.SizeMode == PictureBoxSizeMode.CenterImage )
            {
            //    Console.WriteLine("picture strech");
                this.pbRinger.SizeMode = PictureBoxSizeMode.StretchImage;
               
            }
            else
            {
              //  Console.WriteLine("picture center");
                this.pbRinger.SizeMode = PictureBoxSizeMode.CenterImage;
                
            }
        }

        private void checkRingStatus(object sender, EventArgs e)
        {

            long length = new System.IO.FileInfo(checkRingStatusFile).Length;
            if (length == 0)
            {
                lblAlertInfo.Text = "";
                checkRingStatusTimer.Stop();
            }

        }
        private string CheckStationFile(string _stationFile)
        {
            string Line = null;
         try
            {
                //Line =   File.ReadLines(_stationFile).First();


                Line = File.ReadAllText(_stationFile);
                //IEnumerable<string> lines = File.ReadLines(_stationFile);
                //Console.WriteLine(String.Join(Environment.NewLine, lines));
                //Line = lines.First();

            }
            catch (InvalidOperationException ioe)
            {
                //nothing
                Console.WriteLine("Error IOE CheckStationFile: " + ioe.Message);            
            }
            catch (Exception e)
            {
                Console.WriteLine("Error CheckStationFile: " + e.Message);            
            }

            if (Line == "") Line = null;
            return Line;
        }



        private void InitProgramm()
        {

            InitParameterDefinition();
            this.pbRinger.Visible = false;
            lblAlertInfo.Text = ""; // init without text visible 

        }

        private void InitParameterDefinition()
        {

            _param = new Params();

            _param.User = System.Environment.UserName;

            if (Environment.GetEnvironmentVariable("CLIENTNAME") != null)
            {
                _param.Computer = Environment.GetEnvironmentVariable("CLIENTNAME");
            }
            else
            {
                _param.Computer = Environment.GetEnvironmentVariable("COMPUTERNAME");
            }

           // lbl_username.Text = "Username: " + _param.User + " -> Computer: " + _param.Computer;


            _param.StationFile = AppDomain.CurrentDomain.BaseDirectory + _param.Computer + "_" + _param.User + ".txt";
            //lbl_username.Text = _param.StationFile;

            this.Text = "JoHeRingRing : " + Path.GetFileName(_param.StationFile).Split('.')[0];
        }

        private string[] getStationFiles()
        {

          

            string[] fileEntries = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory,"*.txt");

            // Only one (own) stationfile present...
            if (fileEntries.Length == 1)
            {
                lblAlertInfo.Text = "No one else here...";
            }
            else
            { 
                // if there is still the old message
                // (without this the textfield will get cleared every time
                if (lblAlertInfo.Text == "No one else here...")
                {
                    lblAlertInfo.Text = ""; 
                }


                // Clear old files
                foreach (string f in fileEntries)
                {
                    DateTime now =  DateTime.Now;
                    DateTime filetime = new System.IO.FileInfo(f).LastAccessTime;

                    if ( (now - filetime).TotalSeconds >= 30)
                    {
                        try
                        {
                            File.Delete(f);
                        }
                        catch (Exception) { }
                        
                    }

                }

            }
            return fileEntries;


            //var numbersList = fileEntries.ToList();

            //foreach (string f in fileEntries)
            //{
            //    if (f != _param.StationFile)
            //    {
            //        Console.WriteLine("Found file: " + f);
            //        numbersList.Add(f);
                 
            //    }
                
            //}
            //string[] StationFiles = numbersList.ToArray();
            //return StationFiles;

        }

        private bool UpdateStationFile(string _stationFile)
        {
       
                       
                try
                {
                    System.IO.File.Create(_stationFile).Close();
                }catch (Exception e1)
                {
                    Console.WriteLine("Error generating StationFile timestamp: " + e1.Message);
                    return false;
                }                
            
            return true;
        }

        private void pbRinger_Click(object sender, EventArgs e)
        {
            alertTimer.Stop();
            alertTimer.Enabled = false;
            pbRinger.Visible = false;
            lblAlertInfo.Text = ""; // Clear infotext
       
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            File.Delete(_param.StationFile);
        }



    }
}
