using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Windows;
using System.Web.UI.HtmlControls;
/// <summary>
/// Summary description for csFunctions
/// </summary>
namespace MGMFnGrales
{

    public class ObjAdoNet
    {
        public bool bDSIsFill(DataSet ds)
        {
            bool bResult = false;
            if ((ds != null) && ds.Tables.Count > 0)
            {
                bResult = true;
            }
            return bResult;
        }

        public bool bDSRowsIsFill(DataSet pDS)
        {
            bool valorReturn_bol = false;
            if ((pDS != null) && pDS.Tables.Count > 0 && pDS.Tables[0].Rows.Count > 0)
            {
                valorReturn_bol = true;
            }
            return valorReturn_bol;
        }

    }

    public class FNGrales {
        MGMFnGrales.FnParametros FNParam = new MGMFnGrales.FnParametros();
        MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();
        ws.Servicio oWS = new ws.Servicio();
        public bool bTitleDesc(System.Web.UI.Page i_Page, string sDescripcion, string sCssPnl = "") {
            Label lblTitlePageDes;
            lblTitlePageDes = ((Label)i_Page.Master.FindControl("lblTitlePage"));
            lblTitlePageDes.Text = sDescripcion;

            if (sCssPnl != "") {
                Panel pnlTitlePage;
                pnlTitlePage = ((Panel)i_Page.Master.FindControl("pnlTitlePage"));
                pnlTitlePage.CssClass = sCssPnl;
            }
 
            return true;
        }
        public bool bManejoSubCliente(string Pag_sConexionLog, string sCiaCve)
        {
            bool bResult = false;

            if (Convert.ToInt32(FNParam.sParametroValInt(Pag_sConexionLog, sCiaCve, "CLIECVE", 6)) == 1)
            {
                bResult = true;
            }

            return bResult;
        }
        public bool bTipoCambioMonedaDefault(string Pag_sConexionLog, string sCiaCve, ref string sMonCve, ref decimal tipoCambio) {
            sMonCve = FNParam.sParametroValStr(Pag_sConexionLog, sCiaCve, "TIPOCAMBIO", 1);
            tipoCambio = FNParam.sParametroValDec(Pag_sConexionLog, sCiaCve, "TIPOCAMBIO", 2);
            return true;
        }
        public string sFoliosAutMan(string Pag_sConexionLog, string sCiaCve, string sFolCve, int ManejoFol, string val)
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_FoliosFormatos";
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
            ProcBD.AgregarParametrosProcedimiento("@ManejoFol", DbType.Int32, 0, ParameterDirection.Input, ManejoFol);
            ProcBD.AgregarParametrosProcedimiento("@folCve", DbType.String, 10, ParameterDirection.Input, sFolCve);
            ProcBD.AgregarParametrosProcedimiento("@VarManual", DbType.String, 30, ParameterDirection.Input, val);
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sFolio;
                sFolio = ds.Tables[0].Rows[0]["Folio"].ToString();
                return sFolio;
            }
            return "";
        }

        //Se agrega metodo sFoliosAutManNuevo para corregir el salto de folios  
        public string sFoliosAutManNuevo(string Pag_sConexionLog, string sCiaCve, string sFolCve, int ManejoFol, string val)
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_FoliosFormatos";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input,1);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
            ProcBD.AgregarParametrosProcedimiento("@ManejoFol", DbType.Int32, 0, ParameterDirection.Input, ManejoFol);
            ProcBD.AgregarParametrosProcedimiento("@folCve", DbType.String, 10, ParameterDirection.Input, sFolCve);
            ProcBD.AgregarParametrosProcedimiento("@VarManual", DbType.String, 30, ParameterDirection.Input, val);
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sFolio;
                sFolio = ds.Tables[0].Rows[0]["Folio"].ToString();
                return sFolio;
            }
            return "";
        }
        //
        public string sFoliosAutManv1(string Pag_sConexionLog, string sCiaCve, string sFolCve, int ManejoFol, string val)
        {

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_Folios";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 60);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
            ProcBD.AgregarParametrosProcedimiento("@folCve", DbType.String, 10, ParameterDirection.Input, sFolCve);
            ProcBD.AgregarParametrosProcedimiento("@ManejoFol", DbType.Int32, 0, ParameterDirection.Input, ManejoFol);
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {

                if (ManejoFol == 1)
                {
                    if (ds.Tables[0].Rows[0]["formFolPos"].ToString() == "1")
                    {
                        val = val.PadRight(Convert.ToInt32(ds.Tables[0].Rows[0]["formFolLon"].ToString()), Convert.ToChar(ds.Tables[0].Rows[0]["formFolChar"].ToString()));
                    }
                    if (ds.Tables[0].Rows[0]["formFolPos"].ToString() == "2")
                    {
                        val = val.PadLeft(Convert.ToInt32(ds.Tables[0].Rows[0]["formFolLon"].ToString()), Convert.ToChar(ds.Tables[0].Rows[0]["formFolChar"].ToString()));
                    }
                    val = val.Substring(0, Convert.ToInt32(ds.Tables[0].Rows[0]["formFolLon"].ToString()));


                    if (val.Length > Convert.ToInt32(ds.Tables[0].Rows[0]["formFolLon"].ToString()))
                    {
                        val.Substring(Convert.ToInt32(ds.Tables[0].Rows[0]["formFolLon"].ToString()));
                    }
                    return val;
                }

                if (ManejoFol == 2)
                {
                    string pref = ds.Tables[0].Rows[0]["formFolPrefijo"].ToString().Trim();
                    string Subj = ds.Tables[0].Rows[0]["formFolSufijo"].ToString().Trim();
                    string preSub = pref + Subj;

                    string Folio = "";
                    string fol = "";

                    if (ds.Tables[0].Rows[0]["folManForm"].ToString() == "False")
                    {
                        if (ds.Tables[0].Rows[0]["folTip"].ToString() == "1")
                        {
                            Folio = ds.Tables[0].Rows[0]["folValInt"].ToString();
                            fol = preSub + Folio;

                            // fol.Substring(Convert.ToInt32(ds.Tables[0].Rows[0]["formFolLon"].ToString()));

                            return fol;
                        }
                        if (ds.Tables[0].Rows[0]["folTip"].ToString() == "2")
                        {
                            Folio = ds.Tables[0].Rows[0]["folValStr"].ToString();
                            fol = preSub + Folio;
                            //if (fol.Length > Convert.ToInt32(ds.Tables[0].Rows[0]["formFolLon"].ToString()))
                            //{
                            //    fol.Substring(Convert.ToInt32(ds.Tables[0].Rows[0]["formFolLon"].ToString()));
                            //}
                            return fol;
                        }
                    }

                    //folio con formatos
                    if (ds.Tables[0].Rows[0]["folManForm"].ToString() == "True")
                    {
                        int longFijospreSub = preSub.Length;
                        int longToPad = Convert.ToInt32(ds.Tables[0].Rows[0]["formFolLon"].ToString());
                        longToPad -= longFijospreSub;

                        if (ds.Tables[0].Rows[0]["folTip"].ToString() == "1")
                        {
                            Folio = ds.Tables[0].Rows[0]["folValInt"].ToString();
                            //fol = Folio;
                            if (ds.Tables[0].Rows[0]["formFolPos"].ToString() == "1")
                            {
                                fol = preSub;
                                if (longToPad > 0)
                                {
                                    fol += Folio.PadRight(longToPad, Convert.ToChar(ds.Tables[0].Rows[0]["formFolChar"].ToString()));
                                }

                            }
                            if (ds.Tables[0].Rows[0]["formFolPos"].ToString() == "2")
                            {
                                fol = preSub;
                                if (longToPad > 0)
                                {
                                    fol += Folio.PadLeft(longToPad, Convert.ToChar(ds.Tables[0].Rows[0]["formFolChar"].ToString()));
                                }
                            }
                            if (fol.Length > Convert.ToInt32(ds.Tables[0].Rows[0]["formFolLon"].ToString()))
                            {
                                fol = fol.Substring(0, Convert.ToInt32(ds.Tables[0].Rows[0]["formFolLon"].ToString()));
                            }
                            return fol;
                        }

                        //VALOR STRING
                        if (ds.Tables[0].Rows[0]["folTip"].ToString() == "2")
                        {
                            Folio = ds.Tables[0].Rows[0]["folValStr"].ToString();
                            fol = Folio;

                            if (ds.Tables[0].Rows[0]["formFolPos"].ToString() == "1")
                            {
                                fol = preSub;
                                if (longToPad > 0)
                                {
                                    fol += Folio.PadRight(longToPad, Convert.ToChar(ds.Tables[0].Rows[0]["formFolChar"].ToString()));
                                }

                            }
                            if (ds.Tables[0].Rows[0]["formFolPos"].ToString() == "2")
                            {
                                fol = preSub;
                                if (longToPad > 0)
                                {
                                    fol += Folio.PadLeft(longToPad, Convert.ToChar(ds.Tables[0].Rows[0]["formFolChar"].ToString()));
                                }
                            }
                            if (fol.Length > Convert.ToInt32(ds.Tables[0].Rows[0]["formFolLon"].ToString()))
                            {
                                fol.Substring(Convert.ToInt32(ds.Tables[0].Rows[0]["formFolLon"].ToString()));
                            }
                            return fol;
                        }
                    }
                }

                return val;
            }

            return "";

        }
        public bool bListPrecio(string Pag_sConexionLog, string sCiaCve)
        {
            bool bResult = false;

            if (Convert.ToInt32(FNParam.sParametroValInt(Pag_sConexionLog, sCiaCve, "LPRCVE", 1)) == 2)
            {
                bResult = true;
            }

            return bResult;
        }


        public string sCtaContCveFotmat(string Pag_sConexionLog, string sCiaCve,  string sCtaContCve)
        {
            string Result = sCtaContCve;

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_CuentasContables";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
            ProcBD.AgregarParametrosProcedimiento("@ctaContCve", DbType.String, 20, ParameterDirection.Input, sCtaContCve);
    
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            if (FnValAdoNet.bDSRowsIsFill(ds))
            {
                Result = ds.Tables[0].Rows[0]["ctaContCveFotmat"].ToString();
            }
           
            return Result;
        }


        
    }

    public class FNDatos {
        ws.Servicio oWS = new ws.Servicio();

        MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();

 //MASH
        public DataSet dsCatalogos_2(string Pag_sConexionLog, string sEntidad, string sCampoCve, string sCampoDes, string sCiaCve)
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_MGMCatalogos_2";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
            ProcBD.AgregarParametrosProcedimiento("@catalogo", DbType.String, 50, ParameterDirection.Input, sEntidad);
            ProcBD.AgregarParametrosProcedimiento("@campoCve", DbType.String, 50, ParameterDirection.Input, sCampoCve);
            ProcBD.AgregarParametrosProcedimiento("@campoDes", DbType.String, 100, ParameterDirection.Input, sCampoDes);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            return ds;
        }


        public DataSet dsCatalogos(string Pag_sConexionLog, string sEntidad, string sCampoCve, string sCampoDes, string sCiaCve)
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_MGMCatalogos";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
            ProcBD.AgregarParametrosProcedimiento("@catalogo", DbType.String, 50, ParameterDirection.Input, sEntidad);
            ProcBD.AgregarParametrosProcedimiento("@campoCve", DbType.String, 50, ParameterDirection.Input, sCampoCve);
            ProcBD.AgregarParametrosProcedimiento("@campoDes", DbType.String, 100, ParameterDirection.Input, sCampoDes);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
           ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            return ds;
        }
        public DataSet dsListasPrecargadas(string Pag_sConexionLog, string sCiaCve, string sListPreCve, string sListPreValTip, int iSec = -1)
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_MGMListasPrecargadas";
            ProcBD.AgregarParametrosProcedimiento("@listPreCve", DbType.String, 10, ParameterDirection.Input, sListPreCve);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
            ProcBD.AgregarParametrosProcedimiento("@listPreValTip", DbType.String, 5, ParameterDirection.Input, sListPreValTip);
            if (iSec != -1) {
                ProcBD.AgregarParametrosProcedimiento("@listPreSec", DbType.Int64, 0, ParameterDirection.Input, iSec);
            }
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            return ds;
        }
        public DataSet dsFormatosImpresion(string Pag_sConexionLog, string sCiaCve, int iFormImpTip)
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_FormatosImpresion";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
            ProcBD.AgregarParametrosProcedimiento("@formImpTip", DbType.String, 5, ParameterDirection.Input, iFormImpTip);
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            return ds;
        }
        public Boolean dsMAMensajes(string Pag_sConexionLog, string maMSGCve, ref string sMSGTip, ref string sMSGDes)
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_MAMensajes";
            ProcBD.AgregarParametrosProcedimiento("@maMSGCve", DbType.String, 50, ParameterDirection.Input, maMSGCve);
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                sMSGTip = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sMSGDes = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
            }
            return false;

        }
        public DataSet dsOtrosDatos(string Pag_sConexionLog, string sCiaCve,string sOtroDatTip)
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_OtrosDatos";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 0, ParameterDirection.Input, 51);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
            ProcBD.AgregarParametrosProcedimiento("@otroDatTip", DbType.String, 10, ParameterDirection.Input, sOtroDatTip);
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            return ds;
        }
        public DataSet dsConceptoCostoConfig(string Pag_sConexionLog, string sCiaCve, Int64 iCptoId)
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ConceptoCostoConfig";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
            ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 20, ParameterDirection.Input, iCptoId);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            return ds; 
        }


        public DataSet dsTransaccionesAux(string Pag_sConexionLog, string sCiaCve, DataRow dr , int iElimianaDatosTrans = 1 )
        {
            DataSet dsConfig = new DataSet();
            Int64 iCptoId = 0;
            if (dr["cptoId"].ToString() != "")
            {
                iCptoId = Convert.ToInt64(dr["cptoId"]);
            }

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_TransaccionesDetalle_Aux";

            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 0);

            if (dr["transDetId"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToString(dr["transDetId"]));
            }
            if (dr["transId"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(dr["transId"]));
            }
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr["ciaCve"]));

            if (dr["cptoId"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToString(dr["cptoId"]));
            }
            if (dr["cptoId"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetSec", DbType.Int64, 0, ParameterDirection.Input, Convert.ToString(dr["transDetSec"]));
            }
            ProcBD.AgregarParametrosProcedimiento("@monCve", DbType.String, 2, ParameterDirection.Input, Convert.ToString(dr["monCve"]));

            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_01", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr["transDetStr10_01"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_02", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr["transDetStr10_02"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_03", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr["transDetStr10_03"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_04", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr["transDetStr10_04"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_05", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr["transDetStr10_05"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_06", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr["transDetStr10_06"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_07", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr["transDetStr10_07"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_08", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr["transDetStr10_08"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_09", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr["transDetStr10_09"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_10", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr["transDetStr10_10"]));

            ProcBD.AgregarParametrosProcedimiento("@transDetStr20_01", DbType.String, 20, ParameterDirection.Input, Convert.ToString(dr["transDetStr20_01"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr20_02", DbType.String, 20, ParameterDirection.Input, Convert.ToString(dr["transDetStr20_02"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr20_03", DbType.String, 20, ParameterDirection.Input, Convert.ToString(dr["transDetStr20_03"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr20_04", DbType.String, 20, ParameterDirection.Input, Convert.ToString(dr["transDetStr20_04"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr20_05", DbType.String, 20, ParameterDirection.Input, Convert.ToString(dr["transDetStr20_05"]));

            ProcBD.AgregarParametrosProcedimiento("@transDetStr40_01", DbType.String, 40, ParameterDirection.Input, Convert.ToString(dr["transDetStr40_01"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr40_02", DbType.String, 40, ParameterDirection.Input, Convert.ToString(dr["transDetStr40_02"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr40_03", DbType.String, 40, ParameterDirection.Input, Convert.ToString(dr["transDetStr40_03"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr40_04", DbType.String, 40, ParameterDirection.Input, Convert.ToString(dr["transDetStr40_04"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr40_05", DbType.String, 40, ParameterDirection.Input, Convert.ToString(dr["transDetStr40_05"]));

            if (dr["transDetImp_01"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_01", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr["transDetImp_01"]));
            }
            if (dr["transDetImp_02"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_02", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr["transDetImp_02"]));
            }
            if (dr["transDetImp_03"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_03", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr["transDetImp_03"]));
            }
            if (dr["transDetImp_04"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_04", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr["transDetImp_04"]));
            }
            if (dr["transDetImp_05"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_05", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr["transDetImp_05"]));
            }
            if (dr["transDetImp_06"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_06", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr["transDetImp_06"]));
            }
            if (dr["transDetImp_07"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_07", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr["transDetImp_07"]));
            }
            if (dr["transDetImp_08"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_08", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr["transDetImp_08"]));
            }
            if (dr["transDetImp_09"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_09", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr["transDetImp_09"]));
            }
            if (dr["transDetImp_10"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_10", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr["transDetImp_10"]));
            }



            if (dr["transDetFec_01"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetFec_01", DbType.String, 100, ParameterDirection.Input, Convert.ToDateTime(dr["transDetFec_01"]).ToString("yyyy-MM-dd"));
            }
            if (dr["transDetFec_02"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetFec_02", DbType.String, 100, ParameterDirection.Input, Convert.ToDateTime(dr["transDetFec_02"]).ToString("yyyy-MM-dd"));
            }
            if (dr["transDetFec_03"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetFec_03", DbType.String, 100, ParameterDirection.Input, Convert.ToDateTime(dr["transDetFec_03"]).ToString("yyyy-MM-dd"));
            }
            if (dr["transDetFec_04"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetFec_04", DbType.String, 100, ParameterDirection.Input, Convert.ToDateTime(dr["transDetFec_04"]).ToString("yyyy-MM-dd"));
            }
            if (dr["transDetFec_05"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetFec_05", DbType.String, 100, ParameterDirection.Input, Convert.ToDateTime(dr["transDetFec_05"]).ToString("yyyy-MM-dd"));
            }


            if (dr["transDetFact_01"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetFact_01", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr["transDetFact_01"]));
            }
            if (dr["transDetFact_02"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetFact_02", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr["transDetFact_02"]));
            }
            if (dr["transDetFact_03"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetFact_03", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr["transDetFact_03"]));
            }
            if (dr["transDetFact_04"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetFact_04", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr["transDetFact_04"]));
            }
            if (dr["transDetFact_05"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetFact_05", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr["transDetFact_05"]));
            }

            if (dr["transDetSit"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetSit", DbType.String, 3, ParameterDirection.Input, Convert.ToString(dr["transDetSit"]));
            }

            ProcBD.AgregarParametrosProcedimiento("@transDetClaveAux", DbType.String, 50, ParameterDirection.Input, "");
            ProcBD.AgregarParametrosProcedimiento("@EliminaDatos", DbType.Int64, 0, ParameterDirection.Input, iElimianaDatosTrans);

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
             

            return ds;
        }
        public DataSet dsTransaccionesAux(string Pag_sConexionLog, string sCiaCve, DataRow[] dr, int iElimianaDatosTrans = 1)
        {
            DataSet dsConfig = new DataSet();
            Int64 iCptoId = 0;
            if (dr[0]["cptoId"].ToString() != "")
            {
                iCptoId = Convert.ToInt64(dr[0]["cptoId"]);
            }



            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_TransaccionesDetalle_Aux";

            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 0);

            if (dr[0]["transDetId"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToString(dr[0]["transDetId"]));
            }
            if (dr[0]["transId"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(dr[0]["transId"]));
            }
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr[0]["ciaCve"]));

            if (dr[0]["cptoId"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToString(dr[0]["cptoId"]));
            }
            if (dr[0]["cptoId"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetSec", DbType.Int64, 0, ParameterDirection.Input, Convert.ToString(dr[0]["transDetSec"]));
            }
            ProcBD.AgregarParametrosProcedimiento("@monCve", DbType.String, 2, ParameterDirection.Input, Convert.ToString(dr[0]["monCve"]));

            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_01", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr10_01"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_02", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr10_02"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_03", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr10_03"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_04", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr10_04"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_05", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr10_05"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_06", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr10_06"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_07", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr10_07"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_08", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr10_08"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_09", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr10_09"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_10", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr10_10"]));

            ProcBD.AgregarParametrosProcedimiento("@transDetStr20_01", DbType.String, 20, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr20_01"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr20_02", DbType.String, 20, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr20_02"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr20_03", DbType.String, 20, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr20_03"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr20_04", DbType.String, 20, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr20_04"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr20_05", DbType.String, 20, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr20_05"]));

            ProcBD.AgregarParametrosProcedimiento("@transDetStr40_01", DbType.String, 40, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr40_01"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr40_02", DbType.String, 40, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr40_02"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr40_03", DbType.String, 40, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr40_03"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr40_04", DbType.String, 40, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr40_04"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr40_05", DbType.String, 40, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr40_05"]));


            if (dr[0]["transDetImp_01"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_01", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr[0]["transDetImp_01"]));
            }
            if (dr[0]["transDetImp_02"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_02", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr[0]["transDetImp_02"]));
            }
            if (dr[0]["transDetImp_03"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_03", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr[0]["transDetImp_03"]));
            }
            if (dr[0]["transDetImp_04"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_04", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr[0]["transDetImp_04"]));
            }
            if (dr[0]["transDetImp_05"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_05", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr[0]["transDetImp_05"]));
            }
            if (dr[0]["transDetImp_06"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_06", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr[0]["transDetImp_06"]));
            }
            if (dr[0]["transDetImp_07"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_07", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr[0]["transDetImp_07"]));
            }
            if (dr[0]["transDetImp_08"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_08", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr[0]["transDetImp_08"]));
            }
            if (dr[0]["transDetImp_09"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_09", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr[0]["transDetImp_09"]));
            }
            if (dr[0]["transDetImp_10"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_10", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr[0]["transDetImp_10"]));
            }


            if (dr[0]["transDetFec_01"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetFec_01", DbType.String, 100, ParameterDirection.Input, Convert.ToDateTime(dr[0]["transDetFec_01"]).ToString("yyyy-MM-dd"));
            }
            if (dr[0]["transDetFec_02"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetFec_02", DbType.String, 100, ParameterDirection.Input, Convert.ToDateTime(dr[0]["transDetFec_02"]).ToString("yyyy-MM-dd"));
            }
            if (dr[0]["transDetFec_03"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetFec_03", DbType.String, 100, ParameterDirection.Input, Convert.ToDateTime(dr[0]["transDetFec_03"]).ToString("yyyy-MM-dd"));
            }
            if (dr[0]["transDetFec_04"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetFec_04", DbType.String, 100, ParameterDirection.Input, Convert.ToDateTime(dr[0]["transDetFec_04"]).ToString("yyyy-MM-dd"));
            }
            if (dr[0]["transDetFec_05"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetFec_05", DbType.String, 100, ParameterDirection.Input, Convert.ToDateTime(dr[0]["transDetFec_05"]).ToString("yyyy-MM-dd"));
            }


            if (dr[0]["transDetFact_01"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetFact_01", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr[0]["transDetFact_01"]));
            }
            if (dr[0]["transDetFact_02"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetFact_02", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr[0]["transDetFact_02"]));
            }
            if (dr[0]["transDetFact_03"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetFact_03", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr[0]["transDetFact_03"]));
            }
            if (dr[0]["transDetFact_04"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetFact_04", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr[0]["transDetFact_04"]));
            }
            if (dr[0]["transDetFact_05"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetFact_05", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr[0]["transDetFact_05"]));
            }

            if (dr[0]["transDetSit"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetSit", DbType.String, 3, ParameterDirection.Input, Convert.ToString(dr[0]["transDetSit"]));
            }

            ProcBD.AgregarParametrosProcedimiento("@transDetClaveAux", DbType.String, 50, ParameterDirection.Input, "");
            ProcBD.AgregarParametrosProcedimiento("@EliminaDatos", DbType.Int64,0 , ParameterDirection.Input, iElimianaDatosTrans);

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            return ds;
        }




        public Boolean ActualizaCosto(string Pag_sConexionLog, string sCiaCve, ref DataRow dr)
        {
            DataSet dsConfig = new DataSet();
            Int64 iCptoId = 0;
            if (dr["cptoId"].ToString() != "")
            {
                iCptoId = Convert.ToInt64(dr["cptoId"]);
            }

            dsConfig = dsConceptoCostoConfig(Pag_sConexionLog, sCiaCve, iCptoId);
            if (FnValAdoNet.bDSRowsIsFill(dsConfig))
            {
                if (Convert.ToInt64(dsConfig.Tables[0].Rows[0]["RecuperaOtroCosto"]) == 1)
                {
                    DataSet ds = new DataSet();
                    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                    ProcBD.NombreProcedimiento = "sp_TransaccionesDetalle_Aux";

                    ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);

                    if (dr["transDetId"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToString(dr["transDetId"]));
                    }
                    if (dr["transId"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(dr["transId"]));
                    }
                    ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr["ciaCve"]));

                    if (dr["cptoId"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToString(dr["cptoId"]));
                    }
                    if (dr["cptoId"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetSec", DbType.Int64, 0, ParameterDirection.Input, Convert.ToString(dr["transDetSec"]));
                    }
                    ProcBD.AgregarParametrosProcedimiento("@monCve", DbType.String, 2, ParameterDirection.Input, Convert.ToString(dr["monCve"]));

                    ProcBD.AgregarParametrosProcedimiento("@transDetStr10_01", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr["transDetStr10_01"]));
                    ProcBD.AgregarParametrosProcedimiento("@transDetStr10_02", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr["transDetStr10_02"]));
                    ProcBD.AgregarParametrosProcedimiento("@transDetStr10_03", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr["transDetStr10_03"]));
                    ProcBD.AgregarParametrosProcedimiento("@transDetStr10_04", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr["transDetStr10_04"]));
                    ProcBD.AgregarParametrosProcedimiento("@transDetStr10_05", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr["transDetStr10_05"]));
                    ProcBD.AgregarParametrosProcedimiento("@transDetStr10_06", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr["transDetStr10_06"]));
                    ProcBD.AgregarParametrosProcedimiento("@transDetStr10_07", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr["transDetStr10_07"]));
                    ProcBD.AgregarParametrosProcedimiento("@transDetStr10_08", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr["transDetStr10_08"]));
                    ProcBD.AgregarParametrosProcedimiento("@transDetStr10_09", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr["transDetStr10_09"]));
                    ProcBD.AgregarParametrosProcedimiento("@transDetStr10_10", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr["transDetStr10_10"]));

                    ProcBD.AgregarParametrosProcedimiento("@transDetStr20_01", DbType.String, 20, ParameterDirection.Input, Convert.ToString(dr["transDetStr20_01"]));
                    ProcBD.AgregarParametrosProcedimiento("@transDetStr20_02", DbType.String, 20, ParameterDirection.Input, Convert.ToString(dr["transDetStr20_02"]));
                    ProcBD.AgregarParametrosProcedimiento("@transDetStr20_03", DbType.String, 20, ParameterDirection.Input, Convert.ToString(dr["transDetStr20_03"]));
                    ProcBD.AgregarParametrosProcedimiento("@transDetStr20_04", DbType.String, 20, ParameterDirection.Input, Convert.ToString(dr["transDetStr20_04"]));
                    ProcBD.AgregarParametrosProcedimiento("@transDetStr20_05", DbType.String, 20, ParameterDirection.Input, Convert.ToString(dr["transDetStr20_05"]));

                    ProcBD.AgregarParametrosProcedimiento("@transDetStr40_01", DbType.String, 40, ParameterDirection.Input, Convert.ToString(dr["transDetStr40_01"]));
                    ProcBD.AgregarParametrosProcedimiento("@transDetStr40_02", DbType.String, 40, ParameterDirection.Input, Convert.ToString(dr["transDetStr40_02"]));
                    ProcBD.AgregarParametrosProcedimiento("@transDetStr40_03", DbType.String, 40, ParameterDirection.Input, Convert.ToString(dr["transDetStr40_03"]));
                    ProcBD.AgregarParametrosProcedimiento("@transDetStr40_04", DbType.String, 40, ParameterDirection.Input, Convert.ToString(dr["transDetStr40_04"]));
                    ProcBD.AgregarParametrosProcedimiento("@transDetStr40_05", DbType.String, 40, ParameterDirection.Input, Convert.ToString(dr["transDetStr40_05"]));

                    if (dr["transDetImp_01"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetImp_01", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr["transDetImp_01"]));
                    }
                    if (dr["transDetImp_02"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetImp_02", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr["transDetImp_02"]));
                    }
                    if (dr["transDetImp_03"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetImp_03", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr["transDetImp_03"]));
                    }
                    if (dr["transDetImp_04"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetImp_04", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr["transDetImp_04"]));
                    }
                    if (dr["transDetImp_05"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetImp_05", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr["transDetImp_05"]));
                    }
                    if (dr["transDetImp_06"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetImp_06", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr["transDetImp_06"]));
                    }
                    if (dr["transDetImp_07"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetImp_07", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr["transDetImp_07"]));
                    }
                    if (dr["transDetImp_08"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetImp_08", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr["transDetImp_08"]));
                    }
                    if (dr["transDetImp_09"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetImp_09", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr["transDetImp_09"]));
                    }
                    if (dr["transDetImp_10"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetImp_10", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr["transDetImp_10"]));
                    }



                    if (dr["transDetFec_01"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetFec_01", DbType.String, 100, ParameterDirection.Input, Convert.ToDateTime(dr["transDetFec_01"]).ToString("yyyy-MM-dd"));
                    }
                    if (dr["transDetFec_02"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetFec_02", DbType.String, 100, ParameterDirection.Input, Convert.ToDateTime(dr["transDetFec_02"]).ToString("yyyy-MM-dd"));
                    }
                    if (dr["transDetFec_03"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetFec_03", DbType.String, 100, ParameterDirection.Input, Convert.ToDateTime(dr["transDetFec_03"]).ToString("yyyy-MM-dd"));
                    }
                    if (dr["transDetFec_04"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetFec_04", DbType.String, 100, ParameterDirection.Input, Convert.ToDateTime(dr["transDetFec_04"]).ToString("yyyy-MM-dd"));
                    }
                    if (dr["transDetFec_05"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetFec_05", DbType.String, 100, ParameterDirection.Input, Convert.ToDateTime(dr["transDetFec_05"]).ToString("yyyy-MM-dd"));
                    }


                    if (dr["transDetFact_01"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetFact_01", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr["transDetFact_01"]));
                    }
                    if (dr["transDetFact_02"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetFact_02", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr["transDetFact_02"]));
                    }
                    if (dr["transDetFact_03"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetFact_03", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr["transDetFact_03"]));
                    }
                    if (dr["transDetFact_04"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetFact_04", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr["transDetFact_04"]));
                    }
                    if (dr["transDetFact_05"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetFact_05", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr["transDetFact_05"]));
                    }

                    if (dr["transDetSit"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetSit", DbType.String, 3, ParameterDirection.Input, Convert.ToString(dr["transDetSit"]));
                    }

                    ProcBD.AgregarParametrosProcedimiento("@transDetClaveAux", DbType.String, 50, ParameterDirection.Input, "");

                    ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

                    //dr["transDetFact_03"] = ds.Tables[0].Rows[0]["transDetFact_03"].ToString();
                    dr["transDetImp_01"] = ds.Tables[0].Rows[0]["transDetImp_01"].ToString();
                    dr["transDetImp_02"] = ds.Tables[0].Rows[0]["transDetImp_02"].ToString();
                    dr["transDetImp_03"] = ds.Tables[0].Rows[0]["transDetImp_03"].ToString();
                    dr["transDetImp_04"] = ds.Tables[0].Rows[0]["transDetImp_04"].ToString();
                    dr["transDetImp_05"] = ds.Tables[0].Rows[0]["transDetImp_05"].ToString();
                    dr["transDetImp_06"] = ds.Tables[0].Rows[0]["transDetImp_06"].ToString();
                    dr["transDetImp_07"] = ds.Tables[0].Rows[0]["transDetImp_07"].ToString();
                    dr["transDetImp_08"] = ds.Tables[0].Rows[0]["transDetImp_08"].ToString();
                    dr["transDetImp_09"] = ds.Tables[0].Rows[0]["transDetImp_09"].ToString();
                    dr["transDetImp_10"] = ds.Tables[0].Rows[0]["transDetImp_10"].ToString();

                    dr["transDetFact_01"] = ds.Tables[0].Rows[0]["transDetFact_01"].ToString();
                    dr["transDetFact_02"] = ds.Tables[0].Rows[0]["transDetFact_02"].ToString();
                    dr["transDetFact_03"] = ds.Tables[0].Rows[0]["transDetFact_03"].ToString();
                    dr["transDetFact_04"] = ds.Tables[0].Rows[0]["transDetFact_04"].ToString();
                    dr["transDetFact_05"] = ds.Tables[0].Rows[0]["transDetFact_05"].ToString();
                }
            }
            return true;
        }
        public Boolean ActualizaCosto(string Pag_sConexionLog, string sCiaCve, ref DataRow[] dr)
        {
            DataSet dsConfig = new DataSet();
            Int64 iCptoId = 0;
            if (dr[0]["cptoId"].ToString() != "")
            {
                iCptoId = Convert.ToInt64(dr[0]["cptoId"]);
            }

            dsConfig = dsConceptoCostoConfig(Pag_sConexionLog, sCiaCve, iCptoId);
            if (FnValAdoNet.bDSRowsIsFill(dsConfig))
            {
                if (Convert.ToInt64(dsConfig.Tables[0].Rows[0]["RecuperaOtroCosto"]) == 1) {

                    DataSet ds = new DataSet();
                    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                    ProcBD.NombreProcedimiento = "sp_TransaccionesDetalle_Aux";

                    ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);

                    if (dr[0]["transDetId"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToString(dr[0]["transDetId"]));
                    }
                    if (dr[0]["transId"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(dr[0]["transId"]));
                    }
                    ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr[0]["ciaCve"]));

                    if (dr[0]["cptoId"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToString(dr[0]["cptoId"]));
                    }
                    if (dr[0]["cptoId"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetSec", DbType.Int64, 0, ParameterDirection.Input, Convert.ToString(dr[0]["transDetSec"]));
                    }
                    ProcBD.AgregarParametrosProcedimiento("@monCve", DbType.String, 2, ParameterDirection.Input, Convert.ToString(dr[0]["monCve"]));

                    ProcBD.AgregarParametrosProcedimiento("@transDetStr10_01", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr10_01"]));
                    ProcBD.AgregarParametrosProcedimiento("@transDetStr10_02", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr10_02"]));
                    ProcBD.AgregarParametrosProcedimiento("@transDetStr10_03", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr10_03"]));
                    ProcBD.AgregarParametrosProcedimiento("@transDetStr10_04", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr10_04"]));
                    ProcBD.AgregarParametrosProcedimiento("@transDetStr10_05", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr10_05"]));
                    ProcBD.AgregarParametrosProcedimiento("@transDetStr10_06", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr10_06"]));
                    ProcBD.AgregarParametrosProcedimiento("@transDetStr10_07", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr10_07"]));
                    ProcBD.AgregarParametrosProcedimiento("@transDetStr10_08", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr10_08"]));
                    ProcBD.AgregarParametrosProcedimiento("@transDetStr10_09", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr10_09"]));
                    ProcBD.AgregarParametrosProcedimiento("@transDetStr10_10", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr10_10"]));

                    ProcBD.AgregarParametrosProcedimiento("@transDetStr20_01", DbType.String, 20, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr20_01"]));
                    ProcBD.AgregarParametrosProcedimiento("@transDetStr20_02", DbType.String, 20, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr20_02"]));
                    ProcBD.AgregarParametrosProcedimiento("@transDetStr20_03", DbType.String, 20, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr20_03"]));
                    ProcBD.AgregarParametrosProcedimiento("@transDetStr20_04", DbType.String, 20, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr20_04"]));
                    ProcBD.AgregarParametrosProcedimiento("@transDetStr20_05", DbType.String, 20, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr20_05"]));

                    ProcBD.AgregarParametrosProcedimiento("@transDetStr40_01", DbType.String, 40, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr40_01"]));
                    ProcBD.AgregarParametrosProcedimiento("@transDetStr40_02", DbType.String, 40, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr40_02"]));
                    ProcBD.AgregarParametrosProcedimiento("@transDetStr40_03", DbType.String, 40, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr40_03"]));
                    ProcBD.AgregarParametrosProcedimiento("@transDetStr40_04", DbType.String, 40, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr40_04"]));
                    ProcBD.AgregarParametrosProcedimiento("@transDetStr40_05", DbType.String, 40, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr40_05"]));


                    if (dr[0]["transDetImp_01"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetImp_01", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr[0]["transDetImp_01"]));
                    }
                    if (dr[0]["transDetImp_02"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetImp_02", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr[0]["transDetImp_02"]));
                    }
                    if (dr[0]["transDetImp_03"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetImp_03", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr[0]["transDetImp_03"]));
                    }
                    if (dr[0]["transDetImp_04"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetImp_04", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr[0]["transDetImp_04"]));
                    }
                    if (dr[0]["transDetImp_05"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetImp_05", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr[0]["transDetImp_05"]));
                    }
                    if (dr[0]["transDetImp_06"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetImp_06", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr[0]["transDetImp_06"]));
                    }
                    if (dr[0]["transDetImp_07"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetImp_07", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr[0]["transDetImp_07"]));
                    }
                    if (dr[0]["transDetImp_08"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetImp_08", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr[0]["transDetImp_08"]));
                    }
                    if (dr[0]["transDetImp_09"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetImp_09", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr[0]["transDetImp_09"]));
                    }
                    if (dr[0]["transDetImp_10"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetImp_10", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr[0]["transDetImp_10"]));
                    }


                    if (dr[0]["transDetFec_01"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetFec_01", DbType.String, 100, ParameterDirection.Input, Convert.ToDateTime(dr[0]["transDetFec_01"]).ToString("yyyy-MM-dd"));
                    }
                    if (dr[0]["transDetFec_02"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetFec_02", DbType.String, 100, ParameterDirection.Input, Convert.ToDateTime(dr[0]["transDetFec_02"]).ToString("yyyy-MM-dd"));
                    }
                    if (dr[0]["transDetFec_03"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetFec_03", DbType.String, 100, ParameterDirection.Input, Convert.ToDateTime(dr[0]["transDetFec_03"]).ToString("yyyy-MM-dd"));
                    }
                    if (dr[0]["transDetFec_04"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetFec_04", DbType.String, 100, ParameterDirection.Input, Convert.ToDateTime(dr[0]["transDetFec_04"]).ToString("yyyy-MM-dd"));
                    }
                    if (dr[0]["transDetFec_05"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetFec_05", DbType.String, 100, ParameterDirection.Input, Convert.ToDateTime(dr[0]["transDetFec_05"]).ToString("yyyy-MM-dd"));
                    }


                    if (dr[0]["transDetFact_01"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetFact_01", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr[0]["transDetFact_01"]));
                    }
                    if (dr[0]["transDetFact_02"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetFact_02", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr[0]["transDetFact_02"]));
                    }
                    if (dr[0]["transDetFact_03"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetFact_03", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr[0]["transDetFact_03"]));
                    }
                    if (dr[0]["transDetFact_04"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetFact_04", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr[0]["transDetFact_04"]));
                    }
                    if (dr[0]["transDetFact_05"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetFact_05", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr[0]["transDetFact_05"]));
                    }

                    if (dr[0]["transDetSit"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@transDetSit", DbType.String, 3, ParameterDirection.Input, Convert.ToString(dr[0]["transDetSit"]));
                    }

                    ProcBD.AgregarParametrosProcedimiento("@transDetClaveAux", DbType.String, 50, ParameterDirection.Input, "");

                    ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

                    //dr[0]["transDetFact_03"] = ds.Tables[0].Rows[0]["transDetFact_03"].ToString();
                    dr[0]["transDetImp_01"] = ds.Tables[0].Rows[0]["transDetImp_01"].ToString();
                    dr[0]["transDetImp_02"] = ds.Tables[0].Rows[0]["transDetImp_02"].ToString();
                    dr[0]["transDetImp_03"] = ds.Tables[0].Rows[0]["transDetImp_03"].ToString();
                    dr[0]["transDetImp_04"] = ds.Tables[0].Rows[0]["transDetImp_04"].ToString();
                    dr[0]["transDetImp_05"] = ds.Tables[0].Rows[0]["transDetImp_05"].ToString();
                    dr[0]["transDetImp_06"] = ds.Tables[0].Rows[0]["transDetImp_06"].ToString();
                    dr[0]["transDetImp_07"] = ds.Tables[0].Rows[0]["transDetImp_07"].ToString();
                    dr[0]["transDetImp_08"] = ds.Tables[0].Rows[0]["transDetImp_08"].ToString();
                    dr[0]["transDetImp_09"] = ds.Tables[0].Rows[0]["transDetImp_09"].ToString();
                    dr[0]["transDetImp_10"] = ds.Tables[0].Rows[0]["transDetImp_10"].ToString();

                    dr[0]["transDetFact_01"] = ds.Tables[0].Rows[0]["transDetFact_01"].ToString();
                    dr[0]["transDetFact_02"] = ds.Tables[0].Rows[0]["transDetFact_02"].ToString();
                    dr[0]["transDetFact_03"] = ds.Tables[0].Rows[0]["transDetFact_03"].ToString();
                    dr[0]["transDetFact_04"] = ds.Tables[0].Rows[0]["transDetFact_04"].ToString();
                    dr[0]["transDetFact_05"] = ds.Tables[0].Rows[0]["transDetFact_05"].ToString();

                }
            }

            return true;
        }


        public Boolean ActualizaTransacciones(string Pag_sConexionLog, string sCiaCve, ref DataRow dr)
        {
            DataSet dsConfig = new DataSet();
            Int64 iCptoId = 0;
            if (dr["cptoId"].ToString() != "")
            {
                iCptoId = Convert.ToInt64(dr["cptoId"]);
            }

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_TransaccionesDetalle_Aux";

            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 2);

            if (dr["transDetId"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToString(dr["transDetId"]));
            }
            if (dr["transId"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(dr["transId"]));
            }
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr["ciaCve"]));

            if (dr["cptoId"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToString(dr["cptoId"]));
            }
            if (dr["cptoId"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetSec", DbType.Int64, 0, ParameterDirection.Input, Convert.ToString(dr["transDetSec"]));
            }
            ProcBD.AgregarParametrosProcedimiento("@monCve", DbType.String, 2, ParameterDirection.Input, Convert.ToString(dr["monCve"]));

            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_01", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr["transDetStr10_01"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_02", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr["transDetStr10_02"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_03", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr["transDetStr10_03"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_04", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr["transDetStr10_04"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_05", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr["transDetStr10_05"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_06", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr["transDetStr10_06"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_07", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr["transDetStr10_07"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_08", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr["transDetStr10_08"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_09", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr["transDetStr10_09"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_10", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr["transDetStr10_10"]));

            ProcBD.AgregarParametrosProcedimiento("@transDetStr20_01", DbType.String, 20, ParameterDirection.Input, Convert.ToString(dr["transDetStr20_01"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr20_02", DbType.String, 20, ParameterDirection.Input, Convert.ToString(dr["transDetStr20_02"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr20_03", DbType.String, 20, ParameterDirection.Input, Convert.ToString(dr["transDetStr20_03"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr20_04", DbType.String, 20, ParameterDirection.Input, Convert.ToString(dr["transDetStr20_04"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr20_05", DbType.String, 20, ParameterDirection.Input, Convert.ToString(dr["transDetStr20_05"]));

            ProcBD.AgregarParametrosProcedimiento("@transDetStr40_01", DbType.String, 40, ParameterDirection.Input, Convert.ToString(dr["transDetStr40_01"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr40_02", DbType.String, 40, ParameterDirection.Input, Convert.ToString(dr["transDetStr40_02"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr40_03", DbType.String, 40, ParameterDirection.Input, Convert.ToString(dr["transDetStr40_03"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr40_04", DbType.String, 40, ParameterDirection.Input, Convert.ToString(dr["transDetStr40_04"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr40_05", DbType.String, 40, ParameterDirection.Input, Convert.ToString(dr["transDetStr40_05"]));

            if (dr["transDetImp_01"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_01", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr["transDetImp_01"]));
            }
            if (dr["transDetImp_02"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_02", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr["transDetImp_02"]));
            }
            if (dr["transDetImp_03"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_03", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr["transDetImp_03"]));
            }
            if (dr["transDetImp_04"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_04", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr["transDetImp_04"]));
            }
            if (dr["transDetImp_05"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_05", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr["transDetImp_05"]));
            }
            if (dr["transDetImp_06"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_06", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr["transDetImp_06"]));
            }
            if (dr["transDetImp_07"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_07", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr["transDetImp_07"]));
            }
            if (dr["transDetImp_08"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_08", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr["transDetImp_08"]));
            }
            if (dr["transDetImp_09"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_09", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr["transDetImp_09"]));
            }
            if (dr["transDetImp_10"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_10", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr["transDetImp_10"]));
            }



            if (dr["transDetFec_01"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetFec_01", DbType.String, 100, ParameterDirection.Input, Convert.ToDateTime(dr["transDetFec_01"]).ToString("yyyy-MM-dd"));
            }
            if (dr["transDetFec_02"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetFec_02", DbType.String, 100, ParameterDirection.Input, Convert.ToDateTime(dr["transDetFec_02"]).ToString("yyyy-MM-dd"));
            }
            if (dr["transDetFec_03"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetFec_03", DbType.String, 100, ParameterDirection.Input, Convert.ToDateTime(dr["transDetFec_03"]).ToString("yyyy-MM-dd"));
            }
            if (dr["transDetFec_04"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetFec_04", DbType.String, 100, ParameterDirection.Input, Convert.ToDateTime(dr["transDetFec_04"]).ToString("yyyy-MM-dd"));
            }
            if (dr["transDetFec_05"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetFec_05", DbType.String, 100, ParameterDirection.Input, Convert.ToDateTime(dr["transDetFec_05"]).ToString("yyyy-MM-dd"));
            }


            if (dr["transDetFact_01"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetFact_01", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr["transDetFact_01"]));
            }
            if (dr["transDetFact_02"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetFact_02", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr["transDetFact_02"]));
            }
            if (dr["transDetFact_03"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetFact_03", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr["transDetFact_03"]));
            }
            if (dr["transDetFact_04"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetFact_04", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr["transDetFact_04"]));
            }
            if (dr["transDetFact_05"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetFact_05", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr["transDetFact_05"]));
            }

            if (dr["transDetSit"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetSit", DbType.String, 3, ParameterDirection.Input, Convert.ToString(dr["transDetSit"]));
            }

            ProcBD.AgregarParametrosProcedimiento("@transDetClaveAux", DbType.String, 50, ParameterDirection.Input, "");

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            //dr["transDetFact_03"] = ds.Tables[0].Rows[0]["transDetFact_03"].ToString();
            dr["transDetImp_01"] = ds.Tables[0].Rows[0]["transDetImp_01"].ToString();
            dr["transDetImp_02"] = ds.Tables[0].Rows[0]["transDetImp_02"].ToString();
            dr["transDetImp_03"] = ds.Tables[0].Rows[0]["transDetImp_03"].ToString();
            dr["transDetImp_04"] = ds.Tables[0].Rows[0]["transDetImp_04"].ToString();
            dr["transDetImp_05"] = ds.Tables[0].Rows[0]["transDetImp_05"].ToString();
            dr["transDetImp_06"] = ds.Tables[0].Rows[0]["transDetImp_06"].ToString();
            dr["transDetImp_07"] = ds.Tables[0].Rows[0]["transDetImp_07"].ToString();
            dr["transDetImp_08"] = ds.Tables[0].Rows[0]["transDetImp_08"].ToString();
            dr["transDetImp_09"] = ds.Tables[0].Rows[0]["transDetImp_09"].ToString();
            dr["transDetImp_10"] = ds.Tables[0].Rows[0]["transDetImp_10"].ToString();

            dr["transDetFact_01"] = ds.Tables[0].Rows[0]["transDetFact_01"].ToString();
            dr["transDetFact_02"] = ds.Tables[0].Rows[0]["transDetFact_02"].ToString();
            dr["transDetFact_03"] = ds.Tables[0].Rows[0]["transDetFact_03"].ToString();
            dr["transDetFact_04"] = ds.Tables[0].Rows[0]["transDetFact_04"].ToString();
            dr["transDetFact_05"] = ds.Tables[0].Rows[0]["transDetFact_05"].ToString();

            return true;
        }
        public Boolean ActualizaTransacciones(string Pag_sConexionLog, string sCiaCve, ref DataRow[] dr)
        {
            DataSet dsConfig = new DataSet();
            Int64 iCptoId = 0;
            if (dr[0]["cptoId"].ToString() != "")
            {
                iCptoId = Convert.ToInt64(dr[0]["cptoId"]);
            }



            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_TransaccionesDetalle_Aux";

            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 2);

            if (dr[0]["transDetId"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToString(dr[0]["transDetId"]));
            }
            if (dr[0]["transId"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(dr[0]["transId"]));
            }
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr[0]["ciaCve"]));

            if (dr[0]["cptoId"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToString(dr[0]["cptoId"]));
            }
            if (dr[0]["cptoId"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetSec", DbType.Int64, 0, ParameterDirection.Input, Convert.ToString(dr[0]["transDetSec"]));
            }
            ProcBD.AgregarParametrosProcedimiento("@monCve", DbType.String, 2, ParameterDirection.Input, Convert.ToString(dr[0]["monCve"]));

            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_01", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr10_01"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_02", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr10_02"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_03", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr10_03"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_04", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr10_04"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_05", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr10_05"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_06", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr10_06"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_07", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr10_07"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_08", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr10_08"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_09", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr10_09"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_10", DbType.String, 10, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr10_10"]));

            ProcBD.AgregarParametrosProcedimiento("@transDetStr20_01", DbType.String, 20, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr20_01"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr20_02", DbType.String, 20, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr20_02"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr20_03", DbType.String, 20, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr20_03"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr20_04", DbType.String, 20, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr20_04"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr20_05", DbType.String, 20, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr20_05"]));

            ProcBD.AgregarParametrosProcedimiento("@transDetStr40_01", DbType.String, 40, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr40_01"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr40_02", DbType.String, 40, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr40_02"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr40_03", DbType.String, 40, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr40_03"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr40_04", DbType.String, 40, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr40_04"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr40_05", DbType.String, 40, ParameterDirection.Input, Convert.ToString(dr[0]["transDetStr40_05"]));


            if (dr[0]["transDetImp_01"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_01", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr[0]["transDetImp_01"]));
            }
            if (dr[0]["transDetImp_02"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_02", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr[0]["transDetImp_02"]));
            }
            if (dr[0]["transDetImp_03"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_03", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr[0]["transDetImp_03"]));
            }
            if (dr[0]["transDetImp_04"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_04", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr[0]["transDetImp_04"]));
            }
            if (dr[0]["transDetImp_05"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_05", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr[0]["transDetImp_05"]));
            }
            if (dr[0]["transDetImp_06"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_06", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr[0]["transDetImp_06"]));
            }
            if (dr[0]["transDetImp_07"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_07", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr[0]["transDetImp_07"]));
            }
            if (dr[0]["transDetImp_08"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_08", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr[0]["transDetImp_08"]));
            }
            if (dr[0]["transDetImp_09"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_09", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr[0]["transDetImp_09"]));
            }
            if (dr[0]["transDetImp_10"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_10", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr[0]["transDetImp_10"]));
            }


            if (dr[0]["transDetFec_01"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetFec_01", DbType.String, 100, ParameterDirection.Input, Convert.ToDateTime(dr[0]["transDetFec_01"]).ToString("yyyy-MM-dd"));
            }
            if (dr[0]["transDetFec_02"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetFec_02", DbType.String, 100, ParameterDirection.Input, Convert.ToDateTime(dr[0]["transDetFec_02"]).ToString("yyyy-MM-dd"));
            }
            if (dr[0]["transDetFec_03"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetFec_03", DbType.String, 100, ParameterDirection.Input, Convert.ToDateTime(dr[0]["transDetFec_03"]).ToString("yyyy-MM-dd"));
            }
            if (dr[0]["transDetFec_04"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetFec_04", DbType.String, 100, ParameterDirection.Input, Convert.ToDateTime(dr[0]["transDetFec_04"]).ToString("yyyy-MM-dd"));
            }
            if (dr[0]["transDetFec_05"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetFec_05", DbType.String, 100, ParameterDirection.Input, Convert.ToDateTime(dr[0]["transDetFec_05"]).ToString("yyyy-MM-dd"));
            }


            if (dr[0]["transDetFact_01"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetFact_01", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr[0]["transDetFact_01"]));
            }
            if (dr[0]["transDetFact_02"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetFact_02", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr[0]["transDetFact_02"]));
            }
            if (dr[0]["transDetFact_03"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetFact_03", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr[0]["transDetFact_03"]));
            }
            if (dr[0]["transDetFact_04"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetFact_04", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr[0]["transDetFact_04"]));
            }
            if (dr[0]["transDetFact_05"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetFact_05", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(dr[0]["transDetFact_05"]));
            }

            if (dr[0]["transDetSit"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetSit", DbType.String, 3, ParameterDirection.Input, Convert.ToString(dr[0]["transDetSit"]));
            }

            ProcBD.AgregarParametrosProcedimiento("@transDetClaveAux", DbType.String, 50, ParameterDirection.Input, "");

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            dr[0]["transDetImp_01"] = ds.Tables[0].Rows[0]["transDetImp_01"].ToString();
            dr[0]["transDetImp_02"] = ds.Tables[0].Rows[0]["transDetImp_02"].ToString();
            dr[0]["transDetImp_03"] = ds.Tables[0].Rows[0]["transDetImp_03"].ToString();
            dr[0]["transDetImp_04"] = ds.Tables[0].Rows[0]["transDetImp_04"].ToString();
            dr[0]["transDetImp_05"] = ds.Tables[0].Rows[0]["transDetImp_05"].ToString();
            dr[0]["transDetImp_06"] = ds.Tables[0].Rows[0]["transDetImp_06"].ToString();
            dr[0]["transDetImp_07"] = ds.Tables[0].Rows[0]["transDetImp_07"].ToString();
            dr[0]["transDetImp_08"] = ds.Tables[0].Rows[0]["transDetImp_08"].ToString();
            dr[0]["transDetImp_09"] = ds.Tables[0].Rows[0]["transDetImp_09"].ToString();
            dr[0]["transDetImp_10"] = ds.Tables[0].Rows[0]["transDetImp_10"].ToString();

            dr[0]["transDetFact_01"] = ds.Tables[0].Rows[0]["transDetFact_01"].ToString();
            dr[0]["transDetFact_02"] = ds.Tables[0].Rows[0]["transDetFact_02"].ToString();
            dr[0]["transDetFact_03"] = ds.Tables[0].Rows[0]["transDetFact_03"].ToString();
            dr[0]["transDetFact_04"] = ds.Tables[0].Rows[0]["transDetFact_04"].ToString();
            dr[0]["transDetFact_05"] = ds.Tables[0].Rows[0]["transDetFact_05"].ToString();


            return true;
        }


    }

    public class FnParametros {
        ws.Servicio oWS = new ws.Servicio();

        public DataSet dsParametros(string Pag_sConexionLog, string sCiaCve, string sParmCve, int iParmSec = 0) {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_parametrosDinamico";
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
            ProcBD.AgregarParametrosProcedimiento("@parmCve", DbType.String, 10, ParameterDirection.Input, sParmCve);
            if (iParmSec > 0)
            {
                ProcBD.AgregarParametrosProcedimiento("@parmSec", DbType.Int64, 0, ParameterDirection.Input, iParmSec);
            }
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            return ds;
        }
        public string sParametroValStr(string Pag_sConexionLog, string sCiaCve, string sParmCve, int iParmSec)
        {
            string sValStr = "";
            DataSet ds = new DataSet();

            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_parametrosDinamico";
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
            ProcBD.AgregarParametrosProcedimiento("@parmCve", DbType.String, 10, ParameterDirection.Input, sParmCve);
            ProcBD.AgregarParametrosProcedimiento("@parmSec", DbType.Int64, 0, ParameterDirection.Input, iParmSec);
            ProcBD.AgregarParametrosProcedimiento("@parmValTip", DbType.String, 5, ParameterDirection.Input, "Str");
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            sValStr = ds.Tables[0].Rows[0]["parmValor"].ToString();

            return sValStr;
        }
        public string sParametroValInt(string Pag_sConexionLog, string sCiaCve, string sParmCve, int iParmSec)
        {
            string iInt = "";
            DataSet ds = new DataSet();

            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_parametrosDinamico";
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
            ProcBD.AgregarParametrosProcedimiento("@parmCve", DbType.String, 10, ParameterDirection.Input, sParmCve);
            ProcBD.AgregarParametrosProcedimiento("@parmSec", DbType.Int64, 0, ParameterDirection.Input, iParmSec);
            ProcBD.AgregarParametrosProcedimiento("@parmValTip", DbType.String, 5, ParameterDirection.Input, "Int");
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            iInt = ds.Tables[0].Rows[0]["parmValor"].ToString();

            return iInt;
        }
        public decimal sParametroValDec(string Pag_sConexionLog, string sCiaCve, string sParmCve, int iParmSec)
        {
            Decimal iDec = 0;
            DataSet ds = new DataSet();

            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_parametrosDinamico";
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
            ProcBD.AgregarParametrosProcedimiento("@parmCve", DbType.String, 10, ParameterDirection.Input, sParmCve);
            ProcBD.AgregarParametrosProcedimiento("@parmSec", DbType.Int64, 0, ParameterDirection.Input, iParmSec);
            ProcBD.AgregarParametrosProcedimiento("@parmValTip", DbType.String, 5, ParameterDirection.Input, "Dec");
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            iDec = Convert.ToDecimal(ds.Tables[0].Rows[0]["parmValor"]);

            return iDec;
        }
        public DateTime sParametroValDate(string Pag_sConexionLog, string sCiaCve, string sParmCve, int iParmSec)
        {
            DateTime iDate;
            DataSet ds = new DataSet();

            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_parametrosDinamico";
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
            ProcBD.AgregarParametrosProcedimiento("@parmCve", DbType.String, 10, ParameterDirection.Input, sParmCve);
            ProcBD.AgregarParametrosProcedimiento("@parmSec", DbType.Int64, 0, ParameterDirection.Input, iParmSec);
            ProcBD.AgregarParametrosProcedimiento("@parmValTip", DbType.String, 5, ParameterDirection.Input, "Date");
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            iDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["parmValor"]);

            return iDate;
        }

    }

    public class FNPeriodosCalendario{
        ws.Servicio oWS = new ws.Servicio();
        public DataSet dsValidaPeriodoFecha(string Pag_sConexionLog, string sCiaCve, DateTime FechaPer)
        {
            DataSet ds = new DataSet();
            string Val_TransFec = FechaPer.Year + "/" + FechaPer.Month.ToString().PadLeft(2, '0') + "/" + FechaPer.Day.ToString().PadLeft(2, '0');

            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_Periodos";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 60);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
            ProcBD.AgregarParametrosProcedimiento("@perFec", DbType.String, 100, ParameterDirection.Input, Val_TransFec + " 00:00:00");
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            return ds;
        }

        public DataSet dsAniosPeriodos(string Pag_sConexionLog, string sCiaCve) {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_Periodos";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 100);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            return ds;
        }

        public DataSet dsNumeroPeriodos(string Pag_sConexionLog, string sCiaCve, int perAnio)
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_Periodos";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 101);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
            ProcBD.AgregarParametrosProcedimiento("@perAnio", DbType.Int64 , 0, ParameterDirection.Input, perAnio);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            return ds;
        }

    }

    public class FnValidaciones {
        ws.Servicio oWS = new ws.Servicio();
        MGMFnGrales.FNDatos FNDatos = new MGMFnGrales.FNDatos();

        public Boolean bAcciones_ValidacionesGVSelect(string Pag_sConexionLog, RadGrid objRadGrid, string[] Validaciones, ref string sMSGTip, ref string sResult)
        {
            Boolean Result = true;
            string maMSGCve;

            for (int i = 0; i < Validaciones.Length; i++)
            {
                maMSGCve = Validaciones[i];

                if (maMSGCve == "VAL0003")
                {
                    if (objRadGrid.SelectedItems.Count == 0)
                    {
                        FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                        return false;
                    }
                }
                if (maMSGCve == "VAL0008")
                {
                    if (objRadGrid.SelectedItems.Count > 1)
                    {
                        FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0008", ref sMSGTip, ref sResult);
                        return false;
                    }
                }
            }
            return Result;
        }

        
        public DataSet dsEXPRODoc_ValidacionesProcesos(string Pag_sConexionLog, string sCiaCve, int iFolioId, string sDocCve, string sMaUsuCve, int sSitCve)
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_EXPRODoc_Valida";
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
            ProcBD.AgregarParametrosProcedimiento("@RegId", DbType.Int64, 0, ParameterDirection.Input, iFolioId);
            ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, sDocCve);
            ProcBD.AgregarParametrosProcedimiento("@docRegSitId", DbType.Int64, 0, ParameterDirection.Input, sSitCve);
            ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 10, ParameterDirection.Input, sMaUsuCve);
            ProcBD.AgregarParametrosProcedimiento("@ExValida", DbType.Int64, 0, ParameterDirection.Input, 1);
            ProcBD.AgregarParametrosProcedimiento("@MuestraMgs", DbType.Int64, 0, ParameterDirection.Input, 1);

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            return ds;
        }
        public DataSet dsEXPROOpe_ValidacionesProcesos(string Pag_sConexionLog, string sCiaCve, int iFolioId, string sValiProcCve, string sMaUsuCve,  string sSitCve)
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_EXPROOpe_ValidacionesProcesos";
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
            ProcBD.AgregarParametrosProcedimiento("@RegId", DbType.Int64, 0, ParameterDirection.Input, iFolioId);
            ProcBD.AgregarParametrosProcedimiento("@valiProcCve", DbType.String, 10, ParameterDirection.Input, sValiProcCve);
            ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 10, ParameterDirection.Input, sMaUsuCve);
            ProcBD.AgregarParametrosProcedimiento("@SitCve", DbType.String, 3, ParameterDirection.Input, sSitCve);
 
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            return ds;
        }


        public Boolean bAcciones_ValidaAccionesSituacionesDoc(string Pag_sConexionLog, int iAccId, int iDocRegSitId, ref string sMSGTip, ref string maMSGDes)
        {
            Boolean Result = true;

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_EXPRODoc_ValidaAccionesSituacionesDoc";
            ProcBD.AgregarParametrosProcedimiento("@accId", DbType.Int64, 0, ParameterDirection.Input, iAccId);
            ProcBD.AgregarParametrosProcedimiento("@docRegSitId", DbType.Int64, 0, ParameterDirection.Input, iDocRegSitId);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            sMSGTip = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
            maMSGDes = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

            if (sMSGTip != "1") {
                return false; 
            }

            return Result;
        }




        public Boolean bAcciones_ValidaAccionesSituacionesOpe(string Pag_sConexionLog, string sCiaCve, int iAccId, string sSitCve,int iTransId, ref string sMSGTip, ref string maMSGDes)
        {
            Boolean Result = true;

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_EXPROOpe_ValidaAccionesSituacionesOpe";
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
            ProcBD.AgregarParametrosProcedimiento("@accId", DbType.Int64, 0, ParameterDirection.Input, iAccId);
            ProcBD.AgregarParametrosProcedimiento("@sitCve", DbType.String , 3, ParameterDirection.Input, sSitCve);
            ProcBD.AgregarParametrosProcedimiento("@RegId", DbType.Int64, 0, ParameterDirection.Input, iTransId);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            sMSGTip = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
            maMSGDes = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

            if (sMSGTip != "1")
            {
                return false;
            }
            return Result;
        }

        public Boolean bAcciones_ValidaAccionesSituacionesOpe_Pagos(string Pag_sConexionLog, string sCiaCve, int iAccId, string sSitCve, int iTransId, ref string sMSGTip, ref string maMSGDes)
        {
            Boolean Result = true;

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_EXPROOpe_ValidaAccionesSituacionesOpe_Pagos";
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
            ProcBD.AgregarParametrosProcedimiento("@accId", DbType.Int64, 0, ParameterDirection.Input, iAccId);
            ProcBD.AgregarParametrosProcedimiento("@sitCve", DbType.String, 3, ParameterDirection.Input, sSitCve);
            ProcBD.AgregarParametrosProcedimiento("@RegId", DbType.Int64, 0, ParameterDirection.Input, iTransId);
            //GRET, se agrega parametro para validar algunas situaciones solo para Admon de Pagos
            ProcBD.AgregarParametrosProcedimiento("@contRefCve", DbType.String, 3, ParameterDirection.Input, "XP");
            //
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            sMSGTip = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
            maMSGDes = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

            if (sMSGTip != "1")
            {
                return false;
            }
            return Result;
        }

        public Boolean bAcciones_ValidaAccionesSituacionesOpe_Cobros(string Pag_sConexionLog, string sCiaCve, int iAccId, string sSitCve, int iTransId, ref string sMSGTip, ref string maMSGDes)
        {
            Boolean Result = true;

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_EXPROOpe_ValidaAccionesSituacionesOpe_Cobros";
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
            ProcBD.AgregarParametrosProcedimiento("@accId", DbType.Int64, 0, ParameterDirection.Input, iAccId);
            ProcBD.AgregarParametrosProcedimiento("@sitCve", DbType.String, 3, ParameterDirection.Input, sSitCve);
            ProcBD.AgregarParametrosProcedimiento("@RegId", DbType.Int64, 0, ParameterDirection.Input, iTransId);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            sMSGTip = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
            maMSGDes = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

            if (sMSGTip != "1")
            {
                return false;
            }
            return Result;
        }





        public Boolean bAcciones_ValidaAccionesSituacionesAisCont(string Pag_sConexionLog, string sCiaCve, int iAccId, string sSitCve, int iTransId, ref string sMSGTip, ref string maMSGDes)
        {
            Boolean Result = true;

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_EXPROOpe_ValidaAccionesSituacionesAsiCont";
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
            ProcBD.AgregarParametrosProcedimiento("@accId", DbType.Int64, 0, ParameterDirection.Input, iAccId);
            ProcBD.AgregarParametrosProcedimiento("@sitCve", DbType.String, 3, ParameterDirection.Input, sSitCve);
            ProcBD.AgregarParametrosProcedimiento("@RegId", DbType.Int64, 0, ParameterDirection.Input, iTransId);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            sMSGTip = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
            maMSGDes = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

            if (sMSGTip != "1")
            {
                return false;
            }
            return Result;
        }
        public Boolean bAcciones_ValidaAccionesSituacionesADMON(string Pag_sConexionLog, string sCiaCve, int iAccId, string sSitCve, int iRegId, ref string sMSGTip, ref string maMSGDes, string sModulo)
        {
            Boolean Result = true;

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_EXPROOpe_ValidaAccionesSituacionesAdmon";
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
            ProcBD.AgregarParametrosProcedimiento("@accId", DbType.Int64, 0, ParameterDirection.Input, iAccId);
            ProcBD.AgregarParametrosProcedimiento("@sitCve", DbType.String, 3, ParameterDirection.Input, sSitCve);
            ProcBD.AgregarParametrosProcedimiento("@RegId", DbType.Int64, 0, ParameterDirection.Input, iRegId);
            ProcBD.AgregarParametrosProcedimiento("@modulo", DbType.String, 2, ParameterDirection.Input, sModulo);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            sMSGTip = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
            maMSGDes = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

            if (sMSGTip != "1")
            {
                return false;
            }
            return Result;
        }




    }


}