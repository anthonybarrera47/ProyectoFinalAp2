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
    public partial class rFactoria : System.Web.UI.Page
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
                int id = Request.QueryString["FactoriaId"].ToInt();
                if (id > 0)
                {
                    RepositorioBase<Factoria> repositorio = new RepositorioBase<Factoria>();
                    var Factoria = repositorio.Buscar(id);
                    if (Factoria.EsNulo() || !PerteneceALaEmpresa(Factoria.FactoriaId))
                        Utils.Alerta(this, TipoTitulo.Informacion, TiposMensajes.RegistroNoEncontrado, IconType.info);
                    else
                        LlenarCampos(Factoria);
                }
            }
                
        }
        private void Limpiar()
        {
            FactoriaIdTxt.Text = "0"; 
            DireccionTxt.Text = string.Empty;
            NombresTxt.Text = string.Empty;
            FechaTextBox.Text = DateTime.Now.ToFormatDate();
            TelefonoTxt.Text = string.Empty;
        }
        public Factoria LLenaClase()
        {
            Factoria factoria = new Factoria
            {
                FactoriaId = FactoriaIdTxt.Text.ToInt(),
                Nombre = NombresTxt.Text,
                Direccion = DireccionTxt.Text,
                Telefono = TelefonoTxt.Text,
                Fecha = FechaTextBox.Text.ToDatetime(),
                EmpresaId = Empresa.EmpresaID,
                UsuarioId = Usuario.UsuarioId
            };
            return factoria;
        }
        public void LlenarCampos(Factoria factoria)
        {
            Limpiar();
            FactoriaIdTxt.Text = factoria.FactoriaId.ToString();
            DireccionTxt.Text = factoria.Direccion;
            FechaTextBox.Text = factoria.Fecha.ToFormatDate();
            TelefonoTxt.Text = factoria.Telefono.ToString();
            NombresTxt.Text = factoria.Nombre;
        }

        protected void NuevoButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
        protected void GuadarButton_Click(object sender, EventArgs e)
        { 
            bool paso = false;
            RepositorioBase<Factoria> repositorio = new RepositorioBase<Factoria>();
            Factoria factoria = LLenaClase();
            TipoTitulo tipoTitulo = TipoTitulo.OperacionFallida;
            TiposMensajes tiposMensajes = TiposMensajes.RegistroNoGuardado;
            IconType iconType = IconType.error;

            if (factoria.FactoriaId == 0)
                paso = repositorio.Guardar(factoria);
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    Utils.ToastSweet(this, IconType.info, TiposMensajes.RegistroNoEncontrado);
                    return;
                }
                paso = repositorio.Modificar(factoria);
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
            RepositorioBase<Factoria> repositorio = new RepositorioBase<Factoria>();
            Factoria factoria = repositorio.Buscar(FactoriaIdTxt.Text.ToInt());
            repositorio.Dispose();
            return !factoria.EsNulo() && PerteneceALaEmpresa(factoria.EmpresaId);
        }
        protected void EliminarButton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Factoria> repositorio = new RepositorioBase<Factoria>();
            int id = FactoriaIdTxt.Text.ToInt();
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
        }
        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Factoria> repositorio = new RepositorioBase<Factoria>();
            int Id = FactoriaIdTxt.Text.ToInt();
            if (Id != 0)
            {
                Factoria factoria= repositorio.Buscar(Id);

                if (!factoria.EsNulo() && PerteneceALaEmpresa(factoria.FactoriaId))
                {
                    Limpiar();
                    LlenarCampos(factoria);
                }
                else
                    Utils.ToastSweet(this, IconType.info, TiposMensajes.RegistroNoEncontrado);
            }
            else
                Utils.ToastSweet(this, IconType.info, TiposMensajes.RegistroNoEncontrado);
        }
        public bool PerteneceALaEmpresa(int id)
        {
            RepositorioBase<Factoria> repositorio = new RepositorioBase<Factoria>();
            Factoria factoria = repositorio.Buscar(id);
            if (factoria.EsNulo())
                return false;
            return factoria.EmpresaId == Empresa.EmpresaID;
        }
    }
}