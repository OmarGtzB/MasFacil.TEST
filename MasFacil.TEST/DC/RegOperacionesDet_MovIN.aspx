<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegOperacionesDet_MovIN.aspx.cs" Inherits="DC_RegOperacionesDet_MovIN" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
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

                    <table border="0" style=" text-align:left; background-color:transparent; width:720px" >
                        <tr>
                            <td style="width:130px; background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel4" runat="server" Text="Movimiento"  ></telerik:RadLabel>
                            </td>
                            <td style="width:230px; background-color:transparent;">
                                                                <asp:RadioButton ID="MovimientoCargo" runat="server" GroupName="Movimiento" Text="Cargo" Checked="true"  Enabled="true"  />
                                <asp:RadioButton ID="MovimientoAbono" runat="server" GroupName="Movimiento" Text="Abono" Checked="false" Enabled="true"  />                    </td>
                            <td style="width:130px; background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel5" runat="server" Text="Aplica O.C."  BackColor="transparent"></telerik:RadLabel>
                            </td>
                            <td style="width:230px; background-color:transparent;">
                                <asp:RadioButton ID="AplicaSi" runat="server" GroupName="TipoAplicacion" Text="Si" Checked="true"  BackColor="transparent" Enabled="true"  />
                                <asp:RadioButton ID="AplicaNo" runat="server" GroupName="TipoAplicacion" Text="No"   Enabled="true" BackColor="transparent"  />

                            </td>
                         </tr>
                    </table>
                </div>
            </fieldset>

 
            <fieldset style="  margin-top:5px;   display: block; text-align:left;" >
                <div style="width:100%; display:table; position:static; background-color:transparent;" >

                    <table border="0" style=" text-align:left; background-color:transparent; width:720px" >
                        <tr>
                            <td style="width:130px; background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel16" runat="server" Text="Artículo"></telerik:RadLabel>
                            </td>
                            <td style="width:230px; background-color:transparent;">
                                <telerik:RadComboBox ID="rCboArticulo" runat="server" Width="210px"  AllowCustomText="true"
                                            HighlightTemplatedItems="true" AutoPostBack="true"
                                            DropDownCssClass="cssRadComboBox" 
                                            DropDownWidth="320px" 
                                            Height="200px"  >
                                    <HeaderTemplate>
                                        <table style="width: 280px" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="width: 110px;">
                                                    Clave
                                                </td>
                                                <td style="width: 170px;">
                                                     Descripción
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table style="width: 280px;"  cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="width:110px" >
                                                    <%# DataBinder.Eval(Container.DataItem, "artCve")%>
                                                </td>
                                                <td style="width: 170px;">
                                                    <%# DataBinder.Eval(Container.DataItem, "artDes") %>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td style="width:130px; background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel22" runat="server" Text="Almacén"   ></telerik:RadLabel>
                            </td>
                            <td style="width:230px; background-color:transparent;">
                                <telerik:RadComboBox ID="rCboAlmacen" runat="server" Width="210px"  BackColor="transparent" AllowCustomText="true"
                                            HighlightTemplatedItems="true" AutoPostBack="true"
                                            DropDownCssClass="cssRadComboBox" 
                                            DropDownWidth="260px" 
                                            Height="200px"  >
                                        <HeaderTemplate>
                                                    <table style="width: 260px" cellspacing="0" cellpadding="0">
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
                                                     <table style="width: 260px;"  cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width:90px" >
                                                                <%# DataBinder.Eval(Container.DataItem, "almCve")%>
                                                            </td>
                                                            <td style="width: 170px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "almDes") %>
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
                            <td style="width:130px; background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel17" runat="server" Text="U.Medida"  BackColor="transparent" ></telerik:RadLabel>
                            </td>
                            <td style="width:230px; background-color:transparent;">
                                          <telerik:RadComboBox ID="rCboUnidadMedida" runat="server" Width="210px"  BackColor="transparent" AllowCustomText="true"
                                            HighlightTemplatedItems="true" AutoPostBack="true"
                                            DropDownCssClass="cssRadComboBox" 
                                            DropDownWidth="215px" 
                                            Height="200px"  >
                                                <HeaderTemplate>
                                                    <table style="width: 170px" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 200px;">
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
                                                                <%# DataBinder.Eval(Container.DataItem, "uniMedCve")%>
                                                            </td>
                                                            <td style="width: 170px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "uniMedDes") %>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                </FooterTemplate>
                                        </telerik:RadComboBox>
                            </td>
                            <td style="width:130px; background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel18" runat="server" Text="Cantidad" Width="90px"  BackColor="transparent"></telerik:RadLabel>
                            </td>
                            <td style="width:230px; background-color:transparent;">
                                <telerik:RadNumericTextBox ID="radTxtCantidad" runat="server" Enabled="true"  Width="210px"
                                    EnabledStyle-CssClass="cssTxtEnabled"
                                    DisabledStyle-CssClass ="cssTxtEnabled"
                                    HoveredStyle-CssClass="cssTxtHovered"
                                    FocusedStyle-CssClass="cssTxtFocused"
                                    InvalidStyle-CssClass="cssTxtInvalid"
                                ></telerik:RadNumericTextBox> 
                            </td>
                        </tr>

                        <tr>
                            <td style="width:130px; background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel14" runat="server" Text="Costo"   BackColor="transparent"></telerik:RadLabel>
                            </td>
                            <td style="width:230px; background-color:transparent;">
                                <telerik:RadNumericTextBox ID="radTxtCosto" runat="server" Enabled="true" Width="210px"
                                    EnabledStyle-CssClass="cssTxtEnabled"
                                    DisabledStyle-CssClass ="cssTxtEnabled"
                                    HoveredStyle-CssClass="cssTxtHovered"
                                    FocusedStyle-CssClass="cssTxtFocused"
                                    InvalidStyle-CssClass="cssTxtInvalid"
                                ></telerik:RadNumericTextBox> 
                            </td>
                            <td style="width:130px; background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel19" runat="server" Text="Importe"    BackColor="transparent" ></telerik:RadLabel>
                            </td>
                            <td style="width:230px; background-color:transparent;">
                                <telerik:RadNumericTextBox ID="radTxtImporte" runat="server" Enabled="true" Width="210px"
                                    EnabledStyle-CssClass="cssTxtEnabled"
                                    DisabledStyle-CssClass ="cssTxtEnabled"
                                    HoveredStyle-CssClass="cssTxtHovered"
                                    FocusedStyle-CssClass="cssTxtFocused"
                                    InvalidStyle-CssClass="cssTxtInvalid"
                                ></telerik:RadNumericTextBox> 
                            </td>
                         </tr>

                        <tr>
                            <td style="width:130px; background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel2" runat="server" Text="Precio"   BackColor="transparent"></telerik:RadLabel>
                            </td>
                            <td style="width:230px; background-color:transparent;">
                                <telerik:RadNumericTextBox ID="radTxtPrecio" runat="server" Enabled="true" Width="210px"
                                    EnabledStyle-CssClass="cssTxtEnabled"
                                    DisabledStyle-CssClass ="cssTxtEnabled"
                                    HoveredStyle-CssClass="cssTxtHovered"
                                    FocusedStyle-CssClass="cssTxtFocused"
                                    InvalidStyle-CssClass="cssTxtInvalid"
                                ></telerik:RadNumericTextBox> 
                            </td>
                            <td style="width:130px; background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel13" runat="server" Text="Lote"   ></telerik:RadLabel>
                            </td>
                            <td style="width:230px; background-color:transparent;">
                                <telerik:RadTextBox ID="rTxtLote" runat="server" Width="210px" Enabled="true" 
                                            EnabledStyle-CssClass="cssTxtEnabled"
                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                            HoveredStyle-CssClass="cssTxtHovered"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid"
                                ></telerik:RadTextBox>
                            </td>
                         </tr>

                        <tr>
                            <td style="width:130px; background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel23" runat="server" Text="Serie"   ></telerik:RadLabel>
                            </td>
                            <td style="width:230px; background-color:transparent;">
                                <telerik:RadTextBox ID="rTxtSerie" runat="server" Width="210px" Enabled="true"   
                                    EnabledStyle-CssClass="cssTxtEnabled"
                                    DisabledStyle-CssClass ="cssTxtEnabled"
                                    HoveredStyle-CssClass="cssTxtHovered"
                                    FocusedStyle-CssClass="cssTxtFocused"
                                    InvalidStyle-CssClass="cssTxtInvalid"
                                ></telerik:RadTextBox>
                            </td>
                            <td style="width:130px; background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel11" runat="server" Text="Moneda"  BackColor="transparent"></telerik:RadLabel>
                            </td>
                            <td style="width:230px; background-color:transparent;">
                                <telerik:RadComboBox ID="rCboMoneda" runat="server" Width="210px"  AllowCustomText="true" OnSelectedIndexChanged="rCboMoneda_SelectedIndexChanged"
                                            HighlightTemplatedItems="true" AutoPostBack="true"
                                          DropDownCssClass="cssRadComboBox" 
                                          DropDownWidth="215px" 
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
                                                            <td style="width:40px" >
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

                        <tr>
                            <td style="width:130px; background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel1" runat="server" Text="Tipo de Cambio"    BackColor="transparent"></telerik:RadLabel>
                            </td>
                            <td style="width:230px; background-color:transparent;">
                                <telerik:RadNumericTextBox ID="radTxtTipoCambio" runat="server" Enabled="true" Width="210px"
                                            EnabledStyle-CssClass="cssTxtEnabled"
                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                            HoveredStyle-CssClass="cssTxtHovered"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid"
                                ></telerik:RadNumericTextBox> 
                            </td>
                            <td style="width:130px; background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel21" runat="server" Text="Centro Costos" BackColor="transparent" ></telerik:RadLabel>
                            </td>
                            <td style="width:230px; background-color:transparent;">
                                <telerik:RadTextBox ID="rTxtCentroCostos" runat="server" Width="210px" Enabled="true" 
                                            EnabledStyle-CssClass="cssTxtEnabled"
                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                            HoveredStyle-CssClass="cssTxtHovered"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid"
                                ></telerik:RadTextBox>
                            </td>
                         </tr>

                        <tr>
                            <td style="width:130px; background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel12" runat="server" Text="O. Compra"  BackColor="transparent" ></telerik:RadLabel>
                            </td>
                            <td style="width:230px; background-color:transparent;">
                                <telerik:RadTextBox ID="rTxtOrdenCompra" runat="server" Width="210px" Enabled="true" 
                                            EnabledStyle-CssClass="cssTxtEnabled"
                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                            HoveredStyle-CssClass="cssTxtHovered"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid"
                                ></telerik:RadTextBox>
                            </td>


                            <td style="width:130px; background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel24" runat="server" Text="Proveedor"   ></telerik:RadLabel>
                            </td>
                            <td style="width:230px; background-color:transparent;">
                                <telerik:RadComboBox ID="rCboProveedor" runat="server" Width="210px"  AllowCustomText="true"
                                            HighlightTemplatedItems="true" AutoPostBack="true"
                                            DropDownCssClass="cssRadComboBox" 
                                            DropDownWidth="320px" 
                                            Height="200px"  >
                                    <HeaderTemplate>
                                        <table style="width: 280px" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="width: 110px;">
                                                    Clave
                                                </td>
                                                <td style="width: 170px;">
                                                     Descripción
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table style="width: 280px;"  cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="width:110px" >
                                                    <%# DataBinder.Eval(Container.DataItem, "Clave")%>
                                                </td>
                                                <td style="width: 170px;">
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
                         </tr>




                         <tr>
                            <td style="width:130px; background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel15" runat="server" Text="Almacén Contra"  BackColor="transparent" ></telerik:RadLabel>
                            </td>
                                <td style="width:230px; background-color:transparent;">
                                <telerik:RadComboBox ID="rCboAlmContra" runat="server" Width="210px"  AllowCustomText="true"
                                            HighlightTemplatedItems="true" AutoPostBack="true"
                                            DropDownCssClass="cssRadComboBox" 
                                            DropDownWidth="320px" 
                                            Height="200px"  >
                                    <HeaderTemplate>
                                        <table style="width: 280px" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="width: 110px;">
                                                    Clave
                                                </td>
                                                <td style="width: 170px;">
                                                     Descripción
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table style="width: 280px;"  cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="width:110px" >
                                                    <%# DataBinder.Eval(Container.DataItem, "Clave")%>
                                                </td>
                                                <td style="width: 170px;">
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


                            <td style="width:130px; background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel20" runat="server" Text="Aduana"   ></telerik:RadLabel>
                            </td>
                             <td style="width:230px; background-color:transparent;">
                                <telerik:RadTextBox ID="rTxtAduana" runat="server" Width="210px" Enabled="true" 
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

 
            <fieldset style="  margin-top:5px;   display: block; text-align:left;" >
                <legend>Fechas</legend>
                <div style="width:100%; display:table; position:static; background-color:transparent;" >

                    <table border="0" style=" text-align:left; background-color:transparent; width:720px" >
                        <tr>
                            <td style="width:130px; background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel10" runat="server" Text="Fecha Mov."  BackColor="transparent"></telerik:RadLabel>
                            </td>
                            <td style="width:230px; background-color:transparent;">
                                <telerik:RadDatePicker ID="RdDatePckrFecha" runat="server" Width="150px"></telerik:RadDatePicker>
                            </td>
                            <td style="width:130px; background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel8" runat="server" Text="Fecha 2"  BackColor="transparent"></telerik:RadLabel>
                            </td>
                            <td style="width:230px; background-color:transparent;">
                                <telerik:RadDatePicker ID="RdDatePckrFecha02" runat="server" Width="150px"></telerik:RadDatePicker>
                            </td>
                         </tr>
                    </table>

                </div>
            </fieldset>

                                
 

            <%--   Referencias--%>
                    <fieldset style="  margin-top:5px;   display: block; text-align:left;" >
                        <legend>Referencias</legend>
                        <div style="width:100%; display:table; position:static; background-color:transparent;" >
                            <table border="0" style=" text-align:left; background-color:transparent; width:720px" >
                                <tr>
                                    <td style="width:130px; background-color:transparent;">
                                         <telerik:RadLabel ID="RadLabel3" runat="server" Text="Referencia 1"   BackColor="transparent"></telerik:RadLabel>
                                   </td>
                                    <td style="width:230px; background-color:transparent;">
                                          <telerik:RadTextBox ID="rTxtReferencia1" runat="server" Width="210px" Enabled="true" 
                                            EnabledStyle-CssClass="cssTxtEnabled"
                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                            HoveredStyle-CssClass="cssTxtHovered"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid"
                                            ></telerik:RadTextBox>
                                    </td>
                                    <td style="width:130px; background-color:transparent;">
                                         <telerik:RadLabel ID="RadLabel6" runat="server" Text="Referencia 2"   BackColor="transparent" ></telerik:RadLabel>
                                        </td>
                                         <td style="width:230px; background-color:transparent;">
                                          <telerik:RadTextBox ID="rTxtReferencia2" runat="server" Width="210px" Enabled="true" 
                                            EnabledStyle-CssClass="cssTxtEnabled"
                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                            HoveredStyle-CssClass="cssTxtHovered"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid"
                                            ></telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:130px; background-color:transparent;">
                                         <telerik:RadLabel ID="RadLabel7" runat="server" Text="Referencia 3"   BackColor="transparent"></telerik:RadLabel>
                                   </td>
                                    <td style="width:230px; background-color:transparent;">
                                          <telerik:RadTextBox ID="rTxtReferencia3" runat="server" Width="210px" Enabled="true" 
                                            EnabledStyle-CssClass="cssTxtEnabled"
                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                            HoveredStyle-CssClass="cssTxtHovered"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid"
                                            ></telerik:RadTextBox>
                                    </td>
                                    <td style="width:130px; background-color:transparent;">
                                         <telerik:RadLabel ID="RadLabel9" runat="server" Text="Referencia 4"  BackColor="transparent" ></telerik:RadLabel>
                                        </td>
                                         <td style="width:230px; background-color:transparent;">
                                          <telerik:RadTextBox ID="rTxtReferencia4" runat="server" Width="210px" Enabled="true" 
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



           <%--   DIV BOTONES GUARDAR Y CANCELAR --%>
                      <div style="width:100%; display:table; position:static; background-color:transparent;">
                        <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
                            <telerik:RadImageButton ID="rBtnGuardar"  runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text =""  OnClick="rBtnGuardar_Click"  OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
                            <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text=""  OnClick="rBtnCancelar_Click"   OnClientClicking="OnClientClic_ConfirmCancel" ></telerik:RadImageButton>
                        </asp:Panel>
                    </div>
                           <asp:HiddenField ID="hdfBtnAccionMov" runat="server" />

        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
                
    <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
    </telerik:RadWindowManager>
    </form>
</body>
</html>
