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
using System.Data.Entity;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using System.Xml;

/// <summary>
/// Summary description for csDocumentos
/// </summary>
/// 
namespace comprobante
{
    public class Datos
    {
        ws.Servicio oWS = new ws.Servicio();
        wsRpt.Service oWSRpt = new wsRpt.Service();
        MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();
        public bool Comprobante_Paso(string sPag_sConexionLog, string sCiaCve, int iDocRegId, string apppath, string maUsuCve)
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_DocumentoCFDI";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
            ProcBD.AgregarParametrosProcedimiento("@docRegId", DbType.Int64, 0, ParameterDirection.Input, iDocRegId);
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);


            if (FnValAdoNet.bDSRowsIsFill(ds))
            {
                byte[] bytes = (byte[])ds.Tables[0].Rows[0]["docCFDIXML"];
                System.IO.File.WriteAllBytes(apppath, bytes);

                //Obtener datos del XML Para llenado de Reporte
                DataSet DS_XML = new DataSet();
                DS_XML = DSInf(apppath);

                EjecutaComprobante_Paso(sPag_sConexionLog, sCiaCve, iDocRegId, DS_XML, maUsuCve);
                EjecutaComprobanteConcepto_Paso(sPag_sConexionLog, sCiaCve, iDocRegId, DS_XML, maUsuCve);
            }

