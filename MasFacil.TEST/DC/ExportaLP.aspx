﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExportaLP.aspx.cs" Inherits="DC_ExportaLP" %>
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


                 </AjaxSettings>
        </telerik:RadAjaxManager>

          <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Silk"></telerik:RadAjaxLoadingPanel>
          <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" BackColor="White" Transparency="30"></telerik:RadAjaxLoadingPanel>
          

                      <asp:UpdatePanel runat="server" ID="UpdatePanel">
                      <ContentTemplate>
                             <%--TXT--%>
                            <asp:Panel runat="server" ID="pnlTxt" >
                                <div>
                                     <fieldset>
                                        <legend>Seleccionar TXT</legend>
                                        <table >
                                        <tr>
                                        <td rowspan="7">
                                             <telerik:RadBinaryImage ID="imgImport" runat="server" Height="120px" Width="120px" ImageAlign="Left"  AutoAdjustImageControlSize="false" BorderColor="Black" BorderWidth="1px"  /> 
                                             <telerik:RadAsyncUpload  ID="RadAsyncUpload1" InputSize="50" AllowedFileExtension="txt"  runat="server" HideFileInput="true"  
                                             MaxFileInputsCount="1"  Width="10px"  Enabled="true" OnFileUploaded="RadAsyncUpload1_FileUploaded"  OnClientFilesUploaded="fileUploaded"  ></telerik:RadAsyncUpload>
                                         </td>
                                        </tr>
                                        <tr>
                                          <td>
                                          </td>
                                        </tr>
                                        </table>
                                  </fieldset>
                                     <asp:Panel ID="Panel1" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server" >
                                        <telerik:RadImageButton  ID="rBtnGuardar"  runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text =""    Enabled="true"   OnClick="rBtnGuardar_Click"  OnClientClicking="OnClientClic_ConfirmOK" ></telerik:RadImageButton>
                                        <telerik:RadImageButton  ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text=""    Enabled="true"  OnClick="rBtnCancelar_Click"  OnClientClicking="OnClientClic_ConfirmCancel" ></telerik:RadImageButton>
                                    </asp:Panel>

                                    <asp:HiddenField ID="arregloExport"   runat="server" />
                                    <asp:HiddenField ID="hdfBtnAccion" runat="server" />
                                    <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server"></telerik:RadWindowManager>
                                </div>
                            </asp:Panel>


                            <%--EXCEL--%>
                   <asp:Panel runat="server" ID="pnlExcel" >
                        <div>
                            <fieldset>
                                <legend>Seleccionar Excel</legend>
                                <table>
                                    <tr>
                                        <td rowspan="7">
                                                <telerik:RadBinaryImage ID="imgImportExcel" runat="server" Height="118px" Width="120px" ImageAlign="Left"  AutoAdjustImageControlSize="false" BorderColor="Black" BorderWidth="1px"  /> 
                                                <telerik:RadAsyncUpload  ID="RadAsyncUpload2" InputSize="50" AllowedFileExtension="txt"  runat="server" HideFileInput="true"  
                                                MaxFileInputsCount="1"  Width="10px"  Enabled="true" OnFileUploaded="RadAsyncUpload1_FileUploadedExcel"  OnClientFilesUploaded="fileUploaded"  ></telerik:RadAsyncUpload>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>

                             <asp:Panel ID="Panel2" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server" >
                                <telerik:RadImageButton  ID="rBtnGuardarExcel"  runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text =""    Enabled="true"    OnClick="rBtnGuardarExcel_Click"  OnClientClicking="OnClientClic_ConfirmOK" ></telerik:RadImageButton>
                                <telerik:RadImageButton  ID="rBtnCancelarExcel"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text=""    Enabled="true"  OnClick="rBtnCancelarExcel_Click"  OnClientClicking="OnClientClic_ConfirmCancel" ></telerik:RadImageButton>
                            </asp:Panel>

                            <asp:HiddenField ID="HdvalInt" runat="server" />
                            <asp:HiddenField ID="HdvalExt" runat="server" />

                            <asp:HiddenField ID="CantItemsElimTrue" Value="0" runat="server" />
                            <asp:HiddenField ID="CantItemsElimFalse" Value="0" runat="server" />

                            <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>
                        </div>
                    </asp:Panel>

                   <%-- TIPO DE ARCHIVO--%>
                    <asp:Panel runat="server" ID="pnlInicio" HorizontalAlign="Center" >

                        <fieldset>
                           <legend>Tipo de Archivo</legend>
                            <asp:Label ID="Label1" runat="server" Text="Selecciona el tipo de archivo que deseas importar."></asp:Label> <br /><br />
                                <asp:RadioButton ID="rbtnTxt" runat="server" Text="Txt" GroupName="Imp" OnCheckedChanged="rbtnTxt_CheckedChanged" AutoPostBack="true" />
                                <asp:RadioButton ID="rbtExcel" runat="server" Text="Excel" GroupName="Imp" OnCheckedChanged="rbtExcel_CheckedChanged" AutoPostBack="true" /><br />
                        </fieldset>
                       
                    </asp:Panel>
                    </ContentTemplate>
                 </asp:UpdatePanel>
     


    </form>
</body>
</html>