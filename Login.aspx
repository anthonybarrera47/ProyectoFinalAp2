<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ProyectoFinalAp2.Login" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>Iniciar</title>
    <meta name="description" content="Iniciar Sesion">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="robots" content="all,follow">


    <!-- Bootstrap CSS-->
    <link rel="stylesheet" href="/Design/vendor/bootstrap/css/bootstrap.min.css">
    <!-- Font Awesome CSS-->
    <link rel="stylesheet" href="/Design/vendor/font-awesome/css/font-awesome.min.css">
    <!-- Fontastic Custom icon font-->
    <link rel="stylesheet" href="/Design/css/fontastic.css">
    <!-- Google fonts - Roboto -->
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700">
    <!-- jQuery Circle-->
    <link rel="stylesheet" href="/Design/css/grasp_mobile_progress_circle-1.0.0.min.css">
    <!-- Custom Scrollbar-->
    <link rel="stylesheet" href="/Design/vendor/malihu-custom-scrollbar-plugin/jquery.mCustomScrollbar.css">
    <!-- theme stylesheet-->
    <link rel="stylesheet" href="/Design/css/style.default.css" id="themestylesheet">
    <!-- Custom stylesheet - for your changes-->
    <link rel="stylesheet" href="/Design/css/custom.css">
    <!-- Favicon-->
    <link rel="shortcut icon" href="/Design/img/favicon.ico">

    <!--Alerta Sweet-->
    <script type="text/javascript" src="/Scripts/sweetalert.min.js"></script>
    <!--Alerta Sweet 2.0-->
    <script type="text/javascript" src="/Scripts/sweetalert2.all.min.js"></script>

    <style type="text/css">
        .btn {
            border-radius: 24px;
        }
    </style>
    <script type="text/javascript">
        function MostrarModal(title) {
            $("#ModalComoUsuario .modal-title").html(title);
            $("#ModalComoUsuario").modal("show");
        }
    </script>
    <script type="text/javascript">
        function MostrarModalEmpresa(title) {
            $("#ModalComoEmpresa .modal-title").html(title);
            $("#ModalComoEmpresa").modal("show");
        }
    </script>
    <script type="text/javascript">
        function MostrarModalRegistro(title) {
            $("#ModalRegistro .modal-title").html(title);
            $("#ModalRegistro").modal("show");
        }
    </script>
    <script type="text/javascript">
        function sweetalert(Title, Text, icon, Button) {
            Swal.fire({
                title: Title,
                text: Text,
                type: icon,
            });
        }
    </script>
    <script type="text/javascript">
        function ToastSweetAlert(Type, Titulo) {

            const Toast = Swal.mixin({
                toast: true,
                position: 'top-end',
                showConfirmButton: false,
                timer: 3000
            });
            Toast.fire({
                type: Type,
                title: Titulo
            });
        }
    </script>
