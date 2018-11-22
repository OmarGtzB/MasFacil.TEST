<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SGUsuario.aspx.cs" Inherits="SG_SGUsuario" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
     <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
     <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
     <script src="../Scripts/JSGeneral.js" type="text/javascript"></script>
     <link href="/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />
     <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />

       <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript">  

                 function fileUploaded(sender, args) {
                    $find('<%= RAJAXMAN1.ClientID %>').ajaxRequest();
                    $telerik.$(".invalid").html("");
                    setTimeout(function () {
                        sender.deleteFileInputAt(0);
                    }, 10);
                 }

                function GetRadWindow() {
                    var oWindow = null;
                    if (window.radWindow) oWindow = window.radWindow;
                    else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                    return oWindow;
                }

                function Close() {
                    GetRadWindow().close();
                }
            </script> 
        </telerik:RadCodeBlock>
       <style type="text/css">
            .rcbHeader ul,
            .rcbFooter ul,
            .rcbItem ul,
            .rcbHovered ul,
            .rcbDisabled ul {
                display: inline-block;
                margin: 0;
                padding: 0;
                list-style-type: none;
                vertical-align: middle;
            }
            .col1, .col2, .col3 {
                width: 50px;
                display: inline-block;
            }
 
            html .RadComboBoxDropDown .rcbItem > label,
            html .RadComboBoxDropDown .rcbHovered > label,
            html .RadComboBoxDropDown .rcbDisabled > label,
            html .RadComboBoxDropDown .rcbLoading > label,
            html .RadComboBoxDropDown .rcbCheckAllItems > label,
            html .RadComboBoxDropDown .rcbCheckAllItemsHovered > label {
                display: inline-block;
            }
    </style>
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

     <telerik:RadAjaxManager ID="RAJAXMAN1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"  >
                <AjaxSettings>


                    <%--<telerik:AjaxSetting AjaxControlID="RAJAXMAN1">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID = "imgPerfil"/>
                            <telerik:AjaxUpdatedControl ControlID = "arregloImagen"/> 
                        </UpdatedControls>
                    </telerik:AjaxSetting>

                    <telerik:AjaxSetting AjaxControlID="RadAsyncUpload1">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID = "imgPerfil"/>
                        </UpdatedControls>
                    </telerik:AjaxSetting>

                     <telerik:AjaxSetting AjaxControlID="rBtnGuardar">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID = "rGdvUsuario"/>
                            <telerik:AjaxUpdatedControl ControlID = "imgPerfil"/>
                            <telerik:AjaxUpdatedControl ControlID = "rTxtClave"/>
                            <telerik:AjaxUpdatedControl ControlID = "rTxtNombre"/>
                            <telerik:AjaxUpdatedControl ControlID = "rTxtContrasenia1"/>
                            <telerik:AjaxUpdatedControl ControlID = "rTxtContrasenia2"/>
                            <telerik:AjaxUpdatedControl ControlID = "CheckStatus"/>
                            <telerik:AjaxUpdatedControl ControlID = "rCboPerfil"/>
                            <telerik:AjaxUpdatedControl ControlID = "RadAsyncUpload1"/>
                            <telerik:AjaxUpdatedControl ControlID = "rBtnModificar"/>
                        </UpdatedControls>
                    </telerik:AjaxSetting>

                    <telerik:AjaxSetting AjaxControlID="rBtnCancelar">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID = "rGdvUsuario"/>
                            <telerik:AjaxUpdatedControl ControlID = "imgPerfil"/>
                            <telerik:AjaxUpdatedControl ControlID = "rTxtClave"/>
                            <telerik:AjaxUpdatedControl ControlID = "rTxtNombre"/>
                            <telerik:AjaxUpdatedControl ControlID = "rTxtContrasenia1"/>
                            <telerik:AjaxUpdatedControl ControlID = "rTxtContrasenia2"/>
                            <telerik:AjaxUpdatedControl ControlID = "CheckStatus"/>
                            <telerik:AjaxUpdatedControl ControlID = "rCboPerfil"/>
                            <telerik:AjaxUpdatedControl ControlID = "RadAsyncUpload1"/>
                            <telerik:AjaxUpdatedControl ControlID = "pnlBtnsAcciones"/>
                        </UpdatedControls>
                    </telerik:AjaxSetting>

                    <telerik:AjaxSetting AjaxControlID="rGdvUsuario">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID = "imgPerfil"/>
                            <telerik:AjaxUpdatedControl ControlID = "rTxtClave"/>
                            <telerik:AjaxUpdatedControl ControlID = "rTxtNombre"/>
                            <telerik:AjaxUpdatedControl ControlID = "rTxtContrasenia1"/>
                            <telerik:AjaxUpdatedControl ControlID = "rTxtContrasenia2"/>
                            <telerik:AjaxUpdatedControl ControlID = "CheckStatus"/>
                            <telerik:AjaxUpdatedControl ControlID = "rCboPerfil"/> 
                            <telerik:AjaxUpdatedControl ControlID = "hdfBtnPSW"/> 
                        </UpdatedControls>
                    </telerik:AjaxSetting>


                    <telerik:AjaxSetting AjaxControlID="rBtnModificar">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID = "imgPerfil"/>
                            <telerik:AjaxUpdatedControl ControlID = "rTxtClave"/>
                            <telerik:AjaxUpdatedControl ControlID = "rTxtNombre"/>
                            <telerik:AjaxUpdatedControl ControlID = "rTxtContrasenia1"/>
                            <telerik:AjaxUpdatedControl ControlID = "rTxtContrasenia2"/>
                            <telerik:AjaxUpdatedControl ControlID = "CheckStatus"/>
                            <telerik:AjaxUpdatedControl ControlID = "rCboPerfil"/> 
                            <telerik:AjaxUpdatedControl ControlID = "hdfBtnPSW"/> 
                            <telerik:AjaxUpdatedControl ControlID = "rCboPerfil"/> 
                            <telerik:AjaxUpdatedControl ControlID = "hdfBtnPSW"/>   
                            <telerik:AjaxUpdatedControl ControlID = "Panel1"/>
                            <telerik:AjaxUpdatedControl ControlID = "RadAsyncUpload1"/>
                        </UpdatedControls>
                    </telerik:AjaxSetting>

                    <telerik:AjaxSetting AjaxControlID="rBtnNuevo">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID = "imgPerfil"/>
                            <telerik:AjaxUpdatedControl ControlID = "rTxtClave"/>
                            <telerik:AjaxUpdatedControl ControlID = "rTxtNombre"/>
                            <telerik:AjaxUpdatedControl ControlID = "rTxtContrasenia1"/>
                            <telerik:AjaxUpdatedControl ControlID = "rTxtContrasenia2"/>
                            <telerik:AjaxUpdatedControl ControlID = "CheckStatus"/>
                            <telerik:AjaxUpdatedControl ControlID = "rCboPerfil"/> 
                            <telerik:AjaxUpdatedControl ControlID = "hdfBtnPSW"/> 
                            <telerik:AjaxUpdatedControl ControlID = "rCboPerfil"/> 
                            <telerik:AjaxUpdatedControl ControlID = "hdfBtnPSW"/>   
                            <telerik:AjaxUpdatedControl ControlID = "Panel1"/>
                            <telerik:AjaxUpdatedControl ControlID = "RadAsyncUpload1"/>
                        </UpdatedControls>
                    </telerik:AjaxSetting>--%>

                 </AjaxSettings>
        </telerik:RadAjaxManager>

          <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Silk"></telerik:RadAjaxLoadingPanel>
          <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" BackColor="White" Transparency="30"></telerik:RadAjaxLoadingPanel>
          <asp:UpdatePanel runat="server" ID="UpdatePanel">
          <ContentTemplate>

         

