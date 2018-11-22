﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MttoMonedas.aspx.cs" Inherits="DC_MttoMonedas" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
     <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
       <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
     <link href="~/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />
        <style type="text/css">
            .auto-style1 {
                width: 777px;
            }
            .auto-style2 {
                width: 638px;
            }
        </style>
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
    <div>

    <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server" Height="34px">
        <telerik:RadImageButton ID="rBtnNuevo"     runat ="server" Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text ="" Visible="false" OnClick="rBtnNuevo_Click"></telerik:RadImageButton>
        <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  ToolTip="Modificar"  Text="" Visible="false" OnClick="rBtnModificar_Click"></telerik:RadImageButton>
        <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text="" Visible="false" OnClick="rBtnEliminar_Click" OnClientClicking="OnClientClic_ConfirmOK" ></telerik:RadImageButton>
        <telerik:RadImageButton ID="rBtnTipoCambio" runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnTipoCambioMonedaDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnTipoCambioMoneda.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnTipoCambioMonedaHovered.png"  ToolTip="Tipo de Cambio"  Text="" Visible="false" OnClick="rBtnMonedaCve_Click"></telerik:RadImageButton>   
        <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar" OnClick="rBtnLimpiar_Click"  Text=""></telerik:RadImageButton>      
    </asp:Panel>


            <fieldset  >       

           <div style="width:100%; display:table; position:static; background-color:transparent;" align="center" >

                    <table border="0" style=" width:640px; text-align:left; background-color:transparent ;">
                        <tr style="height:18px;">
                            <td style=" width:100px; background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel1" runat="server" Text="Clave"></telerik:RadLabel>  
                            </td>
                                <td style=" width:220px;  background-color:transparent;">                             
                                <telerik:RadTextBox ID="rtxtCve" runat="server" Width="200px"
                                                            EnabledStyle-CssClass="cssTxtEnabled"
                                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                                            HoveredStyle-CssClass="cssTxtHovered"
                                                            FocusedStyle-CssClass="cssTxtFocused"
                                                            InvalidStyle-CssClass="cssTxtInvalid"       
                                                            MaxLength="2"   Enabled="false"           
                                ></telerik:RadTextBox> 
                            </td>

                            <td style=" width:100px; background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel3" runat="server" Text="Descripción"></telerik:RadLabel>  
                            </td>
                            <td style=" width:220px;  background-color:transparent;">                             
                                <telerik:RadTextBox ID="rTxtDes" runat="server" Width="200px"
                                                            EnabledStyle-CssClass="cssTxtEnabled"
                                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                                            HoveredStyle-CssClass="cssTxtHovered"
                                                            FocusedStyle-CssClass="cssTxtFocused"
                                                            InvalidStyle-CssClass="cssTxtInvalid"  Enabled="false"                                   
                                ></telerik:RadTextBox> 
                            </td>
                        </tr>
                        <tr style="height:18px;">
                            <td style=" width:100px; background-color:transparent;">
                                   <telerik:RadLabel ID="RadLabel2" runat="server" Text="Abreviatura"></telerik:RadLabel>  
                            </td>
                            <td style=" width:220px;  background-color:transparent;">                             
                                <telerik:RadTextBox ID="rTxtAbr" runat="server" Width="200px" 
                                                            EnabledStyle-CssClass="cssTxtEnabled"
                                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                                            HoveredStyle-CssClass="cssTxtHovered"
                                                            FocusedStyle-CssClass="cssTxtFocused" Enabled="false"
                                                            InvalidStyle-CssClass="cssTxtInvalid"  
                                 ></telerik:RadTextBox> 
                            </td>
                            <td  style=" width:100px; background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel5" runat="server" Text="Siglas"></telerik:RadLabel>  
                            </td>      
                            <td  style=" width:100px; background-color:transparent;">    
                                <telerik:RadTextBox ID="rTxtSiglas" runat="server" Width="68px"  
                                                            EnabledStyle-CssClass="cssTxtEnabled"
                                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                                            HoveredStyle-CssClass="cssTxtHovered" Enabled="false"
                                                            FocusedStyle-CssClass="cssTxtFocused"
                                                            InvalidStyle-CssClass="cssTxtInvalid" 
                                                            Maxlength="3">
                                </telerik:RadTextBox>
                                <telerik:RadLabel ID="RadLabel6" runat="server" Text="" Width="50px"></telerik:RadLabel> 
                                 
                                 <telerik:RadLabel ID="RadLabel4" runat="server" Text="Signo"></telerik:RadLabel>  
                                                     
                                <telerik:RadTextBox ID="rTxtSigno" runat="server" Width="23px"  
                                                            EnabledStyle-CssClass="cssTxtEnabled"
                                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                                            HoveredStyle-CssClass="cssTxtHovered" Enabled="false"
                                                            FocusedStyle-CssClass="cssTxtFocused"
                                                            InvalidStyle-CssClass="cssTxtInvalid" 
                                                            Maxlength="1"
                                ></telerik:RadTextBox>
                            </td>
                        </tr>
                    </table>

                    <table id="sat" runat ="server" visible="false" border="0" style=" width:640px; text-align:left; background-color:transparent ;">
                        <tr style="height:18px;">
                           <td style=" width:100px; background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel7" runat="server" Text="CFDI"></telerik:RadLabel>   
                           </td>
                           <td style=" width:220px;  background-color:transparent;">                             
                                 <telerik:RadComboBox ID="rCboSatMoneda" runat="server" Width="200px"   AllowCustomText="true"
                                                HighlightTemplatedItems="true" AutoPostBack="true"
                                              DropDownCssClass="cssRadComboBox" 
                                              DropDownWidth="260px" 
                                               Height="200px"  >
                                                    <HeaderTemplate>
                                                        <table style="width: 150px" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td style="width: 80px;">
                                                                        Clave
                                                                    </td>
                                                                    <td style="width: 70px;">
                                                                        Descripción
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table style="width: 250px;"  cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width:80px" >
                                                                    <%# DataBinder.Eval(Container.DataItem, "satMonCve")%>
                                                                </td>
                                                                <td style="width: 170px;">
                                                                    <%# DataBinder.Eval(Container.DataItem, "satMonDes") %>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                    </FooterTemplate>
                                            </telerik:RadComboBox>
                            </td>

                            <td style=" width:100px; background-color:transparent;">
   
                            </td>
                            <td style=" width:220px;  background-color:transparent;">                             
 
                            </td>
                        </tr>
                    </table>

                </div>

            </fieldset>


              <div style="width:100%; display:table; margin-top:5px; position:static; background-color:transparent;" align="center">
       <%--   <div style="width:100%; margin-top:5px; display:table; position:static; background-color:transparent;" align="center" > --%> 
 
                  
                    <telerik:RadGrid ID="rGdvInformacion" 
                        runat="server"
                        OnSelectedIndexChanged="rGdvInformacion_SelectedIndexChanged" 
                       
                           AllowMultiRowSelection="true"
                           AutoGenerateColumns="False" 
                           Width="640px" Height="350px"   
                           CssClass="Grid" 
                           Skin="Office2010Silver"
                       
                          >

                        <MasterTableView DataKeyNames="monCve"  
                                         AutoGenerateColumns="False"  
                                          CssClass="GridTable" >

                         <Columns>
                           
                                <telerik:GridBoundColumn DataField="monCve"  HeaderText="Clave"  HeaderStyle-Width="100px" ItemStyle-Width="100px" />
                                <telerik:GridBoundColumn DataField="monDes" HeaderText="Descripción"  HeaderStyle-Width="200px"  ItemStyle-Width="200px" />
                                <telerik:GridBoundColumn DataField="monAbr"  HeaderText="Abreviatura" HeaderStyle-Width="100px" ItemStyle-Width="100px" />
                                <telerik:GridBoundColumn DataField="monSigl" HeaderText="Siglas" HeaderStyle-Width="60px"  ItemStyle-Width="60px"/>
                                <telerik:GridBoundColumn DataField="monSig" HeaderText="Signo" HeaderStyle-Width="50px"  ItemStyle-Width="50px"/>
                                <telerik:GridBoundColumn DataField="satMonCve" HeaderText="CFDI" HeaderStyle-Width="50px"  ItemStyle-Width="50px" Display="false"/>

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

        <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
            <telerik:RadImageButton ID="rBtnGuardar"   runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text ="" OnClick="rBtnGuardar_Click"  OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
            <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text="" OnClick="rBtnCancelar_Click"  OnClientClicking="OnClientClic_ConfirmCancel"></telerik:RadImageButton>
        </asp:Panel>

        <asp:HiddenField ID="hdfRawUrl" runat="server" />
        <asp:HiddenField ID="hdfBtnAccion" runat="server" />
        </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>  

    <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
    </telerik:RadWindowManager>

    </form>
</body>
</html>