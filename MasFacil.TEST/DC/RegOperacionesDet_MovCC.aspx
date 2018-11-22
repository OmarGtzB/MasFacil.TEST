<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegOperacionesDet_MovCC.aspx.cs" Inherits="DC_RegOperacionesDet_MovCC" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
     <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
       <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
     <link href="~/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />
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
         <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div>
                   <fieldset style="  margin-top:5px;   display: block; text-align:left;" >
                        <div style="width:100%; display:table; position:static; background-color:transparent;" >
                            <table border="0" style=" text-align:left; background-color:transparent; width:100%" >
                                <tr>
                                    <td>
                                        <telerik:RadLabel ID="RadLabel1" runat="server" Text="Movimiento" Width="80px" BackColor="transparent"></telerik:RadLabel>
                                        <asp:RadioButton ID="RdioBtnCargo" runat="server" GroupName="TipoMov" Text="Cargo" Checked="true" Width="110px" BackColor="transparent" Enabled="true"  />
                                        <asp:RadioButton ID="RdioBtnAbono" runat="server" GroupName="TipoMov" Text="Abono"  Enabled="true" BackColor="transparent" Width="100px" />
                                    </td>
                                </tr>
                            </table>
                         </div>
                    </fieldset>
                   <%-- ------------------------------------------------------------------------------------------------------%>
          <%--   Tipo de Aplicacion--%>
                <fieldset style="  margin-top:5px;   display: block; text-align:left;" >
                    <legend>Tipo de Aplicación</legend>
                        <div style="width:100%; display:table; position:static; background-color:transparent;" >

                            <table border="0" style=" text-align:left; background-color:transparent; width:100%" >
                                <tr>
                                    <td>
                                        <telerik:RadLabel ID="RadLabel5" runat="server" Text="Tipo" Width="80px" BackColor="transparent"></telerik:RadLabel>
                                        <asp:RadioButton ID="TipoCtaDeposito" runat="server" GroupName="TipoAplicacion" Text="Cta.Depósito" Checked="true" Width="110px" BackColor="Transparent" Enabled="true"  />
                                        <asp:RadioButton ID="TipoProveedor" runat="server" GroupName="TipoAplicacion" Text="Cliente"   Enabled="true" BackColor="Transparent" Width="100px" />
                                    </td>
                                    <td>
                                         <telerik:RadLabel ID="RadLabel8" runat="server" Text="Cliente" Width="125px" BackColor="transparent"></telerik:RadLabel>
                                         <td style=" width:200px; background-color:transparent;">
                                                <telerik:RadComboBox ID="rCboCuenta" runat="server" Width="180px"  
                                                    HighlightTemplatedItems="true" AutoPostBack="true" 
                                                     DropDownCssClass="cssRadComboBox" 
                                                    DropDownWidth="260px" 
                                                   Height="200px"  >
                                                    <HeaderTemplate>
                                                        <table style="width: 250px" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td style="width: 90px;">
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
                                                                <td style="width:90px" >
                                                                    <%# DataBinder.Eval(Container.DataItem, "cliCve")%>
                                                                </td>
                                                                <td style="width: 170px;">
                                                                    <%# DataBinder.Eval(Container.DataItem, "clieNom") %>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                    </FooterTemplate>
                                            </telerik:RadComboBox>
                                    </td>
                                    </td>
                                </tr>
                            </table>
                         </div>
                    </fieldset>
                                     <%--   Referencias--%>
                    <fieldset style="  margin-top:5px;   display: block; text-align:left;" >
                        <legend>Referencias</legend>
                        <div style="width:100%; display:table; position:static; background-color:transparent;" >

                            <table border="0" style=" text-align:left; background-color:transparent; width:100%" >
                                <tr>
                                    <td style="width:100px; background-color:transparent;">
                                         <telerik:RadLabel ID="RadLabel3" runat="server" Text="Principal" Width="90px"  BackColor="transparent"></telerik:RadLabel>
                                   </td>
                                    <td style="width:100px; background-color:transparent;">

