<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MttoClieDirEntrega.aspx.cs" Inherits="DC_MttoClieDirEntrega" %>
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
            <telerik:RadImageButton ID="rBtnNuevo"     runat ="server" Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text ="" OnClick="rBtnNuevo_Click" Visible="false" ></telerik:RadImageButton> <%--OnClick="rBtnNuevo_Click"--%>
            <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  ToolTip="Modificar"  Text="" OnClick="rBtnModificar_Click" Visible="false" ></telerik:RadImageButton> <%--OnClick="rBtnModificar_Click"--%>
            <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text="" OnClick="rBtnEliminar_Click" OnClientClicking="OnClientClic_ConfirmOK" Visible="false" ></telerik:RadImageButton> <%--OnClick="rBtnEliminar_Click"--%>
            <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"  Text=""  OnClick="rBtnLimpiar_Click"  ></telerik:RadImageButton> <%--OnClick="rBtnLimpiar_Click"--%>
        </asp:Panel>
        <fieldset   >
                <asp:Table ID="Table1" runat="server">
                    <asp:TableRow >
                        <asp:TableCell BackColor="Transparent" BorderColor="Black" Width="108px">
                            <telerik:RadLabel ID="RadLabel1" runat="server" Text="Cliente" Height="30px" Width="50px" BorderColor="Black" ></telerik:RadLabel>
                        </asp:TableCell>
                        <asp:TableCell BackColor="Transparent" BorderColor="Black">
                            <telerik:RadLabel ID="lbl_Clien" runat="server" Text=""  Height="30px" Width="120px" BorderColor="Black"></telerik:RadLabel>
                        </asp:TableCell>
                        
                        <asp:TableCell BackColor="Transparent">
                            <telerik:RadLabel ID="rLblSubClie" runat="server" Text="Subcliente" BorderColor="Black"  Width="108px"></telerik:RadLabel>
                        </asp:TableCell>
                        <asp:TableCell BackColor="Transparent">
                            <telerik:RadLabel ID="lbl_subclie" runat="server" Text="" BorderColor="Black" Width="140px" ></telerik:RadLabel>
                        </asp:TableCell>
                    </asp:TableRow>
                  </asp:Table>
                <asp:Table ID="Table2" runat="server">
                    <asp:TableRow>
                        <asp:TableCell BackColor="Transparent"  Width="108px">
                            <telerik:RadLabel ID="RadLabel5" runat="server" Text="Nombre" ></telerik:RadLabel>
                        </asp:TableCell>
                        <asp:TableCell BackColor="Transparent">
                        <telerik:RadLabel ID="lbl_Nombre" runat="server" Text="" BorderColor="Black" Width="400px" ></telerik:RadLabel>

                        </asp:TableCell>

                    </asp:TableRow>
                </asp:Table>
                <asp:Table ID="Table6" runat="server">
                    <asp:TableRow>
                        <asp:TableCell BackColor="Transparent" Width="108px" >
                            <telerik:RadLabel ID="RadLabel4" runat="server" Text="Entrega" ></telerik:RadLabel>
                        </asp:TableCell>
                        <asp:TableCell BackColor="Transparent">
                            <telerik:RadTextBox ID="txtbox_entrega" runat="server"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid"
                                 Width="170px" MaxLength="10" ></telerik:RadTextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>

      </fieldset>
        <fieldset  >
                <legend>Direccíon</legend>
                <asp:Table ID="Table3" runat="server">
                    <asp:TableRow>
                        <asp:TableCell>
                            <telerik:RadLabel ID="RadLabel6" runat="server" Text="País" ></telerik:RadLabel>
                        </asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadComboBox ID="cmbobox_pais" runat="server" Width="200px" OnSelectedIndexChanged="cmbobox_pais_SelectedIndexChanged" AutoPostBack="True"
                                HighlightTemplatedItems="true"  AllowCustomText="true"
                              DropDownCssClass="cssRadComboBox" 
                              DropDownWidth="290px" 
                               Height="200px"  >
                                    <HeaderTemplate>
                                        <table style="width: 260px" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 260px;">
                                                        Descripción
                                                    </td>
                                                </tr>
                                            </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                            <table style="width: 260px;"  cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 260px;">
                                                        <%# DataBinder.Eval(Container.DataItem, "paisDes") %>
                                                    </td>
                                  
                                                </tr>
                                            </table>

                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                    </FooterTemplate>


                            </telerik:RadComboBox>
                     
                        </asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadLabel ID="RadLabel7" runat="server" Text="Estado" ></telerik:RadLabel>
                        </asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadComboBox ID="cmbobox_estado" runat="server" Width="200px" OnSelectedIndexChanged="cmbobox_estado_SelectedIndexChanged"  AutoPostBack="true" 
                                HighlightTemplatedItems="true" AllowCustomText="true"
                              DropDownCssClass="cssRadComboBox" 
                              DropDownWidth="290px" 
                               Height="200px"  >

                                    <HeaderTemplate>
                                        <table style="width: 260px" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 260px;">
                                                        Descripción
                                                    </td>
                                                </tr>
                                            </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                            <table style="width: 260px;"  cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 260px;">
                                                        <%# DataBinder.Eval(Container.DataItem, "entFDes") %>
                                                    </td>
                                  
                                                </tr>
                                            </table>

                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                    </FooterTemplate>
                                 </telerik:RadComboBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow >
                        <asp:TableCell>
                             <telerik:RadLabel ID="RadLabel8" runat="server" Text="Del/Muni" ></telerik:RadLabel>
                        </asp:TableCell>
                        <asp:TableCell>
                                 <telerik:RadComboBox ID="cmbobox_poblacion" runat="server" 
                                     Width="200px"  AutoPostBack="false" AllowCustomText="true"
                                     HighlightTemplatedItems="true"
                               DropDownCssClass="cssRadComboBox" 
                              DropDownWidth="290px" 
                               Height="200px"  >

                                    <HeaderTemplate>
                                        <table style="width: 260px" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 260px;">
                                                        Descripción
                                                    </td>
                                                </tr>
                                            </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                            <table style="width: 260px;"  cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 260px;">
                                                        <%# DataBinder.Eval(Container.DataItem, "provDes") %>
                                                    </td>
                                  
                                                </tr>
                                            </table>

                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                    </FooterTemplate>

                                 </telerik:RadComboBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadLabel ID="RadLabel9" runat="server" Text="Colonia" ></telerik:RadLabel>
                        </asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txt_colonia" runat="server" Width="200px"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid"
                                 MaxLength="50" ></telerik:RadTextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    
                    <asp:TableRow  BackColor="Transparent">
                        <asp:TableCell>
                            <telerik:RadLabel ID="RadLabel10" runat="server" Text="Calle"  ></telerik:RadLabel>
                        </asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txtbox_calle" runat="server" Width="160px" MaxLength="50" 
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid"
                                ></telerik:RadTextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadLabel ID="RadLabel11" runat="server" Text="No Ext" Width="75px" ></telerik:RadLabel>
                        </asp:TableCell>
                        <asp:TableCell>
                            <%--<telerik:RadNumericTextBox ID="txtbox_NoExter" runat="server" Width="75px" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>--%>
                            <telerik:RadTextBox ID="txtbox_NoExter" runat="server" Width="75px" 
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid"
                                 MaxLength="30" ></telerik:RadTextBox>
                            <telerik:RadLabel ID="RadLabel12" runat="server" Text="No Int" ></telerik:RadLabel>
                            
                            <%--<telerik:RadNumericTextBox ID="txtbox_NoInter" runat="server" Width="75px"  NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>--%>
                            <telerik:RadTextBox ID="txtbox_NoInter" runat="server" Width="75px"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid"
                                 MaxLength="30" ></telerik:RadTextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <telerik:RadLabel ID="RadLabel13" runat="server" Text="Calles Aledañas"  ></telerik:RadLabel>
                        </asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txtbox_calleAled" runat="server" Width="160px" MaxLength="50"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid"
                                  ></telerik:RadTextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadLabel ID="RadLabel14" runat="server" Text="Cod.Postal" Width="75px" ></telerik:RadLabel>
                        </asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadNumericTextBox ID="txt_codPost" runat="server" Width="75px" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>

                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <asp:Table ID="Table4" runat="server">
                     <asp:TableRow>
                        <asp:TableCell>
                            <telerik:RadLabel ID="RadLabel15" runat="server" Text="Referencias" Width="104px" ></telerik:RadLabel>
                        </asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txt_ref" runat="server" Width="526px"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid"
                                 MaxLength="50" ></telerik:RadTextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>


                <asp:Table runat="server" ID="Table5">
                    <asp:TableRow BackColor="Transparent">
                        <asp:TableCell >
                            <telerik:RadLabel ID="RadLabel16" runat="server" Text="Teléfono 1" Width="104px" ></telerik:RadLabel>
                        </asp:TableCell>
                        <asp:TableCell>
                            <%--                         NumberFormat-DecimalDigits="0" NumberFormat-GroupSizes="8"--%>
                                <telerik:RadNumericTextBox
                                ID="txtbox_tel1" Width="200px"
                                LabelWidth="40%" runat="server" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="4" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>

                         </asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadLabel ID="RadLabel17" runat="server" Text="Teléfono 2" Width="75px" ></telerik:RadLabel>
                        </asp:TableCell>
                        <asp:TableCell>
 
                             <telerik:RadNumericTextBox 
                                 ID="txtbox_tel2" Width="200px"
                                  LabelWidth="40%" runat="server" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="4" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>
 

                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow Visible="false">
                        <asp:TableCell>
                            <telerik:RadLabel ID="RadLabel18" runat="server" Text="Fax" Width="75px" ></telerik:RadLabel>
                        </asp:TableCell>
                        <asp:TableCell>
 
                                <telerik:RadNumericTextBox
                                ID="txtbox_fax" Width="200px"
                                LabelWidth="40%" runat="server" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="4" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>
 
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>

            </fieldset>

  
        <div style="width:100%; margin-top:5px; display:table; position:static; background-color:transparent;" align="center" >  
               
                    <telerik:RadGrid  ID="rad_domEntre"
                         runat="server"
                        AutoGenerateColumns="False" 
                        OnSelectedIndexChanged="rad_domEntre_SelectedIndexChanged" 
                         Height="130px"
                         CssClass="Grid" 
                         Skin ="Office2010Silver"  >
                        <MasterTableView DataKeyNames="cliDirEntId" AutoGenerateColumns="false" CssClass="GridTable" >
                            <NoRecordsTemplate>No se encontraron registros</NoRecordsTemplate>
                            <Columns>
                                <telerik:GridBoundColumn HeaderText="Entrega" DataField="cliDirEntCve" HeaderStyle-Width="100px"  ItemStyle-Width="100px" ></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Pais" DataField="paisDes" HeaderStyle-Width="90px"  ItemStyle-Width="90px"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Estado" DataField="entFDes" HeaderStyle-Width="100px"  ItemStyle-Width="100px"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Población" DataField="provDes" HeaderStyle-Width="100px"  ItemStyle-Width="100px" ></telerik:GridBoundColumn>


                                <telerik:GridBoundColumn HeaderText="Colonia--Calle--No.Int--No.Ext" DataField="DomClie" Display="false"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="domCol" DataField="domCol" Display="false"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="domClle" DataField="domClle" Display="false"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="domCllsA" DataField="domCllsA" Display="false"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="domNInt" DataField="domNInt" Display="false"></telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn HeaderText="domNExt" DataField="domNExt" Display="false"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="domRef" DataField="domRef" Display="false"></telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn HeaderText="domCP" DataField="domCP" Display="false"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="domTel" DataField="domTel" Display="false"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="domTel2" DataField="domTel2" Display="false"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="domFax" DataField="domFax" Display="false"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="paisCve" DataField="paisCve" Display="false"></telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn HeaderText="entFCve" DataField="entFCve" Display="false"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="provCve" DataField="provCve" Display="false"></telerik:GridBoundColumn>
                              </Columns>
                        </MasterTableView>
                    <HeaderStyle CssClass="GridHeaderStyle"/>
                    <ItemStyle CssClass="GridRowStyle"/>
                    <AlternatingItemStyle  CssClass="GridAlternatingRowStyle" />
                    <selecteditemstyle CssClass="GridSelectedItem"/>
                    <FooterStyle CssClass="GridFooterStyle" />

                    <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true" >
                        <Selecting AllowRowSelect="true" EnableDragToSelectRows="False" ></Selecting>
                        <Scrolling AllowScroll="true" UseStaticHeaders="true"    ScrollHeight="130px"     />
                    </ClientSettings>
                    </telerik:RadGrid>
    
        </div>           

                <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server"></telerik:RadWindowManager>
            
        <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
                <telerik:RadImageButton ID="rBtnGuardar" Enabled="false"   runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text ="" OnClick="rBtnGuardar_Click" OnClientClicking="OnClientClic_ConfirmOK" ></telerik:RadImageButton> <%--OnClick="rBtnGuardar_Click" OnClientClicking="OnClientClic_ConfirmOK"--%>
                <telerik:RadImageButton ID="rBtnCancelar" Enabled="false"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text=""  OnClick="rBtnCancelar_Click" OnClientClicking="OnClientClic_ConfirmCancel" ></telerik:RadImageButton> <%--OnClick="rBtnCancelar_Click" OnClientClicking="OnClientClic_ConfirmCancel"--%>
        </asp:Panel>
        
    <asp:HiddenField ID="hdfBtnAccion" runat="server" />
            </ContentTemplate>
    </asp:UpdatePanel>
        <telerik:RadLabel runat="server" ID="numTab" Visible="false"></telerik:RadLabel>


    </div>
    </form>
</body>
</html>
