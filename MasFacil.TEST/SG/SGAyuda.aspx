<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SGAyuda.aspx.cs" Inherits="SG_SGAyuda" %>
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

          <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript">  
                 function fileUploaded(sender, args) {
                    $find('<%= RAJAXMAN1.ClientID %>').ajaxRequest();
                    $telerik.$(".invalid").html("");
                    setTimeout(function () {
                        sender.deleteFileInputAt(0);
                    }, 10);
                }
            </script> 
        </telerik:RadCodeBlock>

</head>
<body>
 
        <form id="form1" runat="server">
                 <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
                      <telerik:RadAjaxManager ID="RAJAXMAN1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1" >
                        <AjaxSettings>
                            <telerik:AjaxSetting AjaxControlID="RAJAXMAN1">
                                <UpdatedControls>
                                    <telerik:AjaxUpdatedControl ControlID = "RadBinaryImage1" />
                                </UpdatedControls>
                            </telerik:AjaxSetting>

                            <telerik:AjaxSetting AjaxControlID="RadAsyncUpload1">
                                <UpdatedControls>
                                    <telerik:AjaxUpdatedControl ControlID = "RadBinaryImage1" />
                                </UpdatedControls>
                            </telerik:AjaxSetting>

                        <telerik:AjaxSetting AjaxControlID="rBtnGuardar">
                                <UpdatedControls>
                                    <telerik:AjaxUpdatedControl ControlID = "Panel1" />
                                </UpdatedControls>
                            </telerik:AjaxSetting>

                             <telerik:AjaxSetting AjaxControlID="rBtnCancelar">
                                <UpdatedControls>
                                    <telerik:AjaxUpdatedControl ControlID = "Panel1" />
                                </UpdatedControls>
                            </telerik:AjaxSetting>
                         </AjaxSettings>
                </telerik:RadAjaxManager>

  <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Silk"></telerik:RadAjaxLoadingPanel>

  <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" BackColor="White" Transparency="30"></telerik:RadAjaxLoadingPanel>
        <asp:Panel ID="Panel1" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
          
                <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
                    <telerik:RadImageButton ID="rBtnNuevo"     runat ="server" Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text =""  OnClick="rBtnNuevo_Click" Visible="false" ></telerik:RadImageButton>
                    <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  ToolTip="Modificar"  Text="" OnClick="rBtnModificar_Click" Visible="false" ></telerik:RadImageButton>
                    <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text=""  OnClick="rBtnEliminar_Click" OnClientClicking="OnClientClic_ConfirmOK" Visible="false"  ></telerik:RadImageButton>
                    <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"  Text="" OnClick="rBtnLimpiar_Click"  ></telerik:RadImageButton>
                </asp:Panel>
                <table>
                <tr>
                    <td>
                    <fieldset style=" height:205px; width:365px;  margin-top:0px;   display: block; text-align:left; align-items:center; background-color:transparent; " >
                <legend>Datos</legend>
                <table style="background-color:transparent; width:50%;">
                    <tr style="height:18px;">
                        <td style=" width:90px; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel2" runat="server" Text="Tipo"></telerik:RadLabel>  
                        </td>
                        <td style=" width:200px;  background-color:transparent;">    
                                    <telerik:RadComboBox ID="rCboTipo" runat="server" Width="170px" Enabled="false"
                                    HighlightTemplatedItems="true" OnSelectedIndexChanged="rCboTipo_SelectedIndexChanged"
                                    DropDownCssClass="cssRadComboBox"  
                                    DropDownWidth="210px" Height="180px" 
                                    AutoPostBack="True" >
                                            <HeaderTemplate>
                                                <table style="width: 200px" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td style="width: 100px;">
                                                            Clave
                                                        </td>
                                                            <td style="width: 100px;">
                                                            Nombre
                                                        </td>      
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table style="width: 200px" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td style="width: 100px;">
                                                        <%# DataBinder.Eval(Container.DataItem, "maAyuTipCve") %>
                                                        </td>
                                                        <td style="width: 100px;">
                                                        <%# DataBinder.Eval(Container.DataItem, "maAyuTipAbr") %>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Literal runat="server" ID="RadComboItemsCount" />
                                            </FooterTemplate>
                                        </telerik:RadComboBox>
                        </td>
                        <td style=" width:90px; background-color:transparent;">
                        </td>
                        <td style=" width:200px;  background-color:transparent;">      
                        </td>
                    </tr>
                    <tr>
                        <td style=" width:100px; background-color:transparent;">
                            <telerik:RadLabel ID="Label1" runat="server" Text="Clave"  ></telerik:RadLabel>
                        </td>
                        <td style=" width:190px;  background-color:transparent;">                             
                                <telerik:RadTextBox  ID="rTxtCve" runat="server" MaxLength="10"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                            Width="170px" Enabled="false"></telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr style="height:18px;">
                        <td style=" width:90px; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel5" runat="server" Text="Descripción" ></telerik:RadLabel>  
                        </td>
                        <td style=" width:200px;  background-color:transparent;"> 
                                                   
                            <telerik:RadTextBox ID="rTxtDes" runat="server" MaxLength="50"  
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid"  Enabled="false" Width="273px"></telerik:RadTextBox>

                        </td>
                        <td style=" width:90px; background-color:transparent;">
                        </td>
                        <td style=" width:200px;  background-color:transparent;">                             
                        </td>
                    </tr>
                    <tr style="height:18px;">
                        <td style=" width:100px; background-color:transparent;" >
                            <telerik:RadLabel ID="RadLabel1" runat="server" Text="Descripción Extendida"></telerik:RadLabel>
                        </td>
                        <td style=" width:190px;  background-color:transparent;">
                            <textarea id="txtDesExt" runat="server" cols="35" rows="6" maxlength="300"></textarea>
                        </td>
                            <td style=" width:190px;  background-color:transparent;" colspan="2">
                        
                        </td>
                    </tr>
                </table>
                </fieldset>
                </td>
                    <td>
                    <fieldset style=" height:205px; width:360px;  margin-top:0px; margin-left:3px; margin-right:3px;   display: block; text-align:center; align-items:center; background-color:transparent; " >
                                    <legend>Archivo</legend>
                    <table border="0" style=" width:50%;  text-align:left; background-color:transparent ;">
                        <tr style="height:18px;">
                            <td style=" width:90px;  background-color:transparent;" rowspan="4">
                                    
                                    <telerik:RadBinaryImage ImageAlign="Middle" ID="RadBinaryImage1" AutoAdjustImageControlSize="false"  Width="348px" Height="167px" runat="server" Enabled="false" BackColor="WhiteSmoke"  />
                                    <telerik:RadAsyncUpload   ID="RadAsyncUpload1" InputSize="50" OnClientFilesUploaded="fileUploaded" OnFileUploaded="RadAsyncUpload1_FileUploaded"  AllowedFileExtension="jpg,jpeg,png,gif"  runat="server" HideFileInput="true"  
                                    MaxFileInputsCount="1"  Width="10px"  Enabled="false" ></telerik:RadAsyncUpload>
                                <div class="demo-container" >
                                    <video controls="controls"  id="video1" runat="server" style="background-color:aqua; height:134px; width:280px;" visible="false"> 
                                      <source id="sourceid" type="video/mp4" runat="server" src="" /> 
                                    </video>
                                </div>
                            </td>
                        </tr>
                    </table>       
                   </fieldset>
                </td>
                </tr>
                </table>
               <%-- DATOS--%>
                
              <%--  ARCHIVO--%>
               
                   <table>
                        <tr>
                            <td>
                                <telerik:RadTextBox ID="rtxtFiltro" runat="server" Width="355px" MaxLength="50"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid"></telerik:RadTextBox>
                            </td>
                            <td>
                                <telerik:RadImageButton  ID="rBtnBuscar"  runat ="server" Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/icoAyuda/Buscar.png" Image-Url="~/Imagenes/icoAyuda/Buscar.png" Image-HoveredUrl="~/Imagenes/icoAyuda/Buscar.png"   OnClick="rBtnBuscar_Click"  ToolTip="Buscar"  Text ="" ></telerik:RadImageButton>
                            </td>
                        </tr>
                        <tr>
                        </tr>
                    </table>

                    <div style="width:100%; margin-top:5px; display:table; position:static; background-color:transparent;" align="center" > 
                          <telerik:RadGrid ID="rGdv_Ayuda"  OnSelectedIndexChanged="rGdv_Ayuda_SelectedIndexChanged"
                                           runat="server" 
                                           AutoGenerateColumns="False"
                                           Width="760px"   Height="290px"  
                                           CssClass="Grid" 
                                           Skin="Office2010Silver">
                                <MasterTableView DataKeyNames="maAyuCve" AutoGenerateColumns="false" CssClass="GridTable">
                                    <Columns>
                                        <telerik:GridBoundColumn HeaderText="Clave" DataField="maAyuCve" HeaderStyle-Width="50px" ></telerik:GridBoundColumn>
                                         <telerik:GridBoundColumn HeaderText="Tipo" DataField="maAyuTipDes" HeaderStyle-Width="55px" ></telerik:GridBoundColumn>
                                         <telerik:GridBoundColumn HeaderText="Descripción" DataField="maAyuDes" HeaderStyle-Width="100px" ></telerik:GridBoundColumn>                             
                                        <telerik:GridBoundColumn HeaderText="Descripción Extendida" DataField="maAyuDesExt" HeaderStyle-Width="200px"  ></telerik:GridBoundColumn>
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
         </asp:Panel>
              <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                    <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
                            <telerik:RadImageButton ID="rBtnGuardar" Enabled="false"   runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text =""  OnClientClicking="OnClientClic_ConfirmOK" OnClick="rBtnGuardar_Click"  ></telerik:RadImageButton>
                            <telerik:RadImageButton ID="rBtnCancelar" Enabled="false"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text="" OnClientClicking="OnClientClic_ConfirmCancel" OnClick="rBtnCancelar_Click" ></telerik:RadImageButton>
                    </asp:Panel>
                    <asp:HiddenField ID="hdfBtnAccion" runat="server" />
                    <asp:HiddenField ID="arregloImagen"   runat="server" />
            
           </ContentTemplate>
            </asp:UpdatePanel>
        <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server"></telerik:RadWindowManager>
    </form>
      
</body>
</html>
