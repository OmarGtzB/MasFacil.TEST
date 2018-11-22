<%@ Page Title="" Language="C#" MasterPageFile="~/MPForm.master" AutoEventWireup="true" CodeFile="RegOperacionesDet.aspx.cs" Inherits="DC_RegOperacionesDet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMPForm" Runat="Server">

    <script src="http://code.jquery.com/jquery-1.11.0.min.js"></script>
    
    <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
    <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
 
    <link href="../css/styles.css" rel="stylesheet" type="text/css" />
    <link href="/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />

  <script language="javascript" type="text/javascript">


      <%--function OnClientClose(sender, args) {
            var masterTable = $find("<%= rGdvOperacionesTrans.ClientID %>").get_masterTableView();
          
          }--%>


        //function CloseAndRebind(args) {
        //    GetRadWindow().BrowserWindow.refreshGrid(args);
        //    GetRadWindow().close();
        //}

        //function GetRadWindow() {
        //    var oWindow = null;
        //    if (window.radWindow) oWindow = window.radWindow;
        //    else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
        //    return oWindow;
        //}
        function closeRadWindow()  
        {  
           $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest(); 
            

           //var x = document.getElementById('<%=hdfBtnAccionDet.ClientID%>').value
            //var x = document.getElementById('ContentPlaceHolderMPForm_hdfBtnAccionDet').value;
            //alert(x);
        }  


        
    </script>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMPForm" Runat="Server">

<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest" DefaultLoadingPanelID="RadAjaxLoadingPanel1" >  

                    <AjaxSettings>  

                         <telerik:AjaxSetting AjaxControlID="rGdvOperacionesTrans">  
                            <UpdatedControls>  
                                <telerik:AjaxUpdatedControl ControlID="rGdvOperacionesTrans" />  
                                <telerik:AjaxUpdatedControl ControlID = "hdf_transDetSec" /> 
                            </UpdatedControls>  
                        </telerik:AjaxSetting>

                        <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">  
                            <UpdatedControls>  
                                <telerik:AjaxUpdatedControl ControlID="rGdvOperacionesTrans" />  
                                 <telerik:AjaxUpdatedControl ControlID="rGdvOperacionesMovCC" />   
                            </UpdatedControls>  
                         </telerik:AjaxSetting> 

                         <telerik:AjaxSetting AjaxControlID="rGdvOperacionesMovCC">  
                            <UpdatedControls>  
                                <telerik:AjaxUpdatedControl ControlID="rGdvOperacionesMovCC" />   
                            </UpdatedControls>  
                        </telerik:AjaxSetting>
                          
                         <telerik:AjaxSetting AjaxControlID="rBtnNuevoDet">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID = "pnlBody" />
                                <telerik:AjaxUpdatedControl ControlID = "TextCombos" /> 
                                <telerik:AjaxUpdatedControl ControlID = "rWinRegOpeDet_MovCC" /> 
                                <telerik:AjaxUpdatedControl ControlID = "rWinRegOpeDet_MovXP" /> 
                                <telerik:AjaxUpdatedControl ControlID = "rWinRegOpeDet_MovIN" /> 
                            </UpdatedControls>
                        </telerik:AjaxSetting>

                        <telerik:AjaxSetting AjaxControlID="rBtnModificarDet">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID = "pnlBody" />
                                <telerik:AjaxUpdatedControl ControlID = "TextCombos" /> 
                                <telerik:AjaxUpdatedControl ControlID = "rWinRegOpeDet_MovCC" />
                                <telerik:AjaxUpdatedControl ControlID = "rWinRegOpeDet_MovXP" /> 
                                <telerik:AjaxUpdatedControl ControlID = "rWinRegOpeDet_MovIN" />  
                            </UpdatedControls>
                        </telerik:AjaxSetting>

                        <telerik:AjaxSetting AjaxControlID="rBtnGuardar">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID = "pnlBody" />
                                <telerik:AjaxUpdatedControl ControlID = "TextCombos" /> 
                                <telerik:AjaxUpdatedControl ControlID = "rWinRegOpeDet_MovCC" />
                                <telerik:AjaxUpdatedControl ControlID = "rWinRegOpeDet_MovXP" /> 
                                <telerik:AjaxUpdatedControl ControlID = "rWinRegOpeDet_MovIN" />  
                            </UpdatedControls>
                        </telerik:AjaxSetting>

                        <telerik:AjaxSetting AjaxControlID="rBtnCancelar">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID = "pnlBody" />
                                <telerik:AjaxUpdatedControl ControlID = "TextCombos" /> 
                                <telerik:AjaxUpdatedControl ControlID = "rWinRegOpeDet_MovCC" />
                                <telerik:AjaxUpdatedControl ControlID = "rWinRegOpeDet_MovXP" /> 
                                <telerik:AjaxUpdatedControl ControlID = "rWinRegOpeDet_MovIN" />  
                            </UpdatedControls>
                        </telerik:AjaxSetting>

                        <telerik:AjaxSetting AjaxControlID="rBtnEliminarDet">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID = "pnlBody" />
                            </UpdatedControls>
                        </telerik:AjaxSetting>
                        
                    </AjaxSettings>  

        </telerik:RadAjaxManager>


