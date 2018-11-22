<%@ Page Title="" Language="C#" MasterPageFile="~/MPForm.master" AutoEventWireup="true" CodeFile="RegOperaciones.aspx.cs" Inherits="DC_RegOperaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMPForm" Runat="Server">

        <script src="http://code.jquery.com/jquery-1.11.0.min.js"></script>
    
    <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
    <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
 
    <link href="../css/styles.css" rel="stylesheet" type="text/css" />
   <link href="/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />


    <script type="text/javascript">
       function RowDblClick(sender, eventArgs) {
    //var url = '../DC/RegOperacionesDet.aspx';
    ////$(location).att('href', url);
    //window.location.href = '../DC/RegOperacionesDet.aspx';


    var grid = sender;
    var MasterTable = grid.get_masterTableView(); var row = MasterTable.get_dataItems()[eventArgs.get_itemIndexHierarchical()];
    var cell = MasterTable.getCellByColumnUniqueName(row, "transId");
    //here cell.innerHTML holds the value of the cell
    var empleado = cell.innerHTML;
    location.href = "../DC/RegOperacionesDet.aspx?Ope=" + empleado;


}

                   function closeRadWindow()  
        {  
            $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest();   
        }
   </script> 

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMPForm" Runat="Server">

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest" DefaultLoadingPanelID="RadAjaxLoadingPanel1" >  
        <AjaxSettings> 
            <telerik:AjaxSetting AjaxControlID="rBtnNuevo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID = "pnlBody" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rBtnModificar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID = "pnlBody" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rBtnEliminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID = "pnlBody" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rBtnValidacion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID = "pnlBody" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rBtVerErr">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID = "pnlBody" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rBtnGenera">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID = "pnlBody" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rBtnAplica">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID = "pnlBody" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rBtnPolizaEdit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID = "pnlBody" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rBtnPolizaImp">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID = "pnlBody" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rBtnLimpiar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID = "pnlBody" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>  
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Silk"></telerik:RadAjaxLoadingPanel>
       
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>

    <asp:Panel ID="pnlBody" runat="server">
        <div>

            <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
                <telerik:RadImageButton ID="rBtnNuevo"       runat ="server" Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png"          Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png"        Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"          ToolTip="Nuevo"  Text =""  OnClick="rBtnNuevo_Click"></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnModificar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png"      Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png"    Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"    ToolTip="Modificar"  Text=""  OnClick="rBtnModificar_Click" ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnEliminar"    runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"       Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png"     Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text="" OnClick="rBtnEliminar_Click"  OnClientClicking="OnClientClic_ConfirmOK" ></telerik:RadImageButton>
                
                <telerik:RadImageButton ID="rBtnValidacion"  runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnValidacionDisabled.png"     Image-Url="~/Imagenes/IcoBotones/IcoBtnValidacion.png"   Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnValidacionHovered.png"   ToolTip="Validacion"  Text="" OnClick="rBtnValidacion_Click" OnClientClicking="OnClientClic_ConfirmOK" ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtVerErr"       runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnVerErroresDisabled.png"     Image-Url="~/Imagenes/IcoBotones/IcoBtnVerErrores.png"   Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnVerErroresHovered.png"  ToolTip="Ver Errores"  Text=""  OnClick="rBtVerErr_Click"  ></telerik:RadImageButton>

                <telerik:RadImageButton ID="rBtnGenera"      runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnProcesarDisabled.png"       Image-Url="~/Imagenes/IcoBotones/IcoBtnProcesar.png"     Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnProcesarHovered.png"  ToolTip="Genera"  Text="" OnClick="rBtnGenera_Click" OnClientClicking="OnClientClic_ConfirmOK" ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnAplica"      runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnAutorizarDisabled.png"      Image-Url="~/Imagenes/IcoBotones/IcoBtnAutorizar.png"    Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnAutorizarHovered.png"  ToolTip="Aplica"  Text="" OnClick="rBtnAplica_Click" OnClientClicking="OnClientClic_ConfirmOK" ></telerik:RadImageButton>
                 <telerik:RadImageButton ID="rBtnImprimir"    runat ="server" Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnImprimirDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnImprimir.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnImprimirHovered.png"    ToolTip="Imprimir"  Text ="" OnClick="rBtnImprimir_Click"></telerik:RadImageButton>
               
                <telerik:RadImageButton ID="rBtnPolizaEdit" runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarPolizaDisabled.png"      Image-Url="~/Imagenes/IcoBotones/IcoBtnModificarPoliza.png"    Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarPolizaHovered.png"  ToolTip="Edita Poliza"  Text="" OnClick="rBtnPolizaEdit_Click" ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnPolizaImp"   runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnImprimirDisabled.png"       Image-Url="~/Imagenes/IcoBotones/IcoBtnImprimir.png"     Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnImprimirHovered.png"  ToolTip="Imprime Poliza"  Text="" OnClick="rBtnPolizaImp_Click"  ></telerik:RadImageButton>

                <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px"   Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"        Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png"      Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"  Text="" OnClick="rBtnLimpiar_Click" ></telerik:RadImageButton>
            </asp:Panel>     


            <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none; width:100%; text-align:left; background-color:transparent ;">
                <tr  style="width:100%;">

                    <td style=" width:100%; background-color:transparent; vertical-align:top;">
                        
                        <fieldset style="">
                            <legend>Filtros</legend>
                            <div style="width:100%; display:table; position:static; background-color:transparent;" align="center" > 

                           
                            <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none;   text-align:left; background-color:transparent;">
                                <tr   >                              
                                    <td style=" width:110px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label6" runat="server" Text="Tipo Concepto"></asp:Label>
                                    </td>
                                    <td style=" width:215px; background-color:transparent; vertical-align:top;">
                                         
                                        <telerik:RadComboBox ID="rCboTipoConcepto" runat="server" Width="190px" AutoPostBack="true" OnSelectedIndexChanged="rCboTipoConcepto_SelectedIndexChanged"
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
                                                                    <%# DataBinder.Eval(Container.DataItem, "Clave")%>
                                                                </td>
                                                                <td style="width: 200px;">
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


                                    <td style=" width:75px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label5" runat="server" Text="Concepto"></asp:Label>
                                    </td>
                                    <td style=" width:240px; background-color:transparent; vertical-align:top;">
                
                                         <telerik:RadComboBox ID="rCboConcepto" runat="server" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="rCboConcepto_SelectedIndexChanged"
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


                                    <td style=" width:75px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label7" runat="server" Text="Situacion"></asp:Label>
                                    </td>

                                    <td style=" width:175px; background-color:transparent; vertical-align:top;">
                                         <telerik:RadComboBox ID="rCboSituacion" runat="server" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="rCboSituacion_SelectedIndexChanged"
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
                                      <!-- Se agrega combo de Descripcion-->

                                         <td style=" width:75px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label3" runat="server" Text="Descripcion"></asp:Label>
                                    </td>

                                    <td style=" width:175px; background-color:transparent; vertical-align:top;">
                                         <telerik:RadComboBox ID="rCboDescripcion" runat="server" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="rCboDescripcion_SelectedIndexChanged"
                                                    HighlightTemplatedItems="true"
                                                    DropDownCssClass="cssRadComboBox" 
                                                    DropDownWidth="220px" 
                                                    Height="200px"    >
                                                      <HeaderTemplate>
                                                        <table style="width: 300px" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td style="width: 100px;">
                                                                        Folio
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
                                                                    <%# DataBinder.Eval(Container.DataItem, "transFolio")%>
                                                                </td>
                                                                <td style="width: 200px;">
                                                                    <%# DataBinder.Eval(Container.DataItem, "transDes") %>
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
                                    <td style=" width:120px; background-color:transparent; vertical-align:top;">
                                         <telerik:RadDatePicker ID="RdDateFecha_Inicio" runat="server" Width="105px"    AutoPostBack="true" OnSelectedDateChanged="RdDateFecha_Inicio_SelectedDateChanged" ></telerik:RadDatePicker>
                                     </td>
                                    <td style=" width:65px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label10" runat="server" Text="Fec. Fin."></asp:Label>
                                    </td>
                                    <td style=" width:120px; background-color:transparent; vertical-align:top;">
                                        <telerik:RadDatePicker ID="RdDateFecha_Final" runat="server" Width="105px"    AutoPostBack="true" OnSelectedDateChanged="RdDateFecha_Final_SelectedDateChanged" ></telerik:RadDatePicker>
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
                                                    skin="Office2010Silver" OnSelectedIndexChanged="rGdv_Documentos_SelectedIndexChanged" >
                                                    <MasterTableView   DataKeyNames="transId" AutoGenerateColumns="False"  CssClass="GridTable"  > 
         
                                            <Columns>
                                                 <telerik:GridBoundColumn DataField="transId"     HeaderStyle-Width="208px" HeaderText="transId" ItemStyle-Width="100px"  Display="false" />
                                                 <telerik:GridBoundColumn DataField="cptoDes"     HeaderStyle-Width="330px" HeaderText="Concepto" ItemStyle-Width="100px"   />
                                                 <telerik:GridBoundColumn DataField="transFolio"  HeaderStyle-Width="100px" HeaderText="Folio" ItemStyle-Width="100px"  />
                                                <telerik:GridBoundColumn  DataField="transPolCve" HeaderStyle-Width="98px" HeaderText="A. Contable" ItemStyle-Width="98px"  />
                                                 <telerik:GridBoundColumn DataField="transFec"   HeaderStyle-Width="90px" HeaderText="Fecha" ItemStyle-Width="90px" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>
                                                 <telerik:GridBoundColumn DataField="transDes"   HeaderStyle-Width="324px" HeaderText="Descripcion" ItemStyle-Width="100px"  />
                                                 <telerik:GridBoundColumn DataField="maUsuNom"   HeaderStyle-Width="208px" HeaderText="Usuario" ItemStyle-Width="100px"  />
                                                 <telerik:GridBoundColumn DataField="transSit"   HeaderStyle-Width="208px" HeaderText="transSit" ItemStyle-Width="100px"  Display="false" />
                                                 <telerik:GridBoundColumn DataField="accId"   HeaderStyle-Width="50px" HeaderText="accId" ItemStyle-Width="50px"  Display="false" />
                                                <telerik:GridBoundColumn DataField="asiContEncId"   HeaderStyle-Width="50px" HeaderText="asiContEncId" ItemStyle-Width="50px"  Display="false" /> 
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
                                            <ClientEvents OnRowDblClick="RowDblClick" />
                                        </ClientSettings>
                                    </telerik:RadGrid>


                                </div>
                                <div style="width:100%;  display:table; position:static; background-color:transparent;" align="right" > 
                                     <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none; font-size:14px;   text-align:left; background-color:transparent;">
                                        <tr   > 
                                        <td style=" width:70px; background-color:transparent;  vertical-align:top;">
                                        </td>
                                        <td style=" width:70px; background-color:transparent;  vertical-align:top;">
                                            <asp:Image ID="Image1" runat="server" Width="12px" Height="12px" ImageUrl="~/Imagenes/icoSituacion/imgSitR.png" />
                                            <asp:Label ID="Label1" runat="server" Text="Reg."></asp:Label>
                                        </td>
                                        <td style=" width:60px; background-color:transparent;  vertical-align:top;">
                                            <asp:Image ID="Image2" runat="server" Width="12px" Height="12px" ImageUrl="~/Imagenes/icoSituacion/imgSitV.png" />
                                            <asp:Label ID="Label2" runat="server" Text="Val."></asp:Label>
                                        </td>
                                        <td style=" width:70px; background-color:transparent;  vertical-align:top;">
                                            <asp:Image ID="Image6" runat="server" Width="12px" Height="12px" ImageUrl="~/Imagenes/icoSituacion/imgSitE.png" />
                                            <asp:Label ID="Label11" runat="server" Text="Error."></asp:Label>
                                        </td>                                        <td style=" width:90px; background-color:transparent;  vertical-align:top;">
                                            <asp:Image ID="Image7" runat="server" Width="12px" Height="12px" ImageUrl="~/Imagenes/icoSituacion/imgSitGV.png" />
                                            <asp:Label ID="Label12" runat="server" Text="Gen. Val."></asp:Label>
                                        </td>
                                        <td style=" width:95px; background-color:transparent;  vertical-align:top;">
                                            <asp:Image ID="Image5" runat="server" Width="12px" Height="12px" ImageUrl="~/Imagenes/icoSituacion/imgSitGE.png" />
                                            <asp:Label ID="Label9" runat="server" Text="Gen. Error"></asp:Label>
                                        </td>
                                        <td style=" width:80px; background-color:transparent;  vertical-align:top;">
                                            <asp:Image ID="Image4" runat="server" Width="12px" Height="12px" ImageUrl="~/Imagenes/icoSituacion/imgSitI.png" />
                                            <asp:Label ID="Label4" runat="server" Text="Incorp."></asp:Label>
                                        </td>


                                        </tr>
                                    </table>     
                                </div>                         
                            </div>


                        </fieldset>

                    </td>

                </tr>
            </table>


            <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
            </telerik:RadWindowManager>

            <asp:HiddenField ID="hdfRawUrl" runat="server" />
            <asp:HiddenField ID="hdfBtnAccion" runat="server" />

             <telerik:RadWindow ID="RadWindowVerErrores" runat="server" OnClientClose="closeRadWindow"  Title="Errores" Width="700px" Height="440px" Modal="true" VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close"  >               
            </telerik:RadWindow>

        </div>
     </asp:Panel>       

        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>



</asp:Content>