<asp:Panel runat="server" ID="pnlGral" >



    <div>
        <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server" >
            <telerik:RadImageButton ID="rBtnNuevo"     runat ="server" Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text =""   OnClick="rBtnNuevo_Click" Visible="false"  ></telerik:RadImageButton>
            <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  ToolTip="Modificar"  Text=""   OnClick="rBtnModificar_Click" Visible="false" ></telerik:RadImageButton>
            <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text="" OnClick="rBtnEliminar_Click"   OnClientClicking="OnClientClic_ConfirmOK" Visible="false" ></telerik:RadImageButton>
            <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"  Text=""  OnClick="rBtnLimpiar_Click" ></telerik:RadImageButton>
        </asp:Panel>

         <fieldset>
            <legend>Datos Usuario</legend>
            <table >
            <tr>
            <td >
                <telerik:RadLabel ID="RadLabel1" runat="server" Text="Usuario"></telerik:RadLabel>
            </td>
            <td>
                 <telerik:RadTextBox  ID="rTxtClave" runat="server" MaxLength="20"
                        EnabledStyle-CssClass="cssTxtEnabled"
                        DisabledStyle-CssClass ="cssTxtEnabled"
                        HoveredStyle-CssClass="cssTxtHovered"
                        FocusedStyle-CssClass="cssTxtFocused"
                        InvalidStyle-CssClass="cssTxtInvalid" 
                    Width="180px" Enabled="false"></telerik:RadTextBox>
            </td>
            <td >
                <telerik:RadLabel ID="RadLabel8" runat="server" Text=""></telerik:RadLabel>
            </td>
             <td >
                <telerik:RadLabel ID="RadLabel9" runat="server" Text=""></telerik:RadLabel>
            </td>
            <td >
                <telerik:RadLabel ID="RadLabel10" runat="server" Text=""></telerik:RadLabel>
            </td>
             <td >
                <telerik:RadLabel ID="RadLabel11" runat="server" Text=""></telerik:RadLabel>
            </td>
            <td >
                <telerik:RadLabel ID="RadLabel12" runat="server" Text=""></telerik:RadLabel>
            </td>
             <td >
                <telerik:RadLabel ID="RadLabel13" runat="server" Text=""></telerik:RadLabel>
            </td>
            <td >
                <telerik:RadLabel ID="RadLabel14" runat="server" Text=""></telerik:RadLabel>
            </td>
             <td >
                 <telerik:RadLabel ID="RadLabel15" runat="server"></telerik:RadLabel>
            </td>
              <td >
                <telerik:RadLabel ID="RadLabel16" runat="server" Text=""></telerik:RadLabel>
            </td>
             <td >
                <telerik:RadLabel ID="RadLabel17" runat="server" Text=""></telerik:RadLabel>
            </td>
            <td rowspan="7">
                 <telerik:RadBinaryImage ID="imgPerfil" runat="server" Height="140px" Width="120px" ImageAlign="Left"  AutoAdjustImageControlSize="false" BorderColor="Black" BorderWidth="1px"  /> 
                 <telerik:RadAsyncUpload  ID="RadAsyncUpload1" InputSize="50" AllowedFileExtension="jpg,jpeg,png,gif"  runat="server" HideFileInput="true"  
                 MaxFileInputsCount="1"  Width="10px"  Enabled="true" OnFileUploaded="RadAsyncUpload1_FileUploaded"  OnClientFilesUploaded="fileUploaded"  ></telerik:RadAsyncUpload>
             </td>
            </tr>
            <tr>
             <td>
                <telerik:RadLabel ID="RadLabel2" runat="server" Text="Nombre"></telerik:RadLabel>
            </td>
            <td>
                <telerik:RadTextBox ID="rTxtNombre" Width="180px" runat="server" MaxLength="40"
                    EnabledStyle-CssClass="cssTxtEnabled"  Enabled="false"
                    DisabledStyle-CssClass ="cssTxtEnabled"
                    HoveredStyle-CssClass="cssTxtHovered"
                    FocusedStyle-CssClass="cssTxtFocused"
                    InvalidStyle-CssClass="cssTxtInvalid" 
                    ></telerik:RadTextBox>
            </td>
            </tr>
            <tr>
            <td>
                <telerik:RadLabel ID="RadLabel3" runat="server" Text="Contraseña"></telerik:RadLabel>
            </td>
                <td>
                    <telerik:RadTextBox ID="rTxtContrasenia1" Width="180px" runat="server" MaxLength="15"  TextMode="Password"
                    EnabledStyle-CssClass="cssTxtEnabled" Enabled="false"
                    DisabledStyle-CssClass ="cssTxtEnabled"
                    HoveredStyle-CssClass="cssTxtHovered"
                    FocusedStyle-CssClass="cssTxtFocused"
                    InvalidStyle-CssClass="cssTxtInvalid" 
                    ></telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td>
                <telerik:RadLabel ID="RadLabel4" runat="server" Text="Rep. Contraseña"></telerik:RadLabel>
            </td>
                <td>
                    <telerik:RadTextBox ID="rTxtContrasenia2" Width="180px" runat="server" MaxLength="15" TextMode="Password"
                    EnabledStyle-CssClass="cssTxtEnabled" Enabled="false"
                    DisabledStyle-CssClass ="cssTxtEnabled"
                    HoveredStyle-CssClass="cssTxtHovered"
                    FocusedStyle-CssClass="cssTxtFocused"
                    InvalidStyle-CssClass="cssTxtInvalid" 
                    ></telerik:RadTextBox>
                </td>
            </tr>
            <tr>
            <td>
                 <telerik:RadLabel ID="RadLabel5" runat="server" Text="Activo"></telerik:RadLabel>
            </td>
            <td>
                <asp:CheckBox ID="CheckStatus" runat="server" Enabled="false" />
            </td>
            </tr>
            <tr>
            <td>
                <telerik:RadLabel ID="RadLabel6" runat="server" Text="Perfiles"></telerik:RadLabel>
            </td>
            <td>
             <telerik:RadComboBox ID="rCboPerfil" Width="180px" DropDownCssClass="cssRadComboBox"  AutoPostBack="true" Enabled="false" CheckBoxes="true"
                    DropDownWidth="400px" Height="200px" HighlightTemplatedItems="true" runat="server">
                        <HeaderTemplate>
                            <table style="width: 500px" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width: 90px; vertical-align:middle"; ">
                                        Clave
                                    </td>
                                    <td style="width: 210px; vertical-align:middle";  ">
                                        Descripción
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <ul>
                                <li  class="col1" style="width:100px;" ><%# DataBinder.Eval(Container.DataItem, "maPerfCve")%></li>
                                <li class="col2" style="width:210px;" ><%# DataBinder.Eval(Container.DataItem, "maPerfDes")%></li>
                            </ul>
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                        </FooterTemplate>
                    </telerik:RadComboBox>
            </td>
            </tr>
        </table>
      </fieldset>
       <div style="width:100%; display:table; margin-top:5px; position:static; background-color:transparent;" >
                        <telerik:RadGrid ID="rGdvUsuario" runat="server"   OnSelectedIndexChanged="rGdvUsuario_SelectedIndexChanged"
                                           AllowMultiRowSelection="true" 
                                           AutoGenerateColumns="False" 
                                           Width="608px" Height="280px"  
                                           CssClass="Grid" 
                                           Skin="Office2010Silver">
                             <MasterTableView DataKeyNames="maUsuCve"  AutoGenerateColumns="False"  CssClass="GridTable"   >
                             <Columns >
                                     <telerik:GridBinaryImageColumn DataField="maUsuFoto" ImageAlign="Middle"  ImageHeight="110px" ImageWidth="100px"   AutoAdjustImageControlSize="false" HeaderText="Foto"  HeaderStyle-Width="50" ItemStyle-Width="50"  ></telerik:GridBinaryImageColumn >
                                     <telerik:GridBoundColumn DataField="maUsuCve"  HeaderText="Usuario" HeaderStyle-Width="60px" ItemStyle-Width="60px" />
                                     <telerik:GridBoundColumn DataField="maUsuNom" HeaderText="Nombre"  HeaderStyle-Width="80px"  ItemStyle-Width="80px" />
                                     <telerik:GridBoundColumn DataField="Estatus" HeaderText="Estatus" HeaderStyle-Width="40px"  ItemStyle-Width="40px"  ></telerik:GridBoundColumn>
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
        <asp:Panel ID="Panel1" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server" >

            <telerik:RadImageButton  ID="rBtnGuardar"  runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text =""    Enabled="true"   OnClick="rBtnGuardar_Click"  OnClientClicking="OnClientClic_ConfirmOK" ></telerik:RadImageButton>
            <telerik:RadImageButton  ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text=""    Enabled="true"  OnClick="rBtnCancelar_Click"  OnClientClicking="OnClientClic_ConfirmCancel" ></telerik:RadImageButton>
        </asp:Panel>
        <asp:HiddenField ID="arregloImagen"   runat="server" />
        <asp:HiddenField ID="hdfBtnAccion" runat="server" />
        <asp:HiddenField ID="hdfBtnPSW" runat="server" />
        <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server"></telerik:RadWindowManager>
    </div>
</asp:Panel>
 </ContentTemplate>
          </asp:UpdatePanel>
    </form>
</body>
</html>