<%--                                          <telerik:RadComboBox ID="rCboReferenciasPrincipal" runat="server" Width="200px"  AllowCustomText="true"
                                            HighlightTemplatedItems="true" AutoPostBack="true"
                                          DropDownCssClass="cssRadComboBox" 
                                          DropDownWidth="260px" 
                                           Height="200px"  >
                                                <HeaderTemplate>
                                                    <table style="width: 150px" cellspacing="0" cellpadding="0">
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
                                                                <%# DataBinder.Eval(Container.DataItem, "cptoConfId")%>
                                                            </td>
                                                            <td style="width: 170px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "cptoConfDes") %>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                </FooterTemplate>
                                        </telerik:RadComboBox>--%>
                                        <telerik:RadTextBox ID="radtxt_principal" runat="server" Width="180px" Enabled="true" 
                                            EnabledStyle-CssClass="cssTxtEnabled"
                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                            HoveredStyle-CssClass="cssTxtHovered"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid"
                                            ></telerik:RadTextBox>
                                    </td>

                                    <td style="width:115px; background-color:transparent;">
                                         <telerik:RadLabel ID="RadLabel6" runat="server" Text="Aplicacion" Width="115px" BackColor="transparent" ></telerik:RadLabel>
                                        </td>
                                         <td style="width:150px; background-color:transparent;">

                                        
                                    
                                       <%--   <telerik:RadComboBox ID="rCboReferenciasAplicacion" runat="server" Width="200px"  BackColor="transparent" AllowCustomText="true"
                                            HighlightTemplatedItems="true" AutoPostBack="true"
                                            DropDownCssClass="cssRadComboBox" 
                                            DropDownWidth="260px" 
                                            Height="200px"  >
                                                <HeaderTemplate>
                                                    <table style="width: 150px" cellspacing="0" cellpadding="0">
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
                                                                <%# DataBinder.Eval(Container.DataItem, "cptoConfId")%>
                                                            </td>
                                                            <td style="width: 170px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "cptoConfDes") %>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                </FooterTemplate>
                                        </telerik:RadComboBox>--%>
                                             <telerik:RadTextBox ID="radtxt_aplicacion" runat="server" Width="180px" Enabled="true" 
                                            EnabledStyle-CssClass="cssTxtEnabled"
                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                            HoveredStyle-CssClass="cssTxtHovered"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid"
                                            ></telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:100px; background-color:transparent;">
                                         <telerik:RadLabel ID="RadLabel7" runat="server" Text="Referencia 3" Width="90px"  BackColor="transparent"></telerik:RadLabel>
                                   </td>
                                    <td style="width:100px; background-color:transparent;">

