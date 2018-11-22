<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GeneraFolios.aspx.cs" Inherits="TEST_GeneraFolios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>

      <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
             <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />

            </Scripts>
        </telerik:RadScriptManager>

        <br />
        <telerik:RadLabel ID="ManejoFolio" Text="Manejo de Folio" runat="server"></telerik:RadLabel>
        <telerik:RadTextBox ID="txtManejoFolio" runat="server"></telerik:RadTextBox>

        <telerik:RadLabel ID="ClaveFolio" Text="Clave del Folio" runat="server"></telerik:RadLabel>
        <telerik:RadTextBox ID="txtClaveFolio" runat="server"></telerik:RadTextBox>

        <telerik:RadLabel ID="valor" runat="server"></telerik:RadLabel>
        <telerik:RadTextBox ID="txtValor" runat="server"></telerik:RadTextBox>
        
        <telerik:RadButton ID="rBtnObtenFolio" runat="server" Text="Genera Folio" OnClick="rBtnObtenFolio_Click"></telerik:RadButton>
        <br />
        <br />
        <br />
        <telerik:RadTextBox ID="rTxtResultado" Runat="server">
        </telerik:RadTextBox>
    </div>
    </form>
</body>
</html>
