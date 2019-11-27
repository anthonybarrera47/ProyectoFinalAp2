<%@ Page Title="Registro de Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="rUsuarios.aspx.cs" Inherits="ProyectoFinalAp2.UI.Registros.rUsuarios" %>

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
                                        <label for="UsuarioIdTxt">Usuario ID</label>
                                        <div class="input-group">
                                            <asp:TextBox runat="server" ID="UsuarioIdTxt" TextMode="Number" placeholder="Usuario ID" CssClass="form-control "></asp:TextBox>
                                            <div class="input-group-append">
                                                <asp:LinkButton runat="server" ID="BuscarButton" CssClass="btn btn-info" OnClick="BuscarButton_Click">
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
                                        <label for="UserNameTxt">Usuario</label>
                                        <asp:TextBox runat="server" ID="UserNameTxt" placeholder="Nombre de usuario" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RFVUser" runat="server"
                                            ControlToValidate="UserNameTxt" ValidationGroup="GuardarVG"
                                            Display="Dynamic" SetFocusOnError="true"
                                            ForeColor="Red" ToolTip="Campo Contraseña Obligatorio"
                                            ErrorMessage="Este campo no puede estar vacio">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12 col-md-5">
                                    <div class="form-group">
                                        <label for="ClaveTxt">Contrase&ntilde;a</label>
                                        <asp:TextBox runat="server" ID="ClaveTxt" TextMode="Password" placeholder="Contraseña" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RFVClave" runat="server"
                                            ControlToValidate="ClaveTxt" ValidationGroup="GuardarVG"
                                            Display="Dynamic" SetFocusOnError="true"
                                            ForeColor="Red" ToolTip="Campo Contraseña Obligatorio"
                                            ErrorMessage="Este campo no puede estar vacio">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-12 col-md-5">
                                    <div class="form-group">
                                        <label for="ConfClaveTxt">Confirmar Contrase&ntilde;a</label>
                                        <asp:TextBox runat="server" ID="ConfClaveTxt" TextMode="Password" placeholder="Contraseña" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                            ControlToValidate="ClaveTxt" ValidationGroup="GuardarVG"
                                            Display="Dynamic" SetFocusOnError="true"
                                            ForeColor="Red" ToolTip="Campo Contraseña Obligatorio"
                                            ErrorMessage="Este campo no puede estar vacio">
                                        </asp:RequiredFieldValidator>
                                        <asp:CompareValidator runat="server" ID="CVPassword"
                                            ErrorMessage="Contrase&ntilde;a no Coinciden" ControlToValidate="ClaveTxt" ControlToCompare="ConfClaveTxt"
                                            CssClass="bg-danger text-white" Display="Dynamic" Type="String" Operator="Equal" ValidationGroup="GuardarVG">
                                        </asp:CompareValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group d-inline-flex">
                                <label class="label-custom">Tipo de usuario</label>
                                <div class="i-checks" runat="server">
                                    <input id="radioCustom1" type="radio" value="1" name="TipoUsuario" class="form-control-custom radio-custom">
                                    <label for="radioCustom1">Normal</label>
                                </div>
                                <div class="i-checks" runat="server">
                                    <input id="radioCustom2" type="radio" value="0" name="TipoUsuario" class="form-control-custom radio-custom">
                                    <label for="radioCustom2">Administrador</label>
                                </div>
                            </div>
                        </div>
                        <div class="card-footer">
                            <div class="text-center">
                                <div class="form-group" display: inline-block>
                                    <asp:Button Text="Nuevo" CssClass="btn btn-warning btn-lg" runat="server" ID="NuevoButton" OnClick="NuevoButton_Click" />
                                    <asp:Button Text="Guardar" CssClass="btn btn-success btn-lg" runat="server" ID="GuadarButton" OnClick="GuadarButton_Click" ValidationGroup="GuardarVG" />
                                    <asp:Button Text="Eliminar" CssClass="btn btn-danger btn-lg" runat="server" ID="EliminarButton" OnClick="EliminarButton_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="card">
                        <div class="card-header ">
                            <strong>Informacion Adicional</strong>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-12">
                                    <div class="form-group">
                                        <label for="NombreCompletoTxt">Nombre Completo</label>
                                        <asp:TextBox runat="server" ID="NombreCompletoTxt" placeholder="Nombre y Apellido" CssClass="form-control "></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12">
                                    <div class="form-group">
                                        <label for="EmailTextBox">Correo</label>
                                        <asp:TextBox runat="server" ID="EmailTextBox" TextMode="Email" CssClass="form-control "></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12">
                                    <div class="form-group">
                                        <label for="DireccionTextBox">Direccion</label>
                                        <asp:TextBox runat="server" ID="DireccionTextBox" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12">
                                    <div class="form-group">
                                        <label for="FechaNacimientoTxt">Fecha Nacimientos</label>
                                        <asp:TextBox runat="server" ID="FechaNacimientoTxt" TextMode="Date" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="card-footer">
                        <div class="text-center">
                            <div class="form-group" display: inline-block>
                                <asp:Button Text="Nuevo" CssClass="btn btn-warning btn-lg" runat="server" ID="Button1" OnClick="NuevoButton_Click" />
                                <asp:Button Text="Guardar" CssClass="btn btn-success btn-lg" runat="server" ID="Button2" OnClick="GuadarButton_Click" ValidationGroup="GuardarVG" />
                                <asp:Button Text="Eliminar" CssClass="btn btn-danger btn-lg" runat="server" ID="Button3" OnClick="EliminarButton_Click" />
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
