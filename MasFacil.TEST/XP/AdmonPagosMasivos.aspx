<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdmonPagosMasivos.aspx.cs" Inherits="XP_AdmonPagosMasivos" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="http://code.jquery.com/jquery-1.11.0.min.js"></script>
    <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
    <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
    <link href="../css/styles.css" rel="stylesheet" type="text/css" />
    <link href="/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function Close() {

            //showAlert_ConfirmXPmasivo(countProv, countPago, countFact);

            GetRadWindow().close();
        }


        function showAlert_ConfirmXPmasivo(countProv, countPago, countFact) {
            
            var text = "Se han incluido:" + countProv + " Proveedores ";
            text += " Se han seleccionado:" + countPago + " Pasivos ";
            text += " Se han generado:" + countFact + " Facturas ";
            text += " Esta seguro que desea realizar la accion? ";

            radalert(text);
                
        }

    </script>
    
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

        <telerik:RadAjaxManager ID="RAJAXMAN1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
            <AjaxSettings>    
                <telerik:AjaxSetting AjaxControlID="RAJAXMAN1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID = "pnlBody" />
                    </UpdatedControls>
                </telerik:AjaxSetting>


                <telerik:AjaxSetting AjaxControlID="rBtnGuardar">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID = "pnlBody" />
                    </UpdatedControls>
                </telerik:AjaxSetting>

                
            </AjaxSettings>
        </telerik:RadAjaxManager>
        
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Silk"></telerik:RadAjaxLoadingPanel>

        <asp:Panel ID="pnlBody" runat="server">
            
                <div style="width:100%; display:table; position:static; background-color:transparent;"> 
                    
                    <table border="0" style=" border-style:none; width:100%; text-align:left; background-color:transparent ;">
                        <tr  style="width:100%;">
                            <td style=" width:100%; background-color:transparent; vertical-align:top;">
                                <fieldset style="">
                                    <legend>Encabezado</legend>
                                    <div style="width:100%; display:table; position:static; background-color:transparent;"> 
                                        <table border="0" style=" border-style:none;   text-align:left; background-color:transparent;">
                                            <tr>             
                                                
                                                <td style=" width:180px; background-color:transparent; vertical-align:top;" >
                                                    <asp:Label ID="Label6" runat="server" Text="Concepto"></asp:Label>
                                                </td>
                                                <td style=" width:225px; background-color:transparent; vertical-align:top;">
                                                    <telerik:RadComboBox ID="rCboConcepto" runat="server" Width="220px" AutoPostBack="true" 
                                                        OnSelectedIndexChanged="rCboConcepto_SelectedIndexChanged"
                                                        HighlightTemplatedItems="true"
                                                                DropDownCssClass="cssRadComboBox" 
                                                                DropDownWidth="300px" 
                                                                Height="200px"   >
                                                                <HeaderTemplate>
                                                                    <table style="width: 300px" cellspacing="0" cellpadding="0">
                                                                            <tr>
                                                                                <td style="width: 100px;">
                                                                                    Clave
                                                                                </td>
                                                                                <td style="width: 200px;">
                                                                                    Descripción
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <table style="width: 300px;"  cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td style="width:100px" >
                                                                                <%# DataBinder.Eval(Container.DataItem, "cptoId")%>
                                                                            </td>
                                                                            <td style="width: 200px;">
                                                                                <%# DataBinder.Eval(Container.DataItem, "cptoDes") %>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                                </FooterTemplate>
                                                    </telerik:RadComboBox>
                                                </td>
                                                
                                                
                                                <td style=" width:55px; background-color:transparent; vertical-align:top; text-align:left" >
                                                    <asp:Label ID="Label8" runat="server" Text="Fecha"></asp:Label>
                                                    
                                                </td>
                                                <td style=" width:100px; background-color:transparent; vertical-align:top;">
                                                    <telerik:RadDatePicker ID="RdDateFecha" runat="server" Width="120px" OnSelectedDateChanged="RdDateFecha_SelectedDateChanged" AutoPostBack="true"></telerik:RadDatePicker>
                                                </td>

                                           </tr>

                                            <tr>


                                                         
                                                <td style=" width:180px; background-color:transparent; vertical-align:top;">
                                                    <asp:Label ID="Label5" runat="server" Text="Cta. Deposito"></asp:Label>
                                                </td>
                                                <td style=" width:225px; background-color:transparent; vertical-align:top;">
                                                        <telerik:RadComboBox ID="rCboCuenta" runat="server" Width="220px" AutoPostBack="true"
                                                                HighlightTemplatedItems="true"
                                                                DropDownCssClass="cssRadComboBox" 
                                                                DropDownWidth="300px" 
                                                                Height="200px"  >
                                                                <HeaderTemplate>
                                                                    <table style="width: 300px" cellspacing="0" cellpadding="0">
                                                                            <tr>
                                                                                <td style="width: 100px;">
                                                                                    Clave
                                                                                </td>
                                                                                <td style="width: 200px;">
                                                                                    Descripción
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <table style="width: 300px;"  cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td style="width:100px" >
                                                                                <%# DataBinder.Eval(Container.DataItem, "ctaDepCve")%>
                                                                            </td>
                                                                            <td style="width: 200px;">
                                                                                <%# DataBinder.Eval(Container.DataItem, "ctaDepDes") %>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                                </FooterTemplate>
                                                        </telerik:RadComboBox>
                                                </td>

                                                

                                           

                                                <td style=" width:100px; background-color:transparent; vertical-align:top; text-align:left" >
                                                    <asp:Label ID="Label7" runat="server" Text="Descripción"></asp:Label>
                                                    
                                                </td>



                                                <td style=" background-color:transparent; vertical-align:top;">
                                                    <telerik:RadTextBox ID="rTxtDescripcion"
                                                        EnabledStyle-CssClass="cssTxtEnabled"
                                                        DisabledStyle-CssClass ="cssTxtEnabled"
                                                        HoveredStyle-CssClass="cssTxtHovered"
                                                        FocusedStyle-CssClass="cssTxtFocused"
                                                        InvalidStyle-CssClass="cssTxtInvalid" 
                                                            Width="175px" runat="server"></telerik:RadTextBox>
                                                </td>

                                            </tr>

                                            <tr>


                                                <td style=" width:180px; background-color:transparent; vertical-align:top;">
                                                    <asp:Label ID="Label1" runat="server" Text="Manejo de Pagos:"></asp:Label>
                                                </td>

                                                <td style=" width:250px; background-color:transparent; vertical-align:top;">
                                                    
                                                    <asp:RadioButton ID="rBtnManPagProv" runat="server" GroupName="ManPag"  Text="Por Proveedor" Checked="true" Enabled="true" OnCheckedChanged="rBtnManPagProv_CheckedChanged" AutoPostBack="true" />
                                                    <asp:RadioButton ID="rBtnManPagFact"  runat="server"   GroupName="ManPag" Text="Por Factura" Checked="false" Enabled="true" OnCheckedChanged="rBtnManPagFact_CheckedChanged" AutoPostBack="true" />
                                                </td>

                                            </tr> 

                                        </table>                          
                                    </div>
                                </fieldset>
                            </td>
                        </tr>

                    </table>

                    

                    <table border="0" style=" border-style:none; width:100%; text-align:left; background-color:transparent ;">

                        <tr  style="width:100%;">
                            <td style=" width:100%; background-color:transparent; vertical-align:top;">
                                <fieldset style="">
                                    <legend>Criterios de Selección</legend>
                                    <div style="width:100%; display:table; position:static; background-color:transparent;"> 
                                        <table border="0" style=" border-style:none;   text-align:left; background-color:transparent;">
                                            <tr>                      
                                                <td style=" width:180px; background-color:transparent; vertical-align:top;">
                                                    <asp:Label ID="Label2" runat="server" Text="Proveedor"></asp:Label>
                                                </td>
                                                <td style=" width:250px; background-color:transparent; vertical-align:top;">
                                                    <asp:Label ID="Label11" runat="server" Text="Clave" Width="50px"></asp:Label>   
                                                
                                                    <telerik:RadTextBox ID="rTxtProvCve"
                                                            OnTextChanged="rTxtProvCve_TextChanged"
                                                            AutoPostBack="true" Width="170px"
                                                        EnabledStyle-CssClass="cssTxtEnabled"
                                                        DisabledStyle-CssClass ="cssTxtEnabled"
                                                        HoveredStyle-CssClass="cssTxtHovered"
                                                        FocusedStyle-CssClass="cssTxtFocused"
                                                        InvalidStyle-CssClass="cssTxtInvalid" 
                                                         runat="server"></telerik:RadTextBox>
                                                </td>
                                                <td style=" width:300px; background-color:transparent; vertical-align:top;">
                                                    <asp:Label ID="Label3" runat="server" Text="Nombre" Width="100px"></asp:Label>   
                                                
                                                    <telerik:RadTextBox ID="rTxtProvNom"
                                                        Width="170px"
                                                         OnTextChanged="rTxtProvNom_TextChanged"
                                                         AutoPostBack="true"
                                                        EnabledStyle-CssClass="cssTxtEnabled"
                                                        DisabledStyle-CssClass ="cssTxtEnabled"
                                                        HoveredStyle-CssClass="cssTxtHovered"
                                                        FocusedStyle-CssClass="cssTxtFocused"
                                                        InvalidStyle-CssClass="cssTxtInvalid" 
                                                        runat="server"></telerik:RadTextBox>
                                                </td>

                                        
                                            </tr>


                                            <tr>                      
                                                <td style=" width:180px; background-color:transparent; vertical-align:top;">
                                                    <asp:Label ID="Label4" runat="server" Text="Fecha Vencimiento"></asp:Label>
                                                </td>
                                                <td style=" width:250px; background-color:transparent; vertical-align:top;">
                                                        <asp:Label ID="Label9" runat="server" Text="Desde" Width="50px"></asp:Label>   
                                                
                                                    <telerik:RadDatePicker ID="RdDateFecha_InicioVenc" runat="server" Width="120px" AutoPostBack="true" OnSelectedDateChanged="RdDateFecha_InicioVenc_SelectedDateChanged" ></telerik:RadDatePicker> 
                                                </td>
                                                <td style=" width:300px; background-color:transparent; vertical-align:top;">
                                                    <asp:Label ID="Label10" runat="server" Text="Hasta" Width="100px"></asp:Label>   
                                                
                                                    <telerik:RadDatePicker ID="RdDateFecha_FinalVenc" runat="server" Width="120px" AutoPostBack="true" OnSelectedDateChanged="RdDateFecha_FinalVenc_SelectedDateChanged" ></telerik:RadDatePicker> 
                                                </td>

                                        
                                            </tr>

                                            <tr>                      
                                                <td style=" width:180px;; background-color:transparent; vertical-align:top;">
                                                    <asp:Label ID="Label12" runat="server" Text="Fecha Programada"></asp:Label>
                                                </td>
                                                <td style=" width:250px; background-color:transparent; vertical-align:top;">
                                                        <asp:Label ID="Label13" runat="server" Text="Desde" Width="50px"></asp:Label>   
                                                
                                                    <telerik:RadDatePicker ID="RdDateFecha_InicioProg" runat="server" Width="120px" AutoPostBack="true" OnSelectedDateChanged="RdDateFecha_InicioProg_SelectedDateChanged" ></telerik:RadDatePicker> 
                                                </td>
                                                <td style=" width:300px; background-color:transparent; vertical-align:top;">
                                                    <asp:Label ID="Label14" runat="server" Text="Hasta" Width="100px"></asp:Label>   
                                                
                                                    <telerik:RadDatePicker ID="RdDateFecha_FinalProg" runat="server" Width="120px" AutoPostBack="true" OnSelectedDateChanged="RdDateFecha_FinalProg_SelectedDateChanged" ></telerik:RadDatePicker> 
                                                </td>

                                        
                                            </tr>

                                            <tr>                      
                                                <td style=" width:180px; background-color:transparent; vertical-align:top;">
                                                    <asp:Label ID="Label15" runat="server" Text="Importe"></asp:Label>
                                                </td>
                                                <td style=" width:250px; background-color:transparent; vertical-align:top;">
                                                        <asp:Label ID="Label16" runat="server" Text="Desde" Width="50px"></asp:Label>   
                                                
                                                    <telerik:RadNumericTextBox runat="server" ID="rTxtImpIni" Width="100px" 
                                                         OnTextChanged="rTxtImpIni_TextChanged"
                                                         AutoPostBack="true"
                                                            EnabledStyle-CssClass="cssTxtEnabled"
                                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                                            HoveredStyle-CssClass="cssTxtHovered"
                                                            FocusedStyle-CssClass="cssTxtFocused"
                                                            InvalidStyle-CssClass="cssTxtInvalid" 
                                                            MinValue="0" ShowSpinButtons="false" NumberFormat-DecimalDigits="2" Type="Currency"></telerik:RadNumericTextBox>
                                                </td>
                                                <td style=" width:300px; background-color:transparent; vertical-align:top;">
                                                    <asp:Label ID="Label17" runat="server" Text="Hasta" Width="100px"></asp:Label>   
                                                
                                                    <telerik:RadNumericTextBox runat="server" ID="rTxtImpFin" Width="100px" 
                                                         OnTextChanged="rTxtImpFin_TextChanged"
                                                         AutoPostBack="true"
                                                            EnabledStyle-CssClass="cssTxtEnabled"
                                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                                            HoveredStyle-CssClass="cssTxtHovered"
                                                            FocusedStyle-CssClass="cssTxtFocused"
                                                            InvalidStyle-CssClass="cssTxtInvalid" 
                                                            MinValue="0" ShowSpinButtons="false" NumberFormat-DecimalDigits="2" Type="Currency"></telerik:RadNumericTextBox>
                                                </td>

                                        
                                            </tr>

                                    
                                        </table>                          
                                    </div>
                                    
                                    

                                </fieldset>
                            </td>
                        </tr>

                        
                        <tr  style="width:100%;">
                        </tr>

                        <tr  style="width:100%;">
                            <td style=" width:100%; background-color:transparent; vertical-align:top;">
                            
                                <telerik:RadGrid    ID="rGdvPagosPend" 
                                                     
                                                    runat="server"
                                                    AllowMultiRowSelection="true"
                                                    AutoGenerateColumns="False"  
                                                    Height="200px"
                                                    CssClass="Grid"
                                                    skin="Office2010Silver"  >
                                                    
                                            <MasterTableView runat="server"  DataKeyNames="partPenPagId" AutoGenerateColumns="false"  CssClass="GridTable"  >
                                                    <Columns>
                                                        <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" ItemStyle-Width="15px" HeaderStyle-Width="15px">
                                                          <ItemTemplate>
                                                            <asp:CheckBox Width="15px" ID="CheckBox1" runat="server" OnCheckedChanged="ToggleRowSelection"
                                                              AutoPostBack="True" />
                                                          </ItemTemplate>
                                                          <HeaderTemplate>
                                                            <asp:CheckBox Width="15px" ID="headerChkbox" runat="server" OnCheckedChanged="ToggleSelectedState"
                                                              AutoPostBack="True" />
                                                          </HeaderTemplate>
                                                        </telerik:GridTemplateColumn>

                                                        <%--<telerik:GridClientSelectColumn UniqueName="ClientSelectColumn1"  ItemStyle-Width="10px" HeaderStyle-Width="10px">
                                                        </telerik:GridClientSelectColumn>--%>
                                                        <telerik:GridBoundColumn DataField="provCve"  UniqueName="provCve"   HeaderText="Proveedor" HeaderStyle-Width="50px"  ItemStyle-Width="50px"   Display="true"/>
                                                        <telerik:GridBoundColumn DataField="provNom"   UniqueName="provNom"  HeaderText="Nombre" HeaderStyle-Width="50px"  ItemStyle-Width="50px"   Display="true"/>
                                                        <telerik:GridBoundColumn DataField="movRef10_Princ"   UniqueName="movRef10_Princ"  HeaderText="Referencia"         HeaderStyle-Width="50px"  ItemStyle-Width="50px"   Display="true"/>
                                                        <telerik:GridBoundColumn DataField="partPenPagFecVenc" UniqueName="partPenPagFecVenc" HeaderText="Fecha Venc." HeaderStyle-Width="33px"  ItemStyle-Width="33px"   Display="true" DataFormatString="{0:d}"/>    
                                                        <telerik:GridBoundColumn DataField="partPenPagFecProg" HeaderText="Fecha Prog."        HeaderStyle-Width="33px"  ItemStyle-Width="33px"   Display="true" DataFormatString="{0:d}"/>
                                                        <telerik:GridBoundColumn DataField="partPenPagImpAbo"  UniqueName="partPenPagImpAbo" HeaderText="Importe"            HeaderStyle-Width="40px"  ItemStyle-Width="40px"   Display="true" DataFormatString="{0:###,##0.00}" ItemStyle-HorizontalAlign="Right"/>
                                                    </Columns>
                                                    <NoRecordsTemplate> No se encontraron registros.</NoRecordsTemplate>    
                                            </MasterTableView>
                                            <HeaderStyle CssClass="GridHeaderStyle"/>
                                            <ItemStyle CssClass="GridRowStyle"/>
                                            <AlternatingItemStyle  CssClass="GridAlternatingRowStyle" />
                                            <selecteditemstyle CssClass="GridSelectedItem"></selecteditemstyle>
 
                                            <ClientSettings Selecting-AllowRowSelect="true"  EnablePostBackOnRowClick="true" >
                                                <Selecting AllowRowSelect="true"  EnableDragToSelectRows="False" ></Selecting>
                                                <Scrolling AllowScroll="true" UseStaticHeaders="true"  ScrollHeight="330px"    />
                                            
                                            </ClientSettings>
                                        </telerik:RadGrid>


                                
                            
                            </td>
                        </tr>

                    </table>

                </div>

                
            <table border="0" style=" border-style:none; width:100%; text-align:left; background-color:transparent ;">

                    <tr style="width:100%;">
                        <td style=" width:75%; background-color:transparent; vertical-align:top;">
                            <asp:Label ID="lblTotPagos" runat="server" Font-Size="Smaller" Text="Se han incluido: 0 Proveedor(es) | Se han seleccionado: 0 Pasivo(s) | Se han generado: 0 Factura(s) " Width="100%"></asp:Label>   
                        </td>

                        <td style=" width:25%; background-color:transparent; vertical-align:top;">

                            <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
                                <%--GRET, se agrega boton para hacer visibles solo los items seleccionados al momento de dar Aceptar--%>
                                 <telerik:RadImageButton ID="rBtnGuardar2"  runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Aceptar"  Text =""    OnClick="rBtnGuardar2_Click"></telerik:RadImageButton>
                                <telerik:RadImageButton ID="rBtnGuardar"  runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text =""    OnClick="rBtnGuardar_Click"  OnClientClicking ="OnClientClic_ConfirmOK" ></telerik:RadImageButton>
                                <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text=""   OnClick="rBtnCancelar_Click" OnClientClicking="OnClientClic_ConfirmOK"  ></telerik:RadImageButton>
                            </asp:Panel>  
                        </td>

                    </tr>
            </table>
                

                

        </asp:Panel>
         
        <asp:HiddenField ID="hdfBtnAccionDet" runat="server" />
        <asp:HiddenField ID="hdfPag_sOpe" runat="server" /> 
        <asp:HiddenField ID="hdfBtnAccion" runat="server" />
   
         <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false">    
         </telerik:RadWindowManager>
    </form>
</body>
</html>
