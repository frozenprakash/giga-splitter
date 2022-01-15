using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

//Form Created by Chandraprakash [2017-06-19]
namespace GigaSplitter
{
    public partial class frmCountandSplit : Form
    {
        StreamReader sr;
        StreamWriter sw;

        public frmCountandSplit()
        {
            InitializeComponent();
        }

        //Created by Chandraprakash [2017-07-19 | 10:11 PM]
        enum ExecutionType : byte
        {
            pbLineCount,
            pbFileCount
        }

        async private void CountandSplit_Load(object sender, EventArgs e)
        {
            ss1Pb1.Width = ss1.Width - (ss1Lbl1.Width + ss1Lbl2.Width);

            lbl_TotalClaimCountValue.Text = "";
            txt_SplitClaimCount.Text = "100,000";
            ss2Lbl1.Text = "";
        }

        private void btn_SplitClaims_Click(object sender, EventArgs e)
        {
            FN_SplitClaims();
        }

        //Created by Chandraprakash
        //Modified by Chandraprakash [2017-07-18 | 10:38 PM]
        async void FN_SplitClaims()
        {
            string out_DestinationFolder = "";
            try
            {
                //Validations
                if (SplitClaimsValidation() == false)
                {
                    return;
                }
                ButtonsStatusDuringProcess(false);

                string strSourceFile,
                        strDestinationFolder;

                strSourceFile = txtSourceFile.Text.Trim();
                strDestinationFolder = CreateDestinationDirectory(strSourceFile);
                out_DestinationFolder = strDestinationFolder; //To pass string to catch block, to delete this folder if the form is closed during splitting half way

                int TotalClaimCount = 0,
                    SplitClaimCount = 0,
                    TotalFileCount = 0;

                //Already confirmed above validation that, Both TotalClaimCount and SplitClimCount number entered by user is a Integer Number
                TotalClaimCount = Convert.ToInt32(lbl_TotalClaimCountValue.Text.Replace(",", ""));
                SplitClaimCount = Convert.ToInt32(txt_SplitClaimCount.Text.Replace(",", ""));

                //Already confirmed above validation that, SplitClaimCount is less than TotalClaimCount [Not greater and not equal]
                //Decimals will be omitted during implicit integer conversion
                if ((TotalClaimCount % SplitClaimCount) == 0)
                {
                    //Example: TotalClaim = 6, 
                    //SplitClaimCount = 2, 
                    //Total/Split = 6/2 = 3
                    //3 + 0 = Split to 3 files
                    TotalFileCount = (TotalClaimCount / SplitClaimCount);
                }

                else if ((TotalClaimCount % SplitClaimCount) != 0)
                {
                    //Example: TotalClaim = 5, 
                    //SplitClaimCount = 2, 
                    //Total/Split = 5/2 = 2
                    //2 + 1 = Split to 3 files
                    TotalFileCount = (TotalClaimCount / SplitClaimCount) + 1;
                }

                Transfer.Splitter ObjSplitter = new Transfer.Splitter();
                bool isSplit = await Task.Run(() => ObjSplitter.Split(strSourceFile,
                                                                        strDestinationFolder,
                                                                        SplitClaimCount,

                                                                        (byte)ExecutionType.pbLineCount,

                                                                        TotalFileCount,
                                                                        TotalClaimCount,

                                                                        ss1,
                                                                        ss2,

                                                                        ss1Pb1,
                                                                        ss2Lbl1)
                                            );

                if (isSplit)
                {
                    ss2Lbl1.Text = "Splitting Completed Successfully";
                    MessageBox.Show("Splitting Completed Successfully !");
                }

                txt_SplitClaimCount.Clear();
                txt_SplitClaimCount.Focus();
            }
            //Created by Chandraprakash [2017-06-21]
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ButtonsStatusDuringProcess(true);
                txt_SplitClaimCount.Focus();
                txt_SplitClaimCount.SelectAll();
            }
        }

