<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MttoAsientosContablesDet.aspx.cs" Inherits="DC_MttoAsientosContablesDet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="http://code.jquery.com/jquery-1.11.0.min.js"></script>
    <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
    <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
    <link href="../css/styles.css" rel="stylesheet" type="text/css" />
    <link href="/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function Close() {
            GetRadWindow().close();
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


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                    <fieldset style="  margin-top:5px;   display: block; text-align:left;" >
                        <div style="width:100%; display:table; position:static; background-color:transparent;" >
                            <table border="0" style=" text-align:left; background-color:transparent;" >
                                <tr style="background-color:transparent;" >
                                    <td colspan="2">
                                        <telerik:RadLabel ID="RadLabel5" runat="server" Text="Movimiento" Width="130px" BackColor="transparent"></telerik:RadLabel>
                                        <asp:RadioButton ID="MoviCargo" runat="server" GroupName="Movimiento" Text="Cargo" Checked="true" Width="80px" BackColor="transparent" Enabled="true"  />
                                        <asp:RadioButton ID="MoviAbono" runat="server" GroupName="Movimiento" Text="Abono"   Enabled="true" BackColor="transparent" Width="100px" />
                                    </td>
                                </tr>
                                <tr  style="background-color:transparent;">
                                    <td>
                                        <telerik:RadLabel ID="RadLabel3" runat="server" Text="Codificación Cont." Width="130px"></telerik:RadLabel>
                                        </td>
                                     <td style=" width:220px;">
                                          <telerik:RadComboBox ID="rCboCodificacion" runat="server" Width="200px"  AllowCustomText="true"
                                            HighlightTemplatedItems="true" AutoPostBack="true"
                                          DropDownCssClass="cssRadComboBox" 
                                          DropDownWidth="260px" 
                                           Height="200px"  >
                                                <HeaderTemplate>
                                                        <table style="width: 300px" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td style="width: 100px;">
                                                                        Clave
                                                                    </td>
                                                                    <td style="width: 200px;">
                                                                        Descripción
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                                <table style="width: 300px;"  cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                        <td style="width:100px" >
                                                                            <%# DataBinder.Eval(Container.DataItem, "Clave")%>
                                                                        </td>
                                                                        <td style="width: 200px;">
                                                                            <%# DataBinder.Eval(Container.DataItem, "Descripcion") %>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                </FooterTemplate>
                                        </telerik:RadComboBox>
                                         
                                    </td>
                                     <td >
                                         <telerik:RadLabel ID="RadLabel4" runat="server" Text="Descripción" Width="110px" BackColor="transparent"></telerik:RadLabel>
                                         </td>
                                     <td >
                                         <telerik:RadTextBox ID="rTxtDescripcion" runat="server" Width="200px"
                                                    EnabledStyle-CssClass="cssTxtEnabled"
                                                    DisabledStyle-CssClass ="cssTxtEnabled"
                                                    HoveredStyle-CssClass="cssTxtHovered"
                                                    FocusedStyle-CssClass="cssTxtFocused"
                                                    InvalidStyle-CssClass="cssTxtInvalid">
                                         </telerik:RadTextBox>
                                     </td>
                                </tr>
                            </table>
                         </div>
                    </fieldset>

                   <fieldset style="  margin-top:5px;   display: block; text-align:left;" >
                        <legend>Referencias</legend>
                        <div style="width:100%; display:table; position:static; background-color:transparent;" >

                            <table border="0" style=" text-align:left; background-color:transparent; " >
                                <tr style="background-color:transparent;">
                                    <td style="width:110px; background-color:transparent;">
                                         <telerik:RadLabel ID="RadLabel6" runat="server" Text="Referencia 1" Width="130px" BackColor="transparent"></telerik:RadLabel>
                                   </td>
                                    <td style=" width:220px;">

                                          <telerik:RadTextBox ID="rdtxtReferencia01" runat="server" Width="200px"
                                                    EnabledStyle-CssClass="cssTxtEnabled"
                                                    DisabledStyle-CssClass ="cssTxtEnabled"
                                                    HoveredStyle-CssClass="cssTxtHovered"
                                                    FocusedStyle-CssClass="cssTxtFocused"
                                                    InvalidStyle-CssClass="cssTxtInvalid">
                                         </telerik:RadTextBox>
                                    </td>

                                    <td style="width:110px; background-color:transparent;">
                                         <telerik:RadLabel ID="RadLabel7" runat="server" Text="Referencia 2" Width="110px" BackColor="transparent" ></telerik:RadLabel>
                                        </td>
                                         <td style="width:150px; background-color:transparent;">
                                          <telerik:RadTextBox ID="rdtxtReferencia02" runat="server" Width="200px"
                                                    EnabledStyle-CssClass="cssTxtEnabled"
                                                    DisabledStyle-CssClass ="cssTxtEnabled"
                                                    HoveredStyle-CssClass="cssTxtHovered"
                                                    FocusedStyle-CssClass="cssTxtFocused"
                                                    InvalidStyle-CssClass="cssTxtInvalid">
                                         </telerik:RadTextBox>
                                    </td>
                                </tr>
                                  <tr  style="background-color:transparent;">
                                    <td style="width:110px; background-color:transparent;">
                                         <telerik:RadLabel ID="RadLabel8" runat="server" Text="Referencia 3" Width="120px"  BackColor="transparent"></telerik:RadLabel>
                                   </td>
                                    <td style="width:100px; background-color:transparent;">

                                          <telerik:RadTextBox ID="rdtxtReferencia03" runat="server" Width="200px"
                                                    EnabledStyle-CssClass="cssTxtEnabled"
                                                    DisabledStyle-CssClass ="cssTxtEnabled"
                                                    HoveredStyle-CssClass="cssTxtHovered"
                                                    FocusedStyle-CssClass="cssTxtFocused"
                                                    InvalidStyle-CssClass="cssTxtInvalid">
                                         </telerik:RadTextBox>
                                    </td>

                                    <td style="width:110px; background-color:transparent;">
                                         <telerik:RadLabel ID="RadLabel9" runat="server" Text="Referencia 4" Width="110px" BackColor="transparent" ></telerik:RadLabel>
                                        </td>
                                         <td style="width:150px; background-color:transparent;">

                                         <telerik:RadTextBox ID="rdtxtReferencia04" runat="server" Width="200px"
                                                    EnabledStyle-CssClass="cssTxtEnabled"
                                                    DisabledStyle-CssClass ="cssTxtEnabled"
                                                    HoveredStyle-CssClass="cssTxtHovered"
                                                    FocusedStyle-CssClass="cssTxtFocused"
                                                    InvalidStyle-CssClass="cssTxtInvalid">
                                         </telerik:RadTextBox>
                                    </td>
                                </tr>
                               

                    
                            </table>
                         </div>
                    </fieldset>

                   <fieldset style="  margin-top:5px;   display: block; text-align:left;" >
                        <legend>Fechas</legend>
                        <div style="width:100%; display:table; position:static; background-color:transparent;" >

                            <table border="0" style=" text-align:left; background-color:transparent;" >
                                <tr style=" background-color:transparent;">
                                    <td style="width:135px; background-color:transparent;">
                                         <telerik:RadLabel ID="RadLabel10" runat="server" Text="Movimiento"   BackColor="transparent"></telerik:RadLabel>
                                   </td>
                                      <td style="width:225px; background-color:transparent;">

                                          <telerik:RadDatePicker ID="RadDateMovimiento" runat="server" Width="150px"></telerik:RadDatePicker>
                                    </td>

                                     <td style="width:110px; background-color:transparent;">
                                         <telerik:RadLabel ID="RadLabel11" runat="server" Text="Vencimiento" BackColor="transparent" ></telerik:RadLabel>
                                        </td>
                                         <td style="width:150px; background-color:transparent;">
                                          <telerik:RadDatePicker ID="RadDateVencimiento" runat="server" Width="150px"></telerik:RadDatePicker>
                                    </td>
                                </tr>
                            </table>
                         </div>
                    </fieldset>
                <fieldset style="  margin-top:5px;   display: block; text-align:left;" >
                      <legend>CCO/Tipo Cambio</legend>
                        <div style="width:100%; display:table; position:static; background-color:transparent;" >

                            <table border="0" style=" text-align:left; background-color:transparent;"  >
                                <tr style="background-color:transparent;">
                                   <td style="width:135px; background-color:transparent;">
                                         <telerik:RadLabel ID="RadLabel12" runat="server" Text="Centro de Costo"  BackColor="transparent"></telerik:RadLabel>
                                   </td>
                                    <td style="width:225px; background-color:transparent;">
                                         <telerik:RadTextBox ID="rdtxtCentroCosto" runat="server" Width="200px" MaxLength="10"
                                                    EnabledStyle-CssClass="cssTxtEnabled"
                                                    DisabledStyle-CssClass ="cssTxtEnabled"
                                                    HoveredStyle-CssClass="cssTxtHovered"
                                                    FocusedStyle-CssClass="cssTxtFocused"
                                                    InvalidStyle-CssClass="cssTxtInvalid">
                                         </telerik:RadTextBox>
                                    </td>

                                     <td style="width:105px; background-color:transparent;">
                                         <telerik:RadLabel ID="RadLabel13" runat="server" Text="Tipo de Cambio" Width="110px" BackColor="transparent" ></telerik:RadLabel>
                                        </td>
                                         <td style="width:225px; background-color:transparent;">

                                             <telerik:RadNumericTextBox ID="rdtxtTipoCambio" runat="server"
                                             Width="200px"
                                                    EnabledStyle-CssClass="cssTxtEnabled"
                                                    DisabledStyle-CssClass ="cssTxtEnabled"
                                                    HoveredStyle-CssClass="cssTxtHovered"
                                                    FocusedStyle-CssClass="cssTxtFocused"
                                                    InvalidStyle-CssClass="cssTxtInvalid">
                                        </telerik:RadNumericTextBox>
                                    </td>
                                </tr>
                                <tr style="background-color:transparent;">
                                  <td style="width:135px; background-color:transparent;">
                                         <telerik:RadLabel ID="RadLabel14" runat="server" Text="Importe"  BackColor="transparent" ></telerik:RadLabel>
                                        </td>
                                    <td style="width:150px; background-color:transparent;">

                                        <telerik:RadNumericTextBox ID="RdNumricImporte" runat="server"
                                             Width="200px"
                                                    EnabledStyle-CssClass="cssTxtEnabled"
                                                    DisabledStyle-CssClass ="cssTxtEnabled"
                                                    HoveredStyle-CssClass="cssTxtHovered"
                                                    FocusedStyle-CssClass="cssTxtFocused"
                                                    InvalidStyle-CssClass="cssTxtInvalid">
                                        </telerik:RadNumericTextBox>
                                  
                                    </td>
                                     <td style="width:105px; background-color:transparent;">
                                        <telerik:RadLabel ID="RadLabel2" runat="server" Text="Moneda" Width="110px" BackColor="transparent" ></telerik:RadLabel>
                                    </td>
                                    <td>
                                            <telerik:RadComboBox ID="rCboMoneda" runat="server" Width="200px"  AllowCustomText="true"
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
                                                                <%# DataBinder.Eval(Container.DataItem, "monCve")%>
                                                            </td>
                                                            <td style="width: 170px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "monDes") %>
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
                         </div>
                    </fieldset>


                  <div style="width:100%; display:table; margin-top:5px; position:static; background-color:transparent;" align="center">         
                    </div>  
       </div>
            <div style="width:100%; display:table; position:static; background-color:transparent;">
            <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
                <telerik:RadImageButton ID="rBtnGuardar"  runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text =""  OnClick="rBtnGuardar_Click"  OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text=""   OnClick="rBtnCancelar_Click" OnClientClicking="OnClientClic_ConfirmCancel" ></telerik:RadImageButton>
            </asp:Panel>

                   <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
                </telerik:RadWindowManager>

                   

            </div>
          <asp:HiddenField ID="hdfBtnAccion" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