            return true;
        }

        private DataSet DSInf(string RutaName_FileXML)
        {


            DataSet DS = new DataSet();
            try
            {
                string myXMLfile = RutaName_FileXML;
                System.IO.FileStream fsReadXml = new System.IO.FileStream(myXMLfile, System.IO.FileMode.Open);
                try
                {
                    DS.ReadXml(fsReadXml);
                }
                catch (Exception ex)
                {
                         }
                finally
                {
                    fsReadXml.Close();
                }
            }
            catch (Exception ex)
            {
            }
            return DS;
        }
        private void EjecutaComprobante_Paso(string sPag_sConexionLog, string sCiaCve, Int32 iDocRegId, DataSet ValCFDI, string maUsuCve)
        {
            try
            {
     
                DataSet ds = new DataSet();

                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                ProcBD.NombreProcedimiento = "sp_SATCFDIComprobante_Paso";
                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);

                ProcBD.AgregarParametrosProcedimiento("@docRegId", DbType.Int32, 0, ParameterDirection.Input, iDocRegId);
                ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
                ProcBD.AgregarParametrosProcedimiento("@cSerie", DbType.String, 10, ParameterDirection.Input, ValCFDI.Tables["Comprobante"].Rows[0]["Serie"].ToString());
                ProcBD.AgregarParametrosProcedimiento("@cFolio", DbType.String, 10, ParameterDirection.Input, ValCFDI.Tables["Comprobante"].Rows[0]["Folio"].ToString());
                ProcBD.AgregarParametrosProcedimiento("@cFecha", DbType.String, 100, ParameterDirection.Input, Convert.ToString(ValCFDI.Tables["Comprobante"].Rows[0]["Fecha"]));

                ProcBD.AgregarParametrosProcedimiento("@cSello", DbType.String, 8000, ParameterDirection.Input, ValCFDI.Tables["Comprobante"].Rows[0]["Sello"].ToString());
                ProcBD.AgregarParametrosProcedimiento("@cFormaPago", DbType.String, 2, ParameterDirection.Input, ValCFDI.Tables["Comprobante"].Rows[0]["FormaPago"].ToString());
                ProcBD.AgregarParametrosProcedimiento("@cMoneda", DbType.String, 3, ParameterDirection.Input, ValCFDI.Tables["Comprobante"].Rows[0]["Moneda"].ToString());
                ProcBD.AgregarParametrosProcedimiento("@cTipoCambio", DbType.Decimal, 0, ParameterDirection.Input, Convert.ToDecimal(ValCFDI.Tables["Comprobante"].Rows[0]["TipoCambio"]));
                ProcBD.AgregarParametrosProcedimiento("@cTotal", DbType.Decimal, 0, ParameterDirection.Input, Convert.ToDecimal(ValCFDI.Tables["Comprobante"].Rows[0]["Total"]));

                ProcBD.AgregarParametrosProcedimiento("@cTipoDeComprobante", DbType.String, 1, ParameterDirection.Input, ValCFDI.Tables["Comprobante"].Rows[0]["TipodeComprobante"].ToString());
                ProcBD.AgregarParametrosProcedimiento("@cMetodoPago", DbType.String, 3, ParameterDirection.Input, ValCFDI.Tables["Comprobante"].Rows[0]["MetodoPago"].ToString());
                ProcBD.AgregarParametrosProcedimiento("@cLugarExpedicion", DbType.String, 10, ParameterDirection.Input, ValCFDI.Tables["Comprobante"].Rows[0]["LugarExpedicion"].ToString());

                ProcBD.AgregarParametrosProcedimiento("@cNoCertificado", DbType.String, 8000, ParameterDirection.Input, ValCFDI.Tables["Comprobante"].Rows[0]["NoCertificado"].ToString());
                ProcBD.AgregarParametrosProcedimiento("@cCertificado", DbType.String, 8000, ParameterDirection.Input, ValCFDI.Tables["Comprobante"].Rows[0]["Certificado"].ToString());
                ProcBD.AgregarParametrosProcedimiento("@cConfirmacion", DbType.String, 100, ParameterDirection.Input, "");
                ProcBD.AgregarParametrosProcedimiento("@cSubtotal", DbType.Decimal, 0, ParameterDirection.Input, Convert.ToDecimal(ValCFDI.Tables["Comprobante"].Rows[0]["Subtotal"]));
                ProcBD.AgregarParametrosProcedimiento("@cCondicionesDePago", DbType.String, 100, ParameterDirection.Input, ValCFDI.Tables["Comprobante"].Rows[0]["CondicionesDePago"].ToString());
                ProcBD.AgregarParametrosProcedimiento("@cDescuento", DbType.Decimal, 0, ParameterDirection.Input, Convert.ToDecimal(ValCFDI.Tables["Comprobante"].Rows[0]["Descuento"]));


                //EMISOR
                ProcBD.AgregarParametrosProcedimiento("@cEmiRFC", DbType.String, 50, ParameterDirection.Input, ValCFDI.Tables["Emisor"].Rows[0]["Rfc"].ToString());
                ProcBD.AgregarParametrosProcedimiento("@cEmiNombre", DbType.String, 300, ParameterDirection.Input, ValCFDI.Tables["Emisor"].Rows[0]["Nombre"].ToString());
                ProcBD.AgregarParametrosProcedimiento("@cEmiRegimenFiscal", DbType.String, 50, ParameterDirection.Input, ValCFDI.Tables["Emisor"].Rows[0]["RegimenFiscal"].ToString());

                ////Receptor
                ProcBD.AgregarParametrosProcedimiento("@cRecRFC", DbType.String, 50, ParameterDirection.Input, ValCFDI.Tables["Receptor"].Rows[0]["Rfc"].ToString());
                ProcBD.AgregarParametrosProcedimiento("@cRecNombre", DbType.String, 300, ParameterDirection.Input, ValCFDI.Tables["Receptor"].Rows[0]["Nombre"].ToString());
                //ProcBD.AgregarParametrosProcedimiento("@cRecRecidenciaFiscal", DbType.String, 3, ParameterDirection.Input, ValCFDI.Tables["Receptor"].Rows[0]["ResidenciaFiscal"].ToString());
                //ProcBD.AgregarParametrosProcedimiento("@cRecNumRegIdTrib", DbType.String, 50, ParameterDirection.Input, ValCFDI.Tables["Receptor"].Rows[0]["NumRegIdTrib"].ToString());
                ProcBD.AgregarParametrosProcedimiento("@cRecUsoCFDI", DbType.String, 100, ParameterDirection.Input, ValCFDI.Tables["Receptor"].Rows[0]["UsoCFDI"].ToString());

                //TimbreFiscalDigital
                ProcBD.AgregarParametrosProcedimiento("@cComTFDUUID", DbType.String, 100, ParameterDirection.Input, ValCFDI.Tables["TimbreFiscalDigital"].Rows[0]["UUID"].ToString());
                ProcBD.AgregarParametrosProcedimiento("@cComTFDSelloSAT", DbType.String, 8000, ParameterDirection.Input, ValCFDI.Tables["TimbreFiscalDigital"].Rows[0]["SelloSAT"].ToString());
                ProcBD.AgregarParametrosProcedimiento("@cComTFDSelloCFD", DbType.String, 8000, ParameterDirection.Input, ValCFDI.Tables["TimbreFiscalDigital"].Rows[0]["SelloCFD"].ToString());
                ProcBD.AgregarParametrosProcedimiento("@cComTFDRfcProvCertif", DbType.String, 100, ParameterDirection.Input, ValCFDI.Tables["TimbreFiscalDigital"].Rows[0]["RfcProvCertif"].ToString());
                ProcBD.AgregarParametrosProcedimiento("@cComTFDNoCertificadoSAT", DbType.String, 100, ParameterDirection.Input, ValCFDI.Tables["TimbreFiscalDigital"].Rows[0]["NoCertificadoSAT"].ToString());
                ProcBD.AgregarParametrosProcedimiento("@cComTFDFechaTimbrado", DbType.String, 100, ParameterDirection.Input, ValCFDI.Tables["TimbreFiscalDigital"].Rows[0]["FechaTimbrado"].ToString());

                //USUARIO
                ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, maUsuCve);

                ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);

                if (FnValAdoNet.bDSIsFill(ds))
                {
                    string sEjecEstatus, sEjecMSG = "";
                    sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                    sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                    //ShowAlert(sEjecEstatus, sEjecMSG);
                    if (sEjecEstatus == "1")
                    {
                        //InicioPagina();
                        // InicioAlt();

                    }
                }
            }
            catch (Exception ex)
            {
                string MsgError = ex.Message.Trim();
                //MessageBox.Show(ex.ToString());
            }
        }


        private void EjecutaComprobanteConcepto_Paso(string sPag_sConexionLog, string sCiaCve, Int32 iDocRegId, DataSet ValCFDI, string maUsuCve)
        {

            try
            {
                int val = 0;
                foreach (DataRow row in ValCFDI.Tables["Concepto"].Rows)
                {
                    DataSet ds = new DataSet();
                    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                    ProcBD.NombreProcedimiento = "sp_SATCFDIComprobanteConcepto_Paso";
                    ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
                    ProcBD.AgregarParametrosProcedimiento("@docRegId", DbType.Int32, 0, ParameterDirection.Input, iDocRegId);
                    ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);

                    ProcBD.AgregarParametrosProcedimiento("@cCptoClaveProdServ", DbType.String, 8, ParameterDirection.Input, ValCFDI.Tables["Concepto"].Rows[val]["ClaveProdServ"]);
                    ProcBD.AgregarParametrosProcedimiento("@cCptoNoIdentificacion", DbType.String, 50, ParameterDirection.Input, ValCFDI.Tables["Concepto"].Rows[val]["NoIdentificacion"]);
                    ProcBD.AgregarParametrosProcedimiento("@cCptoCantidad", DbType.Decimal, 0, ParameterDirection.Input, Convert.ToDecimal(ValCFDI.Tables["Concepto"].Rows[val]["Cantidad"]));
                    ProcBD.AgregarParametrosProcedimiento("@cCptoClaveUnidad", DbType.String, 3, ParameterDirection.Input, ValCFDI.Tables["Concepto"].Rows[val]["ClaveUnidad"]);
                    ProcBD.AgregarParametrosProcedimiento("@cCptoUnidad", DbType.String, 50, ParameterDirection.Input, ValCFDI.Tables["Concepto"].Rows[val]["Unidad"]);
                    ProcBD.AgregarParametrosProcedimiento("@cCptoDescripcion", DbType.String, 50, ParameterDirection.Input, ValCFDI.Tables["Concepto"].Rows[val]["Descripcion"]);
                    ProcBD.AgregarParametrosProcedimiento("@cCptoValorUnitario", DbType.Decimal, 0, ParameterDirection.Input, Convert.ToDecimal(ValCFDI.Tables["Concepto"].Rows[val]["ValorUnitario"]));
                    ProcBD.AgregarParametrosProcedimiento("@cCptoImporte", DbType.Decimal, 0, ParameterDirection.Input, Convert.ToDecimal(ValCFDI.Tables["Concepto"].Rows[val]["Importe"]));
                    ProcBD.AgregarParametrosProcedimiento("@cCptoDescuento", DbType.Decimal, 0, ParameterDirection.Input, Convert.ToDecimal(ValCFDI.Tables["Concepto"].Rows[val]["Descuento"]));
                    ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, maUsuCve);

                    ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);

                    val++;

                }
            }
            catch (Exception ex)
            {
                string MsgError = ex.Message.Trim();
                //MessageBox.Show(ex.ToString());
            }
        }
    }

}
