<%@ Page Title="" Language="C#" MasterPageFile="~/MPForm.master" AutoEventWireup="true" CodeFile="Poliza.aspx.cs" Inherits="CG_Poliza" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMPForm" Runat="Server">
   <script src="http://code.jquery.com/jquery-1.11.0.min.js"></script>
    <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
    <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
    <link href="../css/styles.css" rel="stylesheet" type="text/css" />
   <link href="/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />
      <script type="text/javascript">
   
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



            <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none; width:100%; text-align:left; background-color:transparent ;">
                <tr  style="width:100%;">
                    <td style=" width:100%; background-color:transparent; vertical-align:top;">
                        
                        <fieldset style="">
                            <legend>Filtros</legend>
                            <div style="width:100%; display:table; position:static; background-color:transparent;" align="center" > 

                            <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none;   text-align:left; background-color:transparent;">
                                <tr style=" height:30px"   >                              
                                    <td style=" width:100px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="lblConcepto" runat="server" Text="Concepto"></asp:Label>
                                    </td>
                                    <td style=" width:250px; background-color:transparent; vertical-align:top;">
                                        <telerik:RadComboBox ID="rCboConcepto" runat="server" Width="230px"   OnSelectedIndexChanged="rCboConcepto_SelectedIndexChanged" AutoPostBack ="true"
                                                    HighlightTemplatedItems="true"
                                                    DropDownCssClass="cssRadComboBox" 
                                                    DropDownWidth="310px" 
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

                                    <td style=" width:90px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="lblAContable" runat="server" Text="A. Contable"></asp:Label>
                                    </td>
                                    <td style=" width:220px; background-color:transparent; vertical-align:top;">
                                        <telerik:RadTextBox ID="rTxtAsientoContable" runat="server" Width="200px" Enabled="false"
                                                            EnabledStyle-CssClass="cssTxtEnabled"
                                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                                            HoveredStyle-CssClass="cssTxtHovered"
                                                            FocusedStyle-CssClass="cssTxtFocused" 
                                                            InvalidStyle-CssClass="cssTxtInvalid"
                                                            OnTextChanged="rTxtAsientoContable_TextChanged"
                                                            AutoPostBack="true" >
                                        </telerik:RadTextBox>
                                    </td>

                                    <td style=" width:90px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label1" runat="server" Text="Descripción"></asp:Label>
                                    </td>
                                    <td style=" width:320px; background-color:transparent; vertical-align:top;">
                                        <telerik:RadTextBox ID="rTxtDescripcion" runat="server" Width="300px" 
                                                        EnabledStyle-CssClass="cssTxtEnabled"
                                                        DisabledStyle-CssClass ="cssTxtEnabled"
                                                        HoveredStyle-CssClass="cssTxtHovered"
                                                        FocusedStyle-CssClass="cssTxtFocused" 
                                                        InvalidStyle-CssClass="cssTxtInvalid">
                                        </telerik:RadTextBox> 
                                     </td>

                                    <td style=" width:50px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label2" runat="server" Text="Fecha"></asp:Label>
                                    </td>
                                    <td style=" width:120px; background-color:transparent; vertical-align:top;">
                                         <telerik:RadDatePicker ID="RdDateFecha" runat="server" Width="100px" OnSelectedDateChanged="RdDateFecha_SelectedDateChanged"  AutoPostBack="true" ></telerik:RadDatePicker>
                                    </td>

                                </tr>
                                <tr style=" height:30px"   >                              
                                    <td style=" width:100px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label3" runat="server" Text="Cifra Control"></asp:Label>
                                    </td>
                                    <td style=" width:250px; background-color:transparent; vertical-align:top;">
                                        <telerik:RadNumericTextBox ID="rNumCifraControl" runat="server" Width="120px"
                                                    EnabledStyle-CssClass="cssTxtEnabled"
                                                    DisabledStyle-CssClass ="cssTxtEnabled"
                                                    HoveredStyle-CssClass="cssTxtHovered"
                                                    FocusedStyle-CssClass="cssTxtFocused"
                                                    InvalidStyle-CssClass="cssTxtInvalid">
                                        </telerik:RadNumericTextBox>
                                    </td>

                                    <td style=" width:90px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label4" runat="server" Text="Cargos"></asp:Label>
                                    </td>
                                    <td style=" width:220px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="lblTotalCargos" runat="server" Text=""></asp:Label>
                                    </td>

                                    <td style=" width:90px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label5" runat="server" Text="Abonos"></asp:Label>
                                    </td>
                                    <td style=" width:320px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="lblTotalAbonos" runat="server" Text=""></asp:Label>
                                     </td>

                                    <td style=" width:50px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label6" runat="server" Text="Sit."></asp:Label>
                                    </td>
                                    <td style=" width:120px; background-color:transparent; vertical-align:top;">
                                        <telerik:RadBinaryImage ID="RdBnryImagenSituacion" runat="server" Height="15px" Width="15px" ImageAlign="Middle"/>
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

                                <div>
                                      <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
                                        <telerik:RadImageButton ID="rBtnNuevoDet"       runat ="server" Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png"          Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png"        Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"         ToolTip="Nuevo"  Text =""  OnClick="rBtnNuevoDet_Click" ></telerik:RadImageButton>
                                        <telerik:RadImageButton ID="rBtnModificarDet"   runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png"      Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png"    Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"    ToolTip="Modificar"  Text=""  OnClick="rBtnModificarDet_Click" ></telerik:RadImageButton>
                                        <telerik:RadImageButton ID="rBtnEliminarDet"    runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"       Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png"     Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"    ToolTip="Eliminar"  Text="" OnClick="rBtnEliminarDet_Click" OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
                                        <telerik:RadImageButton ID="rBtnLimpiar"     runat="server"  Width="30px" Height="30px"   Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"        Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png"      Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"   ToolTip="Limpiar"  Text="" OnClick="rBtnLimpiar_Click"  ></telerik:RadImageButton>
                                      </asp:Panel> 

                                   
                                     <telerik:RadGrid ID="rGdvPolizasDet" 
                                                    runat="server"
                                                    AutoGenerateColumns="False" 
                                                    Width="1250px" Height="285px" 
                                                    CssClass="Grid"
                                                    skin="Office2010Silver"  >
                                          <MasterTableView DataKeyNames="asiContDetId" AutoGenerateColumns="False"  CssClass="GridTable"  > 
                                                <Columns>
                                                        <telerik:GridBoundColumn DataField="asiContDetId"    HeaderStyle-Width="80px" HeaderText="Sec" ItemStyle-Width="80px"  Display="false"  />
                                                        <telerik:GridBoundColumn DataField="polDetSec"    HeaderStyle-Width="50px" HeaderText="Sec" ItemStyle-Width="50px"   />
                                                        <telerik:GridBoundColumn DataField="ctaContCve"    HeaderStyle-Width="120px" HeaderText="Codificación Cont." ItemStyle-Width="120px" Display="false"   />
                                                        <telerik:GridBoundColumn DataField="ctaContCveFotmat"    HeaderStyle-Width="160px" HeaderText="Cuenta Contable" ItemStyle-Width="160px"   />
                                                        <telerik:GridBoundColumn DataField="ctaContDes"    HeaderStyle-Width="210px" HeaderText="Nombre de la Cuenta" ItemStyle-Width="210px"     />
                                                        <telerik:GridBoundColumn DataField="polDetDes" HeaderStyle-Width="250px" HeaderText="Descripción" ItemStyle-Width="250px"  />
                                                        <telerik:GridBoundColumn DataField="polDetCoADes"   HeaderStyle-Width="70px" HeaderText="Movimiento" ItemStyle-Width="70px"/>
                                                        <telerik:GridBoundColumn DataField="polDetFecMov"   HeaderStyle-Width="80px" HeaderText="Fecha Mov." ItemStyle-Width="80px"  DataFormatString="{0:d}"/>
                                                        <telerik:GridBoundColumn DataField="polDetFecVenc"   HeaderStyle-Width="80px" HeaderText="Fecha Ven." ItemStyle-Width="80px"  DataFormatString="{0:d}"/>
                                                        <telerik:GridBoundColumn DataField="polDetImp"   HeaderStyle-Width="100px" HeaderText="Importe" ItemStyle-Width="100px"  DataFormatString="{0:###,##0.00}" ItemStyle-HorizontalAlign="Right"/>
                                                        <telerik:GridImageColumn DataType="System.String" DataImageUrlFields="imgSit"  HeaderStyle-Width="100px"  ItemStyle-Width="100px" 
                                                        DataAlternateTextField="ContactName" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                        ImageAlign="Middle" ImageHeight="11px" ImageWidth="11px" HeaderText="Estatus" Display="false">
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
                                            <Scrolling AllowScroll="true" UseStaticHeaders="true"  ScrollHeight="330px"/>
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
        

            <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
            </telerik:RadWindowManager>

            <telerik:RadWindow ID="RadWindowPolizaDetalle" runat="server" OnClientClose="closeRadWindow"  Width="770px" Height="420px" Modal="true" VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close" Title="Poliza Detalle"  >               
            </telerik:RadWindow>


                <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server"   
                        OnAjaxRequest="RadAjaxManager1_AjaxRequest"  >  
                    <AjaxSettings>  
                         
                    </AjaxSettings>  
                </telerik:RadAjaxManager>

            <asp:HiddenField ID="hdfPag_asiContEncId" runat="server" />

            <asp:HiddenField ID="hdfBtnAccion" runat="server" />
            <asp:HiddenField ID="hdfBtnAccionDet" runat="server" />

        </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

