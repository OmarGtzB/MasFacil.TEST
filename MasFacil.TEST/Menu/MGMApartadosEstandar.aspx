<%@ Page Title="" Language="C#" MasterPageFile="~/MPForm.master" AutoEventWireup="true" CodeFile="MGMApartadosEstandar.aspx.cs" Inherits="Menu_MGMApartadosEstandar" %>

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

       <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title="" Modal="true"  RenderMode="Classic"
           Behaviors="Close" VisibleStatusbar="false"  >
           <Windows></Windows>
       </telerik:RadWindowManager>


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
                                                               <%--<img src='<%# Eval("maMenuIMG") %>' width="70px" height="70px"/>--%>
                                                                <asp:Image ID="Image1" runat="server"  ImageUrl='<%# Eval("maMenuIMG") %>' width="70px" height="70px" />
                                                            </td>
                                                            <td align="center" style=" width:230px;    text-align:left ">
                                                                 <telerik:RadLabel ID="radLb_Nombre_menu" runat="server" Text='<%# Eval("maMenuDes") %>'  ForeColor="#365C82" CssClass="LblApartadoEstandarMenu"></telerik:RadLabel>
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


     <telerik:RadWindow runat="server" ID="FNSeleccionRegistroApartadoEstandar" Width="600px" Height="580px" Modal="true" VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close" >
        <ContentTemplate>
            <asp:UpdatePanel ID="Updatepanel1" runat="server" UpdateMode="Always">
                <ContentTemplate>

                <div align="right" style=" background-color:transparent; position:relative; width:100%;  text-align:right;  " >

                    <asp:Panel ID="pnlBtnsAcciones" Width="100%"   CssClass="cspnlBtnsAcciones"  runat="server">

                         <table id="tbBotones" runat="server"  border="0" style=" width:150px; margin-left:5px;  text-align:left; background-color:transparent ;">
                            <tr style="height:15px;">
                                <td id="dtrBtnNuevo" runat="server" style=" width:55px; background-color:transparent;"> 
                                    <telerik:RadImageButton ID="rBtnNuevo" runat ="server" Image-Sizing="Original" Width="30px" Height="30px" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text ="" Visible="false" OnClick="rBtnNuevo_Click"  ></telerik:RadImageButton> 
                                </td>
                                <td id="dtrBtnModificar" runat="server" style=" width:55px; background-color:transparent;">   
                                    <telerik:RadImageButton ID="rBtnModificar" runat="server" Image-Sizing="Original"  Width="30px" Height="30px" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  ToolTip="Modificar"  Text="" Visible="false" OnClick="rBtnModificar_Click" ></telerik:RadImageButton>
                                </td>
                                <td id="dtrBtnEliminar" runat="server"  style=" width:55px; background-color:transparent;">       
                                     <telerik:RadImageButton ID="rBtnEliminar"  runat="server" Image-Sizing="Original" Width="30px" Height="30px"    Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text="" Visible="false" OnClick="rBtnEliminar_Click" OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
                                </td>
                                <td style=" width:55px; background-color:transparent;">   
                                    <telerik:RadImageButton ID="rBtnLimpiar"   runat="server" Image-Sizing="Original"  Width="30px" Height="30px" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"  Text="" OnClick="rBtnLimpiar_Click"></telerik:RadImageButton>
                                </td>
                            </tr>
                        </table>  

                    </asp:Panel>     

               </div>
 
                    
            <fieldset >  
                <legend>Busqueda</legend> 
                    <div style="width:99%; display:table; position:static; background-color:transparent;" align="left" >

                        <table border="0" style=" width:510px; text-align:left; background-color:transparent ;">
                            <tr style="height:18px;">
                                <td style=" width:310px; background-color:transparent;">
                                    <telerik:RadTextBox ID="rTxtBusqueda" runat="server"  Width="300px"
                                                        EnabledStyle-CssClass="cssTxtEnabled"
                                                        DisabledStyle-CssClass ="cssTxtEnabled"
                                                        HoveredStyle-CssClass="cssTxtHovered"
                                                        FocusedStyle-CssClass="cssTxtFocused"
                                                        InvalidStyle-CssClass="cssTxtInvalid" 
                                                     ></telerik:RadTextBox>
                                </td>
                                <td style=" width:200px; background-color:transparent;">
                                    <telerik:RadImageButton ID="rBtnBuscar"  runat="server"   OnClick="rBtnBuscar_Click" Image-Sizing="Stretch" Text="Buscar"></telerik:RadImageButton>
                                </td>

                            </tr>
                        </table>           

                    </div>
                </fieldset>


            <fieldset >   
                    <div style="width:100%; display:table; position:static; background-color:transparent;" align="center" >

                        <table border="0" style=" width:100%; text-align:left; background-color:transparent ;">
                            <tr style="height:18px;">
                                <td style=" width:100%; background-color:transparent;">


     

                                <div>
                                    <telerik:RadGrid ID="rGdvRegistros"  
                                                     runat="server"  
                                                     AutoGenerateColumns ="false"
                                                     OnSelectedIndexChanged="rGdvRegistros_SelectedIndexChanged"
                                                     Width="560px"   Height="395px" 
                                                     CssClass="Grid" 
                                                     skin="Office2010Silver" 
                                                     >

                                                    <MasterTableView AutoGenerateColumns="False" DataKeyNames="Cve" CssClass="GridTable" >
                                                        <NoRecordsTemplate>
                                                            No se encontraron registros.
                                                        </NoRecordsTemplate>
                                                        <Columns>
                                                            <telerik:GridBoundColumn DataField="Cve" HeaderStyle-Width="155px" HeaderText="Clave" ItemStyle-Width="155px" Display="false" />
                                                            <telerik:GridBoundColumn DataField="CveFormat" HeaderStyle-Width="155px" HeaderText="Clave" ItemStyle-Width="155px" />
                                                            <telerik:GridBoundColumn DataField="Descripcion" HeaderStyle-Width="325px" HeaderText="Descripción" ItemStyle-Width="325px" />

                                                        </Columns>
                                                    </MasterTableView>
                                                    <HeaderStyle CssClass="GridHeaderStyle"/>
                                                    <ItemStyle CssClass="GridRowStyle"/>
                                                    <AlternatingItemStyle  CssClass="GridAlternatingRowStyle" />
                                                    <selecteditemstyle CssClass="GridSelectedItem"/>
                                                    <FooterStyle CssClass="GridFooterStyle" />

                                                    <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true" >
                                                        <Selecting AllowRowSelect="true" EnableDragToSelectRows="False" ></Selecting>
                                                        <Scrolling AllowScroll="true" UseStaticHeaders="true"    ScrollHeight="280px"     />
                                                    </ClientSettings>

                                    </telerik:RadGrid>
                                </div> 
                       

                                </td>
                            </tr>
                        </table>           

                    </div>
                </fieldset>

            <%--<asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server" Visible="false">
            </asp:Panel>--%>
                  
            



            <asp:HiddenField ID="hdfBtnAccion" runat="server" />


           </ContentTemplate>
            </asp:UpdatePanel>
     

        </ContentTemplate>
    </telerik:RadWindow>



</asp:Content>

