<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CatalogoImpuesto.aspx.cs" Inherits="SAT_CatalogoMetodoPago" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title></title>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
     <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
     <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
     <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
     <script src="../Scripts/JSGeneral.js" type="text/javascript"></script>
    <link href="/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />
</head>
<body>
          <form id="form1" runat="server">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
      
             <asp:Panel ID="Panel1" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
          
                <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
                    <telerik:RadImageButton ID="rBtnNuevo"     runat ="server" Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text ="" OnClick="rBtnNuevo_Click" Visible="false"  ></telerik:RadImageButton>
                    <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  ToolTip="Modificar"  Text=""  OnClick="rBtnModificar_Click" Visible="false" ></telerik:RadImageButton>
                    <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text=""  OnClick="rBtnEliminar_Click"  OnClientClicking="OnClientClic_ConfirmOK" Visible="false" ></telerik:RadImageButton>
                    <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"  Text="" OnClick="rBtnLimpiar_Click"  ></telerik:RadImageButton>
                </asp:Panel>
                <div> 
                    <fieldset>
                    <legend>Datos</legend>
                        <table>
                            <tr>
                                <td><asp:Label ID="Label1" runat="server" Text="Clave" ></asp:Label></td>
                                <td><telerik:RadTextBox ID="rTxtCve"  runat="server" Enabled="false" MaxLength="3" Width="45px"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid"></telerik:RadTextBox></td>
                                <td><asp:Label ID="Label2" runat="server" Text="Descripción"></asp:Label></td>
                                <td><telerik:RadTextBox ID="rTxtDes"  runat="server"  Enabled="false" MaxLength="50" Width="265px"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid"></telerik:RadTextBox></td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
                 <div style="width:100%; margin-top:5px; display:table; position:static; background-color:transparent;" align="center" > 
                    <telerik:RadGrid ID="rGdv_Impuesto" OnSelectedIndexChanged="rGdv_Impuesto_SelectedIndexChanged"
                            runat="server" 
                            AutoGenerateColumns="False"
                            Width="463px"   Height="280px"  
                            CssClass="Grid" 
                            Skin="Office2010Silver">
                                <MasterTableView DataKeyNames="satImpuCve" AutoGenerateColumns="false" CssClass="GridTable">
                                    <Columns>
                                        <telerik:GridBoundColumn HeaderText="Clave" DataField="satImpuCve" HeaderStyle-Width="55px" ></telerik:GridBoundColumn>
                                         <telerik:GridBoundColumn HeaderText="Descripción" DataField="satImpuDes" HeaderStyle-Width="200px" ></telerik:GridBoundColumn>
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
                            <telerik:RadImageButton ID="rBtnGuardar" Enabled="false"   runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text =""  OnClientClicking="OnClientClic_ConfirmOK"   OnClick="rBtnGuardar_Click"  ></telerik:RadImageButton>
                            <telerik:RadImageButton ID="rBtnCancelar" Enabled="false"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text="" OnClientClicking="OnClientClic_ConfirmCancel"  OnClick="rBtnCancelar_Click" ></telerik:RadImageButton>
                    </asp:Panel>
           </asp:Panel>
          <asp:HiddenField ID="hdfBtnAccion" runat="server" />
        <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server"></telerik:RadWindowManager>
    </ContentTemplate>
    </asp:UpdatePanel>
        </form>
</body>
</html>
