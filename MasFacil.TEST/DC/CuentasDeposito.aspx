<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CuentasDeposito.aspx.cs" Inherits="DC_CuentasDeposito" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Scripts/JSGeneral.js" type="text/javascript"></script>      
    <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
    <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
    <link href="~/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />
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
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
                    <telerik:RadImageButton ID="rBtnNuevo"     runat ="server" Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text ="" Visible="false" OnClick="rBtnNuevo_Click" ></telerik:RadImageButton> <%--OnClick="rBtnNuevo_Click"--%>
                    <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  ToolTip="Modificar"  Text="" Visible="false" OnClick="rBtnModificar_Click"  ></telerik:RadImageButton> <%--OnClick="rBtnModificar_Click"--%>
                    <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text="" Visible="false" OnClick="rBtnEliminar_Click" OnClientClicking="OnClientClic_ConfirmOK" ></telerik:RadImageButton> <%--OnClick="rBtnEliminar_Click"--%>
                    <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"  Text=""   OnClick="rBtnLimpiar_Click" ></telerik:RadImageButton> <%--OnClick="rBtnLimpiar_Click"--%>
                </asp:Panel>
                <fieldset>
                    <legend>Dirección</legend>
                   <table>
                        <tr>
                            <td>
                                <telerik:RadLabel ID="RadLabel1" runat="server" Text="Clave Cuenta" Width="100px"></telerik:RadLabel>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="rtxtClaveCuenta" runat="server" Enabled="false"
                                     EnabledStyle-CssClass="cssTxtEnabled"
                                     DisabledStyle-CssClass ="cssTxtEnabled"
                                     HoveredStyle-CssClass="cssTxtHovered"
                                     FocusedStyle-CssClass="cssTxtFocused"
                                     InvalidStyle-CssClass="cssTxtInvalid"
                                     MaxLength="10"
                                     ></telerik:RadTextBox>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadLabel ID="RadLabel2" runat="server" Text="Descripción" Width="98px"></telerik:RadLabel>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="rtTxtDescripcion" runat="server" Width="433px" Enabled="false"
                                    EnabledStyle-CssClass="cssTxtEnabled"
                                     DisabledStyle-CssClass ="cssTxtEnabled"
                                     HoveredStyle-CssClass="cssTxtHovered"
                                     FocusedStyle-CssClass="cssTxtFocused"
                                     InvalidStyle-CssClass="cssTxtInvalid"
                                     MaxLength="100"
                                     ></telerik:RadTextBox>
                            </td>
                        </tr>
                       <tr>
                            <td>
                                <telerik:RadLabel ID="RadLabel3" runat="server" Text="Institución"></telerik:RadLabel>
                            </td>
                            <td>

                                <telerik:RadComboBox ID="rCboInstitucion" runat="server"  Enabled="false"
                                AutoPostBack="true"
                                 HighlightTemplatedItems="true"
                                 DropDownCssClass="cssRadComboBox"  
                                 Width="160px" DropDownWidth="280px" Height="200px"
                                   
                                    >
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
                                                    <%# DataBinder.Eval(Container.DataItem, "insDepCve")%>
                                                </td>
                                                <td style="width: 170px;">
                                                    <%# DataBinder.Eval(Container.DataItem, "insDepDes") %>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                    </FooterTemplate>
                                </telerik:RadComboBox>


                                <telerik:RadLabel ID="RadLabel4" runat="server" Text="Sucursal" BorderColor="Red" Width="98px"></telerik:RadLabel>
                                <telerik:RadTextBox ID="rTxtSucursal" runat="server" Enabled="false"
                                    EnabledStyle-CssClass="cssTxtEnabled"
                                     DisabledStyle-CssClass ="cssTxtEnabled"
                                     HoveredStyle-CssClass="cssTxtHovered"
                                     FocusedStyle-CssClass="cssTxtFocused"
                                     InvalidStyle-CssClass="cssTxtInvalid"
                                      MaxLength="50"
                                     ></telerik:RadTextBox>

                            </td>
                        </tr>
                         <tr>
                            <td>
                                <telerik:RadLabel ID="RadLabel5" runat="server" Text="Número Cuenta" Width="110px"></telerik:RadLabel>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="rTxtNumCuenta" runat="server" EnabledStyle-CssClass="cssTxtEnabled" Enabled="false"
                                     DisabledStyle-CssClass ="cssTxtEnabled"
                                     HoveredStyle-CssClass="cssTxtHovered"
                                     FocusedStyle-CssClass="cssTxtFocused"
                                     InvalidStyle-CssClass="cssTxtInvalid"
                                     MaxLength="30"
                                     ></telerik:RadTextBox>
                                <telerik:RadLabel ID="RadLabel6" runat="server" Text="Folio" Width="100px"></telerik:RadLabel>
                                <telerik:RadTextBox ID="rTxtFolio" runat="server" Enabled="false"
                                    EnabledStyle-CssClass="cssTxtEnabled"
                                     DisabledStyle-CssClass ="cssTxtEnabled"
                                     HoveredStyle-CssClass="cssTxtHovered"
                                     FocusedStyle-CssClass="cssTxtFocused"
                                     InvalidStyle-CssClass="cssTxtInvalid"
                                     MaxLength="20"
                                     ></telerik:RadTextBox>
                            </td>
                            <td>
                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadLabel ID="RadLabel7" runat="server" Text="Moneda"></telerik:RadLabel>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rCboMoneda" runat="server" Enabled="false"
                                AutoPostBack="true"
                                 HighlightTemplatedItems="true"
                                 DropDownCssClass="cssRadComboBox"  
                                 Width="160px" DropDownWidth="280px" Height="200px" OnSelectedIndexChanged="rCboFormato_SelectedIndexChanged"
                                   
                                    >
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

                                 <telerik:RadLabel ID="RadLabel11" runat="server" Text="Estado Emisión" BorderColor="Red" Width="98px"></telerik:RadLabel>
                                <telerik:RadTextBox ID="rTxtEstadoEmision" runat="server" Enabled="false"
                                    EnabledStyle-CssClass="cssTxtEnabled"
                                     DisabledStyle-CssClass ="cssTxtEnabled"
                                     HoveredStyle-CssClass="cssTxtHovered"
                                     FocusedStyle-CssClass="cssTxtFocused"
                                     InvalidStyle-CssClass="cssTxtInvalid"
                                      MaxLength="50"
                                     ></telerik:RadTextBox>

                            </td>
                        </tr>
                    </table>


                </fieldset>

                 <fieldset>
                    <legend>Formato de Cheque</legend>
                    <table>
                        <tr>
                            <td>
                                <telerik:RadLabel ID="RadLabel8" runat="server" Text="Formato" Width="110px"></telerik:RadLabel>
                            </td>
                            <td>
                                <%--<telerik:RadComboBox ID="rCboFormato" runat="server" Enabled="false"></telerik:RadComboBox>--%>
                                <telerik:RadComboBox ID="rCboFormato" runat="server" Enabled="false"
                                AutoPostBack="true"
                                 HighlightTemplatedItems="true"
                                 DropDownCssClass="cssRadComboBox"  
                                 Width="160px" DropDownWidth="280px" Height="200px" OnSelectedIndexChanged="rCboFormato_SelectedIndexChanged"
                                   
                                    >
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
                                                    <%# DataBinder.Eval(Container.DataItem, "listPreValInt")%>
                                                </td>
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





                                <telerik:RadLabel ID="RadLabel10" runat="server" Text="Layout Imp." Width="100px"></telerik:RadLabel>
                                <telerik:RadComboBox ID="rNoFormato" runat="server" Enabled="false"
                                AutoPostBack="true"
                                 HighlightTemplatedItems="true"
                                 DropDownCssClass="cssRadComboBox"  
                                 Width="160px" DropDownWidth="370px" Height="230px" OnSelectedIndexChanged="rNoFormato_SelectedIndexChanged"
                                   
                                    >
                                    <HeaderTemplate>
                                        <table style="width: 350px" cellspacing="0" cellpadding="0">
                                              <tr>    
                                                <td style="width: 80px;">
                                                    Clave
                                                </td>
                                                <td style="width: 100px;">
                                                    Descripción
                                                </td>
                                                <td style="width: 170px;">
                                                    Formato
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table style="width: 350px;"  cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="width:80px" >
                                                    <%# DataBinder.Eval(Container.DataItem, "formImpCve")%>
                                                </td>
                                                <td style="width:100px" >
                                                    <%# DataBinder.Eval(Container.DataItem, "formImpDes")%>
                                                </td>
                                                <td style="width: 170px;">
                                                    <asp:Image runat="server" Width="150px" Height="80px" ImageUrl ='<%# DataBinder.Eval(Container.DataItem, "formImpImg") %>'  />
                                                   
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
                                <telerik:RadLabel ID="RadLabel9" runat="server" Text="Leyenda"></telerik:RadLabel>
                            </td>
                            <td>
                                <textarea id="txtAreaLeyenda" cols="51" rows="2" disabled="disabled" name="txtAreaLeyenda" runat="server" maxlength="200" ></textarea>
                                
                             </td>
                        </tr>
                    </table>
                </fieldset>



                 <div style="width:100%; margin-top:5px; display:table; position:static; background-color:transparent;" align="center" >  
                <telerik:RadGrid  BorderColor="Gray" ID="RGridCuentasDeposito" runat="server" 
                                     AutoGenerateColumns="False" 
                                   Width="750px"   Height="170px"  
                                   CssClass="Grid" 
                                   Skin="Office2010Silver" OnSelectedIndexChanged="RGridCuentasDeposito_SelectedIndexChanged">
                        <MasterTableView DataKeyNames="ctaDepCve" AutoGenerateColumns="false" CssClass="GridTable">
                          
                            <Columns>
                                
                                <telerik:GridBoundColumn HeaderText="Clave"    HeaderStyle-Width="55px"         DataField="ctaDepCve"             ></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Descripción"      HeaderStyle-Width="120px"     DataField="ctaDepDes"             ></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Institución"  HeaderStyle-Width="135px"      DataField="insDepDes"             ></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Sucursal"   HeaderStyle-Width="150px"       DataField="ctaDepSuc"             ></telerik:GridBoundColumn>

                                <telerik:GridBoundColumn HeaderText="N. De Cuenta"  HeaderStyle-Width="110px"    DataField="ctaDepNoCta"            ></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Moneda"          HeaderStyle-Width="80px"   DataField="inMonDes"           ></telerik:GridBoundColumn>

                                <telerik:GridBoundColumn HeaderText="Edo. Emisión"          HeaderStyle-Width="80px"   DataField="EdoEmision"           ></telerik:GridBoundColumn>
                              
                                 <%--  DataType="System.Decimal" DataFormatString="{0:N}"   ItemStyle-HorizontalAlign="Right" --%>
                           </Columns>
                            <NoRecordsTemplate> No se encontraron registros.</NoRecordsTemplate>
                        </MasterTableView>
                    <HeaderStyle CssClass="GridHeaderStyle"/>
                    <ItemStyle CssClass="GridRowStyle"/>
                    <AlternatingItemStyle  CssClass="GridAlternatingRowStyle" />
                    <selecteditemstyle CssClass="GridSelectedItem"></selecteditemstyle>
 
                    <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true" >
                        <Selecting AllowRowSelect="true" EnableDragToSelectRows="False" ></Selecting>
                        <Scrolling AllowScroll="true" UseStaticHeaders="true"  ScrollHeight="310px"    />
                    </ClientSettings>
                </telerik:RadGrid>
                </div>

                    <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
                       <telerik:RadImageButton ID="rBtnGuardar" Enabled="false"    runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text ="" OnClick="rBtnGuardar_Click" OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
                       <telerik:RadImageButton ID="rBtnCancelar" Enabled="false" runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text="" OnClick="rBtnCancelar_Click" OnClientClicking="OnClientClic_ConfirmCancel"></telerik:RadImageButton>
                   </asp:Panel>
                   <asp:HiddenField ID="hdfBtnAccion" runat="server" />
                   <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server"></telerik:RadWindowManager>
            </ContentTemplate>
        </asp:UpdatePanel>
  
    </div>
    </form>
</body>
</html>
