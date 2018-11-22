<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MttoListaPrecios.aspx.cs" Inherits="DC_MttoListaPrecios" %>
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


    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />


    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            function closeRadWindow()  
            {  
                $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest();   
            }

            var isDoubleClick = false;
            var clickHandler = null;
            var ClikedDataKey = null;

            function RowClick(sender, args) {
                ClikedDataKey = args._dataKeyValues.lisPreCve;
                isDoubleClick = false;
                if (clickHandler) {
                    window.clearTimeout(clickHandler);
                    clickHandler = null;
                }
                clickHandler = window.setTimeout(ActualClick, 200);
            }

            function RowDblClick(sender, args) {
                ClikedDataKey = args._dataKeyValues.lisPreCve;
                isDoubleClick = true;
                if (clickHandler) {
                    window.clearTimeout(clickHandler);
                    clickHandler = null;
                }
                clickHandler = window.setTimeout(ActualClick, 200);
            }

            function ActualClick() {
                if (isDoubleClick) {
                    var grid = $find("<%=rGdvList.ClientID %>");
                    if (grid) {
                        var MasterTable = grid.get_masterTableView();
                        var Rows = MasterTable.get_dataItems();
                        for (var i = 0; i < Rows.length; i++) {
                            var row = Rows[i];
                            if (ClikedDataKey != null && ClikedDataKey == row.getDataKeyValue("lisPreCve")) {
                                MasterTable.fireCommand("RowDoubleClick", ClikedDataKey);
                            }
                        }
                    }
                }
                else {
                    var grid = $find("<%=rGdvList.ClientID %>");
                    if (grid) {
                        var MasterTable = grid.get_masterTableView();
                        var Rows = MasterTable.get_dataItems();
                        for (var i = 0; i < Rows.length; i++) {
                            var row = Rows[i];
                            if (ClikedDataKey != null && ClikedDataKey == row.getDataKeyValue("lisPreCve")) {
                                MasterTable.fireCommand("RowSingleClick", ClikedDataKey);
                            }
                        }
                    }
                }
            }
        </script>


        
    </telerik:RadCodeBlock>

</head>
<body>

    <form id="form1" runat="server">

        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest" DefaultLoadingPanelID="RadAjaxLoadingPanel1" >  
                    <AjaxSettings> 
                        <telerik:AjaxSetting AjaxControlID="rBtnNuevo">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID = "pnlHead" /> 
                                <telerik:AjaxUpdatedControl ControlID = "pnlDetail" />
                            </UpdatedControls>
                        </telerik:AjaxSetting>
                        <telerik:AjaxSetting AjaxControlID="rBtnModificar">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID = "pnlHead" />
                                <telerik:AjaxUpdatedControl ControlID = "pnlDetail" />
                            </UpdatedControls>
                        </telerik:AjaxSetting>
                        <telerik:AjaxSetting AjaxControlID="rBtnEliminar">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID = "pnlHead" />
                                <telerik:AjaxUpdatedControl ControlID = "pnlDetail" />
                            </UpdatedControls>
                        </telerik:AjaxSetting>
                        <telerik:AjaxSetting AjaxControlID="rBtnValidacion">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID = "pnlHead" />
                                <telerik:AjaxUpdatedControl ControlID = "pnlDetail" />
                            </UpdatedControls>
                        </telerik:AjaxSetting>
                        <telerik:AjaxSetting AjaxControlID="rBtVerErr">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID = "pnlHead" />
                                <telerik:AjaxUpdatedControl ControlID = "pnlDetail" />
                            </UpdatedControls>
                        </telerik:AjaxSetting>
                        <telerik:AjaxSetting AjaxControlID="rBtnGenera">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID = "pnlHead" />
                                <telerik:AjaxUpdatedControl ControlID = "pnlDetail" />
                            </UpdatedControls>
                        </telerik:AjaxSetting>
                        <telerik:AjaxSetting AjaxControlID="rBtnAplica">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID = "pnlHead" />
                                <telerik:AjaxUpdatedControl ControlID = "pnlDetail" />
                            </UpdatedControls>
                        </telerik:AjaxSetting>
                        <telerik:AjaxSetting AjaxControlID="rBtnExportar">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID = "pnlHead" />
                                <telerik:AjaxUpdatedControl ControlID = "pnlDetail" />
                            </UpdatedControls>
                        </telerik:AjaxSetting>
                        <telerik:AjaxSetting AjaxControlID="rBtnExcel">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID = "pnlHead" />
                                <telerik:AjaxUpdatedControl ControlID = "pnlDetail" />
                            </UpdatedControls>
                        </telerik:AjaxSetting>

                        <telerik:AjaxSetting AjaxControlID="rBtnExcel">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID = "rGdvList" />
                            </UpdatedControls>
                        </telerik:AjaxSetting>

                        <telerik:AjaxSetting AjaxControlID="rBtnPolizaImp">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID = "pnlHead" />
                                <telerik:AjaxUpdatedControl ControlID = "pnlDetail" />
                            </UpdatedControls>
                        </telerik:AjaxSetting>
                        <telerik:AjaxSetting AjaxControlID="rBtnLimpiar">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID = "pnlHead" />
                                <telerik:AjaxUpdatedControl ControlID = "pnlDetail" />
                            </UpdatedControls>
                        </telerik:AjaxSetting>
                    </AjaxSettings>  
        </telerik:RadAjaxManager>

        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
            </Scripts>
        </telerik:RadScriptManager>

