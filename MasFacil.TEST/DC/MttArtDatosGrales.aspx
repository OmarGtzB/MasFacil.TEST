<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MttArtDatosGrales.aspx.cs" Inherits="DC_Articulo" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


  
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title></title>
    <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
    <script src="../Scripts/MGM/jsMGMMENU.js" type="text/javascript"></script>
    <script src="../Scripts/JSGeneral.js" type="text/javascript"></script>
    <link href="~/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />
    <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">  

        //function fileUploaded(sender, args) {
        ////    //var manager = $find("ajaxManager");
        ////   //manager.ajaxRequest();
        //}

         function fileUploaded(sender, args) {
            $find('<%= RAJAXMAN1.ClientID %>').ajaxRequest();
            $telerik.$(".invalid").html("");
            setTimeout(function () {
                sender.deleteFileInputAt(0);
            }, 10);
        }
  
    </script> 
        </telerik:RadCodeBlock>



</head>
<body>
    <form id="form1" runat="server">
                     
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

<telerik:RadAjaxManager ID="RAJAXMAN1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings>
        
        <telerik:AjaxSetting AjaxControlID="RAJAXMAN1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID = "RadBinaryImage1" />
                </UpdatedControls>
            </telerik:AjaxSetting>


             <telerik:AjaxSetting AjaxControlID="DATALISTPRUEBA">
                <UpdatedControls>

                    <telerik:AjaxUpdatedControl ControlID = "pnlBody" />
                </UpdatedControls>
            </telerik:AjaxSetting>

         </AjaxSettings>
</telerik:RadAjaxManager>

  <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Silk"></telerik:RadAjaxLoadingPanel>
  <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" BackColor="White" Transparency="30"></telerik:RadAjaxLoadingPanel>

<%--  
<asp:UpdatePanel ID="UpdatePanel1" runat="server" >

        <ContentTemplate>--%>

        <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server" >
            <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  ToolTip="Modificar"  Text="" OnClick="rBtnModificar_Click" Visible="false" ></telerik:RadImageButton>
            <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"  Text="" OnClick="rBtnLimpiar_Click"  ></telerik:RadImageButton>
        </asp:Panel>

<asp:Panel ID="pnlBody" runat="server">
            
<div >

   <asp:HiddenField ID="hdfSecArtAct" runat="server" />     

<div style=" width:100%; display:table;  background-color:transparent ">
    <asp:Table runat="server">
            <asp:TableRow>
                <asp:TableCell>

                   <%-- <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" >
<ContentTemplate>--%>

                    <fieldset style="    width:390px; height:186px;  display: block;  float:left;  background-color:aliceblue;""  >   
         <legend >Generar codigo del Articulo</legend>
            <div style="width:100%; display:table;  float:left;position:static; background-color:transparent;" align="center" >
                <div style=" overflow:auto; width:390px; height:162px; background-color:transparent ;" id="divClave"  >
                    <div class="demo-container size-thin">
                           
	            <asp:DataList ID="DATALISTPRUEBA" runat="server" DataKeyField="artEstCodElem">
				        <ItemTemplate>
				                <table  border="0"  cellpadding="0" cellspacing="0" style=" width:380px; text-align:left; background-color:transparent;">
				                    <tr style=" height:22px;  vertical-align:text-top  ">
				                        <td style=" width:180px; background-color:transparent;">
				                            <telerik:RadLabel ID="RadLabel1" runat="server" Text='<%# Eval("artEstCodDes") %>' ></telerik:RadLabel>
				                        </td>
				                        <td style=" width:200px; background-color:transparent;">

                                            <telerik:RadTextBox ID="rTxtPrueba" runat="server"  Width="200px" Visible="false"
                                                Enabled="false" OnTextChanged="rTxtPrueba_TextChanged" AutoPostBack="true"
                                                                EnabledStyle-CssClass="cssTxtEnabled"
                                                                DisabledStyle-CssClass ="cssTxtEnabled"
                                                                HoveredStyle-CssClass="cssTxtHovered"
                                                                FocusedStyle-CssClass="cssTxtFocused"
                                                                InvalidStyle-CssClass="cssTxtInvalid">
                                                
                                            </telerik:RadTextBox>
				                            <telerik:RadComboBox ID="rCboPrueba" runat="server" Width="200px" 
                                                HighlightTemplatedItems="true"   Enabled="false"  
                                                
                                                DropDownCssClass="cssRadComboBox" 
                                                DropDownWidth="260px" 
                                                Height="200px" AutoPostBack="true" OnSelectedIndexChanged="rCboPrueba_SelectedIndexChanged">
                                                    <HeaderTemplate>
                                                        <table style="width: 140px" cellspacing="0" cellpadding="0">
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
                                                                <td style="width:45px" >
                                                                    <%# DataBinder.Eval(Container.DataItem, "agrDatoCve")%>
                                                                </td>
                                                                <td style="width: 170px;">
                                                                    <%# DataBinder.Eval(Container.DataItem, "agrDatoDes") %>
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
				            <asp:HiddenField  ID="hdn_AgrCve" Value='<%# Eval("agrCve") %>' runat="server" />
				            <asp:HiddenField  ID="EstCodTip" Value='<%# Eval("artEstCodTip") %>' runat="server" />

                            <asp:HiddenField ID="hdfSecArt"  Value='<%# Eval("artEstCodElem") %>'   runat="server" />

				    </ItemTemplate>
				</asp:DataList> 
    
                                   
                    </div>
                </div>
            </div>
         </fieldset>  

               <%-- </ContentTemplate>
                </asp:UpdatePanel>--%>

                </asp:TableCell>
                <asp:TableCell>

                                        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
