<%@ Page Title="" Language="C#" MasterPageFile="~/MPForm.master" AutoEventWireup="true" CodeFile="MGMMenu.aspx.cs" Inherits="Menu_MGMMenu" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMPForm" Runat="Server">
   
    <link href="../css/styles.css" rel="stylesheet" type="text/css" />

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMPForm" Runat="Server">
        
    <table style=" width:100%; background-color:transparent; margin-top:10px">
        <tr>
            <td align="center">

                <asp:DataList ID="DataList1" runat="server" HorizontalAlign="Center" DataKeyField ="maMenuId" Height="300px" Width="300px"
                     CssClass="LblDescOpc" >
                    <ItemTemplate> 
                        <div runat="server" id="div_menu" align="center" class="">  

                            <telerik:RadButton  runat="server"
                                                ID="btn_Menu"
                                  
                                                NavigateUrl='<%# Eval("maOperEjecUrl") %>' 
                                                OnClientClicked='<%# Eval("maOperEjecFn") %>' 
                                                AutoPostBack="false" 
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

    <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"   >
    </telerik:RadWindowManager> 



</asp:Content>

