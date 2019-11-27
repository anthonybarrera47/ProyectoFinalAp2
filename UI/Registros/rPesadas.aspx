<%@ Page Title="Registro de Pesadas"
    Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="rPesadas.aspx.cs" Inherits="ProyectoFinalAp2.UI.Registros.rPesadas" %>

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
                            <strong><%: Page.Title %></strong>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-12 col-md-5">
                                    <div class="form-group">
                                        <label for="PesadaIdTxt">Pesada ID</label>
                                        <div class="input-group">
                                            <asp:TextBox runat="server" ID="PesadaIdTxt" TextMode="Number" placeholder="Pesada ID" CssClass="form-control "></asp:TextBox>
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
                                        <asp:TextBox runat="server" ID="FechaTxt" TextMode="Date" CssClass="form-control"></asp:TextBox>
                                    </div>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12 col-md-5">
                                    <div class="form-group">
                                        <label for="ProductorIdTextBox">Productor ID</label>
                                        <div class="input-group">
                                            <asp:TextBox runat="server" ID="ProductorIdTextBox" TextMode="Number" placeholder="Pesada ID" CssClass="form-control "></asp:TextBox>
                                            <div class="input-group-append">
                                                <asp:LinkButton runat="server" ID="BuscarProductorButton" OnClick="BuscarProductorButton_Click" class="btn btn-info">
                                         <i class="fas fa-search" aria-hidden="true"></i>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <asp:RequiredFieldValidator ID="RFVProductor" runat="server"
                                            ControlToValidate="ProductorIdTextBox" ValidationGroup="GuardarVG"
                                            Display="Dynamic" SetFocusOnError="true"
                                            ForeColor="Red" ToolTip="Campo Descripcion Obligatorio"
                                            ErrorMessage="Este campo no puede estar vacio">
                                        </asp:RequiredFieldValidator>
                                        <asp:CustomValidator runat="server" ID="ProductorCV" ControlToValidate="ProductorIdTextBox" OnServerValidate="ProductorCV_ServerValidate"
                                            ErrorMessage="Este productor no existe" CssClass="bg-danger text-white" Display="Dynamic" ValidationGroup="GuardarVG" />
                                    </div>
                                </div>
                                <div class="col-12 col-md-5">
                                    <div class="form-group">
                                        <label for="NombreProductorTextBox">Nombre</label>
                                        <asp:TextBox runat="server" ID="NombreProductorTextBox" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12 col-md-5">
                                    <div class="form-group">
                                        <label for="FactoriaIdTextBox">Factoria ID</label>
                                        <div class="input-group">
                                            <asp:TextBox runat="server" ID="FactoriaIdTextBox" TextMode="Number" placeholder="Pesada ID" CssClass="form-control "></asp:TextBox>
                                            <div class="input-group-append">
                                                <asp:LinkButton runat="server" ID="BuscarFactoriaButton" OnClick="BuscarFactoriaButton_Click" class="btn btn-info">
                                         <i class="fas fa-search" aria-hidden="true"></i>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <asp:RequiredFieldValidator ID="RFVFactoria" runat="server"
                                            ControlToValidate="FactoriaIdTextBox" ValidationGroup="GuardarVG"
                                            Display="Dynamic" SetFocusOnError="true"
                                            ForeColor="Red" ToolTip="Campo Descripcion Obligatorio"
                                            ErrorMessage="Este campo no puede estar vacio">
                                        </asp:RequiredFieldValidator>

                                        <asp:CustomValidator runat="server" ID="CustomFactoria" ControlToValidate="FactoriaIdTextBox" OnServerValidate="CustomFactoria_ServerValidate"
                                            ErrorMessage="Esta factoria no existe" CssClass="bg-danger text-white" Display="Dynamic" ValidationGroup="GuardarVG" />
                                    </div>
                                </div>
                                <div class="col-12 col-md-5">
                                    <div class="form-group">
                                        <label for="DescripcionFactoriaTextBox">Descripcion</label>
                                        <asp:TextBox runat="server" ID="DescripcionFactoriaTextBox" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12 col-md-5">
                                    <div class="form-group">
                                        <label for="SubTotalKGTextBox">Sub-Total de Kilos</label>
                                        <asp:TextBox runat="server" ID="SubTotalKGTextBox" placeholder="Kilos sin descuentos" ReadOnly="true" CssClass="form-control "></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-12 col-md-5">
                                    <div class="form-group">
                                        <label for="TotalSacosTextBox">Total de sacos</label>
                                        <asp:TextBox runat="server" ID="TotalSacosTextBox" placeholder="Total de sacos" ReadOnly="true" CssClass="form-control "></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-12 col-md-5">
                                    <div class="form-group">
                                        <label for="PrecioFanegaTextBox">Precio Fanega</label>
                                        <asp:TextBox runat="server" ID="PrecioFanegaTextBox" TextMode="Number" placeholder="Precio de la Fanega" CssClass="form-control "></asp:TextBox>
                                    </div>
                                    <asp:CustomValidator runat="server" ID="PrecioCustom" ControlToValidate="PrecioFanegaTextBox" OnServerValidate="cusCustom_ServerValidate"
                                        ErrorMessage="Este campo debe ser mayor a 0" CssClass="bg-danger text-white" Display="Dynamic" ValidationGroup="GuardarVG" />
                                </div>
                                <div class="col-12 col-md-5">
                                    <div class="form-group">
                                        <label for="HumedadTextBox">Humedad</label>
                                        <div class="input-group">
                                            <asp:TextBox runat="server" ID="HumedadTextBox" TextMode="Number" placeholder="Porcentaje de humedad" CssClass="form-control"></asp:TextBox>
                                            <div class="input-group-append">
                                                <span class="input-group-text">%</span>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:CustomValidator runat="server" ID="HumedaCustom" ControlToValidate="HumedadTextBox" OnServerValidate="cusCustom_ServerValidate"
                                        ErrorMessage="Este campo debe ser mayor a 0" CssClass="bg-danger text-white" Display="Dynamic" ValidationGroup="GuardarVG" />

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12 col-md-5">
                                    <div class="form-group">
                                        <label for="DescuentoXSacos">Descuento X sacos</label>
                                        <div class="input-group">
                                            <asp:TextBox runat="server" ID="DescuentoXSacosTextBox" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                            <div class="input-group-append">
                                                <span class="input-group-text">KG</span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 col-md-5">
                                    <div class="form-group">
                                        <label for="TotalKgTextBox">Total de kilos</label>
                                        <asp:TextBox runat="server" ID="TotalKgTextBox" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12 col-md-5">
                                    <div class="form-group">
                                        <label for="FanegaTextBox">Fanega</label>
                                        <asp:TextBox runat="server" ID="FanegaTextBox" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-12 col-md-5">
                                    <div class="form-group">
                                        <label for="TotalPagarTextBox">Total a Pagar</label>
                                        <asp:TextBox runat="server" ID="TotalPagarTextBox" BackColor="LightGreen" Font-Bold="true" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="card">
                        <div class="card-header ">
                            <strong><%: "Información de las pesadas"%></strong>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-12 col-md-5">
                                    <div class="form-group">
                                        <label for="KilosTextBox">Tipo de Arroz</label>
                                        <div class="input-group">
                                            <asp:DropDownList runat="server" ID="TipoArrozDropDown" CssClass="custom-select"></asp:DropDownList>
                                            <div class="input-group-append">
                                                <button runat="server" id="AgregarNuevoTipoArroz" onserverclick="AgregarNuevoTipoArroz_Click" class="btn btn-info"><i class="fas fa-search" aria-hidden="true"></i></button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12 col-md-5">
                                    <div class="form-group">
                                        <label for="CantidadSacosTextBox">Cantidad de Sacos</label>
                                        <asp:TextBox runat="server" ID="CantidadSacosTextBox" TextMode="Number" placeholder="Cantidad de Sacos" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-12 col-md-5">
                                    <div class="form-group">
                                        <label for="KilosTextBox">Kilos</label>
                                        <div class="input-group">
                                            <asp:TextBox runat="server" ID="KilosTextBox" TextMode="Number" placeHolder="Kilos" CssClass="form-control"></asp:TextBox>
                                            <div class="input-group-append">
                                                <asp:LinkButton runat="server" ID="AgregarPesadaButton" Text="<i class='fas fa-plus-circle'></i>" OnClick="AgregarPesadaButton_Click" CssClass="btn btn-info" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="DetalleGridView"
                                            runat="server"
                                            CssClass="table table-condensed table-bordered table-striped table-hover"
                                            CellPadding="4" ForeColor="#333333" GridLines="None"
                                            AllowPaging="true" PageSize="5" AutoGenerateColumns="false"
                                            OnPageIndexChanging="DetalleGridView_PageIndexChanging">
                                            <AlternatingRowStyle BackColor="LightBlue" />
                                            <Columns>
                                                <asp:TemplateField ShowHeader="False" HeaderText="Opciones">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="RemoverDetalleClick" runat="server"
                                                            Text="Remover" class="btn btn-danger btn-sm" OnClick="RemoverDetalleClick_Click">
                                                            <i class="far fa-trash-alt" aria-hidden="true"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="DetalleId" DataField="DetalleId" Visible="false" />
                                                <asp:BoundField HeaderText="PesadasId" DataField="PesadasId" Visible="false" />
                                                <asp:BoundField HeaderText="TipoArrozId" DataField="TipoArrozId" Visible="false" />
                                                <asp:BoundField HeaderText="Tipo de arroz" DataField="Descripcion" />
                                                <asp:BoundField HeaderText="Cant. de sacos" DataField="CantidadDeSacos" DataFormatString="{0:0.00}" />
                                                <asp:BoundField HeaderText="Kilos" DataField="Kilos" DataFormatString="{0:0.00}" />
                                            </Columns>
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <RowStyle BackColor="#EFF3FB" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <footer class="blockquote-footer">
                <div class="text-center">
                    <div class="form-group" display: inline-block>
                        <asp:Button Text="Nuevo" CssClass="btn btn-warning btn-lg" runat="server" ID="NuevoButton" OnClick="NuevoButton_Click" />
                        <asp:Button Text="Guardar" CssClass="btn btn-success btn-lg" runat="server" ID="GuadarButton" OnClick="GuadarButton_Click" ValidationGroup="GuardarVG" />
                        <asp:Button Text="Eliminar" CssClass="btn btn-danger btn-lg" runat="server" ID="EliminarButton" OnClick="EliminarButton_Click" />
                    </div>
                </div>
            </footer>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="BuscarButton" />
            <asp:AsyncPostBackTrigger ControlID="NuevoButton" />
            <asp:AsyncPostBackTrigger ControlID="GuadarButton" />
            <asp:AsyncPostBackTrigger ControlID="EliminarButton" />
            <asp:AsyncPostBackTrigger ControlID="AgregarPesadaButton" />
            <asp:AsyncPostBackTrigger ControlID="ProductorIdTextBox" />
            <asp:AsyncPostBackTrigger ControlID="FactoriaIdTextBox" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
