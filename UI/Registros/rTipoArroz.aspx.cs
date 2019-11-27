using BLL;
using Entidades;
using Enums;
using Extensores;
using Herramientas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoFinalAp2.UI.Registros
{
    public partial class rTipoArroz : System.Web.UI.Page
    {
        Empresas Empresa = new Empresas();
        Usuarios Usuario = new Usuarios();
        protected void Page_Load(object sender, EventArgs e)
        {
            Empresa = (Session["Empresas"] as Entidades.Empresas);
            Usuario = (Session["Usuario"] as Entidades.Usuarios);
            if (!Page.IsPostBack)
            {
                Limpiar();
                FechaTextBox.Text = DateTime.Now.ToFormatDate();
                int id = Request.QueryString["TipoArrozId"].ToInt();
                if (id > 0)
                {
                    RepositorioBase<TipoArroz> repositorio = new RepositorioBase<TipoArroz>();
                    var TipoArroz = repositorio.Buscar(id);
                    if (TipoArroz.EsNulo() || PerteneceALaEmpresa(TipoArroz.EmpresaId))
                        Utils.Alerta(this, TipoTitulo.Informacion, TiposMensajes.RegistroNoEncontrado, IconType.info);
                    else
                        LlenarCampos(TipoArroz);
                }
            }
        }

        private void Limpiar()
        {
            TipoArrozIdTxt.Text = "0";
            DescripcionTxt.Text = string.Empty;
            KilosTxt.Text = "0";
            FechaTextBox.Text = DateTime.Now.ToFormatDate();
        }

        protected void NuevoButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        protected void GuadarButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                bool paso = false;
                RepositorioBase<TipoArroz> repositorio = new RepositorioBase<TipoArroz>();
                TipoArroz tipoArroz = LLenaClase();
                TipoTitulo tipoTitulo = TipoTitulo.OperacionFallida;
                TiposMensajes tiposMensajes = TiposMensajes.RegistroNoGuardado;
                IconType iconType = IconType.error;

                if (tipoArroz.TipoArrozId == 0)
                {
                    tipoArroz.Kilos = 0;
                    paso = repositorio.Guardar(tipoArroz);
                }
                else
                {
                    if (!ExisteEnLaBaseDeDatos())
                    {
                        Utils.ToastSweet(this, IconType.info, TiposMensajes.RegistroNoEncontrado);
                        return;
                    }
                    tipoArroz.Kilos = repositorio.Buscar(tipoArroz.TipoArrozId).Kilos;
                    paso = repositorio.Modificar(tipoArroz);
                }

                if (paso)
                {
                    Limpiar();
                    tipoTitulo = TipoTitulo.OperacionExitosa;
                    tiposMensajes = TiposMensajes.RegistroGuardado;
                    iconType = IconType.success;
                }
                Utils.Alerta(this, tipoTitulo, tiposMensajes, iconType);
            }

        }

        private bool ExisteEnLaBaseDeDatos()
        {
            RepositorioBase<TipoArroz> repositorio = new RepositorioBase<TipoArroz>();
            TipoArroz tipoArroz = repositorio.Buscar(TipoArrozIdTxt.Text.ToInt());
            repositorio.Dispose();
            return !tipoArroz.EsNulo() && PerteneceALaEmpresa(tipoArroz.EmpresaId);
        }

        private TipoArroz LLenaClase()
        {
            TipoArroz tipoArroz = new TipoArroz();
            tipoArroz.TipoArrozId = TipoArrozIdTxt.Text.ToInt();
            tipoArroz.Descripcion = DescripcionTxt.Text;
            tipoArroz.Fecha = FechaTextBox.Text.ToDatetime();
            tipoArroz.Kilos = KilosTxt.Text.ToDecimal();
            tipoArroz.EmpresaId = Empresa.EmpresaID;
            tipoArroz.UsuarioId = Usuario.UsuarioId;
            return tipoArroz;
        }

        protected void EliminarButton_Click(object sender, EventArgs e)
        {
            RepositorioBase<TipoArroz> repositorio = new RepositorioBase<TipoArroz>();
            int id = TipoArrozIdTxt.Text.ToInt();
            if (!ExisteEnLaBaseDeDatos())
            {
                Utils.Alerta(this, TipoTitulo.OperacionFallida, TiposMensajes.RegistroInexistente, IconType.error);
                return;
            }
            else
            {
                if (repositorio.Eliminar(id))
                {
                    Utils.Alerta(this, TipoTitulo.OperacionExitosa, TiposMensajes.RegistroEliminado, IconType.success);
                    Limpiar();
                }
            }
            repositorio.Dispose();
        }
        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            RepositorioBase<TipoArroz> repositorio = new RepositorioBase<TipoArroz>();
            int Id = TipoArrozIdTxt.Text.ToInt();
            if (Id != 0)
            {
                TipoArroz tipoArroz = repositorio.Buscar(Id);
                if (!tipoArroz.EsNulo() && PerteneceALaEmpresa(tipoArroz.EmpresaId))
                {
                    Limpiar();
                    LlenarCampos(tipoArroz);
                }
                else
                    Utils.ToastSweet(this, IconType.info, TiposMensajes.RegistroNoEncontrado);
            }
            else
                Utils.ToastSweet(this, IconType.info, TiposMensajes.RegistroNoEncontrado);
            repositorio.Dispose();
        }
        private void LlenarCampos(TipoArroz tipoArroz)
        {
            Limpiar();
            TipoArrozIdTxt.Text = tipoArroz.TipoArrozId.ToString();
            DescripcionTxt.Text = tipoArroz.Descripcion;
            FechaTextBox.Text = tipoArroz.Fecha.ToFormatDate();
            KilosTxt.Text = tipoArroz.Kilos.ToString();
        }
        public bool PerteneceALaEmpresa(int id)
        {
            RepositorioBase<TipoArroz> repositorio = new RepositorioBase<TipoArroz>();
            TipoArroz tipoArroz = repositorio.Buscar(id);
            return tipoArroz.EmpresaId == Empresa.EmpresaID;
        }
    }
}