        bool SplitClaimsValidation()
        {
            if (txtSourceFile.Text.Trim() == "")
            {
                ss2Lbl1.Text = "";
                MessageBox.Show("Please select a source file !");
                return false;
            }

            if (txtDestinationFolder.Text.Trim() == "")
            {
                ss2Lbl1.Text = "";
                MessageBox.Show("Please enter the destination folder !");
                return false;
            }

            if (lbl_TotalClaimCountValue.Text.Trim() == "")
            {
                ss2Lbl1.Text = "";
                MessageBox.Show("Please select a source File, and split only after the total Line count is populated !");
                return false;
            }

            if (Convert.ToInt32(lbl_TotalClaimCountValue.Text.Trim().Replace(",", "")) == 0)
            {
                ss2Lbl1.Text = "";
                MessageBox.Show("The source file is empty and has no Lines !");

                txtSourceFile.Clear();
                txtDestinationFolder.Clear();

                return false;
            }

            if (txt_SplitClaimCount.Text.Trim() == "")
            {
                ss2Lbl1.Text = "";
                MessageBox.Show("Split Line count cannot be empty, please enter a value for split line count and proceed again !");
                txt_SplitClaimCount.Focus();
                return false;
            }

            int i_temp = 0;
            if (int.TryParse(txt_SplitClaimCount.Text.Trim().Replace(",", ""), out i_temp) == false)
            {
                ss2Lbl1.Text = "";
                MessageBox.Show("Enter only numbers in split line count Text Box !");
                txt_SplitClaimCount.Focus();
                return false;
            }

            if (Convert.ToInt32(txt_SplitClaimCount.Text.Replace(",", "")) == 0)
            {
                MessageBox.Show("Split line count cannot be zero !");
                txt_SplitClaimCount.Focus();
                return false;
            }

            if (Convert.ToInt32(txt_SplitClaimCount.Text.Replace(",", "")) < 0)
            {
                MessageBox.Show("Split line count cannot be a negative value !");
                txt_SplitClaimCount.Text = Math.Abs(Convert.ToInt32(txt_SplitClaimCount.Text.Replace(",", ""))).ToString();
                txt_SplitClaimCount.Focus();
                return false;
            }

            if (Convert.ToInt32(txt_SplitClaimCount.Text.Replace(",", "")) >= Convert.ToInt32(lbl_TotalClaimCountValue.Text.Replace(",", "")))
            {
                ss2Lbl1.Text = "";
                MessageBox.Show("Split line count should be less than the total line count !");
                txt_SplitClaimCount.Focus();
                return false;
            }

            return true;
        }

        //Count Claims
        private void btnSourceFile_Click(object sender, EventArgs e)
        {
            FN_SourceFile();
        }

        //Added by Chandraprakash [2017-06-21]
        //Modified by Chandraprkash [2017-06-21]
        //Select Source File and Count Claims
        async void FN_SourceFile()
        {
            try
            {
                lbl_TotalClaimCountValue.Text = "";
                ss2Lbl1.Text = "";
                bool isPassed = false;

                if (ofdSourceFile.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(ofdSourceFile.FileName))
                    {
                        txtSourceFile.Text = ofdSourceFile.FileName;
                        ButtonsStatusDuringProcess(false);
                    }
                    else
                    {
                        MessageBox.Show("Selected file cannot be found !");
                        return;
                    }

                    ss2Lbl1.Text = "Total Claim Count Calculation in Progress...";
                    isPassed = await Task.Run(() => CountClaims(txtSourceFile.Text.Trim(),
                                                                ss1,
                                                                ss2,
                                                                ss1Pb1,
                                                                ss2Lbl1
                                                                )
                                            );
                    if (isPassed == false)
                    {
                        return;
                    }

                    txtDestinationFolder.Text = Path.GetDirectoryName(ofdSourceFile.FileName);
                    ss2Lbl1.Text = "Completed Calculating Total Claim Count";
                }
                else
                {
                    // If user pressed cancel in Open File Dialog do nothing and exit
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ButtonsStatusDuringProcess(true);
                txt_SplitClaimCount.Focus();
                txt_SplitClaimCount.SelectAll();
            }
        }

