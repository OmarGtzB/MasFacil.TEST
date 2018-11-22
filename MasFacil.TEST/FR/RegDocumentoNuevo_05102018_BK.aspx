<%@ Page Title="" Language="C#" MasterPageFile="~/MPForm.master" AutoEventWireup="true" CodeFile="RegDocumentoNuevo.aspx.cs" Inherits="FR_RegDocumentoNuevo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMPForm" Runat="Server">

    <script src="http://code.jquery.com/jquery-1.11.0.min.js"></script>
    
    <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
    <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
    <link href="~/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />
    <link href="../css/styles.css" rel="stylesheet" type="text/css" />


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMPForm" Runat="Server">

    <script type="text/javascript">

        function formatFol(sender, eventArg) {
            
            var originalValue;
            var longClie;
            var posClie;
            var charFormat;
            var newValue;

            
            alert(eventArg.value);
            
            originalValue = document.getElementById("rTxtCliente").value;
            longClie = document.getElementById("hdfLongFol").value;
            posClie = document.getElementById("hdfPosFol").value;
            charFormat = document.getElementById("hdfCharFormat").value;

            intLongClie = parseInt(longClie);

            if (posClie == "1") {
                newValue = padding_left(originalValue, charFormat, intLongClie);
            } else if (posClie == "2") {
                newValue = padding_right(originalValue, charFormat, intLongClie);
            }
            else {
                return false;
            }

            document.getElementById("rTxtFolio").value = newValue;
            
        }

        // left padding s with c to a total of n chars
        function padding_left(s, c, n) {
            if (!s || !c || s.length >= n) {
                return s;
            }
            var max = (n - s.length) / c.length;
            for (var i = 0; i < max; i++) {
                s = c + s;
            }
            return s;
        }

        // right padding s with c to a total of n chars
        function padding_right(s, c, n) {
            if (!s || !c || s.length >= n) {
                return s;
            }
            var max = (n - s.length) / c.length;
            for (var i = 0; i < max; i++) {
                s += c;
            }
            return s;
        }

    </script>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>



        <div>

            <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none; width:100%; text-align:left; margin-top:5px; background-color:transparent ;">
                <tr  style="width:100%;">

                    <td style=" width:100%; background-color:transparent; vertical-align:top;">

                            <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none; width:1280px; text-align:left; background-color:transparent;">
                                <tr  style="width:1280px;">                              
                                    <td style=" width:90px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label1" runat="server" Text="Documento"></asp:Label>
                                    </td>
                                    <td style=" width:260px; background-color:transparent; vertical-align:top;">
                                        <telerik:RadComboBox ID="rCboDocumento" runat="server" Width="250px" 
                                            OnSelectedIndexChanged="rCboDocumento_SelectedIndexChanged"
                                            HighlightTemplatedItems="true"
                                            DropDownCssClass="cssRadComboBox"  
                                            DropDownWidth="360px" Height="200px" 
                                            AutoPostBack="True" >
                                            <HeaderTemplate>
                                                <table style="width: 400px" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td style="width: 110px;">
                                                            Documento
                                                        </td>
                                                       <td style="width: 200px;">
                                                            Descripcion
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                              <ItemTemplate>
                                                <table style="width: 400px;"  cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td style="width: 110px;">
                                                        <%# DataBinder.Eval(Container.DataItem, "docCve") %>
                                                        </td>
                                                        <td style="width: 200px;">
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




                                      <td style=" width:180px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="rLblFormaPago" runat="server" Text="Forma Pago"></asp:Label>
                                    </td>
                                    <td style=" width:260px; background-color:transparent; vertical-align:top;">
                                        <telerik:RadComboBox ID="rCboFormaPago" runat="server" Width="250px" 
                                            OnSelectedIndexChanged="rCboFormaPago_SelectedIndexChanged"
                                            HighlightTemplatedItems="true"
                                            DropDownCssClass="cssRadComboBox"  
                                            DropDownWidth="360px" Height="200px" 
                                            AutoPostBack="True" >
                                            <HeaderTemplate>
                                                <table style="width: 400px" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td style="width: 110px;">
                                                            Forma Pago
                                                        </td>
                                                       <td style="width: 200px;">
                                                            Descripción
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                              <ItemTemplate>
                                                <table style="width: 400px;"  cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td style="width: 110px;">
                                                        <%# DataBinder.Eval(Container.DataItem, "satFormaPagCve") %>
                                                        </td>
                                                        <td style="width: 200px;">
                                                        <%# DataBinder.Eval(Container.DataItem, "satFormaPagDes") %>
                                                        </td>
                                                    </tr>
                                                </table>
                                             </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                </FooterTemplate>
                                        </telerik:RadComboBox>
                                    </td>






                                    <td style=" width:750px; background-color:transparent; vertical-align:top;">
                            
                                    </td>

                                     <td style=" width:90px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="rLblIncoterm" runat="server" Text="Incoterms"></asp:Label>
                                    </td>

                                         <td style=" width:300px; background-color:transparent; vertical-align:top;">
                                        <telerik:RadComboBox ID="rCboIncoterm" runat="server" Width="250px" 
                                            OnSelectedIndexChanged="rCboIncoterm_SelectedIndexChanged"
                                            HighlightTemplatedItems="true"
                                            DropDownCssClass="cssRadComboBox"  
                                            DropDownWidth="360px" Height="200px" 
                                            AutoPostBack="True" >
                                            <HeaderTemplate>
                                                <table style="width: 600px" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td style="width: 110px;">
                                                            Incoterms
                                                        </td>
                                                       <td style="width: 200px;">
                                                            Descripción
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                              <ItemTemplate>
                                                <table style="width: 400px;"  cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td style="width: 110px;">
                                                        <%# DataBinder.Eval(Container.DataItem, "incoCve") %>
                                                        </td>
                                                        <td style="width: 200px;">
                                                        <%# DataBinder.Eval(Container.DataItem, "incoDes") %>
                                                        </td>
                                                    </tr>
                                                </table>
                                             </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                </FooterTemplate>
                                        </telerik:RadComboBox>
                                    </td>


                                    <td style=" width:200px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label4" runat="server" Text="Fecha"></asp:Label>
                                    </td>


                                    <td style=" width:110px; background-color:transparent; vertical-align:top;">
                                        
                                        <telerik:RadDateInput ID="rFchDoc" OnTextChanged="rFchDoc_TextChanged" AutoPostBack="true" runat="server" Width="100px" Culture="es-MX" EmptyMessage="Ingresa Fecha Valida"
                                            InvalidStyleDuration="100" >
                                        </telerik:RadDateInput>
                                    </td>

                                




                                </tr>
                            </table>                          

                    </td>

                </tr>
            </table>

            <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none; width:100%; text-align:left; background-color:transparent;"  align="left">
                <tr  style="width:100%;"  align="left">
                    <td style=" width:100%; background-color:transparent; vertical-align:top;">

                            <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none; width:700px; text-align:left;  float:left;  background-color:transparent ;">
                                <tr  style="width:700px;">
                                    <td style=" width:700px; background-color:transparent; vertical-align:top;" >
                        
                                        <fieldset id="panelCliente" runat="server" style="">
                                            <legend>Cliente</legend>
                                            <div style="width:100%; display:table; position:static; background-color:transparent;" > 
                            
                                            <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none; width:690px; text-align:left; background-color:transparent;">
                                                <tr  style="width:690px;"> 
                                                                 
                                                    <td style=" width:65px; background-color:transparent; vertical-align:top;">
                                                        <asp:Label ID="Label1a" runat="server" Text="Cliente"></asp:Label>
                                                    </td>

                                                    <td style=" width:270px; background-color:transparent; vertical-align:top;">
                                                        <telerik:RadComboBox ID="rCboCliente" runat="server" Width="250px" 
                                                                     HighlightTemplatedItems="true"
                                                                     DropDownCssClass="cssRadComboBox"  
                                                                     DropDownWidth="530px" Height="400px" 
                                                                     AutoPostBack="True" OnSelectedIndexChanged="rCboCliente_SelectedIndexChanged" >
                                                    <HeaderTemplate>
                                                        <table style="width: 760px" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                 <td style="width: 110px;">
                                                                    Clave
                                                                </td>
                                                                 <td style="width: 400px;">
                                                                    Nombre
                                                                </td>
                                                              
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table style="width: 760px" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 110px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "cliCve") %>
                                                                </td>
                                                                <td style="width: 400px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "clieNom") %>
                                                                </td>

                                                            </tr>
                                                        </table>

                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                    </FooterTemplate>
                                                </telerik:RadComboBox>
                                                    </td>

                                                    <td style=" width:150px; background-color:transparent; vertical-align:top;">
                                                        <asp:Label ID="Label2" runat="server" Text="Direccion Entrega"></asp:Label>
                                                    </td>

                                                    <td style=" width:205px; background-color:transparent; vertical-align:top;">
                                                        <telerik:RadComboBox ID="rCboDireccionEntrega" runat="server" Width="150px" 
                                                                     HighlightTemplatedItems="true"
                                                                     DropDownCssClass="cssRadComboBox"  
                                                                     DropDownWidth="500px" Height="200px" 
                                                                     AutoPostBack="True" >
                                                                         <HeaderTemplate>
                                                                         <table style="width: 450px;"  cellspacing="0" cellpadding="0">
                                                                            <tr>
                                                                                <td style="width: 450px;">
                                                                                    Direccion
                                                                                </td>
                                                         
                                                                            </tr>
                                                                        </table>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                              <table style="width: 450px;"  cellspacing="0" cellpadding="0">
                                                                                <tr>
                                                                                    <td style="width: 450px;">
                                                                                    <%# DataBinder.Eval(Container.DataItem, "cliDirEntDom") %>
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

                                    </td>
                                </tr>


                            </table>

                            <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none; width:565px ; text-align:left;  float:right; background-color:transparent ;">
                                <tr  style="width:565px;">
                                    <td style=" width:565px; background-color:transparent; vertical-align:top;" >

                                        <fieldset style="">
                                            <legend>Otros</legend>
                                            <div style="width:100%; display:table; position:static; background-color:transparent;" > 

                                            <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none; width:550px; text-align:left; background-color:transparent;">
                                                <tr  style="width:550px;"> 

                                                    <td style=" width:160px; background-color:transparent; vertical-align:top;">
                                                        <asp:Label ID="Label3s" runat="server" Text="Via Embarque"></asp:Label>
                                                    </td>

                                                    <td style=" width:240px; background-color:transparent; vertical-align:top;">
                                                        <telerik:RadComboBox ID="rCboViaEmbarque" runat="server" Width="180px" 
                                                            HighlightTemplatedItems="true"
                                                            DropDownCssClass="cssRadComboBox"  
                                                            DropDownWidth="340px" Height="200px" 
                                                            AutoPostBack="True" >
                                                                <HeaderTemplate>
                                                                    <table style="width: 330px" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td style="width: 80px;">
                                                                                Clave
                                                                            </td>

                                                  
                                                                           <td style="width: 240px;">
                                                                                Descripcion
                                                                            </td>

                                                                        </tr>
                                                                    </table>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <table style="width: 330px;"  cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td style="width: 80px;">
                                                                            <%# DataBinder.Eval(Container.DataItem, "viaEmbCve") %>
                                                                            </td>

                                                                            <td style="width: 240px;">
                                                                            <%# DataBinder.Eval(Container.DataItem, "viaEmbDes") %>
                                                                            </td>

                                                                        </tr>
                                                                    </table>

                                                                </ItemTemplate>

                                                                    <FooterTemplate>
                                                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                                    </FooterTemplate>
                                                        </telerik:RadComboBox>
                                                    </td>

                                                    <td style=" width:70px; background-color:transparent; vertical-align:top;">
                                                        <asp:Label ID="Label3sd" runat="server" Text="Moneda"></asp:Label>
                                                    </td>

                                                    
                                                    <td style=" width:180px; background-color:transparent; vertical-align:top;">
                                                        <telerik:RadComboBox ID="rCboMoneda" runat="server" Width="160px"
                                                            OnSelectedIndexChanged="rCboMoneda_SelectedIndexChanged"  
                                                            HighlightTemplatedItems="true"
                                                            DropDownCssClass="cssRadComboBox"  
                                                            DropDownWidth="210px" Height="200px" 
                                                            AutoPostBack="True" >
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
                                                                                <%# DataBinder.Eval(Container.DataItem, "monCve")%>
                                                                            </td>
                                                                            <td style="width: 170px;">
                                                                                <%# DataBinder.Eval(Container.DataItem, "monDes") %>
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

                                    </td>
                                </tr>
                            </table>


                     </td>
                </tr>
            </table>
    
