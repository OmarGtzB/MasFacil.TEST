<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Confirm.aspx.cs" Inherits="DC_Confirm" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

     <script src="../Scripts/MGM/jsConfirm.js" type="text/javascript"></script>


</head>
<body>
    <form id="form1" runat="server">
    <div>
        <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />

    <div >
        <div style="height: 50px">
            <telerik:RadButton RenderMode="Lightweight" ID="btnStandardConfirm" runat="server" Text="Standard window.confirm"
                OnClientClicking="StandardConfirm" OnClick="Button_Click">
            </telerik:RadButton>
            <asp:Label ID="Label1" runat="server" EnableViewState="false" ForeColor="Green"></asp:Label>
        </div>
        <div style="height: 50px">
            <telerik:RadButton RenderMode="Classic" ID="btnRadConfirm" runat="server"   Text="RadConfirm" OnClientClicking="RadConfirm" OnClick="Button_Click">
            </telerik:RadButton>
            <asp:Label ID="Label2" runat="server" EnableViewState="false" ForeColor="Green"></asp:Label>
            <telerik:RadWindowManager RenderMode="Lightweight" ID="windowManager1" B runat="server" Style="z-index: 100001">
            </telerik:RadWindowManager>
        </div>
        <div>
            <telerik:RadButton RenderMode="Lightweight" ID="btnCustomRadWindowConfirm" runat="server" Text="Custom RadWindow Confirm"
                OnClientClicking="CustomRadWindowConfirm" OnClick="Button_Click">
            </telerik:RadButton>


            <asp:Label ID="Label3" runat="server" EnableViewState="false" ForeColor="Green"></asp:Label>

            <telerik:RadWindow RenderMode="Lightweight" ID="confirmWindow" runat="server" VisibleTitlebar="true" Title="sdasd" VisibleStatusbar="false"
                Modal="true" Behaviors="None" Height="170px" Width="240px" Style="z-index: 100001">
                <ContentTemplate>
                    <div style="margin-top: 30px; float: left;">
                        <div style="width: 60px; padding-left: 15px; float: left;">
                            <img src="images/ModalDialogAlert.gif" alt="Confirm Page" />
                        </div>
                        <div style="width: 300px; float: left;">
                            <asp:Label ID="lblConfirm" Font-Size="14px" Text="Are you sure you want to submit the page?"
                                runat="server"></asp:Label>
                            <br />
                            <br />
                            <telerik:RadButton RenderMode="Lightweight" ID="btnYes" runat="server" Text="Yes" AutoPostBack="false" OnClientClicked="YesOrNoClicked">
                                <Icon PrimaryIconCssClass="rbOk"></Icon>
                            </telerik:RadButton>
                            <telerik:RadButton RenderMode="Lightweight" ID="btnNo" runat="server" Text="No" AutoPostBack="false" OnClientClicked="YesOrNoClicked">
                                <Icon PrimaryIconCssClass="rbCancel"></Icon>
                            </telerik:RadButton>
                        </div>
                    </div>
                </ContentTemplate>
            </telerik:RadWindow>
        </div>
    </div>
    <script type="text/javascript">
        function pageLoad() {
           confirmWindow = $find("<%=confirmWindow.ClientID %>");
            btnCustomRadWindowConfirm = $find("<%=btnCustomRadWindowConfirm.ClientID %>");
            btnYes = $find("<%=btnYes.ClientID%>"); 
        }
    </script>
    </div>
    </form>
</body>
</html>
