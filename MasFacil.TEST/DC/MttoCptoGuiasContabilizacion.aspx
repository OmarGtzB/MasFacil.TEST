<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MttoCptoGuiasContabilizacion.aspx.cs" Inherits="DC_MttoCptoGuiasContabilizacion" %>

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
                <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
                    <telerik:RadImageButton ID="rBtnNuevo"     runat ="server" Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text ="" OnClick="rBtnNuevo_Click" Visible="false"  ></telerik:RadImageButton>
                    <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  ToolTip="Modificar"  Text="" OnClick="rBtnModificar_Click" Visible="false"  ></telerik:RadImageButton>
                    <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text=""  OnClick="rBtnEliminar_Click" OnClientClicking="OnClientClic_ConfirmOK" Visible="false"  ></telerik:RadImageButton>
                    <telerik:RadImageButton ID="rBtnSustituciones"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/rBtnSustitucionesDisabled.png"    Image-Url="~/Imagenes/IcoBotones/rBtnSustituciones.png" Image-HoveredUrl="~/Imagenes/IcoBotones/rBtnSustitucionesHovered.png"  ToolTip="Sustituciones"  Text="" OnClick="rBtnSustituciones_Click"></telerik:RadImageButton> 
                    <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"  Text="" OnClick="rBtnLimpiar_Click" ></telerik:RadImageButton>       
                </asp:Panel>
                <fieldset style="  margin-top:5px;   display: block; text-align:left;" >
                    <div style="width:100%; display:table; position:static; background-color:transparent;" >
                        <table border="0" style=" text-align:left; background-color:transparent; width:100%" >
                            <tr>
                                <td>
                                    <telerik:RadLabel ID="RadLabel1" runat="server" Text="Concepto"></telerik:RadLabel>
                                </td>
                                <td>
                                    <telerik:RadLabel ID="radLabelConcepto" runat="server" Text="" BackColor="transparent" Width="50px" ></telerik:RadLabel>
                                    <telerik:RadLabel ID="radLabelConceptoDes" runat="server" Text="" BackColor="transparent" Width="500px"  ></telerik:RadLabel>
                                </td>
                            </tr>
                        </table>
                     </div>
                </fieldset>
                    <fieldset style="  margin-top:5px;   display: block; text-align:left;" >
                        <div style="width:100%; display:table; position:static; background-color:transparent;" >
                            <table border="0" style=" text-align:left; background-color:transparent; width:100%" >
                                <tr>
                                    <td>
                                        <telerik:RadLabel ID="RadLabel5" runat="server" Text="Movimiento" Width="130px" BackColor="transparent"></telerik:RadLabel>
                                        <asp:RadioButton ID="MoviCargo" runat="server" GroupName="Movimiento" Text="Cargo" Checked="true" Width="80px" BackColor="transparent" Enabled="false"  />
                                        <asp:RadioButton ID="MoviAbono" runat="server" GroupName="Movimiento" Text="Abono"   Enabled="false" BackColor="transparent" Width="100px" />
                                    </td>
                                
                                    <td>
                                        <telerik:RadLabel ID="RadLabel2" runat="server" Text="Sustituciones" Width="80px" BackColor="transparent"></telerik:RadLabel>
                                        <asp:RadioButton ID="SustitucionesSI" runat="server" GroupName="Sustituciones" Text="Si" Checked="true" Width="50px" BackColor="transparent" Enabled="false"  />
                                        <asp:RadioButton ID="SustitucionesNO" runat="server" GroupName="Sustituciones" Text="No"   Enabled="false" BackColor="transparent" Width="100px" />
                                    </td>
                                </tr>
                                    <td>
                                        <telerik:RadLabel ID="RadLabel3" runat="server" Text="Codificación Cont." Width="130px" BackColor="transparent"></telerik:RadLabel>
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
                                                                            <%# DataBinder.Eval(Container.DataItem, "ctaContCveFotmat")%>
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
                                     <td colspan="3">
                                            <telerik:RadLabel ID="RadLabel4" runat="server" Text="Descripción" Width="80px" BackColor="transparent"></telerik:RadLabel>
                                         <telerik:RadTextBox ID="rTxtDescripcion" runat="server" Width="230px"
                                                    EnabledStyle-CssClass="cssTxtEnabled"
                                                    DisabledStyle-CssClass ="cssTxtEnabled"
                                                    HoveredStyle-CssClass="cssTxtHovered"
                                                    FocusedStyle-CssClass="cssTxtFocused"
                                                    InvalidStyle-CssClass="cssTxtInvalid">
                                         </telerik:RadTextBox>
                                        </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        
                                    </td>
                                </tr>
                            </table>
                         </div>
                    </fieldset>
                   <fieldset style="  margin-top:5px;   display: block; text-align:left;" >
                        <legend>Referencias</legend>
                        <div style="width:100%; display:table; position:static; background-color:transparent;" >

                            <table border="0" style=" text-align:left; background-color:transparent; width:100%" >
                                <tr>
                                    <td style="width:110px; background-color:transparent;">
                                         <telerik:RadLabel ID="RadLabel6" runat="server" Text="Referencia 1" Width="120px"  BackColor="transparent"></telerik:RadLabel>
                                   </td>
                                    <td style="width:100px; background-color:transparent;">

                                          <telerik:RadComboBox ID="rCboReferencia1" runat="server" Width="200px"  AllowCustomText="true"
                                            HighlightTemplatedItems="true" AutoPostBack="true"
                                          DropDownCssClass="cssRadComboBox" 
                                          DropDownWidth="260px" 
                                           Height="200px"  >
                                                <HeaderTemplate>
                                                    <table style="width: 150px" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 170px;">
                                                                    Descripción
                                                                </td>
                                                            </tr>
                                                        </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table style="width: 250px;"  cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width: 170px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "cptoConfDes") %>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                </FooterTemplate>
                                        </telerik:RadComboBox>
                                    </td>

                                    <td style="width:110px; background-color:transparent;">
                                         <telerik:RadLabel ID="RadLabel7" runat="server" Text="Referencia 2" Width="110px" BackColor="transparent" ></telerik:RadLabel>
                                        </td>
                                         <td style="width:150px; background-color:transparent;">
                                          <telerik:RadComboBox ID="rCboReferencia2" runat="server" Width="200px"  BackColor="transparent" AllowCustomText="true"
                                            HighlightTemplatedItems="true" AutoPostBack="true"
                                            DropDownCssClass="cssRadComboBox" 
                                            DropDownWidth="260px" 
                                            Height="200px"  >
                                                <HeaderTemplate>
                                                    <table style="width: 150px" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 170px;">
                                                                    Descripción
                                                                </td>
                                                            </tr>
                                                        </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table style="width: 250px;"  cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width: 170px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "cptoConfDes") %>
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
                                    <td style="width:110px; background-color:transparent;">
                                         <telerik:RadLabel ID="RadLabel8" runat="server" Text="Referencia 3" Width="120px"  BackColor="transparent"></telerik:RadLabel>
                                   </td>
                                    <td style="width:100px; background-color:transparent;">

                                          <telerik:RadComboBox ID="rCboReferencia3" runat="server" Width="200px"  AllowCustomText="true"
                                            HighlightTemplatedItems="true" AutoPostBack="true"
                                          DropDownCssClass="cssRadComboBox" 
                                          DropDownWidth="260px" 
                                           Height="200px"  >
                                                <HeaderTemplate>
                                                    <table style="width: 150px" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 170px;">
                                                                    Descripción
                                                                </td>
                                                            </tr>
                                                        </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table style="width: 250px;"  cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width: 170px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "cptoConfDes") %>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                </FooterTemplate>
                                        </telerik:RadComboBox>
                                    </td>

                                    <td style="width:110px; background-color:transparent;">
                                         <telerik:RadLabel ID="RadLabel9" runat="server" Text="Referencia 4" Width="110px" BackColor="transparent" ></telerik:RadLabel>
                                        </td>
                                         <td style="width:150px; background-color:transparent;">

                                        
                                    
                                          <telerik:RadComboBox ID="rCboReferencia4" runat="server" Width="200px"  BackColor="transparent" AllowCustomText="true"
                                            HighlightTemplatedItems="true" AutoPostBack="true"
                                            DropDownCssClass="cssRadComboBox" 
                                            DropDownWidth="260px" 
                                            Height="200px"  >
                                                <HeaderTemplate>
                                                    <table style="width: 150px" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 170px;">
                                                                    Descripción
                                                                </td>
                                                            </tr>
                                                        </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table style="width: 250px;"  cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width: 170px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "cptoConfDes") %>
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
                   <fieldset style="  margin-top:5px;   display: block; text-align:left;" >
                        <legend>Fechas</legend>
                        <div style="width:100%; display:table; position:static; background-color:transparent;" >

                            <table border="0" style=" text-align:left; background-color:transparent; width:100%" >
                                <tr>
                                    <td style="width:110px; background-color:transparent;">
                                         <telerik:RadLabel ID="RadLabel10" runat="server" Text="Movimiento" Width="120px"  BackColor="transparent"></telerik:RadLabel>
                                   </td>
                                    <td style="width:100px; background-color:transparent;">

                                          <telerik:RadComboBox ID="rCboMovimiento" runat="server" Width="200px"  AllowCustomText="true"
                                            HighlightTemplatedItems="true" AutoPostBack="true"
                                          DropDownCssClass="cssRadComboBox" 
                                          DropDownWidth="260px" 
                                           Height="200px"  >
                                                <HeaderTemplate>
                                                    <table style="width: 150px" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 170px;">
                                                                    Descripción
                                                                </td>
                                                            </tr>
                                                        </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table style="width: 250px;"  cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width: 170px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "cptoConfDes") %>
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
                                         <telerik:RadLabel ID="RadLabel11" runat="server" Text="Vencimiento" Width="115px" BackColor="transparent" ></telerik:RadLabel>
                                        </td>
                                         <td style="width:150px; background-color:transparent;">

                                        
                                    
                                          <telerik:RadComboBox ID="rCboVencimiento" runat="server" Width="200px"  BackColor="transparent" AllowCustomText="true"
                                            HighlightTemplatedItems="true" AutoPostBack="true"
                                            DropDownCssClass="cssRadComboBox" 
                                            DropDownWidth="260px" 
                                            Height="200px"  >
                                                <HeaderTemplate>
                                                    <table style="width: 150px" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 170px;">
                                                                    Descripción
                                                                </td>
                                                            </tr>
                                                        </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table style="width: 250px;"  cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width: 170px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "cptoConfDes") %>
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
                <fieldset style="  margin-top:5px;   display: block; text-align:left;" >
                      <legend>CCO/Tipo Cambio</legend>
                        <div style="width:100%; display:table; position:static; background-color:transparent;" >

                            <table border="0" style=" text-align:left; background-color:transparent; width:100%" >
                                <tr>
                                    <td style="width:100px; background-color:transparent;">
                                         <telerik:RadLabel ID="RadLabel12" runat="server" Text="Centro de Costo" Width="120px"  BackColor="transparent"></telerik:RadLabel>
                                   </td>
                                    <td style="width:100px; background-color:transparent;">
                                          <telerik:RadComboBox ID="rCboCentroCosto" runat="server" Width="200px"  AllowCustomText="true"
                                            HighlightTemplatedItems="true" AutoPostBack="true"
                                          DropDownCssClass="cssRadComboBox" 
                                          DropDownWidth="260px" 
                                           Height="200px"  >
                                                <HeaderTemplate>
                                                    <table style="width: 150px" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 170px;">
                                                                    Descripción
                                                                </td>
                                                            </tr>
                                                        </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table style="width: 250px;"  cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width: 170px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "cptoConfDes") %>
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

                                          <telerik:RadComboBox ID="rCboTipCambio" runat="server" Width="200px"  BackColor="transparent" AllowCustomText="true"
                                            HighlightTemplatedItems="true" AutoPostBack="true"
                                            DropDownCssClass="cssRadComboBox" 
                                            DropDownWidth="260px" 
                                            Height="200px"  >
                                                <HeaderTemplate>
                                                    <table style="width: 150px" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 170px;">
                                                                    Descripción
                                                                </td>
                                                            </tr>
                                                        </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table style="width: 250px;"  cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width: 170px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "cptoConfDes") %>
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
                                      <td style="width:115px; background-color:transparent;">
                                         <telerik:RadLabel ID="RadLabel14" runat="server" Text="Importe" Width="115px" BackColor="transparent" ></telerik:RadLabel>
                                        </td>
                                    <td style="width:150px; background-color:transparent;">

                                          <telerik:RadComboBox ID="rCboImporte" runat="server" Width="200px"  BackColor="transparent" AllowCustomText="true"
                                            HighlightTemplatedItems="true" AutoPostBack="true"
                                            DropDownCssClass="cssRadComboBox" 
                                            DropDownWidth="260px" 
                                            Height="200px"  >
                                                <HeaderTemplate>
                                                    <table style="width: 150px" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 170px;">
                                                                    Descripción
                                                                </td>
                                                            </tr>
                                                        </table>
                                                </HeaderTemplate>
                                              <%--  <ItemTemplate>
                                                    <table style="width: 250px;"  cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width: 170px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "cptoConfDes") %>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>--%>
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

          <telerik:RadGrid ID="rGdv_GuiaContable"  OnSelectedIndexChanged="rGdv_GuiaContable_SelectedIndexChanged"
                           runat="server" 
                           AllowMultiRowSelection="true"
                           AutoGenerateColumns="False" 
                           Width="727px" Height="155px"   
                           CssClass="Grid"  
                           Skin="Office2010Silver">

                <MasterTableView DataKeyNames="cptoGuiContId"  AutoGenerateColumns="false" CssClass="GridTable"     >
                    <Columns> 
                        <telerik:GridBoundColumn HeaderText="Movimiento"  DataField="cptoGuiContCoA"   HeaderStyle-Width="50px"  ItemStyle-Width="100px" ></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Codificación Cont."  DataField="ctaContNom"   HeaderStyle-Width="80px"  ItemStyle-Width="100px" ></telerik:GridBoundColumn>      
                        <telerik:GridBoundColumn HeaderText="Sustituciones"  DataField="cptoGuiContSus"   HeaderStyle-Width="60px"  ItemStyle-Width="100px" ></telerik:GridBoundColumn>  
                        <telerik:GridBoundColumn HeaderText="Descripción"  DataField="cptoGuiContDes"   HeaderStyle-Width="140px"  ItemStyle-Width="100px" ></telerik:GridBoundColumn>   
                        <telerik:GridBoundColumn HeaderText="Sustituciones"  DataField="cptoGuiContSus"   HeaderStyle-Width="140px"  ItemStyle-Width="100px" Display="false"  ></telerik:GridBoundColumn>                                                    
                    </Columns>
                    <NoRecordsTemplate>No se encontraron registros.</NoRecordsTemplate>
                </MasterTableView>

                    <HeaderStyle CssClass="GridHeaderStyle"/>
                    <ItemStyle CssClass="GridRowStyle"/>
                    <AlternatingItemStyle  CssClass="GridAlternatingRowStyle" />
                    <selecteditemstyle CssClass="GridSelectedItem"/>
                    <FooterStyle CssClass="GridFooterStyle" />

                    <ClientSettings Selecting-AllowRowSelect="true"  EnablePostBackOnRowClick="true" >
                        <Selecting  AllowRowSelect="true" EnableDragToSelectRows="False" ></Selecting>
                        <Scrolling AllowScroll="true" UseStaticHeaders="true"    ScrollHeight="280px"     />
                    </ClientSettings>
            </telerik:RadGrid>
        </div>  
       </div>
            <div style="width:100%; display:table; position:static; background-color:transparent;">
            <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
                <telerik:RadImageButton ID="rBtnGuardar"  runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text ="" OnClick="rBtnGuardar_Click"  OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text="" OnClick="rBtnCancelar_Click"  OnClientClicking="OnClientClic_ConfirmCancel" ></telerik:RadImageButton>
            </asp:Panel>
            </div>
          <asp:HiddenField ID="hdfBtnAccion" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
            <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
    </telerik:RadWindowManager>
    </form>
</body>
</html>
