<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MttoAgrEstandar.aspx.cs" Inherits="DC_MttoAgrEstandar" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
    <script src="../Scripts/JSGeneral.js" type="text/javascript"></script>
    <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
    <link href="~/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />


</head>
<body>
    <form id="form1" runat="server">
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
    
        <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
            <telerik:RadImageButton ID="rBtnNuevo"     runat ="server" Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text ="" Visible="false" OnClick="rBtnNuevo_Click"></telerik:RadImageButton>
            <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text="" Visible="false" OnClick="rBtnEliminar_Click"  OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
            <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar" OnClick="rBtnLimpiar_Click"  Text=""></telerik:RadImageButton>    
        </asp:Panel>

        <fieldset >   
            <div style="width:100%; display:table; position:static; background-color:transparent;" align="center" >

                <table border="0" style=" width:640px; text-align:left; background-color:transparent ;">
                    <tr style="height:18px;">
                        <td style=" width:140px; background-color:transparent;">
                            <telerik:RadLabel ID="rLblClave" runat="server" Text=""></telerik:RadLabel>  
                        </td>
                        <td style=" width:540px;  background-color:transparent;">                             
                            <telerik:RadLabel ID="rLblDescripcion" runat="server" Text=""></telerik:RadLabel>    
                        </td>
                    </tr>
                </table>

            </div>
        </fieldset>

        <fieldset  > 
         <legend>Agrupación / Dato</legend>      
            <div style="width:100%; display:table;  position:static; background-color:transparent;" align="center" >
                    <table border="0" style=" width:640px; text-align:left; background-color:transparent ;">
                        <tr style="height:18px;">
                            <td style=" width:60px; background-color:transparent;">
                                 <telerik:RadLabel ID="RadLabel1" runat="server" Text="Agrupación"></telerik:RadLabel> 
                            </td>
                            <td style=" width:180px; background-color:transparent;">
                                <telerik:RadComboBox ID="rCboAgrupaciones" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rCboAgrupaciones_SelectedIndexChanged"
                                                     HighlightTemplatedItems="true"
                                                     DropDownCssClass="cssRadComboBox"  
                                                     Width="175px" DropDownWidth="330px" Height="200px" 
                                                     Enabled="false">
                                    <HeaderTemplate>
                                        <table style="width: 300px" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="width: 90px;">
                                                    Clave
                                                </td>
                                                <td style="width: 210px;">
                                                    Descripción
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table style="width: 300px;"  cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="width:90px">
                                                    <%# DataBinder.Eval(Container.DataItem, "agrCve")%>
                                                </td>
                                                <td style="width: 210px;">
                                                    <%# DataBinder.Eval(Container.DataItem, "agrDes") %>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                    </FooterTemplate>
                                 </telerik:RadComboBox>
                            </td>
                            <td style=" width:120px; background-color:transparent;">
                                 <telerik:RadLabel ID="RadLabel2" runat="server" Text="Dato Agrupación:"></telerik:RadLabel> 
                            </td>
                            <td style=" width:180px; background-color:transparent;">
                                 <telerik:RadComboBox ID="rCboAgrupacionesDatos" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rCboAgrupacionesDatos_SelectedIndexChanged"
                                                      HighlightTemplatedItems="true"
                                                      DropDownCssClass="cssRadComboBox"  
                                                      Width="175px" DropDownWidth="330px" Height="200px" 
                                                      Enabled="false">
                                    <HeaderTemplate>
                                        <table style="width: 300px" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="width: 90px;">
                                                    Clave
                                                </td>
                                                <td style="width: 210px;">
                                                    Descripción
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table style="width: 300px;"  cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="width:90px">
                                                    <%# DataBinder.Eval(Container.DataItem, "agrDatoCve")%>
                                                </td>
                                                <td style="width: 210px;">
                                                    <%# DataBinder.Eval(Container.DataItem, "agrDatoDes") %>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                    </FooterTemplate>
                                 </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
            </div>
        </fieldset>

         
         <div style="width:100%; margin-top:5px; display:table; position:static; background-color:transparent;" align="center" > 
  
          <telerik:RadGrid ID="rGdvInformacion"
              runat="server" 
              AutoGenerateColumns="False" 
              Width="660px" Height="315px"
              CssClass="Grid" 
              Skin="Office2010Silver"  
                >
                <MasterTableView DataKeyNames="agrDatoCve" AutoGenerateColumns="False" CssClass="GridTable"  >
                    
                    <Columns>
                        <telerik:GridBoundColumn HeaderText="Clave Agrupación"        DataField="agrCve" HeaderStyle-Width="130px"      ItemStyle-Width="130px" />
                        <telerik:GridBoundColumn HeaderText="Descripción Agrupación"  DataField="agrDes" HeaderStyle-Width="190px"      ItemStyle-Width="190px" />
                        <telerik:GridBoundColumn HeaderText="Clave Dato"              DataField="agrDatoCve" HeaderStyle-Width="130px"  ItemStyle-Width="130px" />  
                        <telerik:GridBoundColumn HeaderText="Descripción Dato"        DataField="agrDatoDes" HeaderStyle-Width="190px"  ItemStyle-Width="190px" />                                           
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
                        <Scrolling AllowScroll="true" UseStaticHeaders="true"    ScrollHeight="315px"     />
                    </ClientSettings>
            </telerik:RadGrid>
        </div>            
    

        <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
            <telerik:RadImageButton ID="rBtnGuardar"   runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text ="" OnClick="rBtnGuardar_Click"  OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
            <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text="" OnClick="rBtnCancelar_Click"  OnClientClicking="OnClientClic_ConfirmCancel"></telerik:RadImageButton>
        </asp:Panel>

        <asp:HiddenField ID="hdfBtnAccion" runat="server" />

    </div>
    </ContentTemplate>
    </asp:UpdatePanel>  

    <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
    </telerik:RadWindowManager>


    </form>
</body>
</html>
