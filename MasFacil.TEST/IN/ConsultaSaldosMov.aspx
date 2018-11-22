<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConsultaSaldosMov.aspx.cs" Inherits="IN_ConsultaSaldosMov" %>

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
                    <asp:Label ID="lblTitlePage" runat="server" Text="Consulta de Movimientos"></asp:Label> 
                </asp:Panel> 
            </td>
        </tr>
    </table>

    <table border="0"  style="width:100%; height:100%; border-style:none; background-color:white;">
        <tr align="top" align="left" style="width:100%; ">
            <td>
                <fieldset style="">
                <legend>Articulo / Almacen</legend>
                <div style="width:100%; display:table; margin-top:0px; position:static; background-color:transparent;" align="left">
                    <asp:Label ID="LblReferencia" runat="server"></asp:Label>
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
                        Width="1170px" Height="320px"   
                        CssClass="Grid" 
                        Skin="Office2010Silver">
                        <MasterTableView     AutoGenerateColumns="False" CssClass="GridTable">
                        <Columns >
                            
                                <telerik:GridBoundColumn DataField="Folio"  HeaderText="Folio"  HeaderStyle-Width="80px" ItemStyle-Width="80px" Display="true" />
                                <telerik:GridBoundColumn DataField="Poliza" HeaderText="A. Cont"  HeaderStyle-Width="100px"  ItemStyle-Width="100px" Display="true" />
                                <telerik:GridBoundColumn DataField="cptoId"  HeaderText="Concepto" HeaderStyle-Width="80px" ItemStyle-Width="80px" Display="true" />
                                <telerik:GridBoundColumn DataField="Fec_Mov" HeaderText="Fecha Mov"  HeaderStyle-Width="180px"  ItemStyle-Width="90px" Display="true"  DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>
                                <telerik:GridBoundColumn DataField="CoA" HeaderText="CoA"  HeaderStyle-Width="80px"  ItemStyle-Width="80px" Display="true" />
                                <telerik:GridBoundColumn DataField="acumMovINRef_Art" HeaderText="Articulo"  HeaderStyle-Width="100px"  ItemStyle-Width="100px" Display="true" />
                                <telerik:GridBoundColumn DataField="artDes" HeaderText="Descripción"  HeaderStyle-Width="250px"  ItemStyle-Width="250px" Display="true" />
                                <telerik:GridBoundColumn DataField="acumMovINRef_Alm" HeaderText="Almacen"  HeaderStyle-Width="150px"  ItemStyle-Width="150px" Display="true" />                             
                                <telerik:GridBoundColumn DataField="acumMovINFac_Cant" HeaderText="Cantidad"  HeaderStyle-Width="70px"  ItemStyle-Width="70px" Display="true"  DataFormatString="{0:###,##0}" HeaderStyle-HorizontalAlign ="Right" ItemStyle-HorizontalAlign="Right"  />                             
                                <telerik:GridBoundColumn DataField="acumMovINRef_Lote" HeaderText="Lote"  HeaderStyle-Width="100px"  ItemStyle-Width="100px" Display="true" />                             
                                <telerik:GridBoundColumn DataField="acumMovINRef_UniMed" HeaderText="U. Medida"  HeaderStyle-Width="100px"  ItemStyle-Width="100px" Display="true" />                             
                                <telerik:GridBoundColumn DataField="acumMovINFac_Costo" HeaderText="Costo"  HeaderStyle-Width="100px"  ItemStyle-Width="100px" Display="true"  DataFormatString="{0:###,##0.00}"  HeaderStyle-HorizontalAlign ="Right" ItemStyle-HorizontalAlign="Right"  />                             
                                <telerik:GridBoundColumn DataField="acumMovINImp_Imp" HeaderText="Importe"  HeaderStyle-Width="100px"  ItemStyle-Width="100px" Display="true" DataFormatString="{0:###,##0.00}"  HeaderStyle-HorizontalAlign ="Right" ItemStyle-HorizontalAlign="Right" />                             
                                <telerik:GridBoundColumn DataField="acumMovINFac_Prec" HeaderText="Precio"  HeaderStyle-Width="100px"  ItemStyle-Width="100px" Display="true"  DataFormatString="{0:###,##0.00}"  HeaderStyle-HorizontalAlign ="Right" ItemStyle-HorizontalAlign="Right" />                             
                                <telerik:GridBoundColumn DataField="acumMovINRef_OrdComp" HeaderText="O. Compra"  HeaderStyle-Width="100px"  ItemStyle-Width="100px" Display="true" />                             
                                <telerik:GridBoundColumn DataField="acumMovINRef_Prov" HeaderText="Proveedor"  HeaderStyle-Width="100px"  ItemStyle-Width="100px" Display="true" />                             
                
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
    



    
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>  

    <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
    </telerik:RadWindowManager>

    </form>
</body>
</html>
