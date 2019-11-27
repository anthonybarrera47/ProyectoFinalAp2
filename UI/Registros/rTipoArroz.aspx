<%@ Page Title="Registro de Tipos de Arroz"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="rTipoArroz.aspx.cs"
    Inherits="ProyectoFinalAp2.UI.Registros.rTipoArroz" %>

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
                                        <label for="TipoArrozIdTxt">TipoArroz ID</label>
                                        <div class="input-group">
                                            <asp:TextBox runat="server" ID="TipoArrozIdTxt" TextMode="Number" placeholder="Tipo Arroz ID" CssClass="form-control "></asp:TextBox>
                                            <div class="input-group-append">
                                                <asp:LinkButton runat="server" ID="BuscarButton" OnClick="BuscarButton_Click" class="btn btn-info">
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
                                        <label for="DescripcionTxt">Descripción</label>
                                        <asp:TextBox runat="server" ID="DescripcionTxt" placeholder="Descripcion" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RFVDescripcion" runat="server"
                                            ControlToValidate="DescripcionTxt" ValidationGroup="GuardarVG"
                                            Display="Dynamic" SetFocusOnError="true"
                                            ForeColor="Red" ToolTip="Campo Descripcion Obligatorio"
                                            ErrorMessage="Este campo no puede estar vacio" >
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-12 col-md-5">
                                    <div class="form-group">
                                        <label for="KilosTxt">Kilos</label>
                                        <asp:TextBox runat="server" ID="KilosTxt" TextMode="Number" placeholder="Kilos" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card-footer">
                            <div class="text-center">
                                <div class="form-group" display: inline-block>
                                    <asp:Button Text="Nuevo" CssClass="btn btn-warning btn-lg" runat="server" ID="NuevoButton" OnClick="NuevoButton_Click"/>
                                    <asp:Button Text="Guardar" CssClass="btn btn-success btn-lg" runat="server" ID="GuadarButton" OnClick="GuadarButton_Click" ValidationGroup="GuardarVG" />
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
