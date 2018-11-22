<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TEST.aspx.cs" Inherits="TEST" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txtVal" runat="server" OnTextChanged="TextChanged_TextVal"  AutoPostBack="true"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Button" />
    
    
    </div>
    </form>
</body>
</html>
