<%@ Page Title="" Language="C#" MasterPageFile="~/MPForm.master" AutoEventWireup="true" CodeFile="AdmonCobros.aspx.cs" Inherits="CC_AdmonCobros" %>

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

            <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
                <telerik:RadImageButton ID="rBtnNuevo"       runat ="server" Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png"          Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png"        Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"          ToolTip="Nuevo"  Text =""  OnClick="rBtnNuevo_Click" Visible="false" ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnModificar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png"      Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png"    Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"    ToolTip="Modificar"  Text=""  OnClick="rBtnModificar_Click" Visible="false" ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnEliminar"    runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"       Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png"     Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text="" OnClick="rBtnEliminar_Click" OnClientClicking="OnClientClic_ConfirmOK" Visible="false" ></telerik:RadImageButton>
                
                <telerik:RadImageButton ID="rBtnValidacion"  runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnValidacionDisabled.png"     Image-Url="~/Imagenes/IcoBotones/IcoBtnValidacion.png"   Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnValidacionHovered.png"   ToolTip="Validacion" OnClick="rBtnValidacion_Click" Text="" OnClientClicking="OnClientClic_ConfirmOK" Visible="false"  ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtVerErr"       runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnVerErroresDisabled.png"     Image-Url="~/Imagenes/IcoBotones/IcoBtnVerErrores.png"   Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnVerErroresHovered.png"  ToolTip="Ver Errores"  OnClick="rBtVerErr_Click" Text="" Visible="false" ></telerik:RadImageButton>

                <telerik:RadImageButton ID="rBtnGenera"      runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnProcesarDisabled.png"       Image-Url="~/Imagenes/IcoBotones/IcoBtnProcesar.png"     Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnProcesarHovered.png"  ToolTip="Genera"  OnClick="rBtnGenera_Click" Text="" OnClientClicking="OnClientClic_ConfirmOK" Visible="false" ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnAplica"      runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnAutorizarDisabled.png"      Image-Url="~/Imagenes/IcoBotones/IcoBtnAutorizar.png"    Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnAutorizarHovered.png"  ToolTip="Aplica"  OnClick="rBtnAplica_Click" Text="" OnClientClicking="OnClientClic_ConfirmOK" Visible="false" ></telerik:RadImageButton>
                
                <telerik:RadImageButton ID="rBtnPolizaEdit" runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarPolizaDisabled.png"      Image-Url="~/Imagenes/IcoBotones/IcoBtnModificarPoliza.png"    Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarPolizaHovered.png"  ToolTip="Edita Poliza"  Text="" OnClick="rBtnPolizaeEdit_Click"  Visible="false" ></telerik:RadImageButton>

                <telerik:RadImageButton ID="rBtnPolizaImp"   runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnImprimirDisabled.png"       Image-Url="~/Imagenes/IcoBotones/IcoBtnImprimir.png"     Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnImprimirHovered.png"  ToolTip="Imprime Poliza"  OnClick="rBtnPolizaImp_Click" Text="" Visible="false" ></telerik:RadImageButton>

                <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px"   Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"        Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png"      Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"  Text="" OnClick="rBtnLimpiar_Click" ></telerik:RadImageButton>

            </asp:Panel>  


            <table border="0" style=" border-style:none; width:100%; text-align:left; background-color:transparent ;">
                <tr  style="width:100%;">
                    <td style=" width:100%; background-color:transparent; vertical-align:top;">                      
                        <fieldset style="">
                            <legend>Filtros</legend>
                            <div style="width:100%; display:table; position:static; background-color:transparent;"> 

                              <table border="0" style=" border-style:none;   text-align:left; background-color:transparent;">
                                <tr>                              
                                    <td style=" width:80px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label6" runat="server" Text="Concepto"></asp:Label>
                                    </td>
                                    <td style=" width:200px; background-color:transparent; vertical-align:top;">
                                        <telerik:RadComboBox ID="rCboConcepto" runat="server" Width="190px" AutoPostBack="true" 
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

                                    <td style=" width:60px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label5" runat="server" Text="Cuenta"></asp:Label>
                                    </td>
                                    <td style=" width:200px; background-color:transparent; vertical-align:top;">
                                         <telerik:RadComboBox ID="rCboCuenta" runat="server" Width="190px" AutoPostBack="true"
                                              OnSelectedIndexChanged="rCboCuenta_SelectedIndexChanged"
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

                                        <!-- Se agrega combo de Descripcion-->

                                         <td style=" width:75px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label14" runat="server" Text="Descripcion"></asp:Label>
                                    </td>

                                    <td style=" width:175px; background-color:transparent; vertical-align:top;">
                                         <telerik:RadComboBox ID="rCboDescripcion" runat="server" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="rCboDescripcion_SelectedIndexChanged"
                                                    HighlightTemplatedItems="true"
                                                    DropDownCssClass="cssRadComboBox" 
                                                    DropDownWidth="405px" 
                                                    Height="200px"    >
                                                      <HeaderTemplate>
                                                        <table style="width: 400px" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td style="width: 50px;">
                                                                        Folio
                                                                    </td>
                                                                    <td style="width: 350px;">
                                                                        Descripción
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table style="width: 400px;"  cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width:50px" >
                                                                    <%# DataBinder.Eval(Container.DataItem, "copCCPFolio")%>
                                                                </td>
                                                                <td style="width: 350px;">
                                                                    <%# DataBinder.Eval(Container.DataItem, "cobCCDes") %>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                    </FooterTemplate>
                                            </telerik:RadComboBox>

                                    </td>

                                    <!--Fin combo Descripcion-->

                                    <td style=" width:60px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label8" runat="server" Text="Fec. Ini."></asp:Label>
                                    </td>
                                    <td style=" width:100px; background-color:transparent; vertical-align:top;">
                                         <telerik:RadDatePicker ID="RdDateFecha_Inicio" runat="server" Width="120px"    AutoPostBack="true" OnSelectedDateChanged="RdDateFecha_Inicio_SelectedDateChanged" ></telerik:RadDatePicker>
                                     </td>
                                    <td style=" width:65px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label10" runat="server" Text="Fec. Fin."></asp:Label>
                                    </td>
                                    <td style=" width:100px; background-color:transparent; vertical-align:top;">
                                        <telerik:RadDatePicker ID="RdDateFecha_Final" runat="server" Width="120px"    AutoPostBack="true" OnSelectedDateChanged="RdDateFecha_Final_SelectedDateChanged" ></telerik:RadDatePicker>
                                    </td>

                                    

                                    

                                    <td style=" width:40px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label7" runat="server" Text="Situación"></asp:Label>
                                    </td>

                                    <td style=" width:200px; background-color:transparent; vertical-align:top;">

                                        <telerik:RadComboBox ID="rCboSituacion" runat="server" Width="190px" AutoPostBack="true" 
                                             OnSelectedIndexChanged="rCboSituacion_SelectedIndexChanged"
                                                    HighlightTemplatedItems="true"
                                                    DropDownCssClass="cssRadComboBox" 
                                                    DropDownWidth="220px" 
                                                    Height="200px"    >
                                                    <HeaderTemplate>
                                                        <table style="width: 195px" cellspacing="0" cellpadding="0">

         
                                                                <tr>

                                                                    <td style="width: 25px;">
                                                                    </td>
                                                                    <td style="width: 160px; text-align:left;">
                                                                        Descripción
                                                                    </td>

                                                                </tr>
                                                            </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table style="width: 150px;"  cellspacing="0" cellpadding="0">
                                                            <tr>


                                                                <td style="width: 25px;">
                                                                    <asp:Image runat="server" Width="12px" Height="12px" ImageUrl ='<%# DataBinder.Eval(Container.DataItem, "imgSit") %>'  />
                                            
                                                                </td>
                                                                <td style="width: 160px;">
                                                                    <%# DataBinder.Eval(Container.DataItem, "Descripcion") %>
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
                                        <asp:Label ID="Label13" runat="server" Text="Cliente" Visible="false"></asp:Label>
                                    </td>


                                    <td>
                                        <telerik:RadTextBox ID="rTxtBeneficiario" Visible="false" runat="server" AutoPostBack="true" OnTextChanged="rTxtBeneficiario_TextChanged"></telerik:RadTextBox>

                                        <telerik:RadComboBox ID="rCboBeneficiario" runat="server" Width="130px" AutoPostBack="true" Visible="false"
                                              OnSelectedIndexChanged="rCboCuenta_SelectedIndexChanged"
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
                                                                    <%# DataBinder.Eval(Container.DataItem, "cliCve")%>
                                                                </td>
                                                                <td style="width: 200px;">
                                                                    <%# DataBinder.Eval(Container.DataItem, "clieNom") %>
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
            </table>


            <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none; width:100%; text-align:left; background-color:transparent ;">
                <tr  style="width:100%;">
                    <td style=" width:1200px; background-color:transparent; vertical-align:top;">
                        <fieldset style="">
                            <legend>Operaciones</legend>
                            <div style="width:100%; height:310px;  display:table; position:static; background-color:transparent;" align="center" > 

                                <div>


                                      <telerik:RadGrid ID="rGdvOperaciones" 
                                                    runat="server"
                                                    AutoGenerateColumns="False" 
                                                    Width="1250px" Height="285px" 
                                                    CssClass="Grid"
                                                    skin="Office2010Silver"  >
                                                    <MasterTableView   DataKeyNames="cobCCId" AutoGenerateColumns="False"  CssClass="GridTable"  > 
         
                                            <Columns>
                                                 <telerik:GridBoundColumn DataField="cptoDes2"     HeaderStyle-Width="100px" HeaderText="Concepto" ItemStyle-Width="100px"   />
                                                 <telerik:GridBoundColumn DataField="cobCCId"  HeaderStyle-Width="75px" HeaderText="cobCCId" ItemStyle-Width="100px" Display="false"   /> 
                                                 <telerik:GridBoundColumn DataField="transId"  HeaderStyle-Width="75px" HeaderText="transId" ItemStyle-Width="100px" Display="false"   /> 
                                                <telerik:GridBoundColumn DataField="asiContEncId"  HeaderStyle-Width="75px" HeaderText="asiContEncId" ItemStyle-Width="100px" Display="false"   />  
                                                <telerik:GridBoundColumn DataField="copCCPFolio"  HeaderStyle-Width="75px" HeaderText="Folio" ItemStyle-Width="100px"   />
                                                <telerik:GridBoundColumn DataField="cobCCDes"    HeaderStyle-Width="200px" HeaderText="Descripción" ItemStyle-Width="100px"   />
                                                <telerik:GridBoundColumn DataField="ctaDepDes"   HeaderStyle-Width="100px" HeaderText="Cuenta" ItemStyle-Width="100px"   />
                                                <telerik:GridBoundColumn DataField="cobCCFec"    HeaderStyle-Width="75px" HeaderText="Fecha" ItemStyle-Width="100px"  DataFormatString="{0:d}" />
                                                <telerik:GridBoundColumn DataField="cobCCImp"    HeaderStyle-Width="75px" HeaderText="Importe" ItemStyle-Width="100px" DataFormatString="{0:###,##0.00}" ItemStyle-HorizontalAlign="Right"  />
                                                 <telerik:GridBoundColumn DataField="cptoId"    HeaderStyle-Width="15px" HeaderText="cptoId" ItemStyle-Width="15px" Display="false"  />
                                               
                                                <telerik:GridImageColumn DataType="System.String" DataImageUrlFields="imgSit"  HeaderStyle-Width="40px"  ItemStyle-Width="40px" 
                                                                     DataAlternateTextField="ContactName" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                     ImageAlign="Middle" ImageHeight="11px" ImageWidth="11px" HeaderText="Sit">
                                                </telerik:GridImageColumn>
                                                <telerik:GridBoundColumn DataField="cobCCSit"  HeaderStyle-Width="75px" HeaderText="Sit" ItemStyle-Width="100px" Display="false"   />                                                                                    

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


                                </div>
                                <div style="width:100%;  display:table; position:static; background-color:transparent;" align="right" > 
                                     <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none; font-size:14px;   text-align:left; background-color:transparent;">
                                        <tr   >   
                                        <td style=" width:70px; background-color:transparent;  vertical-align:top;">
                                            <asp:Image ID="Image1" runat="server" Width="12px" Height="12px" ImageUrl="~/Imagenes/icoSituacion/imgSitR.png" />
                                            <asp:Label ID="Label1" runat="server" Text="Reg."></asp:Label>
                                        </td>
                                        <td style=" width:70px; background-color:transparent;  vertical-align:top;">
                                            <asp:Image ID="Image2" runat="server" Width="12px" Height="12px" ImageUrl="~/Imagenes/icoSituacion/imgSitV.png" />
                                            <asp:Label ID="Label2" runat="server" Text="Val."></asp:Label>
                                        </td>
                                        <td style=" width:70px; background-color:transparent;  vertical-align:top;">
                                            <asp:Image ID="Image3" runat="server" Width="12px" Height="12px" ImageUrl="~/Imagenes/icoSituacion/imgSitG.png" />
                                            <asp:Label ID="Label3" runat="server" Text="Gen."></asp:Label>
                                        </td>
                                        <td style=" width:90px; background-color:transparent;  vertical-align:top;">
                                            <asp:Image ID="Image7" runat="server" Width="12px" Height="12px" ImageUrl="~/Imagenes/icoSituacion/imgSitGV.png" />
                                            <asp:Label ID="Label12" runat="server" Text="Gen. Val."></asp:Label>
                                        </td>
                                        <td style=" width:90px; background-color:transparent;  vertical-align:top;">
                                            <asp:Image ID="Image5" runat="server" Width="12px" Height="12px" ImageUrl="~/Imagenes/icoSituacion/imgSitGE.png" />
                                            <asp:Label ID="Label9" runat="server" Text="Gen. Error"></asp:Label>
                                        </td>
                                        <td style=" width:90px; background-color:transparent;  vertical-align:top;">
                                            <asp:Image ID="Image4" runat="server" Width="12px" Height="12px" ImageUrl="~/Imagenes/icoSituacion/imgSitI.png" />
                                            <asp:Label ID="Label4" runat="server" Text="Incorp."></asp:Label>
                                        </td>

                                        <td style=" width:90px; background-color:transparent;  vertical-align:top;">
                                            <asp:Image ID="Image6" runat="server" Width="12px" Height="12px" ImageUrl="~/Imagenes/icoSituacion/imgSitE.png" />
                                            <asp:Label ID="Label11" runat="server" Text="Error."></asp:Label>
                                        </td>
                                        </tr>
                                    </table>     
                                </div>                         
                            </div>
                        </fieldset>
                    </td>
                </tr>
            </table>


            <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
                <telerik:RadImageButton ID="rBtnGuardar"  runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text =""    OnClick="rBtnGuardar_Click"   OnClientClicking ="OnClientClic_ConfirmOK" Visible="false" ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text=""  OnClick="rBtnCancelar_Click" OnClientClicking="OnClientClic_ConfirmOK"   Visible="false" ></telerik:RadImageButton>
            </asp:Panel>    
        
            <telerik:RadWindow ID="RadWindowVerErrores" runat="server" OnClientClose="closeRadWindow"  Width="700px" Height="440px" Modal="true" VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close" Title="Ver Errores"  >               
            </telerik:RadWindow>

            <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
            </telerik:RadWindowManager>

            <asp:HiddenField ID="hdfRawUrl" runat="server" />
            <asp:HiddenField ID="hdfBtnAccion" runat="server" />

        </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server"   
                    OnAjaxRequest="RadAjaxManager1_AjaxRequest" >  
                    <AjaxSettings>  
                    </AjaxSettings>  
    </telerik:RadAjaxManager>

</asp:Content>

