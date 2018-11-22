<%@ Page Title="" Language="C#" MasterPageFile="~/MPForm.master" AutoEventWireup="true" CodeFile="rptConfigAcumulados.aspx.cs" Inherits="RPT_rptConfigAcum" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMPForm" Runat="Server">
        <script src="http://code.jquery.com/jquery-1.11.0.min.js"></script>

        <script src="http://code.jquery.com/jquery-1.11.0.min.js"></script>
        <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
        <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
        <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
        <link href="../css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />
        <link href="../css/styles.css" rel="stylesheet" type="text/css" />
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
        <asp:Panel runat="server" ID="pnlGral" >

        
            <div>
                    <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
                        <telerik:RadImageButton ID="rBtnNuevo"     runat ="server" Width="30px" Height="25px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text ="" OnClick="rBtnNuevo_Click" Visible="false" ></telerik:RadImageButton>
                        <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="25px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  ToolTip="Modificar"  Text=""  OnClick="rBtnModificar_Click" Visible="false" ></telerik:RadImageButton>
                        <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="30px" Height="25px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text=""  OnClick="rBtnEliminar_Click"  OnClientClicking="OnClientClic_ConfirmOK" Visible="false" ></telerik:RadImageButton>
                        <telerik:RadImageButton ID="rBtnCopiar"      runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCopiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnCopiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCopiarHovered.png"  ToolTip="Copiar"  Text="" OnClick="rBtnCopiar_Click" OnClientClicking="OnClientClic_ConfirmOK" Visible="false" ></telerik:RadImageButton>
                        <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="25px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"   Text="" OnClick="rBtnLimpiar_Click"></telerik:RadImageButton>    
                    </asp:Panel>

                        <table>
                        <tr>
                            <td>
                                <fieldset>
                                <legend>Reporte</legend>
                                <table id="tbcvedes">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Text="Clave"  Width="58px" ></asp:Label>
                                        </td>
                                        <td>
                                           <telerik:RadTextBox ID="rTxtCve" runat="server" Enabled="false" Width="100px" MaxLength="20"
                                                EnabledStyle-CssClass="cssTxtEnabled"
                                                DisabledStyle-CssClass ="cssTxtEnabled"
                                                HoveredStyle-CssClass="cssTxtHovered"
                                                FocusedStyle-CssClass="cssTxtFocused"
                                                InvalidStyle-CssClass="cssTxtInvalid" ></telerik:RadTextBox> 
                                        </td>
                                           
                                    </tr>
                                    <tr>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Text="Nombre"  ></asp:Label>
                                             </td>
                                             <td>
                                                <telerik:RadTextBox ID="rTxtDes" runat="server"   Enabled="false"  Width="200px" MaxLength="50"
                                                    EnabledStyle-CssClass="cssTxtEnabled"
                                                    DisabledStyle-CssClass ="cssTxtEnabled"
                                                    HoveredStyle-CssClass="cssTxtHovered"
                                                    FocusedStyle-CssClass="cssTxtFocused"
                                                    InvalidStyle-CssClass="cssTxtInvalid"></telerik:RadTextBox>
                                             </td>
                                    </tr>
                                        <tr>
                                        <td colspan="2">
                                            <telerik:RadGrid ID="rGdv_Reporte" 
                                                runat="server" 
                                                AutoGenerateColumns="False" 
                                                OnSelectedIndexChanged="rGdv_Documentos_SelectedIndexChanged"
                                                Width="340px"   Height="300px"   
                                                CssClass="Grid" 
                                                Skin="Office2010Silver">
                                                <MasterTableView DataKeyNames="idReporte"  AutoGenerateColumns="false" CssClass="GridTable"   >
                                                    <Columns> 
                                                        <telerik:GridBoundColumn HeaderText="Clave"    DataField="idReporte"  HeaderStyle-Width="80px"  ItemStyle-Width="80px" ></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderText="Nombre"  DataField="nombre"   HeaderStyle-Width="120px"  ItemStyle-Width="120px"  ></telerik:GridBoundColumn>
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
                                            </td>
                                        </tr>
                                </table>
                                </fieldset>
                            </td>


                           <%-- COLUMNAS DEL REPORTE--%>
                           <td>
                           <fieldset>
                                <legend>Columnas Reporte</legend>
                                <table   >
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label3" runat="server" Text="No Columna"   Width="100px" ></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="rTxtNoColumna" runat="server" Width="60px" Enabled="false"   NumberFormat-GroupSeparator="0"
                                                    EnabledStyle-CssClass="cssTxtEnabled"
                                                    DisabledStyle-CssClass ="cssTxtEnabled"
                                                    HoveredStyle-CssClass="cssTxtHovered"
                                                    FocusedStyle-CssClass="cssTxtFocused"
                                                    InvalidStyle-CssClass="cssTxtInvalid">
                                                    <NumberFormat GroupSeparator="" DecimalDigits="0" /> 
                                            </telerik:RadNumericTextBox>
                                         </td>
                                        <td>
                                            <asp:Label ID="Label4" runat="server" Text="Titulo" ></asp:Label>
                                        </td>
                                        <td colspan="4"  >
                                            <telerik:RadTextBox ID="rTxtTitulo" runat="server" Width="260px" Enabled="false" 
                                                    EnabledStyle-CssClass="cssTxtEnabled"
                                                    DisabledStyle-CssClass ="cssTxtEnabled"
                                                    HoveredStyle-CssClass="cssTxtHovered"
                                                    FocusedStyle-CssClass="cssTxtFocused"
                                                    InvalidStyle-CssClass="cssTxtInvalid"></telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label5" runat="server" Text="Subtitulo" ></asp:Label>
                                            </td>
                                        <td colspan="2">
                                            <telerik:RadTextBox ID="rTxtNoSubtitulo" runat="server" Enabled="false" 
                                                    EnabledStyle-CssClass="cssTxtEnabled"
                                                    DisabledStyle-CssClass ="cssTxtEnabled"
                                                    HoveredStyle-CssClass="cssTxtHovered"
                                                    FocusedStyle-CssClass="cssTxtFocused"
                                                    InvalidStyle-CssClass="cssTxtInvalid"></telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        <asp:Label ID="Label6" runat="server" Text="Tipo"></asp:Label>
                                        </td>
                                        <td>
                                        <telerik:RadComboBox ID="rCboTipoColumn" runat="server" Width="200px"  Enabled="false"  Visible="true"
                                                HighlightTemplatedItems="true" AutoPostBack="true" OnSelectedIndexChanged="rCboTipoColumn_SelectedIndexChanged"
                                              DropDownCssClass="cssRadComboBox" 
                                              DropDownWidth="260px" 
                                               Height="200px"  >
                                                    <HeaderTemplate>
                                                        <table style="width: 150px" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td style="width: 80px;">
                                                                        Tipo
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
                                                                    <%# DataBinder.Eval(Container.DataItem, "idTipoColumna")%>
                                                                </td>
                                                                <td style="width: 170px;">
                                                                    <%# DataBinder.Eval(Container.DataItem, "descripcion") %>
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
                                                 <asp:Label ID="Label7" runat="server" Text="Formato"></asp:Label>
                                                 </td>
                                        <td>
                                                 <asp:RadioButton ID="rBtnValCreEntero" runat="server" GroupName="valCredito" Text="Entero"  Enabled="false" />
                                                 </td>
                                        <td>
                                                 <asp:RadioButton ID="rBtnValCreDecimal" runat="server" GroupName="valCredito" Text="Decimal" Checked="true"   Enabled="false" />
                                                 </td>
                                        <td>
                                                 <asp:RadioButton ID="rBtnValCreNoAplica" runat="server" GroupName="valCredito" Text="N/A" Checked="true"  Enabled="false" />
                                                     </td>
                                        <td>
                                                <asp:CheckBox ID="CheckMiles" runat="server" Text="Miles"  Enabled="false"  />
                                            </td>
                                             <td>
                                            <asp:Label ID="Label8" runat="server" Text="Formula"  ></asp:Label>
                                            </td>
                                        <td>
                                            <telerik:RadTextBox ID="rTxtFormula" runat="server"
                                                    EnabledStyle-CssClass ="cssTxtEnabled"
                                                    DisabledStyle-CssClass ="cssTxtEnabled"
                                                    HoveredStyle-CssClass="cssTxtHovered"
                                                    FocusedStyle-CssClass="cssTxtFocused"
                                                    InvalidStyle-CssClass="cssTxtInvalid" Enabled="false"  ></telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="11">    
                                             <telerik:RadGrid ID="rGdv_ReporteColumnas" 
                                               runat="server"  
                                               AutoGenerateColumns="False" 
                                               OnSelectedIndexChanged="rGdv_Referencias_SelectedIndexChanged"
                                               Width="880px" Height="300px"
                                               CssClass="Grid" 
                                               Skin="Office2010Silver">
                                                    <MasterTableView DataKeyNames="idReporte" AutoGenerateColumns="False" CssClass="GridTable">
                                                        <Columns>
                                                            <telerik:GridBoundColumn HeaderText="Clave"              DataField="idReporte" HeaderStyle-Width="40px"   ItemStyle-Width="40px" Display="false" />
                                                            <telerik:GridBoundColumn HeaderText="Num Columna"        DataField="numeroColumna" HeaderStyle-Width="35px"   ItemStyle-Width="35px" />
                                                            <telerik:GridBoundColumn HeaderText="Titulo"             DataField="tituloColumna" HeaderStyle-Width="40px"   ItemStyle-Width="40px" />
                                                            <telerik:GridBoundColumn HeaderText="Subtitulo"        DataField="subtituloColumna" HeaderStyle-Width="40px"   ItemStyle-Width="40px" />
                                                            <telerik:GridBoundColumn HeaderText="Tipo Columna"       DataField="idTipoColumna" HeaderStyle-Width="40px"   ItemStyle-Width="40px" Display="false" />
                                                            <telerik:GridBoundColumn HeaderText="Tipo Columna"       DataField="idTipoColumnaDes" HeaderStyle-Width="40px"   ItemStyle-Width="40px" />
                                                            <telerik:GridBoundColumn HeaderText="Formato"        DataField="formato" HeaderStyle-Width="40px"   ItemStyle-Width="40px" Display="false" />
                                                            <telerik:GridBoundColumn HeaderText="Formato"        DataField="formatoDes" HeaderStyle-Width="25px"   ItemStyle-Width="25px" />
                                                            <telerik:GridBoundColumn HeaderText="Miles"        DataField="miles" HeaderStyle-Width="40px"   ItemStyle-Width="40px" Display="false" />
                                                            <telerik:GridBoundColumn HeaderText="Miles"        DataField="milesDes" HeaderStyle-Width="15px"   ItemStyle-Width="15px" />
                                                            <telerik:GridBoundColumn HeaderText="Formula"        DataField="formula" HeaderStyle-Width="40px"   ItemStyle-Width="40px" />

                                                        </Columns>
                                                        <NoRecordsTemplate>No se encontraron registros</NoRecordsTemplate>
                                                    </MasterTableView>

                                                        <HeaderStyle CssClass="GridHeaderStyle"/>
                                                        <ItemStyle CssClass="GridRowStyle"/>
                                                        <AlternatingItemStyle  CssClass="GridAlternatingRowStyle" />
                                                        <selecteditemstyle CssClass="GridSelectedItem"></selecteditemstyle>
 
                                                        <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true" >
                                                            <Selecting AllowRowSelect="true" EnableDragToSelectRows="False" ></Selecting>
                                                            <Scrolling AllowScroll="true" UseStaticHeaders="true"  ScrollHeight="230px"    />
                                                            <Animation AllowColumnReorderAnimation="True" />
                                                        </ClientSettings>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                           </td>
                        </tr>
                    </table>
                        
        <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
            <telerik:RadImageButton ID="rBtnGuardar"   runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text =""   OnClick="rBtnGuardar_Click"  OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
            <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text=""  OnClick="rBtnCancelar_Click"  OnClientClicking="OnClientClic_ConfirmCancel"></telerik:RadImageButton>
        </asp:Panel>

         <telerik:RadWindow ID="RadWindowCopiaConfigDocAcum" runat="server" OnClientClose="closeRadWindow"  Width="440px" Height="270px" Modal="true" VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close" Title="Copiar Reporte"  >               
            </telerik:RadWindow>

            <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server"  OnAjaxRequest="RadAjaxManager1_AjaxRequest"  >  
                        <AjaxSettings>  
                            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">  
                                <UpdatedControls>  
                                    <telerik:AjaxUpdatedControl ControlID="rGdv_Reporte" />  
                                </UpdatedControls>  
                            </telerik:AjaxSetting>  
                        </AjaxSettings>  
            </telerik:RadAjaxManager>

            <asp:HiddenField ID="hdfBtnAccion" runat="server" />
            
            <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false">
            </telerik:RadWindowManager> 
            </div>
            
                <asp:HiddenField ID="hdfModifAgr" runat="server" /> 
                <asp:HiddenField ID="hdfCveAgr" runat="server" />   
        </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel> 
</asp:Content>

