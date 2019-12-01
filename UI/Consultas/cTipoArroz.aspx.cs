using BLL;
using ClosedXML.Excel;
using ClosedXML.Extensions;
using Entidades;
using Extensores;
using Herramientas;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoFinalAp2.UI.Consultas
{
    public partial class cTipoArroz : System.Web.UI.Page
    {
        readonly string KeyViewState = "Factorias";
        static List<TipoArroz> lista = new List<TipoArroz>();
        Empresas Empresa = new Empresas();
        protected void Page_Load(object sender, EventArgs e)
        {
            Empresa = (Session["Empresas"] as Entidades.Empresas);
            if (!Page.IsPostBack)
            {
                ViewState[KeyViewState] = new List<TipoArroz>();
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
            this.BindGrid(lista.Where(x => x.EmpresaId == Empresa.EmpresaID).ToList());
        }
        private void BindGrid(List<TipoArroz> lista)
        {
            DatosGridView.DataSource = null;
            DatosGridView.DataSource = lista;
            ViewState[KeyViewState] = lista;
            DatosGridView.DataBind();
        }
        protected void ImprimirButton_Click(object sender, EventArgs e)
        {
            Utils.MostrarModal(this, "ModalReporte", "Listado de Tipos de Arroz");
            
            List<Empresas> empresas = new List<Empresas>();
            List<TipoArroz> ListaImprimir = ((List<TipoArroz>)ViewState[KeyViewState]);
            empresas.Add(Empresa);
            Reportviewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
            Reportviewer.Reset();
            Reportviewer.LocalReport.ReportPath = Server.MapPath(@"~\UI\Reportes\ListadoTipoArroz.rdlc");
            Reportviewer.LocalReport.DataSources.Clear();

            Reportviewer.LocalReport.DataSources.Add(new ReportDataSource("Empresa",
                                                               empresas));
            Reportviewer.LocalReport.DataSources.Add(new ReportDataSource("TipoArrozDataSet",
                                                               ListaImprimir));
            Reportviewer.LocalReport.Refresh();
        }

        protected void DatosGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DatosGridView.DataSource = lista;
            DatosGridView.PageIndex = e.NewPageIndex;
            DatosGridView.DataBind();
        }

        protected void ExportarButton_Click(object sender, EventArgs e)
        {
            List<TipoArroz> lista = ((List<TipoArroz>)ViewState[KeyViewState]);

            DataTable dt = Utils.ToDataTable<TipoArroz>(lista);
            XLWorkbook workbook = new XLWorkbook();
            workbook.AddWorksheet(dt);
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=\"ListadoTipoArroz.xlsx\"");

            using (var memoryStream = new MemoryStream())
            {
                workbook.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
            }
            Response.End();
            workbook.Dispose();
        }
    }
}
