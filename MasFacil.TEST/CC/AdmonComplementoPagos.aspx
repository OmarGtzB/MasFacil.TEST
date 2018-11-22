<%@ Page Title="" Language="C#" MasterPageFile="~/MPForm.master" AutoEventWireup="true" CodeFile="AdmonComplementoPagos.aspx.cs" Inherits="CC_AdmonComplementoPagos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMPForm" Runat="Server">

    <script src="http://code.jquery.com/jquery-1.11.0.min.js"></script>
    <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
    <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" /> 
    <link href="../css/styles.css" rel="stylesheet" type="text/css" />
    <link href="/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function closeRadWindow()  
        {  
            $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest();   
        }  
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMPForm" Runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <div>

            <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
                <telerik:RadImageButton ID="rBtnNuevo"       runat ="server" Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png"          Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png"        Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"          ToolTip="Nuevo"  Text =""  OnClick="rBtnNuevo_Click" Visible="false" ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnModificar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png"      Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png"    Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"    ToolTip="Modificar"  Text=""  OnClick="rBtnModificar_Click" Visible="false" ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnEliminar"    runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"       Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png"     Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text="" OnClick="rBtnEliminar_Click" OnClientClicking="OnClientClic_ConfirmOK" Visible="false" ></telerik:RadImageButton>
                
                <telerik:RadImageButton ID="rBtnValidacion"  runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnValidacionDisabled.png"     Image-Url="~/Imagenes/IcoBotones/IcoBtnValidacion.png"   Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnValidacionHovered.png"   ToolTip="Validacion" OnClick="rBtnValidacion_Click" Text="" Visible="false"  ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtVerErr"       runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnVerErroresDisabled.png"     Image-Url="~/Imagenes/IcoBotones/IcoBtnVerErrores.png"   Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnVerErroresHovered.png"  ToolTip="Ver Errores"  OnClick="rBtVerErr_Click" Text="" Visible="false" ></telerik:RadImageButton>

                <telerik:RadImageButton ID="rBtnGenera"      runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnProcesarDisabled.png"       Image-Url="~/Imagenes/IcoBotones/IcoBtnProcesar.png"     Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnProcesarHovered.png"  ToolTip="Genera"  OnClick="rBtnGenera_Click" Text="" Visible="false" ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnAplica"      runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnAutorizarDisabled.png"      Image-Url="~/Imagenes/IcoBotones/IcoBtnAutorizar.png"    Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnAutorizarHovered.png"  ToolTip="Aplica"  OnClick="rBtnAplica_Click" Text="" Visible="false" ></telerik:RadImageButton>
                
                <telerik:RadImageButton ID="rBtnPolizaEdit" runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarPolizaDisabled.png"      Image-Url="~/Imagenes/IcoBotones/IcoBtnModificarPoliza.png"    Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarPolizaHovered.png"  ToolTip="Edita Poliza"  Text="" OnClick="rBtnPolizaeEdit_Click"  Visible="false" ></telerik:RadImageButton>

                <telerik:RadImageButton ID="rBtnPolizaImp"   runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnImprimirDisabled.png"       Image-Url="~/Imagenes/IcoBotones/IcoBtnImprimir.png"     Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnImprimirHovered.png"  ToolTip="Imprime Poliza"  OnClick="rBtnPolizaImp_Click" Text="" Visible="false" ></telerik:RadImageButton>

                <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px"   Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"        Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png"      Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"  Text="" OnClick="rBtnLimpiar_Click" ></telerik:RadImageButton>

            </asp:Panel>  


            <table border="0" style=" border-style:none; width:100%; text-align:left; background-color:transparent ;">
                <tr  style="width:100%;">
                    <td style=" width:100%; background-color:transparent; vertical-align:top;">                      
                        <fieldset style="">
                            <legend>Filtros</legend>
                            <div style="width:100%; display:table; position:static; background-color:transparent;"> 

                            </div>
                        </fieldset>
                    </td>
                </tr>
            </table>

            <table border="0" style=" border-style:none; width:100%; text-align:left; background-color:transparent ;">
                <tr  style="width:100%;">
                    <td style=" width:100%; background-color:transparent; vertical-align:top;">                      
                        <fieldset style="">
                            <legend>Operaciones</legend>
                            <div style="width:100%; height:310px; display:table; position:static; background-color:transparent;"> 

                            </div>
                        </fieldset>
                    </td>
                </tr>
            </table>


            <telerik:RadWindow ID="RadWindowVerErrores" runat="server" OnClientClose="closeRadWindow"  Width="700px" Height="440px" Modal="true" VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close" Title="Ver Errores"  >               
            </telerik:RadWindow>

            <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
            </telerik:RadWindowManager>

            <asp:HiddenField ID="hdfRawUrl" runat="server" />
            <asp:HiddenField ID="hdfBtnAccion" runat="server" />

        </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server"   
                    OnAjaxRequest="RadAjaxManager1_AjaxRequest" >  
                    <AjaxSettings>  
                    </AjaxSettings>  
    </telerik:RadAjaxManager>

</asp:Content>