</head>
<body>
    <form method="get" class="text-left form-validate" runat="server">
        <div class="row login-page">
            <div class="container-fluid">
                <div class="form-outer text-center d-flex align-items-center">
                    <div class="form-inner col-12">
                        <div class="card">
                            <div class="card-body">
                                <div class="logo text-uppercase"><strong class="text-primary">Inicio De Sesión</strong></div>
                                <p>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;</p>

                                <div class="row">
                                    <div class="col-12">
                                        <div class="form-group-material">
                                            <asp:TextBox ID="UserNameTextBox" runat="server" CssClass="input-material"></asp:TextBox>
                                            <label for="UserNameTextBox" class="label-material">Nombre de Usuario</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12">
                                        <div class="form-group-material">
                                            <asp:TextBox ID="PasswordTextBox" runat="server" CssClass="input-material" TextMode="Password"></asp:TextBox>
                                            <label for="PasswordTextBox" class="label-material">Contrase&ntilde;a</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12">
                                        <div class="form-group text-center">
                                            <asp:Button ID="LoginButton" runat="server" CssClass="btn btn-primary" Text="Iniciar Sesión" OnClick="LoginButton_Click" />
                                        </div>
                                    </div>
                                </div>
                                <!-- Modal para Registrarse como Usuario -->
                                <div class="modal fade" id="ModalComoUsuario" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                    <div class="modal-dialog ml-sm-auto" role="document">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <h5 class="modal-title" id="ComoUsuarioModal">Como usuario.....!!</h5>
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                    <span aria-hidden="true">&times;</span>
                                                </button>
                                            </div>
                                            <div class="modal-body text-left">
                                                <div class="row">
                                                    <%--UserName--%>
                                                    <div class="col-12 col-md-5">
                                                        <div class="form-group">
                                                            <label for="UserNameComoUserTxt">UserName</label>
                                                            <asp:TextBox runat="server" ID="UserNameComoUserTxt" placeholder="Nombre de usuario" CssClass="form-control "></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <%--CodigoEmpresa--%>
                                                    <div class="col-12 col-md-5">
                                                        <div class="form-group">
                                                            <label for="CodigoEmpresaTxt">Codigo de la empresa</label>
                                                            <asp:TextBox runat="server" ID="CodigoEmpresaTxt" placeholder="Codigo de la empresa" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <asp:CustomValidator runat="server" ID="CustomCodigoValidate" ControlToValidate="CodigoEmpresaTxt" OnServerValidate="CustomCodigoValidate_ServerValidate"
                                                        ErrorMessage="Esta empresa no existe" CssClass="bg-danger text-white" Display="Dynamic" ValidationGroup="GuardarComoUsuario" />
                                                </div>
                                                <div class="row">
                                                    <%--Email--%>
                                                    <div class="col-12 col-md-5">
                                                        <div class="form-group">
                                                            <label for="EmailTxtComousuario">Email</label>
                                                            <asp:TextBox TextMode="Email" runat="server" ID="EmailTxtComousuario" placeholder="El correo debe ser Gmail." CssClass="form-control "></asp:TextBox>
                                                        </div>
                                                        <asp:RequiredFieldValidator ID="RFVEmail" runat="server"
                                                            ControlToValidate="EmailTxtComousuario" ValidationGroup="GuardarComoUsuario"
                                                            Display="Dynamic" SetFocusOnError="true"
                                                            ForeColor="Red" ToolTip="Campo Descripcion Obligatorio"
                                                            ErrorMessage="Este campo no puede estar vacio">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <%--Password--%>
                                                    <div class="col-12 col-md-5">
                                                        <div class="form-group">
                                                            <label for="ClaveTxt">Contrase&ntilde;a</label>
                                                            <asp:TextBox runat="server" ID="ClaveTxt" TextMode="Password" placeholder="Clave" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <%--ConfirmarPassword--%>
                                                    <div class="col-12 col-md-5">
                                                        <div class="form-group">
                                                            <label for="ConfClaveTxt">Confirmar Contrase&ntilde;a</label>
                                                            <asp:TextBox runat="server" ID="ConfClaveTxt" TextMode="Password" placeholder="Confirmar clave" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <asp:CompareValidator runat="server" ID="CVPassword"
                                                        ErrorMessage="Contrase&ntilde;a no Coinciden" ControlToValidate="ClaveTxt" ControlToCompare="ConfClaveTxt"
                                                        CssClass="bg-danger text-white" Display="Dynamic" Type="String" Operator="Equal" ValidationGroup="GuardarComoUsuario">
                                                    </asp:CompareValidator>
                                                </div>
                                            </div>
                                            <div class="modal-footer">
                                                <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                                                <asp:Button ID="GuardarComoUsuario" ValidationGroup="GuardarComoUsuario" OnClick="GuardarComoUsuario_Click" CssClass="btn btn-success" Text="Guardar" runat="server" data-dismiss="modal" UseSubmitBehavior="false" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- Modal para Registrarse como empresa -->
                                <div class="modal fade" id="ModalComoEmpresa" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                    <div class="modal-dialog ml-sm-auto" role="document">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <h5 class="modal-title" id="ModaEmpresa">Como empresa.....!!</h5>
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                    <span aria-hidden="true">&times;</span>
                                                </button>
                                            </div>
                                            <div class="modal-body text-left">
                                                <div class="row">
                                                    <%--UserName--%>
                                                    <div class="col-12 col-md-5">
                                                        <div class="form-group">
                                                            <label for="UserNameTxt">UserName</label>
                                                            <asp:TextBox runat="server" ID="UserNameComoEmpresa" placeholder="Nombre de usuario" CssClass="form-control "></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <%--NombreEmpresa--%>
                                                    <div class="col-12 col-md-5">
                                                        <div class="form-group">
                                                            <label for="NombreEmpresaTxt">Nombre de la empresa</label>
                                                            <asp:TextBox runat="server" ID="NombreEmpresaTxt" placeholder="Nombre de la empresa" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <%--Email--%>
                                                    <div class="col-12 col-md-5">
                                                        <div class="form-group">
                                                            <label for="EmailComoEmpresatxt">Email</label>
                                                            <asp:TextBox TextMode="Email" runat="server" ID="EmailComoEmpresatxt" placeholder="El correo debe ser Gmail." CssClass="form-control "></asp:TextBox>
                                                        </div>
                                                        <asp:RequiredFieldValidator ID="RFVEmailComoEmpres" runat="server"
                                                            ControlToValidate="EmailComoEmpresatxt" ValidationGroup="GuardarComoEmpresaButton"
                                                            Display="Dynamic" SetFocusOnError="true"
                                                            ForeColor="Red" ToolTip="Campo Descripcion Obligatorio"
                                                            ErrorMessage="Este campo no puede estar vacio">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <%--Password--%>
                                                    <div class="col-12 col-md-5">
                                                        <div class="form-group">
                                                            <label for="PasswordComoEmpresa">Contrase&ntilde;a</label>
                                                            <asp:TextBox runat="server" ID="PasswordComoEmpresa" TextMode="Password" placeholder="Clave" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <%--ConfirmarPassword--%>
                                                    <div class="col-12 col-md-5">
                                                        <div class="form-group">
                                                            <label for="ConfPasswordComoEmpresa">Confirmar Contrase&ntilde;a</label>
                                                            <asp:TextBox runat="server" ID="ConfPasswordComoEmpresa" TextMode="Password" placeholder="Confirmar clave" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <asp:CompareValidator runat="server" ID="CompareValidator1"
                                                        ErrorMessage="Contrase&ntilde;a no Coinciden" ControlToValidate="PasswordComoEmpresa" ControlToCompare="ConfPasswordComoEmpresa"
                                                        CssClass="bg-danger text-white" Display="Dynamic" Type="String" Operator="Equal" ValidationGroup="GuardarComoEmpresaButton">
                                                    </asp:CompareValidator>
                                                </div>
                                            </div>
                                            <div class="modal-footer">
                                                <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                                                <asp:Button ID="GuardarComoEmpresaButton" ValidationGroup="GuardarComoEmpresaButton" OnClick="GuardarComoEmpresaButton_Click" CssClass="btn btn-success" Text="Guardar" runat="server" data-dismiss="modal" UseSubmitBehavior="false" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <a href="#" class="forgot-pass">¿Olvidaste la contrase&ntilde;a?</a><small>¿No tienes una cuenta? </small><a id="RegistrateLink" onclick="MostrarModalRegistro('Registro')" data-dismiss="modal" runat="server" class="signup">Registrate</a>
                            </div>
                        </div>
                    </div>
                    <div class="copyrights text-center">
                        <p>Design by <a href="https://bootstrapious.com/p/bootstrap-4-dashboard" class="external">Bootstrapious</a></p>
                        <!-- Please do not remove the backlink to us unless you support further theme's development at https://bootstrapious.com/donate. It is part of the license conditions. Thank you for understanding :)-->
                    </div>
                </div>
            </div>
        </div>
        <!--Modal para registrarse-->
        <div class="modal fade" id="ModalRegistro" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog ml-sm-auto" role="document">
                <div class="modal-content">
                    <div class="modal-header ">
                        <h5 class="modal-title text-center" id="Registro">Bienvenido!!</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body text-center" runat="server">
                        <strong class="text-primary">¿Como desea registrarse?</strong>
                        <div class="row">
                            <div class="col-6">
                                <div class="form-group">
                                    <button id="EmpresaButton" data-dismiss="modal" onclick="MostrarModalEmpresa('Como empresa')" class="btn btn-outline-primary text-black-50"><strong>Como empresa</strong></button>
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <button id="UsuarioButton" data-dismiss="modal" onclick="MostrarModal('Como usuario')" class="btn btn-outline-dark"><strong>Como usuario</strong> </button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-outline-danger" data-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <!-- JavaScript files-->
    <script src="/Design/vendor/jquery/jquery.min.js"></script>
    <script src="/Design/vendor/popper.js/umd/popper.min.js"> </script>
    <script src="/Design/vendor/bootstrap/js/bootstrap.min.js"></script>
    <script src="/Design/js/grasp_mobile_progress_circle-1.0.0.min.js"></script>
    <script src="/Design/vendor/jquery.cookie/jquery.cookie.js"> </script>
    <script src="/Design/vendor/chart.js/Chart.min.js"></script>
    <script src="/Design/vendor/jquery-validation/jquery.validate.min.js"></script>
    <script src="/Design/vendor/malihu-custom-scrollbar-plugin/jquery.mCustomScrollbar.concat.min.js"></script>
    <!-- Main File-->
    <script src="/Design/js/front.js"></script>
</body>
</html>
