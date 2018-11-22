<%@ Page Title="" Language="C#" MasterPageFile="~/MPForm.master" AutoEventWireup="true" CodeFile="MGMApartados.aspx.cs" Inherits="Menu_MGMApartados" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMPForm" Runat="Server">

      <link href="../css/styles.css" rel="stylesheet" type="text/css" />   
     <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMPForm" Runat="Server">



        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>



            <table style=" width:100%; background-color:transparent; margin-top:1px">
                <tr>
                    <td align="center">

                    <asp:Panel ID="Panel1" runat="server" CssClass="CssPnlMenuApartadoSelecccion">

                    <table style=" width:1250px; background-color:transparent;">
                            <tr>
                                <td style=" width:1000px; text-align:left;  ">
                                    <asp:Label ID="lblSeleccion" runat="server" Text=""></asp:Label>
                                </td>
                                <td  style=" width:250px; text-align:right;  ">
                                        <telerik:RadImageButton ID="rBtnSeleccion"  runat="server"  AutoPostBack="true" Width="95px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnSeleccionDisabledArt.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnSeleccionArt.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnSeleccionHoveredArt.png"  ToolTip="Seleccionar" OnClick="rBtnSeleccion_Click"     ></telerik:RadImageButton>                      
                                </td>
                            </tr>
                    </table>  

                    </asp:Panel>


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
                                                               <img src='<%# Eval("maMenuIMG") %>' width="80" height="80"/>
                                                            </td>
                                                            <td align="center" style=" width:230px; text-align:left ">
                                                                 <telerik:RadLabel ID="radLb_Nombre_menu" runat="server" Text='<%# Eval("maMenuDes") %>'  ForeColor="#ffffff" CssClass="LblApartadoEstandarMenu" ></telerik:RadLabel>
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


      <%--RenderMode="Lightweight"   
          CssClass='<%# Eval("maOperCSS") %>'--%>

    <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
    
             <Windows>
            <telerik:RadWindow  ID="FNRWSeleccionArticulo" Width="500px" ReloadOnShow="false"  runat="server" Title="ARTICULOS"  >
                <ContentTemplate>

                    
                                           <div  align="right" style=" position:relative; width:520px;  text-align:right;  " >
        
                                <telerik:RadButton ID="rbtnNuevoArt" runat="server" Text="Nuevo" OnClick="rbtnNuevoArt_Click" > <Icon PrimaryIconCssClass="rbSave"></Icon></telerik:RadButton>
                                  
                              </div>
                 <asp:Panel ID="pnlArticulos" runat="server">


                    <div>
                        <telerik:RadGrid ID="rGdvArticulos"  
                                         runat="server"  
                                         ClientSettings-Scrolling-AllowScroll ="true" 
                                         Height="235px" 
                                         GridViewEditEventArgs="True" 
                                         skin="Office2010Silver" 
                                         Width="530px" 
                                        OnSelectedIndexChanged="rGdvArticulos_SelectedIndexChanged">
                                        <MasterTableView AutoGenerateColumns="False" DataKeyNames="artCve" GroupsDefaultExpanded="true" Width="100%" TableLayout="Fixed">
                                            <NoRecordsTemplate>
                                                No se encontraron registros.
                                            </NoRecordsTemplate>
                                            <Columns>
                 
                                                <telerik:GridBoundColumn DataField="artCve" HeaderStyle-Width="100px" HeaderText="Clave" ItemStyle-Width="100px" />
                                                <telerik:GridBoundColumn DataField="artDes" HeaderStyle-Width="430px" HeaderText="Descripción" ItemStyle-Width="430px" />

                                            </Columns>
                                        </MasterTableView>
                                        <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                        <ClientSettings EnablePostBackOnRowClick="true" Selecting-AllowRowSelect="true">
                                            <Selecting AllowRowSelect="True" EnableDragToSelectRows="False" />
                                            <Scrolling UseStaticHeaders="true" />
                                            <Animation AllowColumnReorderAnimation="True" />
                                        </ClientSettings>
                                        <FilterMenu RenderMode="Lightweight">
                                        </FilterMenu>
                                        <HeaderContextMenu RenderMode="Lightweight">
                                        </HeaderContextMenu>

                        </telerik:RadGrid>
                    </div> 
                </asp:Panel>
                 

                </ContentTemplate>
                
            </telerik:RadWindow>
        </Windows>

    
    
    </telerik:RadWindowManager> 



                  
    
         </ContentTemplate>
        </asp:UpdatePanel> 

</asp:Content>

