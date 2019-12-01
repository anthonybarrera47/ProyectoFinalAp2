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
    public partial class cPesadas : System.Web.UI.Page
    {

        readonly string KeyViewState = "Factorias";
        static List<Pesadas> lista = new List<Pesadas>();
        Empresas Empresa = new Empresas();
        protected void Page_Load(object sender, EventArgs e)
        {
            Empresa = (Session["Empresas"] as Entidades.Empresas);
            if (!Page.IsPostBack)
            {
                ViewState[KeyViewState] = new List<Pesadas>();
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
            Expression<Func<Pesadas, bool>> filtro = x => true;
            RepositorioPesadas repositorio = new RepositorioPesadas();
            switch (BuscarPorDropDownList.SelectedIndex)
            {
                case 0:
                    filtro = x => true;
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
        private void BindGrid(List<Pesadas> lista)
        {
            DatosGridView.DataSource = null;
            DatosGridView.DataSource = lista;
            ViewState[KeyViewState] = lista;
            DatosGridView.DataBind();
        }

        protected void ExportarButton_Click(object sender, EventArgs e)
        {
            List<Pesadas> lista = ((List<Pesadas>)ViewState[KeyViewState]);

            DataTable dt = Utils.ToDataTable<Pesadas>(lista);
            XLWorkbook workbook = new XLWorkbook();
            workbook.AddWorksheet(dt);
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=\"ListadoPesadas.xlsx\"");

            using (var memoryStream = new MemoryStream())
            {
                workbook.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
            }
            Response.End();
            workbook.Dispose();
        }

        protected void DetalleDatosGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DetalleDatosGridView.DataSource = lista;
            DetalleDatosGridView.PageIndex = e.NewPageIndex;
            DetalleDatosGridView.DataBind();
        }

        protected void VerDetalleButton_Click(object sender, EventArgs e)
        {
            Utils.MostrarModal(this.Page, "ModalDetalle", "Detalle");
            GridViewRow row = (sender as Button).NamingContainer as GridViewRow;
            var Pesada = lista.ElementAt(row.RowIndex);
            RepositorioPesadas Repositorio = new RepositorioPesadas();
            List<PesadasDetalle> Details = Repositorio.Buscar(Pesada.PesadaId).Detalles;
            DetalleDatosGridView.DataSource = null;
            DetalleDatosGridView.DataSource = Details;
            DetalleDatosGridView.DataBind();
            Repositorio.Dispose();
        }

        protected void ImprimirButton_Click(object sender, EventArgs e)
        {
            Utils.MostrarModal(this, "ModalReporte", "Listado de Pesadas");
            RepositorioBase<Productores> repositorio = new RepositorioBase<Productores>();
            RepositorioBase<Factoria> repositorioFactoria = new RepositorioBase<Factoria>();
            List<Empresas> empresas = new List<Empresas>();
            List<Productores> productores = new List<Productores>();
            List<Factoria> Factorias = new List<Factoria>();
            List<Pesadas> pesadasImprimir = ((List<Pesadas>)ViewState[KeyViewState]);
            Productores productor = new Productores();
            Factoria factoria = new Factoria();
            foreach (var item in lista)
            {
                productor = repositorio.Buscar(item.ProductorId);
                productores.Add(productor);
                factoria = repositorioFactoria.Buscar(item.FactoriaId);
                Factorias.Add(factoria);
            }
            repositorioFactoria.Dispose();
            repositorio.Dispose();
            empresas.Add(Empresa);
            Reportviewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
            Reportviewer.Reset();
            Reportviewer.LocalReport.ReportPath = Server.MapPath(@"~\UI\Reportes\ListadoPesadas.rdlc");
            Reportviewer.LocalReport.DataSources.Clear();
            Reportviewer.LocalReport.DataSources.Add(new ReportDataSource("EmpresaDS",
                                                               empresas));
            Reportviewer.LocalReport.DataSources.Add(new ReportDataSource("PesadaDS",
                                                               pesadasImprimir));
            Reportviewer.LocalReport.DataSources.Add(new ReportDataSource("ProductoresDS",
                                                               productores));
            Reportviewer.LocalReport.DataSources.Add(new ReportDataSource("Factoria",
                                                               Factorias));
            Reportviewer.LocalReport.Refresh();
        }
    }
}