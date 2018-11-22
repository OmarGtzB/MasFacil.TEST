<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MttoCptoAutoridadUsuarios.aspx.cs" Inherits="DC_MttoCptoAutoridadUsuarios" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
     <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
    <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
     <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
    <script src="../Scripts/JSGeneral.js" type="text/javascript"></script>
     <link href="/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />
    </head>

<body>
    <form id="form2" runat="server">

        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
            </Scripts>
        </telerik:RadScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
            <telerik:RadImageButton ID="rBtnNuevo"     runat ="server" Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text ="" OnClick="rBtnNuevo_Click" Visible="false"  ></telerik:RadImageButton>
            <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  OnClick="rBtnModificar_Click" ToolTip="Modificar"  Text="" Visible="false"  ></telerik:RadImageButton>
            <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text="" OnClick="rBtnEliminar_Click" OnClientClicking="OnClientClic_ConfirmOK" Visible="false"  ></telerik:RadImageButton> 
            <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar" OnClick="rBtnLimpiar_Click"  Text=""></telerik:RadImageButton>    
        </asp:Panel>

        <div style="height:10px;">

        </div>


        <fieldset style="  margin-top:5px;   display: block; text-align:left;" >   
            
            <div style="width:100%; display:table; position:static; background-color:transparent;" >

                <table border="0" style=" text-align:left; background-color:transparent;" >
                    <tr style="height:18px;">
                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel6" runat="server" Text="Concepto" width="120px"></telerik:RadLabel>
                            <telerik:RadLabel ID="rLblCptoId" runat="server" Text=""></telerik:RadLabel>
                             <telerik:RadLabel ID="rLblcptoDes" runat="server" Text=""></telerik:RadLabel>
                        </td>
                   

                    </tr>
        
                </table>
            </div>
        </fieldset>




        <fieldset style="  margin-top:5px;   display: block; text-align:left;" > 
            <legend>Valores</legend>  
            <div style=" width:100%; display:table; background-color:transparent;" class="auto-style3" >

                <table border="0" style=" width:100%; text-align:left; background-color:transparent;" >
                    <tr >
                        <td style=" width:10%; background-color:transparent;"> 
                            <telerik:RadLabel ID="RadLabel11" runat="server" Text="Usuario" width="50px"></telerik:RadLabel>
                        </td>
                        <td style=" width:30%; background-color:transparent;">
                            <telerik:RadComboBox ID="rCboTipoDato" width="220px" runat="server" HighlightTemplatedItems="true" DropDownWidth="350px" Height="200px"
                                                     DropDownCssClass="cssRadComboBox" AutoPostBack="True" >            
                                  <HeaderTemplate>
                                        <table style="width: 350px" cellspacing="0" cellpadding="0">
                                                <tr>
                                                     <td style="width: 110px;">
                                                        Clave
                                                    </td>
                                                    <td style="width: 240px;">
                                                        Nombre
                                                    </td>
                                                </tr>
                                            </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                            <table style="width: 350px;"  cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 110px;">
                                                   <%# DataBinder.Eval(Container.DataItem, "maUsuCve") %>
                                                    </td>
                                                     <td style="width: 240px;">
                                                     <%# DataBinder.Eval(Container.DataItem, "maUsuNom") %>
                                                    </td>
                                                </tr>
                                            </table>

                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                    </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td style=" width:20%; background-color:transparent;">
                            <asp:RadioButton ID="rBtnConsultar" runat="server" Checked="true" GroupName="isEmployee" Text="Consultar" />
                        </td>
                                   <td style=" width:20%; background-color:transparent;">
                                             <asp:RadioButton ID="rBtnRegistrar" runat="server" GroupName="isEmployee" Text="Registrar" />
                                            </td> 
                                   <td style=" width:20%; background-color:transparent;">
                                       <asp:RadioButton ID="rBtnGenerar" runat="server" GroupName="isEmployee" Text="Generar" />
                                        </td>
                    </tr>
                </table>

                   <telerik:RadTextBox ID="rTxtsegCptoId" runat="server"  Width="300px"
                                                        EnabledStyle-CssClass="cssTxtEnabled"
                                                        DisabledStyle-CssClass ="cssTxtEnabled"
                                                        HoveredStyle-CssClass="cssTxtHovered"
                                                        FocusedStyle-CssClass="cssTxtFocused"
                                                        InvalidStyle-CssClass="cssTxtInvalid"
                                                        Visible="false" 
                                                     ></telerik:RadTextBox>
            </div>
        </fieldset>



             <div style="width:100%; margin-top:5px; display:table; position:static; background-color:transparent;" align="center" >  
 
                  
                    <telerik:RadGrid ID="rGdvInformacion" 
                        runat="server"
                        AutoGenerateColumns ="false" 
                    OnSelectedIndexChanged="rGdvInformacion_SelectedIndexChanged"
                        Width="715px" Height="300px" 
                        CssClass="Grid" 
                        skin="Office2010Silver"  
                          >

                        <MasterTableView  DataKeyNames="cptoId" 
                                         AutoGenerateColumns="False"  
                                          CssClass="GridTable" >

                         <Columns >
 
                              <telerik:GridBoundColumn DataField="maUsuCve"  HeaderText="Usuario"  HeaderStyle-Width="230px" ItemStyle-Width="230px"  />
                           <telerik:GridBoundColumn DataField="maUsuNo"  HeaderText="Nombre" HeaderStyle-Width="230px" ItemStyle-Width="230px"    ItemStyle-HorizontalAlign="Right"/>
                            <telerik:GridBoundColumn DataField="segCptoAut"    HeaderText="Se." HeaderStyle-Width="150px"  ItemStyle-Width="150px"   ItemStyle-HorizontalAlign="Right" Display="false" />
                              <telerik:GridBoundColumn DataField="segCptoAut1"    HeaderText="Autoridad" HeaderStyle-Width="150px"  ItemStyle-Width="150px"   ItemStyle-HorizontalAlign="Right" />


                           <telerik:GridBoundColumn DataField="segCptoId"    HeaderText="CptoId." HeaderStyle-Width="150px"  ItemStyle-Width="150px"   ItemStyle-HorizontalAlign="Right" Display="false" />
                               </Columns>
                        <NoRecordsTemplate>No se encontraron registros.</NoRecordsTemplate>    
                </MasterTableView>

              
                    <HeaderStyle CssClass="GridHeaderStyle"/>
                    <ItemStyle CssClass="GridRowStyle"/>
                    <AlternatingItemStyle  CssClass="GridAlternatingRowStyle" />
                    <selecteditemstyle CssClass="GridSelectedItem"/>
                    <FooterStyle CssClass="GridFooterStyle" />

                    <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true" >
                        <Selecting AllowRowSelect="true" EnableDragToSelectRows="False" ></Selecting>
                        <Scrolling AllowScroll="true" UseStaticHeaders="true"    ScrollHeight="225px"     />
                    </ClientSettings>

                </telerik:RadGrid> 
                 
                  
         </div> 


        <div style="width:100%; display:table; position:static; background-color:transparent;">
            <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
                <telerik:RadImageButton ID="rBtnGuardar"  runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text ="" OnClick="rBtnGuardar_Click" OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text="" OnClick="rBtnCancelar_Click" OnClientClicking="OnClientClic_ConfirmCancel" ></telerik:RadImageButton>
            </asp:Panel>
        </div>

        <asp:HiddenField ID="hdfBtnAccion" runat="server" />
        <asp:HiddenField ID="hdfSecuencia" runat="server" />
            </div>

    </ContentTemplate>
    </asp:UpdatePanel>

        <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
        </telerik:RadWindowManager>

    </form>
</body>
</html>
