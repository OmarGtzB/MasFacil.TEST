<%@ Page Title="" Language="C#" MasterPageFile="~/MPForm.master" AutoEventWireup="true" CodeFile="MGMApartadosEstandar2.aspx.cs" Inherits="Menu_MGMApartadosEstandar" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMPForm" Runat="Server">
     <link href="../css/styles.css" rel="stylesheet" type="text/css" />   
     <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
     <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
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
                                        <telerik:RadImageButton ID="rBtnSeleccion"  runat="server"  AutoPostBack="true" Width="95px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnSeleccionDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnSeleccion.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnSeleccionHovered.png"  ToolTip="Seleccionar" OnClick="rBtnSeleccion_Click" ></telerik:RadImageButton>                      
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
                                                               <img src='<%# Eval("maMenuIMG") %>' width="50" height="50"/>
                                                            </td>
                                                            <td align="center" style=" width:230px; text-align:left ">
                                                                 <telerik:RadLabel ID="radLb_Nombre_menu" runat="server" Text='<%# Eval("maMenuDes") %>'  ForeColor="#ffffff" ></telerik:RadLabel>
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
        
        

        <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  RenderMode="Classic" Behaviors="Close" VisibleStatusbar="false"     >
        <Windows>
            <telerik:RadWindow  ID="FNRWSeleccionRegistroEstandar"   Width="800px" ReloadOnShow="false" Modal="true"  
                 Style="z-index: 1000" runat="server" Title=""  >
                <ContentTemplate>
                
                <div align="right" style=" background-color:transparent; position:relative; width:100%;  text-align:right;  " >


                 <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
                    <telerik:RadImageButton ID="rBtnNuevo"    AutoPostBack="false"  runat ="server" Width="20px" Height="20px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text ="" OnClick="rBtnNuevo_Click" ></telerik:RadImageButton>
                    <telerik:RadImageButton ID="rBtnEliminar" AutoPostBack="false"  runat="server"  Width="19px" Height="22px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text="" OnClick="rBtnEliminar_Click" ></telerik:RadImageButton>
                    <telerik:RadImageButton ID="rBtnLimpiar"  AutoPostBack="false" runat="server"  Width="19px" Height="22px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"  Text="" OnClick="rBtnLimpiar_Click"></telerik:RadImageButton>
                </asp:Panel>     

<fieldset style="  margin-top:10px;   display: block; text-align:left;" >   
        <div style="width:100%; display:table; position:static; background-color:transparent;" align="center" >

            <table border="0" style=" width:100%; text-align:left; background-color:transparent ;">
                <tr style="height:18px;">
                    <td style=" width:100%; background-color:transparent;">


 <asp:Panel ID="pnlSeleccionRegistro" runat="server">


                    <div>
                        <telerik:RadGrid ID="rGdvRegistros"  
                                         runat="server"  
                                         ClientSettings-Scrolling-AllowScroll ="true" 
                                         Height="360px" 
                                         GridViewEditEventArgs="True" 
                                         skin="Office2010Silver" 
                                         Width="610px" 
                                        OnSelectedIndexChanged="rGdvRegistros_SelectedIndexChanged" >
                                        <MasterTableView AutoGenerateColumns="False" DataKeyNames="Cve" GroupsDefaultExpanded="true" Width="100%" TableLayout="Fixed">
                                            <NoRecordsTemplate>
                                                No se encontraron registros.
                                            </NoRecordsTemplate>
                                            <Columns>
                 
                                                <telerik:GridBoundColumn DataField="Cve" HeaderStyle-Width="100px" HeaderText="Clave" ItemStyle-Width="100px" />
                                                <telerik:GridBoundColumn DataField="Descripcion" HeaderStyle-Width="430px" HeaderText="Descripción" ItemStyle-Width="430px" />

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
                       

                    </td>
                </tr>
            </table>           

        </div>
    </fieldset>


    <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
        <telerik:RadImageButton ID="rBtnGuardar"  runat ="server" Width="60px" Height="25px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text ="" OnClick="rBtnGuardar_Click" OnClientClicking="OnClientClic_ConfirmOK2"></telerik:RadImageButton>
        <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="60px" Height="25px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text="" OnClick="rBtnCancelar_Click" OnClientClicking="OnClientClic_ConfirmCancel"></telerik:RadImageButton>
    </asp:Panel>


    </div>

    




                    
    

                </ContentTemplate>
                
            </telerik:RadWindow>

        </Windows>      
        </telerik:RadWindowManager> 
     
                           
    </ContentTemplate>
    </asp:UpdatePanel> 

</asp:Content>

