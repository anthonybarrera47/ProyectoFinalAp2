using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoFinalAp2
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LogOut_ServerClick(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Session["Usuario"] = new Usuarios();
            Session["Empresa"] = new Empresas();
            FormsAuthentication.RedirectToLoginPage();
        }
    }
}