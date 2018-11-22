<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CopiaConfigAcum.aspx.cs" Inherits="RPT_CopiaConfigAcum" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        
    <link rel="stylesheet" type="text/css" href="../css/cssMPForm.css"/>
    <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
    <script src="../Scripts/JSGeneral.js" type="text/javascript"></script>
    <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
    <link href="~/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate> 
    <div>

        <fieldset  >
        <legend>Reporte</legend>       
            <div style="display:table;  background-color:transparent; width:100%; position: static; " >
                    <table border="0" style=" width:380px; text-align:left; background-color:transparent;">
                        <tr style="height:18px;">
                            <td style=" width:100px; background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel1" runat="server" Text="Clave"></telerik:RadLabel>  
                            </td>
                            <td style=" width:280px; background-color:transparent;">
                                <telerik:RadLabel ID="lblRep" runat="server" Text=""></telerik:RadLabel>  
                            </td>
                        </tr>
                        <tr style="height:18px;">
                            <td style=" width:100px; background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel10" runat="server" Text="Nombre"></telerik:RadLabel>  
                            </td>
                            <td style=" width:280px; background-color:transparent;">
                                <telerik:RadLabel ID="lblDes" runat="server" Text=""></telerik:RadLabel>  
                            </td>
                        </tr>
                    </table>
            </div>
        </fieldset>

            <fieldset  >   
            <legend style="text-align:left;">Reporte Copia</legend>    
                <div style="display:table;  background-color:transparent; width:100%; position: static; ">

                    <table border="0" style=" width:380px; text-align:left; background-color:transparent;">
                        <tr style="height:18px;">
                            <td style=" width:100px; background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel3" runat="server" Text="Clave"></telerik:RadLabel>  
                            </td>
                            <td style=" width:280px; background-color:transparent;">
                                <telerik:RadTextBox ID="rTxtCve" runat="server" Width="200px" 
                                                            EnabledStyle-CssClass="cssTxtEnabled"
                                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                                            HoveredStyle-CssClass="cssTxtHovered"
                                                            FocusedStyle-CssClass="cssTxtFocused"
                                                            InvalidStyle-CssClass="cssTxtInvalid"  
                                                            MaxLength="20"
                                 ></telerik:RadTextBox>  
                            </td>
                        </tr>
                        <tr style="height:18px;">
                            <td style=" width:100px; background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel2" runat="server" Text="Nombre"></telerik:RadLabel>  
                            </td>
                            <td style=" width:280px; background-color:transparent;">
                                <telerik:RadTextBox ID="rTxtDes" runat="server" Width="200px" 
                                                            EnabledStyle-CssClass="cssTxtEnabled"
                                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                                            HoveredStyle-CssClass="cssTxtHovered"
                                                            FocusedStyle-CssClass="cssTxtFocused"
                                                            InvalidStyle-CssClass="cssTxtInvalid"  
                                                            MaxLength="100"
                                 ></telerik:RadTextBox>  
                            </td>
                        </tr>

                    </table>
                </div>
            </fieldset>   

        <asp:Panel ID="pnlBtnsAplicaAccion" Width="99%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
            <telerik:RadImageButton ID="rBtnGuardar"   runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text ="" OnClick="rBtnGuardar_Click"  OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
        </asp:Panel>

    </div>

    <asp:HiddenField ID="hdfDocCve" runat="server" />

    </ContentTemplate>
    </asp:UpdatePanel>  

    <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
    </telerik:RadWindowManager>

    </form>
</body>
</html>
