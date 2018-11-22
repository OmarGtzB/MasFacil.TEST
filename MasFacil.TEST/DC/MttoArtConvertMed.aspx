<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MttoArtConvertMed.aspx.cs" Inherits="DC_MttoArtConvertMed" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
    <script src="../Scripts/JSGeneral.js" type="text/javascript"></script>
    <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
    <link href="~/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

         <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
            <telerik:RadImageButton ID="rBtnNuevo"     runat ="server" Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text ="" OnClick="rBtnNuevo_Click" Visible="false" ></telerik:RadImageButton>
            <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  ToolTip="Modificar"  Text="" OnClick="rBtnModificar_Click" Visible="false" ></telerik:RadImageButton>
            <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text="" OnClick="rBtnEliminar_Click"  OnClientClicking="OnClientClic_ConfirmOK" Visible="false" ></telerik:RadImageButton>
            <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"  Text="" OnClick="rBtnLimpiar_Click" ></telerik:RadImageButton>
        </asp:Panel>
     

    <div>
        <fieldset >
            <telerik:RadLabel ID="lbl_codigArt" runat="server" Text="" ></telerik:RadLabel>&nbsp
            <telerik:RadLabel ID="lbl_descripArt" runat="server" Text="" ></telerik:RadLabel>
            <br />
            <telerik:RadLabel ID="lbl_UniMed" runat="server" Text="Unidad de Medida" ></telerik:RadLabel>&nbsp
            <telerik:RadLabel ID="lbl_UniMed_Art" runat="server" Text="" ></telerik:RadLabel>
            <telerik:RadLabel ID="lbl_uni_MedDes" runat="server" Text="" ></telerik:RadLabel>
        </fieldset>

        <fieldset  >
            <telerik:RadLabel ID="RadLabel3" runat="server" Text="Unidad" ></telerik:RadLabel>
            <telerik:RadComboBox ID="radCbo_Unidad" runat="server" OnSelectedIndexChanged="radCbo_Unidad_SelectedIndexChanged"
                                 HighlightTemplatedItems="true"
                                 DropDownCssClass="cssRadComboBox"  
                                 Width="210px" DropDownWidth="310px" Height="200px" 
                                 AutoPostBack="true" Enabled="false">

                <HeaderTemplate>
                    <table style="width: 280px" cellspacing="0" cellpadding="0">
                        <tr>
                            <td style="width: 90px;">
                                Clave
                            </td>
                            <td style="width: 190px;">
                                Descripción
                            </td>
                        </tr>
                    </table>
                </HeaderTemplate>
                <ItemTemplate>
                    <table style="width: 280px;"  cellspacing="0" cellpadding="0">
                        <tr>
                            <td style="width:90px" >
                                <%# DataBinder.Eval(Container.DataItem, "uniMedCve")%>
                            </td>
                            <td style="width: 190px;">
                                <%# DataBinder.Eval(Container.DataItem, "uniMedDes") %>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Literal runat="server" ID="RadComboItemsCount" />
                </FooterTemplate>
            </telerik:RadComboBox>&nbsp&nbsp



            <telerik:RadLabel ID="lbl_Factor" runat="server" Text="Factor" ></telerik:RadLabel>
            <telerik:RadNumericTextBox ID="radtxt_fact" runat="server"  MaxLength="15"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid"
                                            AllowRounding="false" 
                      Enabled="false">
                <NumberFormat AllowRounding="false"  DecimalDigits="6"  />

</telerik:RadNumericTextBox>
            
        </fieldset>

         <div style="width:100%; margin-top:5px; display:table; position:static; background-color:transparent;" align="Center" >  

             <telerik:RadGrid ID="radGrid_conversiones" 
                 runat="server"
                 AutoGenerateColumns="false"
                 OnSelectedIndexChanged="radGrid_conversiones_SelectedIndexChanged" 
              Width="657px"    Height="270px"
                 CssClass="Grid" 
                 Skin="Office2010Silver"
                 
                 >
                 <MasterTableView  DataKeyNames="artCve" CssClass="GridTable"   >
                     <Columns>
                        <telerik:GridBoundColumn HeaderText="Unidad" DataField="uniMedCve"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Clave Compañia" DataField="ciaCve" Visible="false"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Descripción" DataField="uniMedDes" ></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Factor" DataType="System.Decimal" DataFormatString="{0:###,##0.000000}"  DataField="artConMedFact" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                     </Columns>
                     <NoRecordsTemplate>No se encontraron registros</NoRecordsTemplate>
                 </MasterTableView>
                    <HeaderStyle CssClass="GridHeaderStyle"/>
                    <ItemStyle CssClass="GridRowStyle"/>
                    <AlternatingItemStyle  CssClass="GridAlternatingRowStyle" />
                    <selecteditemstyle CssClass="GridSelectedItem"/>
                    <FooterStyle CssClass="GridFooterStyle" />

                    <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true" >
                        <Selecting AllowRowSelect="true" EnableDragToSelectRows="False" ></Selecting>
                        <Scrolling AllowScroll="true" UseStaticHeaders="true"    ScrollHeight="270px"     />
                    </ClientSettings>
             </telerik:RadGrid>
         </div>      

        <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
            <telerik:RadImageButton ID="rBtnGuardar"     runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text ="" OnClick="rBtnGuardar_Click" OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
            <telerik:RadImageButton ID="rBtnCancelar" runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text="" OnClick="rBtnCancelar_Click" OnClientClicking="OnClientClic_ConfirmCancel"></telerik:RadImageButton>
        </asp:Panel>
    </div>
          <telerik:RadWindowManager ID="RadWindowManager" runat="server"></telerik:RadWindowManager>
          <telerik:RadLabel ID="RADLBL_ERROR" runat="server" ForeColor="Red"></telerik:RadLabel>

            <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server"></telerik:RadWindowManager>


             <asp:HiddenField ID="hdfBtnAccion" runat="server" />
    </ContentTemplate>
    </asp:UpdatePanel> 
    </form>
         
</body>
</html>
