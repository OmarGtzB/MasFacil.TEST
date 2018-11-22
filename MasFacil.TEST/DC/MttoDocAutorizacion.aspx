<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MttoDocAutorizacion.aspx.cs" Inherits="DC_MttoDocAutorizacion" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
     <link href="/css/cssControles.css" rel="stylesheet" type="text/css" />
     <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
     <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
     <script src="../Scripts/JSGeneral.js" type="text/javascript"></script>
    <link href="~/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />
    <style type="text/css">

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

    <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
        <telerik:RadImageButton ID="rBtnNuevo"     runat ="server" Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text ="" OnClick="rBtnNuevo_Click" Visible="false" ></telerik:RadImageButton>
        <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  ToolTip="Modificar"  Text="" OnClick="rBtnModificar_Click" Visible="false"  ></telerik:RadImageButton>
        <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text="" OnClick="rBtnEliminar_Click" Visible="false" ></telerik:RadImageButton>
        <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar" OnClick="rBtnLimpiar_Click"  Text=""></telerik:RadImageButton>    
    </asp:Panel>

            <fieldset>   
 

        <div style="width:100%; display:table; position:static; background-color:transparent;" align="center" >

            <table border="0" style="  text-align:left; background-color:transparent;" >
                <tr style="height:18px;">
                    <td style=" width:40px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel2" runat="server" Text="Documento"></telerik:RadLabel>  
                    </td>
                    <td style=" width:300px;  background-color:transparent;">                             
