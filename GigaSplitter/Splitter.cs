using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Transfer
{
    class Splitter
    {
        #region Members

        private StreamReader sr;
        private StreamWriter sw;
        private ArrayList alFileNames;

        #endregion

        #region Constructors

        public Splitter()
        {
            alFileNames = new ArrayList();
        }

        #endregion

        #region Methods

        //Created by Chandraprakash [2017-07-19 | 10:35 PM]
        enum ExecutionType : byte
        {
            pbLineCount,
            pbFileCount
        }

        //Created by Chandraprakash [2017-07-18 | 11:51 PM]
        //New method with advanced optional parameters for live UI Updates while splitting
        public bool Split(string strFilePath,
                            string strDestFolder,
                            int SplitClaimCount,

                            byte executionType = 0,
                            // This value will be assigned by enum named [ExecutionType]
                            //  0 - Show File Count and Line Count  [ProgressBar = LineCount]
                            //  1 - Show File Count only            [ProgressBar = FileCount]

                            int TotalFileCount = -1,
                            int TotalClaimCount = -1,

                            StatusStrip ss1 = null,
                            StatusStrip ss2 = null,

                            ToolStripProgressBar ss1Pb1 = null,
                            ToolStripLabel ss2Lbl1 = null
                        )
        {
            bool isSplit = false;

            try
            {
                FileInfo info = new FileInfo(strFilePath);
                sr = File.OpenText(strFilePath);

                string dir = strDestFolder;
                string FileName = Path.GetFileNameWithoutExtension(strFilePath);
                string extension = Path.GetExtension(strFilePath);

                string lineRead;
                int fileCount = 1;
                string postfix;

                #region ProgressBar Initialization
                if (ss1Pb1 != null)
                {
                    ss1.BeginInvoke(new Action(() =>
                    {
                        if (executionType == (byte)ExecutionType.pbLineCount)
                        {
                            ss1Pb1.Minimum = 0;
                            ss1Pb1.Maximum = SplitClaimCount;
                        }
                        else if (executionType == (byte)ExecutionType.pbFileCount)
                        {
                            ss1Pb1.Minimum = 0;
                            ss1Pb1.Maximum = TotalFileCount;
                        }
                    }));
                }
                #endregion

                DateTime dtPrevious = DateTime.Now;

                do
                {
                    if (sr.Peek() == -1)
                    {
                        break;
                    }

                    postfix = ('_' + fileCount.ToString());
                    string newPath = Path.Combine(dir, FileName + postfix + extension);

                    sw = File.CreateText(newPath);
                    System.Diagnostics.Debug.WriteLine(newPath);

                    for (int i = 0; i < SplitClaimCount; i++)
                    {
                        if (sr.Peek() == -1)
                        {
                            #region Final UI Update at 100% for n'th FileCount
                            if (ss2Lbl1 != null)
                            {
                                ss2.Invoke(new Action(() =>
                                {
                                    if (executionType == (byte)ExecutionType.pbLineCount)
                                    {
                                        ss2Lbl1.Text = "Splitting file: " + fileCount.ToString() + " / " + TotalFileCount.ToString() +
                                                        "     Line Count: " + (i + 1).ToString("N0", CultureInfo.InvariantCulture) + " / " + SplitClaimCount.ToString("N0", CultureInfo.InvariantCulture);
                                    }
                                    else if (executionType == (byte)ExecutionType.pbFileCount)
                                    {
                                        ss2Lbl1.Text = "Splitting file: " + fileCount.ToString() + " / " + TotalFileCount.ToString();
                                    }
                                }));
                            }
                            if (ss1Pb1 != null)
                            {
                                ss1.Invoke(new Action(() =>
                                {
                                    if (executionType == (byte)ExecutionType.pbLineCount)
                                    {
                                        ss1Pb1.Value = SplitClaimCount;
                                    }
                                    else if (executionType == (byte)ExecutionType.pbFileCount)
                                    {
                                        ss1Pb1.Value = fileCount;
                                    }
                                }
                                ));
                            }
                            #endregion

                            break;
                        }
                        lineRead = sr.ReadLine();
                        sw.WriteLine(lineRead);

                        if (ss2Lbl1 != null) //Will skip the DateTime calculation alltogether if this is null
                        {
                            #region UI General Update from 1 to 99%
                            if ((DateTime.Now - dtPrevious).TotalMilliseconds > 50)
                            {
                                ss2.BeginInvoke(new Action(() =>
                                {
                                    if (executionType == (byte)ExecutionType.pbLineCount)
                                    {
                                        ss2Lbl1.Text = $"Splitting file: {fileCount} / {TotalFileCount}" +
                                                        $"     Line Count: {(i + 1).ToString("N0", CultureInfo.InvariantCulture)} / {SplitClaimCount.ToString("N0", CultureInfo.InvariantCulture)}";
                                    }
                                    else if (executionType == (byte)ExecutionType.pbFileCount)
                                    {
                                        ss2Lbl1.Text = $"Splitting file: {fileCount} / {TotalFileCount}";
                                    }
                                }));

                                if (ss1Pb1 != null)
                                {
                                    ss1.BeginInvoke(new Action(() =>
                                    {
                                        if (executionType == (byte)ExecutionType.pbLineCount)
                                        {
                                            ss1Pb1.Value = i + 1;
                                        }
                                        else if (executionType == (byte)ExecutionType.pbFileCount)
                                        {
                                            ss1Pb1.Value = fileCount;
                                        }
                                    }));
                                }

                                dtPrevious = DateTime.Now;
                            }
                            #endregion

                            #region UI UPdate at 100% for FileCount 1 to (n-1)
                            //Will work for FileCount 1 to (n-1),
                            //as nth end loop wont always end at SplitClaimCount but before that itself mostly, and it'll be handled at the EOF above
                            if (i == SplitClaimCount - 1)
                            {
                                ss2.Invoke(new Action(() =>
                                {
                                    if (executionType == (byte)ExecutionType.pbLineCount)
                                    {
                                        ss2Lbl1.Text = "Splitting file: " + fileCount.ToString() + " / " + TotalFileCount.ToString() +
                                                    "     Line Count: " + (i + 1).ToString("N0", CultureInfo.InvariantCulture) + " / " + SplitClaimCount.ToString("N0", CultureInfo.InvariantCulture);
                                    }
                                    else if (executionType == (byte)ExecutionType.pbFileCount)
                                    {
                                        ss2Lbl1.Text = "Splitting file: " + fileCount.ToString() + " / " + TotalFileCount.ToString();
                                    }
                                }));

                                if (ss1Pb1 != null)
                                {
                                    ss1.Invoke(new Action(() =>
                                    {
                                        if (executionType == (byte)ExecutionType.pbLineCount)
                                        {
                                            ss1Pb1.Value = i + 1;
                                        }
                                        else if (executionType == (byte)ExecutionType.pbLineCount)
                                        {
                                            ss1Pb1.Value = fileCount;
                                        }
                                    }));
                                }
                            }
                            #endregion
                        }


                    }
                    sw.Close();
                    fileCount++;

                } while (true);

                sr.Close();
                sr.Dispose();

                isSplit = true;
            }
            catch (InvalidOperationException ex)
            {
                try
                {
                    sr.Close();
                    sw.Close();

                    sr.Dispose();
                    sw.Dispose();

                    if (Directory.Exists(strDestFolder))
                    {
                        Directory.Delete(strDestFolder, true);
                    }
                }
                catch (Exception ex1)
                {
                    MessageBox.Show(ex1.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (ss1 != null)
                {
                    ss1.Invoke(new Action(() =>
                        ss1Pb1.Value = 0
                    ));
                }

                if (ss2 != null)
                {
                    ss2.Invoke(new Action(() =>
                        ss2Lbl1.Text = ""
                    ));
                }
            }

            return isSplit;
        }


        //Commented by Chandraprakash [2017-07-18 | 11:49 PM]
        //public int Split(string fullPath)
        //{
        //	return Split(fullPath, 1000);
        //}

        //Changed From Split to Split_Old by Chandraprakash [2017-07-18 | 11:48 PM]
        public int Split_Old(string str_inpFilepath,
                            string str_destFolder,
                            int lineCount)
        {
            if (!File.Exists(str_inpFilepath))
            {
                return -1;
            }
            alFileNames.Clear();

            FileInfo info = new FileInfo(str_inpFilepath);
            sr = File.OpenText(str_inpFilepath);

            // separate the path, filename, and extension
            //string dir = Path.GetDirectoryName(str_inpFilepath);
            string dir = str_destFolder;
            string FileName = Path.GetFileNameWithoutExtension(str_inpFilepath);
            string extension = Path.GetExtension(str_inpFilepath);

            string lineRead;
            string postfix;

            //Modified by Chandraprakash [2017-06-30]
            //Number will start from 1 rather than 0 for easier understanding to end users
            //Also updated in CleanupTempFiles() in ImportFileReader.cs
            int fileCount = 1;

            do
            {
                if (sr.Peek() == -1)
                {
                    break;
                }

                /* Note that if we change the way we name the split files we must
                 * also change the regular expression in the CleanupTempFiles method
                 * in ImportFileReader.cs so that it deletes the temp files if an
                 * error is encoutered.  Temp files are normally deleted once they
                 * have been succesfully merged into the DB 
                 */

                postfix = ('_' + fileCount.ToString());
                string newPath = Path.Combine(dir, FileName + postfix + extension);

                sw = File.CreateText(newPath);
                alFileNames.Add(newPath);
                System.Diagnostics.Debug.WriteLine(newPath);

                for (int i = 0; i < lineCount; i++)
                {
                    if (sr.Peek() == -1)
                    {
                        //wtr.Close();
                        break;
                    }

                    lineRead = sr.ReadLine();
                    sw.WriteLine(lineRead);
                }

                sw.Close();
                fileCount++;
            } while (true);

            sr.Close();
            sr.Dispose();
            return fileCount - 1;
        }

        //Added by Chandraprakash [2017-07-10 | 12:32 PM]
        //Created to update the splitting file number on status bar label of [frmStatusDisplay.cs]
        public int SplitAndUpdate(string str_inpFilepath,
                                    string str_destFolder,
                                    int lineCount)
        {
            if (!File.Exists(str_inpFilepath))
            {
                return -1;
            }
            alFileNames.Clear();

            FileInfo info = new FileInfo(str_inpFilepath);
            sr = File.OpenText(str_inpFilepath);

            // separate the path, filename, and extension
            //string dir = Path.GetDirectoryName(str_inpFilepath);
            string dir = str_destFolder;
            string FileName = Path.GetFileNameWithoutExtension(str_inpFilepath);
            string extension = Path.GetExtension(str_inpFilepath);

            string lineRead;
            string postfix;

            //Modified by Chandraprakash [2017-06-30]
            //Number will start from 1 rather than 0 for easier understanding to end users
            //Also updated in CleanupTempFiles() in ImportFileReader.cs
            int fileCount = 1;

            do
            {
                if (sr.Peek() == -1)
                {
                    break;
                }

                /* Note that if we change the way we name the split files we must
                 * also change the regular expression in the CleanupTempFiles method
                 * in ImportFileReader.cs so that it deletes the temp files if an
                 * error is encoutered.  Temp files are normally deleted once they
                 * have been succesfully merged into the DB 
                 */

                postfix = ('_' + fileCount.ToString());
                string newPath = Path.Combine(dir, FileName + postfix + extension);

                sw = File.CreateText(newPath);
                alFileNames.Add(newPath);
                System.Diagnostics.Debug.WriteLine(newPath);

                for (int i = 0; i < lineCount; i++)
                {
                    if (sr.Peek() == -1)
                    {
                        //wtr.Close();
                        break;
                    }

                    lineRead = sr.ReadLine();
                    sw.WriteLine(lineRead);
                }

                sw.Close();
                fileCount++;
            } while (true);

            sr.Close();
            sr.Dispose();
            return fileCount - 1;
        }

        //New1 logic Created by Chandraprakash [2017-05-17]
        //R&D - Type 1
        public int Split_new1(string fullPath, int lineCount)
        {
            if (!File.Exists(fullPath))
            {
                return -1;
            }
            alFileNames.Clear();

            string dir = Path.GetDirectoryName(fullPath);
            string FileName = Path.GetFileNameWithoutExtension(fullPath);
            string extension = Path.GetExtension(fullPath);

            string Loop_FileName;
            string postfix;

            int fileCount = 0;

            int totalLines = 0;
            int range = lineCount;
            int start = 0;
            totalLines = TotalLines(fullPath);

            if (totalLines == 0)
            {
                return -1; //Debug: Need to change to -2 after analysis
            }

            while (start < totalLines)
            {
                postfix = ('_' + fileCount.ToString());
                fileCount++;
                Loop_FileName = Path.Combine(dir, FileName + postfix + extension);

                alFileNames.Add(Loop_FileName);
                System.Diagnostics.Debug.WriteLine(Loop_FileName);


                if ((totalLines - start) >= range)
                {
                    ArraytoFile(File.ReadLines(fullPath).Skip(start).Take(range).ToArray(),
                                Loop_FileName);
                    start += range;
                }
                else if (totalLines <= range)
                {
                    ArraytoFile(File.ReadLines(fullPath).Skip(start).Take(totalLines).ToArray(),
                                Loop_FileName);
                    start += totalLines;
                }
                else if ((totalLines - start) < range)
                {
                    ArraytoFile(File.ReadLines(fullPath).Skip(start).Take(totalLines - start).ToArray(),
                                Loop_FileName);
                    start += (totalLines - start);
                }
            }
            return fileCount;
        }

        //New2 logic Created by Chandraprakash [2017-05-19]
        //R&D - Type 2
        public int Split_testing(string fullPath, int lineCount)
        {
            if (!File.Exists(fullPath))
            {
                return -1;
            }
            alFileNames.Clear();

            string dir = Path.GetDirectoryName(fullPath);
            string FileName = Path.GetFileNameWithoutExtension(fullPath);
            string extension = Path.GetExtension(fullPath);

            string Loop_FileName;
            string[] str_content_array;
            string[] str_temp_array;
            string postfix;

            int fileCount = 0;

            int totalLines = 0;
            int range = lineCount;
            int start = 0;
            int cntArray = 0;
            totalLines = TotalLines(fullPath);

            if (totalLines == 0)
            {
                return -1; //Debug: Need to change to -2 after analysis
            }

            str_content_array = File.ReadAllLines(fullPath);

            while (start < totalLines)
            {
                postfix = ('_' + fileCount.ToString());
                fileCount++;
                Loop_FileName = Path.Combine(dir, FileName + postfix + extension);

                alFileNames.Add(Loop_FileName);
                System.Diagnostics.Debug.WriteLine(Loop_FileName);

                if ((totalLines - start) >= range)
                {
                    //ArraytoFile(File.ReadLines(fullPath).Skip(start).Take(range).ToArray(),
                    //            Loop_FileName);

                    str_temp_array = new string[range];
                    cntArray = 0;
                    for (int i = start; i < start + range; i++)
                    {
                        str_temp_array[cntArray] = str_content_array[i];
                        cntArray++;
                    }
                    ArraytoFile(str_temp_array, Loop_FileName);

                    start += range;
                }
                else if (totalLines <= range)
                {
                    //ArraytoFile(File.ReadLines(fullPath).Skip(start).Take(totalLines).ToArray(),
                    //            Loop_FileName);

                    str_temp_array = new string[totalLines - start];
                    cntArray = 0;
                    for (int i = start; i < totalLines; i++)
                    {
                        str_temp_array[cntArray] = str_content_array[i];
                        cntArray++;
                    }
                    ArraytoFile(str_temp_array, Loop_FileName);

                    start += totalLines;
                }
                else if ((totalLines - start) < range)
                {
                    //ArraytoFile(File.ReadLines(fullPath).Skip(start).Take(totalLines - start).ToArray(),
                    //            Loop_FileName);

                    str_temp_array = new string[(totalLines - start) - start];
                    cntArray = 0;
                    for (int i = start; i < (totalLines - start); i++)
                    {
                        str_temp_array[cntArray] = str_content_array[i];
                        cntArray++;
                    }
                    ArraytoFile(str_temp_array, Loop_FileName);

                    start += (totalLines - start);
                }
            }
            return fileCount;
        }

        public void ArraytoFile(string[] str_array, string str_FileName)
        {
            File.WriteAllLines(str_FileName, str_array);
        }

        //Created by Chandraprakash [2017-05-19]
        //Modified by Chandraprakash [2017-07-17 | 03:43 PM]
        public int TotalLines(string strFileName,

                                //Optional Parameters
                                StatusStrip ss1 = null,
                                StatusStrip ss2 = null,
                                ToolStripProgressBar ss1Pb1 = null,
                                ToolStripLabel ss2Lbl1 = null
                                )
        {
            int intLineCount = 0;
            try
            {
                bool isCounted = false;

                if (ss1Pb1 != null)
                {
                    ss1.Invoke(new Action(() =>
                    {
                        ss1Pb1.Style = ProgressBarStyle.Marquee;
                        ss1Pb1.MarqueeAnimationSpeed = 10;
                    }
                    ));
                }

                if (!File.Exists(strFileName))
                {
                    return -1;
                }

                sr = File.OpenText(strFileName);
                intLineCount = 0;

                DateTime dtPrevious = DateTime.Now;
                while (sr.Peek() != -1)
                {
                    sr.ReadLine();
                    intLineCount++;

                    if (ss2Lbl1 != null)
                    {
                        if ((DateTime.Now - dtPrevious).TotalMilliseconds > 50)
                        {
                            ss2.BeginInvoke(new Action(() =>
                                ss2Lbl1.Text = "Counting Claims: " + intLineCount.ToString("N0", CultureInfo.InvariantCulture)
                            ));

                            dtPrevious = DateTime.Now;
                        }
                    }
                }
                isCounted = true;

                sr.Close();
                sr.Dispose();

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
            finally
            {
                if (ss1Pb1 != null)
                {
                    ss1.Invoke(new Action(() =>
                    {
                        ss1Pb1.Style = ProgressBarStyle.Blocks;
                        ss1Pb1.Value = 0;
                    }));
                }
                if (ss2Lbl1 != null)
                {
                    ss2.Invoke(new Action(() =>
                        ss2Lbl1.Text = ""
                    ));
                }
            }

            return intLineCount;
        }

        #region commented
        //			public int split(string full_path, long lineCount)
        //			{
        //
        //			
        //				if (!File.Exists(full_path))
        //					return -1;
        //				FileInfo info = new FileInfo(full_path);
        //				rdr = File.OpenText(full_path);
        //			
        //				// separate the path, filename, and extension
        //				string dir = info.Directory.ToString();
        //				string name = info.Name;
        //			
        //				string[] nameArr  = name.Split('.');
        //				string extension = nameArr[nameArr.Length - 1];
        //			
        //				int lastDotIndex = name.LastIndexOf('.');
        //				string shortfilename = name.Remove(lastDotIndex, name.Length - lastDotIndex);
        //		
        //
        //				string lineRead;
        //				int fileCount = 0 ;
        //				string postfix;
        //
        //				do
        //				{
        //					if (rdr.Peek() == -1)
        //						break;
        //				
        //					postfix = (fileCount.ToString());
        //					wtr = File.CreateText(dir + '\\' + shortfilename + postfix + '.' + extension);
        //					System.Diagnostics.Debug.WriteLine(dir + shortfilename + postfix + '.' + extension);
        //					for (int i = 0; i < lineCount; i++)
        //					{
        //						if (rdr.Peek() == -1)
        //						{
        //							wtr.Close();
        //							break;
        //						}
        //						lineRead = rdr.ReadLine();
        //						wtr.WriteLine(lineRead);
        //					}
        //					wtr.Close();
        //					fileCount ++;
        //				} while(true);
        //
        //				rdr.Close();
        //				return fileCount;
        //			}
        #endregion

        #endregion

        #region Accessors

        public Array Filenames
        {
            get
            {
                Array arrNames = alFileNames.ToArray(Type.GetType("System.String"));
                return arrNames;
            }
        }

        #endregion

    }
}