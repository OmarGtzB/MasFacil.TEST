<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MttoFormasPagoEsp.aspx.cs" Inherits="DC_MttoFormasPagoEsp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
    <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
     <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
    <script src="../Scripts/JSGeneral.js" type="text/javascript"></script>
     <link href="/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
         <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>



          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                
                <div>
                    <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
                        <telerik:RadImageButton ID="rBtnNuevo"     runat ="server" Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text ="" visible="false" OnClick="rBtnNuevo_Click"  ></telerik:RadImageButton>
                        <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  ToolTip="Modificar" Text=""  visible="false" OnClick="rBtnModificar_Click"  ></telerik:RadImageButton>
                        <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text="" visible="false" OnClick="rBtnEliminar_Click" OnClientClicking="OnClientClic_ConfirmOK" ></telerik:RadImageButton>
                        <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"   Text="" OnClick="rBtnLimpiar_Click" ></telerik:RadImageButton>    
                    </asp:Panel>
                     <fieldset style="  margin-top:5px;   display: block; text-align:left;" >
                                 
                         <asp:Table runat="server">
                             <asp:TableRow>

                                 <asp:TableCell>
                                        <telerik:RadLabel ID="RadLabel1" runat="server" Text="Clave" Width="110px"></telerik:RadLabel>
                                        <telerik:RadTextBox ID="txt_clave" runat="server" Width="150px" Enabled="false" MaxLength="10"
                                            EnabledStyle-CssClass="cssTxtEnabled"
                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                            HoveredStyle-CssClass="cssTxtHovered"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid"
                                            ></telerik:RadTextBox>&nbsp;
                                        <telerik:RadLabel ID="RadLabel2" runat="server" Text="Abreviatura" Width="100px"></telerik:RadLabel>
                                        <telerik:RadTextBox ID="txt_abreviatura" runat="server" Width="150px" Enabled="false" MaxLength="20"
                                            EnabledStyle-CssClass="cssTxtEnabled"
                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                            HoveredStyle-CssClass="cssTxtHovered"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid"
                                            ></telerik:RadTextBox><br />

                                        <telerik:RadLabel ID="RadLabel4" runat="server" Text="Descripción" Width="110px"></telerik:RadLabel>
                                        <telerik:RadTextBox ID="txt_descripcion" EnabledStyle-CssClass="cssTxtEnabled" Enabled="false" MaxLength="50"
                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                            HoveredStyle-CssClass="cssTxtHovered"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid"
                                            runat="server" Width="413px"></telerik:RadTextBox><br />
                                   
                                   
                                </asp:TableCell>

                             </asp:TableRow>
                         </asp:Table>
                         </fieldset>
                        <div style="width:100%; display:table; margin-top:5px; position:static; background-color:transparent;" align="center">
                           <telerik:RadGrid ID="rGdv_FormasPagoEspecial" 
                            runat="server" OnSelectedIndexChanged="rGdv_FormasPagoEspecial_SelectedIndexChanged"
                            AllowMultiRowSelection="true"
                            AutoGenerateColumns="False" 
                            Width="555px" Height="220px"   
                            CssClass="Grid" 
                            Skin="Office2010Silver">

                                        <MasterTableView DataKeyNames="forPagEspCve" AutoGenerateColumns="False" CssClass="GridTable" >

                                               <Columns >
                                                        <telerik:GridBoundColumn DataField="forPagEspCve" HeaderText="Clave" HeaderStyle-Width="40px"  ItemStyle-Width="40px" />  
                                                        <telerik:GridBoundColumn DataField="forPagEspAbr" HeaderText="Abreviatura" HeaderStyle-Width="55px"  ItemStyle-Width="55px"  /> 
                                                        <telerik:GridBoundColumn DataField="forPagEspDes" HeaderText="Descripción" HeaderStyle-Width="100px"  ItemStyle-Width="100px"  />         
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
                                                <Scrolling AllowScroll="true" UseStaticHeaders="true"    ScrollHeight="225px"/>
                                            </ClientSettings>
                            </telerik:RadGrid>
                            </div>  


                         
                     <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
                        <telerik:RadImageButton ID="rBtnGuardar"   runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text =""  OnClick="rBtnGuardar_Click"  OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
                        <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text=""  OnClick="rBtnCancelar_Click" OnClientClicking="OnClientClic_ConfirmCancel"></telerik:RadImageButton>
                    </asp:Panel>
                    <asp:HiddenField ID="hdfBtnAccion" runat="server" />


                </div>
                </ContentTemplate>
              </asp:UpdatePanel>
            <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
    </telerik:RadWindowManager>
    </form>
</body>
</html>
