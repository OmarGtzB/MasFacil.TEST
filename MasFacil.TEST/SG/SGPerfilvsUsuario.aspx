<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SGPerfilvsUsuario.aspx.cs" Inherits="SG_SGPerfilvsUsuario" %>

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
    <div>
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server" Height="34px">
                    <telerik:RadImageButton ID="rBtnNuevo"     runat ="server" Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text =""   Visible="false" ></telerik:RadImageButton>
                    <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  ToolTip="Modificar"  Text="" OnClick="rBtnModificar_Click" Visible="false" ></telerik:RadImageButton>
                    <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text=""  Visible="false"  ></telerik:RadImageButton>
                    <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"   Text=""  OnClick="rBtnLimpiar_Click"  ></telerik:RadImageButton>      
                </asp:Panel>
                <fieldset>
                    <legend>Relacionar por</legend>
                        <table>
                            <tr>
                                <td style="width:150px; background-color:transparent;">
                                    <asp:RadioButton ID="rbtnPerfil"  GroupName="relPerf"  Text="Perfil" runat="server" Checked="true" OnCheckedChanged="rbtnPerfil_CheckedChanged" AutoPostBack="true" />
                                    <asp:RadioButton ID="rbtnUsuario" GroupName="relPerf"  Text="Usuario" runat="server" OnCheckedChanged="rbtnUsuario_CheckedChanged"  AutoPostBack="true" />
                                 </td>
                                <td>
                                    <telerik:RadComboBox ID="rCboPerfil" Width="200px" DropDownCssClass="cssRadComboBox"  OnSelectedIndexChanged="rCboPerfil_SelectedIndexChanged" AutoPostBack="true"
                                      DropDownWidth="330px" Height="200px" HighlightTemplatedItems="true" runat="server">
                                            <HeaderTemplate>
                                                <table style="width: 300px" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td style="width: 70px;">
                                                            Clave
                                                        </td>
                                                        <td style="width: 230px;">
                                                            Descripción
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table style="width: 300px;"  cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td style="width:70px">
                                                            <%# DataBinder.Eval(Container.DataItem, "maPerfCve")%>
                                                        </td>
                                                        <td style="width: 230px;">
                                                            <%# DataBinder.Eval(Container.DataItem, "maPerfDes") %>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                            <asp:Literal runat="server" ID="RadComboItemsCount" />
                                            </FooterTemplate>
                                        </telerik:RadComboBox>

                                    <telerik:RadComboBox ID="rCboUsuario" Width="200px" DropDownCssClass="cssRadComboBox"  Visible="false" OnSelectedIndexChanged="rCboUsuario_SelectedIndexChanged" AutoPostBack="true"
                                       DropDownWidth="440px" Height="255px" HighlightTemplatedItems="true" runat="server">
                                            <HeaderTemplate>
                                                <table style="width: 400px" cellspacing="0" cellpadding="0">
                                                      <tr>    
                                                        <td style="width:30px; background-color:transparent;">
                                                            
                                                        </td>
                                                        <td style="width: 80px; background-color:transparent;">
                                                            Foto
                                                        </td>
                                                        <td style="width: 100px; background-color:transparent; ">
                                                            Usuario
                                                        </td>
                                                        <td style="width: 120px; background-color:transparent;">
                                                            Nombre
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table style="width: 400px;"  cellspacing="0" cellpadding="0">
                                                    <tr>
                                                     <td style="width: 140px;">
                                                         <telerik:RadBinaryImage runat="server" ID="RbiConPic1" DataValue='<%#Eval("maUsuFoto") is DBNull ? null : Eval("maUsuFoto")%>' AutoAdjustImageControlSize="false" Height="100px" Width="100px"  />

                                                        </td>
                                                        <td style="width:120px" >
                                                            <%# DataBinder.Eval(Container.DataItem, "maUsuCve")%>
                                                        </td>
                                                        <td style="width:140px" > 
                                                            <%# DataBinder.Eval(Container.DataItem, "maUsuNom")%>
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

                <%--RADGRID--%>
                <fieldset>
                      <div style="width:100%; display:table; margin-top:5px; position:static; background-color:transparent;" >
                      <%-- <telerik:RadGrid ID=""   runat="server"  Enabled="false" 
                           AllowMultiRowSelection="true" 
                           AutoGenerateColumns="False" 
                           Width="630px" Height="280px"  
                           CssClass="Grid" 
                           Skin="Office2010Silver">
                            <MasterTableView DataKeyNames="maUsuCve"  AutoGenerateColumns="False"  CssClass="GridTable"   >
                             <Columns >
                                     <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn1"    HeaderStyle-Width="35px" ItemStyle-Width="35px"  ></telerik:GridClientSelectColumn>
                                     <telerik:GridBinaryImageColumn DataField="maUsuFoto" ImageAlign="Middle"  ImageHeight="110px" ImageWidth="100px"   AutoAdjustImageControlSize="false" HeaderText="Foto"  HeaderStyle-Width="100" ItemStyle-Width="100"  ></telerik:GridBinaryImageColumn >
                                     <telerik:GridBoundColumn DataField="maUsuCve"  HeaderText="Usuario" HeaderStyle-Width="100px" ItemStyle-Width="100px" />
                                     <telerik:GridBoundColumn DataField="maUsuNom" HeaderText="Nombre"  HeaderStyle-Width="230px"  ItemStyle-Width="230px" />
                                     <telerik:GridBoundColumn HeaderText="Seg"  DataField="chkSeg"   HeaderStyle-Width="10px"  ItemStyle-Width="10px" Display="false" ></telerik:GridBoundColumn>
                            </Columns>
                            <NoRecordsTemplate>No se encontraron registros.</NoRecordsTemplate>    
                            </MasterTableView>
                            <HeaderStyle CssClass="GridHeaderStyle"/>
                            <ItemStyle CssClass="GridRowStyle"/>
                            <AlternatingItemStyle  CssClass="GridAlternatingRowStyle" />
                            <selecteditemstyle CssClass="GridSelectedItem"/>
                            <FooterStyle CssClass="GridFooterStyle" />
                            <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true" >
                                <Selecting AllowRowSelect="false" EnableDragToSelectRows="false" ></Selecting>
                                <Scrolling AllowScroll="true" UseStaticHeaders="true"    ScrollHeight="225px"     />
                            </ClientSettings>
                      </telerik:RadGrid>  --%>
                      
                      <telerik:RadGrid ID="rGdvUsuario" runat="server"  Enabled="false" 
                           AllowMultiRowSelection="true" 
                           AutoGenerateColumns="False" 
                           Width="630px" Height="280px"  
                           CssClass="Grid" 
                           Skin="Office2010Silver">
                             <MasterTableView DataKeyNames="maUsuCve"  AutoGenerateColumns="False"  CssClass="GridTable"   >
                             <Columns >
                                     <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn1"    HeaderStyle-Width="35px" ItemStyle-Width="35px"  ></telerik:GridClientSelectColumn>
                                     <telerik:GridBinaryImageColumn DataField="maUsuFoto" ImageAlign="Middle"  ImageHeight="110px" ImageWidth="100px"   AutoAdjustImageControlSize="false" HeaderText="Foto"  HeaderStyle-Width="100" ItemStyle-Width="100"  ></telerik:GridBinaryImageColumn >
                                     <telerik:GridBoundColumn DataField="maUsuCve"  HeaderText="Usuario" HeaderStyle-Width="100px" ItemStyle-Width="100px" />
                                     <telerik:GridBoundColumn DataField="maUsuNom" HeaderText="Nombre"  HeaderStyle-Width="230px"  ItemStyle-Width="230px" />
                                     <telerik:GridBoundColumn HeaderText="Seg"  DataField="chkSeg"   HeaderStyle-Width="10px"  ItemStyle-Width="10px" Display="false" ></telerik:GridBoundColumn>
                            </Columns>
                            <NoRecordsTemplate>No se encontraron registros.</NoRecordsTemplate>    
                            </MasterTableView>
                            <HeaderStyle CssClass="GridHeaderStyle"/>
                            <ItemStyle CssClass="GridRowStyle"/>
                            <AlternatingItemStyle  CssClass="GridAlternatingRowStyle" />
                            <selecteditemstyle CssClass="GridSelectedItem"/>
                            <FooterStyle CssClass="GridFooterStyle" />
                            <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true" >
                                <Selecting AllowRowSelect="true" EnableDragToSelectRows="false" ></Selecting>
                                <Scrolling AllowScroll="true" UseStaticHeaders="true"    ScrollHeight="225px"     />
                            </ClientSettings>
                      </telerik:RadGrid>   
                      </div>

                      <div style="width:100%; display:table; margin-top:5px; position:static; background-color:transparent;" >
                    <telerik:RadGrid ID="rGdvPerfil" Visible="false" runat="server"  Enabled="false" 
                           AllowMultiRowSelection="true" 
                           AutoGenerateColumns="False" 
                           Width="630px" Height="280px"  
                           CssClass="Grid" 
                           Skin="Office2010Silver">
                            <MasterTableView DataKeyNames="maPerfCve"  AutoGenerateColumns="False"  CssClass="GridTable" Enabled="false"  >
                             <Columns>
                                    <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn1"  HeaderStyle-Width="35px" ItemStyle-Width="35px"  ></telerik:GridClientSelectColumn>
                                    <telerik:GridBoundColumn DataField="maPerfCve"  HeaderText="Clave" HeaderStyle-Width="100px" ItemStyle-Width="100px" />
                                    <telerik:GridBoundColumn DataField="maPerfDes"  HeaderText="Descripción" HeaderStyle-Width="100px" ItemStyle-Width="100px" />
                                    <telerik:GridBoundColumn HeaderText="Seg"  DataField="chkSeg"   HeaderStyle-Width="220px"  ItemStyle-Width="140px" Display="false" ></telerik:GridBoundColumn>
                            </Columns>
                            <NoRecordsTemplate>No se encontraron registros.</NoRecordsTemplate>    
                            </MasterTableView>
                            <HeaderStyle CssClass="GridHeaderStyle"/>
                            <ItemStyle CssClass="GridRowStyle"/>
                            <AlternatingItemStyle  CssClass="GridAlternatingRowStyle" />
                            <selecteditemstyle CssClass="GridSelectedItem"/>
                            <FooterStyle CssClass="GridFooterStyle" />
                            <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true" >
                                <Selecting AllowRowSelect="true" EnableDragToSelectRows="false" ></Selecting>
                                <Scrolling AllowScroll="true" UseStaticHeaders="true"    ScrollHeight="225px"     />
                            </ClientSettings>
                      </telerik:RadGrid>  
                    </div>     
                 </fieldset>

              <%--Botones--%>
              <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
                <telerik:RadImageButton ID="rBtnGuardar"   runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"    Text=""   OnClick="rBtnGuardar_Click"   OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text=""   OnClick="rBtnCancelar_Click" OnClientClicking="OnClientClic_ConfirmCancel"></telerik:RadImageButton>
            </asp:Panel>
            <asp:HiddenField ID="hdfBtnAccion" runat="server" />  
            </div>
        </ContentTemplate>
        </asp:UpdatePanel>
        <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
    </telerik:RadWindowManager>
    </div>
    </form>
</body>
</html>
