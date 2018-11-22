<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MttoDocumentoListaP.aspx.cs" Inherits="DC_MttoDescuentoListaP" %>

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
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
            <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server" Height="34px">
                <telerik:RadImageButton ID="rBtnNuevo"     runat ="server" Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text =""  OnClick="rBtnNuevo_Click" Visible="false" ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar" Text="" OnClick="rBtnEliminar_Click" OnClientClicking="OnClientClic_ConfirmOK" Visible="false" ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar" Text="" OnClick="rBtnLimpiar_Click"></telerik:RadImageButton>      
            </asp:Panel>
                 <fieldset> 
                               <div style="width:100%; display:table; position:static; background-color:transparent;" align="center" >
                                      <table border="0" style=" width:610px;  background-color:transparent ;">
                                          <tr style=" background-color:transparent;">
                                              <td style=" background-color:transparent;">
                                                    <telerik:RadLabel ID="RadLabel1" runat="server" Text="Documento"></telerik:RadLabel>  
                                               </td>
                                              <td>
                                                  <telerik:RadComboBox ID="rCboDocumento" runat="server"  Enabled="true"
                                                    AutoPostBack="true"
                                                     HighlightTemplatedItems="true"
                                                     DropDownCssClass="cssRadComboBox"  
                                                     Width="180px" DropDownWidth="300px" Height="200px">
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
                                                                        <%# DataBinder.Eval(Container.DataItem, "docCve")%>
                                                                    </td>
                                                                    <td style="width: 170px;">
                                                                        <%# DataBinder.Eval(Container.DataItem, "docDes") %>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                        </FooterTemplate>
                                                    </telerik:RadComboBox> 
                                              </td>
                                              <tb>
                                                   
                                              </tb>
                                              <td>
                                                  <telerik:RadLabel ID="RadLabel2" runat="server" Text="Lista de Precios" BackColor="transparent"></telerik:RadLabel>  
                                                   <telerik:RadComboBox ID="rCboListaPrecio" runat="server"  Enabled="true"
                                                            AutoPostBack="true"
                                                             HighlightTemplatedItems="true"
                                                             DropDownCssClass="cssRadComboBox"  
                                                             Width="180px" DropDownWidth="300px" Height="200px">
                                                                <HeaderTemplate>
                                                                    <table style="width: 250px" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <%--<td style="width: 80px;">
                                                                                Clave
                                                                            </td>--%>
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
                                                                                <%# DataBinder.Eval(Container.DataItem, "lisPreCve")%>
                                                                            </td>
                                                                            <td style="width: 170px;">
                                                                                <%# DataBinder.Eval(Container.DataItem, "lisPreDes") %>
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
                     <telerik:RadGrid ID="rGdvDocumento_ListaP"   OnSelectedIndexChanged="rGdvDocumento_ListaP_SelectedIndexChanged"
                        runat="server" 
                           AllowMultiRowSelection="false"
                           AutoGenerateColumns="False" 
                           Width="616px" Height="260px"   
                           CssClass="Grid" 
                           Skin="Office2010Silver" >
                         
                                     <MasterTableView  AutoGenerateColumns="False"   CssClass="GridTable" >
                                         <Columns>
                                            <telerik:GridBoundColumn DataField="docCve" HeaderText="DocumentoCve" HeaderStyle-Width="150px"  ItemStyle-Width="150px"  Display="false"/>
                                             <telerik:GridBoundColumn DataField="docDes" HeaderText="Documento" HeaderStyle-Width="150px"  ItemStyle-Width="150px"  />
                                                <telerik:GridBoundColumn DataField="lisPreCve" HeaderText="" HeaderStyle-Width="150px"  ItemStyle-Width="150px" Display="false" />
                                             <telerik:GridBoundColumn DataField="lisPreDes" HeaderText="Lista de Precios" HeaderStyle-Width="150px"  ItemStyle-Width="150px"  />
                                           
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
            </div>
            <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
                <telerik:RadImageButton ID="rBtnGuardar"   runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text =""   OnClick="rBtnGuardar_Click"  OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text=""  OnClick="rBtnCancelar_Click" OnClientClicking="OnClientClic_ConfirmCancel"></telerik:RadImageButton>
            </asp:Panel>
        <asp:HiddenField ID="hdfBtnAccion" runat="server" />
         </ContentTemplate>
        </asp:UpdatePanel>
        <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
    </telerik:RadWindowManager>
    </form>
</body>
</html>
