using BLL;
using ClosedXML.Excel;
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
    public partial class cFactorias : System.Web.UI.Page
    {
        readonly string KeyViewState = "Factorias";
        static List<Factoria> lista = new List<Factoria>(); 
        Empresas Empresa = new Empresas();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            Empresa = (Session["Empresas"] as Entidades.Empresas);
            if (!Page.IsPostBack)
            {
                ViewState[KeyViewState] = new List<Factoria>();
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
            Expression<Func<Factoria, bool>> filtro = x => true;
            RepositorioBase<Factoria> repositorio = new RepositorioBase<Factoria>();
            switch (BuscarPorDropDownList.SelectedIndex)
            {
                case 0:
                    filtro = x => true;
                    break;
                case 1:
                    filtro = x => x.Nombre.Contains(CriterioTextBox.Text);
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
        private void BindGrid(List<Factoria> lista)
        {
            DatosGridView.DataSource = null;
            DatosGridView.DataSource = lista;
            ViewState[KeyViewState] = lista;
            DatosGridView.DataBind();
        }

        protected void ExportarButton_Click(object sender, EventArgs e)
        {
            List<Factoria> lista = ((List<Factoria>)ViewState[KeyViewState]);

            DataTable dt = Utils.ToDataTable<Factoria>(lista);
            XLWorkbook workbook = new XLWorkbook();
            workbook.AddWorksheet(dt);
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=\"ListadoFactorias.xlsx\"");

            using (var memoryStream = new MemoryStream())
            {
                workbook.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
            }
            Response.End();
            workbook.Dispose();
        }
        
        protected void ImprimirButton_Click(object sender, EventArgs e)
        {
            Utils.MostrarModal(this, "ModalReporte", "Listado de Factorias");

            List<Empresas> empresas = new List<Empresas>();
            List<Factoria> ListaImprimir = ((List<Factoria>)ViewState[KeyViewState]);

            empresas.Add(Empresa);
            Reportviewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
            Reportviewer.Reset();
            Reportviewer.LocalReport.ReportPath = Server.MapPath(@"~\UI\Reportes\ListadoFactorias.rdlc");
            Reportviewer.LocalReport.DataSources.Clear();
            Reportviewer.LocalReport.DataSources.Add(new ReportDataSource("EmpresasDS",
                                                               empresas));
            Reportviewer.LocalReport.DataSources.Add(new ReportDataSource("FactoriasDS",
                                                               ListaImprimir));
            Reportviewer.LocalReport.Refresh();
        }
    }
}