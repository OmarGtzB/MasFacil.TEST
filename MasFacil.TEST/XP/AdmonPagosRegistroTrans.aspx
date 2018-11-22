<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdmonPagosRegistroTrans.aspx.cs" Inherits="XP_AdmonPagosRegistroTrans" %>
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
            GetRadWindow().close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">



    <div>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                 <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                    
                    

                
	                <div style="width:100%; display:table; position:static; background-color:transparent;"> 
		                <table border="0" style=" border-style:none; width:100%; text-align:left; float:right; background-color:transparent;">
			                <tr  style="width:100%; vertical-align:middle;">  
                                
                                <td style=" width:80%; background-color:transparent; vertical-align:middle;">


                                    <asp:Panel ID="pnlBtnsAcciones" CssClass="cspnlBtnsAcciones"  runat="server">
                                        <telerik:RadImageButton ID="rBtnNuevo" runat ="server" Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text =""  OnClick="rBtnNuevo_Click"></telerik:RadImageButton>
                                        <telerik:RadImageButton ID="rBtnBuscar" runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnRastreoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnRastreo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnRastreoHovered.png"  ToolTip="Buscar"  Text=""  OnClick="rBtnBuscar_Click" ></telerik:RadImageButton>
                        
                                        <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"  Text="" OnClick="rBtnLimpiar_Click" ></telerik:RadImageButton>

                                    </asp:Panel> 

                                </td> 

                                <td style=" width:30px; background-color:transparent; vertical-align:middle;">

                                    <asp:Label ID="lblApli" runat="server" Text="Aplicar" Visible="false"></asp:Label>

                                </td>

				                <td style=" width:100px; background-color:transparent; vertical-align:middle;">
					                
                                    <telerik:RadNumericTextBox runat="server" Visible="false" 
                                         Height="20px"
                                        ID="rTxtImpApli" Width="120px" Value="1" EmptyMessage="Enter units count" MinValue="0" ShowSpinButtons="false" NumberFormat-DecimalDigits="2" Type="Currency" EnabledStyle-HorizontalAlign="Right"></telerik:RadNumericTextBox>
                                    
				                </td>
				                <td style=" width:40px; background-color:transparent; vertical-align:middle;">
                                     
                                        <telerik:RadImageButton ID="rImgBtnAceptarP" Visible="false" runat ="server" Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnAutorizarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnAutorizar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnAutorizarHovered.png"    ToolTip="Nuevo"  Text ="" OnClick="rImgBtnAceptarP_Click">
                                        </telerik:RadImageButton>
                                    
                                </td>
                            </tr>
                        </table>
                    </div>
                


                    <div runat="server" id="divForm" visible="true">   

                        <fieldset style="height:317px">
                            <legend>Transaccion Detalle</legend>
                                <div style="overflow:auto; width:800px; height:250px; margin-left:1px; background-color:Transparent;" >
                             


                                        <table style=" background-color:Transparent;" cellspacing="0" cellpadding="0" >
                                             <tr style="height:26px;">
                                                 <td style="width:180px; background-color:transparent;">
                                                    <telerik:RadLabel ID="rlblMoneda" runat="server" Text="Moneda" BackColor="Transparent"></telerik:RadLabel>
                                                 </td>
                                                 <td style="width: 205px; background-color:transparent; ">
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
                                     <asp:DataList ID="DataList1" runat="server" DataKeyField="columTrans" Width="780px">
                                       <ItemTemplate>
                                        <table style="cellspacing="0" cellpadding="0" >
                                             <tr style="height:26px;">
                                                 <td style="width:180px; height:26px; background-color:transparent; vertical-align:top; text-align:left ">
                                                        <telerik:RadLabel ID="RadLabel1" runat="server" Text='<%# Eval("cptoConfDes") %>'  ></telerik:RadLabel><br />
                                                 </td>
                                                        <td  style="width: 205px; height:26px; background-color:transparent; vertical-align:central; text-align:right  "  >
                                                            <telerik:RadTextBox ID="RadTextBox" runat="server"  Width="200px"
                                                                    EnabledStyle-CssClass="cssTxtEnabled"
                                                                    DisabledStyle-CssClass ="cssTxtEnabled"
                                                                    HoveredStyle-CssClass="cssTxtHovered"
                                                                    FocusedStyle-CssClass="cssTxtFocused"
                                                                    InvalidStyle-CssClass="cssTxtInvalid"
                                                                OnTextChanged="CadenaAlineaValor_TextChanged"
                                                            >
                                                            </telerik:RadTextBox>
                                                            <telerik:RadComboBox ID="RadComboBox" runat="server" Width="200px" AutoPostBack="false"
                                                                        AllowCustomText="true"  
                                                                        HighlightTemplatedItems="true" 
                                                                        DropDownCssClass="cssRadComboBox"
                                                                        DropDownWidth="260"   
                                                                        Height="200px" Filter="StartsWith">
                                                                <HeaderTemplate>
                                                                <table style="width: 250px" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td style="width: 150px;">
                                                                                Clave
                                                                            </td>
                                                                            <td style="width: 250px;">
                                                                                Descripción
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                            </HeaderTemplate>
                                                                <ItemTemplate   >
                                                                 <table style="width: 250px;"  cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                        <td style="width:150px" >
                                                                            <%# DataBinder.Eval(Container.DataItem, "Clave")%>
                                                                        </td>
                                                                        <td style="width: 250px;">
                                                                            <%# DataBinder.Eval(Container.DataItem, "Descripcion") %>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                              <FooterTemplate>
                                                                <asp:Literal runat="server" ID="RadComboItemsCount"  />
                                                            </FooterTemplate>
                                                           </telerik:RadComboBox>
                                                            <telerik:RadNumericTextBox ID="RadNumericTextBox" runat="server"   Width="200px"
                                                                EnabledStyle-CssClass="cssTxtEnabled"
                                                                DisabledStyle-CssClass ="cssTxtEnabled"
                                                                HoveredStyle-CssClass="cssTxtHovered"
                                                                FocusedStyle-CssClass="cssTxtFocused"
                                                                InvalidStyle-CssClass="cssTxtInvalid">
                                                            </telerik:RadNumericTextBox>
                                                            <telerik:RadDatePicker ID="RadDatePicker" runat="server" Width="130px" AutoPostBack="false" ></telerik:RadDatePicker>

                                                            <telerik:RadCheckBox ID="RadCheckBox_TipoCaptura" runat="server" Text="RadCheckBox" Checked='<%# Eval("cptoTipCap") %>' Visible="false" ></telerik:RadCheckBox>
                                                            <telerik:RadCheckBox ID="RadCheckBox_ProgValida" runat="server" Text='<%# Eval("cptoConfProgCveVal") %>' Checked='<%# Eval("ProgCveValCheck") %>' Value='<%# Eval("cptoConfProgCveVal") %>' Visible="false"></telerik:RadCheckBox>
                                                            <telerik:RadCheckBox ID="RadCheckBox_Justificacion" runat="server" Text='<%# Eval("cptoConfRell") %>' Checked='<%# Eval("CheckJustIzq") %>' Value='<%# Eval("cptoConfRell") %>'  Visible="false"></telerik:RadCheckBox>
                                                       </td>
                                            </tr>
                                        </table>
                                       </ItemTemplate>
                                   </asp:DataList>



                               
                               </div>


                        </fieldset>

                    </div>

                    <div id="divGrid" runat="server" visible="false">

                        <fieldset style="">
                            <legend>Pagos Pendientes</legend>



                            <telerik:RadGrid ID="rGdvPagosPend" 
                                                    runat="server"
                                 AllowMultiRowSelection="true"
                                 OnSelectedIndexChanged="rGdvPagosPend_SelectedIndexChanged"
                                                    AutoGenerateColumns="False" 
                                                    Width="100%" Height="298px" 
                                                    CssClass="Grid"
                                                    skin="Office2010Silver"  >
                                                    <MasterTableView   DataKeyNames="partPenPagId" AutoGenerateColumns="false"  CssClass="GridTable"  >
         
                                            <Columns >
                                                    
                                                    <telerik:GridBoundColumn DataField="movRef10_Princ"    HeaderText="Referencia"         HeaderStyle-Width="50px"  ItemStyle-Width="50px"   Display="true"/>
                                                    <telerik:GridBoundColumn DataField="partPenPagFecMov"  HeaderText="Fecha"              HeaderStyle-Width="50px"  ItemStyle-Width="50px"   Display="true" DataFormatString="{0:d}"/>
                                                    <telerik:GridBoundColumn DataField="partPenPagFecProg" HeaderText="Fecha Prog."        HeaderStyle-Width="50px"  ItemStyle-Width="50px"   Display="true" DataFormatString="{0:d}"/>
                                                    <telerik:GridBoundColumn DataField="partPenPagImpAbo"  HeaderText="Importe"            HeaderStyle-Width="50px"  ItemStyle-Width="50px"   Display="true" DataFormatString="{0:###,##0.00}" ItemStyle-HorizontalAlign="Right"/>
                                                    <telerik:GridBoundColumn DataField="partPenPagImpAbo"  HeaderText="Importe a Aplicar"  HeaderStyle-Width="50px"  ItemStyle-Width="50px"   Display="true" DataFormatString="{0:###,##0.00}" ItemStyle-HorizontalAlign="Right"/>
                                                    
                                                    <telerik:GridBoundColumn DataField="provCve"    HeaderText="Proveedor" HeaderStyle-Width="50px"  ItemStyle-Width="50px"   Display="true"/>
                                                    <telerik:GridBoundColumn DataField="partPenPagFecVenc" HeaderText="Fecha Venc." HeaderStyle-Width="50px"  ItemStyle-Width="50px"   Display="true" DataFormatString="{0:d}"/>
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





                        </fieldset>

                    </div>

                    
                           <br />



                        <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
                            <telerik:RadImageButton ID="rBtnGuardar"  runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text =""    OnClick="rBtnGuardar_Click"  OnClientClicking ="OnClientClic_ConfirmOK" ></telerik:RadImageButton>
                            <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text=""   OnClick="rBtnCancelar_Click" OnClientClicking="OnClientClic_ConfirmOK"  ></telerik:RadImageButton>
                        </asp:Panel>  

                        <asp:HiddenField ID="hdfBtnAccionDet" runat="server" />
                        <asp:HiddenField ID="hdfPag_sOpe" runat="server" /> 
                     <asp:HiddenField ID="hdfBtnAccion" runat="server" />
                <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
                </telerik:RadWindowManager>



            </ContentTemplate>
        </asp:UpdatePanel>
    
    </div>
    </form>
</body>
</html>
