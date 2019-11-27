<%@ Page Title="Consulta de solicitudes de usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="cConsultaSolicitudes.aspx.cs" Inherits="ProyectoFinalAp2.UI.Consultas.cConsultaSolicitudes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-10">
                    <div class="card">
                        <div class="card-header ">
                            <strong><%:Page.Title %></strong>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-sm-5 col-md-5">
                                    <div class="form-group">
                                        <label for="BuscarPorDropDownList">Filtrar</label>
                                        <asp:DropDownList ID="BuscarPorDropDownList" runat="server" CssClass="form-control custom-select">
                                            <asp:ListItem>Todos</asp:ListItem>
                                            <asp:ListItem>Autorizadas</asp:ListItem>
                                            <asp:ListItem>Pendiente</asp:ListItem>
                                            <asp:ListItem>Denegado</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-7 col-md-7 ">
                                    <div class="form-group">
                                        <label for="CriterioTextBox">Criterio</label>
                                        <div class="input-group">
                                            <asp:TextBox runat="server" ID="CriterioTextBox" placeHolder="Criterio de Busqueda" CssClass="form-control"></asp:TextBox>
                                            <div class="input-group-append">
                                                <asp:LinkButton runat="server" ID="BuscarButton" OnClick="BuscarButton_Click" CssClass="btn btn-info">
                                         <i class="fas fa-search" aria-hidden="true"></i>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="i-checks" runat="server">
                                    <input id="FiltrarPorFechaCB" type="checkbox" name="FiltraFecha" checked="checked" value="1" class="form-control-custom">
                                    <label for="FiltrarPorFechaCB"><strong>Filtrar por Fecha</strong></label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12 col-md-5">
                                    <div class="form-group">
                                        <label for="FechaDesdeTxt">Desde</label>
                                        <asp:TextBox runat="server" ID="FechaDesdeTextBox" TextMode="Date" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-12 col-md-5">
                                    <div class="form-group">
                                        <label for="FechaHastaTxt">Hasta</label>
                                        <asp:TextBox runat="server" ID="FechaHastaTextBox" TextMode="Date" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <%--GRID--%>

                            <div class="row">
                                <div class="col-12">
                                    <div class="table table-responsive">
                                        <asp:GridView ID="DatosGridView"
                                            runat="server"
                                            CssClass="table table-condensed table-bordered table-striped table-hover"
                                            CellPadding="4" ForeColor="#333333"
                                            OnPageIndexChanging="DatosGridView_PageIndexChanging"
                                            AllowPaging="true" PageSize="6" AutoGenerateColumns="false"
                                            GridLines="None">

                                            <AlternatingRowStyle BackColor="SkyBlue" />

                                            <HeaderStyle BackColor="#4DB6AC" Font-Bold="True" ForeColor="Black" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <Columns>
                                                <asp:BoundField HeaderText="SolicitudId" DataField="SolicitudId" Visible="false" />
                                                <asp:BoundField HeaderText="Fecha" DataField="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField HeaderText="Usuario" DataField="NombreUsuario" />
                                                <asp:BoundField HeaderText="Estado" DataField="Estado" />
                                                <asp:BoundField HeaderText="EmpresaId" DataField="EmpresaId" Visible="false" />
                                                <asp:TemplateField ShowHeader="true" HeaderText="Acción">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="AprobarButton" runat="server"
                                                            CssClass="btn btn-primary btn-sm" OnClick="AprobarButton_Click">
                                                            <i class="fas fa-check-circle" aria-hidden="true"></i>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="DenegarButton" runat="server"
                                                            CssClass="btn btn-danger btn-sm" OnClick="DenegarButton_Click">
                                                            <i class="fas fa-ban" aria-hidden="true"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>

                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card-footer">
                            <div class="text-left">
                                <div class="form-group" display: inline-block>
                                    <asp:Button Text="Imprimir" CssClass="btn btn-outline-info btn-lg" runat="server" ID="ImprimirButton" />
                                    <asp:Button Text="Exportar" CssClass="btn btn-outline-primary btn-lg" runat="server" ID="ExportarButton" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="DatosGridView" />
            <asp:AsyncPostBackTrigger ControlID="BuscarButton" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
