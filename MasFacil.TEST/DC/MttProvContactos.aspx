<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MttProvContactos.aspx.cs" Inherits="DC_MttProvContactos" %>
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
        <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar" OnClick="rBtnLimpiar_Click"  Text=""></telerik:RadImageButton>    
    </asp:Panel>

    <fieldset   >   
    <legend>Datos Personales</legend>
        <div style="width:100%; display:table; position:static; background-color:transparent;" align="center" >

            <table border="0"  cellpadding="0" cellspacing="0" style=" width:720px; text-align:left; background-color:transparent ;">
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel1" runat="server" Text="Nombre(s)"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                        <telerik:RadTextBox ID="rTxtNombre" runat="server"  Width="220px" MaxLength="50"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel2" runat="server" Text="Apellido Paterno"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                        <telerik:RadTextBox ID="rTxtApPaterno" runat="server"  Width="220px" MaxLength="50"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                </tr>
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel3" runat="server" Text="Apellido Materno"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                        <telerik:RadTextBox ID="rTxtApMaterno" runat="server"  Width="220px" MaxLength="50"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                    <td style=" width:130px; background-color:transparent;">
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                    </td>
                </tr>

            </table>           

        </div>
    </fieldset>



        <fieldset style="  margin-top:5px;   display: block; text-align:left;" >  
            <legend>Otros Datos</legend>                 
            <div style="width:100%;   height:32px; overflow:auto; margin-left:10px; background-color:transparent;"   >
                <asp:DataList ID="DataListOtrosDatos" runat="server"   DataKeyField="otroDatCve"  >
                    <ItemTemplate>
                        <table border="0" style=" width:100%; text-align:left; background-color:transparent;">
                            <tr>
                                <td style=" width:125px;  background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel19" runat="server" Text='<%# Eval("otroDatDes") %>'  ></telerik:RadLabel>

                                </td>
                                <td style="  width:220px; background-color:transparent;">
                                    <telerik:RadTextBox ID="txt_OtrosDatos" runat="server" Width="220px"  
                                                            EnabledStyle-CssClass="cssTxtEnabled"
                                                            DisabledStyle-CssClass ="cssTxtEnabled"  
                                                            HoveredStyle-CssClass="cssTxtHovered"
                                                            FocusedStyle-CssClass="cssTxtFocused" Text='<%# Eval("OtroDatVal") %>'
                                                            InvalidStyle-CssClass="cssTxtInvalid">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </fieldset>







     <fieldset  >   
    <legend>Dirección</legend>
        <div style="width:100%; display:table; position:static; background-color:transparent;" align="center" >

            <table border="0"  cellpadding="0" cellspacing="0" style=" width:720px; text-align:left; background-color:transparent ;">
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel4" runat="server" Text="País"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                                <telerik:RadComboBox ID="rCboPais" runat="server" OnSelectedIndexChanged="rCboPais_SelectedIndexChanged" AutoPostBack="True"  
                                                     HighlightTemplatedItems="true" AllowCustomText="true"
                                                     DropDownCssClass="cssRadComboBox"  
                                                     Width="220px" DropDownWidth="290px" Height="200px">
                                    <HeaderTemplate>
                                        <table style="width: 260px" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 260px;">
                                                        Descripción
                                                    </td>
                                                </tr>
                                            </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                            <table style="width: 260px;"  cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 260px;">
                                                        <%# DataBinder.Eval(Container.DataItem, "paisDes") %>
                                                    </td>
                                  
                                                </tr>
                                            </table>

                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                    </FooterTemplate>
                                 </telerik:RadComboBox>
                    </td>
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel5" runat="server" Text="Estado"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                                <telerik:RadComboBox ID="rCboEntidadFed" runat="server" OnSelectedIndexChanged="rCboEntidadFed_SelectedIndexChanged" AutoPostBack="True"
                                                     HighlightTemplatedItems="true"
                                                     DropDownCssClass="cssRadComboBox"   AllowCustomText="true"
                                                     Width="220px" DropDownWidth="290px" Height="200px">
                                    <HeaderTemplate>
                                        <table style="width: 260px" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 260px;">
                                                        Descripción
                                                    </td>
                                                </tr>
                                            </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                            <table style="width: 260px;"  cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 260px;">
                                                        <%# DataBinder.Eval(Container.DataItem, "entFDes") %>
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
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel6" runat="server" Text="Del/Muni"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                                <telerik:RadComboBox ID="rCboProvincia" runat="server" AutoPostBack="True"
                                                     HighlightTemplatedItems="true" AllowCustomText="True"
                                                     DropDownCssClass="cssRadComboBox"  
                                                     Width="220px" DropDownWidth="290px" Height="200px">
                                    <HeaderTemplate>
                                        <table style="width: 260px" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 260px;">
                                                        Descripción
                                                    </td>
                                                </tr>
                                            </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                            <table style="width: 260px;"  cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 260px;">
                                                        <%# DataBinder.Eval(Container.DataItem, "provDes") %>
                                                    </td>
                                  
                                                </tr>
                                            </table>

                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                    </FooterTemplate>
                                 </telerik:RadComboBox>
                    </td>
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel7" runat="server" Text="Colonia"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;"> 
                        <telerik:RadTextBox ID="rTxtColonia" runat="server"  Width="220px" MaxLength="50"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>                              
                    </td>
                </tr>

            </table>           

            <table border="0"  cellpadding="0" cellspacing="0" style=" width:720px; text-align:left; background-color:transparent ;">
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel8" runat="server" Text="Calle"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                        <telerik:RadTextBox ID="rTxtCalle" runat="server"  Width="220px" MaxLength="50"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel9" runat="server" Text=" No Exterior"></telerik:RadLabel>  
                    </td>
                    <td style=" width:70px;  background-color:transparent;">                             
                        <%--<telerik:RadNumericTextBox ID="rTxtNoExt" runat="server"  Width="60px"
                             NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="64"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadNumericTextBox>--%>
                        <telerik:RadTextBox ID="rTxtNoExt" runat="server"   Width="60px" MaxLength="30"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>

                    <td style=" width:90px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel10" runat="server" Text="No Interior"></telerik:RadLabel>  
                    </td>
                    <td style=" width:70px; background-color:transparent;">
                       <%-- <telerik:RadNumericTextBox ID="rTxtNoInt" runat="server"  Width="60px"
                             NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="64"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid"  
                         ></telerik:RadNumericTextBox>--%>
                         <telerik:RadTextBox ID="rTxtNoInt" runat="server"   Width="60px" MaxLength="30"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                </tr>
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel11" runat="server" Text="Calles Aledañas"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                        <telerik:RadTextBox ID="rTxtCallesAledanas" runat="server"  Width="220px" MaxLength="50"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel12" runat="server" Text="Codigo Postal"></telerik:RadLabel>  
                    </td>
                    <td style=" width:70px;  background-color:transparent;">                             
                        <telerik:RadNumericTextBox ID="rTxtCodigoPostal" runat="server"  Width="60px"
                             NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="64"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid"  
                         ></telerik:RadNumericTextBox>
                    </td>
                    <td style=" width:90px; background-color:transparent;">
                         
                    </td>
                    <td style=" width:70px; background-color:transparent;">

                    </td>
                </tr>
            </table>           

            <table border="0"  cellpadding="0" cellspacing="0" style=" width:720px; text-align:left; background-color:transparent ;">
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel13" runat="server" Text="Referencia"></telerik:RadLabel>  
                    </td>
                    <td style=" width:590px;  background-color:transparent;">                             
                        <telerik:RadTextBox ID="rTxtReferencia" runat="server"  Width="580px" MaxLength="50"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                </tr>
            </table>  

            <table border="0"  cellpadding="0" cellspacing="0" style=" width:720px; text-align:left; background-color:transparent ;">

                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel14" runat="server" Text="Teléfono 1"></telerik:RadLabel>
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                        <telerik:RadNumericTextBox ID="rTxtTelefono1" runat="server"  Width="220px"
                            NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="4" NumberFormat-DecimalDigits="0" MaxLength="64"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadNumericTextBox>
                    </td>
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel15" runat="server" Text="Teléfono 2"></telerik:RadLabel>
                    </td>
                    <td style=" width:230px;  background-color:transparent;"> 
                        <telerik:RadNumericTextBox ID="rTxtTelefono2" runat="server"  Width="220px"
                            NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="4" NumberFormat-DecimalDigits="0" MaxLength="64"
                            EnabledStyle-CssClass="cssTxtEnabled"
                          
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid"
                         ></telerik:RadNumericTextBox>
                    </td>
                </tr>

            </table>        

            <table runat="server" visible="false" border="0"  cellpadding="0" cellspacing="0" style=" width:720px; text-align:left; background-color:transparent ;">
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                      <telerik:RadLabel ID="RadLabel16" runat="server" Text="Fax"></telerik:RadLabel> 
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                        <telerik:RadNumericTextBox ID="rTxtFax" runat="server"  Width="220px"
                            NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="4" NumberFormat-DecimalDigits="0" MaxLength="64"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid"  
                         ></telerik:RadNumericTextBox>
                    </td>
                    <td style=" width:130px; background-color:transparent;">
                     
                    </td>
                    <td style=" width:230px;  background-color:transparent;"> 
 
                    </td>
                </tr>

            </table>   

            <telerik:RadTextBox ID="RadTextProvConId" runat="server" Visible="false"></telerik:RadTextBox>

        </div>
    </fieldset>

    <div runat="server" visible="false" style=" width:100%; display:table; ">
       <fieldset style="  width:95%; height:90px; ">
                <legend>Referencias</legend>
                    <div  style="overflow:auto; width:355px; height:80px;" >
                        <asp:DataList ID="DATAREFCLIE" runat="server" DataKeyField="revasec" >
                            <ItemTemplate>


                                 <table border="0"  cellpadding="0" cellspacing="0" style=" width:345px; text-align:left; background-color:transparent ;">
                                        <tr style="height:22px;">
                                            <td style=" width:120px; background-color:transparent;">
                                                 <telerik:RadLabel ID="lbrad" runat="server" Text='<%# Eval("revaDes") %>' ></telerik:RadLabel>
                                            </td>
                                            <td style=" width:225px;  background-color:transparent;">                             
                                                            <telerik:RadTextBox ID="txtrefclie"  Text='<%# Eval("revaValRef") %>' 
                                                                        EnabledStyle-CssClass="cssTxtEnabled"
                                                                        DisabledStyle-CssClass ="cssTxtEnabled"
                                                                        HoveredStyle-CssClass="cssTxtHovered"
                                                                        FocusedStyle-CssClass="cssTxtFocused"
                                                                        InvalidStyle-CssClass="cssTxtInvalid"
                                                                        runat="server" Width="220px"
                                                                        ></telerik:RadTextBox>
                                            </td>
                                        </tr>

                                    </table>      





                            </ItemTemplate>
                        </asp:DataList>
                    </div>
            </fieldset>
                   
    </div>

 
      <div style="width:100%; margin-top:5px; display:table; position:static; background-color:transparent;" align="center" >  
            
            
                    <telerik:RadGrid ID="rGdvDatos" 
                        runat="server"  
                        AutoGenerateColumns="false"
                        OnSelectedIndexChanged="rGdvDatos_SelectedIndexChanged"
                        Width="740px" Height="90px" 
                        CssClass="Grid"  
                        Skin="Office2010Silver" >
                        <MasterTableView DataKeyNames="provConId" CssClass="GridTable"   >
                            <NoRecordsTemplate>No hay Registros</NoRecordsTemplate>
                            <Columns>
                                <telerik:GridBoundColumn HeaderText="Nombre" DataField="provConNom" HeaderStyle-Width="360px" ItemStyle-Width="400px" ></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="provConAPat" DataField="provConAPat" Display="false"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="provConAMat" DataField="provConAMat" Display="false"></telerik:GridBoundColumn>
                                
                                
                                <telerik:GridBoundColumn HeaderText="País" DataField="paisDes" Display="false"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Estado" DataField="entFDes" Display="false"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Población" DataField="provDes" Display="false"></telerik:GridBoundColumn>
                              
                                <telerik:GridBoundColumn HeaderText="domCol" DataField="domCol" Display="false"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="domClle" DataField="domClle" Display="false"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="domCllsA" DataField="domCllsA" Display="false"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="domNInt" DataField="domNInt" Display="false"></telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn HeaderText="domNExt" DataField="domNExt" Display="false"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="domRef" DataField="domRef" Display="false"></telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn HeaderText="domCP" DataField="domCP" Display="false"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="domRef" DataField="domRef" Display="false"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Teléfono 1" DataField="domTel"  HeaderStyle-Width="160px" ItemStyle-Width="160px" ></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Teléfono 2" DataField="domTel2" HeaderStyle-Width="160px" ItemStyle-Width="160px" ></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="domFax" DataField="domFax" Display="false"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="paisCve" DataField="paisCve" Display="false"></telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn HeaderText="entFCve" DataField="entFCve" Display="false"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="provCve" DataField="provCve" Display="false"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="provConId" DataField="provConId" Display="false" Visible="false"></telerik:GridBoundColumn>
                              </Columns>
                        </MasterTableView>
                        <HeaderStyle CssClass="GridHeaderStyle"/>
                        <ItemStyle CssClass="GridRowStyle"/>
                        <AlternatingItemStyle  CssClass="GridAlternatingRowStyle" />
                        <selecteditemstyle CssClass="GridSelectedItem"/>
                        <FooterStyle CssClass="GridFooterStyle" />

                        <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true" >
                            <Selecting AllowRowSelect="true" EnableDragToSelectRows="False" ></Selecting>
                            <Scrolling AllowScroll="true" UseStaticHeaders="true"    ScrollHeight="130px"     />
                        </ClientSettings>
                    </telerik:RadGrid>

 

        </div>                 
                     
 
    <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
        <telerik:RadImageButton ID="rBtnGuardar"  runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text ="" OnClick="rBtnGuardar_Click" OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
        <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text="" OnClick="rBtnCancelar_Click" OnClientClicking="OnClientClic_ConfirmCancel"></telerik:RadImageButton>
    </asp:Panel>


    <asp:HiddenField ID="hdfBtnAccion" runat="server" />
    <asp:HiddenField ID="hdfIdRegSel" runat="server" />

    </div>
    </ContentTemplate>
    </asp:UpdatePanel> 
          
    <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
    </telerik:RadWindowManager>


    </form>
</body>
</html>