<%--                                          <telerik:RadComboBox ID="rCboReferencia3" runat="server" Width="200px"  AllowCustomText="true"
                                            HighlightTemplatedItems="true" AutoPostBack="true"
                                          DropDownCssClass="cssRadComboBox" 
                                          DropDownWidth="260px" 
                                           Height="200px"  >
                                                <HeaderTemplate>
                                                    <table style="width: 150px" cellspacing="0" cellpadding="0">
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
                                                                <%# DataBinder.Eval(Container.DataItem, "cptoConfId")%>
                                                            </td>
                                                            <td style="width: 170px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "cptoConfDes") %>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                </FooterTemplate>
                                        </telerik:RadComboBox>--%>
                                        <telerik:RadTextBox ID="radtxt_referencia3" runat="server" Width="180px" Enabled="true" 
                                            EnabledStyle-CssClass="cssTxtEnabled"
                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                            HoveredStyle-CssClass="cssTxtHovered"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid"
                                            ></telerik:RadTextBox>
                                    </td>

                                    <td style="width:115px; background-color:transparent;">
                                         <telerik:RadLabel ID="RadLabel9" runat="server" Text="Referencia 4" Width="115px" BackColor="transparent" ></telerik:RadLabel>
                                        </td>
                                         <td style="width:150px; background-color:transparent;">

                                        
                                    
                                          <%--<telerik:RadComboBox ID="rCboReferencia4" runat="server" Width="200px"  BackColor="transparent" AllowCustomText="true"
                                            HighlightTemplatedItems="true" AutoPostBack="true"
                                            DropDownCssClass="cssRadComboBox" 
                                            DropDownWidth="260px" 
                                            Height="200px"  >
                                                <HeaderTemplate>
                                                    <table style="width: 150px" cellspacing="0" cellpadding="0">
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
                                                                <%# DataBinder.Eval(Container.DataItem, "cptoConfId")%>
                                                            </td>
                                                            <td style="width: 170px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "cptoConfDes") %>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                </FooterTemplate>
                                        </telerik:RadComboBox>--%>
                                             <telerik:RadTextBox ID="radtxt_referencia4" runat="server" Width="180px" Enabled="true" 
                                            EnabledStyle-CssClass="cssTxtEnabled"
                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                            HoveredStyle-CssClass="cssTxtHovered"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid"
                                            ></telerik:RadTextBox>
                                    </td>
                                </tr>
                               

                    
                            </table>
                         </div>
                    </fieldset>
                     <%-- Fechas--%>
                    <fieldset style="  margin-top:5px;   display: block; text-align:left;" >
                        <legend>Fechas</legend>
                        <div style="width:100%; display:table; position:static; background-color:transparent;" >

                            <table border="0" style=" text-align:left; background-color:transparent; width:100%" >
                                <tr>
                                    <td style=" width:96px; background-color:transparent;">
                                         <telerik:RadLabel ID="RadLabel10" runat="server" Text="Movimiento" Width="80px"  BackColor="transparent"></telerik:RadLabel>
                                   </td>
                                    <td style="width:170px; background-color:transparent;">

                                         <%-- <telerik:RadComboBox ID="rCboMovimiento" runat="server" Width="200px"  AllowCustomText="true"
                                            HighlightTemplatedItems="true" AutoPostBack="true"
                                          DropDownCssClass="cssRadComboBox" 
                                          DropDownWidth="260px" 
                                           Height="200px"  >
                                                <HeaderTemplate>
                                                    <table style="width: 150px" cellspacing="0" cellpadding="0">
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
                                                                <%# DataBinder.Eval(Container.DataItem, "cptoConfId")%>
                                                            </td>
                                                            <td style="width: 170px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "cptoConfDes") %>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                </FooterTemplate>
                                        </telerik:RadComboBox>--%>
                                        <telerik:RadDatePicker ID="RdDatePckrFecha_Movimiento" runat="server"></telerik:RadDatePicker>

                                    </td>

                                    <td style="width:115px; background-color:transparent;">
                                         <telerik:RadLabel ID="RadLabel11" runat="server" Text="Vencimiento" Width="115px" BackColor="transparent" ></telerik:RadLabel>
                                        </td>
                                         <td style="width:170px; background-color:transparent;">
                                          <%--<telerik:RadComboBox ID="rCboVencimiento" runat="server" Width="200px"  BackColor="transparent" AllowCustomText="true"
                                            HighlightTemplatedItems="true" AutoPostBack="true"
                                            DropDownCssClass="cssRadComboBox" 
                                            DropDownWidth="260px" 
                                            Height="200px"  >
                                                <HeaderTemplate>
                                                    <table style="width: 150px" cellspacing="0" cellpadding="0">
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
                                                                <%# DataBinder.Eval(Container.DataItem, "cptoConfId")%>
                                                            </td>
                                                            <td style="width: 170px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "cptoConfDes") %>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                </FooterTemplate>
                                        </telerik:RadComboBox>--%>
                                             <telerik:RadDatePicker ID="RdDatePckrFecha_Vencimiento" runat="server" ></telerik:RadDatePicker>
                                    </td>
                                </tr>
                            </table>
                         </div>
                    </fieldset>
                     <fieldset style="  margin-top:5px;   display: block; text-align:left;" >
                      
                        <div style="width:100%; display:table; position:static; background-color:transparent;" >

                            <table border="0" style=" text-align:left; background-color:transparent; width:100%" >
                                <tr>
                                    <td style="width:100px; background-color:transparent;">
                                         <telerik:RadLabel ID="RadLabel12" runat="server" Text="Moneda" Width="90px"  BackColor="transparent"></telerik:RadLabel>
                                   </td>
                                    <td style="width:100px; background-color:transparent;">

                                          <telerik:RadComboBox ID="rCboMoneda" runat="server" Width="180px"  AllowCustomText="true"
                                            HighlightTemplatedItems="true" AutoPostBack="true" OnSelectedIndexChanged="rCboMoneda_SelectedIndexChanged"
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

                                    <td style="width:115px; background-color:transparent;">
                                         <telerik:RadLabel ID="RadLabel13" runat="server" Text="Tipo de Cambio" Width="115px" BackColor="transparent" ></telerik:RadLabel>
                                        </td>
                                         <td style="width:150px; background-color:transparent;">

                                        
                                    
                                        <%--  <telerik:RadComboBox ID="rCboTipCambio" runat="server" Width="200px"  BackColor="transparent" AllowCustomText="true"
                                            HighlightTemplatedItems="true" AutoPostBack="true"
                                            DropDownCssClass="cssRadComboBox" 
                                            DropDownWidth="260px" 
                                            Height="200px"  >
                                                <HeaderTemplate>
                                                    <table style="width: 150px" cellspacing="0" cellpadding="0">
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
                                                                <%# DataBinder.Eval(Container.DataItem, "cptoConfId")%>
                                                            </td>
                                                            <td style="width: 170px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "cptoConfDes") %>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                </FooterTemplate>
                                        </telerik:RadComboBox>--%>

                                             <telerik:RadNumericTextBox ID="RdNmrc_tipodcambio" runat="server"
                                                 Enabled="true"  Width="180px"
                                            EnabledStyle-CssClass="cssTxtEnabled"
                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                            HoveredStyle-CssClass="cssTxtHovered"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid"></telerik:RadNumericTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:100px; background-color:transparent;">
                                         <telerik:RadLabel ID="RadLabel14" runat="server" Text="Importe" Width="90px"  BackColor="transparent"></telerik:RadLabel>
                                   </td>
                                    <td style="width:100px; background-color:transparent;">

                                          <%--<telerik:RadComboBox ID="rCboImporte" runat="server" Width="200px"  AllowCustomText="true"
                                            HighlightTemplatedItems="true" AutoPostBack="true"
                                          DropDownCssClass="cssRadComboBox" 
                                          DropDownWidth="260px" 
                                           Height="200px"  >
                                                <HeaderTemplate>
                                                    <table style="width: 150px" cellspacing="0" cellpadding="0">
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
                                                                <%# DataBinder.Eval(Container.DataItem, "cptoConfId")%>
                                                            </td>
                                                            <td style="width: 170px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "cptoConfDes") %>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                </FooterTemplate>
                                        </telerik:RadComboBox>--%>
                                        <telerik:RadNumericTextBox ID="RdNmrc_importe" runat="server" Enabled="true" 
                                            EnabledStyle-CssClass="cssTxtEnabled" Width="180px"
                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                            HoveredStyle-CssClass="cssTxtHovered"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid"></telerik:RadNumericTextBox>
                                    </td>

                                    <td style="width:115px; background-color:transparent;">
                                         <telerik:RadLabel ID="RadLabel15" runat="server" Text="Descripción" Width="115px" BackColor="transparent" ></telerik:RadLabel>
                                        </td>
                                         <td style="width:150px; background-color:transparent;">

                                        
                                    
                                          <%--<telerik:RadComboBox ID="rCboDescripcion" runat="server" Width="200px"  BackColor="transparent" AllowCustomText="true"
                                            HighlightTemplatedItems="true" AutoPostBack="true"
                                            DropDownCssClass="cssRadComboBox" 
                                            DropDownWidth="260px" 
                                            Height="200px"  >
                                                <HeaderTemplate>
                                                    <table style="width: 150px" cellspacing="0" cellpadding="0">
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
                                                                <%# DataBinder.Eval(Container.DataItem, "cptoConfId")%>
                                                            </td>
                                                            <td style="width: 170px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "cptoConfDes") %>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                </FooterTemplate>
                                        </telerik:RadComboBox>--%>
                                            <telerik:RadTextBox ID="Rdtxt_descripcion" runat="server" Width="180px" Enabled="true" 
                                        EnabledStyle-CssClass="cssTxtEnabled"
                                        DisabledStyle-CssClass ="cssTxtEnabled"
                                        HoveredStyle-CssClass="cssTxtHovered"
                                        FocusedStyle-CssClass="cssTxtFocused"
                                        InvalidStyle-CssClass="cssTxtInvalid"
                                        ></telerik:RadTextBox>

                                    </td>
                                </tr>
                            </table>
                         </div>
                    </fieldset>
                </div>
                <div style="width:100%; display:table; position:static; background-color:transparent;">
                <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
                    <telerik:RadImageButton ID="rBtnGuardar"  runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  OnClick="rBtnGuardar_Click" Text =""  OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
                    <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  OnClick="rBtnCancelar_Click" Text=""  OnClientClicking="OnClientClic_ConfirmCancel" ></telerik:RadImageButton>
                </asp:Panel>
            </div>
                 <asp:HiddenField ID="hdfBtnAccionMov" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
            <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
    </telerik:RadWindowManager>
    </form>
</body>
</html>
