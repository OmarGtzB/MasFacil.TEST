<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CancelaDoc.aspx.cs" Inherits="FR_CancelaDoc" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
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
    <div  >
    
        <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
            <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar" OnClick="rBtnLimpiar_Click"  Text="" ></telerik:RadImageButton>    
        </asp:Panel>
                    <fieldset  >
                    <legend>Documento a Cancelar</legend>       
                <div style="display:table;  background-color:transparent; width:100%; position: static; " >

                    <table border="0" style=" width:380px; text-align:left; background-color:transparent;">
                        <tr style="height:18px;">
                            <td style=" width:100px; background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel1" runat="server" Text="Documento"></telerik:RadLabel>  
                            </td>
                            <td style=" width:280px; background-color:transparent;">
                                <telerik:RadLabel ID="lblDoc" runat="server" Text=""></telerik:RadLabel>  
                            </td>
                        </tr>
                        <tr style="height:18px;">
                            <td style=" width:100px; background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel10" runat="server" Text="Folio"></telerik:RadLabel>  
                            </td>
                            <td style=" width:280px; background-color:transparent;">
                                <telerik:RadLabel ID="lblFolio" runat="server" Text=""></telerik:RadLabel>  
                            </td>
                        </tr>
                        <tr style="height:18px;">
                            <td style=" width:100px; background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel4" runat="server" Text="Descripción"></telerik:RadLabel>  
                            </td>
                            <td style=" width:280px; background-color:transparent;">
                                <telerik:RadLabel ID="lblDes" runat="server" Text=""></telerik:RadLabel>  
                            </td>
                        </tr>
                        <tr style="height:18px;">
                            <td style=" width:100px; background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel12" runat="server" Text="Cliente"></telerik:RadLabel>  
                            </td>
                            <td style=" width:280px; background-color:transparent;">
                                <telerik:RadLabel ID="lblCliente" runat="server" Text=""></telerik:RadLabel>   
                            </td>
                        </tr>
                    </table>
                </div>
            </fieldset>

            <fieldset  >   
            <legend style="text-align:left;">Documento Contra</legend>    
                <div style="display:table;  background-color:transparent; width:100%; position: static; ">

                    <table border="0" style=" width:380px; text-align:left; background-color:transparent;">
                        <tr style="height:18px;">
                            <td style=" width:100px; background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel2" runat="server" Text="Documento"></telerik:RadLabel>  
                            </td>
                            <td style=" width:280px; background-color:transparent;">
                                <telerik:RadComboBox ID="rCboDocumento" runat="server" Width="270px" AutoPostBack="true"
                                                    HighlightTemplatedItems="true"
                                                    DropDownCssClass="cssRadComboBox" 
                                                    DropDownWidth="280px" 
                                                    Height="200px" OnSelectedIndexChanged="rCboDocumento_SelectedIndexChanged"  >
                                                    <HeaderTemplate>
                                                        <table style="width: 280px" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td style="width: 90px;">
                                                                        Documento
                                                                    </td>
                                                                    <td style="width: 190px;">
                                                                        Descripción
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table style="width: 280px;"  cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width:90px" >
                                                                    <%# DataBinder.Eval(Container.DataItem, "docCve")%>
                                                                </td>
                                                                <td style="width: 190px;">
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
                        </tr>
                        <tr style="height:18px;">
                            <td style=" width:100px; background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel3" runat="server" Text="Folio"></telerik:RadLabel>  
                            </td>
                            <td style=" width:280px; background-color:transparent;">
                                <telerik:RadTextBox ID="rTxtFolio" runat="server" Width="100px" 
                                        EnabledStyle-CssClass="cssTxtEnabled"
                                        DisabledStyle-CssClass ="cssTxtEnabled"
                                        HoveredStyle-CssClass="cssTxtHovered"
                                        FocusedStyle-CssClass="cssTxtFocused"
                                        InvalidStyle-CssClass="cssTxtInvalid"  
                                        MaxLength="10"
                                 ></telerik:RadTextBox>  
                            </td>
                        </tr>
                        <tr style="height:18px;">
                            <td style=" width:100px; background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel14" runat="server" Text="Fecha"></telerik:RadLabel>  
                            </td>
                            <td style=" width:280px; background-color:transparent;">
                                 <telerik:RadLabel ID="lblFecha" runat="server" Text=""></telerik:RadLabel> 
                            </td>
                        </tr>
                    </table>
                </div>
            </fieldset>

        <asp:Panel ID="pnlBtnsAplicaAccion" Width="99%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
            <telerik:RadImageButton ID="rBtnGuardar"   runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text ="" OnClick="rBtnGuardar_Click"  OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
            <%--<telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text="" OnClick="rBtnCancelar_Click"  OnClientClicking="OnClientClic_ConfirmCancel"></telerik:RadImageButton>--%>
        </asp:Panel>

    </div>
    </ContentTemplate>
    </asp:UpdatePanel>  

    <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
    </telerik:RadWindowManager>


    </form>
</body>
</html>
