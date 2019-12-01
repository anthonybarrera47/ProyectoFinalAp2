using BLL;
using Entidades;
using Enums;
using Extensores;
using Herramientas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoFinalAp2
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RepositorioBase<Usuarios> repositorio = new RepositorioBase<Usuarios>();
            repositorio.GetList(x => true);
            repositorio.Dispose();
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            if (RepositorioUsuarios.Autenticar(UserNameTextBox.Text, PasswordTextBox.Text))
            {
                Usuarios usuarios = RepositorioUsuarios.GetUser(UserNameTextBox.Text.EliminarEspaciosEnBlanco());
                Empresas Empresa = RepositorioUsuarios.GetEmpresas(usuarios.UsuarioId);
                if (!usuarios.EsPropietarioEmpresa)
                {
                    RepositorioBase<SolicitudUsuarios> repositorio = new RepositorioBase<SolicitudUsuarios>();
                    SolicitudUsuarios solicitud = repositorio.GetList(x => x.UsuarioId == usuarios.UsuarioId).FirstOrDefault();
                    if (solicitud.EsNulo())
                        Utils.Alerta(this, Enums.TipoTitulo.Informacion, Enums.TiposMensajes.SinSolicitud, Enums.IconType.info);
                    else if (solicitud.Estado == Entidades.EstadoSolicitud.Denegado)
                        Utils.Alerta(this, Enums.TipoTitulo.OperacionFallida, Enums.TiposMensajes.SolicitudDenegada, Enums.IconType.error);
                    else if (solicitud.Estado == Entidades.EstadoSolicitud.Pendiente)
                        Utils.Alerta(this, Enums.TipoTitulo.OperacionFallida, Enums.TiposMensajes.SolicitudEspera, Enums.IconType.info);
                    else if (solicitud.Estado == Entidades.EstadoSolicitud.Autorizado)
                    {
                        Session["Usuario"] = usuarios;
                        Session["Empresas"] = Empresa;
                        FormsAuthentication.RedirectFromLoginPage(usuarios.UserName, true);
                    }
                    repositorio.Dispose();
                }
                else
                {
                    Session["Usuario"] = usuarios;
                    Session["Empresas"] = Empresa;
                    FormsAuthentication.RedirectFromLoginPage(usuarios.UserName, true);
                }

            }
            else
                Utils.ToastSweet(this.Page, Enums.IconType.error, Enums.TiposMensajes.LoginIncorrecto);
        }

        protected void GuardarComoEmpresaButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Usuarios usuarios = new Usuarios
                {
                    UserName = UserNameComoEmpresa.Text,
                    Correo = EmailComoEmpresatxt.Text.ToString(),
                    Password = RepositorioUsuarios.SHA1(PasswordComoEmpresa.Text),
                    EsPropietarioEmpresa = true,
                    TipoUsuario = TipoUsuario.Administrador
                };
                GuardarUsuario(usuarios);
            }


        }
        private bool ValidarClave()
        {
            if (!RepositorioUsuarios.SHA1(PasswordComoEmpresa.Text).Equals(RepositorioUsuarios.SHA1(ConfPasswordComoEmpresa.Text)))
            {
                Utils.Alerta(this, Enums.TipoTitulo.OperacionFallida, Enums.TiposMensajes.ClaveNoCoincide, Enums.IconType.error);
                return false;
            }
            return true;
        }
        private bool ExisteEmpresa()
        {
            RepositorioBase<Empresas> repositorio = new RepositorioBase<Empresas>();
            repositorio.Dispose();
            return repositorio.ExisteEnLaBaseDeDatos(CodigoEmpresaTxt.Text.ToInt());
        }
        protected void GuardarComoUsuario_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Usuarios usuarios = new Usuarios
                {
                    UserName = UserNameComoUserTxt.Text,
                    Password = RepositorioUsuarios.SHA1(ClaveTxt.Text),
                    TipoUsuario = TipoUsuario.UsuarioNormal,
                    Correo = EmailTxtComousuario.Text.ToString(),
                    EsPropietarioEmpresa = false,
                    Empresa = CodigoEmpresaTxt.Text.ToInt()
                };
                GuardarUsuario(usuarios);
            }
            else
                Utils.ToastSweet(this, IconType.error, TiposMensajes.EmpresaNoExiste);

        }
        private void GuardarUsuario(Usuarios usuarios)
        {
            if (Page.IsValid)
            {
                if (!ValidarClave())
                    return;
                if (!RepositorioUsuarios.ValidarUsuario(usuarios))
                {
                    Utils.Alerta(this, Enums.TipoTitulo.OperacionFallida, Enums.TiposMensajes.UsuarioExistente, Enums.IconType.error);
                    return;
                }
                if (!RepositorioUsuarios.ValidarCorreo(usuarios))
                {
                    Utils.Alerta(this, Enums.TipoTitulo.OperacionFallida, Enums.TiposMensajes.CorreExistente, Enums.IconType.error);
                    return;
                }
                RepositorioUsuarios repositorio = new RepositorioUsuarios();
                RepositorioBase<Empresas> repositorioEmpresa = new RepositorioBase<Empresas>();
                Empresas empresas = new Empresas();
                TipoTitulo tipoTitulo = TipoTitulo.OperacionFallida;
                TiposMensajes tiposMensajes = TiposMensajes.RegistroNoGuardado;
                IconType iconType = IconType.error;

                if (repositorio.Guardar(usuarios))
                {
                    tipoTitulo = TipoTitulo.OperacionExitosa;
                    tiposMensajes = TiposMensajes.RegistroGuardado;
                    iconType = IconType.success;
                    if (usuarios.EsPropietarioEmpresa)
                    {
                        empresas.UsuarioId = usuarios.UsuarioId;
                        empresas.NombreEmpresa = NombreEmpresaTxt.Text;
                        repositorioEmpresa.Guardar(empresas);
                        usuarios.Empresa = empresas.EmpresaID;
                        repositorio.Modificar(usuarios);
                        repositorio.Dispose();
                    }
                    else
                    {
                        if (!ExisteEmpresa())
                        {
                            Utils.Alerta(this, TipoTitulo.OperacionFallida, TiposMensajes.EmpresaNoExiste, IconType.error);
                            return;
                        }
                        RepositorioBase<SolicitudUsuarios> repositorioBase = new RepositorioBase<SolicitudUsuarios>();
                        SolicitudUsuarios solicitud = new SolicitudUsuarios
                        {
                            SolicitudId = 0,
                            UsuarioId = usuarios.UsuarioId,
                            EmpresaId = usuarios.Empresa,
                            Estado = EstadoSolicitud.Pendiente
                        };
                        if (repositorioBase.Guardar(solicitud))
                            Utils.Alerta(this, TipoTitulo.Informacion, TiposMensajes.EsperarConfirmacion, IconType.info);
                        else
                            Utils.ToastSweet(this, IconType.error, TiposMensajes.ComunicarseConAdmi);
                        repositorioBase.Dispose();
                    }

                }
                else
                    Utils.Alerta(this, TipoTitulo.OperacionFallida, TiposMensajes.RegistroNoGuardado, IconType.error);
                if (usuarios.EsPropietarioEmpresa)
                {
                    UserNameTextBox.Text = usuarios.UserName;
                    PasswordTextBox.Text = PasswordComoEmpresa.Text;
                    LoginButton_Click(null, null);
                }
                Utils.Alerta(this, tipoTitulo, tiposMensajes, iconType);
                repositorio.Dispose();
                repositorioEmpresa.Dispose();
            }
        }

        protected void CustomCodigoValidate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = ExisteEmpresa();
        }
    }
}