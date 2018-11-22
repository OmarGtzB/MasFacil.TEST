<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MttoAgrupaciones.aspx.cs" Inherits="DC_MttoAgrupaciones" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

              
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Agrupaciones</title>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
    <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
    <link href="../css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
        
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
                                
     <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
        <telerik:RadImageButton ID="rBtnNuevo"     runat ="server" Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text ="" OnClick="rBtnNuevo_Click" Visible="false" ></telerik:RadImageButton>
         <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  ToolTip="Modificar"  Text="" OnClick="rBtnModificar_Click" Visible="false" ></telerik:RadImageButton>
        <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text="" OnClick="rBtnEliminar_Click" OnClientClicking="OnClientClic_ConfirmOK" Visible="false" ></telerik:RadImageButton>
        <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"  Text="" OnClick="rBtnLimpiar_Click"></telerik:RadImageButton>
    </asp:Panel>                              

      
         
    <fieldset >   
    <legend>Tipo Agrupación</legend>
        <div style="width:100%; display:table; position:static; background-color:transparent;" align="center" >

            <table border="0" style=" width:100%; text-align:left; background-color:transparent ;">
                <tr style="height:18px;">
                    <td style=" width:100%; background-color:transparent;">

                        <telerik:RadComboBox ID="rCboTiposAgrupacion" runat="server" Width="250px" 
                            HighlightTemplatedItems="true"
                            DropDownCssClass="cssRadComboBox"  
                            DropDownWidth="360px" Height="200px" 
                            OnSelectedIndexChanged="rCboTiposAgrupacion_SelectedIndexChanged" AutoPostBack="True" >
                                <ItemTemplate>
                                        <table style="width: 330px;"  cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="width: 330px;">
                                                    <%# DataBinder.Eval(Container.DataItem, "agrTipDes") %>
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

      
    <div style=" width:100%; display:table;  ">

    <fieldset style="  margin-top:10px; width:48%;    display: block;  float:left; text-align:left;" >   
    <legend>Agrupaciones</legend>
        <div style="width:100%; display:table;  float:left;position:static; background-color:transparent;" align="center" >

            <table border="0" style=" width:95%; text-align:left; background-color:transparent ;">
                <tr style="height:18px;">
                    <td style=" width:25%; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel3" runat="server">Agrupación</telerik:RadLabel>             
                    </td>
                    <td style=" width:75%; background-color:transparent;">
                          <telerik:RadTextBox ID="rTxtAgrupacionCve" Runat="server"  MaxLength="3"  Enabled="false" Width="150px"
                                              EnabledStyle-CssClass="cssTxtEnabled"
                                              DisabledStyle-CssClass="cssTxtEnabled"
                                              HoveredStyle-CssClass="cssTxtHovered"
                                              FocusedStyle-CssClass="cssTxtFocused"
                                              InvalidStyle-CssClass="cssTxtInvalid"
                          ></telerik:RadTextBox>                       
                    </td>
                </tr>
                <tr style="height:18px;">
                    <td style=" width:25%; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel4" runat="server">Descripción</telerik:RadLabel>
                    </td>
                    <td style=" width:75%; background-color:transparent;">
                            <telerik:RadTextBox ID="rTxtAgrupacionDes" Runat="server" Enabled="false" Width="290px"
                                              EnabledStyle-CssClass="cssTxtEnabled"
                                              DisabledStyle-CssClass="cssTxtEnabled"
                                              HoveredStyle-CssClass="cssTxtHovered"
                                              FocusedStyle-CssClass="cssTxtFocused"
                                              InvalidStyle-CssClass="cssTxtInvalid"
                            ></telerik:RadTextBox>                     
                    </td>
                </tr>

            </table>  
            
            <telerik:RadGrid ID="rGdv_Agrupaciones" 
                             runat="server" 
                             AutoGenerateColumns="False" 
                             OnSelectedIndexChanged="rGdv_Agrupaciones_SelectedIndexChanged"  
                             Width="420px" Height="330px"                              
                             CssClass="Grid"
                             Skin="Office2010Silver" >
                <MasterTableView DataKeyNames="agrCve" AutoGenerateColumns="False"  CssClass="GridTable" >
                        <Columns >
                            <telerik:GridBoundColumn HeaderText="Clave"       DataField="agrCve"   ItemStyle-Width="130px"  HeaderStyle-Width="130px"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Descripción"  DataField="agrDes"  ItemStyle-Width="270px"  HeaderStyle-Width="270px"></telerik:GridBoundColumn>
                        </Columns>
                        <NoRecordsTemplate>No se encontraron registros.</NoRecordsTemplate>
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
    </fieldset>
           
    <fieldset style="  margin-top:10px; width:48%;    display: block;  float:right; text-align:left;" >   
    <legend>Datos Agrupación</legend>
        <div style="width:100%; display:table;  float:left;position:static; background-color:transparent;" align="center" >

            <table border="0" style=" width:95%; text-align:left; background-color:transparent ;">
                <tr style="height:18px;">
                    <td style=" width:25%; background-color:transparent;">
                        <telerik:RadLabel ID="rLblAgurpacionDatoCve" runat="server"> Dato</telerik:RadLabel>         
                    </td>
                    <td style=" width:75%; background-color:transparent;">
                         <telerik:RadTextBox ID="rtxtAgrupacionDatoCve" Runat="server"  MaxLength="15"  Enabled="false" Width="150px"
                                              EnabledStyle-CssClass="cssTxtEnabled"
                                              DisabledStyle-CssClass="cssTxtEnabled"
                                              HoveredStyle-CssClass="cssTxtHovered"
                                              FocusedStyle-CssClass="cssTxtFocused"
                                              InvalidStyle-CssClass="cssTxtInvalid"
                         ></telerik:RadTextBox>                   
                    </td>
                </tr>
                <tr style="height:18px;">
                    <td style=" width:25%; background-color:transparent;">
                        <telerik:RadLabel ID="rLblAgurpacionDatoDes" runat="server">Descripción</telerik:RadLabel>
                    </td>
                    <td style=" width:75%; background-color:transparent;">
                        <telerik:RadTextBox ID="rtxtAgrupacionDatoDes" Runat="server" Enabled="false" Width="290px"
                                              EnabledStyle-CssClass="cssTxtEnabled"
                                              DisabledStyle-CssClass="cssTxtEnabled"
                                              HoveredStyle-CssClass="cssTxtHovered"
                                              FocusedStyle-CssClass="cssTxtFocused"
                                              InvalidStyle-CssClass="cssTxtInvalid"
                        ></telerik:RadTextBox>        
                    </td>
                </tr>

            </table>  

            <telerik:RadGrid ID="rGdv_AgrupacionesDato" 
                            runat="server"
                            AutoGenerateColumns="False" 
                            OnSelectedIndexChanged="rGdv_AgrupacionesDato_SelectedIndexChanged" 
                            Width="420px" Height="330px" 
                            CssClass="Grid"
                            skin="Office2010Silver" >
                <MasterTableView   DataKeyNames="agrDatoCve"  AutoGenerateColumns="False"  CssClass="GridTable" >
                    <Columns>
                        <telerik:GridBoundColumn DataField="agrDatoCve" HeaderStyle-Width="130px" HeaderText="Clave" ItemStyle-Width="130px" />
                        <telerik:GridBoundColumn DataField="agrDatoDes" HeaderStyle-Width="270px" HeaderText="Descripción" ItemStyle-Width="270px" />
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
    </fieldset>
            

    </div> 
   
    <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
        <telerik:RadImageButton ID="rBtnGuardar"  runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text ="" OnClick="rBtnGuardar_Click"></telerik:RadImageButton>
        <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text="" OnClick="rBtnCancelar_Click"></telerik:RadImageButton>
    </asp:Panel>    
        
         

    <asp:HiddenField ID="hdfAccAgr" runat="server" /> 
    <asp:HiddenField ID="hdfBtnAccion" runat="server" />


    </div>

    </ContentTemplate>
    </asp:UpdatePanel> 


    <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>
   
 

    </form>
   
</body>

</html>