<%--  ------------------------------------------------------------------------------------------------------------------------------------------------------------------------- --%>         
           <%-- REFERENCIAS --%>

           <div >


                <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none; width:100%; text-align:left; background-color:transparent ;">
                <tr>

                <%-- REFERENCIAS --%>
                <div id="divRef"  runat="server" visible="false" >
                    <td>
                         <fieldset style="  margin-top:5px;   width:452px; display: block; text-align:left;" >
                            <legend>Referencias</legend>   
                                <div style=" width:620px; height:56px; overflow:auto; background-color:transparent;"  >
                                    <asp:DataList ID="DataListRef" runat="server" DataKeyField="docCve">
                                        <ItemTemplate>
                                                <telerik:RadLabel ID="RadLabel19" runat="server" Text='<%# Eval("revaDes") %>' Width="110px"></telerik:RadLabel>
                                                <telerik:RadTextBox ID="rTxt_listRef" runat="server" Width="200px"  EnabledStyle-CssClass="cssTxtEnabled"
                                                    DisabledStyle-CssClass ="cssTxtEnabled" Enabled="true"
                                                    HoveredStyle-CssClass="cssTxtHovered"
                                                    FocusedStyle-CssClass="cssTxtFocused"  Text='<%# Eval("revaValRef") %>'
                                                    InvalidStyle-CssClass="cssTxtInvalid">
                                                </telerik:RadTextBox>
                                                </ItemTemplate>
                                    </asp:DataList>
                                </div>
                           </fieldset>
                    </td>
                </div>

                     <%--VARIABLES--%>
                  <div id="divVar"  runat="server" visible="false"  >
                    <td>
                           <fieldset style="  margin-top:5px;   width:452px; display: block; text-align:left;" >
                            <legend>Variables</legend>   
                                <div style=" width:620px; height:56px; overflow:auto; background-color:transparent; "  >
                                    <asp:DataList ID="DataListVar" runat="server" DataKeyField="docCve">
                                        <ItemTemplate>
                                                <telerik:RadLabel ID="RadLabel19" runat="server" Text='<%# Eval("revaDes") %>' Width="110px"></telerik:RadLabel>
                                                <telerik:RadNumericTextBox ID="rTxt_listVar" runat="server"  Width="200px" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="2" 
                                                    EnabledStyle-CssClass="cssTxtEnabled" Enabled="true"
                                                    DisabledStyle-CssClass ="cssTxtEnabled"
                                                    HoveredStyle-CssClass="cssTxtHovered"
                                                    FocusedStyle-CssClass="cssTxtFocused"  Text='<%# Eval("revaValVar") %>'
                                                    InvalidStyle-CssClass="cssTxtInvalid" 
                                                 ></telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                    </asp:DataList>
                                </div>
                           </fieldset>

                      </td>
                 </div>


                </tr>
                </table>



           </div>
              
