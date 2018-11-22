<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MttoCptoGuiasContabilizacionSus.aspx.cs" Inherits="DC_MttoCptoGuiasContabilizacionSus" %>

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
                    <telerik:RadImageButton ID="rBtnNuevo"     runat ="server" Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text =""  OnClick="rBtnNuevo_Click" Visible="false" ></telerik:RadImageButton>
                    <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  ToolTip="Modificar"  Text="" OnClick="rBtnModificar_Click" Visible="false" ></telerik:RadImageButton>
                    <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text=""  OnClick="rBtnEliminar_Click" OnClientClicking="OnClientClic_ConfirmOK" Visible="false" ></telerik:RadImageButton>
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
                <%-------%>
                <fieldset style="  margin-top:5px;   display: block; text-align:left;" >
                    <div style="width:100%; display:table; position:static; background-color:transparent;" >
                        <table border="0" style=" text-align:left; background-color:transparent; width:100%" >
                            <tr>
                                <td style="width:150px" >
                                    <telerik:RadLabel ID="RadLabel2" runat="server" Text="Codificación Cont." ></telerik:RadLabel>
                                </td>
                                <td colspan="3">
                                    <telerik:RadLabel ID="lblCta" runat="server" Text="" BackColor="transparent" Width="390px"  ></telerik:RadLabel>
                                </td>
                            </tr>
                                <tr>
                                    <td style="width:150px;">
                                         <telerik:RadLabel ID="RadLabel11" runat="server" Text="Tipo Referencia" Width="120px"  BackColor="transparent"></telerik:RadLabel>
                                   </td>
                                    <td>
                                          <telerik:RadComboBox ID="rCboTipoReferecia" runat="server" Width="200px"  AllowCustomText="true"
                                            HighlightTemplatedItems="true" AutoPostBack="true"  OnSelectedIndexChanged="rCboTipoReferecia_SelectedIndexChanged"
                                          DropDownCssClass="cssRadComboBox"
                                          DropDownWidth="290px" 
                                           Height="200px"  >
                                                <HeaderTemplate>
                                                    <table style="width: 200px" cellspacing="0" cellpadding="0">
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
                                                                <%# DataBinder.Eval(Container.DataItem, "listPreValDes") %>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                </FooterTemplate>
                                        </telerik:RadComboBox>
                                    </td>

                                    <td>
                                         <telerik:RadLabel ID="RadLabel12" runat="server" Text="Sec. Referencia" Width="115px" BackColor="transparent" ></telerik:RadLabel>
                                        </td>
                                         <td style="width:150px; background-color:transparent;">

                                        
                                    
                                          <telerik:RadComboBox ID="rCboSecReferencia" runat="server" Width="200px"  BackColor="transparent" AllowCustomText="true"
                                            HighlightTemplatedItems="true" AutoPostBack="true"  Enabled="false"
                                            DropDownCssClass="cssRadComboBox" 
                                            DropDownWidth="205px" 
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
                                    <td>
                                         <telerik:RadLabel ID="RadLabel4" runat="server" Text="Agrupación" Width="100px"  BackColor="transparent"></telerik:RadLabel>
                                   </td>
                                    <td>
                                          <telerik:RadComboBox ID="rCboAgrupacion" runat="server" Width="200px"  AllowCustomText="true"
                                            HighlightTemplatedItems="true" AutoPostBack="true" Enabled="false"
                                          DropDownCssClass="cssRadComboBox" 
                                          DropDownWidth="230px" 
                                           Height="200px"  >
                                                <HeaderTemplate>
                                                        <table style="width: 300px" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td style="width: 80px;">
                                                                        Clave
                                                                    </td>
                                                                    <td style="width: 220px;">
                                                                        Descripción
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                    </HeaderTemplate>
                                              <ItemTemplate>
                                                        <table style="width: 300px;"  cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width:80px" >
                                                                    <%# DataBinder.Eval(Container.DataItem, "agrCve")%>
                                                                </td>
                                                                <td style="width: 220px;">
                                                                    <%# DataBinder.Eval(Container.DataItem, "agrDes") %>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                </FooterTemplate>
                                        </telerik:RadComboBox>
                                    </td>

                                    <td>
                                         <telerik:RadLabel ID="RadLabel8" runat="server" Text="Colección" Width="115px" BackColor="transparent" ></telerik:RadLabel>
                                        </td>
                                        <td>
                                          <telerik:RadComboBox ID="rCboColeccion" runat="server" Width="200px"  BackColor="transparent" AllowCustomText="true"
                                            HighlightTemplatedItems="true" AutoPostBack="true" 
                                            DropDownCssClass="cssRadComboBox" Enabled="false"
                                            DropDownWidth="205px" 
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
                                                <%--<ItemTemplate>
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
                <%--Posiciones--%>
                <fieldset style="  margin-top:5px;   display: block; text-align:left;" >
                    <legend>Posiciones</legend>
                    <div style="width:100%; display:table; position:static; background-color:transparent;" >
                        <table border="0" style=" text-align:left; background-color:transparent; width:100%" >
                            <tr>
                                <td style="width:39%;">
                                    <telerik:RadLabel ID="RadLabel5" runat="server" Text="Origen" Width="40px" ></telerik:RadLabel>
                                           <telerik:RadNumericTextBox  runat="server" ID="RadNumInicial" Width="60px"  MinValue="1" MaxValue ="60"
                                            EnabledStyle-CssClass="cssTxtEnabled" AutoPostBack="true"
                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                            HoveredStyle-CssClass="cssTxtHovered"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid" 
                                            ShowSpinButtons="true" NumberFormat-DecimalDigits="0">
                                       </telerik:RadNumericTextBox>
                                </td>
                                <td style="width:31%;">
                                    <telerik:RadLabel ID="radLabel6" runat="server" Text="Destino" BackColor="transparent" Width="43px" ></telerik:RadLabel>
                                    <telerik:RadNumericTextBox  runat="server" ID="RadNumFinal" Width="60px"   MinValue="1" MaxValue ="60"
                                            EnabledStyle-CssClass="cssTxtEnabled"  AutoPostBack="true"
                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                            HoveredStyle-CssClass="cssTxtHovered"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid" 
                                            ShowSpinButtons="true" NumberFormat-DecimalDigits="0">
                                       </telerik:RadNumericTextBox>
                                </td>
                                <td style="width:21%;">
                                    <telerik:RadLabel ID="radLabel7" runat="server" Text="Posiciones" BackColor="transparent" Width="70px" ></telerik:RadLabel>
                                    <telerik:RadNumericTextBox  runat="server" ID="RadNumPosiciones" Width="60px"   MinValue="1" MaxValue ="100"
                                            EnabledStyle-CssClass="cssTxtEnabled"
                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                            HoveredStyle-CssClass="cssTxtHovered"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid" 
                                            ShowSpinButtons="true" NumberFormat-DecimalDigits="0">
                                       </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                        </table>
                     </div>
                </fieldset>
  <div style="width:100%; display:table; margin-top:5px; position:static; background-color:transparent;" align="center">  
          <telerik:RadGrid ID="rGdv_GuiaContableSus"  OnSelectedIndexChanged="rGdv_GuiaContable_SelectedIndexChanged"
                           runat="server" 
                           AllowMultiRowSelection="true"
                           AutoGenerateColumns="False" 
                           Width="727px" Height="310px"   
                           CssClass="Grid"  
                           Skin="Office2010Silver">

                <MasterTableView DataKeyNames="cptoGuiContSusId"  AutoGenerateColumns="false" CssClass="GridTable"    >
                    <Columns> 
                        
                        <telerik:GridBoundColumn HeaderText="Tipo Referencia"  DataField="listPreValDes"   HeaderStyle-Width="150px"  ItemStyle-Width="150px" ></telerik:GridBoundColumn>   
                        <telerik:GridBoundColumn HeaderText="Origen"  DataField="cptoGuiContSusPosIni"   HeaderStyle-Width="45px"  ItemStyle-Width="50px" ></telerik:GridBoundColumn>      
                        <telerik:GridBoundColumn HeaderText="Destino"  DataField="cptoGuiContSusPosFin"   HeaderStyle-Width="45px"  ItemStyle-Width="50px" ></telerik:GridBoundColumn>  
                        <telerik:GridBoundColumn HeaderText="Posiciones"  DataField="cptoGuiContSusPosNum"   HeaderStyle-Width="50px"  ItemStyle-Width="50px" ></telerik:GridBoundColumn>   
                        <telerik:GridBoundColumn HeaderText="Ref/Agr/Col"  DataField="Ref_Sec_agrCve_colCve"   HeaderStyle-Width="120px"  ItemStyle-Width="120px" ></telerik:GridBoundColumn>                                                    
                    </Columns>
                    <NoRecordsTemplate>No se encontraron registros.</NoRecordsTemplate>
                </MasterTableView>

                    <HeaderStyle CssClass="GridHeaderStyle"/>
                    <ItemStyle CssClass="GridRowStyle"/>
                    <AlternatingItemStyle  CssClass="GridAlternatingRowStyle" />
                    <selecteditemstyle CssClass="GridSelectedItem"/>
                    <FooterStyle CssClass="GridFooterStyle" />

                    <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true" >
                        <Selecting AllowRowSelect="true" EnableDragToSelectRows="False" ></Selecting>
                        <Scrolling AllowScroll="true" UseStaticHeaders="true"    ScrollHeight="280px"     />
                    </ClientSettings>
            </telerik:RadGrid>
        </div> 
      </div>
            <div style="width:100%; display:table; position:static; background-color:transparent;">
                <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
                    <telerik:RadImageButton ID="rBtnGuardar"  runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text =""  OnClick="rBtnGuardar_Click" OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
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
