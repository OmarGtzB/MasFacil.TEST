<%@ Page Title="" Language="C#" MasterPageFile="~/MPForm.master"  AutoEventWireup="true" CodeFile="ConsultaSaldos.aspx.cs" Inherits="XP_ConsultaSaldos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMPForm" Runat="Server">

<link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
    <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
    <link href="../css/styles.css" rel="stylesheet" type="text/css" />
    <link href="/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />

    <script>
        function openPopUpSaldosMes(pPerAnio, pPerNum, provCve) {
            window.open("ConsultaSaldosMes.aspx?pPerAnio=" + pPerAnio + "&pPerNum=" + pPerNum + "&provCve=" + provCve, "WindowPopup", "width=690px, height=480px, resizable");
        }
        function openPopUpSaldosMov(pPerAnio, pPerNum, provCve) {
            window.open("ConsultaSaldosMov.aspx?pPerAnio=" + pPerAnio + "&pPerNum=" + pPerNum + "&provCve=" + provCve + "&pMovAcum=1" , "WindowPopup1", "width=810px, height=480px, resizable");
        }
        function openPopUpSaldosAntiguedad(pPerAnio, pPerNum, provCve) {
            window.open("ConsultaSaldosAntig.aspx?pPerAnio=" + pPerAnio + "&pPerNum=" + pPerNum + "&provCve=" + provCve, "WindowPopup3", "width=500px, height=560px, resizable");
        }
        function openPopUpSaldosIntegra(pPerAnio, pPerNum, provCve) {
            window.open("ConsultaSaldosInteg.aspx?pPerAnio=" + pPerAnio + "&pPerNum=" + pPerNum + "&provCve=" + provCve, "WindowPopup2", "width=820px, height=500px, resizable");;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMPForm" Runat="Server">


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <div>
        
            <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
                <telerik:RadImageButton ID="rBtnSaldoMensual" runat="server"  Width="30px" Height="30px"   Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnSaldosDisabled.png"        Image-Url="~/Imagenes/IcoBotones/IcoBtnSaldos.png"      Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnSaldosHovered.png"  ToolTip="Saldo"  Text="" OnClick="rBtnSaldo_Click"   ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnSaldoMov"     runat="server"  Width="30px" Height="30px"   Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnMovimientosDisabled.png"        Image-Url="~/Imagenes/IcoBotones/IcoBtnMovimientos.png"      Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnMovimientosHovered.png"  ToolTip="Movimientos"  Text="" OnClick="rBtnSaldoMov_Click"></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnSaldoAntig"   runat="server"  Width="30px" Height="30px"   Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnAntiguedadDisabled.png"        Image-Url="~/Imagenes/IcoBotones/IcoBtnAntiguedad.png"      Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnAntiguedadHovered.png"  ToolTip="Antiguedad"  Text="" OnClick="rBtnSaldoAntig_Click"></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnSaldoInteg"   runat="server"  Width="30px" Height="30px"   Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnIntegracionDisabled.png"        Image-Url="~/Imagenes/IcoBotones/IcoBtnIntegracion.png"      Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnIntegracionHovered.png"  ToolTip="Integración"  Text="" OnClick="rBtnSaldoInteg_Click"></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnLimpiar"      runat="server"  Width="30px" Height="30px"   Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"        Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png"      Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"  Text="" OnClick="rBtnLimpiar_Click"></telerik:RadImageButton>
            </asp:Panel>      
        
            <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none; width:100%; text-align:left; background-color:transparent ;">
                <tr  style="width:100%;">

                    <td style=" width:100%; background-color:transparent; vertical-align:top;">
                        
                        <fieldset style="">
                            <legend>Filtros</legend>
                            <div style="width:100%; display:table; position:static; background-color:transparent;" align="left" > 
                           
                            <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none;   text-align:left; background-color:transparent;">
                                <tr   >                              
                                    <td style=" width:40px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label6" runat="server" Text="Año"></asp:Label>
                                    </td>
                                    <td style=" width:170px; background-color:transparent; vertical-align:top;">
                                        <telerik:RadComboBox ID="rCboAnioPeriodo" runat="server" Width="120px"  OnSelectedIndexChanged="rCboAnioPeriodo_SelectedIndexChanged"
                                                                                AutoPostBack="true"
                                                                                HighlightTemplatedItems="true"
                                                                                DropDownCssClass="cssRadComboBox" 
                                                                                DropDownWidth="120px" 
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
                                    <td style=" width:240px; background-color:transparent; vertical-align:top;">
                                        <telerik:RadComboBox ID="rCboNumPeriodo" runat="server" Width="190px" 
                                                                                AutoPostBack="true" OnSelectedIndexChanged="rCboNumPeriodo_SelectedIndexChanged"
                                                                                HighlightTemplatedItems="true"
                                                                                DropDownCssClass="cssRadComboBox" 
                                                                                DropDownWidth="210px" 
                                                                                Height="190px">
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


                                    <td style=" width:75px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label7" runat="server" Text="Proveedor"></asp:Label>
                                    </td>

                                    <td style=" width:265px; background-color:transparent; vertical-align:top;">
                                    <telerik:RadComboBox ID="rCboProveedores" runat="server"   OnSelectedIndexChanged="rCboProveedores_SelectedIndexChanged"
                                                        Width="210px"  OnItemChecked="rCboProveedores_ItemChecked" OnCheckAllCheck="rCboProveedores_CheckAllCheck"
                                                        Height="230px" 
                                                        AutoPostBack="true" 
                                                        HighlightTemplatedItems="true"
                                                        DropDownCssClass="cssRadComboBox" 
                                                        DropDownWidth="500px"            
                                                        CheckBoxes="true" 
                                                        EnableCheckAllItemsCheckBox="true"                                                       
                                                        >
                                                            <HeaderTemplate>
                                                                <table style="width: 280px" cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                        <td style="width: 10px;">
                                                                                                     
                                                                        </td>  
                                                                        <td style="width: 260px;">
                                                                            Clave - Nombre 
                                                                        </td>                                                                                            
                                                                    </tr>
                                                                </table>
                                                            </HeaderTemplate>
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
                            <legend>Saldos</legend>
                            <div style="width:100%; height:310px;  display:table; position:static; background-color:transparent;" align="center" > 

                                <div>


                                      <telerik:RadGrid ID="rGdvSaldos" 
                                                    runat="server"
                                                    AutoGenerateColumns="False" 
                                                    Width="1250px" Height="300px" 
                                                    CssClass="Grid"
                                                    skin="Office2010Silver"
                                                    ShowFooter ="true" 
                                                    >
                                                    <MasterTableView   AutoGenerateColumns="False"  CssClass="GridTable"  > 
         
                                            <Columns>
                                                <telerik:GridBoundColumn HeaderText="ciaCve" DataField="ciaCve"     HeaderStyle-Width="208px"  ItemStyle-Width="100px"  Display="false"  />
                                                <telerik:GridBoundColumn HeaderText="salXPTipApli" DataField="salXPTipApli"     HeaderStyle-Width="208px"  ItemStyle-Width="100px"  Display="false"  />
                                                <telerik:GridBoundColumn HeaderText="Proveedor" DataField="provCve"     HeaderStyle-Width="100px"  ItemStyle-Width="100px"  Display="true" />
                                                <telerik:GridBoundColumn HeaderText="Nombre" DataField="provNom"     HeaderStyle-Width="150px" ItemStyle-Width="150px"  Display="true"  />
                                                <telerik:GridBoundColumn HeaderText="Tipo de Proveedor" DataField="TipoProvDes"     HeaderStyle-Width="100px"  ItemStyle-Width="100px"  Display="true"  />
                                                <telerik:GridBoundColumn HeaderText="Registro Fiscal" DataField="provRegFis"     HeaderStyle-Width="100px"  ItemStyle-Width="100px"  Display="true"  />
                                                <telerik:GridBoundColumn HeaderText="Saldo" DataField="saldo" Aggregate="Sum"  FooterStyle-HorizontalAlign="Right"   HeaderStyle-Width="110px" ItemStyle-Width="110px" DataFormatString="{0:###,##0.00}" ItemStyle-HorizontalAlign="Right" />
                                              
                
                                                <telerik:GridBoundColumn HeaderText="salXPIni" DataField="salXPIni"     HeaderStyle-Width="208px"  ItemStyle-Width="100px"  Display="false"   />
                                                <telerik:GridBoundColumn HeaderText="salXPCgo" DataField="salXPCgo"     HeaderStyle-Width="208px"  ItemStyle-Width="100px"  Display="false"  />
                                                <telerik:GridBoundColumn HeaderText="salXPAbo" DataField="salXPAbo"     HeaderStyle-Width="208px"  ItemStyle-Width="100px"  Display="false"  />
                       
                                                
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
        </ContentTemplate>
    </asp:UpdatePanel>



</asp:Content>

