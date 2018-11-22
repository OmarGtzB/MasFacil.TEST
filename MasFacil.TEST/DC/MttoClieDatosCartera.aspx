<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MttoClieDatosCartera.aspx.cs" Inherits="DC_MttoClieDatosCartera" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
    <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
              <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
             <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />

              </Scripts>
              </telerik:RadScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                                                                                
                <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
                    <telerik:RadImageButton ID="rBtnNuevo"     runat ="server" Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text ="" OnClick="rBtnNuevo_Click" Visible="false" ></telerik:RadImageButton><%-- OnClick="rBtnNuevo_Click"  --%>
                    <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  ToolTip="Modificar"  Text="" OnClick="rBtnModificar_Click" Visible="false" ></telerik:RadImageButton>
                    <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text="" OnClick="rBtnEliminar_Click" OnClientClicking="OnClientClic_ConfirmOK" Visible="false" ></telerik:RadImageButton>
                    <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"  Text="" OnClick="rBtnLimpiar_Click"></telerik:RadImageButton>
                </asp:Panel>                                                                                  
                 
                  
          
            <fieldset  >   
                        <table border="0" style=" width:540px; text-align:left; background-color:transparent ;">
                            <tr style="height:18px;">
                                <td style=" width:70px; background-color:transparent;">
                                   <telerik:RadLabel ID="RadLabel5" runat="server">Cliente:</telerik:RadLabel>
                                </td>  
                                <td style=" width:200px; background-color:transparent;">
                                    <telerik:RadLabel ID="rLblClave" runat="server" Text="Cliente"></telerik:RadLabel>     
                                </td>    
                                <td style=" width:70px; background-color:transparent;">
                                    <telerik:RadLabel ID="rLblSubClie" runat="server">SubCliente:</telerik:RadLabel>  
                                </td>   
                                <td style=" width:200px; background-color:transparent;">
                                     <telerik:RadLabel ID="rLblSubClient" runat="server" Text="Subcliente"> </telerik:RadLabel>
                                </td>                                                                                                         
                           </tr>
                       </table>

                        <table border="0" style=" width:540px; text-align:left; background-color:transparent ;">
                            <tr style="height:18px;">
                                <td style=" width:70px; background-color:transparent;">
                                        <telerik:RadLabel ID="RadLabel7" runat="server">Nombre:</telerik:RadLabel>
                                </td>  
                                <td style=" width:470px; background-color:transparent;">
                                        <telerik:RadLabel ID="rLblDescripcion" runat="server" Text="Nombre"> </telerik:RadLabel>
                                </td>                                                                                                                     
                           </tr>
                       </table>

            </fieldset>
    
                          <fieldset >     
                          <legend>Datos</legend>



                        <table border="0" style=" width:540px; text-align:left; background-color:transparent ;">
                            <tr style="height:18px;">
                                <td style=" width:150px; background-color:transparent;">
                                    <telerik:RadLabel ID="RadLabel2" runat="server" Text="Condición de pago:"></telerik:RadLabel>   
                                </td>  
                                <td style=" width:390px; background-color:transparent;">
