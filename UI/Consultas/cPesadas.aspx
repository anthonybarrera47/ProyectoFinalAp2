<%@ Page Title="Consulta de Pesadas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="cPesadas.aspx.cs" Inherits="ProyectoFinalAp2.UI.Consultas.cPesadas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
        Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
    <script type="text/javascript">
        function ShowReporte(idmodal, title) {
            $("#" + idmodal + " .modal-title").html(title);
            $("#" + idmodal).modal("show");
        }
    </script>
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
                                            <asp:ListItem>FactoriaId</asp:ListItem>
                                            <asp:ListItem>Nombre</asp:ListItem>
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
                                                <asp:HyperLinkField HeaderText="Opciones" ControlStyle-CssClass="btn btn-light btn-sm"
                                                    DataNavigateUrlFields="PesadaId"
                                                    DataNavigateUrlFormatString="~/UI/Registros/rPesadas.aspx?PesadaId={0}" Text="<i class='far fa-edit' aria-hidden='true'></i>"></asp:HyperLinkField>
                                                <asp:TemplateField HeaderText="Opciones">
                                                    <ItemTemplate>
                                                        <asp:Button ID="VerDetalleButton" runat="server"
                                                            Text="Ver Detalle" CssClass="btn btn-info" OnClick="VerDetalleButton_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Pesada ID" DataField="PesadaId" />
                                                <asp:BoundField HeaderText="Fecha" DataField="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField HeaderText="ProductorId" DataField="ProductorId" Visible="false" />
                                                <asp:BoundField HeaderText="Productor" DataField="NombreProductor" />
                                                <asp:BoundField HeaderText="FactoriaId" DataField="FactoriaId" Visible="false" />
                                                <asp:BoundField HeaderText="Factoria" DataField="NombreFactoria" />
                                                <asp:BoundField HeaderText="Humedad" DataField="Humedad" DataFormatString="{0:0.00}%" />
                                                <asp:BoundField HeaderText="Sub-Total Kilos" DataField="SubTotalKiloGramos" DataFormatString="{0:0.00}" />
                                                <asp:BoundField HeaderText="Total de sacos" DataField="TotalSacos" DataFormatString="{0:0.00}" />
                                                <asp:BoundField HeaderText="Desc. X sacos " DataField="DescuentoXSacos" DataFormatString="{0:0.00}" />
                                                <asp:BoundField HeaderText="Total Kilos" DataField="TotalKiloGramos" DataFormatString="{0:0.00}" />
                                                <asp:BoundField HeaderText="Total Pagado" DataField="TotalPagar" DataFormatString="{0:0.00}" />
                                                <asp:BoundField HeaderText="UsuarioId" DataField="UsuarioId" Visible="false" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card-footer">
                            <div class="text-left">
                                <label for="ElegirReporte">Elegir: </label>
                                <asp:DropDownList ID="ElegirReporte" runat="server" CssClass="form-control custom-select">
                                    <asp:ListItem>Listado de pesadas</asp:ListItem>
                                    <asp:ListItem>Listado de pesadas Detallado</asp:ListItem>
                                </asp:DropDownList>
                                <div class="form-group" display: inline-block>
                                    <asp:Button Text="Imprimir" CssClass="btn btn-outline-info btn-lg" runat="server" ID="ImprimirButton" OnClick="ImprimirButton_Click" />
                                    <asp:Button Text="Exportar" CssClass="btn btn-outline-primary btn-lg" runat="server" ID="ExportarButton" OnClick="ExportarButton_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--Modal Para ver el detalle-->
            <div class="modal fade" id="ModalDetalle" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog ml-sm-auto" role="document">
                    <div class="modal-content bigModal">
                        <div class="modal-header">
                            <h5 class="modal-title"></h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <%--GRID--%>
                            <div class="row">
                                <div class="col-12">
                                    <div class="table table-responsive">
                                        <asp:GridView ID="DetalleDatosGridView"
                                            runat="server"
                                            CssClass="table table-condensed table-bordered table-striped table-hover"
                                            CellPadding="4" ForeColor="#333333"
                                            OnPageIndexChanging="DetalleDatosGridView_PageIndexChanging"
                                            AllowPaging="true" PageSize="6"
                                            GridLines="None" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:BoundField HeaderText="Detalle ID" DataField="DetalleId" Visible="false" />
                                                <asp:BoundField HeaderText="PesadasId" DataField="PesadasId" Visible="false" />
                                                <asp:BoundField HeaderText="TipoArrozId" DataField="TipoArrozId" Visible="false" />
                                                <asp:BoundField HeaderText="Descripcion" DataField="Descripcion" />
                                                <asp:BoundField HeaderText="Kilos" DataField="Kilos" DataFormatString="{0:0.00}" />
                                                <asp:BoundField HeaderText="CantidadDeSacos" DataField="CantidadDeSacos" DataFormatString="{0:0.00}" />
                                            </Columns>
                                            <AlternatingRowStyle BackColor="LightBlue" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <RowStyle BackColor="#EFF3FB" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="DatosGridView" />
            <asp:AsyncPostBackTrigger ControlID="BuscarButton" />
            <asp:AsyncPostBackTrigger ControlID="DetalleDatosGridView" />
            <asp:PostBackTrigger ControlID="ExportarButton" />
        </Triggers>
    </asp:UpdatePanel>
    <div class="modal fade" id="ModalReporte" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content bigModal">
                <div class="modal-header">
                    <h5 class="modal-title" id="ModalLebel"></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <%--Viewer--%>
                    <rsweb:ReportViewer ID="Reportviewer" runat="server" ProcessingMode="Remote" Height="100%" Width="100%">
                    </rsweb:ReportViewer>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
