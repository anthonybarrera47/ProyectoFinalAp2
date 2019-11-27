<%@ Page Title="Registro de Factorias"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="rFactoria.aspx.cs"
    Inherits="ProyectoFinalAp2.UI.Registros.rFactoria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManger" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-6">
                    <div class="card">
                        <div class="card-header ">
                            <strong><%:Page.Title %></strong>
                        </div>
                        <div class="card-body">

                            <div class="row">
                                <div class="col-12 col-md-5">
                                    <div class="form-group">
                                        <label for="FactoriaIdTxt">Factoria ID</label>
                                        <div class="input-group">
                                            <asp:TextBox runat="server" ID="FactoriaIdTxt" TextMode="Number" placeholder="Factoria ID" CssClass="form-control "></asp:TextBox>
                                            <div class="input-group-append">
                                                <asp:LinkButton runat="server" ID="BuscarButton" OnClick="BuscarButton_Click" CssClass="btn btn-info">
                                         <i class="fas fa-search" aria-hidden="true"></i>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 col-md-5">
                                    <div class="form-group">
                                        <label for="FechaTxt">Fecha</label>
                                        <asp:TextBox runat="server" ID="FechaTextBox" TextMode="Date" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12 col-md-5">
                                    <div class="form-group">
                                        <label for="NombresTxt">Nombre</label>
                                        <asp:TextBox runat="server" ID="NombresTxt" placeholder="Nombre de la Factoria" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-12 col-md-5">
                                    <div class="form-group">
                                        <label for="TelefonoTxt">Telefono</label>
                                        <asp:TextBox runat="server" ID="TelefonoTxt" TextMode="Phone" placeholder="Telefono o Celular" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-12 col-md-10">
                                    <div class="form-group">
                                        <label for="DireccionTxt">Direccion</label>
                                        <asp:TextBox runat="server" ID="DireccionTxt" placeholder="Direccion de la factoria" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="card-footer">
                            <div class="text-center">
                                <div class="form-group" display: inline-block>
                                    <asp:Button Text="Nuevo" CssClass="btn btn-warning btn-lg" runat="server" ID="NuevoButton" OnClick="NuevoButton_Click" />
                                    <asp:Button Text="Guardar" CssClass="btn btn-success btn-lg" runat="server" ID="GuadarButton" OnClick="GuadarButton_Click" />
                                    <asp:Button Text="Eliminar" CssClass="btn btn-danger btn-lg" runat="server" ID="EliminarButton" OnClick="EliminarButton_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="BuscarButton" />
            <asp:AsyncPostBackTrigger ControlID="NuevoButton" />
            <asp:AsyncPostBackTrigger ControlID="GuadarButton" />
            <asp:AsyncPostBackTrigger ControlID="EliminarButton" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
