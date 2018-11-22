<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConsultaSaldosInteg.aspx.cs" Inherits="CC_ConsultasSaldosInteg" %>

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
                    <asp:Label ID="lblTitlePage" runat="server" Text="Consulta de Integración de Saldos"></asp:Label> 
                </asp:Panel> 
            </td>
        </tr>
    </table>

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
        <%--<tr>
        <td>
            <telerik:RadLabel ID="RadLabel1" Text="Fecha Consulta" runat="server"></telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDatePicker ID="RdDtFechaConsulta" runat="server"></telerik:RadDatePicker>
        </td>
            
        </tr>--%>
        <tr align="top" align="center" style="width:100%; ">
            <td>
               <fieldset style="">
                 &nbsp; <telerik:RadLabel ID="RadLabel1" Text="Fecha Consulta" runat="server"></telerik:RadLabel>
            <telerik:RadDatePicker ID="RdDtFechaConsulta" runat="server" Width="110px" OnSelectedDateChanged="RdDtFechaConsulta_SelectedDateChanged" AutoPostBack="true" ></telerik:RadDatePicker>
                <div style="width:100%; display:table; margin-top:5px; position:static; background-color:transparent;" align="center">

                    <telerik:RadGrid ID="rGdvIntegracionSaldosCC"  AllowSorting="True"  ShowFooter="true"   
                        runat="server" 
                        AllowMultiRowSelection="false"
                        AutoGenerateColumns="False" 
                        Width="780px" Height="320px"   
                        CssClass="Grid" 
                        Skin="Office2010Silver">
                        <MasterTableView AllowMultiColumnSorting="true" AutoGenerateColumns="False" CssClass="GridTable" >
                             <Columns>
                                    <telerik:GridBoundColumn DataField="movRef10_Princ"  HeaderText="Referencia" HeaderStyle-Width="120px" ItemStyle-Width="120px" Display="true" />
                                    <telerik:GridBoundColumn DataField="cptoId" HeaderText="Concepto" HeaderStyle-Width="150px"  ItemStyle-Width="150px" Display="true" />
                                    <telerik:GridBoundColumn DataField="FecMov"  HeaderText="Fecha Mov" HeaderStyle-Width="100px" ItemStyle-Width="100px" Display="true" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>
                                    <telerik:GridBoundColumn DataField="FecVen" HeaderText="Fecha Ven" HeaderStyle-Width="100px" ItemStyle-Width="100px" Display="true" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>
                                    <telerik:GridBoundColumn DataField="DiasDifVencido" HeaderText="Dias Ven." HeaderStyle-Width="100px" ItemStyle-Width="100px" Display="true" />
                                    <telerik:GridBoundColumn DataField="DiasDifporVencer" HeaderText="Dias Por Ven." HeaderStyle-Width="100px" ItemStyle-Width="100px" Display="true" />
                                    <telerik:GridBoundColumn FooterText="Total" Aggregate="Sum" DataField="saldo" HeaderText="Importe." HeaderStyle-Width="110px" FooterAggregateFormatString="Total: {0:###,##0.00}"  ItemStyle-Width="110px" Display="true"  DataFormatString="{0:###,##0.00}" ItemStyle-HorizontalAlign="Right" /> 


                      
                             </Columns>
                        <NoRecordsTemplate>No se encontraron registros.</NoRecordsTemplate>    
                        </MasterTableView>
                        <ClientSettings AllowDragToGroup="true">
                        </ClientSettings>
                        <GroupingSettings ShowUnGroupButton="true"></GroupingSettings>
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
    <asp:HiddenField ID="hdfBtnAccion" runat="server" />
    </form>
</body>
</html>
