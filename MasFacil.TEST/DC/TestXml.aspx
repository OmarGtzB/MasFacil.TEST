<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestXml.aspx.cs" Inherits="DC_TestXml" %>

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
    <div>
        <telerik:RadLabel ID="RadLabel1" runat="server" Text="1234567"></telerik:RadLabel>
        <asp:Button ID="btnVisializaXML" runat="server" Text="Button" OnClick="btnVisializaXML_Click" />
    </div>
    </form>
</body>
</html>
