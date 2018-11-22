<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MttoMttoFilios.aspx.cs" Inherits="DC_MttoMttoFilios" %>

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
                <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  ToolTip="Modificar"  Text=""  OnClick="rBtnModificar_Click" Visible="false" ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text=""   OnClick="rBtnEliminar_Click" OnClientClicking="OnClientClic_ConfirmOK" Visible="false" ></telerik:RadImageButton>
                    <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"  Text=""  OnClick="rBtnLimpiar_Click" ></telerik:RadImageButton>       
            </asp:Panel>
            <fieldset style="  margin-top:5px;   display: block; text-align:left;" >
                <div style="width:100%; display:table; position:static; background-color:transparent;" >
                        <table border="0" style=" text-align:left; background-color:transparent; width:100%" >
                            <tr>
                                <td >
                                    <telerik:RadLabel ID="RadLabel1" runat="server" Text="Folio"></telerik:RadLabel>
                                </td>
                                <td colspan="3">
                                     <telerik:RadComboBox ID="rCboManFol" width="190px" runat="server"
                                          HighlightTemplatedItems="true"  OnSelectedIndexChanged="rCboManFol_SelectedIndexChanged"
                                         
                                         DropDownWidth="290px" 
                                         Height="200px" 
                                         DropDownCssClass="cssRadComboBox" AutoPostBack="True"   >            
                                      <HeaderTemplate>
                                            <table style="width: 190px" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td style="width: 190px;">
                                                            Descripción
                                                        </td>
                                                    </tr>
                                                </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                                <table style="width: 170px;"  cellspacing="0" cellpadding="0">
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
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadLabel ID="RadLabel2" runat="server" Text="Clave"></telerik:RadLabel>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="rTxtClave" runat="server" Width="80px" Enabled="false" MaxLength="10"
                                        EnabledStyle-CssClass="cssTxtEnabled"
                                        DisabledStyle-CssClass ="cssTxtEnabled"
                                        HoveredStyle-CssClass="cssTxtHovered"
                                        FocusedStyle-CssClass="cssTxtFocused"
                                        InvalidStyle-CssClass="cssTxtInvalid"
                                        ></telerik:RadTextBox>
                                </td>
                                <td>
                                    <telerik:RadLabel ID="RadLabel3" runat="server" Text="Descripción" Width="90px"></telerik:RadLabel>
                                </td>
                                <td >
                                    <telerik:RadTextBox ID="rTxtDescripcion" runat="server" Enabled="false" Width="250px"
                                        EnabledStyle-CssClass="cssTxtEnabled"
                                        DisabledStyle-CssClass ="cssTxtEnabled"
                                        HoveredStyle-CssClass="cssTxtHovered"
                                        FocusedStyle-CssClass="cssTxtFocused"
                                        InvalidStyle-CssClass="cssTxtInvalid">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <telerik:RadLabel ID="rdlblTipo" runat="server" Text="Tipo"></telerik:RadLabel>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rCmboTipo" runat="server" Width="200px"  AllowCustomText="false" Enabled="false"
                                            HighlightTemplatedItems="true" AutoPostBack="true" OnSelectedIndexChanged="rCmboTipo_SelectedIndexChanged"
                                          DropDownCssClass="cssRadComboBox" 
                                          DropDownWidth="260px" 
                                           Height="200px"  >          
                                      <HeaderTemplate>
                                            <table style="width: 190px" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td style="width: 190px;">
                                                            Descripción
                                                        </td>
                                                    </tr>
                                                </table>
                                        </HeaderTemplate>
                                        <%--<ItemTemplate>
                                                <table style="width: 170px;"  cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td style="width: 170px;">
                                                                    <%# DataBinder.Eval(Container.DataItem, "listPreValDes") %>
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
                            <tr>
                                <td>
                                    <telerik:RadLabel ID="rdlblLongitud" runat="server" Text="Longitud" Visible="false"></telerik:RadLabel>
                                    <telerik:RadLabel ID="rdlblValor" runat="server" Text="Valor" Visible="true"></telerik:RadLabel>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="rTxtValor" runat="server" Width="80px" Enabled="false" Visible="false"
                                        EnabledStyle-CssClass="cssTxtEnabled"
                                        DisabledStyle-CssClass ="cssTxtEnabled"
                                        HoveredStyle-CssClass="cssTxtHovered"
                                        FocusedStyle-CssClass="cssTxtFocused"
                                        InvalidStyle-CssClass="cssTxtInvalid"
                                        ></telerik:RadTextBox>

                                    <telerik:RadNumericTextBox ID="rdNumerico" runat="server" Width="80px" Enabled="false" Visible="true"
                                        EnabledStyle-CssClass="cssTxtEnabled" 
                                        DisabledStyle-CssClass ="cssTxtEnabled"
                                        HoveredStyle-CssClass="cssTxtHovered"
                                        FocusedStyle-CssClass="cssTxtFocused"
                                        InvalidStyle-CssClass="cssTxtInvalid">
                                         <NumberFormat GroupSeparator="" DecimalDigits="0" />
                                    </telerik:RadNumericTextBox>

                                  <%--  <telerik:RadTextBox ID="rTxtLongitud" runat="server" Width="80px" Enabled="false" Visible="false"
                                        EnabledStyle-CssClass="cssTxtEnabled"
                                        DisabledStyle-CssClass ="cssTxtEnabled"
                                        HoveredStyle-CssClass="cssTxtHovered"
                                        FocusedStyle-CssClass="cssTxtFocused"
                                        InvalidStyle-CssClass="cssTxtInvalid"
                                        ></telerik:RadTextBox>--%>
                                    <telerik:RadNumericTextBox ID="rTxtLongitud" runat="server" Width="80px" Enabled="false" Visible="false"
                                        EnabledStyle-CssClass="cssTxtEnabled" NumberFormat-DecimalDigits="0" MaxLength="2" MaxValue ="10"
                                        DisabledStyle-CssClass ="cssTxtEnabled"
                                        HoveredStyle-CssClass="cssTxtHovered"
                                        FocusedStyle-CssClass="cssTxtFocused"
                                        InvalidStyle-CssClass="cssTxtInvalid"></telerik:RadNumericTextBox>

                                </td>
                                <td>
                                    <telerik:RadLabel ID="rdlblAlineacion" runat="server" Text="Alineación" Width="80px" BackColor="transparent" Visible="false"></telerik:RadLabel>
                                    <telerik:RadLabel ID="rdlblFormato" runat="server" Text="Formato" Width="80px" BackColor="transparent" Visible="true"></telerik:RadLabel>
                                </td>
                                <td >


                                    <asp:RadioButton ID="AlineaIzquierda" runat="server" GroupName="Alineacion" Text="Izquierda"   Enabled="false" BackColor="transparent" Width="100px"  Visible="false" />
                                    <asp:RadioButton ID="AlineaDerecha" runat="server" GroupName="Alineacion" Text="Derecha" Checked="true" Width="100px" BackColor="transparent" Enabled="false"  Visible="false" />
                                   
                                    <telerik:RadComboBox ID="rCmboFormato" runat="server" Width="200px"  AllowCustomText="true" Visible="true" Enabled="false"
                                            HighlightTemplatedItems="true" AutoPostBack="true"
                                          DropDownCssClass="cssRadComboBox" 
                                          DropDownWidth="290px" 
                                           Height="200px"  >          
                                      <HeaderTemplate>
                                            <table style="width: 290px" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td style="width: 100px;">
                                                            Clave
                                                        </td>
                                                        <td style="width: 170px;">
                                                            Descripción
                                                        </td>
                                                    </tr>
                                                </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table style="width: 290px;"  cellspacing="0" cellpadding="0">
                                                <tr>
                                                        <td style="width: 100px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "Clave") %>
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
                                <td>
                                    <telerik:RadLabel ID="rdlblCaracter" runat="server" Text="Caracter" Visible="false"></telerik:RadLabel>
                                    <telerik:RadLabel ID="rdlblPrefijo" runat="server" Text="Prefijo" Visible="true"></telerik:RadLabel>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="rTxtCaracter" runat="server" Visible="false" Enabled="false" MaxLength="1"
                                        EnabledStyle-CssClass="cssTxtEnabled"
                                        DisabledStyle-CssClass ="cssTxtEnabled"
                                        HoveredStyle-CssClass="cssTxtHovered"
                                        FocusedStyle-CssClass="cssTxtFocused"
                                        InvalidStyle-CssClass="cssTxtInvalid">
                                    </telerik:RadTextBox>
                                    <telerik:RadTextBox ID="rTxtPrefijo" runat="server"  Visible="true" Width="60px" Enabled="false"  MaxLength="5"
                                        EnabledStyle-CssClass="cssTxtEnabled"
                                        DisabledStyle-CssClass ="cssTxtEnabled"
                                        HoveredStyle-CssClass="cssTxtHovered"
                                        FocusedStyle-CssClass="cssTxtFocused"
                                        InvalidStyle-CssClass="cssTxtInvalid">
                                    </telerik:RadTextBox>
                                    <telerik:RadLabel ID="rdlblSubfijo" runat="server" Text="Subfijo" Visible="true" ></telerik:RadLabel>

                                    <telerik:RadTextBox ID="rTxtSubfijo" runat="server" Visible="true" Width="60px" Enabled="false" MaxLength="5"
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
          <div style="width:100%; display:table; margin-top:5px; position:static; background-color:transparent;" align="center">  
          <telerik:RadGrid ID="rGdv_FolioAutomatico"  OnSelectedIndexChanged="rGdv_FolioAutomatico_SelectedIndexChanged" 
                           runat="server" 
                           AllowMultiRowSelection="true"
                           AutoGenerateColumns="False" 
                           Width="782px" Height="190px"   
                           CssClass="Grid"  
                           Skin="Office2010Silver">

                <MasterTableView DataKeyNames="folCve"  AutoGenerateColumns="false" CssClass="GridTable"     >
                    <Columns> 
                        <telerik:GridBoundColumn HeaderText="Clave"  DataField="folCve"   HeaderStyle-Width="60px"  ItemStyle-Width="60px" ></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Descripción"  DataField="folioDes"   HeaderStyle-Width="140px"  ItemStyle-Width="140px" ></telerik:GridBoundColumn>      
                        <telerik:GridBoundColumn HeaderText="Tipo"  DataField="folTip"   HeaderStyle-Width="60px"  ItemStyle-Width="100px" ></telerik:GridBoundColumn>  
                        <telerik:GridBoundColumn HeaderText="Valor"  DataField="folValIntStr"   HeaderStyle-Width="40px"  ItemStyle-Width="40px" ></telerik:GridBoundColumn>   
                        <telerik:GridBoundColumn HeaderText="Formato"  DataField="folManForm"   HeaderStyle-Width="100px"  ItemStyle-Width="100px"   ></telerik:GridBoundColumn>  
                        <telerik:GridBoundColumn HeaderText="Prefijo"  DataField="formFolPrefijo"   HeaderStyle-Width="40px"  ItemStyle-Width="40px" ></telerik:GridBoundColumn>   
                        <telerik:GridBoundColumn HeaderText="Subfijo"  DataField="formFolSufijo"   HeaderStyle-Width="40px"  ItemStyle-Width="40px"   ></telerik:GridBoundColumn> 
                        <telerik:GridImageColumn DataType="System.String" DataImageUrlFields="FolSit"  HeaderStyle-Width="20px"  ItemStyle-Width="20px" 
                            DataAlternateTextField="FolSit" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                            ImageAlign="Middle" ImageHeight="11px" ImageWidth="11px" HeaderText="">


                        </telerik:GridImageColumn>
                                                   
                                                                     
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

            <telerik:RadGrid ID="rGdv_FolioManual"  Visible="false" OnSelectedIndexChanged="rGdv_FolioManual_SelectedIndexChanged"
                           runat="server" 
                           AllowMultiRowSelection="true"
                           AutoGenerateColumns="False" 
                           Width="782px" Height="190px"   
                           CssClass="Grid"  
                           Skin="Office2010Silver">

                <MasterTableView DataKeyNames="formFolCve"  AutoGenerateColumns="false" CssClass="GridTable"     >
                    <Columns> 
                        <telerik:GridBoundColumn HeaderText="Clave"  DataField="formFolCve"   HeaderStyle-Width="50px"  ItemStyle-Width="100px" ></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Descripción"  DataField="formFolDes"   HeaderStyle-Width="80px"  ItemStyle-Width="100px" ></telerik:GridBoundColumn>      
                        <telerik:GridBoundColumn HeaderText="Longitud"  DataField="formFolLon"   HeaderStyle-Width="20px"  ItemStyle-Width="20px" ></telerik:GridBoundColumn>  
                        <telerik:GridBoundColumn HeaderText="Alineación"  DataField="formFolPos"   HeaderStyle-Width="25px"  ItemStyle-Width="25px" ></telerik:GridBoundColumn>   
                        <telerik:GridBoundColumn HeaderText="Caracter"  DataField="formFolChar"   HeaderStyle-Width="20px"  ItemStyle-Width="20px" ></telerik:GridBoundColumn>
                        <telerik:GridImageColumn DataType="System.String" DataImageUrlFields="FolSit"  HeaderStyle-Width="15px"  ItemStyle-Width="15px" 
                            DataAlternateTextField="FolSit" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                            ImageAlign="Middle" ImageHeight="11px" ImageWidth="11px" HeaderText="">


                        </telerik:GridImageColumn>  
                                                                     
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
            <telerik:RadImageButton ID="rBtnGuardar"  runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text =""  OnClick="rBtnGuardar_Click"  OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
            <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text=""  OnClick="rBtnCancelar_Click"  OnClientClicking="OnClientClic_ConfirmCancel" ></telerik:RadImageButton>
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