<ContentTemplate>--%>
                        <fieldset style="width:380px; height:186px;" >
                <legend>Datos del Articulo</legend>
                    <div>

                        <table runat="server" style="background-color:transparent;" >

                            <tr style="  vertical-align:text-top   ">
                                <td style="  width:170px; background-color:transparent;">
                                    <asp:Label ID="Label3" runat="server" Text="Codigo" ></asp:Label> 
                                </td>
                                <td style=" width:250px; background-color:transparent;">
                                       <telerik:RadLabel  ID="allcombos" runat="server" ForeColor="#006699" Font-Bold="true"  Height="20px" ></telerik:RadLabel>  
                                </td>
                            </tr>


                            <tr style=" height:25px;  ">
                                <td style="  width:170px; background-color:transparent;">
                                    <asp:Label ID="Label1" runat="server" Text="Descripción"></asp:Label>
                                </td>
                                <td style=" width:250px; background-color:transparent;">
                              <telerik:RadTextBox ID="rad_TxtdescArt" 
                                    EnabledStyle-CssClass="cssTxtEnabled"
                                    DisabledStyle-CssClass ="cssTxtEnabled"
                                    HoveredStyle-CssClass="cssTxtHovered"
                                    FocusedStyle-CssClass="cssTxtFocused"
                                    InvalidStyle-CssClass="cssTxtInvalid"
                                    MaxLength="50" runat="server"  Resize="None" Width="250px"></telerik:RadTextBox>                                
                                </td>
                            </tr>

                            <tr style=" height:25px;  ">
                                <td style=" width:170px; background-color:transparent;">
                                     <asp:Label ID="Label2" runat="server" Text="Abreviatura" ></asp:Label>
                                </td>
                                <td style=" width:250px; background-color:transparent;">
                         
                                       <telerik:RadTextBox  ID="rad_TxtAbrev" 
                                            EnabledStyle-CssClass="cssTxtEnabled"
                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                            HoveredStyle-CssClass="cssTxtHovered"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid"
                                            MaxLength="15" runat="server" LabelCssClass="" Width="250px"></telerik:RadTextBox>                                          
                                </td>
                            </tr>

                            <tr >
                                <td style=" width:170px; background-color:transparent;">
                                     <asp:Label ID="Label10" runat="server" Text=" Unidad de Medida"></asp:Label>
                                </td>
                                <td >
                                      <telerik:RadComboBox ID="rad_Cbouddmed" HighlightTemplatedItems="true" runat="server"
                                             DropDownCssClass="cssRadComboBox"
                                             Width="250px" DropDownWidth="280px" Height="200px" 
                                             AutoPostBack="true" Enabled="false" >
                                            <HeaderTemplate >
                                                <table style="width:250px;">
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
                                            <ItemTemplate >
                                                <table style="width: 250px;">
                                                    <tr>
                                                        <td style="width:80px;" >
                                                            <%# DataBinder.Eval(Container.DataItem, "uniMedCve")%>
                                                        </td>
                                                        <td style="width: 170px;">
                                                            <%# DataBinder.Eval(Container.DataItem, "uniMedDes") %>
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

                            <tr style=" height:43px;  ">
                                <td style=" width:170px; background-color:transparent;">
                                     <telerik:RadLabel ID="rad_lbl_descrip_extend" runat="server" Text="Descripción Extendida"></telerik:RadLabel>
                                </td>
                                <td style=" width:250px; background-color:transparent;">
                                      <textarea style=" float:right;  margin-right:18px" maxlength="200" id="rad_area_desExted"  
                                        EnabledStyle-CssClass="cssTxtEnabled"
                                        DisabledStyle-CssClass ="cssTxtEnabled"
                                        HoveredStyle-CssClass="cssTxtHovered"
                                        FocusedStyle-CssClass="cssTxtFocused"
                                        InvalidStyle-CssClass="cssTxtInvalid"
                                     cols="28"   runat="server"></textarea>                                          
                                </td>
                            </tr>
                        </table>
                        
                    </div>
         
        </fieldset>

    
               <%-- </ContentTemplate>
                </asp:UpdatePanel>--%>
                </asp:TableCell>

                <asp:TableCell>
                    <fieldset style="width:180px; height:186px; ">
                        <legend>Imagen Artículo</legend>
                                <div  style=" background-color:transparent; width:180px; height:172px;" >
                                   <telerik:RadBinaryImage ID="RadBinaryImage1" AutoAdjustImageControlSize="false"    Width="180px" Height="147px" runat="server" />

                                   <telerik:RadAsyncUpload ID="RadAsyncUpload1" 
                                         InputSize="10" AllowedFileExtension="jpg,jpeg,png,gif"  runat="server" HideFileInput="true"  OnClientFileUploaded="fileUploaded"  OnFileUploaded="RadAsyncUpload1_FileUploaded" 
                                        MaxFileInputsCount="1"  Width="10px"   ></telerik:RadAsyncUpload>






                                     <%-- <telerik:RadAsyncUpload runat="server" ID="RadAsyncUpload1" HideFileInput="true" dir="rtl" MaxFileInputsCount="1" OnClientFileUploaded="fileUploaded"
                    OnFileUploaded="AsyncUpload1_FileUploaded" AllowedFileExtensions="jpeg,jpg,gif,png,bmp"
                    Width="223" Height="22" Skin="Silk">
                    <Localization Select="Seleccionar imagen"  />
                   </telerik:RadAsyncUpload>--%>
                                    
                                   
                               </div>



                     </fieldset>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>

    
         



              

