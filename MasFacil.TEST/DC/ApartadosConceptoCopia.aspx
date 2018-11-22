<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApartadosConceptoCopia.aspx.cs" Inherits="DC_ApartadosConceptoCopia" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
        <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
        <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
        <script src="../Scripts/JSGeneral.js" type="text/javascript"></script>

       <script type="text/javascript" >

            function cerrarpagina() {

                window.close();
                return false;

            }

        </script>

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
    <div>   
    
     
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest" DefaultLoadingPanelID="RadAjaxLoadingPanel1" >  
        <AjaxSettings> 
           
            <telerik:AjaxSetting AjaxControlID="rBtnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID = "pnlBody" />
                </UpdatedControls>
            </telerik:AjaxSetting>
           
        </AjaxSettings>  
    </telerik:RadAjaxManager>


       <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Silk"></telerik:RadAjaxLoadingPanel>
           <asp:Panel ID="pnlBody" runat="server">
         
          <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
                <fieldset>
                <legend>Datos Generales</legend>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Clave"></asp:Label> 
                            </td>
                            <td>
                                 <telerik:RadNumericTextBox ID="rTxtClave" width="200px" runat="server" MaxLength="4"  NumberFormat-DecimalDigits="0"  NumberFormat-GroupSizes="4"
                                    EnabledStyle-CssClass="cssTxtEnabled"
                                    DisabledStyle-CssClass ="cssTxtEnabled"
                                    HoveredStyle-CssClass="cssTxtHovered"
                                    FocusedStyle-CssClass="cssTxtFocused"
                                    InvalidStyle-CssClass="cssTxtInvalid">
                                 </telerik:RadNumericTextBox> 
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="Descripción"></asp:Label> 
                            </td>
                            <td>
                                  <telerik:RadTextBox ID="rTxtDescripcion" width="200px" runat="server" 
                                    EnabledStyle-CssClass="cssTxtEnabled"
                                    DisabledStyle-CssClass ="cssTxtEnabled"
                                    HoveredStyle-CssClass="cssTxtHovered"
                                    FocusedStyle-CssClass="cssTxtFocused"
                                    InvalidStyle-CssClass="cssTxtInvalid" 
                                 ></telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Foliador 1"></asp:Label> 
                            </td>
                            <td>
                              <telerik:RadComboBox ID="rcboFoliador" runat="server" Width="200px"  
                                                HighlightTemplatedItems="true" AutoPostBack="true"
                                              DropDownCssClass="cssRadComboBox" 
                                              DropDownWidth="260px" 
                                               Height="200px"  >
                                                    <HeaderTemplate>
                                                        <table style="width: 250px" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td style="width: 80px;">
                                                                        Clave
                                                                    </td>
                                                                    <td style="width: 170px;">
                                                                        Descripción
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table style="width: 250px;"  cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width:80px" >
                                                                    <%# DataBinder.Eval(Container.DataItem, "FolioCve")%>
                                                                </td>
                                                                <td style="width: 170px;">
                                                                    <%# DataBinder.Eval(Container.DataItem, "FolioDescripcion") %>
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
                                 <asp:Label ID="Label3" runat="server" Text="Foliador 2"></asp:Label> 
                            </td>
                            <td>
                             <telerik:RadComboBox ID="rcboFoliador_AsientoCont" runat="server" Width="200px"  
                                                HighlightTemplatedItems="true" AutoPostBack="true"
                                              DropDownCssClass="cssRadComboBox" 
                                              DropDownWidth="260px" 
                                               Height="200px"  >
                                                    <HeaderTemplate>
                                                        <table style="width: 250px" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td style="width: 80px;">
                                                                        Clave
                                                                    </td>
                                                                    <td style="width: 170px;">
                                                                        Descripción
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table style="width: 250px;"  cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width:80px" >
                                                                    <%# DataBinder.Eval(Container.DataItem, "FolioCve")%>
                                                                </td>
                                                                <td style="width: 170px;">
                                                                    <%# DataBinder.Eval(Container.DataItem, "FolioDescripcion") %>
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
                    </table>
                </fieldset>
                
                <fieldset>
                <legend>Incluir</legend>
                    <table>
                        <tr>
                            <td>
                            <telerik:RadCheckBox ID="RadCheckBox_Configuracion" runat="server" Text="Configuración"  OnCheckedChanged="RadCheckBox_Configuracion_CheckedChanged"  ></telerik:RadCheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            <telerik:RadCheckBox ID="RadCheckBox_GuiaConta" runat="server" Text="Guia Contabilización"  OnCheckedChanged="RadCheckBox_GuiaConta_CheckedChanged"  ></telerik:RadCheckBox>
                            </td>
                         </tr>
                          <tr>
                            <td>
                            <telerik:RadCheckBox ID="RadCheckBox_MapeoDoc" runat="server" Text="Mapeo Documento"  OnCheckedChanged="RadCheckBox_MapeoDoc_CheckedChanged"  ></telerik:RadCheckBox>
                            </td>
                        </tr>
                         <tr>
                            <td>
                            <telerik:RadCheckBox ID="RadCheckBox_MapeoMovi" runat="server" Text="Mapeo Movimientos"  OnCheckedChanged="RadCheckBox_MapeoConta_CheckedChanged"  ></telerik:RadCheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            <telerik:RadCheckBox ID="RadCheckBox_AutoridadUsr" runat="server" Text="Autoridad Usuarios"  OnCheckedChanged="RadCheckBox_AutoridadUsr_CheckedChanged"  ></telerik:RadCheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            <telerik:RadCheckBox ID="RadCheckBox_Costos" runat="server" Text="Costos"   OnCheckedChanged="RadCheckBox_Costos_CheckedChanged" ></telerik:RadCheckBox>
                            </td>
                        </tr>
                </table>
                </fieldset>
                <asp:HiddenField ID="hdfBtnAccion" runat="server" />  
                 <div style="width:100%; display:table; position:static; background-color:transparent;">
                    <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
                        <telerik:RadImageButton ID="rBtnGuardar"  runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text =""  OnClick="rBtnGuardar_Click" OnClientClicking="OnClientClic_ConfirmOK"  ></telerik:RadImageButton>
                        <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text=""  OnClick="rBtnCancelar_Click" OnClientClicking="OnClientClic_ConfirmCancel" Visible="false" ></telerik:RadImageButton>
                     </asp:Panel>
                </div>
            <%--</ContentTemplate>
        </asp:UpdatePanel>--%>

           </asp:Panel>

            <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
    </telerik:RadWindowManager>
    </div>
    </form>
</body>
</html>
