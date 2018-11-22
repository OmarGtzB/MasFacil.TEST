<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VerErrores.aspx.cs" Inherits="DC_VerErrores" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <script src="http://code.jquery.com/jquery-1.11.0.min.js"></script>
    
    <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
    <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
 
    <link href="../css/styles.css" rel="stylesheet" type="text/css" />
   <link href="/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />


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
                      
                    <telerik:RadGrid ID="rGdVerErrores" runat="server"
                           AllowMultiRowSelection="true"
                           AutoGenerateColumns="False" 
                           Width="666px" Height="380px"   
                           CssClass="Grid" 
                           Skin="Office2010Silver" >

                            <MasterTableView DataKeyNames="valProcEjecId"  AutoGenerateColumns="False"   CssClass="GridTable" >
                                 <Columns>
                                        <telerik:GridBoundColumn DataField="maMSGDes"  HeaderText="Descripción"  HeaderStyle-Width="280px" ItemStyle-Width="280px" />
                                        <telerik:GridBoundColumn DataField="maUsuCve" HeaderText="Usuario"  HeaderStyle-Width="100px"  ItemStyle-Width="200px" />
                                        <telerik:GridBoundColumn DataField="valProcEjecFecReg"  HeaderText="Fecha" HeaderStyle-Width="80px" ItemStyle-Width="100px" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" />
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
        </ContentTemplate>
        </asp:UpdatePanel>
        
    <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
    </telerik:RadWindowManager>
    </form>
</body>
</html>