</div>
   
        <fieldset>
            <legend>Medidas</legend>

            <table  border="0"    cellpadding="0" cellspacing="0" style=" width:848px; border-color:transparent; text-align:left; background-color:transparent; margin-left:100px; ">
                <tr>
                    <td style=" width:80px; background-color:transparent;">
                        <asp:Label ID="Largo" runat="server" Text="Largo:"  ></asp:Label>
                    </td>
                    <td  style=" width:132px; background-color:transparent;">
                        <telerik:RadNumericTextBox  ID="rad_Txtlargo" runat="server" Width="80px" 
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid">
                        </telerik:RadNumericTextBox>
                    </td>
                    <td  style=" width:80px; background-color:transparent;">
                         <asp:Label  ID="Label4" runat="server" Text="Ancho:"    ></asp:Label>
                    </td>
                    <td style=" width:132px; background-color:transparent;">
                        <telerik:RadNumericTextBox ID="rad_Txtancho" runat="server" Width="80px" 
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" >
                </telerik:RadNumericTextBox>
                    </td>
                    <td style=" width:80px; background-color:transparent;">
                        <asp:Label ID="Label8" runat="server" Text="Alto:"   ></asp:Label>
                    </td>
                    <td style=" width:132px; background-color:transparent;">
                                                 <telerik:RadNumericTextBox  ID="rad_Txtalt" runat="server" Width="80px" 
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid"
                            >
                        </telerik:RadNumericTextBox>
                    </td>
                    <td style=" width:80px; background-color:transparent;">
                         <asp:Label ID="Label6" runat="server" Text="Peso:"  ></asp:Label>
                    </td>
                    <td style=" width:132px; background-color:transparent;">
                                                  <telerik:RadNumericTextBox ID="rad_Txtpeso" runat="server" Width="80px"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid"
                   ></telerik:RadNumericTextBox>
                    </td>
                </tr>

   </table>
          </fieldset>


             <fieldset>
             <legend>Valuación</legend>
             <table  border="0"   cellpadding="0" cellspacing="0" style=" color:transparent; width:1000px; text-align:left; background-color:transparent; margin-left:6px;">
                <tr>
                    <td style=" width:25%; margin-left:30px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel2" runat="server" ForeColor="Black" Text="Metodo de Valuación:"></telerik:RadLabel>
                    </td>
                    <td style="height:25px; width:25%; ">
                        <telerik:RadComboBox ID="radCmboMetVal" runat="server" HighlightTemplatedItems="true"
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
                                            <td style="width:90px;" >
                                                <%# DataBinder.Eval(Container.DataItem, "metValId")%>
                                            </td>
                                            <td style="width: 190px;">
                                                <%# DataBinder.Eval(Container.DataItem, "metValDes") %>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Literal runat="server" ID="RadComboItemsCount" />
                                </FooterTemplate>
                        </telerik:RadComboBox>
                 </td>
                    <td style=" text-align:left; width:25%; background-color:transparent;">
                        <telerik:RadLabel ID="lblEstándar" Text="Costo Estándar:" runat="server"></telerik:RadLabel>
                    </td>
                    <td style=" text-align:left; width:25%; background-color:transparent;">
                        <telerik:RadNumericTextBox ID="radtxtCstEtndr" runat="server" Enabled="false" Width="210px"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid"
                     MaxLength="8"></telerik:RadNumericTextBox>
                    </td>
                </tr>

                <tr>
                    <td style=" width:25%; background-color:transparent; margin-left:30px;">
                       <telerik:RadLabel ID="RadLabel3" Text="Valor de Reposición:" runat="server" Width="225px"></telerik:RadLabel>
                    </td>
                    <td style=" width:25%; background-color:transparent;">
                      <telerik:RadNumericTextBox ID="radTxtVlorRepo" runat="server" Enabled="false" Width="210px"
                         EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid"
                     MaxLength="8"></telerik:RadNumericTextBox>
                    </td>
                    <td style=" width:25%; text-align:left; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel4" Text="Ult. Precio de Compra:"  Width="180px" runat="server"></telerik:RadLabel>
                    </td>
                    <td style=" width:25%; text-align:left; background-color:transparent;">
                 <telerik:RadNumericTextBox ID="radtxtUltmPre"  runat="server" EnabledStyle-CssClass="cssTxtEnabled" Enabled="false" Width="210px"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid"
                     MaxLength="8"></telerik:RadNumericTextBox>
                    </td>
                </tr>


            </table>
               </fieldset>

        
        <fieldset  >

            <table  border="0"  cellpadding="0" cellspacing="0" style=" width:848px; text-align:left; background-color:transparent; margin-left:6px; ">
                <tr>
                    <td style=" width:169px; background-color:transparent;">
                        <asp:Label ID="Label7" runat="server" Text="Clasificación:"></asp:Label>
                    </td>
                    <td style=" width:310px; background-color:transparent; ">
                        <telerik:RadComboBox ID="rad_Dldclasif"
                            runat="server" 
                            HighlightTemplatedItems="true"
                            DropDownCssClass="cssRadComboBox"  
                            Width="210px" DropDownWidth="310px" Height="200px" 
                            AutoPostBack="true" Enabled="false"
                            >
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
                            <td style="width:90px;"  >
                                <%# DataBinder.Eval(Container.DataItem, "listPreValStr")%>
                            </td>
                            <td style="width: 190px;">
                                <%# DataBinder.Eval(Container.DataItem, "listPreValDes") %>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
                        <FooterTemplate>
                            <asp:Literal runat="server" ID="RadComboItemsCount"  />
                        </FooterTemplate>

                        </telerik:RadComboBox>
                    </td>
                    <td style=" width:195px; background-color:transparent;">
                            <asp:Label ID="Label11" runat="server" Text="Manejo de Existencias:"></asp:Label>
                    </td>
                    <td style=" width:195px; background-color:transparent;">
                             <telerik:RadComboBox ID="rad_Cboexi" runat="server"
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
                                        <td style="width:90px;" >
                                            <%# DataBinder.Eval(Container.DataItem, "listPreValStr")%>
                                        </td>
                                        <td style="width: 190px;">
                                            <%# DataBinder.Eval(Container.DataItem, "listPreValDes") %>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Literal runat="server" ID="RadComboItemsCount"  />
                            </FooterTemplate>
                       </telerik:RadComboBox>
                    </td>
                </tr>
            </table>

          </fieldset>


