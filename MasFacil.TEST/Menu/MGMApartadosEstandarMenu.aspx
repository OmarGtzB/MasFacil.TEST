<%@ Page Title="" Language="C#" MasterPageFile="~/MPForm.master" AutoEventWireup="true" CodeFile="MGMApartadosEstandarMenu.aspx.cs" Inherits="Menu_MGMApartadosEstandarMenu" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMPForm" Runat="Server">
    <link href="../css/styles.css" rel="stylesheet" type="text/css" />   
    <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
    <link href="/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMPForm" Runat="Server">


    <asp:UpdatePanel ID="UpdatePanelMenu" runat="server"  >
    <ContentTemplate>


       <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title="" Modal="true"  RenderMode="Classic" Behaviors="Close" VisibleStatusbar="false"    >
           <Windows></Windows>
       </telerik:RadWindowManager>


        <table style=" width:100%; background-color:transparent; margin-top:1px">
            <tr>
                <td align="center">


                    <asp:Panel ID="pnlMenuApartadoOpciones" runat="server" CssClass="CssPnlMenuApartadoOpciones">

                    <asp:DataList ID="DataListMenu" runat="server" HorizontalAlign="Center" DataKeyField ="maMenuId"  Height="250px"  Width="1250px"   CssClass="CssDataListMenuApartado" >
                    <ItemTemplate> 
                        <div runat="server" id="div_menu" align="center" class="">  
                                             
                            <telerik:RadButton  runat="server"
                                                ID="RadButton1"
                                                NavigateUrl='<%# Eval("maOperEjecUrl") %>' 
                                                OnClientClicked='<%# Eval("maOperEjecFn") %>' 
                                                AutoPostBack="false" 
                                                BackColor="Transparent" 
                                                BorderStyle="None" 
                                                Skin="Default">
                                                <contenttemplate>
                                                    <contenttemplate>
                                                        <table style=" width:300px; background-color:transparent; margin-top:10px">
                                                        <tr>
                                                            <td align="center" style="width:70px">
                                                               <%--<img src='<%# Eval("maMenuIMG") %>' width="70px" height="70px"/>--%>
                                                                <asp:Image ID="Image1" runat="server"  ImageUrl='<%# Eval("maMenuIMG") %>' width="70px" height="70px" />
                                                            </td>
                                                            <td align="center" style=" width:230px; text-align:left ">
                                                                 <telerik:RadLabel ID="radLb_Nombre_menu" runat="server" Text='<%# Eval("maMenuDes") %>' ForeColor="#365C82" CssClass="LblApartadoEstandarMenu" ></telerik:RadLabel>
                                                            </td> 
                                                        </tr>
                                                        </table>   
                                                    </contenttemplate>                                                   
                                                </contenttemplate>
                            </telerik:RadButton>

                        </div> 

                    </ItemTemplate> 
                    </asp:DataList>
                
                    </asp:Panel>
 
                </td>
            </tr>
        </table>    

    </ContentTemplate>
    </asp:UpdatePanel> 



</asp:Content>

