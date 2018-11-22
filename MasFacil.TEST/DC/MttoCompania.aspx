<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MttoCompania.aspx.cs" Inherits="DC_MttoCompanias" %>
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
                            <telerik:AjaxUpdatedControl ControlID = "pnlBody" />
                        </UpdatedControls>
                    </telerik:AjaxSetting>

                     <telerik:AjaxSetting AjaxControlID="rBtnCancelar">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID = "pnlBody" />
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                 </AjaxSettings>
        </telerik:RadAjaxManager>

  <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Silk"></telerik:RadAjaxLoadingPanel>

  <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" BackColor="White" Transparency="30"></telerik:RadAjaxLoadingPanel>

<%--        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
            </Scripts>
        </telerik:RadScriptManager> --%>

<%--    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>

        <asp:Panel ID="pnlBody" runat="server">
           
                
                    <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
                        <telerik:RadImageButton ID="rBtnNuevo"     runat ="server" Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text ="" OnClick="rBtnNuevo_Click" Visible="false" ></telerik:RadImageButton>
                        <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  ToolTip="Modificar"  Text="" OnClick="rBtnModificar_Click" Visible="false" ></telerik:RadImageButton>
                        <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text="" OnClick="rBtnEliminar_Click" Visible="false" ></telerik:RadImageButton>
                        <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"   Text="" OnClick="rBtnLimpiar_Click"></telerik:RadImageButton>    
                    </asp:Panel>
                    <asp:Table runat="server"  >
                        <asp:TableRow>
                                <asp:TableCell>
                                    <fieldset style=" height:135px;  margin-top:5px;   display: block; text-align:left;" >
                                    <legend>Datos</legend>
                                        <telerik:RadLabel ID="RadLabel1" runat="server" Text="Clave" Width="110px"></telerik:RadLabel>
                                        <telerik:RadTextBox ID="txt_clave" runat="server" Width="120px" Enabled="false"
                                            EnabledStyle-CssClass="cssTxtEnabled"
                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                            HoveredStyle-CssClass="cssTxtHovered"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid"
                                            ></telerik:RadTextBox>&nbsp;
                                        <telerik:RadLabel ID="RadLabel2" runat="server" Text="Abreviatura" Width="100px"></telerik:RadLabel>
                                        <telerik:RadTextBox ID="txt_abreviatura" runat="server" Width="120px" Enabled="false"
                                            EnabledStyle-CssClass="cssTxtEnabled"
                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                            HoveredStyle-CssClass="cssTxtHovered"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid"
                                            ></telerik:RadTextBox><br />

                                        <telerik:RadLabel ID="RadLabel3" runat="server" Text="Nombre" Width="110px"></telerik:RadLabel>
                                        <telerik:RadTextBox ID="txt_nombre" EnabledStyle-CssClass="cssTxtEnabled" Enabled="false"
                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                            HoveredStyle-CssClass="cssTxtHovered"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid"
                                            runat="server" Width="353px"></telerik:RadTextBox><br />
                                        <telerik:RadLabel ID="RadLabel4" runat="server" Text="Razón Social" Width="110px"></telerik:RadLabel>
                                        <telerik:RadTextBox ID="txt_razonSocial" EnabledStyle-CssClass="cssTxtEnabled" Enabled="false" MaxLength="50"
                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                            HoveredStyle-CssClass="cssTxtHovered"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid"
                                             runat="server" Width="353px"></telerik:RadTextBox><br />
                                        <telerik:RadLabel ID="RadLabel5" runat="server" Text="Reg. Fiscal" Width="110px"></telerik:RadLabel>
                                        <telerik:RadTextBox ID="txt_regFiscal" EnabledStyle-CssClass="cssTxtEnabled" Enabled="false"
                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                            HoveredStyle-CssClass="cssTxtHovered"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid"
                                             runat="server" Width="353px"></telerik:RadTextBox>
                                        <telerik:RadTextBox ID="RadTextBox1" EnabledStyle-CssClass="cssTxtEnabled" Enabled="false" Visible="false"
                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                            HoveredStyle-CssClass="cssTxtHovered"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid"
                                             runat="server" Width="333px"></telerik:RadTextBox>
                                    </fieldset>
                                </asp:TableCell>
                                <asp:TableCell  HorizontalAlign="Center" VerticalAlign="Middle">
                                    <fieldset style=" height:135px; width:205px;  margin-top:5px;   display: block; text-align:center; align-items:center; " >
                                    <legend>Logotipo </legend>
                                          <telerik:RadBinaryImage ImageAlign="Middle" ID="RadBinaryImage1" AutoAdjustImageControlSize="false"  Width="170px" Height="95px" runat="server" Enabled="false" BackColor="WhiteSmoke"/>

                                   <telerik:RadAsyncUpload  ID="RadAsyncUpload1" OnClientFilesUploaded="fileUploaded" OnFileUploaded="RadAsyncUpload1_FileUploaded" InputSize="50" AllowedFileExtension="jpg,jpeg,png,gif"  runat="server" HideFileInput="true"  
                                        MaxFileInputsCount="1"  Width="10px"  Enabled="false" ></telerik:RadAsyncUpload>
                                 </fieldset>
                                 </asp:TableCell>
                        </asp:TableRow>
                       </asp:Table>

                      <asp:Table runat="server">
                         <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" >
                                      <fieldset style="  margin-top:5px;   display: block; text-align:left;" >
                                        <legend>Otros Datos</legend>   
                                          <div style=" width:695px; height:56px; overflow:auto;"  >
                                              <asp:DataList ID="DataListOtrosDatos" runat="server" DataKeyField="otroDatCve">
                                                  <ItemTemplate>
                                                            <telerik:RadLabel ID="RadLabel19" runat="server" Text='<%# Eval("otroDatDes") %>' Width="110px"></telerik:RadLabel>
                                                            <telerik:RadTextBox ID="txt_OtrosDatos" runat="server" Width="200px"  EnabledStyle-CssClass="cssTxtEnabled"
                                                                DisabledStyle-CssClass ="cssTxtEnabled" Enabled="false"
                                                                HoveredStyle-CssClass="cssTxtHovered"
                                                                FocusedStyle-CssClass="cssTxtFocused" Text='<%# Eval("OtroDatVal") %>'
                                                                InvalidStyle-CssClass="cssTxtInvalid">
                                                            </telerik:RadTextBox>
                                                         </ItemTemplate>
                                              </asp:DataList>
                                          </div>
                                     </fieldset>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                

               
                            <div style="margin-top:10px;">
                                <telerik:RadTabStrip RenderMode="Lightweight" Orientation="HorizontalTop" runat="server" ID="RadTabStrip1"  MultiPageID="RadMultiPage1" SelectedIndex="0" Skin="Silk">
                                    <Tabs>
                                        <telerik:RadTab ImageUrl="../Images/direccion.png" Text="Fiscal" Value="1"></telerik:RadTab>
                                        <telerik:RadTab ImageUrl="../Images/direccion.png" Text="Notificaciones" Font-Size="Medium" Value="2" ></telerik:RadTab>
                                        <telerik:RadTab ImageUrl="../Images/direccion.png" Text="Expedición" Font-Size="Medium" Value="3" ></telerik:RadTab>
                                    </Tabs>
                                </telerik:RadTabStrip> 
                            </div>




