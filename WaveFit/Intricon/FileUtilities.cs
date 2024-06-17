using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace Audion8TestApp
{
    internal class FileUtilities
    {
        public IntriconPlotObject openCurveFileLegacy(string filenameAndPath)
        {
            IntriconPlotObject plot = new IntriconPlotObject();
            int i = 0;

            try
            {
                System.IO.StreamReader reader = new System.IO.StreamReader(filenameAndPath);
                string currentRow;
                if (reader.EndOfStream == false)
                {
                    currentRow = reader.ReadLine();
                    plot.setPlotType(currentRow);
                }
                while (reader.EndOfStream == false)
                {
                    try
                    {
                        currentRow = reader.ReadLine();
                        plot.setResponsePoint(i, Convert.ToSingle(currentRow, CultureInfo.InvariantCulture));
                        i++;
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Line " + err.Message + "is not valid and will be skipped.");
                    }
                }
                reader.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            return plot;
        }

        public short saveCurveFile(IntriconPlotObject plot, string filenameAndPath, bool doLegacy = false)
        {
            try
            {
                if (File.Exists(filenameAndPath))
                {
                    File.Delete(filenameAndPath);
                }

                if (doLegacy)
                {
                    if (plot.getResponseLength() != 64)
                        return -1;
                    using (StreamWriter file = File.AppendText(filenameAndPath))
                    {
                        file.WriteLine("\"\"");
                        for (int i = 0; i <= plot.getResponseLength(); i++)
                        {
                            file.WriteLine(plot.getResponsePoint(i));
                        }

                        file.Close();
                    }
                }
                else
                {
                    using (StreamWriter file = File.AppendText(filenameAndPath))
                    {
                        file.WriteLine("Plot File Format Version = 1");
                        file.WriteLine("Plot Type = " + plot.getPlotType());
                        file.WriteLine("Plot Date = " + plot.getPlotDate());

                        for (int i = 0; i <= plot.getResponseLength(); i++)
                        {
                            file.WriteLine(plot.getFrequencyPoint(i) + " Hz = " + plot.getResponsePoint(i));
                        }

                        file.Close();
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            return 0;
        }
    }
}