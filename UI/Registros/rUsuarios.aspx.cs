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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Limpiar();
                FechaTextBox.Text = DateTime.Now.ToFormatDate();
                int id = Request.QueryString["UsuarioId"].ToInt();
                if (id > 0 || !ExisteEnLaBaseDeDatos())
                {

                }
            }
        }

        private void Limpiar()
        {
            
        }

        protected void NuevoButton_Click(object sender, EventArgs e)
        {
            UsuarioIdTxt.Text = "0";
            UserNameTxt.Text = string.Empty;
            FechaTextBox.Text = DateTime.Now.ToFormatDate();
            ClaveTxt.Text = string.Empty;
        }

        protected void GuadarButton_Click(object sender, EventArgs e)
        {
            bool paso = false;
            RepositorioBase<Usuarios> repositorio = new RepositorioBase<Usuarios>();
            Usuarios usuarios = LLenaClase();
            TipoTitulo tipoTitulo = TipoTitulo.OperacionFallida;
            TiposMensajes tiposMensajes = TiposMensajes.RegistroNoGuardado;
            IconType iconType = IconType.error;
            if (!RepositorioUsuarios.ValidarUsuario(usuarios))
            {
                Utils.Alerta(this, TipoTitulo.OperacionFallida,TiposMensajes.UsuarioExistente, IconType.error);
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

        private bool ExisteEnLaBaseDeDatos()
        {
            RepositorioBase<Usuarios> repositorio = new RepositorioBase<Usuarios>();
            Usuarios usuarios = repositorio.Buscar(UsuarioIdTxt.Text.ToInt());
            repositorio.Dispose();
            return !usuarios.EsNulo();
        }

        private Usuarios LLenaClase()
        {
            Usuarios user = new Usuarios();

            if (UsuarioIdTxt.Text.Equals(string.Empty))
                UsuarioIdTxt.Text = "0";
            user.UsuarioId = UsuarioIdTxt.Text.ToInt();
            user.UserName = UserNameTxt.Text;
            user.Password = RepositorioUsuarios.SHA1(ClaveTxt.Text);
            return user;
        }

        protected void EliminarButton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Usuarios> repositorio = new RepositorioBase<Usuarios>();
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
        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Usuarios> repositorio = new RepositorioBase<Usuarios>();
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

        private void LlenarCampos(Usuarios usuarios)
        {
            UsuarioIdTxt.Text = usuarios.UsuarioId.ToString();
            UserNameTxt.Text = usuarios.UserName;
            FechaTextBox.Text = usuarios.Fecha.ToFormatDate();
        }
    }
}