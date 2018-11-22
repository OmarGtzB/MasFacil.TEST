<%@ Page Title="" Language="C#" MasterPageFile="~/MPForm.master" AutoEventWireup="true" CodeFile="MttoDefinicionDoc.aspx.cs" Inherits="DC_MttoDefinicionDoc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMPForm" Runat="Server">
    <script src="http://code.jquery.com/jquery-1.11.0.min.js"></script>

    <script src="http://code.jquery.com/jquery-1.11.0.min.js"></script>

    
    <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
    <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
    <link href="../css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />
    <link href="../css/styles.css" rel="stylesheet" type="text/css" />

      <script type="text/javascript">
  function TrimIt(sender, args)
  {
    var value = args.get_newValue();
    var trimmed = value.replace(/^\s+|\s+$/g, '');
    args.set_newValue(trimmed);
  }
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMPForm" Runat="Server">
 
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div>

    <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
        <telerik:RadImageButton ID="rBtnNuevo"     runat ="server" Width="30px" Height="25px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text ="" OnClick="rBtnNuevo_Click" Visible="false" ></telerik:RadImageButton>
        <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="25px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  ToolTip="Modificar"  Text="" OnClick="rBtnModificar_Click" Visible="false" ></telerik:RadImageButton>
        <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="30px" Height="25px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text="" OnClick="rBtnEliminar_Click" OnClientClicking="OnClientClic_ConfirmOK" Visible="false" ></telerik:RadImageButton>
        <telerik:RadImageButton ID="rBtnSeguridad"  runat="server"  Width="30px" Height="25px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnSeguridadDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnSeguridad.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnSeguridadHovered.png"  ToolTip="Seguridad" OnClick="rBtnSeguridad_Click"  Text="" Visible="false" ></telerik:RadImageButton> 
        <telerik:RadImageButton ID="rBtnDescuentos"  runat="server"  Width="30px" Height="25px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnDescuentoDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnDescuento.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnDescuentoHovered.png"  ToolTip="Descuentos"  OnClick="rBtnDescuentos_Click"  Text="" Visible="false" ></telerik:RadImageButton>   
        <telerik:RadImageButton ID="rBtnListaPrecios"  runat="server"  Width="30px" Height="25px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnListaPrecioDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnListaPrecio.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnListaPrecioHovered.png"  ToolTip="Lista de Precios"  OnClick="rBtnListaPrecios_Click"  Text="" Visible="false" ></telerik:RadImageButton>
        <telerik:RadImageButton ID="rBtnImpuestos"  runat="server"  Width="30px" Height="25px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnImpuestoDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnImpuesto.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnImpuestoHovered.png"  ToolTip="Impuestos"  OnClick="rBtnImpuestos_Click"  Text="" Visible="false" ></telerik:RadImageButton>    
        <telerik:RadImageButton ID="rBtnUsoCFDI"  runat="server"  Width="30px" Height="25px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnImpuestoDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnImpuesto.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnImpuestoHovered.png"  ToolTip="Uso CFDI"  OnClick="rBtnUsoCFDI_Click"  Text="" Visible ="false"  ></telerik:RadImageButton>    
        <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="25px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar" OnClick="rBtnLimpiar_Click"  Text=""></telerik:RadImageButton>    
    </asp:Panel>
        

    <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none; width:1200px; text-align:left; background-color:transparent ;">
        <tr  style="width:1200px;">
            <td style=" width:200px; background-color:transparent; position:static; vertical-align:top">

                <fieldset>
                    <legend>Documentos</legend>

       
                         <telerik:RadGrid ID="rGdv_Documentos" 
                                           runat="server" 
                                           AutoGenerateColumns="False" 
                                           OnSelectedIndexChanged="rGdv_Documentos_SelectedIndexChanged"
                                           Width="270px"   Height="458px"   
                                           CssClass="Grid" 
                                           Skin="Office2010Silver">

                                <MasterTableView DataKeyNames="docCve"  AutoGenerateColumns="false" CssClass="GridTable"   >
                                    <Columns> 
                                        <telerik:GridBoundColumn HeaderText="Documento"    DataField="docCve"  HeaderStyle-Width="80px"  ItemStyle-Width="80px" ></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Descripción"  DataField="docDes"   HeaderStyle-Width="120px"  ItemStyle-Width="120px"  ></telerik:GridBoundColumn>
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
               
            

                </fieldset>

        <fieldset>
                            <legend>Genera CFDI</legend>

                                    <div style="overflow:auto; width:270px; height:190px; background-color:transparent;">
                                        <asp:Panel ID="pnlCFDI" runat="server">
                                        <table border="0" style=" width:265px; text-align:left; background-color:transparent ;"> 
                                            <tr>
                                                 <td style="width:265px;">
                                                     <asp:CheckBox ID="CheckGenFac" runat="server" Checked="false" Text="Genera Factura" AutoPostBack="true" OnCheckedChanged="CheckGenFac_CheckedChanged" />
                                                 </td>    
                                            </tr>
                                            <tr>
                                                 <td style="width:265px;  ">
                                                     &nbsp;&nbsp;&nbsp;
                                                     <asp:CheckBox ID="CheckEnvCorr" runat="server" Checked="false" Text="Enviar Correo"  AutoPostBack="true" OnCheckedChanged="CheckEnvCorr_CheckedChanged" />
                                                 </td>    
                                            </tr>
                                            <tr>
                                                 <td style="width:265px;">
                                                     &nbsp;&nbsp;&nbsp;
                                                     <asp:CheckBox ID="CheckArchXml" runat="server" Checked="false" Text="Archivo XML" Enabled="false" />
                                                     <asp:CheckBox ID="CheckPDF" runat="server" Checked="false" Text="PDF" Enabled="false" />
                                                 </td>    
                                            </tr>
                                            <tr>
                                                 <td style="width:265px;">
                                                     &nbsp;&nbsp;&nbsp;
                                                     <telerik:RadLabel ID="RadLabel7" runat="server" Text="Copiar a:"></telerik:RadLabel>
                                                     <telerik:RadTextBox RenderMode="Lightweight" ID="RadTxtCopiA" runat="server" >
                                                         <ClientEvents OnValueChanging="TrimIt" />
                                                     </telerik:RadTextBox>


                                                    
                                                 </td>    
                                            </tr>
                                            <tr>
                                                 <td style="width:265px;">
                                                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                      <asp:RegularExpressionValidator ID="emailValidator" runat="server" Display="Dynamic"
                                                    ErrorMessage="e-mail invalido." ValidationExpression="^[\w\.\-]+@[a-zA-Z0-9\-]+(\.[a-zA-Z0-9\-]{1,})*(\.[a-zA-Z]{2,3}){1,2}$"
                                                    ControlToValidate="RadTxtCopiA">
                                                  </asp:RegularExpressionValidator>
                                                 </td>    
                                            </tr>
                                            <tr>
                                                 <td style="width:265px;">
                                                     <asp:CheckBox ID="CheckGuaArch" runat="server" Checked="false" Text="Guardar Archivo" AutoPostBack="true" Enabled="false" />
                                                 </td>    
                                            </tr>
                                            <tr>
                                                 <td style="width:265px;">
                                                     &nbsp;&nbsp;&nbsp;
                                                     <asp:CheckBox ID="CheckGuaXML" runat="server" Checked="false" Text="Archivo XML" Enabled="false" />
                                                     <asp:CheckBox ID="CheckGuaPDF" runat="server" Checked="false" Text="PDF" Enabled="false" />
                                                 </td>    
                                            </tr>
                                        </table>
                                        </asp:Panel>
                                    </div>
               
                        </fieldset> 



            </td>

            <td style=" width:700px; background-color:transparent; position:static; vertical-align:top">

                 <fieldset style="height:215px;  vertical-align:auto;" >   
                    <legend>Datos Generales</legend>
                        <div style="display:table; position:static; background-color:transparent;" >


                            <asp:Panel ID="pnlDatosGenerales" runat="server">

                            <table border="0" style=" width:680px; text-align:left; background-color:transparent;">
                                <tr style="height:20px; background-color:transparent;">
                                    <td style=" width:150px; background-color:transparent;">
                                        <telerik:RadLabel ID="RadLabel1" runat="server" Text="Documento"></telerik:RadLabel>  
                                    </td>
                                    <td style=" width:340px; background-color:transparent;">
                                        <telerik:RadTextBox ID="rTxtDocCve" runat="server" MaxLength="10" Width="150px"
                                            EnabledStyle-CssClass="cssTxtEnabled"
                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                            HoveredStyle-CssClass="cssTxtHovered"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid" 
                                         ></telerik:RadTextBox>
                                    </td>
                                        <td style=" width:225px; background-color:transparent;">
                                        <asp:CheckBox ID="rBtnChkProPar" runat="server" Text="Proceso Parcial" Checked="false" />
                                    
                                    </td>
                                </tr>
                                <tr style="height:20px; background-color:transparent;">
                                    <td style=" width:150px; background-color:transparent;">
                                        <telerik:RadLabel ID="RadLabel2" runat="server" Text="Descripción"></telerik:RadLabel>  
                                    </td>
                                    <td style=" width:50px; background-color:transparent;">
                                        <telerik:RadTextBox ID="rTxtDocDes"  runat="server"   MaxLength="50" Width="260px"
                                            EnabledStyle-CssClass="cssTxtEnabled"
                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                            HoveredStyle-CssClass="cssTxtHovered"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid" 
                                         ></telerik:RadTextBox>
                                    </td>
                                    <td style=" width:110px; background-color:transparent;">
                                        <asp:CheckBox ID="rBtnValExt" runat="server" Text="Valida Existencias" Checked="true" />
                                    </td>
                                     
                                    
                                </tr>
                                <tr style="height:20px; background-color:transparent;">
                                    <td style=" width:150px; background-color:transparent;">
                                        <telerik:RadLabel ID="RadLabel10" runat="server" Text="Manejo de Folio"></telerik:RadLabel> 
                                    </td>
                                    <td style=" width:340px; background-color:transparent;">                              
                                        <telerik:RadComboBox ID="rCboManejoFolio" runat="server" Width="260px" AutoPostBack="true" OnSelectedIndexChanged="rCboManejoFolio_SelectedIndexChanged"
                                            HighlightTemplatedItems="true"
                                          DropDownCssClass="cssRadComboBox" 
                                          DropDownWidth="260px" 
                                           Height="80px"  >
                                                <ItemTemplate>
                                                    <table style="width: 250px;"  cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width: 250px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "listPreValDes") %>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                </FooterTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td style=" width:110px; background-color:transparent;">
                                        <asp:CheckBox ID="rBtnReqAut" runat="server" Text="Req. Autorización" Checked="true"/>
                                    </td>
                                    
                                    
                                </tr>
                                <tr style="height:20px; background-color:transparent;">
                                    <td style=" width:150px; background-color:transparent;">
                                        <telerik:RadLabel ID="RadLabel11" runat="server" Text="Foliador"></telerik:RadLabel>  
                                    </td>
                                    <td style=" width:340px; background-color:transparent;">

                                        <telerik:RadComboBox ID="rcboFoliador" runat="server" Width="260px"  
                                            HighlightTemplatedItems="true" AutoPostBack="true"
                                          DropDownCssClass="cssRadComboBox" 
                                          DropDownWidth="260px" 
                                           Height="200px"  >
                                                <HeaderTemplate>
                                                    <table style="width: 150px" cellspacing="0" cellpadding="0">
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
                                                            <td style="width:80px" >
                                                                <%# DataBinder.Eval(Container.DataItem, "FolioCve")%>
                                                            </td>
                                                            <td style="width: 170px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "FolioDescripcion") %>
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
                                       &nbsp;<telerik:RadLabel ID="RadLabel3" runat="server" Text="Valida Credito"></telerik:RadLabel> 
                                    </td>
                                </tr>
                               <tr  style="height:20px; height:20px; background-color:transparent;">
                                   
                                    <td>
                                        <telerik:RadLabel ID="RadLabel9" runat="server" Width="155px" Text="Maneja Descuento"></telerik:RadLabel>
                                    </td>


                                    
                                    <td style=" width:190px; background-color:transparent;">
                                        <telerik:RadComboBox ID="rCboManejaDescuento"  runat="server" Width="260px" AutoPostBack="True"
                                            HighlightTemplatedItems="true"
                                          DropDownCssClass="cssRadComboBox" 
                                          DropDownWidth="260px" 
                                           Height="200px"  >
                                                <HeaderTemplate>
                                                    <table style="width: 250px" cellspacing="0" cellpadding="0">
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
                                                            <td style="width:80px" >
                                                                <%# DataBinder.Eval(Container.DataItem, "listPreValInt")%>
                                                            </td>
                                                            <td style="width: 170px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "listPreValDes")%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                </FooterTemplate>
                                        </telerik:RadComboBox>
                                        <%-- <asp:CheckBox ID="rBtnChkDescGlbl" runat="server"  Text="Maneja Descuento" Checked="false"/>--%>
                                    </td>
                                   <td style=" width:110px; background-color:transparent;">
                                        <asp:RadioButton ID="rBtnValCreSi" runat="server" GroupName="valCredito" Text="Si"  />
                                        <asp:RadioButton ID="rBtnValCreNo" runat="server" GroupName="valCredito" Text="No" Checked="true"   />
                                        <asp:RadioButton ID="rBtnValCreAut" runat="server" GroupName="valCredito" Text="Aut."  />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                       Maneja Textil
                                   </td>
                                   <td>
                                        <asp:RadioButton ID="rbtnManejoTextilSi" runat="server" GroupName="ManejoTextil" Text="Si"  />
                                        <asp:RadioButton ID="rbtnManejoTextilNo" runat="server" GroupName="ManejoTextil" Text="No" Checked="true"   />
                                   </td>
                                    <td>
                                        &nbsp;<telerik:RadLabel ID="RadLabel14" runat="server" Text="Tipo." width="150px"></telerik:RadLabel>
                                    </td>
                          

                                </tr>





                                <tr style="height:20px;">
                                    <td>
                                        <telerik:RadLabel ID="RadLabeMANEJALISTAPRECIOS" Visible="false" runat="server" Width="155px" Text="Maneja   L.Precios"></telerik:RadLabel>
                                    </td>
                                    <td>
                                                                               <telerik:RadComboBox ID="rCboManejaListaPrecios"   runat="server" Width="260px"  Visible="false"
                                            HighlightTemplatedItems="true" AutoPostBack="true"
                                          DropDownCssClass="cssRadComboBox" 
                                          DropDownWidth="260px" 
                                           Height="200px"  >
                                                <HeaderTemplate>
                                                    <table style="width: 150px" cellspacing="0" cellpadding="0">
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
                                                            <td style="width:50px" >
                                                                <%# DataBinder.Eval(Container.DataItem, "listPreValInt")%>
                                                            </td>
                                                            <td style="width: 200px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "listPreValDes") %>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                </FooterTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td style=" width:340px; background-color:transparent;">
                                        <asp:RadioButton ID="rBtnTipDocDeb" runat="server" GroupName="tipDoc" Text="Debito" Checked="true"/>
                                        <asp:RadioButton ID="rBtnTipDocCre" runat="server" GroupName="tipDoc" Text="Credito" />
                                    </td>
                                    <tr>

                                  <td>
                                       Aplica Incoterm
                                   </td>
                                   <td>
                                        <asp:RadioButton ID="rbtnAplicaIncotermSi" runat="server" GroupName="AplicaIncoterm" Text="Si"  />
                                        <asp:RadioButton ID="rbtnAplicaIncotermNo" runat="server" GroupName="AplicaIncoterm" Text="No" Checked="true"   />
                                   </td>
                                </tr>
                                </tr>

                                

                            </table>
      
                            </asp:Panel>

                        </div>
                    </fieldset>



                <fieldset style="height:115px;" >   
                    <legend>Actualiza Modulos</legend>
                        <div style=" display:table; position:static; background-color:transparent;" align="center" >
                            <asp:Panel ID="pnlActualizaModulos" runat="server">
                            <table border="0" style=" width:680px; text-align:left; background-color:transparent ;">

                                <tr style="height:18px;">
                                    
                                    <td style="width:125px; background-color:transparent;">
                                        <telerik:RadLabel ID="RadLabel4" runat="server" Text="Inventarios"></telerik:RadLabel>  
                                    </td>

                                    <td style=" width:170px;  background-color:transparent;">                             
                                        <asp:RadioButton ID="rBtnActInvAplica" runat="server" GroupName="actInventarios" Text="Reg" AutoPostBack="true" Checked="true"  OnCheckedChanged="rBtnActInvAplica_CheckedChanged" />
                                        <asp:RadioButton ID="rBtnActInvNo" runat="server" GroupName="actInventarios" Text="No"  AutoPostBack="true" OnCheckedChanged="rBtnActInvNo_CheckedChanged"  />
                                        <asp:RadioButton ID="rBtnActInvGenera" runat="server" GroupName="actInventarios" Text="Gen"  AutoPostBack="true"  OnCheckedChanged="rBtnActInvGenera_CheckedChanged"/>
                                    </td>

                                    <td>
                                        <telerik:RadLabel ID="RadLabel5" runat="server" Text="Concepto" Width="120px"></telerik:RadLabel>  
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="rCboConceptoInventarios" runat="server" Width="150px" AutoPostBack="True"
                                            HighlightTemplatedItems="true"
                                          DropDownCssClass="cssRadComboBox" 
                                          DropDownWidth="260px" 
                                           Height="200px"  >
                                                <HeaderTemplate>
                                                    <table style="width: 250px" cellspacing="0" cellpadding="0">
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
                                                            <td style="width:80px" >
                                                                <%# DataBinder.Eval(Container.DataItem, "cptoId")%>
                                                            </td>
                                                            <td style="width: 170px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "cptoDes")%>
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

                                <tr style="height:18px;">
                                    
                                    <td style=" width:100px; background-color:transparent;">
                                        <telerik:RadLabel ID="RadLabel6" runat="server" Text="Cuentas x Cobrar"></telerik:RadLabel>  
                                    </td>

                                    <td style=" width:170px;  background-color:transparent;">                             
                                        <asp:RadioButton ID="rBtnActCxcAplica" runat="server" GroupName="actCxcobrar" Text="Reg" Checked="true"  AutoPostBack="true" OnCheckedChanged="rBtnActCxcAplica_CheckedChanged"/>
                                        <asp:RadioButton ID="rBtnActCxcNo" runat="server" GroupName="actCxcobrar" Text="No"  AutoPostBack="true" OnCheckedChanged="rBtnActCxcNo_CheckedChanged"  />
                                        <asp:RadioButton ID="rBtnActCxcGenera" runat="server" GroupName="actCxcobrar" Text="Gen"  AutoPostBack="true"  OnCheckedChanged="rBtnActCxcGenera_CheckedChanged"/>
                                    </td>

                                    <td>
                                        <telerik:RadLabel ID="RadLabel15" runat="server" Text="Concepto" Width="120px"></telerik:RadLabel>  
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="rCboConceptoCuentasxCobrar" runat="server" Width="150px" AutoPostBack="True"
                                            HighlightTemplatedItems="true"
                                          DropDownCssClass="cssRadComboBox" 
                                          DropDownWidth="260px" 
                                           Height="200px"  >
                                                <HeaderTemplate>
                                                    <table style="width: 250px" cellspacing="0" cellpadding="0">
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
                                                            <td style="width:80px" >
                                                                <%# DataBinder.Eval(Container.DataItem, "cptoId")%>
                                                            </td>
                                                            <td style="width: 170px;">
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

                                </tr>

                                <tr style="height:18px;">
                                    
                                    <td style=" width:150px; background-color:transparent;">
                                        <telerik:RadLabel ID="RadLabel16" runat="server" Text="Contabilidad"></telerik:RadLabel>  
                                    </td>

                                    <td style=" width:170px;  background-color:transparent;">                             
                                        <asp:RadioButton ID="rBtnActContAplica" runat="server" GroupName="actContabilidad" Text="Reg" Checked="true"  AutoPostBack="true" OnCheckedChanged="rBtnActContAplica_CheckedChanged" />
                                        <asp:RadioButton ID="rBtnActContNo" runat="server" GroupName="actContabilidad" Text="No"  AutoPostBack="true" OnCheckedChanged="rBtnActContNo_CheckedChanged" />
                                        <asp:RadioButton ID="rBtnActContGenera" runat="server" GroupName="actContabilidad" Text="Gen"  AutoPostBack="true" OnCheckedChanged="rBtnActContGenera_CheckedChanged"/>
                                    </td>
                                    <td>
                                        <telerik:RadLabel ID="RadLabel17" runat="server" Text="Concepto" Width="120px"></telerik:RadLabel>  
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="rCboConceptoContabilidad" runat="server" Width="150px" AutoPostBack="True"
                                            HighlightTemplatedItems="true"
                                          DropDownCssClass="cssRadComboBox" 
                                          DropDownWidth="260px" 
                                           Height="200px"  >
                                               <HeaderTemplate>
                                                    <table style="width: 250px" cellspacing="0" cellpadding="0">
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
                                                            <td style="width:80px" >
                                                                <%# DataBinder.Eval(Container.DataItem, "cptoId")%>
                                                            </td>
                                                            <td style="width: 170px;">
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

                                </tr>

                            </table>           
                            </asp:Panel>
                        </div>
                    </fieldset>


                 <fieldset style="height:115px;">   
                    <legend>Documentos Derivados</legend>

                        <asp:Panel ID="pnlDocumentosDerivados" runat="server">
                        <div style="display:table; position:static; background-color:transparent;" >

                            <table border="0" style=" width:650px; text-align:left; background-color:transparent ;">
                                
                                  <tr style="height:18px;">

                                    <td style="width:150px; background-color:transparent;">
                                        <telerik:RadLabel ID="RadLabel25" runat="server" Text="Gen. Doc. 1"></telerik:RadLabel>  
                                    </td>
                                    <td style="width:150px;  background-color:transparent;">                             
                                        
                                        <!-- Combo de Formatos-->
                                        <telerik:RadComboBox ID="rCboGenDoc1" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="rCboGenDoc1_SelectedIndexChanged"
                                            HighlightTemplatedItems="true"
                                          DropDownCssClass="cssRadComboBox" 
                                          DropDownWidth="260px" 
                                           Height="180px"  >
                                                <ItemTemplate>
                                                    <table style="width: 250px;"  cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width: ;">
                                                                <%# DataBinder.Eval(Container.DataItem, "docGenDes") %>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate> 
                                                <FooterTemplate>
                                                    <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                </FooterTemplate>
                                        </telerik:RadComboBox>


                                    </td>
                                    <td style=" width:150px; background-color:transparent;">
                                        <telerik:RadLabel ID="RadLabel26" runat="server" Text="Documento 1"></telerik:RadLabel>  
                                    </td>
                                    <td style=" width:150px;  background-color:transparent;">     
                                        
                                        <telerik:RadComboBox ID="rCboDocumento1" runat="server" Width="150px" AutoPostBack="True"
                                            HighlightTemplatedItems="true"
                                          DropDownCssClass="cssRadComboBox" 
                                          DropDownWidth="280px" 
                                           Height="180px"  >
                                                <HeaderTemplate>
                                                    <table style="width: 270px" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 80px;">
                                                                    Clave
                                                                </td>
                                                                <td style="width: 190px;">
                                                                    Descripción
                                                                </td>
                                                            </tr>
                                                        </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table style="width: 270px;"  cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width:80px" >
                                                                <%# DataBinder.Eval(Container.DataItem, "docCve")%>
                                                            </td>
                                                            <td style="width: 190px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "docDes") %>
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




                                  <tr style="height:18px;">



                                    <td style=" width:150px; background-color:transparent;">
                                        <telerik:RadLabel ID="RadLabel27" runat="server" Text="Gen. Doc. 2"></telerik:RadLabel>  
                                    </td>
                                    <td style=" width:150px;  background-color:transparent;">                             
                                        
                                        <!-- Combo de Formatos-->

                                        <telerik:RadComboBox ID="rCboGenDoc2" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="rCboGenDoc2_SelectedIndexChanged"
                                            HighlightTemplatedItems="true"
                                          DropDownCssClass="cssRadComboBox" 
                                          DropDownWidth="260px" 
                                           Height="180px"  >

                                                <ItemTemplate>
                                                    <table style="width: 250px;"  cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width: 250px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "docGenDes") %>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                </FooterTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td style=" width:150px; background-color:transparent;">
                                        <telerik:RadLabel ID="RadLabel28" runat="server" Text="Documento 2"></telerik:RadLabel>  
                                    </td>
                                    <td style=" width:150px;  background-color:transparent;">     
                                        
                                                                           
                                        <telerik:RadComboBox ID="rCboDocumento2" runat="server" Width="150px" AutoPostBack="True"
                                            HighlightTemplatedItems="true"
                                          DropDownCssClass="cssRadComboBox" 
                                          DropDownWidth="280px" 
                                           Height="200px"  >
                                                <HeaderTemplate>
                                                    <table style="width: 270px" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 80px;">
                                                                    Clave
                                                                </td>
                                                                <td style="width: 190px;">
                                                                    Descripción
                                                                </td>
                                                            </tr>
                                                        </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table style="width: 270px;"  cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width:80px" >
                                                                <%# DataBinder.Eval(Container.DataItem, "docCve")%>
                                                            </td>
                                                            <td style="width: 190px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "docDes") %>
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




                                  <tr style="height:18px;">



                                    <td style=" width:150px; background-color:transparent;">
                                        <telerik:RadLabel ID="RadLabel29" runat="server" Text="Gen. Doc. 3"></telerik:RadLabel>  
                                    </td>
                                    <td style=" width:150px;  background-color:transparent;">                             
                                        
                                        <!-- Combo de Formatos-->

                                        <telerik:RadComboBox ID="rCboGenDoc3" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="rCboGenDoc3_SelectedIndexChanged"
                                            HighlightTemplatedItems="true"
                                          DropDownCssClass="cssRadComboBox" 
                                          DropDownWidth="260px" 
                                           Height="180px"  >
                                                <ItemTemplate>
                                                    <table style="width: 250px;"  cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width: ;">
                                                                <%# DataBinder.Eval(Container.DataItem, "docGenDes") %>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate> 
                                                <FooterTemplate>
                                                    <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                </FooterTemplate>
                                        </telerik:RadComboBox>

                                    </td>
                                    <td style=" width:150px; background-color:transparent;">
                                        <telerik:RadLabel ID="RadLabel30" runat="server" Text="Documento 3"></telerik:RadLabel>  
                                    </td>
                                    <td style=" width:150px;  background-color:transparent;">     
                                        
                                        <telerik:RadComboBox ID="rCboDocumento3" runat="server" Width="150px" AutoPostBack="True"
                                            HighlightTemplatedItems="true"
                                          DropDownCssClass="cssRadComboBox" 
                                          DropDownWidth="280px" 
                                           Height="200px"  >
                                                <HeaderTemplate>
                                                    <table style="width: 270px" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 80px;">
                                                                    Clave
                                                                </td>
                                                                <td style="width: 190px;">
                                                                    Descripción
                                                                </td>
                                                            </tr>
                                                        </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table style="width: 270px;"  cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width:80px" >
                                                                <%# DataBinder.Eval(Container.DataItem, "docCve")%>
                                                            </td>
                                                            <td style="width: 190px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "docDes") %>
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
                        </asp:Panel>
                    </fieldset>
                <fieldset style="height:75px;">
                    <legend>Aplicacion Estadistica</legend>
                    <asp:Table runat="server">
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label runat="server" Text="Unidad de Manejo" Width="156px"></asp:Label>
                             </asp:TableCell>
                            <asp:TableCell Width="160px">
                                <asp:RadioButton GroupName="UnidadMed" id="radTotal" runat="server" Text="Total"  Checked="true" ></asp:RadioButton>&nbsp;&nbsp;
                                <asp:RadioButton GroupName="UnidadMed" ID="radSurtido" runat="server" Text="Surtido" ></asp:RadioButton>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" Text="Concepto" Width="160px"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <telerik:RadComboBox ID="rCboConceptoEstadistico" runat="server" Width="150px" AutoPostBack="True" Enabled="false"
                                            HighlightTemplatedItems="true" AllowCustomText="true"   
                                          DropDownCssClass="cssRadComboBox" 
                                          DropDownWidth="280px" 
                                           Height="200px"  >
                                                <HeaderTemplate>
                                                    <table style="width: 270px" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 80px;">
                                                                    Clave
                                                                </td>
                                                                <td style="width: 190px;">
                                                                    Descripción
                                                                </td>
                                                            </tr>
                                                        </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table style="width: 270px;"  cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width:80px" >
                                                                <%# DataBinder.Eval(Container.DataItem, "cptoEstId")%>
                                                            </td>
                                                            <td style="width: 190px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "cptoEstDes") %>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                </FooterTemplate>
                                        </telerik:RadComboBox>
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell>

                            </asp:TableCell>
                            <asp:TableCell >
                                <asp:RadioButton GroupName="UnidadMed" id="radPedido" runat="server" Text="Pedido"  ></asp:RadioButton>&nbsp;&nbsp;
                                <asp:RadioButton GroupName="UnidadMed" ID="radPendiente" runat="server" Text="Pdte." ></asp:RadioButton>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" Text="Forma Aplicacion" Width="140px"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <telerik:RadComboBox ID="rCboFormaAplicacion"  runat="server" Width="150px" AutoPostBack="True" Enabled="false"
                                            HighlightTemplatedItems="true"
                                          DropDownCssClass="cssRadComboBox" 
                                          DropDownWidth="280px" 
                                           Height="200px">
                                                <HeaderTemplate>
                                                    <table style="width: 270px" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 80px;">
                                                                    Clave
                                                                </td>
                                                                <td style="width: 190px;">
                                                                    Descripción
                                                                </td>
                                                            </tr>
                                                        </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table style="width: 270px;"  cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width:80px" >
                                                                <%# DataBinder.Eval(Container.DataItem, "listPreValStr")%>
                                                            </td>
                                                            <td style="width: 190px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "listPreValDes") %>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                </FooterTemplate>
                                        </telerik:RadComboBox>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    </fieldset>


                
                <!-- Referencias y Variables de Documentos -->
                <div style=" width:100%; display:table; background-color:transparent  ">
                    <asp:Panel ID="pnlRefVar" runat="server">

  
                    <fieldset style="width:330px; height:120px;  display: block;  float:left;"  >   
                        <legend>Referencias</legend>
   
                        <asp:Panel ID="Panel1" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
                            
                            <telerik:RadTextBox ID="txtValRef" MaxLength="15"  runat="server" Width="150px" 
                                                    EnabledStyle-CssClass="cssTxtEnabled"
                                                    DisabledStyle-CssClass ="cssTxtEnabled"
                                                    HoveredStyle-CssClass="cssTxtHovered"
                                                    FocusedStyle-CssClass="cssTxtFocused"
                                                    InvalidStyle-CssClass="cssTxtInvalid">
                                                    </telerik:RadTextBox>
                            
                            <telerik:RadImageButton ID="rBtnNewRef"     runat ="server" Width="30px" Height="25px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text ="" OnClick="rBtnNewRef_Click"></telerik:RadImageButton>
                            <telerik:RadImageButton ID="rBtnEdiRef" runat="server"  Width="30px" Height="25px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  ToolTip="Modificar"  Text="" OnClick="rBtnEdiRef_Click"></telerik:RadImageButton>
                            <telerik:RadImageButton ID="rBtnDelRef"  runat="server"  Width="30px" Height="25px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text="" OnClick="rBtnDelRef_Click"></telerik:RadImageButton>
                            <telerik:RadImageButton ID="rBtnAceptarR"   runat="server"  Width="30px" Height="25px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnAutorizarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnAutorizar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnAutorizarHovered.png"  ToolTip="Aceptar" OnClick="rBtnAceptarR_Click"  Text=""></telerik:RadImageButton>    
                        </asp:Panel>

                        <div style="overflow:auto; width:330px; height:80px;" >
                        
                            <telerik:RadGrid ID="rGdv_Referencias" 
                                           runat="server"  
                                           AutoGenerateColumns="False" 
                                           OnSelectedIndexChanged="rGdv_Referencias_SelectedIndexChanged"
                                           Width="310px" Height="60px"
                                           CssClass="Grid" 
                                           Skin="Office2010Silver"     
                                              >
                                <MasterTableView DataKeyNames="revasec" AutoGenerateColumns="False" CssClass="GridTable">
                                    <Columns>
                                        <telerik:GridBoundColumn HeaderText="Referencia"        DataField="revaDes" HeaderStyle-Width="40px"   ItemStyle-Width="40px" />
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
                              
                        </div>
  
                    </fieldset>  

  
                    <fieldset style="  width:330px; height:120px; display: block; float:right;     ">
                        <legend>Variables</legend>

                        <asp:Panel ID="Panel2" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
                            
                            <telerik:RadTextBox ID="txtValVar" MaxLength="15"  runat="server" Width="150px" 
                                                    EnabledStyle-CssClass="cssTxtEnabled"
                                                    DisabledStyle-CssClass ="cssTxtEnabled"
                                                    HoveredStyle-CssClass="cssTxtHovered"
                                                    FocusedStyle-CssClass="cssTxtFocused"
                                                    InvalidStyle-CssClass="cssTxtInvalid">
                                                    </telerik:RadTextBox>
                            
                            <telerik:RadImageButton ID="rBtnNewVar"     runat ="server" Width="30px" Height="25px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text ="" OnClick="rBtnNewVar_Click"></telerik:RadImageButton>
                            <telerik:RadImageButton ID="rBtnEdiVar" runat="server"  Width="30px" Height="25px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  ToolTip="Modificar"  Text="" OnClick="rBtnEdiVar_Click"></telerik:RadImageButton>
                            <telerik:RadImageButton ID="rBtnDelVar"  runat="server"  Width="30px" Height="25px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text="" OnClick="rBtnDelVar_Click"></telerik:RadImageButton>
                            <telerik:RadImageButton ID="rBtnAceptarV"   runat="server"  Width="30px" Height="25px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnAutorizarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnAutorizar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnAutorizarHovered.png"  ToolTip="Limpiar" OnClick="rBtnAceptarV_Click"  Text=""></telerik:RadImageButton>    
                        </asp:Panel>

                        <div  style="overflow:auto; width:330px; height:100px;" >

                            <telerik:RadGrid ID="rGdv_Variables" 
                                           runat="server"  
                                           AutoGenerateColumns="False" 
                                           OnSelectedIndexChanged="rGdv_Variables_SelectedIndexChanged"
                                           Width="310px" Height="60px"
                                           CssClass="Grid" 
                                           Skin="Office2010Silver"     
                                              >
                                <MasterTableView DataKeyNames="revasec" AutoGenerateColumns="False" CssClass="GridTable">
                                    <Columns>
                                        <telerik:GridBoundColumn HeaderText="Variable"        DataField="revaDes" HeaderStyle-Width="40px"   ItemStyle-Width="40px" />
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

                        </div>

                    </fieldset>

                    </asp:Panel>   
                 </div>



            </td>

            <td aling="center" style=" width:300px;background-color:transparent; position:static; vertical-align:top  ;text-align:center;">

            
                   <fieldset>   
                        <legend>Layout de Impresión</legend>
                            <asp:Panel ID="pnlLayoutImpresion" runat="server">
                        
                                   <telerik:RadImageGallery ShowLoadingPanel="false" RenderMode="Lightweight" ID="RadImageGallery1" runat="server" 
                                        DataDescriptionField="formImpDes" DataImageField="formImpImg" 
                                        DataTitleField="formImpCve" Width="295px" Height="195px" Culture="es-ES"   >
                                   
                                       <ThumbnailsAreaSettings Width="0px" Height="0px"  EnableZoneScroll="false"   />

                                       <ToolbarSettings  ShowThumbnailsToggleButton="false"  ShowSlideshowButton="false"   />

                                       <ImageAreaSettings ShowNextPrevImageButtons="false"  />
                                       
                                  </telerik:RadImageGallery>

                            </asp:Panel>
                    </fieldset>

                   <fieldset >   
                    <legend>Formato Registro</legend>
                        <%--<div style=" height:10px; display:table; position:static; background-color:transparent;" align="center" >
                            <table border="0" style=" width:300px; text-align:left; background-color:transparent ;">
                            </table>           
                        </div>--%>

                        <div  style="overflow:auto; width:300px; height:455px; background-color:transparent;" >
                         <asp:Panel ID="pnlFormatoRegistro" runat="server">
                         
                         <asp:DataList ID="DataListDocFormatoRegOpcPantalla" runat="server" DataKeyField="DocOpcPantCve" >
                             <ItemTemplate>


                                 
                                <table  border="0" style=" width:280px; text-align:left; background-color:transparent ;">
                                     <tr style="height:15px; ">
                                         <td style="width:180px;">
                                             <telerik:RadLabel ID="rLblDes" runat="server" Text='<%# Eval("DocOpcPantDes") %>'  Width="180px"  ></telerik:RadLabel>
                                         </td>
                                         <td style=" width:100px; background-color:transparent;  ">
                                            <telerik:RadButton RenderMode="Lightweight" ID="rBtnTog" Width="100px" runat="server" ToggleType="CheckBox" ButtonType="StandardButton" Checked='<%# Eval("docFormRegSitCheck") %>'
                                                AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="Si" CssClass="Checked"  Width="80px"   Value="1" ></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="notChecked"  Width="80px"  Value="2" ></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                         </tb>
                                     </tr>
                                 </table>

                             </ItemTemplate>
                         </asp:DataList>

                         </asp:Panel>
                       </div>



                    </fieldset>
                


            </td>


        </tr>
    </table>

        <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
            <telerik:RadImageButton ID="rBtnGuardar"   runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text ="" OnClick="rBtnGuardar_Click"  OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
            <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text="" OnClick="rBtnCancelar_Click"  OnClientClicking="OnClientClic_ConfirmCancel"></telerik:RadImageButton>
        </asp:Panel>

    <asp:HiddenField ID="hdfBtnAccion" runat="server" />
    <asp:HiddenField ID="hdfBtnAccionR" runat="server" />
    <asp:HiddenField ID="hdfBtnAccionV" runat="server" />

    <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"   >
    </telerik:RadWindowManager> 
    <telerik:RadWindow runat="server" ID="FNMttoDocPermisos" Width="780px" Height="580px" Modal="true"  VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close" Title="Documento Seguridad" >
    </telerik:RadWindow>

    <telerik:RadWindow runat="server" ID="FNMttoDocCFDI" Width="580px" Height="250px" Modal="true"       VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close"  >
    </telerik:RadWindow>

    <telerik:RadWindow runat="server" ID="FNMtto" Width="655px" Height="480px" Modal="true"              VisibleStatusbar   ="false" VisibleTitlebar="true" Behaviors="Close" >
    </telerik:RadWindow>
    </div>

    </ContentTemplate>
    </asp:UpdatePanel> 

</asp:Content>