<%--  ------------------------------------------------------------------------------------------------------------------------------------------------------------------------- --%>


            <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none; width:100%; text-align:left; background-color:transparent ;">
                <tr  style="width:100%;">

                    <td style=" width:100%; background-color:transparent; vertical-align:top;">
                        
                        <fieldset style=" margin-top:15px ">
                            <div style="width:100%; display:table; position:static; background-color:transparent;" align="center" > 
                            
                            
                            <table  style=" border-bottom-style:inset; border-bottom-width:1px; border-bottom-color:#355C82; width:1270px; text-align:left;  background-color:aliceblue;">
                                <tr  style="width:1270px;">                              
                                    <td style=" width:50px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label3" runat="server" Text="Folio"></asp:Label>
                                    </td>
                                    <td style=" width:170px; background-color:transparent; vertical-align:top;">
                                        <telerik:RadTextBox ID="rTxtFolio" runat="server" 
                                            OnTextChanged="rTxtFolio_TextChanged" 
                                             AutoPostBack="true"
                                            MaxLength="20" Width="120px"
                                            EnabledStyle-CssClass="cssTxtEnabled"
                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                            HoveredStyle-CssClass="cssTxtHovered"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid" 
                                         ></telerik:RadTextBox>
                                    </td>

                                    <td style=" width:100px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label9" runat="server" Text="Descripcion"></asp:Label>
                                    </td>
                                    <td style=" width:310px; background-color:transparent; vertical-align:top;">
                                        <telerik:RadTextBox ID="rTxtDescripcion" runat="server"   Width="300px"
                                            EnabledStyle-CssClass="cssTxtEnabled"
                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                            HoveredStyle-CssClass="cssTxtHovered"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid" 
                                         ></telerik:RadTextBox>
                                    </td>



                                    <td style=" width:70px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="rLblLstPreciosTag" runat="server" Text="L. Precios"></asp:Label>
                                    </td>

                                                    
                                    <td style=" width:180px; background-color:transparent; vertical-align:top;">
                                        <telerik:RadComboBox ID="rCboLstPrecios" runat="server" Width="160px" 
                                            HighlightTemplatedItems="true"
                                            DropDownCssClass="cssRadComboBox"  
                                            OnSelectedIndexChanged="rCboLstPrecios_SelectedIndexChanged"
                                            DropDownWidth="280px" Height="200px" 
                                            AutoPostBack="True" >
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
                                                                <%# DataBinder.Eval(Container.DataItem, "lisPreCve")%>
                                                            </td>
                                                            <td style="width: 170px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "lisPreDes") %>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                </FooterTemplate>
                                        </telerik:RadComboBox>
                                    </td>

                                    <td style=" width:70px; background-color:transparent; vertical-align:top;">
                                                        <asp:Label ID="rLblMetodoPago" runat="server" Text="Pago"></asp:Label>
                                                    </td>

                                                    <td style=" width:180px; background-color:transparent; vertical-align:top;">
                                                        <telerik:RadComboBox ID="rCboMetodoPago" runat="server"  AutoPostBack="True"
                                              Enabled="false" 
                              HighlightTemplatedItems="true"
                              DropDownCssClass="cssRadComboBox"  
                              Width="250px" DropDownWidth="390px" Height="200px" >
                                            <HeaderTemplate>
                        <table style="width: 360px" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width: 160px;">
                                        Clave
                                    </td>
                                    <td style="width: 200px;">
                                        Descripción
                                    </td>
                                </tr>
                            </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                            <table style="width: 360px;"  cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width:160px" >
                                        <%# DataBinder.Eval(Container.DataItem, "satMetPagCve")%>
                                    </td>
                                    <td style="width: 200px;">
                                        <%# DataBinder.Eval(Container.DataItem, "satMetPagDes") %>
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

                  
                            <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none; width:100%; text-align:left; background-color:transparent ;">
                                <tr  style="width:100%;">
                                    <td style=" width:100%; background-color:transparent; vertical-align:top;">


                                            <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none; width:1270px; text-align:left; background-color:transparent;">
                                                <tr  style="width:1270px;">   
                                                    <td style=" width:400px; background-color:transparent; vertical-align:bottom;">

                                                            <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
                                                                <table id="tableBtnsAcciones_Texil" runat="server"  border="0" cellpadding="0" cellspacing="0" style=" border-style:none; width:100%; text-align:left; background-color:transparent;">
                                                                    <tr style="width:100%;">  
                                                                        <td style=" width:100%; background-color:transparent; vertical-align:bottom;">
                                                                            <telerik:RadImageButton ID="rBtnNuevoPartidaTexil"    runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"  ToolTip="Nueva Textil"  Text=""  OnClick="rBtnNuevoPartidaTexil_Click"></telerik:RadImageButton>                                                     
                                                                        </td> 
                                                                    </tr>
                                                                </table> 
                                                                <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none; width:100%; text-align:left; background-color:transparent;">
                                                                    <tr style="width:100%;">  
                                                                        <td style=" width:100%; background-color:transparent; vertical-align:bottom;">
                                                                            <telerik:RadImageButton ID="rBtnNuevo"     runat ="server" Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text ="" OnClick="rBtnNuevo_Click"></telerik:RadImageButton>
                                                                            <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  ToolTip="Modificar"  Text="" OnClick="rBtnModificar_Click"></telerik:RadImageButton>
                                                                            <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text="" OnClick="rBtnEliminar_Click" OnClientClicking="OnClientClic_ConfirmOK" ></telerik:RadImageButton>
                                                                            <telerik:RadImageButton ID="rBtnEditDesc"   runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarRegistroDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnModificarRegistro.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarRegistroHovered.png"  ToolTip="Editar Descripcion Partida"  Text="" OnClick="rBtnEditDesc_Click" ></telerik:RadImageButton>
                                                                            <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"  Text="" OnClick="rBtnLimpiar_Click" ></telerik:RadImageButton>
                                                                        </td> 
                                                                    </tr>
                                                                </table> 
                                                            </asp:Panel>    

                                                    </td>
                                                    <td style=" width:870px; background-color:transparent; vertical-align:top; position:static;  ">

                                                            <fieldset id="frmNewPartida" runat="server" style=" margin-top:15px; background-color:aliceblue; ">
                                                                <div style="width:100%; display:table; position:static; background-color:transparent; " align="center" > 
                                                                        <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none; width:1050px; text-align:left; float:right; background-color:transparent;">
                                                                            <tr  style="width:1000px;">   
                                                                                <td style=" width:150px; background-color:transparent; vertical-align:top;">
                                                                                    <asp:Label ID="Label6" runat="server" Text="Articulo"></asp:Label><br />

                                                                                     <telerik:RadComboBox ID="rCboArticulo" runat="server" Width="150px"  
                                                                                        HighlightTemplatedItems="true" AllowCustomText="true" 
                                                                                        DropDownCssClass="cssRadComboBox"  
                                                                                        OnSelectedIndexChanged ="rCboArticulo_SelectedIndexChanged"
                                                                                        DropDownWidth="500px" Height="200px" 
                                                                                        AutoPostBack="True" >
                                                                                                 <HeaderTemplate>
                                                                                                 <table style="width: 500px;"  cellspacing="0" cellpadding="0">
                                                                                                    <tr>
                                                                                                        <td style="width: 150px;">
                                                                                                            Clave
                                                                                                        </td>
                                                            
                                                                                                        <td style="width: 350px;">
                                                                                                            Descripcion
                                                                                                        </td>
                                                         
                                                                                                    </tr>
                                                                                                </table>
                                                                                                </HeaderTemplate>
                                                                                                <ItemTemplate>
                                                                                                      <table style="width: 500px;"  cellspacing="0" cellpadding="0">
                                                                                                        <tr>
                                                                                                             <td style="width: 150px;">
                                                                                                            <%# DataBinder.Eval(Container.DataItem, "artCve") %>
                                                                                                            </td>

                                                                                                             <td style="width: 350px;">
                                                                                                            <%# DataBinder.Eval(Container.DataItem, "artDes") %>
                                                                                                            </td>

                                                                                                        </tr>
                                                                                                    </table>

                                                                                                </ItemTemplate>

                                                                                                <FooterTemplate>
                                                                                                    <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                                                                </FooterTemplate>
                                                                                    </telerik:RadComboBox>

                                                                                </td>

                                                                                 <td style=" width:30px; background-color:transparent; vertical-align:top;">
                                                                                </td>

                                                                                <td style=" width:100px; background-color:transparent; vertical-align:top;">
                                                                                    <asp:Label ID="rLblAlmacenTag" runat="server" Text="Almacen"></asp:Label><br />
                                                                                     <telerik:RadComboBox ID="rCboAlmacen" runat="server" Width="150px" 
                                                                                        HighlightTemplatedItems="true"
                                                                                        DropDownCssClass="cssRadComboBox"
                                                                                        DropDownWidth="500px" Height="200px" 
                                                                                        AutoPostBack="True" >
                                                                                                 <HeaderTemplate>
                                                                                                 <table style="width: 500px;"  cellspacing="0" cellpadding="0">
                                                                                                    <tr>
                                                                                                        <td style="width: 150px;">
                                                                                                            Clave
                                                                                                        </td>
                                                            
                                                                                                        <td style="width: 350px;">
                                                                                                            Descripcion
                                                                                                        </td>
                                                         
                                                                                                    </tr>
                                                                                                </table>
                                                                                                </HeaderTemplate>
                                                                                                <ItemTemplate>
                                                                                                      <table style="width: 500px;"  cellspacing="0" cellpadding="0">
                                                                                                        <tr>
                                                                                                             <td style="width: 150px;">
                                                                                                            <%# DataBinder.Eval(Container.DataItem, "almCve") %>
                                                                                                            </td>

                                                                                                             <td style="width: 350px;">
                                                                                                            <%# DataBinder.Eval(Container.DataItem, "almDes") %>
                                                                                                            </td>

                                                                                                        </tr>
                                                                                                    </table>

                                                                                                </ItemTemplate>

                                                                                                <FooterTemplate>
                                                                                                    <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                                                                </FooterTemplate>
                                                                                    </telerik:RadComboBox>
                                                                                </td>

                                                                                <td style=" width:30px; background-color:transparent; vertical-align:top;">
                                                                                </td>

                                                                                <td>
                                                                                <asp:Label ID="lbl_lote" runat="server" Text="Lote"></asp:Label><br />

                                                                                    <telerik:RadTextBox ID="rTxtPartLote" Width="90px"
                                                                                        EnabledStyle-CssClass="cssTxtEnabled"
                                                                                        DisabledStyle-CssClass ="cssTxtEnabled"
                                                                                        HoveredStyle-CssClass="cssTxtHovered"
                                                                                        FocusedStyle-CssClass="cssTxtFocused"
                                                                                        InvalidStyle-CssClass="cssTxtInvalid" 
                                                                                        
                                                                                        runat="server" 
                                                                                     ></telerik:RadTextBox>
                                                                                </td>

                                                                                  <td>
                                                                                <asp:Label ID="lbl_serie" runat="server" Text="Serie"></asp:Label><br />

                                                                                    <telerik:RadTextBox ID="rTxtPartSerie" Width="90px"
                                                                                        EnabledStyle-CssClass="cssTxtEnabled"
                                                                                        DisabledStyle-CssClass ="cssTxtEnabled"
                                                                                        HoveredStyle-CssClass="cssTxtHovered"
                                                                                        FocusedStyle-CssClass="cssTxtFocused"
                                                                                        InvalidStyle-CssClass="cssTxtInvalid" 
                                                                                        runat="server" 
                                                                                     ></telerik:RadTextBox>
                                                                                </td>



                                                                                <td  style=" width:70px; background-color:transparent; vertical-align:top;">
                                                                                    <telerik:RadLabel ID="rFchEntPartTag" runat="server" Text="F. Entrega" Width="80px"></telerik:RadLabel>  
                                                                        
                                                                                    <telerik:RadDateInput ID="rFchEntPart" runat="server" Width="100px" Culture="es-MX" EmptyMessage="Ingresa Fecha Valida"
                                                                                        InvalidStyleDuration="100" >
                                                                                    </telerik:RadDateInput>
                                                                                </td>

                                                                                <td style=" width:30px; background-color:transparent; vertical-align:top;">
                                                                                </td>

                                                                                <td style=" width:70px; background-color:transparent; vertical-align:top;">
                                                                                    <asp:Label ID="Label8" runat="server" Text="Cantidad"></asp:Label>
                                                                                
                                                                                    <telerik:RadTextBox ID="rTxtPartCant" Width="90px"
                                                                                        EnabledStyle-CssClass="cssTxtEnabled"
                                                                                        DisabledStyle-CssClass ="cssTxtEnabled"
                                                                                        HoveredStyle-CssClass="cssTxtHovered"
                                                                                        FocusedStyle-CssClass="cssTxtFocused"
                                                                                        InvalidStyle-CssClass="cssTxtInvalid" 
                                                                                        OnTextChanged="rTxtPartCant_TextChanged"
                                                                                        AutoPostBack="true"
                                                                                        runat="server" 
                                                                                     ></telerik:RadTextBox>
                                                                                </td>
                                                                                <td style=" width:30px; background-color:transparent; vertical-align:top;">
                                                                                </td>
                                                                                <td style=" width:70px; background-color:transparent; vertical-align:top;">
                                                                                    <asp:Label ID="Label11" runat="server" Text="Precio"></asp:Label>
                                                                                
                                                                                    <telerik:RadNumericTextBox runat="server" ID="rTxtPartPrec" Width="100px" Value="1" EmptyMessage="Enter units count" MinValue="0" ShowSpinButtons="false" NumberFormat-DecimalDigits="2" Type="Currency"></telerik:RadNumericTextBox>
                                                                                </td>
                                                                                
                                                                                <td style=" width:100px; background-color:transparent; vertical-align:central; text-align:center">
                                     
                                                                                        <telerik:RadImageButton ID="rImgBtnAceptarP" runat ="server" Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnAutorizarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnAutorizar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnAutorizarHovered.png"    ToolTip="Nuevo"  Text ="" OnClick="rImgBtnAceptarP_Click"></telerik:RadImageButton>
                                                                                        <telerik:RadImageButton ID="rImgBtnCancelarP" Width="30px" Height="30px" runat="server" Text="" ToolTip="Cancelar" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png" OnClick="rImgBtnCancelarP_Click" ></telerik:RadImageButton>

                                                                                </td>
                                                                            </tr>
                                                                        </table>   
                                                                </div>
                                                             </fieldset>
                                                            
                                                            <fieldset id="frmCustomDescArt" runat="server" style=" margin-top:15px; background-color:aliceblue; ">

                                                                <div style="width:1050px; display:table; position:static; background-color:transparent; " align="center" > 
                                                                        <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none; width:1050px; text-align:left; float:right; background-color:transparent;">
                                                                            <tr  style="width:1050px; vertical-align:middle;">   
                                                                                <td style=" width:200px; background-color:transparent; vertical-align:top;">
                                                                                    <asp:Label ID="Label5" runat="server" Text="Descripción Personalizada"></asp:Label><br />
                                                                                </td>
                                                                                <td style=" width:750px; background-color:transparent; vertical-align:top;">

                                                                                    <telerik:RadTextBox ID="rTxtCustomDescArt" Width="740px" MaxLength="120" TextMode="MultiLine"
                                                                                        EnabledStyle-CssClass="cssTxtEnabled"
                                                                                        DisabledStyle-CssClass ="cssTxtEnabled"
                                                                                        HoveredStyle-CssClass="cssTxtHovered"
                                                                                        FocusedStyle-CssClass="cssTxtFocused"
                                                                                        InvalidStyle-CssClass="cssTxtInvalid" 
                                                                                        AutoPostBack="true"
                                                                                        runat="server" 
                                                                                     ></telerik:RadTextBox>
                                                                                </td>

                                                                                <td style=" width:100px; background-color:transparent; vertical-align:central; text-align:center">
                                     
                                                                                        <telerik:RadImageButton ID="rImgBtnAceptarC" runat ="server" Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnAutorizarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnAutorizar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnAutorizarHovered.png"    ToolTip="Nuevo"  Text ="" OnClick="rImgBtnAceptarC_Click"></telerik:RadImageButton>
                                                                                        <telerik:RadImageButton ID="rImgBtnCancelarC" Width="30px" Height="30px" runat="server" Text="" ToolTip="Cancelar" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png" OnClick="rImgBtnCancelarC_Click" ></telerik:RadImageButton>

                                                                                </td>

                                                                            </tr>
                                                                        </table>
                                                                </div>

                                                            </fieldset>

                                                            <fieldset id="frmNewPartidaTextil" runat="server" style=" margin-top:15px; background-color:aliceblue; ">
                                                                <div style="width:1050px; display:table; position:static; background-color:transparent; " align="center" > 
                                                                        <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none; width:1050px; height:40px; text-align:left; float:right; background-color:transparent;">
                                                                            <tr  style="width:1050px; vertical-align:middle;">   
                                                                                <td style=" width:640px; background-color:transparent; vertical-align:central;">
                                                                                     
                                                                                </td>
                                                                                <td style=" width:100px; background-color:transparent; vertical-align:central;">
                                                                                    <asp:Label ID="Label7" runat="server" Text="Operación"></asp:Label><br />
                                                                                </td>
                                                                                <td style=" width:210px; background-color:transparent; vertical-align:central;">
                                                                                    <telerik:RadNumericTextBox ID="txtTextilOperacion" Width="200px" runat="server"  
                                                                                        EnabledStyle-CssClass="cssTxtEnabled"
                                                                                        DisabledStyle-CssClass ="cssTxtEnabled"
                                                                                        HoveredStyle-CssClass="cssTxtHovered"
                                                                                        FocusedStyle-CssClass="cssTxtFocused"
                                                                                        InvalidStyle-CssClass="cssTxtInvalid">
                                                                                    <NumberFormat GroupSeparator="" DecimalDigits="0"/>  
                                                                                    </telerik:RadNumericTextBox>

                                                                                </td>
                                                                                <td style=" width:100px; background-color:transparent; vertical-align:central; text-align:right">
                                                                                        <telerik:RadImageButton ID="rBtnNuevoPartidaTexil_Ok" runat ="server" Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnAutorizarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnAutorizar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnAutorizarHovered.png"    ToolTip="Nuevo"  Text ="" OnClick="rBtnNuevoPartidaTexil_Ok_Click" ></telerik:RadImageButton>
                                                                                        <telerik:RadImageButton ID="rBtnNuevoPartidaTexil_Cancel" Width="30px" Height="30px" runat="server" Text="" ToolTip="Cancelar" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png" Visible="false"   ></telerik:RadImageButton>
                                                                                </td>

                                                                            </tr>
                                                                        </table>
                                                                </div>
                                                            </fieldset>

                                                    </td>
                                                </tr>
                                            </table>     


                                    </td>
                                </tr>
                            </table>

                            <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none; width:100%; text-align:center; margin-top:5px; background-color:transparent; ">
                                <tr  style="width:100%;"  align="center">
                                    <td style=" width:100%; background-color:transparent; vertical-align:top;  text-align:center"  align="center">

                            <telerik:RadGrid ID="rGdv_RegistoDetalle" 
                                                        
                                            runat="server" 
                                            AutoGenerateColumns="False"
                                            AllowMultiRowSelection="true" 
                                            OnSelectedIndexChanged="rGdv_RegistoDetalle_SelectedIndexChanged"
                                            Width="1265px"   Height="175px"   
                                            CssClass="Grid" 
                                            Skin="Office2010Silver">

                                            <MasterTableView DataKeyNames="docRegPartId" AutoGenerateColumns="False" CssClass="GridTable" ClientDataKeyNames="docRegPartId" >
                                                <Columns> 
                                                    
                                                    <%--<telerik:GridClientSelectColumn UniqueName="ClientSelectColumn1"  ItemStyle-Width="30px" HeaderStyle-Width="30px">
                                                    </telerik:GridClientSelectColumn>--%>
                                                    
                                                    <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" ItemStyle-Width="30px" HeaderStyle-Width="30px" Display="false">
                                                        <ItemTemplate>
                                                        <asp:CheckBox Width="15px" ID="CheckBox1" runat="server" 
                                                            AutoPostBack="True" />
                                                        </ItemTemplate>
                                                        <HeaderTemplate>
                                                        <asp:CheckBox Width="15px" ID="headerChkbox" runat="server" 
                                                            AutoPostBack="True" />
                                                        </HeaderTemplate>
                                                    </telerik:GridTemplateColumn>

                                                    <telerik:GridBoundColumn HeaderText="Articulo" DataField="artCve" HeaderStyle-Width="100px"  ItemStyle-Width="50px" ></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Descripcion" DataField="artDes" HeaderStyle-Width="100px"  ItemStyle-Width="50px" ></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Serie y Lote"  DataField="serieLote" HeaderStyle-Width="100px"  ItemStyle-Width="200px" Display="false" ></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Almacen"  DataField="almCve" HeaderStyle-Width="100px"  ItemStyle-Width="50px" ></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Fch. Entrega"  DataField="docRegPartFecEnt"   HeaderStyle-Width="100px"  ItemStyle-Width="50px" ></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Cantidad"  DataField="docRegPartCant" HeaderStyle-Width="100px"  ItemStyle-Width="50px" ></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="U. M."  DataField="uniMedCve" HeaderStyle-Width="50px"  ItemStyle-Width="30px" ></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Precio Unitario"  DataField="docRegPartPrec"   HeaderStyle-Width="100px"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right" ></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Importe Bruto" DataField="docRegPartImpBrut" HeaderStyle-Width="100px"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Desc. x Volumen" DataField="docRegPartImpDescPart" HeaderStyle-Width="100px"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Descuento(s)" DataField="docRegPartImpDescDoc" HeaderStyle-Width="100px"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="IEPS" DataField="docRegPartImpImpuEsp" HeaderStyle-Width="100px"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Impuesto(s)" DataField="docRegPartImpImpuDoc" HeaderStyle-Width="100px"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Importe Total" DataField="docRegPartImpFact" HeaderStyle-Width="100px"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>  
                                                                                           
                                                    <telerik:GridBoundColumn HeaderText="Lote"  DataField="docRegPartLote" HeaderStyle-Width="50px"  ItemStyle-Width="50px"   ></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Serie"  DataField="docRegPartSerie" HeaderStyle-Width="50px"  ItemStyle-Width="50px"   ></telerik:GridBoundColumn>

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

                    <td style=" width:100%; background-color:transparent; vertical-align:top;">


                            <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none; width:600px; text-align:left;  float:left;  background-color:transparent ;">
                                <tr  style="width:600px;">
                                    <td style=" width:600px; background-color:transparent; vertical-align:top;" >
                                        
                                        <fieldset style="">
                                            <legend>Descuentos e Impuestos</legend>
                                            <div style="width:100%; height:25px; display:table; position:static; background-color:transparent;" > 
                                                <table border="0" style="text-align:left; background-color:transparent;">
                                                    <tr style="height:18px;">


                                                        <td style=" width:300px; background-color:transparent;">
        
                                                            <div style=" overflow:auto; width:310px; height:60px; background-color:transparent ;" id="divDescuentos"  >
                                                                <asp:DataList ID="dt_descuentos" runat="server" DataKeyField="DesCve">
                                                                    <ItemTemplate>
                                                                        <telerik:RadLabel ID="rlblDscDes" Font-Size="Small" runat="server" Width="100px" Text='<%# Eval("DesDes") %>' ></telerik:RadLabel>  
                                                                        <telerik:RadLabel ID="rlblDscTas" Font-Size="Small" runat="server" Width="50px" Text='<%# Eval("DesPorcen") %>' ></telerik:RadLabel>  
                                                                        <telerik:RadLabel ID="rlblDscPor" Font-Size="Small" runat="server" Width="100px" Text="$0.00" ></telerik:RadLabel>  
                                                                    </ItemTemplate>
                                                                </asp:DataList>
                                                            </div> 
                                                        
                                                        </td>

                                                        <td style=" width:300px; background-color:transparent;">
    
                                                          <div style=" overflow:auto; width:310px; height:60px; background-color:transparent ;" id="divImpuestos"  >
                                                            <asp:DataList ID="dt_impuestos" runat="server" DataKeyField="impCve">
                                                                    <ItemTemplate>
                                                                        <telerik:RadLabel ID="rlblImpDes" Font-Size="Small" runat="server" Width="100px" Text='<%# Eval("impAbr") %>' ></telerik:RadLabel>  
                                                                        <telerik:RadLabel ID="rlblImpTas" Font-Size="Small" runat="server" Width="50px" Text='<%# Eval("impTas") %>' ></telerik:RadLabel>  
                                                                        <telerik:RadLabel ID="rlblImpPor" Font-Size="Small" runat="server" Width="100px" Text="$0.00" ></telerik:RadLabel>  
                                                            
                                                                    </ItemTemplate>
                                                                </asp:DataList>
                                                            </div>
                                                        
                                                        </td>

                                                        
                                                    
                                                     </tr>
                                                </table>
                                            </div>
                                        </fieldset>
                                                         
                                    </td>
                                </tr>
                            </table>                       

                            <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none; width:500px ; height:5px; text-align:left;  float:right; background-color:transparent ;">
                                <tr>

                                    <td style=" width:500px; background-color:transparent;">
                                        
                                        <fieldset style="display: block; float:right; ">
                                           <legend>Totales</legend>

                                        <div style=" overflow:auto; width:515px; height:130px; background-color:transparent ;" id="divTotales"  >
                                            <asp:DataList ID="dt_totales" runat="server" DataKeyField="totCve">
                                                    <ItemTemplate>

                                                        <table  border="0" style=" text-align:right; border:none; border-collapse:collapse; width:500px; line-height:1px; text-align:left; background-color:transparent ;">
                                                             <tr style="height:5px; ">

                                                                 <td style="height:5px; width:200px ">
                                                                    <telerik:RadLabel ID="rlblTotalTag" Font-Size="XX-Small" runat="server" Text='<%# Eval("totDes") %>' ></telerik:RadLabel>  
                                                        
                                                                 </td>

                                                                 <td style="height:5px; width:30px ">
                                                                    <telerik:RadLabel ID="rlblTotalSig" Font-Size="XX-Small" runat="server"  Text='<%# Eval("sigCve") %>' ></telerik:RadLabel>  
                                                        
                                                        
                                                                 </td>

                                                                 <td style="height:5px; width:200px; text-align:right; ">
                                                                    <telerik:RadLabel ID="rlblTotalImp" Font-Size="XX-Small" runat="server"  Text="$0.00" ></telerik:RadLabel>  
                                                        
                                                                 </td>
                                                                 
                                                        
                                                             </tr>
                                                         </table>

                                                            
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </div>
                                        </fieldset>
      
                                    </td>
                                </tr>
                                
                            </table>


                    </td>

                </tr>
            </table>

            <asp:HiddenField ID="hdfRawUrl" runat="server" />
      

            <asp:HiddenField ID="hdfBtnAccion" runat="server" />
            <asp:HiddenField ID="hdfBtnAccionP" runat="server" />
            
            <!-- Hidden Fields For ClientFormat-->
            
            <asp:HiddenField ID="hdfPosFol" runat="server" />
            <asp:HiddenField ID="hdfCharFormat" runat="server" />
            <asp:HiddenField ID="hdfLongFol" runat="server" />

            
            <!-- Hidden Fields -->
            <asp:HiddenField ID="hdfDocRegId" runat="server" />


            <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
                <telerik:RadImageButton ID="rBtnGuardar"  runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text ="" OnClick="rBtnGuardar_Click" OnClientClicking="OnClientClic_ConfirmOK" ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text="" OnClick="rBtnCancelar_Click" OnClientClicking="OnClientClic_ConfirmOK" ></telerik:RadImageButton>
            </asp:Panel>    
        

            <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
            </telerik:RadWindowManager>

        </div>



        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

