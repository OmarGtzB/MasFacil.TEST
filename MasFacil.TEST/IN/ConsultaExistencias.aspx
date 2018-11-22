<%@ Page Title="" Language="C#" MasterPageFile="~/MPForm.master" AutoEventWireup="true" CodeFile="ConsultaExistencias.aspx.cs" Inherits="IN_ConsultaExistencias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMPForm" Runat="Server">
    
    <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
    <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
    <link href="../css/styles.css" rel="stylesheet" type="text/css" />
    <link href="/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />

    <script>
        function openPopUpSaldosMes(pPerAnio, pPerNum, artCve, almCve, Consolidado,RefSelect) {
            window.open("ConsultaSaldosMes.aspx?pPerAnio=" + pPerAnio + "&pPerNum=" + pPerNum + "&artCve=" + artCve + "&almCve=" + almCve + "&Consolidado=" + Consolidado + "&RefSelect=" + RefSelect, "WindowPopup", "width=690px, height=480px, resizable");
        }
        function openPopUpSaldosMov(pPerAnio, pPerNum, artCve, almCve, Consolidado, RefSelect) {
            window.open("ConsultaSaldosMov.aspx?pPerAnio=" + pPerAnio + "&pPerNum=" + pPerNum + "&artCve=" + artCve + "&almCve=" + almCve + "&Consolidado=" + Consolidado + "&RefSelect=" + RefSelect + "&pMovAcum=1", "WindowPopup1", "width=1200px, height=480px, resizable");
        }
    </script>
    <style type="text/css">
        .rcbHeader ul,
        .rcbFooter ul,
        .rcbItem ul,
        .rcbHovered ul,
        .rcbDisabled ul {
            display: inline-block;
            margin: 0;
            padding: 0;
            list-style-type: none;
            vertical-align: middle;
        }
        .col1, .col2, .col3 {
            width: 50px;
            display: inline-block;
        }
 
 
        html .RadComboBoxDropDown .rcbItem > label,
        html .RadComboBoxDropDown .rcbHovered > label,
        html .RadComboBoxDropDown .rcbDisabled > label,
        html .RadComboBoxDropDown .rcbLoading > label,
        html .RadComboBoxDropDown .rcbCheckAllItems > label,
        html .RadComboBoxDropDown .rcbCheckAllItemsHovered > label {
            display: inline-block;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMPForm" Runat="Server">





    <telerik:RadAjaxManager ID="RAJAXMAN1" runat="server"  DefaultLoadingPanelID="RadAjaxLoadingPanel1"  >
        <AjaxSettings>



            <telerik:AjaxSetting AjaxControlID="rCboAnioPeriodo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID = "pnlBody" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rCboNumPeriodo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID = "pnlBody" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rCboArticulo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID = "pnlBody" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rCboFiltro1AgrTipo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID = "pnlBody" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rCboFiltro1Agr">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID = "pnlBody" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rCboFiltro1AgrDato">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID = "pnlBody" />
                </UpdatedControls>
            </telerik:AjaxSetting>

 

        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Silk"></telerik:RadAjaxLoadingPanel>







<%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>

    <asp:Panel ID="pnlBody" runat="server">
    
        <div>
            <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
                <telerik:RadImageButton ID="rBtnSaldoMensual" runat="server"  Width="30px" Height="30px"   Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnSaldosDisabled.png"        Image-Url="~/Imagenes/IcoBotones/IcoBtnSaldos.png"      Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnSaldosHovered.png"  ToolTip="Saldo"  Text="" Visible="false" OnClick="rBtnSaldo_Click"   ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnSaldoMov"     runat="server"  Width="30px" Height="30px"   Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnMovimientosDisabled.png"        Image-Url="~/Imagenes/IcoBotones/IcoBtnMovimientos.png"      Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnMovimientosHovered.png"  ToolTip="Movimientos"  Text="" Visible="false" OnClick="rBtnSaldoMov_Click"></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnLimpiar"      runat="server"  Width="30px" Height="30px"   Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"        Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png"      Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"  Text="" OnClick="rBtnLimpiar_Click"></telerik:RadImageButton>
            </asp:Panel> 


            <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none; width:100%; text-align:left; background-color:transparent ;">
                <tr  style="width:100%;">

                    <td style=" width:100%; background-color:transparent; vertical-align:top;">
                        
                        <fieldset style="">
                            <legend>Filtros</legend>
                            <div style="width:100%; display:table; position:static; background-color:transparent;" align="center" > 
                           
                            <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none;   text-align:left; background-color:transparent;">

                                  <tr style="height:30px;" >                             
                                    <td style=" width:40px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label6" runat="server" Text="Año"></asp:Label>
                                    </td>
                                    <td style=" width:200px; background-color:transparent; vertical-align:top;">

                                        <telerik:RadComboBox ID="rCboAnioPeriodo" runat="server" Width="160px"  OnSelectedIndexChanged="rCboAnioPeriodo_SelectedIndexChanged"
                                                    AutoPostBack="true"
                                                    HighlightTemplatedItems="true"
                                                    DropDownCssClass="cssRadComboBox" 
                                                    DropDownWidth="180px" 
                                                    Height="190px" >
                                                        <HeaderTemplate>
                                                            <table style="width: 190px" cellspacing="0" cellpadding="0">
                                                                    <tr>
 
                                                                        <td style="width: 190px;">
                                                                            Año
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <table style="width: 190px;"  cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td style="width:190px" >
                                                                        <%# DataBinder.Eval(Container.DataItem, "perAnio")%>
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
                                        <asp:Label ID="Label5" runat="server" Text="Periodo"></asp:Label>
                                    </td>
                                    <td style=" width:220px; background-color:transparent; vertical-align:top;">
                                        <telerik:RadComboBox ID="rCboNumPeriodo" runat="server" Width="180px" 
                                        AutoPostBack="true" OnSelectedIndexChanged="rCboNumPeriodo_SelectedIndexChanged"
                                        HighlightTemplatedItems="true"
                                        DropDownCssClass="cssRadComboBox" 
                                        DropDownWidth="210px" 
                                        Height="260px">
                                            <HeaderTemplate>
                                                <table style="width: 210px" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width: 70px;">
                                                                Periodo
                                                            </td>
                                                            <td style="width: 140px;">
                                                                Descipción
                                                            </td>
                                                        </tr>
                                                    </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table style="width: 210px;"  cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td style="width:70px" >
                                                            <%# DataBinder.Eval(Container.DataItem, "perNum")%>
                                                        </td>
                                                            <td style="width:140px" >
                                                            <%# DataBinder.Eval(Container.DataItem, "perDes")%>
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
                                        <asp:Label ID="Label7" runat="server" Text="Artículo"></asp:Label>
                                    </td>

                                    <td style=" width:340px; background-color:transparent; vertical-align:top;">
                                        <telerik:RadComboBox ID="rCboArticulo" runat="server"   
                                                            OnSelectedIndexChanged="rCboArticulo_SelectedIndexChanged"
                                                          
                                                            Width="290px"  
                                                            Height="230px" 
                                                            AutoPostBack="true" 
                                                            HighlightTemplatedItems="true"
                                                            DropDownCssClass="cssRadComboBox" 
                                                            DropDownWidth="550px" >
                                                                <HeaderTemplate>
                                                                    <table style="width: 500px" cellspacing="0" cellpadding="0" >
                                                                        <tr>
                                                                             <td style="width:150px; vertical-align:middle";  >
                                                                                Clave
                                                                            </td>
                                                                             <td style="width:350px; vertical-align:middle";  >
                                                                                Descripción
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <ul>
                                                                        <li class="col1" style="width:150px;   vertical-align:middle; " ><%# DataBinder.Eval(Container.DataItem, "Clave")%></li>
                                                                        <li class="col2" style="width:350px;   vertical-align:middle; " ><%# DataBinder.Eval(Container.DataItem, "Descripcion")%></li>
                                                                    </ul>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                                </FooterTemplate>
                                            </telerik:RadComboBox>                                    

                                    </td>

                                    <td style=" width:100px; background-color:transparent;  vertical-align:top;  ">
                                        <asp:Label ID="Label1" runat="server" Text="Por Almacén"></asp:Label>
                                    </td>
                                    <td style=" width:100px; background-color:transparent; vertical-align:top;">
                                        <asp:RadioButton ID="rBtnExisAlmSi" runat="server" GroupName="ConsulExisAlm" Text="Si" OnCheckedChanged="rBtnExisAlm_CheckedChanged"  Checked="true"  AutoPostBack="true" />
                                        <asp:RadioButton ID="rBtnExisAlmNo" runat="server" GroupName="ConsulExisAlm" Text="No"  OnCheckedChanged="rBtnExisAlm_CheckedChanged" Checked="false"  AutoPostBack="true" />
                                     </td>
                                </tr>

                                  <tr style="height:30px;" > 
                                    <td style=" width:70px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label9" runat="server" Text="Filtro"></asp:Label> 
                                    </td>       
                                    <td style=" width:200px; background-color:transparent; vertical-align:top;">
                                        <telerik:RadComboBox ID="rCboFiltro1AgrTipo" runat="server" 
                                            Width="160px" 
                                            Height="130px" 
                                            AutoPostBack="true" 
                                            HighlightTemplatedItems="true"
                                            DropDownCssClass="cssRadComboBox" 
                                            DropDownWidth="160px"
                                            AllowCustomText="true"
                                            OnSelectedIndexChanged="rCboFiltro1AgrTipo_SelectedIndexChanged"
                                            >
                                                <HeaderTemplate>
                                                    <table style="width: 190px" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 190px;">
                                                                    Descripción
                                                                </td>
                                                            </tr>
                                                        </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table style="width: 190px;"  cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width:190px" >
                                                                <%# DataBinder.Eval(Container.DataItem, "agrTipDes")%>
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
                                        <asp:Label ID="Label2" runat="server" Text="Agrupación"></asp:Label> 
                                    </td>
                                    <td style=" width:220px; background-color:transparent; vertical-align:top;">
                                        <telerik:RadComboBox ID="rCboFiltro1Agr" runat="server"  
                                            Width="180px"
                                            Height="160px" 
                                            AutoPostBack="true" 
                                            HighlightTemplatedItems="true"
                                            DropDownCssClass="cssRadComboBox" 
                                            DropDownWidth="300px"  
                                            AllowCustomText="true" 
                                            OnSelectedIndexChanged="rCboFiltro1Agr_SelectedIndexChanged"
                                            >
                                                <HeaderTemplate>
                                                    <table style="width: 280px" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width: 80px;">
                                                                Clave
                                                            </td>                                                                                            
                                                            <td style="width: 200px;">
                                                                Descipcion
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table style="width: 280px;"  cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width: 80px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "agrCve")%>
                                                            </td>                                                                                            
                                                            <td style="width: 200px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "agrDes")%>
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
                                        <asp:Label ID="Label3" runat="server" Text="Dato Agr."></asp:Label> 
                                    </td>       
                                    <td style=" width:300px; background-color:transparent; vertical-align:top;">
                                            <telerik:RadComboBox ID="rCboFiltro1AgrDato" runat="server" 
                                                Width="290px"
                                                Height="160px"  AutoPostBack="true"
                                                HighlightTemplatedItems="true"
                                                DropDownCssClass="cssRadComboBox" 
                                                DropDownWidth="550px"
                                                CheckBoxes="true" 
                                                EnableCheckAllItemsCheckBox="true"  
                                                                                 
                                                OnSelectedIndexChanged="rCboFiltro1AgrDato_SelectedIndexChanged"

                                                    OnItemChecked="rCboFiltro1AgrDato_ItemChecked"

                                                    OnCheckAllCheck="rCboFiltro1AgrDato_CheckAllCheck">
                                                <HeaderTemplate>
                                                <table style="width: 320px" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td style="width: 120px;">
                                                            Clave
                                                        </td>  
                                                        <td style="width: 250px;">
                                                            Descripción 
                                                        </td>                                                                                            
                                                    </tr>
                                                </table>
                                                </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <ul>
                                                        <li  class="col1" style="width:100px;    vertical-align:middle; " ><%# DataBinder.Eval(Container.DataItem, "agrDatoCve")%></li>
                                                        <li  class="col2" style="width:200px;   vertical-align:middle; " ><%# DataBinder.Eval(Container.DataItem, "agrDatoDes")%></li>
                                                    </ul>
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
                            <legend>Existencias</legend>
                            <div style="width:100%; height:310px;  display:table; position:static; background-color:transparent;" align="center" > 

                                <div>



                                      <telerik:RadGrid ID="rGdExistencias" 
                                                    runat="server"
                                                    AutoGenerateColumns="False"  ShowFooter="true"
                                                    Width="1250px" Height="290px" 
                                                    CssClass="Grid"
                                                    skin="Office2010Silver">
                                                    <MasterTableView   AutoGenerateColumns="False"  CssClass="GridTable"  > 
         
                                            <Columns>
                                                <telerik:GridBoundColumn HeaderText="ciaCve" DataField="ciaCve"     HeaderStyle-Width="175px"  ItemStyle-Width="175px"  Display="false"  />
                                                <telerik:GridBoundColumn HeaderText="Artículo" DataField="artCve"     HeaderStyle-Width="175px"  ItemStyle-Width="175px"     />
                                                <telerik:GridBoundColumn HeaderText="Descripción" DataField="artDes"     HeaderStyle-Width="335px"  ItemStyle-Width="335px"  />
                                                <telerik:GridBoundColumn HeaderText="Almacén" DataField="almCve"     HeaderStyle-Width="200px" ItemStyle-Width="200px"    />
                                                <telerik:GridBoundColumn HeaderText="Descripcion" DataField="almDes"     HeaderStyle-Width="200px" ItemStyle-Width="200px" Display="false" />

                                                <telerik:GridBoundColumn HeaderText="Saldo Inicial" DataField="exiCanIni"   Aggregate="Sum"   FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="125px"  ItemStyle-Width="125px" DataFormatString="{0:###,##0}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right"  />
                                                <telerik:GridBoundColumn HeaderText="Entradas" DataField="exiEntrada"       Aggregate="Sum"   FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="125px"  ItemStyle-Width="125px" DataFormatString="{0:###,##0}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" />
                                                <telerik:GridBoundColumn HeaderText="Salidas" DataField="exiSalida"         Aggregate="Sum"   FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="125px"  ItemStyle-Width="125px" DataFormatString="{0:###,##0}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" />
                                                <telerik:GridBoundColumn HeaderText="Saldo Final" DataField="exiActual"     Aggregate="Sum"   FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="125px"  ItemStyle-Width="125px" DataFormatString="{0:###,##0}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" />

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
                       
                            </div>


                        </fieldset>

                    </td>

                </tr>
            </table>

            <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
            </telerik:RadWindowManager>

            <asp:HiddenField ID="hdfBtnAccion" runat="server" />

        </div>

    </asp:Panel>

<%--        </ContentTemplate>
    </asp:UpdatePanel>--%>

</asp:Content>