<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Silk"></telerik:RadAjaxLoadingPanel>


<%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
        <div>


            <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none; width:100%; text-align:left; background-color:transparent ;">
                <tr  style="width:100%;">

                    <td style=" width:100%; background-color:transparent; vertical-align:top;">
                        
                        <fieldset style="">
                             
                            <div style="width:100%; display:table; position:static; background-color:transparent;" align="center" > 

                           
                            <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none;   text-align:left; background-color:transparent;">
                                <tr   >                              

                                    <td style=" width:75px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label5" runat="server" Text="Concepto"></asp:Label>
                                    </td>
                                    <td style=" width:240px; background-color:transparent; vertical-align:top;">
                
                                         <telerik:RadComboBox ID="rCboConcepto" runat="server" Width="200px" AutoPostBack="true"  OnSelectedIndexChanged="rCboConcepto_SelectedIndexChanged" 
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


                                    <td style=" width:60px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label8" runat="server" Text="Fecha"></asp:Label>
                                    </td>
                                    <td style=" width:120px; background-color:transparent; vertical-align:top;">
                                         <telerik:RadDatePicker ID="RdDateFecha" runat="server" Width="105px" AutoPostBack="true" OnSelectedDateChanged="RdDateFecha_SelectedDateChanged"  ></telerik:RadDatePicker>
                                     </td>

                                    <td style=" width:90px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label7" runat="server" Text="Descripción"></asp:Label>
                                    </td>

                                    <td style=" width:335px; background-color:transparent; vertical-align:top;">
                                        <telerik:RadTextBox ID="rTxtDesc" runat="server"  Width="310px"
                                            EnabledStyle-CssClass="cssTxtEnabled"
                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                            HoveredStyle-CssClass="cssTxtHovered"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid" 
                                         ></telerik:RadTextBox>

                                    </td>

                                    <td style=" width:80px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="lblTipoReg" runat="server" Text="Registro"></asp:Label>
                                    </td>
                                    <td style=" width:125px; background-color:transparent; vertical-align:top;">
                                         <asp:RadioButton ID="rBtnRegTransaccion" runat="server" GroupName="x"  Text="Transacción" OnCheckedChanged="rBtnRegTransaccion_CheckedChanged" Checked="true" AutoPostBack="true"  />
                                    </td>
                                    <td style=" width:125px; background-color:transparent; vertical-align:top;">
                                         <asp:RadioButton ID="rBtnRegMovimiento"  runat="server"   GroupName="x" Text="Movimiento"  OnCheckedChanged="rBtnRegMovimiento_CheckedChanged" Checked="false"   AutoPostBack="true" />
                                    </td>


                                </tr>
                            </table>                          


                            </div>
                        </fieldset>

                    </td>

                </tr>
            </table>

            <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none; width:100%; text-align:left; background-color:transparent ;">
                <tr  style="width:100%;">

                    <td style=" width:100%; background-color:transparent; vertical-align:top;">
                        
                        <fieldset style="">
                             
                            <div style="width:100%; display:table; position:static; background-color:transparent;" align="center" > 

                            <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none;   text-align:left; background-color:transparent;">
                                <tr   >                              



                                    <td style=" width:75px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label1" runat="server" Text="Folio"></asp:Label>
                                    </td>
                                    <td style=" width:170px; background-color:transparent; vertical-align:top;">

                                        <telerik:RadTextBox ID="rTxtFolio" runat="server"  Width="100px" 
                                            EnabledStyle-CssClass="cssTxtEnabled"
                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                            HoveredStyle-CssClass="cssTxtHovered"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid" 
                                            OnTextChanged="rTxtFolio_TextChanged"
                                            AutoPostBack ="true"
                                         ></telerik:RadTextBox>

                                    </td>


                                    <td style=" width:130px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label2" runat="server" Text="Asiento Contable"></asp:Label>
                                    </td>
                                    <td style=" width:120px; background-color:transparent; vertical-align:top;">
                                        <telerik:RadTextBox ID="rTxtAsientoCont" runat="server"  Width="100px"
                                            EnabledStyle-CssClass="cssTxtEnabled"
                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                            HoveredStyle-CssClass="cssTxtHovered"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid"
                                            OnTextChanged="rTxtAsientoCont_TextChanged" 
                                            AutoPostBack ="true"
                                         ></telerik:RadTextBox>
                                     </td>

                                    <td style=" width:90px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label3" runat="server" Text="Libro" Visible="false"></asp:Label>
                                    </td>

                                    <td style=" width:335px; background-color:transparent; vertical-align:top;">
                
                                         <telerik:RadComboBox ID="rCboLibro" runat="server" Width="200px" AutoPostBack="true" Visible="false"
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

                                    <td style=" width:80px; background-color:transparent; vertical-align:top;">
                                      
                                    </td>
                                    <td style=" width:125px; background-color:transparent; vertical-align:top;">
                                       
                                    </td>
                                    <td style=" width:125px; background-color:transparent; vertical-align:top;">
                                    
                                    </td>


                                </tr>
                            </table>                          

                            </div>
                        </fieldset>

                    </td>

                </tr>
            </table>

        <asp:Panel ID="pnlBody" runat="server" >
            <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none; width:100%; text-align:left; background-color:transparent ;">
                <tr  style="width:100%;">

                    <td style=" width:1200px; background-color:transparent; vertical-align:top;">

                        <fieldset style="">
                            <legend>Detalle</legend>
                            <div style="width:100%; height:310px;  display:table; position:static; background-color:transparent;" align="center" > 

                                <div>


                                    <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
                                        <telerik:RadImageButton ID="rBtnNuevoDet"     runat ="server" Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"               OnClick="rBtnNuevoDet_Click"  ToolTip="Nuevo"  Text ="" ></telerik:RadImageButton>
                                        <telerik:RadImageButton ID="rBtnModificarDet" runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"    OnClick="rBtnModificarDet_Click" ToolTip="Modificar"  Text=""  ></telerik:RadImageButton>
                                        <telerik:RadImageButton ID="rBtnEliminarDet"  runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"   ToolTip ="Eliminar"    OnClick="rBtnEliminarDet_Click"  Text="" OnClientClicking="OnClientClic_ConfirmOK" ></telerik:RadImageButton>

                                        <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"   OnClick="rBtnLimpiar_Click"  Text=""  ></telerik:RadImageButton>
                                      </asp:Panel>     

                                    <telerik:RadGrid ID="rGdvOperacionesTrans"  OnSelectedIndexChanged="rGdvOperacionesTrans_SelectedIndexChanged"
                                                runat="server"
                                                AutoGenerateColumns="false" 
                                                Width="1245px" Height="290px" 
                                                CssClass="Grid"
                                                skin="Office2010Silver">
                                                <MasterTableView   DataKeyNames="transDetId" AutoGenerateColumns="false"  CssClass="GridTable"  >
                                                <Columns >
                                                    <telerik:GridBoundColumn DataField="transDetId"        HeaderText="transDetId"        HeaderStyle-Width="50px"  ItemStyle-Width="50px"   Display="false"/>
                                                    <telerik:GridBoundColumn DataField="transId"           HeaderText="transId"           HeaderStyle-Width="50px"  ItemStyle-Width="50px"   Display="false"/>

                                                    <telerik:GridBoundColumn DataField="ciaCve"            HeaderText="ciaCve"            HeaderStyle-Width="50px"  ItemStyle-Width="50px"   Display="false"/>
                                                    <telerik:GridBoundColumn DataField="cptoId"            HeaderText="cptoId"            HeaderStyle-Width="50px"  ItemStyle-Width="50px"   Display="false"/>
                                                    <telerik:GridBoundColumn DataField="transDetSec"       HeaderText="Sec."              HeaderStyle-Width="50px"  ItemStyle-Width="50px"   Display="true"/>
                                                    <telerik:GridBoundColumn DataField="transDetStr10_01"  HeaderText="transDetStr10_01"  HeaderStyle-Width="110px" ItemStyle-Width="110px"  Display="false"/>
                                                    <telerik:GridBoundColumn DataField="transDetStr10_02"  HeaderText="transDetStr10_02"  HeaderStyle-Width="110px" ItemStyle-Width="110px"  Display="false"/>
                                                    <telerik:GridBoundColumn DataField="transDetStr10_03"  HeaderText="transDetStr10_03"  HeaderStyle-Width="110px" ItemStyle-Width="110px"  Display="false"/>
                                                    <telerik:GridBoundColumn DataField="transDetStr10_04"  HeaderText="transDetStr10_04"  HeaderStyle-Width="110px" ItemStyle-Width="110px"  Display="false"/>
                                                    <telerik:GridBoundColumn DataField="transDetStr10_05"  HeaderText="transDetStr10_05"  HeaderStyle-Width="110px" ItemStyle-Width="110px"  Display="false"/>
                                                    <telerik:GridBoundColumn DataField="transDetStr10_06"  HeaderText="transDetStr10_06"  HeaderStyle-Width="110px" ItemStyle-Width="110px"  Display="false"/>
                                                    <telerik:GridBoundColumn DataField="transDetStr10_07"  HeaderText="transDetStr10_07"  HeaderStyle-Width="110px" ItemStyle-Width="110px"  Display="false"/>
                                                    <telerik:GridBoundColumn DataField="transDetStr10_08"  HeaderText="transDetStr10_08"  HeaderStyle-Width="110px" ItemStyle-Width="110px"  Display="false"/>
                                                    <telerik:GridBoundColumn DataField="transDetStr10_09"  HeaderText="transDetStr10_09"  HeaderStyle-Width="110px" ItemStyle-Width="110px"  Display="false"/>
                                                    <telerik:GridBoundColumn DataField="transDetStr10_10"  HeaderText="transDetStr10_10"  HeaderStyle-Width="110px" ItemStyle-Width="110px"  Display="false"/>
                                                    <telerik:GridBoundColumn DataField="transDetStr20_01"  HeaderText="transDetStr20_01"  HeaderStyle-Width="160px" ItemStyle-Width="160px"  Display="false"/>
                                                    <telerik:GridBoundColumn DataField="transDetStr20_02"  HeaderText="transDetStr20_02"  HeaderStyle-Width="160px" ItemStyle-Width="160px"  Display="false"/>
                                                    <telerik:GridBoundColumn DataField="transDetStr20_03"  HeaderText="transDetStr20_03"  HeaderStyle-Width="160px" ItemStyle-Width="160px"  Display="false"/>
                                                    <telerik:GridBoundColumn DataField="transDetStr20_04"  HeaderText="transDetStr20_04"  HeaderStyle-Width="160px" ItemStyle-Width="160px"  Display="false"/>
                                                    <telerik:GridBoundColumn DataField="transDetStr20_05"  HeaderText="transDetStr20_05"  HeaderStyle-Width="160px" ItemStyle-Width="160px"  Display="false"/>
                                                    <telerik:GridBoundColumn DataField="transDetStr40_01"  HeaderText="transDetStr40_01"  HeaderStyle-Width="160px" ItemStyle-Width="160px"  Display="false"/>
                                                    <telerik:GridBoundColumn DataField="transDetStr40_02"  HeaderText="transDetStr40_02"  HeaderStyle-Width="160px" ItemStyle-Width="160px"  Display="false"/>
                                                    <telerik:GridBoundColumn DataField="transDetStr40_03"  HeaderText="transDetStr40_03"  HeaderStyle-Width="160px" ItemStyle-Width="160px"  Display="false"/>
                                                    <telerik:GridBoundColumn DataField="transDetStr40_04"  HeaderText="transDetStr40_04"  HeaderStyle-Width="160px" ItemStyle-Width="160px"  Display="false"/>
                                                    <telerik:GridBoundColumn DataField="transDetStr40_05"  HeaderText="transDetStr40_05"  HeaderStyle-Width="160px" ItemStyle-Width="160px"  Display="false"/>
                                                    <telerik:GridBoundColumn DataField="transDetImp_01"    HeaderText="transDetImp_01"    HeaderStyle-Width="110px" ItemStyle-Width="110px"  Display="false" DataFormatString="{0:###,##0.00}" ItemStyle-HorizontalAlign="Right" />
                                                    <telerik:GridBoundColumn DataField="transDetImp_02"    HeaderText="transDetImp_02"    HeaderStyle-Width="110px" ItemStyle-Width="110px"  Display="false" DataFormatString="{0:###,##0.00}" ItemStyle-HorizontalAlign="Right" />
                                                    <telerik:GridBoundColumn DataField="transDetImp_03"    HeaderText="transDetImp_03"    HeaderStyle-Width="110px" ItemStyle-Width="110px"  Display="false" DataFormatString="{0:###,##0.00}" ItemStyle-HorizontalAlign="Right" />
                                                    <telerik:GridBoundColumn DataField="transDetImp_04"    HeaderText="transDetImp_04"    HeaderStyle-Width="110px" ItemStyle-Width="110px"  Display="false" DataFormatString="{0:###,##0.00}" ItemStyle-HorizontalAlign="Right" />
                                                    <telerik:GridBoundColumn DataField="transDetImp_05"    HeaderText="transDetImp_05"    HeaderStyle-Width="110px" ItemStyle-Width="110px"  Display="false" DataFormatString="{0:###,##0.00}" ItemStyle-HorizontalAlign="Right" />
                                                    <telerik:GridBoundColumn DataField="transDetImp_06"    HeaderText="transDetImp_06"    HeaderStyle-Width="110px" ItemStyle-Width="110px"  Display="false" DataFormatString="{0:###,##0.00}" ItemStyle-HorizontalAlign="Right" />
                                                    <telerik:GridBoundColumn DataField="transDetImp_07"    HeaderText="transDetImp_07"    HeaderStyle-Width="110px" ItemStyle-Width="110px"  Display="false" DataFormatString="{0:###,##0.00}" ItemStyle-HorizontalAlign="Right" />
                                                    <telerik:GridBoundColumn DataField="transDetImp_08"    HeaderText="transDetImp_08"    HeaderStyle-Width="110px" ItemStyle-Width="110px"  Display="false" DataFormatString="{0:###,##0.00}" ItemStyle-HorizontalAlign="Right" />
                                                    <telerik:GridBoundColumn DataField="transDetImp_09"    HeaderText="transDetImp_09"    HeaderStyle-Width="110px" ItemStyle-Width="110px"  Display="false" DataFormatString="{0:###,##0.00}" ItemStyle-HorizontalAlign="Right" />
                                                    <telerik:GridBoundColumn DataField="transDetImp_10"    HeaderText="transDetImp_10"    HeaderStyle-Width="110px" ItemStyle-Width="110px"  Display="false" DataFormatString="{0:###,##0.00}" ItemStyle-HorizontalAlign="Right" />
                                                    <telerik:GridBoundColumn DataField="transDetFec_01"    HeaderText="transDetFec_01"    HeaderStyle-Width="100px" ItemStyle-Width="100px"  Display="false" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center"/>
                                                    <telerik:GridBoundColumn DataField="transDetFec_02"    HeaderText="transDetFec_02"    HeaderStyle-Width="100px" ItemStyle-Width="100px"  Display="false" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center"/>
                                                    <telerik:GridBoundColumn DataField="transDetFec_03"    HeaderText="transDetFec_03"    HeaderStyle-Width="100px" ItemStyle-Width="100px"  Display="false" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center"/>
                                                    <telerik:GridBoundColumn DataField="transDetFec_04"    HeaderText="transDetFec_04"    HeaderStyle-Width="100px" ItemStyle-Width="100px"  Display="false" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center"/>
                                                    <telerik:GridBoundColumn DataField="transDetFec_05"    HeaderText="transDetFec_05"    HeaderStyle-Width="100px" ItemStyle-Width="100px"  Display="false" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center"/>
                                                    <telerik:GridBoundColumn DataField="monDes"            HeaderText="Moneda"            HeaderStyle-Width="150px" ItemStyle-Width="150px"  Display="true"/>
                                                    <telerik:GridBoundColumn DataField="transDetFact_01"   HeaderText="transDetFact_01"   HeaderStyle-Width="110px" ItemStyle-Width="110px"  Display="false" DataFormatString="{0:###,##0.00}" ItemStyle-HorizontalAlign="Right"/>
                                                    <telerik:GridBoundColumn DataField="transDetFact_02"   HeaderText="transDetFact_02"   HeaderStyle-Width="110px" ItemStyle-Width="110px"  Display="false" DataFormatString="{0:###,##0.00}" ItemStyle-HorizontalAlign="Right"/>
                                                    <telerik:GridBoundColumn DataField="transDetFact_03"   HeaderText="transDetFact_03"   HeaderStyle-Width="110px" ItemStyle-Width="110px"  Display="false" DataFormatString="{0:###,##0.00}" ItemStyle-HorizontalAlign="Right"/>
                                                    <telerik:GridBoundColumn DataField="transDetFact_04"   HeaderText="transDetFact_04"   HeaderStyle-Width="110px" ItemStyle-Width="110px"  Display="false" DataFormatString="{0:###,##0.00}" ItemStyle-HorizontalAlign="Right"/>
                                                    <telerik:GridBoundColumn DataField="transDetFact_05"   HeaderText="transDetFact_05"   HeaderStyle-Width="110px" ItemStyle-Width="110px"  Display="false" DataFormatString="{0:###,##0.00}" ItemStyle-HorizontalAlign="Right"/>
                                                    <telerik:GridBoundColumn DataField="transDetSit"       HeaderText="transDetSit"       HeaderStyle-Width="100px" ItemStyle-Width="100px"  Display="false"/>
                                                    <telerik:GridImageColumn DataType="System.String"  DataImageUrlFields="imgSit"  HeaderStyle-Width="100px"  ItemStyle-Width="100px" 
                                                                         DataAlternateTextField="ContactName" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                         ImageAlign="Middle" ImageHeight="11px" ImageWidth="11px" HeaderText="Estatus">
                                                 </telerik:GridImageColumn> 
                                                </Columns>
                                                <NoRecordsTemplate> No se encontraron registros.</NoRecordsTemplate>
                                                </MasterTableView>
                                    <HeaderStyle CssClass="GridHeaderStyle"/>
                                    <ItemStyle CssClass="GridRowStyle"/>
                                    <AlternatingItemStyle  CssClass="GridAlternatingRowStyle" />
                                    <selecteditemstyle CssClass="GridSelectedItem"></selecteditemstyle>

                                    <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true" >
                                        <Selecting AllowRowSelect="true" EnableDragToSelectRows="False" ></Selecting>
                                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="5" ScrollHeight="330px"/>
                                    </ClientSettings>
                                </telerik:RadGrid>


 
                                <telerik:RadGrid ID="rGdvOperacionesMovCC" 
                                                runat="server"
                                                AutoGenerateColumns="false"
                                                Width="1245px" Height="290px" 
                                                CssClass="Grid"
                                                skin="Office2010Silver">
                                                <MasterTableView   DataKeyNames="movID" AutoGenerateColumns="false"  CssClass="GridTable"  >
                                                <Columns >
                                                    <telerik:GridBoundColumn DataField="movID"             HeaderText="movCCID"        HeaderStyle-Width="50px"  ItemStyle-Width="50px"   Display="false"/>
                                                    <telerik:GridBoundColumn DataField="transId"             HeaderText="transId"           HeaderStyle-Width="50px"  ItemStyle-Width="50px"   Display="false"/>
                                                    <telerik:GridBoundColumn DataField="ciaCve"              HeaderText="ciaCve"            HeaderStyle-Width="50px"  ItemStyle-Width="50px"   Display="false"/>
                                                    <telerik:GridBoundColumn DataField="cptoId"              HeaderText="cptoId"            HeaderStyle-Width="50px"  ItemStyle-Width="50px"   Display="false"/>
                                                    <telerik:GridBoundColumn DataField="movFolio"          HeaderText="Folio"              HeaderStyle-Width="100px"  ItemStyle-Width="100px"   Display="true"/>
                                                    <telerik:GridBoundColumn DataField="movSec"            HeaderText="Sec."              HeaderStyle-Width="100px"  ItemStyle-Width="100px"   Display="true"/>
                                                    <telerik:GridBoundColumn DataField="monCve"              HeaderText="monCve"              HeaderStyle-Width="50px"  ItemStyle-Width="50px"   Display="false"/>
                                                    
                                                    <telerik:GridBoundColumn DataField="movCoA"            HeaderText="movCCCoA"            HeaderStyle-Width="100px" ItemStyle-Width="100px"  Display="false"/>
                                                    <telerik:GridBoundColumn DataField="movTipApli"        HeaderText="movCCTipApli"            HeaderStyle-Width="100px" ItemStyle-Width="100px"  Display="false"/>
                                                    <telerik:GridBoundColumn DataField="movCoADes"            HeaderText="Movimiento"            HeaderStyle-Width="100px" ItemStyle-Width="100px"  Display="true"/>
                                                    <telerik:GridBoundColumn DataField="movTipApliADes"        HeaderText="Tipo Aplicación"            HeaderStyle-Width="130px" ItemStyle-Width="130px"  Display="true"/>
                                                


                                                    <telerik:GridBoundColumn DataField="movRef10_CodApli"  HeaderText="Codigo"  HeaderStyle-Width="110px" ItemStyle-Width="110px"  Display="true"/>
                                                    <telerik:GridBoundColumn DataField="movRef10_Princ"    HeaderText="Principal"  HeaderStyle-Width="110px" ItemStyle-Width="110px"  Display="true"/>
                                                    <telerik:GridBoundColumn DataField="movRef10_Apli"     HeaderText="Aplicación"  HeaderStyle-Width="110px" ItemStyle-Width="110px"  Display="true"/>
                                                    <telerik:GridBoundColumn DataField="movRef10_03"       HeaderText="Referencia 3"  HeaderStyle-Width="110px" ItemStyle-Width="110px"  Display="true"/>
                                                    <telerik:GridBoundColumn DataField="movRef10_04"       HeaderText="Referencia 4"  HeaderStyle-Width="110px" ItemStyle-Width="110px"  Display="true"/>

                                                    <telerik:GridBoundColumn DataField="movFec_Mov"        HeaderText="Fec. Movimiento"    HeaderStyle-Width="120px" ItemStyle-Width="120px"  Display="true" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center"/>
                                                    <telerik:GridBoundColumn DataField="movFec_Venc"       HeaderText="Fec. Vencimiento"    HeaderStyle-Width="120px" ItemStyle-Width="120px"  Display="true" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center"/>

                                                    <telerik:GridBoundColumn DataField="monDes"              HeaderText="Moneda"            HeaderStyle-Width="150px" ItemStyle-Width="150px"  Display="true"/>

                                                    <telerik:GridBoundColumn DataField="movFac_TipCam"     HeaderText="Tipo Cambio"    HeaderStyle-Width="110px" ItemStyle-Width="110px"  Display="true" DataFormatString="{0:###,##0.00}" ItemStyle-HorizontalAlign="Right" />

                                                    <telerik:GridBoundColumn DataField="movImp_Imp"        HeaderText="Importe"    HeaderStyle-Width="110px" ItemStyle-Width="110px"  Display="true" DataFormatString="{0:###,##0.00}" ItemStyle-HorizontalAlign="Right" />
                                                    <telerik:GridBoundColumn DataField="movRef40_Des"      HeaderText="Descripción"  HeaderStyle-Width="200px" ItemStyle-Width="200px"  Display="true"/>

                                                    <telerik:GridBoundColumn DataField="movOrdComp"       HeaderText="movCCOrdComp"  HeaderStyle-Width="110px" ItemStyle-Width="110px"  Display="false"/>
                                                    <telerik:GridBoundColumn DataField="movProv"          HeaderText="movCCProv"  HeaderStyle-Width="110px" ItemStyle-Width="110px"  Display="false"/>
                                                    <telerik:GridBoundColumn DataField="movApliAntGast"       HeaderText="movCCApliAntGast"  HeaderStyle-Width="110px" ItemStyle-Width="110px"  Display="false"/>
                                                    <telerik:GridBoundColumn DataField="movContRecCve"       HeaderText="movCCContRecCve"  HeaderStyle-Width="110px" ItemStyle-Width="110px"  Display="false"/>

                                                    <telerik:GridBoundColumn DataField="movContFec"        HeaderText="movCCContFec"    HeaderStyle-Width="100px" ItemStyle-Width="100px"  Display="false" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center"/>
                                                    
                                                    <telerik:GridBoundColumn DataField="movDatClasif"       HeaderText="movCCDatClasif"  HeaderStyle-Width="110px" ItemStyle-Width="110px"  Display="false"/>
                                                    <telerik:GridBoundColumn DataField="movPolCve"       HeaderText="movCCPolCve"  HeaderStyle-Width="110px" ItemStyle-Width="110px"  Display="false"/>
                                                    
                                                    <telerik:GridBoundColumn DataField="movSit"             HeaderText="movCCSit"           HeaderStyle-Width="10px"  ItemStyle-Width="10px"   Display="false"/>
                                                    

                                                </Columns>
                                                <NoRecordsTemplate> No se encontraron registros.</NoRecordsTemplate>
                                                </MasterTableView>
                                    <HeaderStyle CssClass="GridHeaderStyle"/>
                                    <ItemStyle CssClass="GridRowStyle"/>
                                    <AlternatingItemStyle  CssClass="GridAlternatingRowStyle" />
                                    <selecteditemstyle CssClass="GridSelectedItem"></selecteditemstyle>

                                    <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true" >
                                        <Selecting AllowRowSelect="true" EnableDragToSelectRows="False" ></Selecting>
                                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="6" ScrollHeight="330px"/>
                                    </ClientSettings>
                                </telerik:RadGrid>



                                    


                                </div> 
                            </div>


                        </fieldset>

                    </td>

                </tr>
            </table>
        </asp:Panel>

            <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
                <telerik:RadImageButton ID="rBtnGuardar"  runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text =""    OnClick="rBtnGuardar_Click"   OnClientClicking ="OnClientClic_ConfirmOK" ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text=""  OnClick="rBtnCancelar_Click" OnClientClicking="OnClientClic_ConfirmOK"  ></telerik:RadImageButton>
            </asp:Panel>    
        

            <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
            </telerik:RadWindowManager>

            <asp:HiddenField ID="hdfBtnAccion" runat="server" />
            <asp:HiddenField ID="hdfBtnAccionDet" runat="server" />
            <asp:HiddenField ID="hdfPag_sOpe" runat="server" />        
            <asp:HiddenField ID="hdf_transDetSec" runat="server" />
            
            <asp:HiddenField ID="hdf_AddColumns" Value="0" runat="server" />

             
        </div>


        <telerik:RadWindow runat="server"  OnClientClose="closeRadWindow" NavigateUrl="RegOperacionesDetAbc.aspx" ID="TextCombos" Width="480px" Height="545" Modal="true" VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close"  >               
        </telerik:RadWindow>
        <telerik:RadWindow runat="server"  OnClientClose="closeRadWindow" NavigateUrl="RegOperacionesDet_MovXP.aspx" ID="rWinRegOpeDet_MovXP" Width="700px" Height="500px" Modal="true" VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close"  >               
        </telerik:RadWindow>
        <telerik:RadWindow runat="server"  OnClientClose="closeRadWindow" NavigateUrl="RegOperacionesDet_MovCC.aspx" ID="rWinRegOpeDet_MovCC" Width="700px" Height="440px" Modal="true" VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close"  >               
        </telerik:RadWindow>
        <telerik:RadWindow runat="server"  OnClientClose="closeRadWindow" NavigateUrl="RegOperacionesDet_MovIN.aspx" ID="rWinRegOpeDet_MovIN" Width="770px" Height="500px" Modal="true" VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close"  >               
        </telerik:RadWindow>


<%--        </ContentTemplate>
    </asp:UpdatePanel>--%>


</asp:Content>

