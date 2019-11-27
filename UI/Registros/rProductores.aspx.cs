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
    public partial class rProductores : System.Web.UI.Page
    {
        Empresas Empresa = new Empresas();
        Usuarios Usuario = new Usuarios();
        protected void Page_Load(object sender, EventArgs e)
        {
            Empresa = (Session["Empresas"] as Entidades.Empresas);
            Usuario = (Session["Usuario"] as Entidades.Usuarios);

            if (!Page.IsPostBack)
            {
                FechaTextBox.Text = DateTime.Now.ToFormatDate();
                int id = Request.QueryString["ProductorId"].ToInt();
                if (id > 0)
                {
                    RepositorioBase<Productores> repositorio = new RepositorioBase<Productores>();
                    var Productores = repositorio.Buscar(id);
                    if (Productores.EsNulo() || PerteneceALaEmpresa(Productores.EmpresaId))
                        Utils.Alerta(this, TipoTitulo.Informacion, TiposMensajes.RegistroNoEncontrado, IconType.info);
                    else
                        LlenarCampos(Productores);
                }
            }
        }
        private void Limpiar()
        {
            ProductorIdTxt.Text = "0";
            NombresTxt.Text = string.Empty;
            TelefonoTxt.Text = string.Empty;
            CedulaTxt.Text = string.Empty;
            FechaTextBox.Text = DateTime.Now.ToFormatDate();
        }
        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Productores> repositorio = new RepositorioBase<Productores>();
            int Id = ProductorIdTxt.Text.ToInt();
            if (Id != 0)
            {
                Productores Productores = repositorio.Buscar(Id);
                if (!Productores.EsNulo() && PerteneceALaEmpresa(Productores.ProductorId))
                {
                    Limpiar();
                    LlenarCampos(Productores);
                }
                else
                    Utils.ToastSweet(this, IconType.info, TiposMensajes.RegistroNoEncontrado);
            }
            else
                Utils.ToastSweet(this, IconType.info, TiposMensajes.RegistroNoEncontrado);
            repositorio.Dispose();
        }

        private void LlenarCampos(Productores productores)
        {
            ProductorIdTxt.Text = productores.ProductorId.ToString();
            NombresTxt.Text = productores.Nombre;
            TelefonoTxt.Text = productores.Telefono;
            CedulaTxt.Text = productores.Cedula;
            FechaNacimientoTxt.Text = productores.FechaNacimiento.ToFormatDate();
            FechaTextBox.Text = productores.Fecha.ToFormatDate();
        }

        protected void NuevoButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        protected void GuadarButton_Click(object sender, EventArgs e)
        {
            bool paso = false;
            RepositorioBase<Productores> repositorio = new RepositorioBase<Productores>();
            Productores productores = LLenaClase();
            TipoTitulo tipoTitulo = TipoTitulo.OperacionFallida;
            TiposMensajes tiposMensajes = TiposMensajes.RegistroNoGuardado;
            IconType iconType = IconType.error;

            if (productores.ProductorId == 0)
                paso = repositorio.Guardar(productores);
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    Utils.ToastSweet(this, IconType.info, TiposMensajes.RegistroNoEncontrado);
                    return;
                }
                paso = repositorio.Modificar(productores);
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

        private bool ExisteEnLaBaseDeDatos()
        {
            RepositorioBase<Productores> repositorio = new RepositorioBase<Productores>();
            Productores Productores = repositorio.Buscar(ProductorIdTxt.Text.ToInt());
            repositorio.Dispose();
            return !Productores.EsNulo() && PerteneceALaEmpresa(Productores.EmpresaId);
        }

        private Productores LLenaClase()
        {
            Productores Productores = new Productores();
            Productores.ProductorId = ProductorIdTxt.Text.ToInt();
            Productores.Nombre = NombresTxt.Text;

            Productores.Telefono = TelefonoTxt.Text;
            Productores.Cedula = CedulaTxt.Text;
            Productores.FechaNacimiento = FechaNacimientoTxt.Text.ToDatetime();
            Productores.Fecha = FechaTextBox.Text.ToDatetime();
            Productores.EmpresaId = Empresa.EmpresaID;
            Productores.UsuarioId = Usuario.UsuarioId;
            return Productores;
        }

        protected void EliminarButton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Productores> repositorio = new RepositorioBase<Productores>();
            int id = ProductorIdTxt.Text.ToInt();
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
        public bool PerteneceALaEmpresa(int id)
        {
            RepositorioBase<Productores> repositorio = new RepositorioBase<Productores>();
            Productores productor = repositorio.Buscar(id);
            return productor.EmpresaId == Empresa.EmpresaID;
        }
    }
}