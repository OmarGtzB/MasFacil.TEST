<%@ Page Title="" Language="C#" MasterPageFile="~/MPForm.master" AutoEventWireup="true" CodeFile="MttDocDatosGrales.aspx.cs" Inherits="DC_MttDocDatosGrales" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMPForm" Runat="Server">
    
    <script src="http://code.jquery.com/jquery-1.11.0.min.js"></script>

    
    <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
    <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
    <link href="~/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />
    <link href="../css/styles.css" rel="stylesheet" type="text/css" />


    <script>

        function showFormatsModal(sender, args) {
            var rop = radopen("", "MttoArtCodigo.aspx", 950, 610);
            rop;
        }

    </script>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMPForm" Runat="Server">



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
       <ContentTemplate>


    <table border="0" style="width:100%; text-align:left; background-color:transparent ;">
        <tr >

            <td style=" width:250px; background-color:transparent;">


                <!-- Opciones de Documentos -->
                <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
                    <telerik:RadImageButton ID="rBtnNuevo"     runat ="server" Width="25px" Height="25px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png"  Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text ="" OnClick="rBtnNuevo_Click"></telerik:RadImageButton>
                    <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="25px" Height="25px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  ToolTip="Modificar"  Text="" OnClick="rBtnModificar_Click" AutoPostBack="true"></telerik:RadImageButton>
                    <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="25px" Height="25px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text="" OnClick="rBtnEliminar_Clicked" AutoPostBack="true"></telerik:RadImageButton>
                    <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="25px" Height="25px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar" OnClick="rBtnLimpiar_Click"  Text="" AutoPostBack="true"></telerik:RadImageButton>    
                </asp:Panel>
                <!-- Listado de Documentos -->
                <fieldset>
                    <legend>Documentos</legend>

                    

                    <div style="width:100%; margin-top:5px; display:table; position:static; background-color:transparent;" align="center" >  
 
                         <telerik:RadGrid ID="rGdv_Documentos" 
                                           runat="server" 
                                           AutoGenerateColumns="False" 

                                           Width="250px"   Height="505px"   
                                           CssClass="Grid" 
                                           Skin="Office2010Silver" OnSelectedIndexChanged="rGdv_Documentos_SelectedIndexChanged">

                                <MasterTableView DataKeyNames="docCve"  AutoGenerateColumns="false" CssClass="GridTable"   >
                                    <Columns> 
                                        <telerik:GridBoundColumn HeaderText="Clave Documento"         DataField="docCve"  HeaderStyle-Width="100px"  ItemStyle-Width="100px" ></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Descripción"  DataField="docDes"   HeaderStyle-Width="100px"  ItemStyle-Width="100px"  ></telerik:GridBoundColumn>
                                  <%--      <telerik:GridBoundColumn HeaderText="Abreviatura"  DataField="docAbre"   HeaderStyle-Width="140px"  ItemStyle-Width="140px" ></telerik:GridBoundColumn>                                       
                                  --%>  </Columns>
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
                    </div>


                </fieldset>

                



            </td>

            <!-- Segundo Apratdo Grid Documentos-->

          


            <td style=" width:500px; background-color:transparent;">
                <!-- Formulario Datos Generales-->
                <div style=" height:38px;"></div>
                 <fieldset style="height:143px;  vertical-align:auto;" >   
                    <legend>Datos Generales</legend>
                        <div style="display:table; position:static; background-color:transparent;" >
                            <table border="0" style=" width:600px; text-align:left; background-color:transparent;">
                                <tr style="height:18px;">
                                    <td style=" width:150px; background-color:transparent;">
                                        <telerik:RadLabel ID="RadLabel1" runat="server" Text="Documento"></telerik:RadLabel>  
                                    </td>
                                    <td style=" width:150px;  background-color:transparent;">                             
                                        <telerik:RadTextBox ID="rTxtDocCve" runat="server" MaxLength="10"
                                            EnabledStyle-CssClass="cssTxtEnabled"
                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                            HoveredStyle-CssClass="cssTxtHovered"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid" 
                                         ></telerik:RadTextBox>
                                    </td>
                                    <td style=" width:150px; background-color:transparent;">
                                        <telerik:RadLabel ID="RadLabel2" runat="server" Text="Descripción"></telerik:RadLabel>  
                                    </td>
                                    <td style=" width:150px;  background-color:transparent;">                             
                                        <telerik:RadTextBox ID="rTxtDocDes"  runat="server"   MaxLength="50"
                                            EnabledStyle-CssClass="cssTxtEnabled"
                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                            HoveredStyle-CssClass="cssTxtHovered"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid" 
                                         ></telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr style="height:18px;">



                                    <td style=" width:150px; background-color:transparent;">
                                        <telerik:RadLabel ID="RadLabel10" runat="server" Text="Manejo de Folio"></telerik:RadLabel>  
                                    </td>
                                    <td style=" width:150px;  background-color:transparent;"> 
                                                                    
                                        <telerik:RadComboBox ID="rCboManejoFolio" runat="server" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="rCboManejoFolio_SelectedIndexChanged"
                                            HighlightTemplatedItems="true"
                                          DropDownCssClass="cssRadComboBox" 
                                          DropDownWidth="150px" 
                                           Height="200px"  >
                                                <HeaderTemplate>
                                                    <table style="width: 150px" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 260px;">
                                                                    Clave
                                                                </td>
                                                                <td style="width: 260px;">
                                                                    Descripción
                                                                </td>
                                                                <td style="width: 260px;">
                                                                    pruebas
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
                                    <td style=" width:150px; background-color:transparent;">
                                        <telerik:RadLabel ID="RadLabel11" runat="server" Text="Numero de Folio"></telerik:RadLabel>  
                                    </td>
                                    <td style=" width:150px;  background-color:transparent;">     
                                        
                                        <telerik:RadNumericTextBox ID="RadNumCveFolio" MaxValue="50" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="2"
                                        runat="server"
                                            EnabledStyle-CssClass="cssTxtEnabled"
                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                            HoveredStyle-CssClass="cssTxtHovered"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid" 
                                        ></telerik:RadNumericTextBox>

                                    </td>
                                </tr>


                                <tr style="height:18px;">

                                    
                                    <td style=" width:150px; background-color:transparent;">
                                        <telerik:RadLabel ID="RadLabel14" runat="server" Text="Tipo." width="150px"></telerik:RadLabel>
                                    
                                    </td>

                                    <td style=" width:150px; background-color:transparent;"> 
                                        
                                        <asp:RadioButton ID="rBtnTipDocCre" runat="server" GroupName="tipDoc" Text="Credito" Checked="true"  AutoPostBack="true"/>
                                        <asp:RadioButton ID="rBtnTipDocDeb" runat="server" GroupName="tipDoc" Text="Debito"  AutoPostBack="true"/>
                                        
                            
                                    </td>









                                    <td style=" width:150px; background-color:transparent;">
                                        <telerik:RadLabel ID="RadLabel13" runat="server" Text="Valida Credito." width="150px"></telerik:RadLabel>
                                    </td>


                                    <td style=" width:150px; background-color:transparent;"> 
                                        
                                        <asp:RadioButton ID="rBtnValCreYes" runat="server" GroupName="valCredito" Text="Si" Checked="true"  AutoPostBack="true"/>
                                        <asp:RadioButton ID="rBtnValCreNo" runat="server" GroupName="valCredito" Text="No"  AutoPostBack="true"/>
                                        <asp:RadioButton ID="rBtnValCreAut" runat="server" GroupName="valCredito" Text="Aut."  AutoPostBack="true"/>
                            
                                    </td>

                                </tr>
                                    



                                <tr style="height:18px;">

                                    <td style=" width:150px; background-color:transparent;">
                                        <asp:CheckBox ID="rBtnChkDescGlbl" runat="server"  Text="Descuento Global" Checked="true"  AutoPostBack="true"/>
                                    </td>
                                    <td style=" width:150px;  background-color:transparent;">                             
                                        
                                        <!-- Combo de Formatos-->
                                        <asp:CheckBox ID="rBtnChkProPar" runat="server" Text="Proceso Parcial" Checked="true"  AutoPostBack="true"/>
                                        
                                    </td>

                                    <td style=" width:150px; background-color:transparent;">
                                    
                                        <asp:CheckBox ID="rBtnReqAut" runat="server" Text="Req. Autorización" Checked="true"  AutoPostBack="true"/>

                                    </td>

                                    <td style=" width:150px; background-color:transparent;">
                                        
                                        <asp:CheckBox ID="rBtnValExt" runat="server" Text="Valida Existencias" Checked="true"  AutoPostBack="true"/>
                                    </td>



                                </tr>


                                


                            </table>           

                        </div>
                    </fieldset>
                <fieldset style="height:115px;" >   
                    <legend>Actualiza Modulos</legend>
                        <div style=" display:table; position:static; background-color:transparent;" align="center" >

                            <table border="0" style=" width:680px; text-align:left; background-color:transparent ;">
                                <tr style="height:18px;">
                                    
                                    <td style="width:125px; background-color:transparent;">
                                        <telerik:RadLabel ID="RadLabel4" runat="server" Text="Inventarios"></telerik:RadLabel>  
                                    </td>

                                    <td style=" width:170px;  background-color:transparent;">                             
                                        <asp:RadioButton ID="rBtnActInvAplica" runat="server" GroupName="actInventarios" Text="Apli" AutoPostBack="true" Checked="true" OnCheckedChanged="rBtnActInvAplica_CheckedChanged"  />
                                        <asp:RadioButton ID="rBtnActInvNo" runat="server" GroupName="actInventarios" Text="No"  AutoPostBack="true" OnCheckedChanged="rBtnActInvNo_CheckedChanged" />
                                        <asp:RadioButton ID="rBtnActInvGenera" runat="server" GroupName="actInventarios" Text="Gen"  AutoPostBack="true" OnCheckedChanged="rBtnActInvGenera_CheckedChanged"/>
                                    </td>

                                    <td>
                                        <telerik:RadLabel ID="RadLabel5" runat="server" Text="Concepto" Width="120px"></telerik:RadLabel>  
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="rCboInventarios" runat="server" Width="150px" AutoPostBack="True"
                                            HighlightTemplatedItems="true"
                                          DropDownCssClass="cssRadComboBox" 
                                          DropDownWidth="150px" 
                                           Height="200px"  >
                                                <HeaderTemplate>
                                                    <table style="width: 150px" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 260px;">
                                                                    Clave
                                                                </td>
                                                                <td style="width: 260px;">
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
                                        <asp:RadioButton ID="rBtnActCxcAplica" runat="server" GroupName="actCxcobrar" Text="Apli" Checked="true"  AutoPostBack="true" OnCheckedChanged="rBtnActCxcAplica_CheckedChanged"/>
                                        <asp:RadioButton ID="rBtnActCxcNo" runat="server" GroupName="actCxcobrar" Text="No"  AutoPostBack="true" OnCheckedChanged="rBtnActCxcNo_CheckedChanged" />
                                        <asp:RadioButton ID="rBtnActCxcGenera" runat="server" GroupName="actCxcobrar" Text="Gen"  AutoPostBack="true" OnCheckedChanged="rBtnActCxcGenera_CheckedChanged" />
                                    </td>

                                    <td>
                                        <telerik:RadLabel ID="RadLabel15" runat="server" Text="Concepto" Width="120px"></telerik:RadLabel>  
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="rCboCuentasxCobrar" runat="server" Width="150px" AutoPostBack="True"
                                            HighlightTemplatedItems="true"
                                          DropDownCssClass="cssRadComboBox" 
                                          DropDownWidth="150px" 
                                           Height="200px"  >
                                                <HeaderTemplate>
                                                    <table style="width: 150px" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 260px;">
                                                                    Clave
                                                                </td>
                                                                <td style="width: 260px;">
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
                                        <asp:RadioButton ID="rBtnActContAplica" runat="server" GroupName="actContabilidad" Text="Apli" Checked="true"  AutoPostBack="true" OnCheckedChanged="rBtnActContAplica_CheckedChanged"/>
                                        <asp:RadioButton ID="rBtnActContNo" runat="server" GroupName="actContabilidad" Text="No"  AutoPostBack="true" OnCheckedChanged="rBtnActContNo_CheckedChanged"/>
                                        <asp:RadioButton ID="rBtnActContGenera" runat="server" GroupName="actContabilidad" Text="Gen"  AutoPostBack="true" OnCheckedChanged="rBtnActContGenera_CheckedChanged"/>
                                    </td>
                                    <td>
                                        <telerik:RadLabel ID="RadLabel17" runat="server" Text="Concepto" Width="120px"></telerik:RadLabel>  
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="rCboContabilidad" runat="server" Width="150px" AutoPostBack="True"
                                            HighlightTemplatedItems="true"
                                          DropDownCssClass="cssRadComboBox" 
                                          DropDownWidth="150px" 
                                           Height="200px"  >
                                               <HeaderTemplate>
                                                    <table style="width: 150px" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 260px;">
                                                                    Clave
                                                                </td>
                                                                <td style="width: 260px;">
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

                        </div>
                    </fieldset>


                 <fieldset style="height:115px;">   
                    <legend>Documentos Derivados</legend>

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
                                          DropDownWidth="150px" 
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
                                                                <%# DataBinder.Eval(Container.DataItem, "docGenId")%>
                                                            </td>
                                                            <td style="width: 170px;">
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
                                          DropDownWidth="150px" 
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
                                                                <%# DataBinder.Eval(Container.DataItem, "docCve")%>
                                                            </td>
                                                            <td style="width: 170px;">
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
                                          DropDownWidth="150px" 
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
                                                                <%# DataBinder.Eval(Container.DataItem, "docGenId")%>
                                                            </td>
                                                            <td style="width: 170px;">
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
                                          DropDownWidth="150px" 
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
                                                                <%# DataBinder.Eval(Container.DataItem, "docCve")%>
                                                            </td>
                                                            <td style="width: 170px;">
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
                                          DropDownWidth="150px" 
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
                                                                <%# DataBinder.Eval(Container.DataItem, "docGenId")%>
                                                            </td>
                                                            <td style="width: 170px;">
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
                                          DropDownWidth="150px" 
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
                                                                <%# DataBinder.Eval(Container.DataItem, "docCve")%>
                                                            </td>
                                                            <td style="width: 170px;">
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
                    </fieldset>

                 

                <!-- Referencias y Variables de Documentos -->
                <div style=" width:700px; display:table;  ">

  
                    <fieldset style="    width:325px; height:110px;  display: block;  float:left;"  >   
                    <legend>Referencias</legend>
   
                          <div style="overflow:auto; width:325px; height:110px;" >
                                <asp:DataList ID="dt_referencias" runat="server" DataKeyField="revasec">
                                    <ItemTemplate>


                                       <table  border="0" style=" width:320px; height:10px; text-align:left; background-color:transparent ;">
                                             <tr style="height:15px; ">
                                                <td style="width:150px;">
                                                  <telerik:RadLabel ID="lbrad" runat="server" Text='<%# Eval("revaDes") %>' Width="150px" ></telerik:RadLabel>
                                                </td>
                                                <td style=" width:150px;  background-color:transparent;"> 
                                                    <telerik:RadTextBox ID="txt_ref" MaxLength="15"  runat="server" Width="150px" Text='<%# Eval("revaValRef") %>' 
                                                    EnabledStyle-CssClass="cssTxtEnabled"
                                                    DisabledStyle-CssClass ="cssTxtEnabled"
                                                    HoveredStyle-CssClass="cssTxtHovered"
                                                    FocusedStyle-CssClass="cssTxtFocused"
                                                    InvalidStyle-CssClass="cssTxtInvalid">
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                        </table>


                                </ItemTemplate>
                            </asp:DataList>
                        </div>
  
                    </fieldset>  

  
                    <fieldset style="  width:325px; height:110px; display: block; float:right;     ">
                        <legend>Variables</legend>
                        <div  style="overflow:auto; width:325px; height:110px;" >
                         <asp:DataList ID="dt_variables" runat="server" DataKeyField="revasec" >
                             <ItemTemplate>
                                <table  border="0" style=" width:320px; height:10px; text-align:left; background-color:transparent ;">
                                     <tr style="height:15px; ">
                                         <td style="width:150px;">
                                             <telerik:RadLabel ID="lbrad" runat="server" Text='<%# Eval("revaDes") %>'  Width="150px"  ></telerik:RadLabel>
                                         </td>
                                         <td style=" width:150px; background-color:transparent; ">
                                             <telerik:RadNumericTextBox ID="txt_var" MaxLength="10"  runat="server"  Width="150px" Text='<%# Eval("revaValVar") %>'
                                                EnabledStyle-CssClass="cssTxtEnabled"
                                                DisabledStyle-CssClass ="cssTxtEnabled"
                                                HoveredStyle-CssClass="cssTxtHovered"
                                                FocusedStyle-CssClass="cssTxtFocused"
                                                InvalidStyle-CssClass="cssTxtInvalid"
                                              ></telerik:RadNumericTextBox>
                                         </tb>
                                     </tr>
                                 </table>

                             </ItemTemplate>
                         </asp:DataList>
                       </div>

                    </fieldset>
     
                 </div>


                

                
                


            </td>


            <td style=" width:300px;background-color:transparent; vertical-align:auto; ">
                
                   <div style=" height:38px;"></div>
                
                   <fieldset >   
                    <legend>Formato Registro</legend>
                        <%--<div style=" height:10px; display:table; position:static; background-color:transparent;" align="center" >
                            <table border="0" style=" width:300px; text-align:left; background-color:transparent ;">
                            </table>           
                        </div>--%>

                        <div  style="overflow:auto; width:320px; height:285px; background-color:transparent;" >

                         <asp:DataList ID="DataListDocFormatoRegOpcPantalla" runat="server" DataKeyField="DocOpcPantCve" >
                             <ItemTemplate>


                                 
                                <table  border="0" style=" width:290px; text-align:left; background-color:transparent ;">
                                     <tr style="height:15px; ">
                                         <td style="width:190px;">
                                             <telerik:RadLabel ID="rLblDes" runat="server" Text='<%# Eval("DocOpcPantDes") %>'  Width="190px"  ></telerik:RadLabel>
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


                       </div>



                    </fieldset>
                


            
                   <fieldset>   
                        <legend>Layout de Impresión</legend>

                                   <telerik:RadImageGallery ShowLoadingPanel="false" RenderMode="Lightweight" ID="RadImageGallery1" runat="server" 
                                        DataDescriptionField="docFormDes" DataImageField="docFormArch"
                                        DataTitleField="docFormCve" Width="319px" Height="195px" Culture="es-ES"   >
                                   
                                       <ThumbnailsAreaSettings Width="0px" Height="0px"  EnableZoneScroll="false"   />

                                       <ToolbarSettings  ShowThumbnailsToggleButton="false"  ShowSlideshowButton="false"   />

                                       <ImageAreaSettings ShowNextPrevImageButtons="false"  />
                                       
                                  </telerik:RadImageGallery>
                    </fieldset>



                


                    
            </td>
        </tr>
    </table>
           <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
                    <telerik:RadImageButton ID="rBtnGuardar"  runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text ="" OnClick="rBtnGuardar_Click" OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
                    <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text="" OnClick="rBtnCancelar_Click" OnClientClicking="OnClientClic_ConfirmCancel"></telerik:RadImageButton>
                </asp:Panel>  

    <asp:HiddenField ID="hdfBtnAccion" runat="server" />

    
       <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"   >
    </telerik:RadWindowManager> 

      

    </ContentTemplate>
    </asp:UpdatePanel >



</asp:Content>






