using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace waRegimenIVA.Helpers
{
    public class SecurityHelper
    {

        //GAPIIRMIvaSup
        //GAPIIRMIvaOpe


        public static bool IsUserSupervisor(string legajo)
        {
           bool bExiste = false;

            using (var context = new PrincipalContext(ContextType.Domain, "BGCMZ"))
            {
                using (var group = GroupPrincipal.FindByIdentity(context, "GAPIIRMIvaSup"))
                {
                    if (group == null)
                    {
                        //TODO: Write log
                    }
                    else
                    {
                        var users = group.GetMembers(true);

                        foreach (UserPrincipal user in users)
                        {
                            if (legajo == user.Name)
                            {
                                bExiste =  true;
                                break;
                            }
                        }
                    }
                }
            }
            return bExiste;
        }

        public static bool IsUserOperador(string legajo)
        {
            bool bExiste = false;

            using (var context = new PrincipalContext(ContextType.Domain, "BGCMZ"))
            {
                using (var group = GroupPrincipal.FindByIdentity(context, "GAPIIRMIvaOpe"))
                {
                    if (group == null)
                    {
                        //TODO: Write log
                    }
                    else
                    {
                        var users = group.GetMembers(true);

                        foreach (UserPrincipal user in users)
                        {
                            if (legajo == user.Name)
                            {
                                bExiste = true;
                                break;
                            }
                        }
                    }
                }
            }
            return bExiste;
        }
    }
}