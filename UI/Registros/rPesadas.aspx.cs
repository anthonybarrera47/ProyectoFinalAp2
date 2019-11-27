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
    public partial class rPesadas : System.Web.UI.Page
    {
        Empresas Empresa = new Empresas();
        Usuarios Usuario = new Usuarios();
        readonly string KeyViewState = "Pesadas";
        protected void Page_Load(object sender, EventArgs e)
        {
            Empresa = (Session["Empresas"] as Entidades.Empresas);
            Usuario = (Session["Usuario"] as Entidades.Usuarios);
            if (!IsPostBack)
            {
                FechaTxt.Text = DateTime.Now.ToFormatDate();
                ViewState[KeyViewState] = new Pesadas();
                LlenarCombos();
                int id = Request.QueryString["PesadaId"].ToInt();
                if (id > 0)
                {
                    var pesadas = new RepositorioPesadas().Buscar(id);
                    if (pesadas.EsNulo() || PerteneceALaEmpresa(pesadas.EmpresaId))
                        Utils.Alerta(this, TipoTitulo.Informacion, TiposMensajes.RegistroNoEncontrado, IconType.info);
                    else
                    {
                        ViewState[KeyViewState] = pesadas;
                        LlenarCampos();
                    }
                }

            }
        }
        private bool Validar()
        {
            Pesadas pesadas = ViewStateToEntity(ViewState[KeyViewState]);
            bool paso = Page.IsValid;
            if (pesadas.Detalles.Count < 1)
                paso = false;
            if (pesadas.FactoriaId < 1)
                paso = false;
            if (pesadas.ProductorId < 1)
                paso = false;
            if (pesadas.Humedad < 1)
                paso = false;
            if (pesadas.PrecioFanega < 1)
                paso = false;
            if (pesadas.TotalPagar < 1)
                paso = false;
            
            return paso;
        }
        private void LlenarCampos()
        {
            Pesadas pesadas = ViewStateToEntity(ViewState[KeyViewState]);
            PesadaIdTxt.Text = pesadas.PesadaId.ToString();
            ProductorIdTextBox.Text = pesadas.ProductorId.ToString();
            FactoriaIdTextBox.Text = pesadas.FactoriaId.ToString();
            SubTotalKGTextBox.Text = pesadas.SubTotalKiloGramos.ToString();
            TotalSacosTextBox.Text = pesadas.TotalSacos.ToString();
            PrecioFanegaTextBox.Text = pesadas.PrecioFanega.ToString();
            HumedadTextBox.Text = pesadas.Humedad.ToString();
            DescuentoXSacosTextBox.Text = pesadas.DescuentoXSacos.ToString();
            TotalKgTextBox.Text = pesadas.TotalKiloGramos.ToString();
            FanegaTextBox.Text = pesadas.Fanega.ToString();
            TotalPagarTextBox.Text = pesadas.TotalPagar.ToString();
            BuscarProductorButton_Click(null, null);
            BuscarFactoriaButton_Click(null, null);
            this.Bindgrid();
        }
        private void LlenarCombos()
        {
            RepositorioBase<TipoArroz> repositorio = new RepositorioBase<TipoArroz>();
            repositorio.LlenarCombo(TipoArrozDropDown, "TipoArrozId", "Descripcion");
        }
        private Pesadas LlenaClase()
        {
            Pesadas pesadas = ViewStateToEntity(ViewState[KeyViewState]);
            pesadas.PesadaId = PesadaIdTxt.Text.ToInt();
            pesadas.ProductorId = ProductorIdTextBox.Text.ToInt();
            pesadas.FactoriaId = FactoriaIdTextBox.Text.ToInt();
            pesadas.SubTotalKiloGramos = SubTotalKGTextBox.Text.ToDecimal();
            pesadas.TotalSacos = TotalSacosTextBox.Text.ToDecimal();
            pesadas.PrecioFanega = PrecioFanegaTextBox.Text.ToDecimal();
            pesadas.Humedad = HumedadTextBox.Text.ToDecimal();
            pesadas.DescuentoXSacos = DescuentoXSacosTextBox.Text.ToDecimal();
            pesadas.TotalKiloGramos = TotalKgTextBox.Text.ToDecimal();
            pesadas.Fanega = FanegaTextBox.Text.ToDecimal();
            pesadas.TotalPagar = TotalPagarTextBox.Text.ToDecimal();
            pesadas.Fecha = FechaTxt.Text.ToDatetime();
            pesadas.UsuarioId = 2;
            return pesadas;
        }
        private void Limpiar()
        {
            PesadaIdTxt.Text = "0";
            ProductorIdTextBox.Text = "0";
            NombreProductorTextBox.Text = string.Empty;
            FactoriaIdTextBox.Text = "0";
            DescripcionFactoriaTextBox.Text = string.Empty;
            SubTotalKGTextBox.Text = "0";
            TotalSacosTextBox.Text = "0";
            PrecioFanegaTextBox.Text = "0";
            HumedadTextBox.Text = "0";
            DescuentoXSacosTextBox.Text = "0";
            TotalKgTextBox.Text = "0";
            FanegaTextBox.Text = "0";
            TotalPagarTextBox.Text = "0";
            PrecioFanegaTextBox.Text = "0";
            FechaTxt.Text = DateTime.Now.ToFormatDate();
            ViewState[KeyViewState] = new Pesadas();
            this.Bindgrid();
        }
        private void Bindgrid()
        {
            Pesadas pesadas = ViewStateToEntity(ViewState[KeyViewState]);
            DetalleGridView.DataSource = pesadas.Detalles;
            DetalleGridView.DataBind();
        }

        protected void AgregarNuevoTipoArroz_Click(object sender, EventArgs e)
        {

        }
        protected void RemoverDetalleClick_Click(object sender, EventArgs e)
        {
            Pesadas pesadas = ViewStateToEntity(ViewState[KeyViewState]);
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            pesadas.RemoverDetalle(row.RowIndex);
            ViewState[KeyViewState] = pesadas;
            this.Bindgrid();
            Totalizar();
        }
        protected void AgregarPesadaButton_Click(object sender, EventArgs e)
        {
            Pesadas pesadas = ViewStateToEntity(ViewState[KeyViewState]);

            pesadas.AgregarDetalle(0, pesadas.PesadaId, GetValueDropDownListTipoArroz(), GetDescripcionTipoArroz(), KilosTextBox.Text.ToDecimal(), CantidadSacosTextBox.Text.ToDecimal());
            ViewState[KeyViewState] = pesadas;
            this.Bindgrid();
            Totalizar();
            CantidadSacosTextBox.Text = string.Empty;
            KilosTextBox.Text = string.Empty;
            CantidadSacosTextBox.Focus();
        }

        protected void DetalleGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Pesadas Pesada = ViewStateToEntity(ViewState[KeyViewState]);
            DetalleGridView.DataSource = Pesada.Detalles;
            DetalleGridView.PageIndex = e.NewPageIndex;
            DetalleGridView.DataBind();
        }
        protected void NuevoButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
        protected void GuadarButton_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                bool paso = false;
                RepositorioPesadas repositorio = new RepositorioPesadas();
                Pesadas pesadas = LlenaClase();
                TipoTitulo tipoTitulo = TipoTitulo.OperacionFallida;
                TiposMensajes tiposMensajes = TiposMensajes.RegistroNoGuardado;
                IconType iconType = IconType.error;

                if (pesadas.PesadaId == 0)
                    paso = repositorio.Guardar(pesadas);
                else
                {
                    if (!ExisteEnLaBaseDeDatos())
                    {
                        Utils.ToastSweet(this, IconType.info, TiposMensajes.RegistroNoEncontrado);
                        return;
                    }
                    paso = repositorio.Modificar(pesadas);
                }

                if (paso)
                {
                    Limpiar();
                    tipoTitulo = TipoTitulo.OperacionExitosa;
                    tiposMensajes = TiposMensajes.RegistroGuardado;
                    iconType = IconType.success;
                }
                int id = pesadas.PesadaId;
                Utils.Alerta(this, tipoTitulo, tiposMensajes, iconType);
            }
            else
                Utils.Alerta(this, TipoTitulo.OperacionFallida, TiposMensajes.RevisarCampos, IconType.error);

        }
        private bool ExisteEnLaBaseDeDatos()
        {
            RepositorioPesadas repositorio = new RepositorioPesadas();
            return repositorio.ExisteEnLaBaseDeDatos(PesadaIdTxt.Text.ToInt());
        }
        protected void EliminarButton_Click(object sender, EventArgs e)
        {
            RepositorioPesadas repositorio = new RepositorioPesadas();
            int id = PesadaIdTxt.Text.ToInt();
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
            RepositorioPesadas repositorio = new RepositorioPesadas();
            int Id = PesadaIdTxt.Text.ToInt();
            if (Id > 0)
            {
                Pesadas Pesadas = repositorio.Buscar(Id);
                if (!Pesadas.EsNulo())
                {
                    //Limpiar();
                    ViewState[KeyViewState] = new Pesadas();
                    ViewState[KeyViewState] = Pesadas;
                    LlenarCampos();
                }
                else
                    Utils.ToastSweet(this, IconType.info, TiposMensajes.RegistroNoEncontrado);
            }
            else
                Utils.ToastSweet(this, IconType.info, TiposMensajes.RegistroNoEncontrado);
        }
        private int GetValueDropDownListTipoArroz()
        {
            return TipoArrozDropDown.SelectedValue.ToInt();
        }
        private string GetDescripcionTipoArroz()
        {
            RepositorioBase<TipoArroz> repositorio = new RepositorioBase<TipoArroz>();
            return repositorio.Buscar(GetValueDropDownListTipoArroz()).Descripcion;
        }
        protected void BuscarProductorButton_Click(object sender, EventArgs e)
        {
            BuscarProductores();
        }
        protected void BuscarFactoriaButton_Click(object sender, EventArgs e)
        {
            BuscarFactoria();
        }
        private void Totalizar()
        {
            decimal SubTotalKG = 0, TotalKG = 0, Nega = 0, CantSacos = 0, DescuentoXSaco = 0, PrecioFanega = 0, Humedad = 0, TotalAPagar = 0;
            Pesadas pesadas = ViewStateToEntity(ViewState[KeyViewState]);

            PrecioFanega = (PrecioFanegaTextBox.Text).ToDecimal();
            Humedad = HumedadTextBox.Text.ToDecimal();
            if (Humedad == 0)
                return;
            DescuentoXSaco = DescuentoXSacosTextBox.Text.ToDecimal();
            foreach (var item in pesadas.Detalles)
            {
                SubTotalKG += item.Kilos;
                CantSacos += item.CantidadDeSacos;
            }
            TotalKG = SubTotalKG - (CantSacos * DescuentoXSaco);
            Nega = (TotalKG / Humedad);
            TotalAPagar = Nega * PrecioFanega;

            SubTotalKGTextBox.Text = SubTotalKG.ToString("N2");
            TotalSacosTextBox.Text = CantSacos.ToString("N2");
            TotalKgTextBox.Text = TotalKG.ToString("N2");
            FanegaTextBox.Text = Nega.ToString("N2");
            TotalPagarTextBox.Text = TotalAPagar.ToString("N2");
        }
        private Pesadas ViewStateToEntity(object obj)
        {
            return (obj as Pesadas);
        }
        protected void cusCustom_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = args.Value.ToDecimal() > 0;
        }
        protected void CustomFactoria_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = BuscarFactoria();
        }
        private bool BuscarFactoria()
        {
            RepositorioBase<Factoria> repositorio = new RepositorioBase<Factoria>();
            Factoria Factoria = repositorio.Buscar(FactoriaIdTextBox.Text.ToInt());
            if (!Factoria.EsNulo())
                DescripcionFactoriaTextBox.Text = Factoria.Nombre;
            else
            {
                DescripcionFactoriaTextBox.Text = string.Empty;
                Utils.ToastSweet(this, IconType.info, TiposMensajes.RegistroNoEncontrado);
            }

            return !Factoria.EsNulo();
        }
        private bool BuscarProductores()
        {
            RepositorioBase<Productores> repositorio = new RepositorioBase<Productores>();
            Productores productores = repositorio.Buscar(ProductorIdTextBox.Text.ToInt());
            if (!productores.EsNulo())
                NombreProductorTextBox.Text = productores.Nombre;
            else
            {
                NombreProductorTextBox.Text = string.Empty;
                Utils.ToastSweet(this, IconType.info, TiposMensajes.RegistroNoEncontrado);
            }

            return !productores.EsNulo();
        }
        protected void ProductorCV_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = BuscarProductores();
        }
        public bool PerteneceALaEmpresa(int id)
        {
            RepositorioPesadas repositorio = new RepositorioPesadas();
            Pesadas Pesadas = repositorio.Buscar(id);
            return Pesadas.EmpresaId == Empresa.EmpresaID;
        }
    }
}