<%--            <fieldset id="fldDirecciones" runat="server" style="background-color:gainsboro;">   
--%>


    <telerik:RadMultiPage runat="server" ID="RadMultiPage1"  SelectedIndex="0" CssClass="outerMultiPage" BackColor="#E6E6E6" BorderColor="Black" BorderWidth="1px" Width ="715px" >

            <telerik:RadPageView runat="server" ID="RadPageView1">
                <div style="width:100%; display:table; position:static; background-color:transparent;">
            
            <table border="0" style=" width:720px; text-align:left; background-color:transparent ;">
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel7" runat="server" Text="País"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                                <telerik:RadComboBox ID="rCboPais" runat="server" OnSelectedIndexChanged="rCboPais_SelectedIndexChanged" AutoPostBack="True"  
                                                     HighlightTemplatedItems="true" 
                                                     DropDownCssClass="cssRadComboBox"   AllowCustomText="true"
                                                     Width="220px" DropDownWidth="290px" Height="200px">
                                    <FooterTemplate>
                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                    </FooterTemplate>
                                 </telerik:RadComboBox>
                    </td>
                    <td style=" width:20px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel8" runat="server" Text="Estado" Width="20px"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                                <telerik:RadComboBox ID="rCboEntidadFed" runat="server" OnSelectedIndexChanged="rCboEntidadFed_SelectedIndexChanged" AutoPostBack="True"
                                                     HighlightTemplatedItems="true" 
                                                     DropDownCssClass="cssRadComboBox"   AllowCustomText="true" Enabled="false"
                                                     Width="220px" DropDownWidth="290px" Height="200px">
                                    <FooterTemplate>
                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                    </FooterTemplate>
                                 </telerik:RadComboBox>                    

                    </td>
                </tr>
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel9" runat="server" Text="Del/Muni"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                                <telerik:RadComboBox ID="rCboProvincia" runat="server" AutoPostBack="True"  AllowCustomText="true" Enabled="false"
                                                     HighlightTemplatedItems="true"
                                                     DropDownCssClass="cssRadComboBox"  
                                                     Width="220px" DropDownWidth="290px" Height="200px">
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
                    </td>
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel10" runat="server" Text="Colonia"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;"> 
                        <telerik:RadTextBox ID="txt_colonia" runat="server"  Width="220px" MaxLength="50"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>                              
                    </td>
                </tr>

            </table>           

            <table border="0" style=" width:720px; text-align:left; background-color:transparent ;">
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel11" runat="server" Text="Calle"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                        <telerik:RadTextBox ID="txt_calle" runat="server"  Width="220px" MaxLength="50"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                    <td style=" width:90px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel12" runat="server" Text="No Exterior" ></telerik:RadLabel>  
                    </td>
                    <td style=" width:70px; background-color:transparent;">
                        <%--<telerik:RadNumericTextBox ID="rdNumericExterior" runat="server" Width="60px"  NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>--%>
                        <telerik:RadTextBox ID="rdNumericExterior" runat="server"  Width="60px"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                    <td style=" width:85px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel13" runat="server" Text="No Interior"></telerik:RadLabel>  
                    </td>
                    <td style=" width:70px;  background-color:transparent;">                             
                        <%--<telerik:RadNumericTextBox ID="rdNumericInterior" runat="server"  Width="60px" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>--%>
                        <telerik:RadTextBox ID="rdNumericInterior" runat="server"  Width="60px" 
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                </tr>
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel14" runat="server" Text="Calles Aledañas"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                        <telerik:RadTextBox ID="txt_callesAle" runat="server"  Width="220px" MaxLength="50"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel15" runat="server" Text="Codigo Postal" ></telerik:RadLabel>  
                    </td>
                    <td style=" width:70px;  background-color:transparent;">                             
                        <%--<telerik:RadNumericTextBox ID="rdNumericCodigoPostal"  Width="60px" runat="server" NumberFormat-GroupSeparator=""  NumberFormat-DecimalDigits="0" MaxLength="50"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>--%>
                        <telerik:RadTextBox ID="rdNumericCodigoPostal" runat="server"  Width="60px"  MaxLength="50"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                    <td style=" width:90px; background-color:transparent;">
                         
                    </td>
                    <td style=" width:70px; background-color:transparent;">

                    </td>
                </tr>
            </table>           

            <table border="0"  cellpadding="0" cellspacing="0" style=" width:720px; text-align:left; background-color:transparent ;">
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel16" runat="server" Text="Referencia"></telerik:RadLabel>  
                    </td>
                    <td style=" width:590px;  background-color:transparent;">                             
                        <telerik:RadTextBox ID="txt_referencias" runat="server"  Width="580px" MaxLength="50"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                </tr>
            </table>  


            <table border="0"  cellpadding="0" cellspacing="0" style=" width:720px; text-align:left; background-color:transparent ;">
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel17" runat="server" Text="Teléfono 1"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                            <telerik:RadNumericTextBox ID="txt_telefono1" runat="server"  Width="220px"  NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="4" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>
                    </td>
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel19" runat="server" Text="Teléfono 2"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                              <telerik:RadNumericTextBox ID="txt_telefono2" runat="server"   Width="220px"  NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="4" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>              

                    </td>
                </tr>
            </table>    

            <table runat="server" visible="false" border="0"  cellpadding="0" cellspacing="0" style=" width:720px; text-align:left; background-color:transparent ;">
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel18" runat="server" Text="Fax"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                             <telerik:RadNumericTextBox ID="txt_fax" runat="server"  Width="220px" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>
                    </td>
                    <td style=" width:130px; background-color:transparent;">

                    </td>
                    <td style=" width:230px;  background-color:transparent;"> 
                             <telerik:RadTextBox ID="iddom" runat="server" Width="200px"  EnabledStyle-CssClass="cssTxtEnabled"
                                            DisabledStyle-CssClass ="cssTxtEnabled" Enabled="false"
                                            HoveredStyle-CssClass="cssTxtHovered"  Visible="false"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid"></telerik:RadTextBox>
                    </td>
                </tr>
            </table>    



        </div>

    </telerik:RadPageView>



                                <telerik:RadPageView runat="server" ID="RadPageView2">
                <div style="width:100%; display:table; position:static; background-color:transparent;">
            
            <table border="0" style=" width:720px; text-align:left; background-color:transparent ;">
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel6" runat="server" Text="País"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                                <telerik:RadComboBox ID="rCboPaisNotif" runat="server" OnSelectedIndexChanged="rCboPaisNotif_SelectedIndexChanged" AutoPostBack="True"  
                                                     HighlightTemplatedItems="true" AllowCustomText="true" 
                                                     DropDownCssClass="cssRadComboBox"  
                                                     Width="220px" DropDownWidth="290px" Height="200px">
                                    <FooterTemplate>
                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                    </FooterTemplate>
                                 </telerik:RadComboBox>
                    </td>
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel20" runat="server" Text="Estado"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                                <telerik:RadComboBox ID="rCboEntidadFedNotif" runat="server" OnSelectedIndexChanged="rCboEntidadFedNotif_SelectedIndexChanged" AutoPostBack="True"
                                                     HighlightTemplatedItems="true"  AllowCustomText="true"
                                                     DropDownCssClass="cssRadComboBox"  
                                                     Width="220px" DropDownWidth="290px" Height="200px">
                                    <FooterTemplate>
                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                    </FooterTemplate>
                                 </telerik:RadComboBox>                    

                    </td>
                </tr>






                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel21" runat="server" Text="Del/Muni"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                                <telerik:RadComboBox ID="rCboProvinciaNotif" runat="server" AutoPostBack="True" AllowCustomText="true"
                                                     HighlightTemplatedItems="true" 
                                                     DropDownCssClass="cssRadComboBox"   
                                                     Width="220px" DropDownWidth="290px" Height="200px">
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
                    </td>
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel22" runat="server" Text="Colonia"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;"> 
                        <telerik:RadTextBox ID="txt_coloniaNotif" runat="server"  Width="220px" MaxLength="50"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>                              
                    </td>
                </tr>

            </table>           

            <table border="0" style=" width:720px; text-align:left; background-color:transparent ;">
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel23" runat="server" Text="Calle"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                        <telerik:RadTextBox ID="txt_calleNotif" runat="server"  Width="220px" MaxLength="50"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                    <td style=" width:90px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel24" runat="server" Text="No Exterior"></telerik:RadLabel>  
                    </td>
                    <td style=" width:70px; background-color:transparent;">
                      <%--  <telerik:RadNumericTextBox ID="rdNumericExteriorNotif" runat="server" Width="60px"  NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>--%>
                        <telerik:RadTextBox ID="rdNumericExteriorNotif" runat="server"  Width="60px"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                    <td style=" width:85px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel25" runat="server" Text="No Interior"></telerik:RadLabel>  
                    </td>
                    <td style=" width:70px;  background-color:transparent;">                             
                        <%--<telerik:RadNumericTextBox ID="rdNumericInteriorNotif" runat="server"  Width="60px" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>--%>
                        <telerik:RadTextBox ID="rdNumericInteriorNotif" runat="server"  Width="60px" 
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                </tr>
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel26" runat="server" Text="Calles Aledañas"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                        <telerik:RadTextBox ID="txt_callesAleNotif" runat="server"  Width="220px" MaxLength="50"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel27" runat="server" Text="Codigo Postal" ></telerik:RadLabel>  
                    </td>
                    <td style=" width:70px;  background-color:transparent;">                             
                        <%--<telerik:RadNumericTextBox ID="rdNumericCodigoPostalNotif"  Width="60px" runat="server" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>--%>
                        <telerik:RadTextBox ID="rdNumericCodigoPostalNotif" runat="server"  Width="60px"  MaxLength="50"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                    <td style=" width:90px; background-color:transparent;">
                         
                    </td>
                    <td style=" width:70px; background-color:transparent;">

                    </td>
                </tr>
            </table>           

            <table border="0"  cellpadding="0" cellspacing="0" style=" width:720px; text-align:left; background-color:transparent ;">
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel28" runat="server" Text="Referencia"></telerik:RadLabel>  
                    </td>
                    <td style=" width:590px;  background-color:transparent;">                             
                        <telerik:RadTextBox ID="txt_referenciasNotif" runat="server"  Width="580px" MaxLength="50"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                </tr>
            </table>  

            <table border="0"  cellpadding="0" cellspacing="0" style=" width:720px; text-align:left; background-color:transparent ;">
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel29" runat="server" Text="Teléfono 1"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                            <telerik:RadNumericTextBox ID="txt_telefono1Notif" runat="server"  Width="220px"  NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="4" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>
                    </td>
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel30" runat="server" Text="Teléfono 2"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                              <telerik:RadNumericTextBox ID="txt_telefono2Notif" runat="server"  Width="220px"  NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="4" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>              

                    </td>
                </tr>

            </table>           
 



            <table runat="server" visible="false" border="0"  cellpadding="0" cellspacing="0" style=" width:720px; text-align:left; background-color:transparent ;">

                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel31" runat="server" Text="Fax"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                             <telerik:RadNumericTextBox ID="txt_faxNotif" runat="server"  Width="220px" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>
                    </td>
                    <td style=" width:130px; background-color:transparent;">

                    </td>
                    <td style=" width:230px;  background-color:transparent;"> 
                     <telerik:RadTextBox ID="iddomNotif" runat="server" Width="200px"  EnabledStyle-CssClass="cssTxtEnabled"
                                            DisabledStyle-CssClass ="cssTxtEnabled" Enabled="false"
                                            HoveredStyle-CssClass="cssTxtHovered"  Visible="false"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid"></telerik:RadTextBox>        
                    </td>
                </tr>

            </table>           
 

        </div>

                        </telerik:RadPageView>


                      <telerik:RadPageView runat="server" ID="RadPageView3">
                <div style="width:100%; display:table; position:static; background-color:transparent;">
            
            <table border="0" style=" width:720px; text-align:left; background-color:transparent ;">
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel32" runat="server" Text="País"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                                <telerik:RadComboBox ID="rCboPaisExped" runat="server" OnSelectedIndexChanged="rCboPaisExped_SelectedIndexChanged" AutoPostBack="True"  
                                                     HighlightTemplatedItems="true" AllowCustomText="true"
                                                     DropDownCssClass="cssRadComboBox"  
                                                     Width="220px" DropDownWidth="290px" Height="200px">
                                    <FooterTemplate>
                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                    </FooterTemplate>
                                 </telerik:RadComboBox>
                    </td>
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel33" runat="server" Text="Estado"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                                <telerik:RadComboBox ID="rCboEntidadFedExped" runat="server" OnSelectedIndexChanged="rCboEntidadFedExped_SelectedIndexChanged" AutoPostBack="True"
                                                     HighlightTemplatedItems="true" AllowCustomText="true"
                                                     DropDownCssClass="cssRadComboBox"  
                                                     Width="220px" DropDownWidth="290px" Height="200px">
                                    <FooterTemplate>
                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                    </FooterTemplate>
                                 </telerik:RadComboBox>                    

                    </td>
                </tr>


                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel34" runat="server" Text="Del/Muni"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                                <telerik:RadComboBox ID="rCboProvinciaExped" runat="server" AutoPostBack="True" AllowCustomText="true"
                                                     HighlightTemplatedItems="true"
                                                     DropDownCssClass="cssRadComboBox"   
                                                     Width="220px" DropDownWidth="290px" Height="200px">
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
                    </td>



                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel35" runat="server" Text="Colonia"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;"> 
                        <telerik:RadTextBox ID="txt_coloniaExped" runat="server"  Width="220px" MaxLength="50"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>                              
                    </td>
                </tr>

            </table>           

            <table border="0" style=" width:720px; text-align:left; background-color:transparent ;">
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel36" runat="server" Text="Calle"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                        <telerik:RadTextBox ID="txt_calleExped" runat="server"  Width="220px" MaxLength="50"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                    <td style=" width:90px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel37" runat="server" Text="No Exterior"></telerik:RadLabel>  
                    </td>
                    <td style=" width:70px; background-color:transparent;">
                       <%-- <telerik:RadNumericTextBox ID="rdNumericExteriorExped" runat="server" Width="60px"  NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>--%>
                        <telerik:RadTextBox ID="rdNumericExteriorExped" runat="server"  Width="60px"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                    <td style=" width:85px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel38" runat="server" Text="No Interior"></telerik:RadLabel>  
                    </td>
                    <td style=" width:70px;  background-color:transparent;">                             
                        <%--<telerik:RadNumericTextBox ID="rdNumericInteriorExped" runat="server"  Width="60px" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>--%>
                        <telerik:RadTextBox ID="rdNumericInteriorExped" runat="server" Width="60px"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                </tr>
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel39" runat="server" Text="Calles Aledañas"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                        <telerik:RadTextBox ID="txt_callesAleExped" runat="server"  Width="220px" MaxLength="50"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel40" runat="server" Text="Codigo Postal" ></telerik:RadLabel>  
                    </td>
                    <td style=" width:70px;  background-color:transparent;">                             
                       <%-- <telerik:RadNumericTextBox ID="rdNumericCodigoPostalExped"  Width="60px" runat="server" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>--%>
                        <telerik:RadTextBox ID="rdNumericCodigoPostalExped" runat="server"  Width="60px"  MaxLength="50"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                    <td style=" width:90px; background-color:transparent;">
                         
                    </td>
                    <td style=" width:70px; background-color:transparent;">

                    </td>
                </tr>
            </table>           

            <table border="0"  cellpadding="0" cellspacing="0" style=" width:720px; text-align:left; background-color:transparent ;">
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel41" runat="server" Text="Referencia"></telerik:RadLabel>  
                    </td>
                    <td style=" width:590px;  background-color:transparent;">                             
                        <telerik:RadTextBox ID="txt_referenciasExped" runat="server"  Width="580px" MaxLength="50"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                </tr>
            </table>  

            <table border="0"  cellpadding="0" cellspacing="0" style=" width:720px; text-align:left; background-color:transparent ;">
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel42" runat="server" Text="Teléfono 1"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                            <telerik:RadNumericTextBox ID="txt_telefono1Exped" runat="server"  Width="220px"  NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="4" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>
                    </td>
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel43" runat="server" Text="Teléfono 2"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                              <telerik:RadNumericTextBox ID="txt_telefono2Exped" runat="server"  Width="220px"  NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="4" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>              

                    </td>
                </tr>

            </table>     
            <table runat="server" visible="false" border="0"  cellpadding="0" cellspacing="0" style=" width:720px; text-align:left; background-color:transparent ;">

                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel44" runat="server" Text="Fax"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                             <telerik:RadNumericTextBox ID="txt_faxExped" runat="server"  Width="220px" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>
                    </td>
                    <td style=" width:130px; background-color:transparent;">

                    </td>
                    <td style=" width:230px;  background-color:transparent;"> 
                            <telerik:RadTextBox ID="iddomExped" runat="server" Width="200px"  EnabledStyle-CssClass="cssTxtEnabled"
                                            DisabledStyle-CssClass ="cssTxtEnabled" Enabled="false"
                                            HoveredStyle-CssClass="cssTxtHovered"  Visible="false"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid"></telerik:RadTextBox> 
                    </td>
                </tr>

            </table>           
 

        </div>

                        </telerik:RadPageView>








                        </telerik:RadMultiPage>
                 <%--   </fieldset>--%>


        </asp:Panel>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
               
                    <asp:Panel  ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
                        <telerik:RadImageButton  ID="rBtnGuardar"  runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text =""  OnClientClicking="OnClientClic_ConfirmOK" OnClick="rBtnGuardar_Click" Enabled="true"></telerik:RadImageButton>
                        <telerik:RadImageButton  ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text=""  OnClientClicking="OnClientClic_ConfirmCancel" OnClick="rBtnCancelar_Click" Enabled="true" ></telerik:RadImageButton>
                    </asp:Panel>
                     
    
                     <asp:HiddenField ID="hdfBtnAccion" runat="server" />
                     <asp:HiddenField ID="arregloImagen"   runat="server" />
                
            </ContentTemplate>
        </asp:UpdatePanel>
           <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false">
            </telerik:RadWindowManager>
    
    </form>
</body>
</html>
