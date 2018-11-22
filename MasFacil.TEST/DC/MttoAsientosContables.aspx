<%@ Page Title="" Language="C#" MasterPageFile="~/MPForm.master" AutoEventWireup="true" CodeFile="MttoAsientosContables.aspx.cs" Inherits="DC_MttoAsientosContables" %>

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
                 
          <fieldset style="">
             <div style="width:100%; display:table; position:static; background-color:transparent;"   > 
                <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none; width:100%; text-align:left; background-color:transparent ;">
                    <tr>
                        <td>
                            <telerik:RadLabel ID="RadLabel1" runat="server" Text="Asiento Contable"></telerik:RadLabel>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="rTxtAsientoContable" runat="server" Width="200px" 
                                                            EnabledStyle-CssClass="cssTxtEnabled"
                                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                                            HoveredStyle-CssClass="cssTxtHovered"
                                                            FocusedStyle-CssClass="cssTxtFocused" Enabled="false"
                                                            InvalidStyle-CssClass="cssTxtInvalid"></telerik:RadTextBox>
                        </td>
                        <td>
                        <telerik:RadLabel ID="RadLabel2" runat="server" Text="Descripción" Width="100px"></telerik:RadLabel>
                        </td>
                        <td>
                        <telerik:RadTextBox ID="rTxtDescripcion" runat="server" Width="200px" 
                                                        EnabledStyle-CssClass="cssTxtEnabled"
                                                        DisabledStyle-CssClass ="cssTxtEnabled"
                                                        HoveredStyle-CssClass="cssTxtHovered"
                                                        FocusedStyle-CssClass="cssTxtFocused" 
                                                        InvalidStyle-CssClass="cssTxtInvalid"  
                                ></telerik:RadTextBox>
                         </td>
                         <td>
                            <telerik:RadLabel ID="RadLabel3" runat="server"  Text="Concepto" Width="100px"></telerik:RadLabel>
                             </td>
                        <td>
                                    <telerik:RadComboBox ID="rCboConcepto" runat="server" Width="200px" AutoPostBack="true" 
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
                                    <td>
                                        <asp:Label ID="Label10" runat="server" Text="Fecha" Width="100px"></asp:Label>
                                        <telerik:RadDatePicker ID="RdDateFecha" runat="server" Width="120px"    AutoPostBack="true"  OnSelectedDateChanged="RdDateFecha_SelectedDateChanged" ></telerik:RadDatePicker>
                                        <telerik:RadDatePicker ID="polFecReg" runat="server" Width="120px"    AutoPostBack="true"  Visible="false" ></telerik:RadDatePicker>
                                        <telerik:RadDatePicker ID="polFec" runat="server" Width="120px"    AutoPostBack="true"  Visible="false" ></telerik:RadDatePicker>
                                    </td>
                                 </tr>
                                <tr >
                                    <td>
                                         <telerik:RadLabel ID="RadLabel4" runat="server"  Text="Cargos" Width="120px"></telerik:RadLabel>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="RdNumricCargos" runat="server"
                                             Width="200px"
                                                    EnabledStyle-CssClass="cssTxtEnabled"
                                                    DisabledStyle-CssClass ="cssTxtEnabled"
                                                    HoveredStyle-CssClass="cssTxtHovered"
                                                    FocusedStyle-CssClass="cssTxtFocused"
                                                    InvalidStyle-CssClass="cssTxtInvalid">
                                        </telerik:RadNumericTextBox>
                                      </td>
                                        <td>
                                         <telerik:RadLabel ID="RadLabel5" runat="server"  Text="Abonos" Width="100px"></telerik:RadLabel>
                                       </td>
                                      <td>
                                        <telerik:RadNumericTextBox ID="RdNumricAbonos" runat="server" Width="200px"
                                                    EnabledStyle-CssClass="cssTxtEnabled"
                                                    DisabledStyle-CssClass ="cssTxtEnabled"
                                                    HoveredStyle-CssClass="cssTxtHovered"
                                                    FocusedStyle-CssClass="cssTxtFocused"
                                                    InvalidStyle-CssClass="cssTxtInvalid">
                                        </telerik:RadNumericTextBox>
                                          </td>
                                      <td>
                                         <telerik:RadLabel ID="RadLabel6" runat="server"  Text="Cifra Control" Width="100px"></telerik:RadLabel>
                                          </td>
                                      <td>
                                        <telerik:RadNumericTextBox ID="RdNumricFiltraControl" runat="server" Width="200px"
                                                    EnabledStyle-CssClass="cssTxtEnabled"
                                                    DisabledStyle-CssClass ="cssTxtEnabled"
                                                    HoveredStyle-CssClass="cssTxtHovered"
                                                    FocusedStyle-CssClass="cssTxtFocused"
                                                    InvalidStyle-CssClass="cssTxtInvalid">
                                        </telerik:RadNumericTextBox>
                                          </td>
                                      <td>
                                        <telerik:RadLabel ID="RadLabel7" runat="server" Text="Situación" Width="90px" ></telerik:RadLabel>
                                        <telerik:RadBinaryImage ID="RdBnryImagenSituacion" runat="server" Height="15px" Width="15px" ImageAlign="Middle"/> 
                                    </td>
                                   
                                </tr>
                            </table>
                            </div>
                        </fieldset>




                <fieldset style="  margin-top:5px;   display: block; text-align:left;" >
                    <legend>Detalle</legend>
                    <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
                        <telerik:RadImageButton ID="rBtnNuevo"       runat ="server" Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png"          Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png"        Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"      OnClick="rBtnNuevo_Click"  ToolTip="Nuevo"  Text =""  ></telerik:RadImageButton>
                        <telerik:RadImageButton ID="rBtnModificar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png"      Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png"    Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  OnClick="rBtnModificar_Click"  ToolTip="Modificar"  Text="" ></telerik:RadImageButton>
                        <telerik:RadImageButton ID="rBtnEliminar"    runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"       Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png"     Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"   OnClick="rBtnEliminar_Click" ToolTip="Eliminar"  Text=""  ></telerik:RadImageButton>
                        <telerik:RadImageButton ID="rBtnLimpiar"     runat="server"  Width="30px" Height="30px"   Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"        Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png"      Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  OnClick="rBtnLimpiar_Click" ToolTip="Limpiar"  Text="" ></telerik:RadImageButton>
                    </asp:Panel> 
                    <telerik:RadGrid ID="rGdvAsientosContables" 
                                                    runat="server"
                                                    AutoGenerateColumns="False" 
                                                    Width="1250px" Height="285px" 
                                                    CssClass="Grid"
                                                    skin="Office2010Silver"  >
                                          <MasterTableView DataKeyNames="asiContDetId" AutoGenerateColumns="False"  CssClass="GridTable"  > 
                                                <Columns>
                                                        <telerik:GridBoundColumn DataField="asiContDetId"    HeaderStyle-Width="100px" HeaderText="Sec" ItemStyle-Width="100px"  Display="false"  />
                                                        <telerik:GridBoundColumn DataField="polDetSec"    HeaderStyle-Width="50px" HeaderText="Sec" ItemStyle-Width="50px"   />
                                                        <telerik:GridBoundColumn DataField="ctaContCve"    HeaderStyle-Width="140px" HeaderText="Codificación Cont." ItemStyle-Width="140px"   />
                                                        <telerik:GridBoundColumn DataField="polDetDes" HeaderStyle-Width="180px" HeaderText="Descripción" ItemStyle-Width="180px"  />
                                                        <telerik:GridBoundColumn DataField="polDetCoADes"   HeaderStyle-Width="90px" HeaderText="Movimiento" ItemStyle-Width="90px"/>
                                                        <telerik:GridBoundColumn DataField="polDetFecMov"   HeaderStyle-Width="120px" HeaderText="Fecha Mov." ItemStyle-Width="120px"  DataFormatString="{0:d}"/>
                                                        <telerik:GridBoundColumn DataField="polDetFecVenc"   HeaderStyle-Width="120px" HeaderText="Fecha Ven." ItemStyle-Width="120px"  DataFormatString="{0:d}"/>
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
                             </fieldset>



            <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
                <telerik:RadImageButton ID="rBtnGuardar"  runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text =""  OnClick="rBtnGuardar_Click"    OnClientClicking ="OnClientClic_ConfirmOK" ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text="" OnClick="rBtnCancelar_Click"   OnClientClicking="OnClientClic_ConfirmOK"  ></telerik:RadImageButton>
            </asp:Panel>    
        


            <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false">
            </telerik:RadWindowManager>

                <telerik:RadWindow ID="RadWindowEditaPoliza" runat="server" OnClientClose="closeRadWindow"  Width="770px" Height="420px" Modal="true" VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close" Title="Poliza Detalle"  >               
                </telerik:RadWindow>

                <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server"   
                        OnAjaxRequest="RadAjaxManager1_AjaxRequest"  >  
                    <AjaxSettings>  
                         
                    </AjaxSettings>  
                </telerik:RadAjaxManager>

            <asp:HiddenField ID="hdfBtnAccion" runat="server" />
            </div>
            </ContentTemplate>
         </asp:UpdatePanel>
</asp:Content>

