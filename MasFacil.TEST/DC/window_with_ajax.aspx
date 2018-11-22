<%@ Page Language="C#" AutoEventWireup="true" CodeFile="window_with_ajax.aspx.cs" Inherits="window_with_ajax" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="Scriptmanager1" runat="server" />



    <telerik:RadWindow runat="server" ID="RadWindow1">
        <ContentTemplate>
            <asp:UpdatePanel ID="Updatepanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label ID="Label1" Text="the current time of the AJAX request in the RadWindow will appear here" runat="server" />
                    <br />
                    <asp:Button ID="Button1" Text="Perform an AJAx request in the RadWindow" OnClick="Button1_Click" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </telerik:RadWindow>

    <asp:Button ID="Button2" Text="open the RadWindow" OnClientClick="$find('RadWindow1').show(); return false;" runat="server" />
    
        
        
        <asp:UpdatePanel ID="Updatepanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Label ID="Label2" Text="this is on the main page - an independent update panel. Time of the postback that will udpate it will appear here" runat="server" />
            <br />
            <asp:Button ID="Button3" Text="Perform an AJAX request that will not dispose the RadWindow" OnClick="Button3_Click" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
