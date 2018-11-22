<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConsultaSaldosMes.aspx.cs" Inherits="CC_ConsultaSaldosMes" %>
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

    <script>
        function openPopUpSaldosMov(pPerAnio, pPerNum, cliCve) {
            window.open("ConsultaSaldosMov.aspx?pPerAnio=" + pPerAnio + "&pPerNum=" + pPerNum + "&cliCve=" + cliCve + "&pMovAcum=0", "WindowPopup1", "width=810px, height=480px, resizable");
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
    <div align="center">

    <table border="0"  style="width:100%; height:100%; border-style:none; background-color:white;">
        <tr align="top" align="center" style="width:100%; ">
            <td>
                <asp:Panel ID="pnlTitlePage" runat="server"  CssClass="PnlMPFormTituloApartado" >
                    <asp:Label ID="lblTitlePage" runat="server" Text="Consulta de Saldos"></asp:Label> 
                </asp:Panel> 
            </td>
        </tr>
    </table>

    <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
        <telerik:RadImageButton ID="rBtnSaldoMov"     runat="server"  Width="30px" Height="30px"   Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnMovimientosDisabled.png"        Image-Url="~/Imagenes/IcoBotones/IcoBtnMovimientos.png"      Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnMovimientosHovered.png"  ToolTip="Movimientos"  Text="" OnClick="rBtnSaldoMov_Click"></telerik:RadImageButton>
        <telerik:RadImageButton ID="rBtnLimpiar"      runat="server"  Width="30px" Height="30px"   Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"        Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png"      Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"  Text="" OnClick="rBtnLimpiar_Click"></telerik:RadImageButton>
    </asp:Panel>     
        

    <table border="0"  style="width:100%; height:100%; border-style:none; background-color:white;">
        <tr align="top" align="left" style="width:100%; ">
            <td>
                <fieldset style="">
                <legend>Cliente</legend>
                <div style="width:100%; display:table; margin-top:0px; position:static; background-color:transparent;" align="left">
                    <asp:Label ID="LblClieNombre" runat="server"></asp:Label>
                </div> 
                </fieldset>
            </td>
        </tr>
    </table>

    <table border="0"  style="width:100%; height:100%; border-style:none; background-color:white;">
        <tr align="top" align="center" style="width:100%; ">
            <td>

                <fieldset style="">
                <div style="width:100%; display:table; margin-top:5px; position:static; background-color:transparent;" align="center">

                    <telerik:RadGrid ID="rGdvSaldos" 
                        runat="server"
                        AllowMultiRowSelection="false"
                        AutoGenerateColumns="False" 
                        Width="665px" Height="320px"   
                        CssClass="Grid" 
                        Skin="Office2010Silver">
                        <MasterTableView     AutoGenerateColumns="False" CssClass="GridTable">
                         <Columns >
                                <telerik:GridBoundColumn DataField="perAnio"  HeaderText="perAnio"  HeaderStyle-Width="80px" ItemStyle-Width="80px" Display="false" />
                                <telerik:GridBoundColumn DataField="perNum" HeaderText="perNum"  HeaderStyle-Width="230px"  ItemStyle-Width="230px" Display="false" />
                                <telerik:GridBoundColumn DataField="perDes"  HeaderText="Periodo" HeaderStyle-Width="100px" ItemStyle-Width="100px" Display="true" />
                                <telerik:GridBoundColumn DataField="salCCIni" HeaderText="Saldo Inicial" HeaderStyle-Width="100px"  ItemStyle-Width="100px" Display="true" DataFormatString="{0:###,##0.00}" ItemStyle-HorizontalAlign="Right" />
                                <telerik:GridBoundColumn DataField="salCCCgo" HeaderText="Cargos" HeaderStyle-Width="100px"  ItemStyle-Width="100px" Display="true" DataFormatString="{0:###,##0.00}" ItemStyle-HorizontalAlign="Right" />
                                <telerik:GridBoundColumn DataField="salCCAbo" HeaderText="Abonos" HeaderStyle-Width="100px"  ItemStyle-Width="100px" Display="true" DataFormatString="{0:###,##0.00}" ItemStyle-HorizontalAlign="Right" />
                                <telerik:GridBoundColumn DataField="salAct" HeaderText="Saldo Final" HeaderStyle-Width="100px"  ItemStyle-Width="100px" Display="true" DataFormatString="{0:###,##0.00}" ItemStyle-HorizontalAlign="Right" />                    
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
                            <Scrolling AllowScroll="true" UseStaticHeaders="true"    ScrollHeight="225px"     />
                        </ClientSettings>
                    </telerik:RadGrid>  

                </div> 
                </fieldset>

            </td>
        </tr>
    </table>

    <asp:HiddenField ID="hdfBtnAccion" runat="server" />
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>  

    <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
    </telerik:RadWindowManager>

    </form>
</body>
</html>
