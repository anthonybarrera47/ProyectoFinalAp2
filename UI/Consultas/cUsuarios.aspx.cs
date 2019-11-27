using BLL;
using Entidades;
using Extensores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoFinalAp2.UI.Consultas
{
    public partial class cUsuarios : System.Web.UI.Page
    {

        static List<Usuarios> lista = new List<Usuarios>();
        Empresas Empresa = new Empresas();
        Usuarios Usuario = new Usuarios();
        protected void Page_Load(object sender, EventArgs e)
        {
            Empresa = (Session["Empresas"] as Entidades.Empresas);
            Usuario = (Session["Usuario"] as Entidades.Usuarios);
            if (!RepositorioUsuarios.UsuarioEsAdministrador(Usuario))
            {
                Response.Redirect("~/default.aspx");
                return;
            }
            if (!Page.IsPostBack)
            {
                FechaDesdeTextBox.Text = DateTime.Now.ToFormatDate();
                FechaHastaTextBox.Text = DateTime.Now.ToFormatDate();
            }
        }

        protected void DatosGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DatosGridView.DataSource = lista;
            DatosGridView.PageIndex = e.NewPageIndex;
            DatosGridView.DataBind();
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            Expression<Func<Usuarios, bool>> filtro = x => true;
            RepositorioUsuarios repositorio = new RepositorioUsuarios();
            switch (BuscarPorDropDownList.SelectedIndex)
            {
                case 0:
                    filtro = x => true;
                    break;
                case 1:
                    filtro = x => x.UserName.Contains(CriterioTextBox.Text);
                    break;
            }
            DateTime fechaDesde = FechaDesdeTextBox.Text.ToDatetime();
            DateTime FechaHasta = FechaHastaTextBox.Text.ToDatetime();

            if (Request.Form["FiltraFecha"] != null)
                lista = repositorio.GetList(filtro).Where(x => x.Fecha >= fechaDesde && x.Fecha <= FechaHasta).ToList();
            else
                lista = repositorio.GetList(filtro);
            repositorio.Dispose();
            this.BindGrid(lista.Where(x => x.Empresa == Empresa.EmpresaID).ToList());
        }
        private void BindGrid(List<Usuarios> lista)
        {
            DatosGridView.DataSource = null;
            DatosGridView.DataSource = lista;
            DatosGridView.DataBind();
        }
    }
}