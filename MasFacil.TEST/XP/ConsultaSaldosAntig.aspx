<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConsultaSaldosAntig.aspx.cs" Inherits="XP_ConsultaSaldosAntig" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link rel="stylesheet" type="text/css" href="../css/cssMPForm.css"/>
    <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
    <script src="../Scripts/JSGeneral.js" type="text/javascript"></script>
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
    <div align="center">

    <table border="0"  style="width:100%; height:100%; border-style:none; background-color:white;">
        <tr align="top" align="center" style="width:100%; ">
            <td>
                <asp:Panel ID="pnlTitlePage" runat="server"  CssClass="PnlMPFormTituloApartado" >
                    <asp:Label ID="lblTitlePage" runat="server" Text="Consulta de Antiguedad de Saldos"></asp:Label> 
                </asp:Panel> 
            </td>
        </tr>
    </table>
    <table border="0"  style="width:100%; height:100%; border-style:none; background-color:white; ">
        <tr align="top" align="left" style="width:100%; ">
            <td>
                <fieldset style="">
                <legend>Proveedor</legend>
                <div style="width:100%; display:table; margin-top:0px; position:static; background-color:transparent;" align="left">
                    <asp:Label ID="LblProvNombre" runat="server"></asp:Label>
                </div> 
                </fieldset>
            </td>
        </tr>
    </table>
    <fieldset>
        <table border="0"  style="width:100%; height:100%; border-style:solid; border-color:transparent; background-color:transparent; ">
            <tr>
                <td style="background-color:transparent; width:240px;" >
                    <asp:Label ID="Label1" runat="server" Text="Fecha Calculo" ></asp:Label>  &nbsp;&nbsp;&nbsp;
                    <telerik:RadDatePicker ID="RdDateFecha" runat="server" Width="110px" OnSelectedDateChanged="RdDateFecha_SelectedDateChanged" AutoPostBack="true"></telerik:RadDatePicker>
                </td>
                 <td  colspan="2" style="background-color:transparent; text-align:right;" >
                      <asp:Label ID="Label12" runat="server" Text="Saldo Actual" ></asp:Label>
                    <b>
                        <asp:Label ID="lblSaldoActual" runat="server" Text=""></asp:Label>
                    </b>
                </td>
            </tr>
            <tr>
                 <td  style="background-color:transparent;"> 
                 </td>
            </tr>
            <tr>
                 <td  style="background-color:transparent;"> 
                 </td>
            </tr>
         <%--PLAZOS POR VENCER--%>
         <tr>
            <td style="background-color:transparent;">
                <asp:Label ID="Label2" runat="server" Text="Plazos por Vencer"></asp:Label> 
            </td>
            <td style=" text-align:right;">
            <b>
                <asp:Label ID="lblPlazoxVencer1" runat="server" ></asp:Label> 
            </b>
            </td>
            <td style=" text-align:right;">
            <b>
                <asp:Label ID="lblPlazoxVencer2" runat="server" ></asp:Label> 
            </b>
            </td>
          </tr>
          <tr>
            <td style="background-color:transparent;">
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label3" runat="server" Text="Saldo de"></asp:Label> &nbsp;&nbsp;&nbsp;
                <telerik:RadNumericTextBox ID="RdTxtSaldo1" runat="server" OnTextChanged="RdTxtSaldo1_TextChanged" AutoPostBack="true" Width="40px" NumberFormat-DecimalDigits="0"  NumberFormat-GroupSizes="4"  MaxLength="4" MinValue="0" MaxValue="9999" ></telerik:RadNumericTextBox>
                <asp:Label ID="Label0" runat="server"  Text="a"></asp:Label>
                <telerik:RadNumericTextBox ID="RdTxtSaldo2" runat="server" OnTextChanged="RdTxtSaldo1_TextChanged" AutoPostBack="true" Width="40px" NumberFormat-DecimalDigits="0"  NumberFormat-GroupSizes="4" MaxLength="4" MinValue="0" MaxValue="9999" ></telerik:RadNumericTextBox>
            </td>
            <td style=" text-align:right;">
                <asp:Label ID="lblSaldo1" runat="server"  ></asp:Label> 
            </td>
            <td style=" text-align:right;">
                <asp:Label ID="lblSaldo2" runat="server" ></asp:Label> 
            </td>
         </tr>
         <tr>
            <td>
                &nbsp;&nbsp;&nbsp;
               <asp:Label ID="Label4" runat="server" Text="Saldo de"></asp:Label> &nbsp;&nbsp;&nbsp;
               <telerik:RadNumericTextBox ID="RdTxtSaldo3" runat="server" OnTextChanged="RdTxtSaldo1_TextChanged" AutoPostBack="true" Width="40px"  NumberFormat-DecimalDigits="0" NumberFormat-GroupSizes="4" MinValue="0" MaxLength="4" MaxValue="9999" ></telerik:RadNumericTextBox>
               <asp:Label ID="Label5" runat="server"  Text="a"></asp:Label>
                <telerik:RadNumericTextBox ID="RdTxtSaldo4" runat="server" OnTextChanged="RdTxtSaldo1_TextChanged" AutoPostBack="true" Width="40px"  NumberFormat-DecimalDigits="0" NumberFormat-GroupSizes="4" MinValue="0" MaxLength="4" MaxValue="9999" ></telerik:RadNumericTextBox>
            </td>
            <td style=" text-align:right;">
                <asp:Label ID="lblSaldo3" runat="server"  ></asp:Label> 
            </td>
            <td style=" text-align:right;">
                <asp:Label ID="lblSaldo4" runat="server"></asp:Label> 
            </td>
         </tr>
         <tr>
            <td>
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label6" runat="server" Text="Saldo de"></asp:Label>  &nbsp;&nbsp;&nbsp;
                <telerik:RadNumericTextBox ID="RdTxtSaldo5" runat="server" OnTextChanged="RdTxtSaldo1_TextChanged" AutoPostBack="true" Width="40px"  NumberFormat-DecimalDigits="0" NumberFormat-GroupSizes="4" MinValue="0" MaxLength="4" MaxValue="9999" ></telerik:RadNumericTextBox>
                <asp:Label ID="Label7" runat="server"  Text="a"></asp:Label>
                <telerik:RadNumericTextBox ID="RdTxtSaldo6" runat="server" OnTextChanged="RdTxtSaldo1_TextChanged" AutoPostBack="true" Width="40px"  NumberFormat-DecimalDigits="0" NumberFormat-GroupSizes="4" MinValue="0" MaxLength="4" MaxValue="9999" ></telerik:RadNumericTextBox>
            </td>
            <td style=" text-align:right;">
                <asp:Label ID="lblSaldo5" runat="server"  ></asp:Label> 
            </td>
            <td style=" text-align:right;">
                <asp:Label ID="lblSaldo6" runat="server" ></asp:Label> 
            </td>
         </tr>
         <tr>
            <td>
                &nbsp;&nbsp;&nbsp;
               <asp:Label ID="Label8" runat="server" Text="Saldo de"></asp:Label> &nbsp;&nbsp;&nbsp;
               <telerik:RadNumericTextBox ID="RdTxtSaldo7" runat="server" OnTextChanged="RdTxtSaldo1_TextChanged" AutoPostBack="true" Width="40px"  NumberFormat-DecimalDigits="0" NumberFormat-GroupSizes="4" MinValue="0" MaxLength="4" MaxValue="9999" ></telerik:RadNumericTextBox>
               <asp:Label ID="Label9" runat="server"  Text="a"></asp:Label>
               <telerik:RadNumericTextBox ID="RdTxtSaldo8" runat="server" OnTextChanged="RdTxtSaldo1_TextChanged" AutoPostBack="true" Width="40px"  NumberFormat-DecimalDigits="0" NumberFormat-GroupSizes="4" MinValue="0" MaxLength="4" MaxValue="9999" ></telerik:RadNumericTextBox>
            </td>
            <td style=" text-align:right;">
                <asp:Label ID="lblSaldo7" runat="server"  ></asp:Label> 
            </td>
            <td style=" text-align:right;">
                <asp:Label ID="lblSaldo8" runat="server" ></asp:Label> 
            </td>
         </tr>
         <tr>
            <td>
                <asp:Label ID="Label10" runat="server" ></asp:Label> 
            </td>
           
            <td style=" text-align:right;">
            <hr style="color:black; background-color:black; width:110px;">
                <b>
                    <asp:Label ID="lblPlazosVencidos1" runat="server"  ></asp:Label> 
                </b>
            </td>
            
            <td style=" text-align:right;">
            <hr style="color:black; background-color:black; width:110px;">
                <b>
                    <asp:Label ID="lblPlazosVencidos2" runat="server" ></asp:Label> 
                </b> 
            </td>
          </tr>

          <tr>
                 <td  style="background-color:transparent;"> 
                 </td>
            </tr>
            <tr>
                 <td  style="background-color:transparent;"> 
                 </td>
            </tr>

          <tr>
            <td>
                <asp:Label ID="Label20" runat="server" Text="Plazos Vencidos"></asp:Label> 
            </td>
            <td style=" text-align:right;">
                <b>
                    <asp:Label ID="Label22" runat="server" ></asp:Label> 
                </b>
            </td>
            <td style=" text-align:right;">
                <b>
                    <asp:Label ID="Label23" runat="server" ></asp:Label> 
                </b> 
            </td>
          </tr>
          <tr>
            <td>
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label11" runat="server" Text="Saldo de"></asp:Label> &nbsp;&nbsp;&nbsp;
                <telerik:RadNumericTextBox ID="RdTxtSaldoVenc1" runat="server" OnTextChanged="RdTxtSaldo1_TextChanged" AutoPostBack="true" Width="40px"  NumberFormat-DecimalDigits="0" NumberFormat-GroupSizes="4" MinValue="0" MaxLength="4" MaxValue="9999" ></telerik:RadNumericTextBox>
                <asp:Label ID="Label16" runat="server"  Text="a"></asp:Label>
                <telerik:RadNumericTextBox ID="RdTxtSaldoVenc2" runat="server" OnTextChanged="RdTxtSaldo1_TextChanged" AutoPostBack="true" Width="40px"  NumberFormat-DecimalDigits="0" NumberFormat-GroupSizes="4" MinValue="0" MaxLength="4" MaxValue="9999" ></telerik:RadNumericTextBox>
            </td>
            <td style=" text-align:right;">
                <asp:Label ID="lblSaldoVenc1" runat="server"  ></asp:Label> 
            </td>
            <td style=" text-align:right;">
                <asp:Label ID="lblSaldoVenc2" runat="server" ></asp:Label> 
            </td>
         </tr>
         <tr>
            <td>
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label13" runat="server" Text="Saldo de"></asp:Label> &nbsp;&nbsp;&nbsp;
                <telerik:RadNumericTextBox ID="RdTxtSaldoVenc3" runat="server" OnTextChanged="RdTxtSaldo1_TextChanged" AutoPostBack="true" Width="40px"  NumberFormat-DecimalDigits="0" NumberFormat-GroupSizes="4" MinValue="0" MaxLength="4" MaxValue="9999" ></telerik:RadNumericTextBox>
                <asp:Label ID="Label14" runat="server"  Text="a"></asp:Label>
                <telerik:RadNumericTextBox ID="RdTxtSaldoVenc4" runat="server" OnTextChanged="RdTxtSaldo1_TextChanged" AutoPostBack="true" Width="40px"  NumberFormat-DecimalDigits="0" NumberFormat-GroupSizes="4" MinValue="0" MaxLength="4" MaxValue="9999" ></telerik:RadNumericTextBox>
            </td>
            <td style=" text-align:right;">
                <asp:Label ID="lblSaldoVenc3" runat="server"  ></asp:Label> 
            </td>
            <td style=" text-align:right;">
                <asp:Label ID="lblSaldoVenc4" runat="server" ></asp:Label> 
            </td>
         </tr>
         <tr>
            <td>
                 &nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label15" runat="server" Text="Saldo de" ></asp:Label> &nbsp;&nbsp;&nbsp;
                <telerik:RadNumericTextBox ID="RdTxtSaldoVenc5" runat="server" OnTextChanged="RdTxtSaldo1_TextChanged" AutoPostBack="true" Width="40px"  NumberFormat-DecimalDigits="0"  NumberFormat-GroupSizes="4" MinValue="0" MaxLength="4" MaxValue="9999" ></telerik:RadNumericTextBox>
                <asp:Label ID="Label19" runat="server"  Text="a"></asp:Label>
                <telerik:RadNumericTextBox ID="RdTxtSaldoVenc6" runat="server" OnTextChanged="RdTxtSaldo1_TextChanged" AutoPostBack="true" Width="40px"  NumberFormat-DecimalDigits="0" NumberFormat-GroupSizes="4" MinValue="0" MaxLength="4" MaxValue="9999" ></telerik:RadNumericTextBox>
            </td>
            <td style=" text-align:right;">
                <asp:Label ID="lblSaldoVenc5" runat="server"  ></asp:Label> 
            </td>
            <td style=" text-align:right;">
                <asp:Label ID="lblSaldoVenc6" runat="server" ></asp:Label> 
            </td>
         </tr>
         <tr>
            <td>
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label17" runat="server" Text="Saldo de"></asp:Label> &nbsp;&nbsp;&nbsp;
                <telerik:RadNumericTextBox ID="RdTxtSaldoVenc7" runat="server" OnTextChanged="RdTxtSaldo1_TextChanged" AutoPostBack="true" Width="40px"  NumberFormat-DecimalDigits="0" NumberFormat-GroupSizes="4" MinValue="0" MaxLength="4" MaxValue="9999" ></telerik:RadNumericTextBox>
                <asp:Label ID="Label18" runat="server"  Text="a"></asp:Label>
                <telerik:RadNumericTextBox ID="RdTxtSaldoVenc8" runat="server" OnTextChanged="RdTxtSaldo1_TextChanged" AutoPostBack="true" Width="40px"  NumberFormat-DecimalDigits="0" NumberFormat-GroupSizes="4" MinValue="0" MaxLength="4" MaxValue="9999" ></telerik:RadNumericTextBox>
            </td>
            <td style=" text-align:right;">
                <asp:Label ID="lblSaldoVenc7" runat="server"  ></asp:Label> 
            </td>
            <td style=" text-align:right;">
                <asp:Label ID="lblSaldoVenc8" runat="server" ></asp:Label> 
            </td>
         </tr>
         
         <tr>
            <td>
                
            </td>
            <td style=" text-align:right;">
                <asp:Label ID="Label21" runat="server"  Text=""></asp:Label>
                <hr style="color:black; background-color:black; width:110px;"> 
                <b>
                <asp:Label ID="lblSaldoTotal1" runat="server" Text=""></asp:Label> 
                </b>
            </td>
            <td style=" text-align:right;">
                
                <hr style="color:black; background-color:black; width:110px;">
                <b>
                <asp:Label ID="lblSaldoTotal2" runat="server" Text=""></asp:Label> 
                </b>
                
            </td>
         </tr>
         <tr>
            <td>    
                <asp:Label ID="Label27" runat="server"  Text="Plazo Total"></asp:Label>
            </td>
            <td style=" text-align:right;">
                <asp:Label ID="Label24" runat="server"  Text=""></asp:Label>
                <hr style="color:black; background-color:black; width:110px;"> 
                <b>
                <asp:Label ID="lblPlazoTotal" runat="server" Text=""></asp:Label> 
                </b>
            </td>
            <td style=" text-align:right;">
                
                <hr style="color:black; background-color:black; width:110px;">
                <b>
                    <asp:Label ID="lblPlazoTotalPorcentaje" runat="server" Text=""></asp:Label> 
                </b>
                
            </td>
         </tr>
        </table>
     </fieldset>

    </div>
    </ContentTemplate>
    </asp:UpdatePanel>  

    <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
    </telerik:RadWindowManager>
    <asp:HiddenField ID="hdfBtnAccion" runat="server" />
    </form>
</body>
</html>
