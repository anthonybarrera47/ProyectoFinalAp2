using BLL;
using Entidades;
using Enums;
using Extensores;
using Herramientas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoFinalAp2.UI.Consultas
{
    public partial class cConsultaSolicitudes : System.Web.UI.Page
    {
        readonly string KeyViewState = "Solicitud";
        static List<SolicitudUsuarios> lista = new List<SolicitudUsuarios>();
        Empresas Empresa = new Empresas();
        protected void Page_Load(object sender, EventArgs e)
        {
            Empresa = (Session["Empresas"] as Entidades.Empresas);
            if (!Page.IsPostBack)
            {
                ViewState[KeyViewState] = new List<SolicitudUsuarios>();
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
            Expression<Func<SolicitudUsuarios, bool>> filtro = x => true;
            RepositorioSolicitudes repositorio = new RepositorioSolicitudes();
            switch (BuscarPorDropDownList.SelectedIndex)
            {
                case 0:
                    filtro = x => true;
                    break;
                case 1:
                    filtro = x => x.NombreUsuario.Contains(CriterioTextBox.Text);
                    break;
                case 2:
                    filtro = x => x.Estado == EstadoSolicitud.Autorizado;
                    break;
                case 3:
                    filtro = x => x.Estado == EstadoSolicitud.Pendiente;
                    break;
                case 4:
                    filtro = x => x.Estado == EstadoSolicitud.Denegado;
                    break;
            }
            DateTime fechaDesde = FechaDesdeTextBox.Text.ToDatetime();
            DateTime FechaHasta = FechaHastaTextBox.Text.ToDatetime();

            if (Request.Form["FiltraFecha"] != null)
                lista = repositorio.GetList(filtro).Where(x => x.Fecha.Date >= fechaDesde && x.Fecha.Date <= FechaHasta).ToList();
            else
                lista = repositorio.GetList(filtro);
            repositorio.Dispose();
            this.BindGrid(lista.Where(x => x.EmpresaId == Empresa.EmpresaID).ToList());
        }
        private void BindGrid(List<SolicitudUsuarios> lista)
        {
            DatosGridView.DataSource = null;
            DatosGridView.DataSource = lista;
            ViewState[KeyViewState] = lista;
            DatosGridView.DataBind();
        }
        protected void AprobarButton_Click(object sender, EventArgs e)
        {
            RepositorioSolicitudes repositorio = new RepositorioSolicitudes();
            List<SolicitudUsuarios> ListaSolicitu = (ViewState[KeyViewState] as List<SolicitudUsuarios>);
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            SolicitudUsuarios solicitud = ListaSolicitu.ElementAt(row.RowIndex);
            TipoTitulo tipoTitulo = TipoTitulo.OperacionFallida;
            TiposMensajes tiposMensajes = TiposMensajes.RegistroNoGuardado;
            IconType iconType = IconType.error;
            bool paso = false;

            solicitud = repositorio.Buscar(solicitud.SolicitudId);
            if (solicitud.Estado == EstadoSolicitud.Pendiente)
            {
                if (solicitud.EmpresaId == Empresa.EmpresaID)
                {
                    solicitud.Estado = EstadoSolicitud.Autorizado;
                    paso = repositorio.Modificar(solicitud);
                }
                if (paso)
                {
                    tipoTitulo = TipoTitulo.OperacionExitosa;
                    tiposMensajes = TiposMensajes.RegistroGuardado;
                    iconType = IconType.success;
                }
                Utils.Alerta(this, tipoTitulo, tiposMensajes, iconType);
            }
            else
                Utils.ToastSweet(this, IconType.error, TiposMensajes.YaFueDenegadaOAprobada);

        }

        protected void DenegarButton_Click(object sender, EventArgs e)
        {
            RepositorioSolicitudes repositorio = new RepositorioSolicitudes();
            List<SolicitudUsuarios> ListaSolicitu = (ViewState[KeyViewState] as List<SolicitudUsuarios>);
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            SolicitudUsuarios solicitud = ListaSolicitu.ElementAt(row.RowIndex);
            TipoTitulo tipoTitulo = TipoTitulo.OperacionFallida;
            TiposMensajes tiposMensajes = TiposMensajes.RegistroNoGuardado;
            IconType iconType = IconType.error;
            bool paso = false;

            solicitud = repositorio.Buscar(solicitud.SolicitudId);
            if (solicitud.Estado == EstadoSolicitud.Pendiente)
            {
                if (solicitud.EmpresaId == Empresa.EmpresaID)
                {
                    solicitud.Estado = EstadoSolicitud.Denegado;
                    paso = repositorio.Modificar(solicitud);
                }
                if (paso)
                {
                    tipoTitulo = TipoTitulo.OperacionExitosa;
                    tiposMensajes = TiposMensajes.RegistroGuardado;
                    iconType = IconType.success;
                }
                Utils.Alerta(this, tipoTitulo, tiposMensajes, iconType);
            }
            else
                Utils.ToastSweet(this, IconType.error, TiposMensajes.YaFueDenegadaOAprobada);
        }
    }
}