<%--        <asp:UpdatePanel runat="server">

            <ContentTemplate>--%>

            <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" PostBackControls="rBtnNuevo,rBtnModificar,rBtnEliminar,rBtnArticulos,rBtnDocumentos,rBtnAgrupaciones,rBtnLimpiar,rBtnExportar,rBtnExcel,rGdvList,rGdvListDetalle,rTxtClave,rTxtAbreviatura,rTxtDescripcion,RdDateFecha_Inicio,RdDateFecha_Final,rCboMoneda,pnlHead,pnlDetail" CssClass="demo-container no-bg">
            
                <asp:Panel ID="pnlHead" runat="server">
                            <div style="width:100%; display:table; position:static; background-color:transparent;"> 
		                        <table border="0" style=" border-style:none; width:100%; text-align:left; float:right; background-color:transparent;">
			                        <tr  style="width:100%; vertical-align:middle;">  
                                
                                        <td style=" width:50%; background-color:transparent; vertical-align:middle;">

                                            <asp:Panel ID="pnlBtnsAcciones" CssClass="cspnlBtnsAcciones"  runat="server">
                                                <telerik:RadImageButton ID="rBtnNuevo" runat ="server" Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text =""  OnClick="rBtnNuevo_Click" Visible="false" ></telerik:RadImageButton>
                                                <telerik:RadImageButton ID="rBtnModificar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png"      Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png"    Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"    ToolTip="Modificar"  Text=""  OnClick="rBtnModificar_Click" Visible="false" ></telerik:RadImageButton>
                                                <telerik:RadImageButton ID="rBtnEliminar"    runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"       Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png"     Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text="" OnClick="rBtnEliminar_Click" OnClientClicking="OnClientClic_ConfirmOK" Visible="false" ></telerik:RadImageButton>
                                                <telerik:RadImageButton ID="rBtnArticulos" runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnArticuloDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnArticulo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnArticuloHovered.png"  ToolTip="Articulos L. Precios" Text="" OnClick="rBtnArtLisPre_Click" Visible="false" ></telerik:RadImageButton> 
                                                <telerik:RadImageButton ID="rBtnDocumentos"  runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnDocumentoDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnDocumento.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnDocumentoHovered.png"  ToolTip="Documentos"   OnClick="rBtnDocumentosListaPrecios_Click" Text="" Visible="false" ></telerik:RadImageButton>
                                                <telerik:RadImageButton ID="rBtnAgrupaciones"  runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnAgrupacionDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnAgrupacion.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnAgrupacionHovered.png"  ToolTip="Agrupaciones"  OnClick="rBtnListaP_Agrupaciones_Click" Text="" Visible="false" ></telerik:RadImageButton>      
                                                <telerik:RadImageButton ID="rBtnImportar" runat ="server" Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnImportarListPDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnImportarListP.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnImportarListPHovered.png"    ToolTip="Importar"  Text =""  OnClick="rBtnImportar_Click" Visible="true" ></telerik:RadImageButton>
                                                <telerik:RadImageButton ID="rBtnExportar" runat ="server" Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnExportarListPDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnExportarListP.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnExportarListPHovered.png"    ToolTip="Exportar"  Text =""  OnClick="rBtnExportar_Click" Visible="true"  Value="Html" ></telerik:RadImageButton>
                                                <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"  Text="" OnClick="rBtnLimpiar_Click1" ></telerik:RadImageButton>
                                            </asp:Panel> 

                                        </td> 
                                    </tr>

                                    <tr  style="width:100%; vertical-align:middle;">  
                                        <td style=" width:100%; background-color:transparent; vertical-align:middle;">

                                                        <fieldset id="fldEncabezado" runat="server">


                                <div style="width:100%; display:table; position:static; background-color:transparent;" align="center" >

                                        <table border="0" style=" text-align:left; background-color:transparent ;">
                                            <tr style="height:18px;">
                                                <td style=" background-color:transparent;">
                                                    <telerik:RadLabel ID="rLblClave" runat="server" Text="Clave"></telerik:RadLabel>  
                                                </td>
                                                <td style="  background-color:transparent;">                             
                                                    <%--<telerik:RadTextBox ID="rTxtClave" Width="120px" runat="server"  InputType="Number"
                                                        EnabledStyle-CssClass="cssTxtEnabled"
                                                        DisabledStyle-CssClass ="cssTxtEnabled"
                                                        HoveredStyle-CssClass="cssTxtHovered"
                                                        FocusedStyle-CssClass="cssTxtFocused"
                                                        InvalidStyle-CssClass="cssTxtInvalid" 
                                                     ></telerik:RadTextBox>--%>

                                                    <telerik:RadNumericTextBox ID="rTxtClave" Width="120px" runat="server" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="1" MaxValue="2147483646"
                                                        EnabledStyle-CssClass="cssTxtEnabled"
                                                        DisabledStyle-CssClass ="cssTxtEnabled"
                                                        HoveredStyle-CssClass="cssTxtHovered"
                                                        FocusedStyle-CssClass="cssTxtFocused"
                                                        InvalidStyle-CssClass="cssTxtInvalid" ></telerik:RadNumericTextBox>

                                                </td>
                                                <td style=" background-color:transparent;">
                                                    <telerik:RadLabel ID="rLblAbreviatura" runat="server" Text="Abreviatura"></telerik:RadLabel>  
                                                </td>
                                                <td style="  background-color:transparent;">                             
                                                    <telerik:RadTextBox ID="rTxtAbreviatura" Width="120px" runat="server" MaxLength="10"
                                                        EnabledStyle-CssClass="cssTxtEnabled"
                                                        DisabledStyle-CssClass ="cssTxtEnabled"
                                                        HoveredStyle-CssClass="cssTxtHovered"
                                                        FocusedStyle-CssClass="cssTxtFocused"
                                                        InvalidStyle-CssClass="cssTxtInvalid" 
                                                     ></telerik:RadTextBox>
                                                </td>
                                                <td style=" background-color:transparent;">
                                                    <telerik:RadLabel ID="rLblDescripcion" runat="server" Text="Descripción"></telerik:RadLabel>  
                                                </td>
                                                <td style="  background-color:transparent;">                             
                                                    <telerik:RadTextBox ID="rTxtDescripcion" Width="250px" runat="server" MaxLength="40"
                                                        EnabledStyle-CssClass="cssTxtEnabled"
                                                        DisabledStyle-CssClass ="cssTxtEnabled"
                                                        HoveredStyle-CssClass="cssTxtHovered"
                                                        FocusedStyle-CssClass="cssTxtFocused"
                                                        InvalidStyle-CssClass="cssTxtInvalid" 
                                                     ></telerik:RadTextBox>
                                                </td>
                                            </tr>

                                            <tr style="height:18px;">
                                                <td style=" background-color:transparent;">
                                                    <telerik:RadLabel ID="rLblFecha_Inicio" runat="server" Text="Vig. Inicial"></telerik:RadLabel>  
                                                </td>
                                                <td style="  background-color:transparent;">                             
                                                    <telerik:RadDatePicker ID="RdDateFecha_Inicio" runat="server" Width="120px"    AutoPostBack="true" OnSelectedDateChanged="RdDateFecha_Inicio_SelectedDateChanged" ></telerik:RadDatePicker>
                                                </td>
                                                <td style=" background-color:transparent;">
                                                    <telerik:RadLabel ID="rLblFecha_Final" runat="server" Text="Vig. Final"></telerik:RadLabel>  
                                                </td>
                                                <td style="  background-color:transparent;">                             
                                                    <telerik:RadDatePicker ID="RdDateFecha_Final" runat="server" Width="120px"    AutoPostBack="true" OnSelectedDateChanged="RdDateFecha_Final_SelectedDateChanged" ></telerik:RadDatePicker>
                                                </td>

                                                <td style=" background-color:transparent;">
                                                    <telerik:RadLabel ID="rLblMoneda" runat="server" Text="Moneda"></telerik:RadLabel>  
                                                </td>
                                                <td style=" background-color:transparent;">
                                                    <telerik:RadComboBox ID="rCboMoneda" runat="server" Width="200px" 
                                                                                HighlightTemplatedItems="true"  
                                                                              DropDownCssClass="cssRadComboBox" 
                                                                              DropDownWidth="260px" 
                                                                               Height="200px" >
                                                                                    <HeaderTemplate>
                                                                                        <table style="width: 140px" cellspacing="0" cellpadding="0">
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
                                                                                                <td style="width:45px" >
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "monCve")%>
                                                                                                </td>
                                                                                                <td style="width: 170px;">
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "monDes") %>
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

                                        </td>
                                    </tr>

                                    <tr  style="width:100%; vertical-align:middle;">  
                                        <td style=" width:100%; background-color:transparent; vertical-align:middle;">

                                            <telerik:RadGrid ID="rGdvList"  runat="server" 
                                            AllowMultiRowSelection="true" OnSelectedIndexChanged="rGdvList_SelectedIndexChanged"
                                             OnExcelMLWorkBookCreated="rGdvList_ExcelMLWorkBookCreated" OnItemCreated="rGdvList_ItemCreated" OnHTMLExporting="rGdvList_HTMLExporting" OnItemCommand="rGdvList_ItemCommand" OnBiffExporting="rGdvList_BiffExporting"
                                            AutoGenerateColumns="False"  
                                            Width="100%" Height="320px" 
                                            CssClass="Grid" 
                                            skin="Office2010Silver"  >
                                             <MasterTableView DataKeyNames="lisPreCve"  ClientDataKeyNames="lisPreCve" AutoGenerateColumns="false"  >
                                           
                                                 <Columns>
                                                        <telerik:GridBoundColumn DataField="lisPreCve" UniqueName="lisPreCve"    HeaderText="Clave" HeaderStyle-Width="30px"  ItemStyle-Width="30px"   Display="true"/>
                                                        <telerik:GridBoundColumn DataField="lisPreAbr" UniqueName="lisPreAbr" HeaderText="Abreviatura" HeaderStyle-Width="30px"  ItemStyle-Width="30px"   Display="true" />
                                                        <telerik:GridBoundColumn DataField="lisPreDes" UniqueName="lisPreDes" HeaderText="Descripción" HeaderStyle-Width="60px"  ItemStyle-Width="60px"   Display="true"/>
                                                        <telerik:GridBoundColumn DataField="lisPreIniVigen" UniqueName="lisPreIniVigen" HeaderText="Vigencia Inicial" HeaderStyle-Width="30px"  ItemStyle-Width="30px"   Display="true" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Left"/>
                                                        <telerik:GridBoundColumn DataField="lisPreFinVigen" UniqueName="lisPreFinVigen" HeaderText="Vigencia Final"  HeaderStyle-Width="30px"  ItemStyle-Width="30px"   Display="true" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Left"/>
                                                        <telerik:GridBoundColumn DataField="monDes" UniqueName="monDes" HeaderText="Moneda" HeaderStyle-Width="60px"  ItemStyle-Width="60px"   Display="true"/>
                                                 </Columns>

                                                <NoRecordsTemplate> No se encontraron registros.</NoRecordsTemplate>
                                            </MasterTableView>
                                            <HeaderStyle CssClass="GridHeaderStyle"/>
                                            <ItemStyle CssClass="GridRowStyle"/>
                                            <AlternatingItemStyle  CssClass="GridAlternatingRowStyle" />
                                            <selecteditemstyle CssClass="GridSelectedItem"></selecteditemstyle>
                                            <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true" >
                                                <Selecting AllowRowSelect="true" EnableDragToSelectRows="False" ></Selecting>
                                                <Scrolling AllowScroll="true" UseStaticHeaders="true"  ScrollHeight="330px"/>
                                            </ClientSettings>
                                        </telerik:RadGrid>

                                        </td>
                                    </tr>

                                </table>
                            </div>

                </asp:Panel>



                <asp:Panel ID="pnlDetail" runat="server">
                    
                            <div style="width:100%; display:table; position:inherit; background-color:transparent;"> 
		                            <table border="0" style=" border-style:none; width:100%; text-align:left; float:right; background-color:transparent;">
			                            <tr  style="width:100%; vertical-align:middle;">  
                                
                                            <td style=" width:100%; background-color:transparent; vertical-align:top;">


                                                <asp:Panel ID="pnlBtnsAccionesDetalle" CssClass="cspnlBtnsAcciones"  runat="server" Visible="false">
                                                    <telerik:RadImageButton ID="rBtnNuevoP" runat ="server" Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text =""  OnClick="rBtnNuevoP_Click"></telerik:RadImageButton>
                                                    <telerik:RadImageButton ID="rBtnModificarP"   runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png"      Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png"    Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"    ToolTip="Modificar"  Text=""  OnClick="rBtnModificarP_Click" ></telerik:RadImageButton>
                                                    <telerik:RadImageButton ID="rBtnEliminarP"    runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"       Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png"     Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text="" OnClick="rBtnEliminarP_Click" OnClientClicking="OnClientClic_ConfirmOK" ></telerik:RadImageButton>
                                                    <telerik:RadImageButton ID="rBtnExcelDetalle" runat ="server" Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnExportarListPDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnExportarListP.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnExportarListPHovered.png"    ToolTip="Excel"  Text =""  OnClick="rBtnExportar_Click" Visible="true"  Value="Html" ></telerik:RadImageButton>
                                                    <telerik:RadImageButton ID="rBtnLimpiarP"   runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"  Text="" OnClick="rBtnLimpiarP_Click" ></telerik:RadImageButton>
                                                    

                                                </asp:Panel> 

                                            </td> 


                                        </tr>

                                        <tr  style="width:100%; vertical-align:middle;">  
                                
                                            <td style=" width:100%; background-color:transparent; vertical-align:middle;">

                                                <fieldset> 
                                                   <legend>Lista de Precios</legend>
                                                    <table border="0" style="  text-align:left; background-color:transparent ;">
                                                     <tr > 
                                                         <td style="text-align:left; background-color:transparent">
                                                             <telerik:RadLabel ID="radlb_ClaveLista" runat="server" Text="Moneda" BackColor="transparent" Width="750px" ></telerik:RadLabel> 
                                                         </td>
                                                      </tr>
                                                    </table>
           
            
                                               </fieldset>

                                            </td>






                                       </tr>

                                        <tr  style="width:100%; vertical-align:middle;">  
                                
                                            <td style=" width:100%; background-color:transparent; vertical-align:middle;">

                                                <fieldset id="fldDetalle" runat="server">

                                <legend id="lgdForm" runat="server">Detalle</legend>

                                <table border="0" style=" border-style:none; width:100%; text-align:left; float:right; background-color:transparent;">
                                                <tr>   
                                                    <td style=" width:120px; background-color:transparent; vertical-align:top;">
                                                        <asp:Label ID="Label6" runat="server" Text="Articulo"></asp:Label>

                                                            <telerik:RadComboBox ID="rCboArticulo" runat="server" Width="120px" 
                                                            HighlightTemplatedItems="true"
                                                            DropDownCssClass="cssRadComboBox"  
                                                             OnSelectedIndexChanged="rCboArticulo_SelectedIndexChanged"                                
                                                            DropDownWidth="400px" Height="200px" 
                                                            AutoPostBack="True" >
                                                                        <HeaderTemplate>
                                                                        <table style="width: 400px;"  cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td style="width: 150px;">
                                                                                Clave
                                                                            </td>
                                                            
                                                                            <td style="width: 250px;">
                                                                                Descripcion
                                                                            </td>
                                                         
                                                                        </tr>
                                                                    </table>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                            <table style="width: 400px;"  cellspacing="0" cellpadding="0">
                                                                            <tr>
                                                                                    <td style="width: 150px;">
                                                                                <%# DataBinder.Eval(Container.DataItem, "artCve") %>
                                                                                </td>

                                                                                    <td style="width: 250px;">
                                                                                <%# DataBinder.Eval(Container.DataItem, "artDes") %>
                                                                                </td>

                                                                            </tr>
                                                                        </table>

                                                                    </ItemTemplate>

                                                                    <FooterTemplate>
                                                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                                    </FooterTemplate>
                                                        </telerik:RadComboBox>

                                                    </td>
                                                                                

                                                    <td style=" width:70px; background-color:transparent; vertical-align:top;">

                                                        <asp:Label ID="Label1" runat="server" Text="Minimo"></asp:Label>
                                                                                
                                                        <telerik:RadNumericTextBox runat="server" ID="rTxtMinimo" Width="100px" Value="1"
                                                            EnabledStyle-CssClass="cssTxtEnabled"
                                                        DisabledStyle-CssClass ="cssTxtEnabled"
                                                        HoveredStyle-CssClass="cssTxtHovered"
                                                        FocusedStyle-CssClass="cssTxtFocused"
                                                        InvalidStyle-CssClass="cssTxtInvalid" 
                                                             OnTextChanged="rTxtMinimo_TextChanged" AutoPostBack="true" Enabled="false" 
                                                             MinValue="0" ShowSpinButtons="false" NumberFormat-DecimalDigits="2" Type="Number"></telerik:RadNumericTextBox>
                                                    </td>

                                                    <td style=" width:70px; background-color:transparent; vertical-align:top;">
                                                        <asp:Label ID="Label8" runat="server" Text="Maximo"></asp:Label>
                                                                                
                                                        <telerik:RadNumericTextBox runat="server" ID="rTxtMaximo" Width="100px" Value="1" 
                                                            EnabledStyle-CssClass="cssTxtEnabled"
                                                        DisabledStyle-CssClass ="cssTxtEnabled"
                                                        HoveredStyle-CssClass="cssTxtHovered"
                                                        FocusedStyle-CssClass="cssTxtFocused"
                                                        InvalidStyle-CssClass="cssTxtInvalid" Enabled="false"
                                                             OnTextChanged="rTxtMaximo_TextChanged" AutoPostBack="true"
                                                             MinValue="0" ShowSpinButtons="false" NumberFormat-DecimalDigits="2" Type="Number"></telerik:RadNumericTextBox>
                                                    </td>
                                                    <td style=" width:50px; background-color:transparent; vertical-align:top;">
                                                        <asp:Label ID="Label11" runat="server" Text="Precio"></asp:Label>
                                                                                
                                                        <telerik:RadNumericTextBox runat="server" ID="rTxtPartPrec" Width="100px" Value="1" 
                                                            EnabledStyle-CssClass="cssTxtEnabled"
                                                        DisabledStyle-CssClass ="cssTxtEnabled"
                                                        HoveredStyle-CssClass="cssTxtHovered"
                                                        FocusedStyle-CssClass="cssTxtFocused"
                                                        InvalidStyle-CssClass="cssTxtInvalid" 
                                                            EmptyMessage="Enter units count" MinValue="0" ShowSpinButtons="false" NumberFormat-DecimalDigits="2" Type="Currency"></telerik:RadNumericTextBox>
                                                    </td>

                                                    <td style=" width:120px; background-color:transparent; vertical-align:top;">
                                                        <asp:Label ID="Label2" runat="server" Text="Descuento"></asp:Label><br />

                                                            <telerik:RadComboBox ID="rCboDescuentos" runat="server" Width="120px" 
                                                            HighlightTemplatedItems="true"
                                                            DropDownCssClass="cssRadComboBox"  
                                                            AllowCustomText="true"                                
                                                            DropDownWidth="250px" Height="200px" 
                                                            AutoPostBack="True" >
                                                                        <HeaderTemplate>
                                                                        <table style="width: 250px;"  cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td style="width: 50px;">
                                                                                Clave
                                                                            </td>
                                                            
                                                                            <td style="width: 200px;">
                                                                                Descripcion
                                                                            </td>
                                                         
                                                                        </tr>
                                                                    </table>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                            <table style="width: 250px;"  cellspacing="0" cellpadding="0">
                                                                            <tr>
                                                                                    <td style="width: 50px;">
                                                                                <%# DataBinder.Eval(Container.DataItem, "desCve") %>
                                                                                </td>

                                                                                    <td style="width: 200px;">
                                                                                <%# DataBinder.Eval(Container.DataItem, "desDes") %>
                                                                                </td>

                                                                            </tr>
                                                                        </table>

                                                                    </ItemTemplate>

                                                                    <FooterTemplate>
                                                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                                    </FooterTemplate>
                                                        </telerik:RadComboBox>

                                                    </td>



                                                    <td style=" width:120px; background-color:transparent; vertical-align:top;">
                                                        <asp:Label ID="Label3" runat="server" Text="Impuesto"></asp:Label><br />

                                                            <telerik:RadComboBox ID="rCboImpuestos" runat="server" Width="120px" 
                                                            HighlightTemplatedItems="true"
                                                            DropDownCssClass="cssRadComboBox"  
                                                            AllowCustomText="true"     
                                                            DropDownWidth="250px" Height="200px" 
                                                            AutoPostBack="True" >
                                                                        <HeaderTemplate>
                                                                        <table style="width: 250px;"  cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td style="width: 50px;">
                                                                                Clave
                                                                            </td>
                                                            
                                                                            <td style="width: 200px;">
                                                                                Descripcion
                                                                            </td>
                                                         
                                                                        </tr>
                                                                    </table>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                            <table style="width: 250px;"  cellspacing="0" cellpadding="0">
                                                                            <tr>
                                                                                    <td style="width: 50px;">
                                                                                <%# DataBinder.Eval(Container.DataItem, "impCve") %>
                                                                                </td>

                                                                                    <td style="width: 200px;">
                                                                                <%# DataBinder.Eval(Container.DataItem, "impDes") %>
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
                           

                            
                            </fieldset>

                                            </td>
                                        </tr>

                                        <tr  style="width:100%; vertical-align:middle;">  
                                
                                            <td style=" width:100%; background-color:transparent; vertical-align:middle;">

                                                <telerik:RadGrid ID="rGdvListDetalle" Visible="false" 
                                                    runat="server"
                                                    AllowMultiRowSelection="true"
                                                    OnSelectedIndexChanged="rGdvListDetalle_SelectedIndexChanged"
                                                    AutoGenerateColumns="False" 
                                                    Width="100%" Height="249px" 
                                                    CssClass="Grid"
                                                    skin="Office2010Silver"  >
                                                    <MasterTableView   DataKeyNames="lisPreSec" AutoGenerateColumns="false"  CssClass="GridTable"  >
         
                                            <Columns>
                                                    
                                                    <telerik:GridBoundColumn DataField="lisPreSec"    HeaderText="Sec."         HeaderStyle-Width="15px"  ItemStyle-Width="15px"   Display="true"/>
                                                    <telerik:GridBoundColumn DataField="artCve"  HeaderText="Articulo"              HeaderStyle-Width="60px"  ItemStyle-Width="60px"   Display="true" />
                                                    <telerik:GridBoundColumn DataField="artDes"  HeaderText="Descripcion"              HeaderStyle-Width="70px"  ItemStyle-Width="70px"   Display="true" />
                                                    <telerik:GridBoundColumn DataField="lisPreCanMin" HeaderText="Minimo" DataFormatString="{0:###,##0.00}" HeaderStyle-Width="30px"  ItemStyle-Width="30px"   Display="true"/>
                                                    <telerik:GridBoundColumn DataField="lisPreCanMax"  HeaderText="Maximo" DataFormatString="{0:###,##0.00}" HeaderStyle-Width="30px"  ItemStyle-Width="30px"  Display="true" />
                                                    <telerik:GridBoundColumn DataField="lisPrecio"  HeaderText="Precio"  DataFormatString="{0:###,##0.00}" HeaderStyle-Width="30px"  ItemStyle-Width="30px"   Display="true"/>
                                                    <telerik:GridBoundColumn DataField="desDes"    HeaderText="Descuento"         HeaderStyle-Width="30px"  ItemStyle-Width="30px"   Display="true"/>
                                                    <telerik:GridBoundColumn DataField="impDes"    HeaderText="Impuesto"         HeaderStyle-Width="30px"  ItemStyle-Width="30px"   Display="true"/>
                                                
                                                    <telerik:GridBoundColumn DataField="DesCve"    HeaderText="DescuentoCve"         HeaderStyle-Width="30px"  ItemStyle-Width="30px"   Display="false"/>
                                                    <telerik:GridBoundColumn DataField="impCve"    HeaderText="ImpuestoCve"         HeaderStyle-Width="30px"  ItemStyle-Width="30px"   Display="false"/>

                                                </Columns>
                                            <NoRecordsTemplate> No se encontraron registros.</NoRecordsTemplate>
                                            </MasterTableView>
                                            <HeaderStyle CssClass="GridHeaderStyle"/>
                                            <ItemStyle CssClass="GridRowStyle"/>
                                            <AlternatingItemStyle  CssClass="GridAlternatingRowStyle" />
                                            <selecteditemstyle CssClass="GridSelectedItem"></selecteditemstyle>
 
                                            <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true" >
                                                <Selecting AllowRowSelect="true"  EnableDragToSelectRows="False" ></Selecting>
                                                <Scrolling AllowScroll="true" UseStaticHeaders="true"  ScrollHeight="330px"    />
                                            
                                        </ClientSettings>
                                    </telerik:RadGrid>

                                            </td>
                                        </tr>

                                    </table>
                                    <telerik:RadWindow runat="server"   ID="rWindowsExportarcion" Width="480px" Height="500" Modal="true" VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close"  >               
                                    </telerik:RadWindow>
                                </div>

                </asp:Panel>

                <asp:HiddenField ID="hdfBtnAccionDet" runat="server" />
                <asp:HiddenField ID="hdfPag_sOpe" runat="server" /> 
                <asp:HiddenField ID="hdfBtnAccion" runat="server" />
        
                <asp:HiddenField ID="opcTabStc" runat="server" />
                <asp:HiddenField ID="cveTabStc" runat="server" />
                <asp:HiddenField ID="arregloImagen"   runat="server" />

                <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
                </telerik:RadWindowManager>


            <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
                <telerik:RadImageButton ID="rBtnGuardar"  runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text =""    OnClick="rBtnGuardar_Click"  OnClientClicking ="OnClientClic_ConfirmOK" ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text=""   OnClick="rBtnCancelar_Click" OnClientClicking="OnClientClic_ConfirmOK"  ></telerik:RadImageButton>
            </asp:Panel>  
            <telerik:RadWindow runat="server" ID="RwExportar"  NavigateUrl="ExportaLP.aspx" OnClientClose="closeRadWindow"  Width="330px" Height="290px" Modal="true" VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close" >
            </telerik:RadWindow>

        </telerik:RadAjaxPanel>
        <%-- </ContentTemplate>
        </asp:UpdatePanel>--%>


    </form>
</body>
</html>

