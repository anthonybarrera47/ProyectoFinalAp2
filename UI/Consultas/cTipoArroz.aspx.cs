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
    public partial class cTipoArroz : System.Web.UI.Page
    {
        static List<TipoArroz> lista = new List<TipoArroz>();
        Empresas Empresa = new Empresas();
        protected void Page_Load(object sender, EventArgs e)
        {
            Empresa = (Session["Empresas"] as Entidades.Empresas);
            if (!Page.IsPostBack)
            {
                FechaDesdeTextBox.Text = DateTime.Now.ToFormatDate();
                FechaHastaTextBox.Text = DateTime.Now.ToFormatDate();
            }
        }
        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            Expression<Func<TipoArroz, bool>> filtro = x => true;
            RepositorioBase<TipoArroz> repositorio = new RepositorioBase<TipoArroz>();
            switch (BuscarPorDropDownList.SelectedIndex)
            {
                case 0:
                    filtro = x => true;
                    break;
                case 1:
                    filtro = x => x.Descripcion.Contains(CriterioTextBox.Text);
                    break;
            }
            DateTime fechaDesde = FechaDesdeTextBox.Text.ToDatetime();
            DateTime FechaHasta = FechaHastaTextBox.Text.ToDatetime();
            
            if (Request.Form["FiltraFecha"] != null)
                lista = repositorio.GetList(filtro).Where(x => x.Fecha >= fechaDesde && x.Fecha <= FechaHasta).ToList();
            else
                lista = repositorio.GetList(filtro);
            repositorio.Dispose();
            this.BindGrid(lista.Where(x=>x.EmpresaId==Empresa.EmpresaID).ToList());
        }
        private void BindGrid(List<TipoArroz> lista)
        {
            DatosGridView.DataSource = null;
            DatosGridView.DataSource = lista;
            DatosGridView.DataBind();
        }
        protected void ImprimirButton_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", $"ShowReporte('Listado de Analisis');", true);

            //ReportViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
            //ReportViewer.Reset();
            //ReportViewer.LocalReport.ReportPath = Server.MapPath(@"~\Reportes\ListadoAnalisis.rdlc");
            //ReportViewer.LocalReport.DataSources.Clear();

            //ReportViewer.LocalReport.DataSources.Add(new ReportDataSource("Analisis",
            //                                                   lista));
            //ReportViewer.LocalReport.Refresh();
        }

        protected void DatosGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DatosGridView.DataSource = lista;
            DatosGridView.PageIndex = e.NewPageIndex;
            DatosGridView.DataBind();
        }

        protected void ExportarButton_Click(object sender, EventArgs e)
        {

        }
    }
}
