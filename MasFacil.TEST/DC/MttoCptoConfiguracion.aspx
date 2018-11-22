<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MttoCptoConfiguracion.aspx.cs" Inherits="DC_MttoCptoConfiguracion" %>
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
    <style type="text/css">
        .auto-style2 {
            width: 100%;
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
                            <telerik:RadLabel ID="rLblCptoId" runat="server" Text="xxxxxxxxxxxxxxxx"></telerik:RadLabel>
                             <telerik:RadLabel ID="rLblcptoDes" runat="server" Text="xxxxxxxxxxxxxxxx"></telerik:RadLabel>
                        </td>
                   

                    </tr>
        
                </table>
            </div>
        </fieldset>

 
<div runat="server" visible="false">
 
                    <table border="0" style=" text-align:left; background-color:transparent;" >
                                    <tr>
                                        <td style=" width:50%; background-color:transparent;">
                                            <telerik:RadLabel ID="RadLabel4" runat="server" Text="Agrupacion" width="120px" Visible="false">
                                            </telerik:RadLabel>
                                            <telerik:RadComboBox ID="rCboAgrupacion" runat="server" AutoPostBack="True" DropDownCssClass="cssRadComboBox" DropDownWidth="290px" Height="200px" HighlightTemplatedItems="true" width="180px" Visible="false">
                                                <HeaderTemplate>
                                                    <table cellpadding="0" cellspacing="0" style="width: 275px">
                                                        <tr>
                                                            <td style="width: 75px;">Id </td>
                                                            <td style="width: 275px;">Nombre </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table cellpadding="0" cellspacing="0" style="width: 275px;">
                                                        <tr>
                                                            <td style="width: 75px;"><%# DataBinder.Eval(Container.DataItem, "empleado") %></td>
                                                            <td style="width: 275px;"><%# DataBinder.Eval(Container.DataItem, "nombre") %></td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Literal ID="RadComboItemsCount" runat="server" />
                                                </FooterTemplate>
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style=" width:50%; background-color:transparent;">
                                            <telerik:RadLabel ID="RadLabel12" runat="server" Text="Clave Lista" width="120px" Visible="false">
                                            </telerik:RadLabel>
                                            <telerik:RadComboBox ID="rCboValidacion" runat="server" AutoPostBack="True" DropDownCssClass="cssRadComboBox" DropDownWidth="290px" Height="200px" HighlightTemplatedItems="true" width="180px" Visible ="false">
                                                <HeaderTemplate>
                                                    <table cellpadding="0" cellspacing="0" style="width: 275px">
                                                        <tr>
                                                            <td style="width: 75px;">Id </td>
                                                            <td style="width: 275px;">Nombre </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table cellpadding="0" cellspacing="0" style="width: 275px;">
                                                        <tr>
                                                            <td style="width: 75px;"><%# DataBinder.Eval(Container.DataItem, "empleado") %></td>
                                                            <td style="width: 275px;"><%# DataBinder.Eval(Container.DataItem, "nombre") %></td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Literal ID="RadComboItemsCount" runat="server" />
                                                </FooterTemplate>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td style=" width:50%; background-color:transparent;">
                                           
                                            <telerik:RadTextBox ID="RadTxtPrgValid" runat="server" DisabledStyle-CssClass="cssTxtEnabled" EnabledStyle-CssClass="cssTxtEnabled" FocusedStyle-CssClass="cssTxtFocused" HoveredStyle-CssClass="cssTxtHovered" InvalidStyle-CssClass="cssTxtInvalid" MaxLength="64" width="180px" Visible="false">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                          </table>
            
   
            <telerik:RadTextBox ID="cptoIConfDetID" width="180px" runat="server" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled" Visible="false"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadTextBox>

</div>


     


        <fieldset style=" display: block; text-align:left;" >
            <legend>Datos</legend>  
            <div style="width:100%; display:table; position:static; background-color:transparent;" align="center" >
                <table border="0" style=" width:760px; text-align:left; background-color:transparent;">
                    <tr style="height:18px;">
                        <td style=" width:110px; background-color:transparent;">
                             Solicitud
                        </td>
                        <td style=" width:210px;  background-color:transparent;">                             
                            <telerik:RadTextBox ID="rTxtSolicitud" runat="server" DisabledStyle-CssClass="cssTxtEnabled" EnabledStyle-CssClass="cssTxtEnabled" FocusedStyle-CssClass="cssTxtFocused" HoveredStyle-CssClass="cssTxtHovered" InvalidStyle-CssClass="cssTxtInvalid" MaxLength="64" width="180px">
                            </telerik:RadTextBox>   
                        </td>
                        <td style=" width:110px; background-color:transparent;">
                            Tipo de Dato
                        </td>
                        <td style=" width:210px;  background-color:transparent;">                             
                            <telerik:RadComboBox ID="rCboTipoDato"  width="180px" runat="server" HighlightTemplatedItems="true" DropDownWidth="290px" Height="200px"
                                                     DropDownCssClass="cssRadComboBox" AutoPostBack="True" 
                                                      OnSelectedIndexChanged="rCboTipoDato_SelectedIndexChanged"
                                >            
                                  <HeaderTemplate>
                                        <table style="width: 190px" cellspacing="0" cellpadding="0">
                                                <tr>
                                                     <td style="width: 95px;">
                                                        Descripción
                                                    </td>
                                                    <td style="width: 95px;">
                                                        Secuencia
                                                    </td>
                                                </tr>
                                            </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                            <table style="width: 180px;"  cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 90px;">
                                                        <%# DataBinder.Eval(Container.DataItem, "listTipDatoCptoDes") %>
                                                    </td>
                                                     <td style="width: 90px;">
                                                        <%# DataBinder.Eval(Container.DataItem, "listTipDatoCptoSec") %>
                                                    </td>
                                                </tr>
                                            </table>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                    </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td style=" width:80px; background-color:transparent;">
                            Secuencia
                        </td>
                        <td style=" width:40px;  background-color:transparent;">                             
                            <telerik:RadNumericTextBox ID="RadTxtSecuenc" runat="server" DisabledStyle-CssClass="cssTxtEnabled" EnabledStyle-CssClass="cssTxtEnabled" FocusedStyle-CssClass="cssTxtFocused" HoveredStyle-CssClass="cssTxtHovered" InvalidStyle-CssClass="cssTxtInvalid" MaxLength="2" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" width="30px">
                            </telerik:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr style="height:18px;">
                        <td style=" width:110px; background-color:transparent;">
                             Captura
                        </td>
                        <td style=" width:210px;  background-color:transparent;">                             
                            <asp:RadioButton ID="rBtnRequerido" runat="server" Checked="true" GroupName="isEmployee" Text="Opcional" />
                            <asp:RadioButton ID="rBtnOpcional" runat="server" GroupName="isEmployee" Text="Requerido" />
                        </td>
                        <td style=" width:110px; background-color:transparent;">
                            Justificacion
                        </td>
                        <td style=" width:210px;  background-color:transparent;">                             
                            <asp:RadioButton ID="rBtnIzquierda" runat="server" Checked="true" GroupName="Justification" Text="Izquierda" />
                            <asp:RadioButton ID="rBtnDerecha" runat="server" GroupName="Justification" Text="Derecha" />
                        </td>
                        <td style=" width:80px; background-color:transparent;">
                            Relleno
                        </td>
                        <td style=" width:40px;  background-color:transparent;">                             
                            <telerik:RadTextBox ID="rTxtRell" runat="server" 
                                DisabledStyle-CssClass="cssTxtEnabled" 
                                EnabledStyle-CssClass="cssTxtEnabled" 
                                FocusedStyle-CssClass="cssTxtFocused" 
                                HoveredStyle-CssClass="cssTxtHovered" 
                                InvalidStyle-CssClass="cssTxtInvalid" 
                                 MaxLength="1"  width="30px">
                            </telerik:RadTextBox> 
                        </td>
                    </tr>
                </table> 
            </div>
        </fieldset>

        <fieldset style=" display: block; text-align:left;" >
            <legend>Programa</legend> 
            <div style="width:100%; display:table; position:static; background-color:transparent;" align="center" >
                <asp:Panel ID="pnlPrograma" runat="server">
                <table border="0" style=" width:760px; text-align:left; background-color:transparent;">
                    <tr style="height:18px;">
                        <td style=" width:110px; background-color:transparent;">
                             Programa
                        </td>
                        <td style=" width:210px;  background-color:transparent;">                             
                            <telerik:RadComboBox ID="rCboPrograma" runat="server"  AutoPostBack="True"
                                  Enabled="false" 
                              HighlightTemplatedItems="true"
                              DropDownCssClass="cssRadComboBox" AllowCustomText="true" 
                              Width="180px" DropDownWidth="390px" Height="200px" >
                                 <HeaderTemplate>
                                    <table style="width: 360px" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="width: 160px;">
                                                    Clave
                                                </td>
                                                <td style="width: 200px;">
                                                    Descripción
                                                </td>
                                            </tr>
                                        </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                        <table style="width: 360px;"  cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="width:160px" >
                                                    <%# DataBinder.Eval(Container.DataItem, "progCve")%>
                                                </td>
                                                <td style="width: 200px;">
                                                    <%# DataBinder.Eval(Container.DataItem, "progDes") %>
                                                </td>
                                            </tr>
                                        </table>
                                </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                    </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td style=" width:110px; background-color:transparent;">
                            Prog. Valida
                        </td>
                        <td style=" width:210px;  background-color:transparent;">                             
                            <telerik:RadComboBox ID="rCboProgValida" runat="server"  AutoPostBack="True"
                                              Enabled="true" 
                              HighlightTemplatedItems="true"
                              DropDownCssClass="cssRadComboBox"  AllowCustomText="true"
                              Width="180px" DropDownWidth="390px" Height="200px" >
                                  <HeaderTemplate>
                                    <table style="width: 320px" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="width: 160px;">
                                                    Clave
                                                </td>
                                                <td style="width: 200px;">
                                                    Descripción
                                                </td>
                                            </tr>
                                        </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                        <table style="width: 360px;"  cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="width:160px" >
                                                    <%# DataBinder.Eval(Container.DataItem, "progCve")%>
                                                </td>
                                                <td style="width: 200px;">
                                                    <%# DataBinder.Eval(Container.DataItem, "progDes") %>
                                                </td>
                                            </tr>
                                        </table>
                                </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                        </FooterTemplate>
                        </telerik:RadComboBox>
                        </td>
                        <td style=" width:80px; background-color:transparent;">
                             
                        </td>
                        <td style=" width:40px;  background-color:transparent;">                             
                       
                        </td>
                    </tr>
                </table>
                </asp:Panel>
            </div>
        </fieldset>

        <fieldset style=" display: block; text-align:left;" >
            <legend>Formula</legend>  
            <div style="width:100%; display:table; position:static; background-color:transparent;" align="center" >
                <asp:Panel ID="pnlFormula" runat="server">

                <table border="0" style=" width:100%; text-align:left; background-color:transparent;">


                    <tr style="height:18px;">
                        <td style=" font-size:11px; color:dimgray; width:100%;  background-color:transparent;"> 
                            Ejemplo: (Imp_01 * Imp_02) / Fact_01
                        </td>
                    </tr>
                    <tr style="height:18px;">
                         <td style=" width:100%;  background-color:transparent;">                             
                            <telerik:RadTextBox ID="txtFormula" runat="server" DisabledStyle-CssClass="cssTxtEnabled" EnabledStyle-CssClass="cssTxtEnabled" FocusedStyle-CssClass="cssTxtFocused" HoveredStyle-CssClass="cssTxtHovered" InvalidStyle-CssClass="cssTxtInvalid"  width="100%">
                            </telerik:RadTextBox>
                        </td>
                    </tr>

                </table> 

                </asp:Panel>
            </div>
            
       </fieldset>


        <div style="width:100%; margin-top:5px; display:table; position:static; background-color:transparent;" align="center" >  
 
                  
                    <telerik:RadGrid ID="rGdvInformacion" 
                        runat="server"
                        AutoGenerateColumns ="false" 
                    OnSelectedIndexChanged="rGdvInformacion_SelectedIndexChanged"
                        Width="99%" Height="190px" 
                        CssClass="Grid" 
                        skin="Office2010Silver"  
                          >

                        <MasterTableView  
                                         AutoGenerateColumns="False"  
                                          CssClass="GridTable" >

                         <Columns >
 
                              <telerik:GridBoundColumn DataField="cptoConfDes"  HeaderText="Solicitud"  HeaderStyle-Width="100px" ItemStyle-Width="100px"  />
                              <telerik:GridBoundColumn DataField="listTipDatoCptoDes"  HeaderText="Tipo de dato" HeaderStyle-Width="100px" ItemStyle-Width="100px"    ItemStyle-HorizontalAlign="Right"/>
                              <telerik:GridBoundColumn DataField="cptoConfSec"    HeaderText="Sec." HeaderStyle-Width="80px"  ItemStyle-Width="80px"   ItemStyle-HorizontalAlign="Right" />
                              <telerik:GridBoundColumn DataField="tipoCapturaDesc"  HeaderText="Tipo de Captura" HeaderStyle-Width="100px" ItemStyle-Width="100px"    ItemStyle-HorizontalAlign="Right"/>

                              <telerik:GridBoundColumn DataField="TipoJust" HeaderText="Justificación" HeaderStyle-Width="90px"  ItemStyle-Width="90px"    ItemStyle-HorizontalAlign="Right" />
                              <telerik:GridBoundColumn DataField="cptoConfRell"    HeaderText="Relleno" HeaderStyle-Width="70px"  ItemStyle-Width="70px"   ItemStyle-HorizontalAlign="Right"/>
                              <telerik:GridBoundColumn DataField="cptoConfFormula"    HeaderText="Formula" HeaderStyle-Width="150px"  ItemStyle-Width="150px"   ItemStyle-HorizontalAlign="Left"/>
                                
                              <telerik:GridBoundColumn DataField="cptoConfTipCap"    HeaderText="cptoConfTipCap" HeaderStyle-Width="100px"  ItemStyle-Width="100px"   ItemStyle-HorizontalAlign="Right" Display="false"/>
                              <telerik:GridBoundColumn DataField="listTipDatoCptoCve"    HeaderText="listTipDatoCptoCve" HeaderStyle-Width="100px"  ItemStyle-Width="100px"   ItemStyle-HorizontalAlign="Right" Display="false"/>
                              <telerik:GridBoundColumn DataField="cptoConfJust"    HeaderText="cptoConfJust" HeaderStyle-Width="100px"  ItemStyle-Width="100px"   ItemStyle-HorizontalAlign="Right" Display="false"/>

                             <telerik:GridBoundColumn DataField="cptoConfProgCve"    HeaderText="cptoConfProgCve" HeaderStyle-Width="100px"  ItemStyle-Width="100px"   ItemStyle-HorizontalAlign="Right" Display="false"/>
                             <telerik:GridBoundColumn DataField="cptoConfProgCveVal"    HeaderText="cptoConfProgCveVal" HeaderStyle-Width="100px"  ItemStyle-Width="100px"   ItemStyle-HorizontalAlign="Right" Display="false"/>
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
 
    </ContentTemplate>
    </asp:UpdatePanel>

        <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
        </telerik:RadWindowManager>

    </form>
</body>
</html>
