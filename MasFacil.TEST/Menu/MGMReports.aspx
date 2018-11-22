<%@ Page Title="" Language="C#" MasterPageFile="~/MPForm.master" AutoEventWireup="true" CodeFile="MGMReports.aspx.cs" Inherits="Menu_MGMReports" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMPForm" Runat="Server">
     <link href="../css/styles.css" rel="stylesheet" type="text/css" />   
     <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
     <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
    <link href="/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />

      <script type="text/javascript">

        function closeRadWindow()  
        {  
          $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest();
        } 
        
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMPForm" Runat="Server">


    <asp:UpdatePanel ID="UpdatePanelMenu" runat="server"  >
    <ContentTemplate>


    <table style=" width:100%; background-color:transparent; margin-top:10px">
        <tr>
            <td align="center">

                <asp:DataList ID="DataListMenu" runat="server" HorizontalAlign="Center" DataKeyField ="maMenuId" Height="300px" Width="300px"
                     CssClass="LblDescOpc" >
                    <ItemTemplate> 
                        <div runat="server" id="div_menu" align="center" class="">  

                            <telerik:RadButton  runat="server"
                                                ID="btn_Menu"

                                                Text='<%# Eval("maOperEjecUrl") %>' 
                                      
                                                OnClick="RadButton1_Click"
                                               

                                  
                                                 
                                                AutoPostBack="true" 
                                                RenderMode="Lightweight"   
                                                CssClass='<%# Eval("maOperCSS") %>'
                                               >
                                                <contenttemplate>
                                                        <table style=" width:180px; height:180px; background-color:transparent;">
                                                        <tr>
                                                            <td  style="width:180px; height:145px ;background-color:transparent;" >
                                                                <asp:Image ID="Image1" runat="server"  ImageUrl='<%# Eval("maMenuIMG") %>' width="100px" height="100px" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td  style="width:180px; height:35px;background-color:transparent;text-align:left;  font-size:14px; vertical-align:top ">
                                                                <telerik:RadLabel ID="radLb_Nombre_menu" runat="server" ForeColor="#ffffff" Text='<%# Eval("maMenuDes") %>'></telerik:RadLabel>
                                                            </td>
                                                        </tr>
                                                        </table>                                                
                                                
                                                
                                                </contenttemplate>
                            </telerik:RadButton>


                        </div> 
                    </ItemTemplate> 
                </asp:DataList>

            </td>
        </tr>
    </table>   
  

            <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server"
                 OnAjaxRequest="RadAjaxManager1_AjaxRequest" >
                <AjaxSettings>
                </AjaxSettings>
            </telerik:RadAjaxManager>


            <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
            </telerik:RadWindowManager>

            <telerik:RadWindow ID="rWinReports" runat="server"  OnClientClose="closeRadWindow" Width="700px" Height="485px" Modal="true" VisibleStatusbar="false" Title="Wizard Reports" VisibleTitlebar="true" Behaviors="Close"  >               
            </telerik:RadWindow>
 

    </ContentTemplate>
    </asp:UpdatePanel> 

</asp:Content>

