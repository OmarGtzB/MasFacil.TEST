using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using Telerik.Windows;
using System.Web.UI.HtmlControls;


public partial class CC_ConsultaSaldosAntig : System.Web.UI.Page
{

    #region VARIABLES
    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    ws.Servicio oWS = new ws.Servicio();

    MGMFnGrales.FNGrales FNGrales = new MGMFnGrales.FNGrales();
    MGMControls.FillIn FnCtlsFillIn = new MGMControls.FillIn();
    MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();
    MGMFnGrales.FNDatos FNDatos = new MGMFnGrales.FNDatos();

    private string Pag_sCompania;
    private string Pag_sConexionLog;
    private string Pag_sSessionLog;
    private string Pag_pPerAnio;
    private string Pag_pPerNum;
    private string Pag_CliCve;
    #endregion

    #region EVENTOS
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SM.IsActiveSession(this.Page))
        {
            Valores_InicioPag();
            if (!IsPostBack)
            {
                InicioPagina();
            }
        }
    }

    protected void RdDateFecha_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        EjecutaAccion();
    }

    protected void RdTxtSaldo1_TextChanged(object sender, EventArgs e)
    {

        EjecutaAccion();
    }
    #endregion

    #region METODOS
    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);

        if (Request.QueryString["pPerAnio"] != null && Request.QueryString["pPerAnio"] != "")
        {
            Pag_pPerAnio = Request.QueryString["pPerAnio"];
        }

        if (Request.QueryString["pPerNum"] != null && Request.QueryString["pPerNum"] != "")
        {
            Pag_pPerNum = Request.QueryString["pPerNum"];
        }

        if (Request.QueryString["cliCve"] != null && Request.QueryString["cliCve"] != "")
        {
            Pag_CliCve = Request.QueryString["cliCve"];
        }
    }

    private void InicioPagina()
    {
        LlenarUiCliente(Pag_CliCve);
        LlenaControles();
        LlenaDatosSaldos();


    }

    private void EjecutaAccion()
    {
        string sMSGTip = "";
        string msgValidacion = validaEjecutaAccion(ref sMSGTip);
        if (msgValidacion == "")
        {
            LimpiarControles();
            LlenaDatosSaldos();
        }
        else
        {
            ShowAlert(sMSGTip, msgValidacion);
        }

    }
    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }

    private string validaEjecutaAccion(ref string sMSGTip)
    {

        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";
        if (hdfBtnAccion.Value == "")
        {
            if (ObtenerFecha() == "10101")
            {
                RdDateFecha.BorderWidth = 1;
                RdDateFecha.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {
                RdDateFecha.BorderWidth = 0;
                RdDateFecha.BorderColor = System.Drawing.Color.Transparent;
            }

            if (RdTxtSaldo1.Text.Trim() == "")
            {
                RdTxtSaldo1.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { RdTxtSaldo1.CssClass = "cssTxtEnabled"; }
            if (RdTxtSaldo2.Text.Trim() == "")
            {
                RdTxtSaldo2.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { RdTxtSaldo2.CssClass = "cssTxtEnabled"; }
            if (RdTxtSaldo3.Text.Trim() == "")
            {
                RdTxtSaldo3.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { RdTxtSaldo3.CssClass = "cssTxtEnabled"; }
            if (RdTxtSaldo4.Text.Trim() == "")
            {
                RdTxtSaldo4.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { RdTxtSaldo4.CssClass = "cssTxtEnabled"; }
            if (RdTxtSaldo5.Text.Trim() == "")
            {
                RdTxtSaldo5.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { RdTxtSaldo5.CssClass = "cssTxtEnabled"; }
            if (RdTxtSaldo6.Text.Trim() == "")
            {
                RdTxtSaldo6.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { RdTxtSaldo6.CssClass = "cssTxtEnabled"; }
            if (RdTxtSaldo7.Text.Trim() == "")
            {
                RdTxtSaldo7.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { RdTxtSaldo7.CssClass = "cssTxtEnabled"; }
            if (RdTxtSaldo8.Text.Trim() == "")
            {
                RdTxtSaldo8.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { RdTxtSaldo8.CssClass = "cssTxtEnabled"; }
            if (RdTxtSaldoVenc1.Text.Trim() == "")
            {
                RdTxtSaldoVenc1.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { RdTxtSaldoVenc1.CssClass = "cssTxtEnabled"; }
            if (RdTxtSaldoVenc2.Text.Trim() == "")
            {
                RdTxtSaldoVenc2.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { RdTxtSaldoVenc2.CssClass = "cssTxtEnabled"; }
            if (RdTxtSaldoVenc3.Text.Trim() == "")
            {
                RdTxtSaldoVenc3.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { RdTxtSaldoVenc3.CssClass = "cssTxtEnabled"; }
            if (RdTxtSaldoVenc4.Text.Trim() == "")
            {
                RdTxtSaldoVenc4.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { RdTxtSaldoVenc4.CssClass = "cssTxtEnabled"; }
            if (RdTxtSaldoVenc5.Text.Trim() == "")
            {
                RdTxtSaldoVenc5.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { RdTxtSaldoVenc5.CssClass = "cssTxtEnabled"; }
            if (RdTxtSaldoVenc6.Text.Trim() == "")
            {
                RdTxtSaldoVenc6.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { RdTxtSaldoVenc6.CssClass = "cssTxtEnabled"; }
            if (RdTxtSaldoVenc7.Text.Trim() == "")
            {
                RdTxtSaldoVenc7.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { RdTxtSaldoVenc7.CssClass = "cssTxtEnabled"; }
            if (RdTxtSaldoVenc8.Text.Trim() == "")
            {
                RdTxtSaldoVenc8.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { RdTxtSaldoVenc8.CssClass = "cssTxtEnabled"; }


            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }



        return sResult;
    }

    #endregion

    #region FUNCIONES
    private void LlenarUiCliente(string cliCve)
    {

        try
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_Clientes";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
            ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, 20, ParameterDirection.Input, cliCve);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSRowsIsFill(ds))
            {
                LblClieNombre.Text = ds.Tables[0].Rows[0]["cliCveClie"].ToString() + ds.Tables[0].Rows[0]["cliCveSubClie"].ToString() + " " + ds.Tables[0].Rows[0]["clieNom"].ToString();
            }
            else
            {
                LblClieNombre.Text = "";
            }
        }
        catch (Exception ex)
        {
            LblClieNombre.Text = "";
            string MsgError = ex.Message.Trim();
        }

    }
    
    private void LlenaDatosSaldos()
    {

        string porcentaje = "";
        

        if (Request.QueryString["pPerAnio"] != null && Request.QueryString["pPerAnio"] != "" &&
         Request.QueryString["pPerNum"] != null && Request.QueryString["pPerNum"] != "" &&
         Request.QueryString["cliCve"] != null && Request.QueryString["cliCve"] != "")
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ConsultaSaldosCCAntiguedad";
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@clieCve", DbType.String, 20, ParameterDirection.Input, Pag_CliCve);
            ProcBD.AgregarParametrosProcedimiento("@perAnio", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(Pag_pPerAnio));
            ProcBD.AgregarParametrosProcedimiento("@perNum", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(Pag_pPerNum));
            ProcBD.AgregarParametrosProcedimiento("@fecCalculo", DbType.String, 10, ParameterDirection.Input, ObtenerFecha());

            
            ProcBD.AgregarParametrosProcedimiento("@saldo1Ini_xven", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(RdTxtSaldo1.Text.Trim()));
            ProcBD.AgregarParametrosProcedimiento("@saldo1Fin_xven", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(RdTxtSaldo2.Text.Trim()));

            ProcBD.AgregarParametrosProcedimiento("@saldo2Ini_xven", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(RdTxtSaldo3.Text.Trim()));
            ProcBD.AgregarParametrosProcedimiento("@saldo2Fin_xven", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(RdTxtSaldo4.Text.Trim()));

            ProcBD.AgregarParametrosProcedimiento("@saldo3Ini_xven", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(RdTxtSaldo5.Text.Trim()));
            ProcBD.AgregarParametrosProcedimiento("@saldo3Fin_xven", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(RdTxtSaldo6.Text.Trim()));

            ProcBD.AgregarParametrosProcedimiento("@saldo4Ini_xven", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(RdTxtSaldo7.Text.Trim()));
            ProcBD.AgregarParametrosProcedimiento("@saldo4Fin_xven", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(RdTxtSaldo8.Text.Trim()));


            ///x VENCER///
            ProcBD.AgregarParametrosProcedimiento("@saldo1Ini_ven", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(RdTxtSaldoVenc1.Text.Trim()));
            ProcBD.AgregarParametrosProcedimiento("@saldo1Fin_ven", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(RdTxtSaldoVenc2.Text.Trim()));

            ProcBD.AgregarParametrosProcedimiento("@saldo2Ini_ven", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(RdTxtSaldoVenc3.Text.Trim()));
            ProcBD.AgregarParametrosProcedimiento("@saldo2Fin_ven", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(RdTxtSaldoVenc4.Text.Trim()));

            ProcBD.AgregarParametrosProcedimiento("@saldo3Ini_ven", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(RdTxtSaldoVenc5.Text.Trim()));
            ProcBD.AgregarParametrosProcedimiento("@saldo3Fin_ven", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(RdTxtSaldoVenc6.Text.Trim()));

            ProcBD.AgregarParametrosProcedimiento("@saldo4Ini_ven", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(RdTxtSaldoVenc7.Text.Trim()));
            ProcBD.AgregarParametrosProcedimiento("@saldo4Fin_ven", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(RdTxtSaldoVenc8.Text.Trim()));

            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSRowsIsFill(ds))
            {

                lblSaldoActual.Text = ds.Tables[0].Rows[0]["saldoActual"].ToString();
                if (lblSaldoActual.Text != "")
                {
                    lblSaldoActual.Text = System.String.Format("{0:C}", System.Convert.ToDecimal(lblSaldoActual.Text.ToString()));
                }

                
                //Saldos por Vencer///
                lblSaldo1.Text = ds.Tables[0].Rows[0]["saldo01XVen"].ToString();
                if (lblSaldo1.Text !="")
                {
                    lblSaldo1.Text = System.String.Format("{0:C}", System.Convert.ToDecimal(lblSaldo1.Text.ToString()));
                }

                lblSaldo3.Text = ds.Tables[0].Rows[0]["saldo02XVen"].ToString();
                if (lblSaldo3.Text != "")
                {
                    lblSaldo3.Text = System.String.Format("{0:C}", System.Convert.ToDecimal(lblSaldo3.Text.ToString()));
                }

                lblSaldo5.Text = ds.Tables[0].Rows[0]["saldo03XVen"].ToString();
                if (lblSaldo5.Text != "")
                {
                    lblSaldo5.Text = System.String.Format("{0:C}", System.Convert.ToDecimal(lblSaldo5.Text.ToString()));
                }

                lblSaldo7.Text = ds.Tables[0].Rows[0]["saldo04XVen"].ToString();
                if (lblSaldo7.Text != "")
                {
                    lblSaldo7.Text = System.String.Format("{0:C}", System.Convert.ToDecimal(lblSaldo7.Text.ToString()));
                }

                ///TOTALES PLAZOS X VENCER

                //DECIMAL
                lblPlazosVencidos1.Text = ds.Tables[0].Rows[0]["saldoSuXVen"].ToString();
                if (lblPlazosVencidos1.Text != "")
                {
                    lblPlazosVencidos1.Text = System.String.Format("{0:C}", System.Convert.ToDecimal(lblPlazosVencidos1.Text.ToString()));

                    if (lblPlazosVencidos1.Text == "0.000000")
                    {
                        lblPlazosVencidos1.Text = "0.0";
                    }
                }
                //PORCENTAJE
                if (ds.Tables[0].Rows[0]["saldoSuXVen_Porc"].ToString() != "")
                {
                    porcentaje = ds.Tables[0].Rows[0]["saldoSuXVen_Porc"].ToString();
                    lblPlazosVencidos2.Text = porcentaje + "%";
                    if (porcentaje.ToString() == "0.000000")
                    {
                        lblPlazosVencidos2.Text = "";
                        lblPlazosVencidos2.Text = "0.0%";
                    }

                    porcentaje = "";
                }




                //PORCENTAJES
                if (ds.Tables[0].Rows[0]["saldo01XVen_Porc"].ToString() != "")
                {
                    porcentaje = ds.Tables[0].Rows[0]["saldo01XVen_Porc"].ToString();
                    
                    lblSaldo2.Text = porcentaje + "%";
                    porcentaje = "";
                }

                if (ds.Tables[0].Rows[0]["saldo02XVen_Porc"].ToString() != "")
                {
                    porcentaje = ds.Tables[0].Rows[0]["saldo02XVen_Porc"].ToString();
                    
                    lblSaldo4.Text = porcentaje + "%";
                    porcentaje = "";
                }

                if (ds.Tables[0].Rows[0]["saldo03XVen_Porc"].ToString() != "")
                {
                    porcentaje = ds.Tables[0].Rows[0]["saldo03XVen_Porc"].ToString();
                    
                    lblSaldo6.Text = porcentaje + "%";
                    porcentaje = "";
                }

                if (ds.Tables[0].Rows[0]["saldo04XVen_Porc"].ToString() != "")
                {
                    porcentaje = ds.Tables[0].Rows[0]["saldo04XVen_Porc"].ToString();
                 
                    lblSaldo8.Text = porcentaje + "%";
                    porcentaje = "";
                }

                

                
 ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///Saldos Vencidos///
                ///


                lblSaldoVenc1.Text =  ds.Tables[0].Rows[0]["saldo01Ven"].ToString();
                if (lblSaldoVenc1.Text != "")
                {
                    lblSaldoVenc1.Text = System.String.Format("{0:C}", System.Convert.ToDecimal(lblSaldoVenc1.Text.ToString()));
                }

                lblSaldoVenc3.Text = ds.Tables[0].Rows[0]["saldo02Ven"].ToString();
                if (lblSaldoVenc3.Text != "")
                {
                    lblSaldoVenc3.Text = System.String.Format("{0:C}", System.Convert.ToDecimal(lblSaldoVenc3.Text.ToString()));
                }

                lblSaldoVenc5.Text = ds.Tables[0].Rows[0]["saldo03Ven"].ToString();
                if (lblSaldoVenc5.Text != "")
                {
                    lblSaldoVenc5.Text = System.String.Format("{0:C}", System.Convert.ToDecimal(lblSaldoVenc5.Text.ToString()));
                }

                lblSaldoVenc7.Text = ds.Tables[0].Rows[0]["saldo04Ven"].ToString();
                if (lblSaldoVenc7.Text != "")
                {
                    lblSaldoVenc7.Text = System.String.Format("{0:C}", System.Convert.ToDecimal(lblSaldoVenc7.Text.ToString()));
                }



               


                //PORCENTAJE
                if (ds.Tables[0].Rows[0]["saldo01Ven_Porc"].ToString() != "")
                {
                    porcentaje = ds.Tables[0].Rows[0]["saldo01Ven_Porc"].ToString();
                    
                    lblSaldoVenc2.Text = porcentaje + "%";
                    porcentaje = "";
                }

                if (ds.Tables[0].Rows[0]["saldo02Ven_Porc"].ToString() != "")
                {
                    porcentaje = ds.Tables[0].Rows[0]["saldo02Ven_Porc"].ToString();
                   
                    lblSaldoVenc4.Text = porcentaje + "%";
                    porcentaje = "";
                }

                if (ds.Tables[0].Rows[0]["saldo03Ven_Porc"].ToString() != "")
                {
                    porcentaje = ds.Tables[0].Rows[0]["saldo03Ven_Porc"].ToString();
                    
                    lblSaldoVenc6.Text = porcentaje + "%";
                    porcentaje = "";
                }

                if (ds.Tables[0].Rows[0]["saldo04Ven_Porc"].ToString() != "")
                {
                    porcentaje = ds.Tables[0].Rows[0]["saldo04Ven_Porc"].ToString();
                    
                    lblSaldoVenc8.Text = porcentaje + "%";
                    porcentaje = "";
                }




                //PORCENTAJES VENCIMIENTO//

                //DECIMAL
                lblSaldoTotal1.Text = ds.Tables[0].Rows[0]["saldoSuVen"].ToString();
                if (lblSaldoTotal1.Text != "")
                {
                    lblSaldoTotal1.Text = System.String.Format("{0:C}", System.Convert.ToDecimal(lblSaldoTotal1.Text.ToString()));
                }
                
                //Plazo Total//
                if (ds.Tables[0].Rows[0]["saldoSuVen_Porc"].ToString() != "")
                {
                    porcentaje = ds.Tables[0].Rows[0]["saldoSuVen_Porc"].ToString();
                    lblSaldoTotal2.Text = porcentaje + "%";
                    if (porcentaje.ToString() == "0.000000")
                    {
                        lblSaldoTotal2.Text = "";
                        lblSaldoTotal2.Text = "0.0%";
                    }

                    porcentaje = "";
                }


                ////TOTALES GENERALES///
                //DECIMAL 
                lblPlazoTotal.Text = ds.Tables[0].Rows[0]["saldoSuTotal"].ToString();
                if (lblPlazoTotal.Text != "")
                {
                    lblPlazoTotal.Text = System.String.Format("{0:C}", System.Convert.ToDecimal(lblPlazoTotal.Text.ToString()));
                }


                //PORCENTAJE
                if (ds.Tables[0].Rows[0]["saldoSuTotal_Porc"].ToString() != "")
                {
                    porcentaje = ds.Tables[0].Rows[0]["saldoSuTotal_Porc"].ToString();
                    lblPlazoTotalPorcentaje.Text = porcentaje + "%";
                    if (porcentaje.ToString() == "0.000000")
                    {
                        lblPlazoTotalPorcentaje.Text = "";
                        lblPlazoTotalPorcentaje.Text = "0.0%";
                    }
                    if (porcentaje.ToString() == "100.000000")
                    {
                        lblPlazoTotalPorcentaje.Text = "100%";
                    }
                    porcentaje = "";
                }



            }

        }
    }

    public string ObtenerFecha()
    {
        string Val_TransFec = "";
        DateTime dt = Convert.ToDateTime(RdDateFecha.SelectedDate);

        //Val_TransFec = dt.Year + "/" + dt.Month.ToString().PadLeft(2, '0') + "/" + dt.Day.ToString().PadLeft(2, '0');
        Val_TransFec = dt.Year  + dt.Month.ToString().PadLeft(2, '0')  + dt.Day.ToString().PadLeft(2, '0');
        return Val_TransFec;
    }

    private void LlenaControles() {
        RdDateFecha.SelectedDate = DateTime.Now;

        RdTxtSaldo1.Text = "0";
        RdTxtSaldo2.Text = "30";

        RdTxtSaldo3.Text = "31";
        RdTxtSaldo4.Text = "60";

        RdTxtSaldo5.Text = "61";
        RdTxtSaldo6.Text = "90";

        RdTxtSaldo7.Text = "91";
        RdTxtSaldo8.Text = "9999";

        RdTxtSaldoVenc1.Text = "0";
        RdTxtSaldoVenc2.Text = "30";

        RdTxtSaldoVenc3.Text = "31";
        RdTxtSaldoVenc4.Text = "60";

        RdTxtSaldoVenc5.Text = "61";
        RdTxtSaldoVenc6.Text = "90";

        RdTxtSaldoVenc7.Text = "91";
        RdTxtSaldoVenc8.Text = "9999";
    }

    public bool IsNumeric(object Expression)
    {
        bool isNum;
        double retNum;
        isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
        return isNum;
    }


    public void LimpiarControles()
    {
        lblPlazoTotalPorcentaje.Text = "";
        lblSaldoVenc2.Text = "";
        lblSaldoActual.Text = "";
        lblPlazoTotal.Text = "";

        lblSaldo1.Text = "";
        lblSaldo3.Text = "";
        lblSaldo5.Text = "";
        lblSaldo7.Text = "";

        lblPlazosVencidos1.Text = "";
        lblSaldo2.Text = "";
        lblSaldo4.Text = "";
        lblSaldo6.Text = "";
        lblSaldo8.Text = "";

        lblPlazosVencidos2.Text = "";
        lblSaldoVenc1.Text = "";
        lblSaldoVenc3.Text = "";
        lblSaldoVenc5.Text = "";
        lblSaldoVenc7.Text = "";

        lblSaldoTotal1.Text = "";
        lblSaldoVenc2.Text = "";
        lblSaldoVenc4.Text = "";
        lblSaldoVenc6.Text = "";
        lblSaldoVenc8.Text = "";
        lblSaldoTotal2.Text = "";



    }
    #endregion
    
}