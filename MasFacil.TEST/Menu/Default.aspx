<%@ Page Title="" Language="C#" MasterPageFile="~/MPForm.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Menu_Default" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMPForm" Runat="Server">
    <link href="../css/styles.css" rel="stylesheet" type="text/css" />   
     <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
     <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMPForm" Runat="Server">
    

 

    <telerik:RadWindow runat="server" ID="RadWindow1"  Modal="true" >
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


    <asp:Button ID="Button4" Text="open the RadWindow" runat="server" />
    
        
        
        <asp:UpdatePanel ID="Updatepanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Label ID="Label2" Text="this is on the main page - an independent update panel. Time of the postback that will udpate it will appear here" runat="server" />
            <br />
            <asp:Button ID="Button3" Text="Perform an AJAX request that will not dispose the RadWindow" OnClick="Button3_Click" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

