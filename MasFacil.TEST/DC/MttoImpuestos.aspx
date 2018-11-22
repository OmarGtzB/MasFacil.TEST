<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MttoImpuestos.aspx.cs" Inherits="DC_MttoImpuestos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                 <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server" Height="34px">
                    <telerik:RadImageButton ID="rBtnNuevo"     runat ="server" Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text ="" Visible="false"  OnClick="rBtnNuevo_Click" ></telerik:RadImageButton>
                    <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  ToolTip="Modificar"  Text="" Visible="false" OnClick="rBtnModificar_Click" ></telerik:RadImageButton>
                    <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text=""  Visible="false" OnClick="rBtnEliminar_Click" OnClientClicking="OnClientClic_ConfirmOK" ></telerik:RadImageButton>
                    <telerik:RadImageButton ID="rBtnDocumentos"  runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnDocumentoDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnDocumento.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnDocumentoHovered.png"  ToolTip="Documento"  Visible="false"  OnClick="rBtnDocumentosImp_Click" Text=""></telerik:RadImageButton>      
                    <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"   Text="" OnClick="rBtnLimpiar_Click"  ></telerik:RadImageButton>      
                </asp:Panel>
                     <fieldset  >  
           <div style="width:100%; display:table; position:static; background-color:transparent;" align="center" >

                    <table border="0" style=" width:640px; text-align:left; background-color:transparent ;">
                        <tr style="height:18px;">
                            <td style=" width:100px; background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel1" runat="server" Text="Clave"></telerik:RadLabel>  
                            </td>
                                <td style=" width:220px;  background-color:transparent;">                             
                                <telerik:RadTextBox ID="rtxtClave" runat="server" Width="200px"
                                                            EnabledStyle-CssClass="cssTxtEnabled"
                                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                                            HoveredStyle-CssClass="cssTxtHovered"
                                                            FocusedStyle-CssClass="cssTxtFocused"
                                                            InvalidStyle-CssClass="cssTxtInvalid"       
                                                            MaxLength="5"   Enabled="false" >
                                </telerik:RadTextBox> 
                            </td>

                            <td style=" width:100px; background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel3" runat="server" Text="Descripción"></telerik:RadLabel>  
                            </td>
                            <td style=" width:220px;  background-color:transparent;">                             
                                <telerik:RadTextBox ID="rTxtDescripcion" runat="server" Width="200px"
                                                            EnabledStyle-CssClass="cssTxtEnabled"
                                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                                            HoveredStyle-CssClass="cssTxtHovered"
                                                            FocusedStyle-CssClass="cssTxtFocused"
                                                            InvalidStyle-CssClass="cssTxtInvalid"  Enabled="false"                                   
                                ></telerik:RadTextBox> 
                            </td>
                        </tr>
                        <tr style="height:18px;">
                            <td style=" width:100px; background-color:transparent;">
                                   <telerik:RadLabel ID="RadLabel2" runat="server" Text="Abreviatura"></telerik:RadLabel>  
                            </td>
                            <td style=" width:220px;  background-color:transparent;">                             
                                <telerik:RadTextBox ID="rTxtAbre" runat="server" Width="200px" 
                                                            EnabledStyle-CssClass="cssTxtEnabled"
                                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                                            HoveredStyle-CssClass="cssTxtHovered"
                                                            FocusedStyle-CssClass="cssTxtFocused" Enabled="false"
                                                            InvalidStyle-CssClass="cssTxtInvalid"   MaxLength="8"
                                 ></telerik:RadTextBox> 
                            </td>
                            <td style=" width:100px; background-color:transparent;">
                                 <telerik:RadLabel ID="RadLabel4" runat="server" Text="Porcentaje"></telerik:RadLabel>  
                            </td>
                            <td style=" width:110px;  background-color:transparent;"> 
                                <telerik:RadNumericTextBox ID="rTxtPorcentaje" runat="server" Width="200px" 
                                    EnabledStyle-CssClass="cssTxtEnabled"
                                    DisabledStyle-CssClass ="cssTxtEnabled"
                                    HoveredStyle-CssClass="cssTxtHovered" Enabled="false"
                                    FocusedStyle-CssClass="cssTxtFocused"
                                    InvalidStyle-CssClass="cssTxtInvalid"></telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                        <td style=" width:100px; background-color:transparent;">
                                 <telerik:RadLabel ID="RadLabel5" runat="server" Text="Impuesto Sat"></telerik:RadLabel>  
                            </td>
                            <td>
                                 <telerik:RadComboBox ID="rCboSatImpuestos" runat="server" Width="200px"   AllowCustomText="true"
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
                                                                    <td style="width: 70px;">
                                                                        Descripción
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table style="width: 250px;"  cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width:80px" >
                                                                    <%# DataBinder.Eval(Container.DataItem, "satImpuCve")%>
                                                                </td>
                                                                <td style="width: 170px;">
                                                                    <%# DataBinder.Eval(Container.DataItem, "satImpuDes") %>
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
                              <div style="display:table; margin-top:5px; position:static; background-color:transparent;" align="center">
       <%--   <div style="width:100%; margin-top:5px; display:table; position:static; background-color:transparent;" align="center" > --%> 
 
                  
                    <telerik:RadGrid ID="rGdvImpuestos"  OnSelectedIndexChanged="rGdvImpuestos_SelectedIndexChanged"
                        runat="server"
                           AllowMultiRowSelection="true"
                           AutoGenerateColumns="False" 
                           Width="665px" Height="280px"   
                           CssClass="Grid" 
                           Skin="Office2010Silver">
                        <MasterTableView DataKeyNames="impCve"  
                                         AutoGenerateColumns="False"  
                                          CssClass="GridTable" >

                         <Columns >
                                <telerik:GridBoundColumn DataField="impCve"  HeaderText="Clave"  HeaderStyle-Width="60px" ItemStyle-Width="60px" />
                                <telerik:GridBoundColumn DataField="impDes" HeaderText="Descripción"  HeaderStyle-Width="230px"  ItemStyle-Width="230px" />
                                <telerik:GridBoundColumn DataField="impAbr"  HeaderText="Abreviatura" HeaderStyle-Width="100px" ItemStyle-Width="100px" />
                                <telerik:GridBoundColumn DataField="impTas" HeaderText="Porcentaje" HeaderStyle-Width="100px"  ItemStyle-Width="100px" /> 
                                <telerik:GridBoundColumn DataField="satImpuDes" HeaderText="CFDI" HeaderStyle-Width="50px"  ItemStyle-Width="50px" /> 
                                <telerik:GridBoundColumn DataField="satImpuCve" HeaderText="CFDI" HeaderStyle-Width="50px"  ItemStyle-Width="50px"  Display="false"  />                      
                      
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
                        <Scrolling AllowScroll="true" UseStaticHeaders="true"    ScrollHeight="225px"     />
                    </ClientSettings>
                </telerik:RadGrid>  
         </div>  
            <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
                <telerik:RadImageButton ID="rBtnGuardar"   runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text ="" OnClick="rBtnGuardar_Click"   OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text=""   OnClick="rBtnCancelar_Click" OnClientClicking="OnClientClic_ConfirmCancel"></telerik:RadImageButton>
            </asp:Panel>

            <asp:HiddenField ID="hdfRawUrl" runat="server" />
            <asp:HiddenField ID="hdfBtnAccion" runat="server" />  
            </div>
            </ContentTemplate>
               </asp:UpdatePanel>
            <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
    </telerik:RadWindowManager>

    </form>
</body>
</html>
