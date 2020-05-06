using System;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.IO.Compression;



namespace slnGrandesExposiciones.Helpers
{


    
    public class LogonUsr{

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const int LOGON32_PROVIDER_DEFAULT = 0;
        private const int LOGON32_LOGON_INTERACTIVE = 2;

        private string domain = "";
        private string user = "";
        private string password = "";
        private IntPtr userHandle = IntPtr.Zero;
        WindowsImpersonationContext wic = null;


        // obtains user token     pu
        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool LogonUser(string pszUsername, string pszDomain, string pszPassword,
            int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

        // closes open handes returned by LogonUser
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private extern static bool CloseHandle(IntPtr handle);
        

        public LogonUsr(){
            
          setContext(ConnectionHelper.GetUserName(),ConnectionHelper.GetUserPassword(),"BGCMZ"); 

        }

        private WindowsImpersonationContext setUser(){
            WindowsImpersonationContext impersonationContext = null;

            try{

                log.Debug(System.Web.Configuration.WebConfigurationManager.AppSettings["LogWeb"] + " windows identify before impersonation: " + WindowsIdentity.GetCurrent().Name);
                
                // Invocar LogonUser para obtener el token del usuario
                bool loggedOn = LogonUser(user, domain, password, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, ref userHandle);

                log.Debug(System.Web.Configuration.WebConfigurationManager.AppSettings["LogWeb"] + " loggedOn: " + loggedOn + "; Token: " + user);
                

                if (!loggedOn)
                {
                    log.Error(System.Web.Configuration.WebConfigurationManager.AppSettings["LogWeb"] + " Exception impersonating user, error code: " + Marshal.GetLastWin32Error());
                    return null;
                }

                // Impersonar
                impersonationContext = WindowsIdentity.Impersonate(userHandle);
                impersonationContext = WindowsIdentity.Impersonate(userHandle);
                //impersonationContext = WindowsIdentity.Impersonate(WindowsIdentity.GetCurrent().Token);

                log.Info(System.Web.Configuration.WebConfigurationManager.AppSettings["LogWeb"] + " Main() windows identify after impersonation: " + WindowsIdentity.GetCurrent().Name);
               
            }
            catch (Exception ex){

                log.Error(System.Web.Configuration.WebConfigurationManager.AppSettings["LogWeb"] + " Exception impersonating user: " + ex.Message);
                return null;
            }

            return impersonationContext;
        }
        
        private void setContext(string user, string password, string domain){
            this.user = user;
            this.password = password;
            this.domain = domain;
        }


        public int RealizarBackupPresentaciones()
        {
            WindowsImpersonationContext wic = setUser();
            int ret = 2;

            if(wic != null)
            {
                log.Debug("Impersonate OK.");
                try
                {
                   
                    ArchivoUtils.RealizarBackupPresentaciones();
                    ret = 1;
                }
                catch (Exception ex)
                {
                    wic.Undo();
                    log.Error("Error en impersonate RealizarBackupPresentaciones ", ex);
                    ret = 2;
                }
                finally
                {
                    wic.Undo();

                    if (userHandle != IntPtr.Zero)
                    {
                        CloseHandle(userHandle);
                    }
                }

            }

            return ret;
        }

        public int GenerarArchivo(string contenido, string rutaArchivo, bool sobrescribir = false)
        {
            WindowsImpersonationContext wic = setUser();

            int ret = 2;

            if (wic != null)
            {

                log.Debug("Impersonate OK.");
                try
                {
                    ArchivoUtils.EscribeEnArchivo(contenido, rutaArchivo, sobrescribir);
                    ret = 1;
                }
                catch (Exception ex)
                {
                    wic.Undo();
                    log.Error("Error en impersonate GenerarArchivo ", ex);
                    ret = 2;
                }
                finally
                {
                    wic.Undo();

                    if (userHandle != IntPtr.Zero)
                    {
                        CloseHandle(userHandle);
                    }
                }

            }

            return ret;
        }


        public int EjecutarBat()
        {
            WindowsImpersonationContext wic = setUser();

            int ret = 2;
            System.Diagnostics.Process process = new System.Diagnostics.Process();

            if (wic != null)
            {
                try
                {

                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    startInfo.FileName = "cmd.exe";
                    startInfo.Arguments = "/C md pepe";
                    process.StartInfo = startInfo;
                    process.Start();
                    process.WaitForExit();
                    log.Info("ExitCode: " + process.ExitCode.ToString());
                    process.Close();
                    ret = 1;
                }
                catch (Exception ex)
                {
                    wic.Undo();
                    log.Error("Error en impersonate ejecutar bat ", ex);
                }
                finally
                {
                    wic.Undo();

                    if (userHandle != IntPtr.Zero)
                    {
                        CloseHandle(userHandle);
                    }
                }

            }

            return ret; ;

        }

        public void Undo(){
            if (wic != null){
                wic.Undo();

                if (userHandle != IntPtr.Zero){
                    CloseHandle(userHandle);
                }

                wic = null;
            }
        }





    }
}