<div style=" width:100%; display:table;  ">

  
         <fieldset style="    width:46%; height:100px;  display: block;  float:left;"  >   
         <legend>Referencias</legend>
                          <div style="overflow:auto; width:472px; height:82px;" >
                                <asp:DataList ID="dt_referencias" runat="server" DataKeyField="revasec">
                                    <ItemTemplate>
                                       <table  border="0" style=" width:380px; height:10px; text-align:left; background-color:transparent; margin-left:6px;">
                                             <tr style="height:15px; ">
                                                <td style="width:160px;">
                                                  <telerik:RadLabel ID="lbrad" runat="server" Text='<%# Eval("revaDes") %>' Width="100px" ></telerik:RadLabel>
                                                </td>
                                                <td style=" width:220px;  background-color:transparent;"> 
                                                    <telerik:RadTextBox ID="txt_ref" MaxLength="15"  runat="server" Width="210px" Text='<%# Eval("revaValRef") %>' 
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

  
        <fieldset style="  width:48%; height:100px; display: block; float:right;     ">
                <legend>Variables</legend>
                        <div  style="overflow:auto; width:472px; height:82px;" >
                         <asp:DataList ID="dt_variables" runat="server" DataKeyField="revasec" >
                             <ItemTemplate>
                                <table  border="0" style=" width:380px; height:10px; text-align:left; background-color:transparent ;">
                                     <tr style="height:15px; ">
                                         <td style="width:160px;">
                                             <telerik:RadLabel ID="lbrad" runat="server" Text='<%# Eval("revaDes") %>'  Width="100px"  ></telerik:RadLabel>
                                         </td>
                                         <td style=" width:220px; background-color:transparent;  ">
                                             <telerik:RadNumericTextBox ID="txt_var" MaxLength="10"  runat="server"  Width="210px" Text='<%# Eval("revaValVar") %>'
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



 
        
               
    
                    
             
      

     </div>

    </asp:Panel>

            <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
                <telerik:RadImageButton ID="rBtnGuardar"     runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text ="" OnClick="rBtnGuardar_Click" OnClientClicking="OnClientClic_ConfirmOK" ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnCancelar" runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text="" OnClick="rBtnCancelar_Click" OnClientClicking="OnClientClic_ConfirmCancel" ></telerik:RadImageButton>
            </asp:Panel>


    
      
        <telerik:RadWindowManager ID="RadWindowManager" runat="server"></telerik:RadWindowManager>
            <telerik:RadLabel ID="lbl_artic_mensaje" runat="server" Text=""  ForeColor="Red" Font-Size="Medium"></telerik:RadLabel>
             <asp:HiddenField ID="hdfBtnAccion" runat="server" />



                <asp:HiddenField ID="NUMCOMBO" VALUE="1" runat="server" />
                <asp:HiddenField ID="parmsec"  runat="server" />
                <asp:HiddenField ID="arregloImagen"   runat="server" />
       

                   

<%-- </ContentTemplate>
</asp:UpdatePanel> --%>
</form>
</body>
</html>
