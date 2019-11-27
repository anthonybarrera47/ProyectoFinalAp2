﻿using BLL;
using Entidades;
using Extensores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoFinalAp2.UI.Consultas
{
    public partial class cProductores : System.Web.UI.Page
    {
        static List<Productores> lista = new List<Productores>();
        Empresas Empresa = new Empresas();
        protected void Page_Load(object sender, EventArgs e)
        {
            Empresa = (Session["Empresas"] as Entidades.Empresas);
            if (!Page.IsPostBack)
            {
                FechaDesdeTextBox.Text = DateTime.Now.ToFormatDate();
                FechaHastaTextBox.Text = DateTime.Now.ToFormatDate();
            }
        }
        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            Expression<Func<Productores, bool>> filtro = x => true;
            RepositorioBase<Productores> repositorio = new RepositorioBase<Productores>();
            switch (BuscarPorDropDownList.SelectedIndex)
            {
                case 0:
                    filtro = x => true;
                    break;
                case 1:
                    filtro = x => x.Nombre.Contains(CriterioTextBox.Text);
                    break;
                case 2:
                    filtro = x => x.Cedula.Contains(CriterioTextBox.Text);
                    break;
            }
            DateTime fechaDesde = FechaDesdeTextBox.Text.ToDatetime();
            DateTime FechaHasta = FechaHastaTextBox.Text.ToDatetime();

            if (Request.Form["FiltraFecha"] != null)
                lista = repositorio.GetList(filtro).Where(x => x.Fecha >= fechaDesde && x.Fecha <= FechaHasta).ToList();
            else
                lista = repositorio.GetList(filtro).ToList();
            repositorio.Dispose();
            this.BindGrid(lista.Where(x=>x.EmpresaId==Empresa.EmpresaID).ToList());
        }
        protected void DatosGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DatosGridView.DataSource = lista;
            DatosGridView.PageIndex = e.NewPageIndex;
            DatosGridView.DataBind();
        }
        private void BindGrid(List<Productores> lista)
        {
            DatosGridView.DataSource = null;
            DatosGridView.DataSource = lista;
            DatosGridView.DataBind();
        }
    }
}