<telerik:RadComboBox ID="rCboDocumentos" runat="server"  Enabled="true"
                                AutoPostBack="true"
                                 HighlightTemplatedItems="true"
                                 DropDownCssClass="cssRadComboBox"  
                                 Width="240px" DropDownWidth="300px" Height="200px"
                                   
                                    >
                                    <HeaderTemplate>
                                        <table style="width: 250px" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="width: 80px;">
                                                    Clave
                                                </td>
                                                <td style="width: 170px;">
                                                    Descripción
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table style="width: 250px;"  cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="width:80px" >
                                                    <%# DataBinder.Eval(Container.DataItem, "docCve")%>
                                                </td>
                                                <td style="width: 170px;">
                                                    <%# DataBinder.Eval(Container.DataItem, "docDes") %>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                    </td>
                    <td style=" width:40px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel1" runat="server" Text="Usuario"></telerik:RadLabel>  
                    </td>
                    <td style=" width:250px;  background-color:transparent;">                             
                                <telerik:RadComboBox ID="rCboUsuarios" runat="server"  Enabled="true"
                                AutoPostBack="true"
                                 HighlightTemplatedItems="true"
                                 DropDownCssClass="cssRadComboBox"  
                                 Width="240px" DropDownWidth="300px" Height="200px" 
                                   
                                    >
                                    <HeaderTemplate>
                                        <table style="width: 250px" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="width: 90px;">
                                                    Clave
                                                </td>
                                                <td style="width: 160px;">
                                                    Descripción
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table style="width: 250px;"  cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="width:90px" >
                                                    <%# DataBinder.Eval(Container.DataItem, "maUsuCve")%>
                                                </td>
                                                <td style="width: 160px;">
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
                </tr>
            </table>           
            
        </div>
    </fieldset>

    <fieldset>   
 <legend>Valores</legend>
        <div style="width:100%; display:table; position:static; background-color:transparent;" align="center" >

            <table border="0" style=" width:700px; text-align:left; background-color:transparent ;">
                <tr style="height:18px;">

                    <td style=" width:135px; background-color:transparent;">
                         
                        <asp:CheckBox ID="CheckReg" runat="server"  Text="Registra" />
                         
                    </td>
                    
                    <td style=" width:135px; background-color:transparent;">
                         
                        <asp:CheckBox ID="CheckAut" runat="server"  Text="Autoriza" />
                         
                    </td>
                    <td style=" width:135px; background-color:transparent;">
                         
                        <asp:CheckBox ID="CheckProce" runat="server" Text="Procesa" />
                         
                    </td>
                   <td style=" width:135px; background-color:transparent;">
                         
                        <asp:CheckBox ID="CheckCance" runat="server" Checked="false" Text="Cancela" />
                         
                    </td>
                     <td style=" width:135px; background-color:transparent;">
                         
                        <asp:CheckBox ID="CheckVal" runat="server" Checked="false" Text="Validar" />
                         
                    </td>
                </tr>
            </table>           
            
        </div>
    </fieldset>
         
    <fieldset>   
    <legend>Seguridad Usuarios</legend>
        <div style="width:100%; display:table; position:static; background-color:transparent;" align="right">  
 
                        <table border="0" style=" width:650px; text-align:left; background-color:transparent;">
                            <tr style="height:18px;">
                                <td style=" width:580px; background-color:transparent; text-align:right;">
                                    <telerik:RadTextBox ID="rTxtBusquedaUsr" runat="server"  Width="300px"
                                                        EnabledStyle-CssClass="cssTxtEnabled"
                                                        DisabledStyle-CssClass ="cssTxtEnabled"
                                                        HoveredStyle-CssClass="cssTxtHovered"
                                                        FocusedStyle-CssClass="cssTxtFocused"
                                                        InvalidStyle-CssClass="cssTxtInvalid" 
                                                     ></telerik:RadTextBox>
                                </td>
                                <td style=" width:70px; background-color:transparent;">
                                    <telerik:RadImageButton ID="rBtnBuscar"  runat="server"   
                                        OnClick="rBtnBuscar_Click" Image-Sizing="Stretch" Height="25px" Text="Buscar"></telerik:RadImageButton>
                                </td>

                            </tr>
                        </table>   

        </div> 
        
        
         <div style="width:100%; display:table; margin-top:5px; position:static; background-color:transparent;" align="center">         

          <telerik:RadGrid ID="rGdv_SeguridadUsuario" 
                           runat="server" 
                           AllowMultiRowSelection="true"
                           AutoGenerateColumns="False" 
                           Width="720px" Height="260px"   OnSelectedIndexChanged="rGdv_SeguridadUsuario_SelectedIndexChanged1" 
                           CssClass="Grid" 
                           Skin="Office2010Silver"  >

                <MasterTableView DataKeyNames="maUsuCve"  AutoGenerateColumns="false" CssClass="GridTable"    >
                    <Columns> 
     <%--  <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn1" ItemStyle-Width="30px" HeaderStyle-Width="30px" ></telerik:GridClientSelectColumn>--%>
                       <telerik:GridBoundColumn HeaderText="Documento"  DataField="docCve"   HeaderStyle-Width="110px"  ItemStyle-Width="110px"   ></telerik:GridBoundColumn> 
                         <telerik:GridBoundColumn HeaderText="Usuario"  DataField="maUsuCve"   HeaderStyle-Width="140px"  ItemStyle-Width="400px" ></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Nombre"  DataField="maUsuNom"   HeaderStyle-Width="250px"  ItemStyle-Width="150px" ></telerik:GridBoundColumn>                                       
                        <%--<telerik:GridBoundColumn HeaderText="Reg."  DataField="segDocRegDes"   HeaderStyle-Width="40px"  ItemStyle-Width="20px" Visible="true" ></telerik:GridBoundColumn>--%>   
                        <%--<telerik:GridBoundColumn HeaderText="Aut."  DataField="segDocAutDes"   HeaderStyle-Width="40px"  ItemStyle-Width="20px" ></telerik:GridBoundColumn>--%>
                        <%--<telerik:GridBoundColumn HeaderText="Proc."  DataField="segDocProcDes"   HeaderStyle-Width="40px"  ItemStyle-Width="20px" ></telerik:GridBoundColumn>                                       
                        <telerik:GridBoundColumn HeaderText="Canc."  DataField="segDocCancDes"   HeaderStyle-Width="40px"  ItemStyle-Width="20px" Visible="true" ></telerik:GridBoundColumn> 
                        <telerik:GridBoundColumn HeaderText="Val."  DataField="segDocValDes"   HeaderStyle-Width="40px"  ItemStyle-Width="20px" Visible="true" ></telerik:GridBoundColumn> --%>
                        
                         <telerik:GridBoundColumn HeaderText="Reg."  DataField="segDocReg"   HeaderStyle-Width="40px"  ItemStyle-Width="40px"  Display="false"  ></telerik:GridBoundColumn>   
                        <telerik:GridBoundColumn HeaderText="Aut."  DataField="segDocAut"   HeaderStyle-Width="40px"  ItemStyle-Width="40px"  Display="false"   ></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Proc."  DataField="segDocProc"   HeaderStyle-Width="40px"  ItemStyle-Width="40px"  Display="false"   ></telerik:GridBoundColumn>                                       
                        <telerik:GridBoundColumn HeaderText="Canc."  DataField="segDocCanc"   HeaderStyle-Width="40px"  ItemStyle-Width="40px"  Display="false"    ></telerik:GridBoundColumn> 
                        <telerik:GridBoundColumn HeaderText="Val."  DataField="segDocVal"   HeaderStyle-Width="40px"  ItemStyle-Width="40px"  Display="false"    ></telerik:GridBoundColumn>                                      


                       <telerik:GridImageColumn DataType="System.String" DataImageUrlFields="imgSegDocReg"  HeaderStyle-Width="40px"  ItemStyle-Width="40px" 
                                               DataAlternateTextField="ContactName" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                     ImageAlign="Middle" ImageHeight="11px" ImageWidth="11px" HeaderText="Reg.">      
                      </telerik:GridImageColumn>  
                        
                       <telerik:GridImageColumn DataType="System.String" DataImageUrlFields="imgSegDocAut"  HeaderStyle-Width="40px"  ItemStyle-Width="40px" 
                                               DataAlternateTextField="ContactName" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                     ImageAlign="Middle" ImageHeight="11px" ImageWidth="11px" HeaderText="Aut.">      
                      </telerik:GridImageColumn>  
                      <telerik:GridImageColumn DataType="System.String" DataImageUrlFields="imgSegDocProc"  HeaderStyle-Width="40px"  ItemStyle-Width="40px" 
                                               DataAlternateTextField="ContactName" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                     ImageAlign="Middle" ImageHeight="11px" ImageWidth="11px" HeaderText="Proc.">      
                      </telerik:GridImageColumn>  
                       <telerik:GridImageColumn DataType="System.String" DataImageUrlFields="imgSegDocCanc"  HeaderStyle-Width="40px"  ItemStyle-Width="40px" 
                                               DataAlternateTextField="ContactName" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                     ImageAlign="Middle" ImageHeight="11px" ImageWidth="11px" HeaderText="Canc.">      
                      </telerik:GridImageColumn> 
                      <telerik:GridImageColumn DataType="System.String" DataImageUrlFields="imgSegDocVal"  HeaderStyle-Width="40px"  ItemStyle-Width="40px" 
                                               DataAlternateTextField="ContactName" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                     ImageAlign="Middle" ImageHeight="11px" ImageWidth="11px" HeaderText="Val.">      
                      </telerik:GridImageColumn>                          
                                                    
                              <telerik:GridBoundColumn HeaderText="maUsuCve."  DataField="maUsuCve"   HeaderStyle-Width="40px"  ItemStyle-Width="20px"  Display="false"    ></telerik:GridBoundColumn>                                      
                                                        
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
                        <Scrolling AllowScroll="true" UseStaticHeaders="true"    ScrollHeight="280px"     />
                    </ClientSettings>
            </telerik:RadGrid>
        
        </div>    
                
     </fieldset>
                      
 
    <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
        <telerik:RadImageButton ID="rBtnGuardar"  runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text ="" OnClick="rBtnGuardar_Click" OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
        <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text="" OnClick="rBtnCancelar_Click" OnClientClicking="OnClientClic_ConfirmCancel"></telerik:RadImageButton>
    </asp:Panel>


    <asp:HiddenField ID="hdfBtnAccion" runat="server" />
                                
    </div>

    </ContentTemplate>
    </asp:UpdatePanel> 

  
    <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server"      >
    </telerik:RadWindowManager>

    </form>
</body>
</html>
