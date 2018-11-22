<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MttoPerfilMs.aspx.cs" Inherits="SG_MttoPerfilMs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
     <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
     <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
     <link href="~/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />

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
                    <telerik:AjaxSetting AjaxControlID="RAJAXMAN1">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID = "imgPerfil" />
                            <telerik:AjaxUpdatedControl ControlID = "arregloImagen" /> 
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                 </AjaxSettings>
        </telerik:RadAjaxManager>
        
  <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Silk"></telerik:RadAjaxLoadingPanel>

  <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" BackColor="White" Transparency="30"></telerik:RadAjaxLoadingPanel>

    <div>
<%--    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>--%>
              <asp:Panel ID="pnlPerfil" runat="server">
    <fieldset>
    <legend>Usuario</legend>
    <table style="background-color:transparent;">
          <tr>
            <td rowspan = "4" style=" border-color:black;">
             <telerik:RadBinaryImage ID="imgPerfil" runat="server" Height="120px" Width="100px" ImageAlign="Left"  AutoAdjustImageControlSize="false" BorderColor="Black" BorderWidth="1px"  /> 
             <telerik:RadAsyncUpload  ID="RadAsyncUpload1" InputSize="50" AllowedFileExtension="jpg,jpeg,png,gif"  runat="server" HideFileInput="true"  
                                        MaxFileInputsCount="1"  Width="10px"  Enabled="true" OnFileUploaded="RadAsyncUpload1_FileUploaded"  OnClientFilesUploaded="fileUploaded" ></telerik:RadAsyncUpload>
            </td>
            <td>
              <telerik:RadLabel ID="RadLabel2" runat="server" CssClass ="LblLeySelctConpania" Text="Usuario:"></telerik:RadLabel>
            </td>
            <td style=" text-align:left;">
              <telerik:RadLabel ID="rlblUsarios" runat="server" CssClass ="LblLeySelctConpania" Text=""  ></telerik:RadLabel>
            </td>
         </tr>
         <tr>
            <td>
             <telerik:RadLabel ID="RadLabel4" runat="server" CssClass ="LblLeySelctConpania" Text="Nombre"></telerik:RadLabel>
            </td>
            <td>
                <telerik:RadTextBox ID="rdtxtNombreUsr" runat="server" Width="200px" MaxLength="25"
                 EnabledStyle-CssClass="cssTxtEnabled"
                DisabledStyle-CssClass ="cssTxtEnabled"
                HoveredStyle-CssClass="cssTxtHovered"
                FocusedStyle-CssClass="cssTxtFocused"
                InvalidStyle-CssClass="cssTxtInvalid"></telerik:RadTextBox>
            </td>
         </tr>
         <tr style="width:200px; height:30px; background-color:transparent;">
         </tr>
    </table>

     </fieldset>
           <asp:Panel ID="Panel1" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server" >
            <telerik:RadImageButton  ID="rBtnGuardarPerfil"  runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text =""    Enabled="true"   OnClick ="rBtnGuardarPerfil_Click"  OnClientClicking="OnClientClic_ConfirmOK" ></telerik:RadImageButton>
            <telerik:RadImageButton  ID="rBtnCancelarPerfil"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text=""    Enabled="true" OnClick="rBtnCancelarPerfil_Click"  OnClientClicking="OnClientClic_ConfirmCancel" ></telerik:RadImageButton>
        </asp:Panel>
        <asp:HiddenField ID="arregloImagen"   runat="server" />
    </asp:Panel>
     <%--   </ContentTemplate>
    </asp:UpdatePanel>--%>
     <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
    </telerik:RadWindowManager>
    </div>
    </form>
</body>
</html>
