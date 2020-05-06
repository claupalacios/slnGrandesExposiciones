using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Security;

namespace slnGrandesExposiciones.Helpers
{
    public static class ProcessFile
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void EjecutarBat(string path)
        {try
            {

                //string path1 = @path;

                //Process process = new Process();

                //ProcessStartInfo processInfo = new ProcessStartInfo(path1);
                //processInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(path);

                //processInfo.CreateNoWindow = true;
                //processInfo.UseShellExecute = false;
                //processInfo.RedirectStandardError = true;
                //processInfo.RedirectStandardOutput = true;
                //log.Info("Ejecutando batch.." + @path);
                //log.Info("Nombre archivo: " + processInfo.FileName);
                //log.Info("Working directory: " + processInfo.WorkingDirectory);
                //process = Process.Start(processInfo);
                //log.Info("Ejecutando batch1.." + @path);
                //string output = process.StandardOutput.ReadToEnd();
                //string error = process.StandardError.ReadToEnd();
                //log.Info("Ejecutando batch2.." + @path);
                //process.WaitForExit();
                //if (process.ExitCode == 0)
                //{
                //    // success
                //    log.Debug("Se ejecuto correctamente el batch " + path);
                //    if (output.Trim() != string.Empty)
                //    {
                //        log.Debug("Salida: " + output);
                //    }
                //}
                //else
                //{
                //    log.Error("Error al ejecutar batch " + @path + " Exit Code: " + process.ExitCode.ToString());
                //    if (!String.IsNullOrEmpty(error.Trim()))
                //    {
                //        log.Error("Error: " + error);

                //    }
                //    if (output.Trim() != string.Empty)
                //    {
                //        log.Error("Salida: " + output);
                //    }
                //}

                //var processInfo = new ProcessStartInfo(@path);
                //processInfo.CreateNoWindow = true;
                //processInfo.UseShellExecute = false;
                //processInfo.RedirectStandardError = true;
                //processInfo.RedirectStandardOutput = true;

                //var process = Process.Start(processInfo);

                //process.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
                //    log.Info("output>>" + e.Data);
                //process.BeginOutputReadLine();

                //process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
                //    log.Error("error>>" + e.Data);
                //process.BeginErrorReadLine();

                //process.WaitForExit();

                //log.Info("ExitCode: " + process.ExitCode.ToString());
                //process.Close();

                

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/C md pepe";
                startInfo.UseShellExecute = true;
                startInfo.WindowStyle = ProcessWindowStyle.Normal;
                
                process.StartInfo = startInfo;
                
                process.Start();
                process.WaitForExit();
                log.Info("ExitCode: " + process.ExitCode.ToString());
                process.Close();
            }
            catch (Exception ex)
            {
                log.Error("Error al procesar bat", ex);

            }
        }
    }
}