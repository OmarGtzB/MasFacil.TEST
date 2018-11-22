<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MttoControlPeriodos.aspx.cs" Inherits="DC_MttoControlPeriodos" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
     <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
       <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
     <link href="~/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />

        </head>
<body>
    <form id="form2" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
            </Scripts>
        </telerik:RadScriptManager> 


        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <div>

    <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server" Height="34px">
        <telerik:RadImageButton ID="rBtnNuevo"     runat ="server" Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text ="" Visible="false" OnClick="rBtnNuevo_Click"></telerik:RadImageButton>
        <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  ToolTip="Modificar"  Text="" Visible="false" OnClick="rBtnModificar_Click"></telerik:RadImageButton>
        <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text="" Visible="false" OnClick="rBtnEliminar_Click" OnClientClicking="OnClientClic_ConfirmOK" ></telerik:RadImageButton>
        <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar" OnClick="rBtnLimpiar_Click"  Text=""></telerik:RadImageButton>    
    </asp:Panel>

      <%--  EmptyMessage="selecciona un año"// message de RadNumericTexBox--%>
            <fieldset>       

           <div style="width:100%; display:table; position:static; background-color:transparent;" align="center" >

                    <table border="0" style=" width:760px; text-align:left; background-color:transparent ;">
                        <tr style="width:100%;">
                            <td style=" width:50px; background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel2" runat="server" Text="Año"></telerik:RadLabel> 
                            </td>
                            <td style=" width:150px;  background-color:transparent;">                             
                                <telerik:RadNumericTextBox  runat="server" ID="RadNumAño" Width="150px"  MinValue="2000" MaxValue ="3000"   ShowSpinButtons="true" NumberFormat-DecimalDigits="0" 
                                 DisabledStyle-CssClass ="cssTxtEnabled" OnTextChanged="RadNumAño_TextChanged"
                                            HoveredStyle-CssClass="cssTxtHovered" AutoPostBack="true"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid" 
                                  NumberFormat-GroupSeparator="" ></telerik:RadNumericTextBox>
                            </td>
                            <td style=" width:50px; background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel1" runat="server" Text="Periodo"></telerik:RadLabel> 
                            </td>
                                <td style=" width:120px;  background-color:transparent;"> 
                                                                
                           <telerik:RadNumericTextBox  runat="server" ID="RadNumPeriodo" Width="150px" Value="1"  MinValue="1" MaxValue ="12"
                               EnabledStyle-CssClass="cssTxtEnabled" OnTextChanged="RadNumPeriodo_TextChanged" AutoPostBack="true"
                                                DisabledStyle-CssClass ="cssTxtEnabled"
                                                HoveredStyle-CssClass="cssTxtHovered"
                                                FocusedStyle-CssClass="cssTxtFocused"
                                                InvalidStyle-CssClass="cssTxtInvalid" 
                                  ShowSpinButtons="true" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>

                            </td>

                            <td style=" width:55px; background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel3" runat="server" Text="Descripción"></telerik:RadLabel>  
                            </td>
                            <td style=" width:10px;  background-color:transparent;">                             
                                <telerik:RadTextBox ID="rTxtDes" runat="server" Width="200px"
                                                    EnabledStyle-CssClass="cssTxtEnabled"
                                                    DisabledStyle-CssClass ="cssTxtEnabled"
                                                    HoveredStyle-CssClass="cssTxtHovered"
                                                    FocusedStyle-CssClass="cssTxtFocused"
                                                    InvalidStyle-CssClass="cssTxtInvalid"                                    
                                ></telerik:RadTextBox>
                               <%--  MaxDate="01-01-2070" //propiedad radDataPicker--%>  
                            </td>
                        </tr>
                        <tr style="width:100%;">
                            <td style=" width:50px;  background-color:transparent;">                             
                                <asp:Label ID="Label8" runat="server" Text="Fec. Ini."></asp:Label>
                            </td>
                            <td style=" width:100px;  background-color:transparent;"> 
                                <telerik:RadDatePicker ID="RdDateFecha_Inicio"  runat="server" AutoPostBack="true"  Width="138px" OnSelectedDateChanged="RdDateFecha_Inicio_SelectedDateChanged">
                                </telerik:RadDatePicker>
                            </td>             
                            <td style=" width:50px; background-color:transparent; vertical-align:top;">
                                <asp:Label ID="Label10" runat="server" Text="Fec. Fin."></asp:Label>
                            </td>
                            <td style=" width:120px; background-color:transparent; vertical-align:top;">
                                <telerik:RadDatePicker ID="RdDateFecha_Final" runat="server" Width="138px"   AutoPostBack="true" OnSelectedDateChanged="RdDateFecha_Final_SelectedDateChanged"></telerik:RadDatePicker>
                            </td>

                            <td style=" width:55px; background-color:transparent; ">
                                <asp:Label ID="Label1" runat="server" Text="Situación"></asp:Label>
                            </td>
                            <td style=" width:10px; background-color:transparent;">
                                <asp:CheckBox ID="CheckReg" runat="server"  BackColor="White" Text="Activo"/> 
                            </td>
                        </tr>
              

                    </table>

                </div>

            </fieldset>

        
              <div style="width:100%; display:table; margin-top:5px; position:static; background-color:transparent;" align="center">
  
 
                     

                    <telerik:RadGrid ID="rGdvInformacion" 
                        runat="server"
                        AutoGenerateColumns ="false" 
                  
                        OnSelectedIndexChanged="rGdvInformacion_SelectedIndexChanged" 
                        Width="782px" Height="225px" 
                        CssClass="Grid" 
                        skin="Office2010Silver"  
                          >

                        <MasterTableView DataKeyNames="perAnio"  
                                         AutoGenerateColumns="False"  
                                          CssClass="GridTable"  >

                         <Columns >
                                <telerik:GridBoundColumn DataField="perAnio"  HeaderText="Año"  HeaderStyle-Width="70px" ItemStyle-Width="70px" />
                                <telerik:GridBoundColumn DataField="perNum" HeaderText="Periodo"  HeaderStyle-Width="70px"  ItemStyle-Width="70px" />
                                <telerik:GridBoundColumn DataField="perDes"  HeaderText="Descripción" HeaderStyle-Width="150px" ItemStyle-Width="150px" />
                                <telerik:GridBoundColumn DataField="perFecIni" HeaderText="Fec.inicio" HeaderStyle-Width="120px"  ItemStyle-Width="120px"  DataFormatString="{0:d}"/>
                                <telerik:GridBoundColumn DataField="perFecFin" HeaderText="Fec.Termino" HeaderStyle-Width="120px"  ItemStyle-Width="120px"   DataFormatString="{0:d}"/>
                               <telerik:GridBoundColumn DataField="perSit" HeaderText="Activo" HeaderStyle-Width="100px"  ItemStyle-Width="100px"  Display="false"/>
                              <telerik:GridImageColumn DataType="System.String" DataImageUrlFields="situacion"  HeaderStyle-Width="40px"  ItemStyle-Width="40px" 
                                               DataAlternateTextField="ContactName" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                     ImageAlign="Middle" ImageHeight="11px" ImageWidth="11px" HeaderText="Sit.">      
                               </telerik:GridImageColumn>  
                        </Columns>
                        <NoRecordsTemplate>No se encontraron registros.</NoRecordsTemplate>    
                </MasterTableView>

              
                    <HeaderStyle CssClass="GridHeaderStyle"/>
                    <ItemStyle CssClass="GridRowStyle"/>
                    <AlternatingItemStyle  CssClass="GridAlternatingRowStyle" />
                    <selecteditemstyle CssClass="GridSelectedItem"/>
                    <FooterStyle CssClass="GridFooterStyle" />

                    <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true" >
                        <Selecting AllowRowSelect="true" EnableDragToSelectRows="False"  ></Selecting>
                        <Scrolling AllowScroll="true" UseStaticHeaders="true"     ScrollHeight="225px"     />
                    </ClientSettings>

                </telerik:RadGrid>  
         </div>    
        <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
            <telerik:RadImageButton ID="rBtnGuardar"   runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text ="" OnClick="rBtnGuardar_Click"  OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
            <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text="" OnClick="rBtnCancelar_Click"  OnClientClicking="OnClientClic_ConfirmCancel"></telerik:RadImageButton>
        </asp:Panel>

        <asp:HiddenField ID="hdfBtnAccion" runat="server" />
        </div>
      </div>

        </ContentTemplate>
    </asp:UpdatePanel>  

    <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
    </telerik:RadWindowManager>

    </form>
</body>
</html>