        //Created by Chandraprakash [2017-06-07]
        //Modified by Chandraprakash [2017-06-20]
        bool CountClaims(string strFileName,

                            StatusStrip ss1,
                            StatusStrip ss2,
                            ToolStripProgressBar ss1Pb1,
                            ToolStripLabel ss2Lbl1)
        {
            bool isPassed = false;

            try
            {
                //Validations
                if (txtSourceFile.Text.Trim() == "")
                {
                    MessageBox.Show("Please Select a Source File !");
                    return false;
                }

                Transfer.Splitter objSplitter = new Transfer.Splitter();
                int i_count = objSplitter.TotalLines(txtSourceFile.Text.Trim(),
                                                        //Optional Parameters
                                                        ss1,
                                                        ss2,
                                                        ss1Pb1,
                                                        ss2Lbl1);

                if (i_count == -1)
                {
                    MessageBox.Show("Error while reading the Source File !!");
                    return false;
                }
                else if (i_count != -1)
                {
                    lbl_TotalClaimCount.Invoke(new Action(() =>
                        lbl_TotalClaimCountValue.Text = i_count.ToString("N0", CultureInfo.InvariantCulture)
                    ));

                    if (i_count <= 1000)
                    {
                        Invoke(new Action(() =>
                            txt_SplitClaimCount.Text = ""
                        ));
                    }
                    else if (i_count <= 100000)
                    {
                        Invoke(new Action(() =>
                            txt_SplitClaimCount.Text = "1,000"
                        ));
                    }
                    else if (i_count <= 1000000)
                    {
                        Invoke(new Action(() =>
                            txt_SplitClaimCount.Text = "100,000"
                        ));
                    }
                    else if (i_count > 1000000)
                    {
                        Invoke(new Action(() =>
                            txt_SplitClaimCount.Text = "1,000,000"
                        ));
                    }
                }
                isPassed = true;
            }
            catch (InvalidOperationException ex)
            {
                //While Counting Claims, 
                //If the Form is closed while the BeginInvoke or Invoke are running,
                //execution will come here and exit without throwing any error.
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return isPassed;
        }

        public string CreateDestinationDirectory(string fullFileName)
        {
            DateTime dt = DateTime.Now;
            string str_dt = "[" +
                            dt.ToString("yyyy") + "-" +
                            dt.ToString("MM") + "-" +
                            dt.ToString("dd") + "] [" +
                            dt.ToString("hh") + "h " +
                            dt.ToString("mm") + "m " +
                            dt.ToString("ss") + "s " +
                            dt.ToString("tt") +
                            "]";

            string dir = Path.Combine(Path.GetDirectoryName(fullFileName),
                                        Path.GetFileNameWithoutExtension(fullFileName) + " " +
                                        str_dt
                                    );

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            return dir;
        }

        void ButtonsStatusDuringProcess(bool b)
        {
            btnSourceFile.Enabled = b;
            btn_SplitClaims.Enabled = b;

            txtSourceFile.Enabled = b;
            txtDestinationFolder.Enabled = b;
            txt_SplitClaimCount.Enabled = b;
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Application is developed by Chandraprakash." + "\n\n" +
                                "Email" + "\t" + ": info@frozenincorporation.com" + "\n" +
                                "Mobile" + "\t" + ": +91 9444 7770 37" + "\n"
                            );
        }

        private void mnuApplication_Click(object sender, EventArgs e)
        {
            MessageBox.Show("\"GigaSplitter\" is used to count and split the file.");
        }

    }

    //Added by Chandraprakash [2017-07-14 | 05:16 PM]
    //This class is used to change the color of the progress bar
    public static class ModifyProgressBarColor
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);
        public static void SetState(this ProgressBar pBar, int state)
        {
            SendMessage(pBar.Handle, 1040, (IntPtr)state, IntPtr.Zero);
        }
    }
}