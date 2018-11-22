﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MPForm.master" AutoEventWireup="true" CodeFile="AdmonPagosRegistro.aspx.cs" Inherits="XP_AdmonPagosRegistro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMPForm" Runat="Server">
    <script src="http://code.jquery.com/jquery-1.11.0.min.js"></script>
    <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
    <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" /> 
    <link href="../css/styles.css" rel="stylesheet" type="text/css" />
    <link href="/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />

    <script>
        function closeRadWindow()
        {  
                $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest();
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMPForm" Runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <div>

            <table border="0" style=" border-style:none; width:100%; text-align:left; background-color:transparent ;">
                <tr  style="width:100%;">

                    <td style=" width:100%; background-color:transparent; vertical-align:top;">
                        
                        <fieldset style="">
                            
                            <div style="width:100%; display:table; position:static; background-color:transparent;"> 

                            <table border="0" style=" border-style:none;   text-align:left; background-color:transparent;">
                                <tr   >                      
                                    <td style=" width:150px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label5" runat="server" Text="Cta. Deposito"></asp:Label>
                                    </td>
                                    <td style=" background-color:transparent; vertical-align:top;" colspan="3">
                                         <telerik:RadComboBox ID="rCboCuenta" runat="server" Width="180px" AutoPostBack="true"
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

                                    <td style=" width:60px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label8" runat="server" Text="Fecha"></asp:Label>
                                    </td>
                                    <td style=" width:130px; background-color:transparent; vertical-align:top;">
                                         <telerik:RadDatePicker ID="RdDateFecha" runat="server" Width="120px" AutoPostBack="true" OnSelectedDateChanged="RdDateFecha_SelectedDateChanged" ></telerik:RadDatePicker>
                                     </td>


                                    <td style=" width:80px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label6" runat="server" Text="Concepto"></asp:Label>
                                    </td>
                                    <td style=" width:220px; background-color:transparent; vertical-align:top;">
                                        <telerik:RadComboBox ID="rCboConcepto" runat="server" Width="200px" AutoPostBack="true" 
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

                                    
                                    

                                    <td style=" width:100px; background-color:transparent; vertical-align:top;">
                                         <asp:Label ID="Label7" runat="server" Text="Descripción"></asp:Label>
                                    </td>

                                    <td style=" width:200px; background-color:transparent; vertical-align:top;">
                                        <telerik:RadTextBox ID="rTxtDescripcion"
                                            EnabledStyle-CssClass="cssTxtEnabled"
                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                            HoveredStyle-CssClass="cssTxtHovered"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid" 
                                             Width="250px" runat="server"></telerik:RadTextBox>
                                        
                                        
                                    </td>

                                    

                                </tr>

                            </table>                          
                            <table border="0" style=" border-style:none;   text-align:left; background-color:transparent;">
                                <tr   >                      
                                    <td style=" width:150px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label13" runat="server" Text="Proveedor"></asp:Label>
                                    
                                    </td>
                                    <td style=" background-color:transparent; vertical-align:top;"  >
                                        <telerik:RadComboBox ID="rCboBeneficiario" runat="server" Width="180px" AutoPostBack="true"
                                               OnSelectedIndexChanged="rCboBeneficiario_SelectedIndexChanged"
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
                                                                    <%# DataBinder.Eval(Container.DataItem, "provCve")%>
                                                                </td>
                                                                <td style="width: 200px;">
                                                                    <%# DataBinder.Eval(Container.DataItem, "provNom") %>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                    </FooterTemplate>
                                            </telerik:RadComboBox>
                                    </td>

                                    <td style=" width:65px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label1" runat="server" Text="Beneficiario"></asp:Label>
                                    </td>
                                    <td style=" width:150px; background-color:transparent; vertical-align:top;">
                                       <telerik:RadTextBox ID="rTxtBeneficiario" Visible="true"  Width="365px" MaxLength="50"
                                            EnabledStyle-CssClass="cssTxtEnabled"
                                                                                        DisabledStyle-CssClass ="cssTxtEnabled"
                                                                                        HoveredStyle-CssClass="cssTxtHovered"
                                                                                        FocusedStyle-CssClass="cssTxtFocused"
                                                                                        InvalidStyle-CssClass="cssTxtInvalid" 
                                            
                                            runat="server"></telerik:RadTextBox>

                                     </td>


                                    <td style=" width:50px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label10" runat="server" Text="Importe"></asp:Label>
                                    </td>
                                    <td style=" width:80px; background-color:transparent; vertical-align:top;">
                                        <telerik:RadNumericTextBox runat="server" ID="rTxtImporte" Width="100px" Value="1" EmptyMessage="Enter units count" MinValue="0" ShowSpinButtons="false" NumberFormat-DecimalDigits="2" Type="Currency"
                                            
                                            EnabledStyle-CssClass="cssTxtEnabled"
                                                                                        DisabledStyle-CssClass ="cssTxtEnabled"
                                                                                        HoveredStyle-CssClass="cssTxtHovered"
                                                                                        FocusedStyle-CssClass="cssTxtFocused"
                                                                                        InvalidStyle-CssClass="cssTxtInvalid" 
                                            >

                                        </telerik:RadNumericTextBox>
                                    </td>

                                    <td style=" width:125px; background-color:transparent; vertical-align:top;">
                                        <asp:RadioButton ID="rBtnRegTransaccion" runat="server" GroupName="x"  Text="Transacción" Checked="true" AutoPostBack="true" Enabled="true" OnCheckedChanged="rBtnRegTransaccion_CheckedChanged" />

                                    </td>

                                    <td style=" width:125px; background-color:transparent; vertical-align:top;">
                                         <asp:RadioButton ID="rBtnRegMovimiento"  runat="server"   GroupName="x" Text="Movimiento" Checked="false"   AutoPostBack="true" Enabled="true"  OnCheckedChanged="rBtnRegMovimiento_CheckedChanged"/>

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

                    <td style=" width:1200px; background-color:transparent; vertical-align:top;">

                        <fieldset style="">
                            <legend>Detalle</legend>
                            <div style="width:100%; height:310px;  display:table; position:static; background-color:transparent;" align="center" > 

                                <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
                                    <telerik:RadImageButton ID="rBtnNuevo"     runat ="server" Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text =""  OnClick="rBtnNuevo_Click"></telerik:RadImageButton>
                                    <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  ToolTip="Modificar"  Text=""  OnClick="rBtnModificar_Click" ></telerik:RadImageButton>
                                    <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text="" OnClick="rBtnEliminar_Click" OnClientClicking="OnClientClic_ConfirmOK" ></telerik:RadImageButton>
                                    <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"  Text="" OnClick="rBtnLimpiar_Click" ></telerik:RadImageButton>
                                </asp:Panel> 

                                <div>
                                    <asp:TextBox ID="rTxtFolio" runat="server" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="rTxtAsientoCont" runat="server" Visible="false"></asp:TextBox>
                                      <telerik:RadGrid ID="rGdvOperacionesTrans" 
                                           OnSelectedIndexChanged="rGdvOperacionesTrans_SelectedIndexChanged"
                                                    runat="server"
                                                    AutoGenerateColumns="False" 
                                                    Width="1250px" Height="285px" 
                                                    CssClass="Grid"
                                                    skin="Office2010Silver"  >
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
                                                        
                                                    <telerik:GridImageColumn DataType="System.String" DataImageUrlFields="imgSit"  HeaderStyle-Width="100px"  ItemStyle-Width="100px" 
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
                                            <Scrolling AllowScroll="true" UseStaticHeaders="true"  ScrollHeight="330px"    />
                                            
                                        </ClientSettings>
                                    </telerik:RadGrid>

                                    <telerik:RadGrid ID="rGdvOperacionesMovCC" 
                                                runat="server"
                                         OnSelectedIndexChanged="rGdvOperacionesMovCC_SelectedIndexChanged"
                                           
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

                                    <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="false" >
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


            <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
                <telerik:RadImageButton ID="rBtnGuardar"  runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text =""    OnClick="rBtnGuardar_Click"   OnClientClicking ="OnClientClic_ConfirmOK" ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text=""  OnClick="rBtnCancelar_Click" OnClientClicking="OnClientClic_ConfirmOK"  ></telerik:RadImageButton>
            </asp:Panel>
            
            
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:HiddenField ID="hdfBtnAccionDet" runat="server" />
            <asp:HiddenField ID="hdfPag_sOpe" runat="server" />  

            <%--GRET, Se agrega campo oculto para guardar número de cheque--%>
            <asp:HiddenField ID="hdfPag_PagXPNum" runat="server" />
            <%--  --%>
            
            <asp:HiddenField ID="hdf_transDetSec" runat="server" />
                
        


            <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
            </telerik:RadWindowManager>

                <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server"   
                         OnAjaxRequest="RadAjaxManager1_AjaxRequest"  >  
                        <AjaxSettings>  
                            <telerik:AjaxSetting AjaxControlID="rGdvOperacionesTrans">  
                                <UpdatedControls>  
                                    <telerik:AjaxUpdatedControl ControlID="rGdvOperacionesTrans" />  
                                </UpdatedControls>  
                            </telerik:AjaxSetting>  
                            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">  
                                <UpdatedControls>  
                                    <telerik:AjaxUpdatedControl ControlID="rGdvOperacionesTrans" />  
                                </UpdatedControls>  
                            </telerik:AjaxSetting>
                        </AjaxSettings>  
            </telerik:RadAjaxManager>

        <telerik:RadWindow runat="server"  OnClientClose="closeRadWindow" NavigateUrl="AdmonPagosRegistroTrans.aspx" ID="rdWindowTrans" Width="850px" Height="500" Modal="true" VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close"  >               
        </telerik:RadWindow>

        <telerik:RadWindow runat="server"  OnClientClose="closeRadWindow" NavigateUrl="AdmonPagosRegistroMov.aspx" ID="rdWindowMov" Width="700px" Height="440px" Modal="true" VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close"  >               
        </telerik:RadWindow>

            <asp:HiddenField ID="hdfBtnAccion" runat="server" />
        </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
