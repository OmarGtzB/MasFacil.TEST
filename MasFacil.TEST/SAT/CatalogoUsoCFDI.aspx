<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CatalogoUsoCFDI.aspx.cs" Inherits="SAT_CatalogoUsoCFDI" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

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
                    <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text=""   OnClientClicking="OnClientClic_ConfirmOK" Visible="false" OnClick="rBtnEliminar_Click"></telerik:RadImageButton>
                    <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"  Text="" OnClick="rBtnLimpiar_Click" ></telerik:RadImageButton>
                </asp:Panel>
                <div> 
                    <fieldset>
                    <legend>Datos</legend>
                        <table>
                            <tr>
                                <td><asp:Label ID="Label1" runat="server" Text="Clave" ></asp:Label></td>
                                <td><telerik:RadTextBox ID="rTxtCve"  runat="server" Enabled="false" MaxLength="3"
                                EnabledStyle-CssClass="cssTxtEnabled" Width="80px"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid"></telerik:RadTextBox></td>
                                <td><asp:Label ID="Label2" runat="server" Text="Descripción"></asp:Label></td>
                                <td><telerik:RadTextBox ID="rTxtDes"  runat="server"  Enabled="false" MaxLength="50"
                                EnabledStyle-CssClass="cssTxtEnabled" Width="290px"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid"></telerik:RadTextBox></td>
                            </tr>
                            <tr>
                            <td><asp:Label ID="Label3" runat="server" Text="Persona"></asp:Label></td>
                            <td colspan="3">
                                <asp:RadioButton ID="rbtnFisica" runat="server" GroupName="CFDI" Text="Fisica"  />
                                <asp:RadioButton ID="rbtnMoral" runat="server" GroupName="CFDI" Text="Moral"  />
                                <asp:RadioButton ID="rbtnAmbas" runat="server" GroupName="CFDI" Text="Ambas"  />
                            </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
                 <div style="width:100%; margin-top:5px; display:table; position:static; background-color:transparent;" align="center" > 
                    <telerik:RadGrid ID="rGdv_UsoCFDI"   OnSelectedIndexChanged="rGdv_CatMoneda_SelectedIndexChanged"
                            runat="server" 
                            AutoGenerateColumns="False"
                            Width="560px"   Height="270px"  
                            CssClass="Grid" 
                            Skin="Office2010Silver">
                                <MasterTableView DataKeyNames="satUsoCFDICve" AutoGenerateColumns="false" CssClass="GridTable">
                                    <Columns>
                                         <telerik:GridBoundColumn HeaderText="Clave" DataField="satUsoCFDICve" HeaderStyle-Width="50px" ></telerik:GridBoundColumn>
                                         <telerik:GridBoundColumn HeaderText="Descripción" DataField="satUsoCFDIDes" HeaderStyle-Width="200px" ></telerik:GridBoundColumn>
                                         <telerik:GridBoundColumn HeaderText="Tipo Persona" DataField="satUsoCFDITipVal" HeaderStyle-Width="50px"  ></telerik:GridBoundColumn>                             
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
                            <telerik:RadImageButton ID="rBtnGuardar" Enabled="false"   runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text =""  OnClientClicking="OnClientClic_ConfirmOK"  OnClick="rBtnGuardar_Click"  ></telerik:RadImageButton>
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
