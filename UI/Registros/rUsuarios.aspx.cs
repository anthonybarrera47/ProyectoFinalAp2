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
    public partial class rUsuarios : System.Web.UI.Page
    {
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
                FechaTextBox.Text = DateTime.Now.ToFormatDate();
                int id = Request.QueryString["UsuarioId"].ToInt();
                if (id > 0 && PerteneceALaEmpresa(Usuario.Empresa))
                {
                    RepositorioUsuarios repositorio = new RepositorioUsuarios();
                    var Usuario = repositorio.Buscar(id);
                    if (Usuario.EsNulo() || PerteneceALaEmpresa(Usuario.Empresa))
                        Utils.Alerta(this, TipoTitulo.Informacion, TiposMensajes.RegistroNoEncontrado, IconType.info);
                    else
                        LlenarCampos(Usuario);
                }
            }
        }
        private bool ExisteEnLaBaseDeDatos()
        {
            RepositorioBase<Usuarios> repositorio = new RepositorioBase<Usuarios>();
            Usuarios usuarios = repositorio.Buscar(UsuarioIdTxt.Text.ToInt());
            repositorio.Dispose();
            return !usuarios.EsNulo() && PerteneceALaEmpresa(usuarios.Empresa);
        }
        private void LlenarCampos(Usuarios usuarios)
        {
            UsuarioIdTxt.Text = usuarios.UsuarioId.ToString();
            UserNameTxt.Text = usuarios.UserName;
            FechaTextBox.Text = usuarios.Fecha.ToFormatDate();
        }
        private Usuarios LLenaClase()
        {
            Usuarios user = new Usuarios();
            int tipo = -1;

            if (UsuarioIdTxt.Text.Equals(string.Empty))
                UsuarioIdTxt.Text = "0";
            user.UsuarioId = UsuarioIdTxt.Text.ToInt();
            user.UserName = UserNameTxt.Text;
            user.Password = RepositorioUsuarios.SHA1(ClaveTxt.Text);
            if (Request.Form["TipoUsuario"] != null)
                tipo = Request.Form["TipoUsuario"].ToInt();
            user.TipoUsuario = (tipo == 0) ? TipoUsuario.Administrador : TipoUsuario.UsuarioNormal;
            return user;
        }
        private void Limpiar()
        {
            UsuarioIdTxt.Text = string.Empty;
            UserNameTxt.Text = string.Empty;
            FechaTextBox.Text = DateTime.Now.ToFormatDate();
            ClaveTxt.Text = string.Empty;
            ConfClaveTxt.Text = string.Empty;
            NombreCompletoTxt.Text = string.Empty;
            DireccionTextBox.Text = string.Empty;
            EmailTextBox.Text = string.Empty;
            FechaNacimientoTxt.Text = DateTime.Now.ToFormatDate();
        }

        public bool PerteneceALaEmpresa(int id)
        {
            RepositorioUsuarios repositorio = new RepositorioUsuarios();
            Usuarios user = repositorio.Buscar(id);
            return user.Empresa == Empresa.EmpresaID;
        }
        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            RepositorioUsuarios repositorio = new RepositorioUsuarios();
            int Id = UsuarioIdTxt.Text.ToInt();
            if (Id != 0)
            {
                Usuarios usuarios = repositorio.Buscar(Id);
                if (!usuarios.EsNulo())
                {
                    Limpiar();
                    LlenarCampos(usuarios);
                }
                else
                    Utils.ToastSweet(this, IconType.info, TiposMensajes.RegistroNoEncontrado);
            }
            else
                Utils.ToastSweet(this, IconType.info, TiposMensajes.RegistroNoEncontrado);
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
                RepositorioUsuarios repositorio = new RepositorioUsuarios();
                Usuarios usuarios = LLenaClase();
                TipoTitulo tipoTitulo = TipoTitulo.OperacionFallida;
                TiposMensajes tiposMensajes = TiposMensajes.RegistroNoGuardado;
                IconType iconType = IconType.error;
                if (!RepositorioUsuarios.ValidarUsuario(usuarios))
                {
                    Utils.Alerta(this, TipoTitulo.OperacionFallida, TiposMensajes.UsuarioExistente, IconType.error);
                    return;
                }
                if (!RepositorioUsuarios.ValidarCorreo(usuarios))
                {
                    Utils.Alerta(this, TipoTitulo.OperacionFallida, TiposMensajes.CorreExistente, IconType.error);
                    return;
                }
                if (usuarios.UsuarioId == 0)
                    paso = repositorio.Guardar(usuarios);
                else
                {
                    if (!ExisteEnLaBaseDeDatos())
                    {
                        Utils.ToastSweet(this, IconType.info, TiposMensajes.RegistroNoEncontrado);
                        return;
                    }
                    paso = repositorio.Modificar(usuarios);
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
        protected void EliminarButton_Click(object sender, EventArgs e)
        {
            RepositorioUsuarios repositorio = new RepositorioUsuarios();
            int id = UsuarioIdTxt.Text.ToInt();
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

    }
}