<%--                                        <telerik:RadComboBox  ID="" runat="server"  AutoPostBack="True"
                                              Enabled="false" 
                              HighlightTemplatedItems="true"
                              DropDownCssClass="cssRadComboBox"  
                              Width="250px" DropDownWidth="390px" Height="200px"  >--%>
                            <telerik:RadComboBox ID="rCboCondicionPago" runat="server"  AutoPostBack="false"  
                                    HighlightTemplatedItems="true"
                                    DropDownCssClass="cssRadComboBox"  
                                    Width="250px" DropDownWidth="390px" Height="200px">
                                    <HeaderTemplate>
                                        <table style="width: 360px" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 160px;">
                                                        Clave
                                                    </td>
                                                    <td style="width: 200px;">
                                                        Descripción
                                                    </td>
                                                </tr>
                                            </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                            <table style="width: 360px;"  cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 160px;">
                                                        <%# DataBinder.Eval(Container.DataItem, "conPagCve") %>
                                                    </td>
                                                    <td style="width: 200px;">
                                                        <%# DataBinder.Eval(Container.DataItem, "conPagDes") %>
                                                    </td>
                                  
                                                </tr>
                                            </table>

                                    </ItemTemplate>

                            <FooterTemplate>
                                <asp:Literal runat="server" ID="RadComboItemsCount" />
                            </FooterTemplate>
                            </telerik:RadComboBox>
 
 

                                </td>                                                                                                                     
                           </tr>
                            <tr style="height:18px;">
                                <td style=" width:150px; background-color:transparent;">
                                    <telerik:RadLabel ID="RadLabel4" runat="server" Text="Limite de Credito:"></telerik:RadLabel>   
                                </td>  
                                <td style=" width:390px; background-color:transparent;">
                                         <telerik:RadNumericTextBox ID="RadNTxtLimitCredito" runat="server" Width="250px" MaxLength="50"
                                                                    EnabledStyle-CssClass="cssTxtEnabled"
                                                                    DisabledStyle-CssClass ="cssTxtEnabled"
                                                                    HoveredStyle-CssClass="cssTxtHovered"
                                                                    FocusedStyle-CssClass="cssTxtFocused"
                                                                    InvalidStyle-CssClass="cssTxtInvalid"
                                              AutoPostBack="false">
                                         </telerik:RadNumericTextBox>
                                </td>                                                                                                                     
                           </tr>
                            <tr style="height:18px;">
                                <td style=" width:150px; background-color:transparent;">
                                       <telerik:RadLabel ID="RadLabel1" runat="server" Text="Situación Crediticia:"></telerik:RadLabel>
                                </td>  
                                <td style=" width:390px; background-color:transparent;">
                        <telerik:RadComboBox ID="rCboSitCrediticia" runat="server"  AutoPostBack="True"
                            Enabled="false" 
                              HighlightTemplatedItems="true"
                              DropDownCssClass="cssRadComboBox"  
                              Width="250px" DropDownWidth="390px" Height="200px" >
                                            <HeaderTemplate>
                        <table style="width: 360px" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width: 160px;">
                                        Clave
                                    </td>
                                    <td style="width: 200px;">
                                        Descripción
                                    </td>
                                </tr>
                            </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                            <table style="width: 360px;"  cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width:160px" >
                                        <%# DataBinder.Eval(Container.DataItem, "siCrCve")%>
                                    </td>
                                    <td style="width: 200px;">
                                        <%# DataBinder.Eval(Container.DataItem, "siCrDes") %>
                                    </td>
                                </tr>
                            </table>
                    </ItemTemplate>
                        <FooterTemplate>
                            <asp:Literal runat="server" ID="RadComboItemsCount" />
                        </FooterTemplate>
                        </telerik:RadComboBox>
            </td>                                                                                                                     
        </tr>
                            <tr>
                                <td>
                                    <telerik:RadLabel ID="RadLabel3" runat="server" Text="Metodo de Pago:"></telerik:RadLabel>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rCboMetodoPago" runat="server"  AutoPostBack="True"
                                              Enabled="false" 
                              HighlightTemplatedItems="true"
                              DropDownCssClass="cssRadComboBox"  
                              Width="250px" DropDownWidth="390px" Height="200px" >
                                            <HeaderTemplate>
                        <table style="width: 360px" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width: 160px;">
                                        Clave
                                    </td>
                                    <td style="width: 200px;">
                                        Descripción
                                    </td>
                                </tr>
                            </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                            <table style="width: 360px;"  cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width:160px" >
                                        <%# DataBinder.Eval(Container.DataItem, "satMetPagCve")%>
                                    </td>
                                    <td style="width: 200px;">
                                        <%# DataBinder.Eval(Container.DataItem, "satMetPagDes") %>
                                    </td>
                                </tr>
                            </table>
                    </ItemTemplate>
                        <FooterTemplate>
                            <asp:Literal runat="server" ID="RadComboItemsCount" />
                        </FooterTemplate>
                        </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadLabel ID="RadLabel6" runat="server" Text="Número de Cuenta"></telerik:RadLabel>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="RdNmricNumCuenta" runat="server" Width="250px" MaxLength="4" 
                                                NumberFormat-DecimalDigits="0"  NumberFormat-GroupSeparator="0" NumberFormat-GroupSizes="4"
                                                EnabledStyle-CssClass="cssTxtEnabled"
                                                DisabledStyle-CssClass ="cssTxtEnabled"
                                                HoveredStyle-CssClass="cssTxtHovered"
                                                FocusedStyle-CssClass="cssTxtFocused"
                                                InvalidStyle-CssClass="cssTxtInvalid"
                                              AutoPostBack="false">
                                    </telerik:RadNumericTextBox>
                                </td>
                                    
                            </tr>
                    </table>


            </fieldset>


 
                 
 
    <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
        <telerik:RadImageButton ID="rBtnGuardar"  runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text ="" OnClick="rBtnGuardar_Click" OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
        <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text="" OnClick="rBtnCancelar_Click" OnClientClicking="OnClientClic_ConfirmCancel"></telerik:RadImageButton>
    </asp:Panel>




                                   <asp:HiddenField ID="hdfBtnAccion" runat="server" />
                                   <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
                                   </telerik:RadWindowManager>
                                  

                                   <telerik:RadTextBox ID="rTxtciaCve" runat="server" Enabled="false" Visible="false">
                                   </telerik:RadTextBox>
         


    </ContentTemplate>
    </asp:UpdatePanel> 


    </form>
                          


 
</body>

</html>
