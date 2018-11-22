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

//Librerias Uso de Datos
using System.Globalization;

//Librerias Envio de Doc
using System.Drawing;
using System.Net;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Net.Mail;
using System.Text;
using Newtonsoft.Json;


/// <summary>
/// Summary description for csStamps
/// </summary>
public class csStamps : System.Web.UI.Page
{
    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    ws.Servicio oWS = new ws.Servicio();
    wsRpt.Service oWSRpt = new wsRpt.Service();

    //Variables staticas Conexion Timbrado
    private static string Pag_sUrlApi;
    private static string Pag_sKeyApi;
    private static string Pag_sUsrApi;
    private static string Pag_sNamApi;

    //Variables de Mensajes de Error
    private static string msgWarningsPB = "";
    private static string msgErrorsPB = "";

    //Variables Servidor de Mensajeria
    private static string valSmtpServer = "";
    private static string valMailServer = "";
    private static string valPswServer = "";

    //Variables staticas Valor Moneda NACIONAL
    private static string Pag_sMonDef;

    //Variables SAT parametros Compañia
    private static string Pag_sRegFisc;

    //Variable statica Valor Correo a Copiar por Def. Doc
    private static string valMailCopyServer = "";


    public csStamps()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //Metodo a llamar para Timbrar Documentos

    public bool stampDoc(string prmDocRegId, string prmDocCve, string Pag_sCompania, string Pag_sConexionLog, string prmCliCveMail, string urlSaveTxt, string urlSavePdf, string urlSaveXml, ref DataTable dtLogProc, int docCmpTrnId, string maUser)
    {

        //Crear txt Log

        string UrlTxt = "";
      
        string logTxt = "";


        FileStream stream = new FileStream(urlSaveTxt, FileMode.OpenOrCreate, FileAccess.Write);
        StreamWriter writer = new StreamWriter(stream);


        //PARAMETROS
        Valores_PB(Pag_sCompania, Pag_sConexionLog);
        Valores_Com(Pag_sCompania, Pag_sConexionLog);
        Valores_ComSAT(Pag_sCompania, Pag_sConexionLog);
        getValServer(Pag_sCompania, Pag_sConexionLog);

 
        string fil = prmDocRegId;

        string saltoLinea = "\r\n";
        string spDocCve = prmDocCve;

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_RptDataSetDoc";

        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, spDocCve);
        ProcBD.AgregarParametrosProcedimiento("@fltrDocRegIds", DbType.String, 2048, ParameterDirection.Input, fil);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0 && chkDocReadyStmp(Pag_sCompania, Pag_sConexionLog, ds.Tables[0].Rows[0]["docRegId"].ToString()))
            {
                //Crear JSON

                //Iniciar session y obtencion de token
                string tokenUsrPB;
                tokenUsrPB = getTokenPB(Pag_sKeyApi, Pag_sUsrApi);

                //Llamado de ENDPOINT INVOICES PB
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(Pag_sUrlApi + "invoicing/mx/invoices");

                //var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://sync.paybook.com/v1/invoicing/mx/invoices/check");

                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";


                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {

                    string jsonXml = bldJsonPB(Pag_sCompania, Pag_sConexionLog, tokenUsrPB, ds);
                    streamWriter.Write(jsonXml);
                }

                string xmlPB = "";

                //Controlar errores 4XX's

                try
                {
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var responseText = streamReader.ReadToEnd();

                        //Obtener XML
                        JsonTextReader reader = new JsonTextReader(new StringReader(responseText));

                        bool getVal = false;
                        bool getWar = false;
                        bool getErr = false;



                        while (reader.Read())
                        {
                            if (reader.Value != null)
                            {
                                // Obtener XML dea respuesta JSON

                                if (getVal)
                                {
                                    xmlPB = reader.Value.ToString();
                                    getVal = false;
                                }

                                if (reader.TokenType.ToString() == "PropertyName" && reader.Value.ToString() == "xml")
                                {
                                    getVal = true;
                                }

                                // Obtener Warnings de respuesta JSON

                                if (getWar && reader.TokenType.ToString() == "String")
                                {

                                    msgWarningsPB += ds.Tables[0].Rows[0]["docCve"].ToString();
                                    msgWarningsPB += " - ";
                                    msgWarningsPB += ds.Tables[0].Rows[0]["docRegFolio"].ToString();
                                    msgWarningsPB += " : ";
                                    msgWarningsPB += reader.Value.ToString();
                                    msgWarningsPB += " || ";

                                    msgWarningsPB += saltoLinea;

                                    getWar = false;
                                }

                                if (reader.TokenType.ToString() == "PropertyName" && reader.Value.ToString() == "warnings")
                                {
                                    getWar = true;
                                }

                                if (reader.TokenType.ToString() == "PropertyName" && reader.Value.ToString() == "status")
                                {
                                    getErr = false;
                                }

                                // Obtener Errores de respuesta JSON

                                if ((getErr && reader.TokenType.ToString() == "String") || (getErr && reader.TokenType.ToString() == "Boolean"))
                                {

                                    if (reader.Value.ToString() == "False" || reader.Value.ToString() == "AAA010101AAA")
                                    {
                                        msgErrorsPB += "";
                                        getErr = false;
                                    }
                                    else
                                    {
                                        msgErrorsPB += ds.Tables[0].Rows[0]["docCve"].ToString();
                                        msgErrorsPB += " - ";
                                        msgErrorsPB += ds.Tables[0].Rows[0]["docRegFolio"].ToString();
                                        msgErrorsPB += " : ";
                                        msgErrorsPB += reader.Value.ToString();
                                        msgErrorsPB += " || ";
                                        msgErrorsPB += saltoLinea;


                                        //Grabar errores en tabla
                                        DataRow rowTmp = dtLogProc.NewRow();
                                        rowTmp["docRegId"] = ds.Tables[0].Rows[0]["docRegId"].ToString();
                                        rowTmp["docCve"] = ds.Tables[0].Rows[0]["docCve"].ToString() + " - " + ds.Tables[0].Rows[0]["docRegFolio"].ToString();
                                        rowTmp["docCmpMsgTxt"] = "ERROR: " + reader.Value.ToString();
                                        rowTmp["docCmpMsgUrlSit"] = "~/Imagenes/IcoDocProceso/imgSit5.png";
                                        dtLogProc.Rows.Add(rowTmp);


                                        //saveErrCmp(ds.Tables[0].Rows[0]["docRegId"].ToString(),)
                                        saveErrCmp(ds.Tables[0].Rows[0]["docRegId"].ToString(), "ERROR: " + reader.Value.ToString(), Pag_sConexionLog, ds.Tables[0].Rows[0]["docCve"].ToString() + " - " + ds.Tables[0].Rows[0]["docRegFolio"].ToString(), "~/Imagenes/IcoDocProceso/imgSit5.png", docCmpTrnId.ToString());

                                        getErr = false;
                                    }


                                }

                                if (reader.TokenType.ToString() == "PropertyName" && reader.Value.ToString() == "errors")
                                {
                                    getErr = true;
                                }


                                // Obtener Codigo y mensaje de respuesta JSON

                                if ((getErr && reader.TokenType.ToString() == "String") || (getErr && reader.TokenType.ToString() == "Boolean"))
                                {

                                    if (reader.Value.ToString() == "False")
                                    {
                                        msgErrorsPB += "";
                                        getErr = false;
                                    }
                                    else
                                    {
                                        msgErrorsPB += ds.Tables[0].Rows[0]["docCve"].ToString();
                                        msgErrorsPB += " - ";
                                        msgErrorsPB += ds.Tables[0].Rows[0]["docRegFolio"].ToString();
                                        msgErrorsPB += " : ";
                                        msgErrorsPB += reader.Value.ToString();
                                        msgErrorsPB += " || ";
                                        msgErrorsPB += saltoLinea;

                                        //Grabar errores en tabla
                                        DataRow rowTmp = dtLogProc.NewRow();
                                        rowTmp["docRegId"] = ds.Tables[0].Rows[0]["docRegId"].ToString();
                                        rowTmp["docCve"] = ds.Tables[0].Rows[0]["docCve"].ToString() + " - " + ds.Tables[0].Rows[0]["docRegFolio"].ToString();
                                        rowTmp["docCmpMsgTxt"] = "ERROR: " + reader.Value.ToString();
                                        rowTmp["docCmpMsgUrlSit"] = "~/Imagenes/IcoDocProceso/imgSit5.png";
                                        dtLogProc.Rows.Add(rowTmp);


                                        saveErrCmp(ds.Tables[0].Rows[0]["docRegId"].ToString(), "ERROR: " + reader.Value.ToString(), Pag_sConexionLog, ds.Tables[0].Rows[0]["docCve"].ToString() + " - " + ds.Tables[0].Rows[0]["docRegFolio"].ToString(), "~/Imagenes/IcoDocProceso/imgSit5.png", docCmpTrnId.ToString());

                                        getErr = false;
                                    }


                                }

                                if (reader.TokenType.ToString() == "PropertyName" && reader.Value.ToString() == "message")
                                {
                                    getErr = true;
                                }


                            }
                            else
                            {
                                //Console.WriteLine("Token: {0}", reader.TokenType);
                                //MessageBox.Show(reader.TokenType.ToString());

                            }

                        }

                    }




                    /*
                    //Guardar XML
                    string fileName = "";

                    string apppath = MapPath("~");
                    //string apppath = "";
                    apppath += "Temp\\";

                    UrlXml = "MGMXML" + "-" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() +
                        "-" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + ".xml";

                    fileName = @apppath + UrlXml;
                    */

                    FileStream streamXML = new FileStream(urlSaveXml, FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter writerXML = new StreamWriter(streamXML);

                    writerXML.WriteLine(xmlPB);
                    writerXML.Close();

                    //Leer XML

                    string xmlRespuesta = "";

                    string docCmpUUID = "";
                    string docCmpFch = "";
                    string docCmpNoCFDemi = "";
                    string docCmpNoCFDsat = "";
                    string docCmpSelloEmi = "";
                    string docCmpSelloSat = "";
                    string docCmpCertificado = "";
                    string docCmpCadena = "";

                    string docCmpXML = "";
                    Byte[] bytesCmpXml = File.ReadAllBytes(urlSaveXml);
                    docCmpXML = Convert.ToBase64String(bytesCmpXml);

                    string docCmpPDF = "";


                    XNamespace cfdi = XNamespace.Get("http://www.sat.gob.mx/cfd/3");
                    XNamespace implocal = XNamespace.Get("http://www.sat.gob.mx/implocal");
                    XNamespace tfd = XNamespace.Get("http://www.sat.gob.mx/TimbreFiscalDigital");
                    XNamespace xsi = XNamespace.Get("http://www.w3.org/2001/XMLSchema-instance");


                    if (xmlPB != "")
                    {

                        //Grabar errores en tabla
                        DataRow rowTmp = dtLogProc.NewRow();
                        rowTmp["docRegId"] = ds.Tables[0].Rows[0]["docRegId"].ToString();
                        rowTmp["docCve"] = ds.Tables[0].Rows[0]["docCve"].ToString() + " - " + ds.Tables[0].Rows[0]["docRegFolio"].ToString();
                        rowTmp["docCmpMsgTxt"] = "Timbrado Correctamente";
                        rowTmp["docCmpMsgUrlSit"] = "~/Imagenes/IcoDocProceso/imgSit2.png";
                        dtLogProc.Rows.Add(rowTmp);

                        saveErrCmp(ds.Tables[0].Rows[0]["docRegId"].ToString(), "Timbrado Correctamente", Pag_sConexionLog, ds.Tables[0].Rows[0]["docCve"].ToString() + " - " + ds.Tables[0].Rows[0]["docRegFolio"].ToString(), "~/Imagenes/IcoDocProceso/imgSit2.png", docCmpTrnId.ToString());


                        Int64 prmDocProcIdocRegid = Convert.ToInt64(ds.Tables[0].Rows[0]["docRegId"].ToString());

                        //Documento timbrado , pasa a ser procesado
                        docProcess(Pag_sConexionLog, Pag_sCompania, prmDocProcIdocRegid, 2, maUser, ds.Tables[0].Rows[0]["docCve"].ToString(), docCmpTrnId, ds.Tables[0].Rows[0]["docRegFolio"].ToString());


                        XDocument rdf = XDocument.Load(new StringReader(xmlPB));

                        //Variables a guardar en tb_DocumentoComprobantes


                        //Recorremos nodo CFDI nameSpace Comprobante
                        foreach (var comprobante in rdf.Descendants(cfdi + "Comprobante"))
                        {
                            //Guardar Datos de Facturacion
                            docCmpCertificado = comprobante.Attribute("certificado").Value.ToString();
                            docCmpNoCFDemi = comprobante.Attribute("noCertificado").Value.ToString();
                        }

                        //Recorremos nodo TIMBRE nameSpace Comprobante
                        foreach (var comprobante in rdf.Descendants(tfd + "TimbreFiscalDigital"))
                        {
                            docCmpUUID = comprobante.Attribute("UUID").Value.ToString();
                            docCmpFch = comprobante.Attribute("FechaTimbrado").Value.ToString();
                            docCmpNoCFDsat = comprobante.Attribute("noCertificadoSAT").Value.ToString();
                            docCmpSelloEmi = comprobante.Attribute("selloCFD").Value.ToString();
                            docCmpSelloSat = comprobante.Attribute("selloSAT").Value.ToString();
                        }



                        XName nameX = XName.Get("certificado");



                        docCmpCadena = "||1.0|";
                        docCmpCadena += docCmpUUID;
                        docCmpCadena += "|";
                        docCmpCadena += docCmpFch;
                        docCmpCadena += "|";
                        docCmpCadena += docCmpSelloEmi;
                        docCmpCadena += "|";
                        docCmpCadena += docCmpNoCFDsat;
                        docCmpCadena += "||";


                        //Imagen QR de la Factura
                        string urlApiqr;
                        urlApiqr = "https://api.qrserver.com/v1/create-qr-code/?size=150x150&data=";
                        //scapestring ?
                        urlApiqr += "%3F";
                        urlApiqr += "re";
                        //scapestring =
                        urlApiqr += "%3D";
                        urlApiqr += "AAA010101AAA";
                        //scapestring &
                        urlApiqr += "%26";
                        urlApiqr += "rr";
                        urlApiqr += "%3D";
                        urlApiqr += ds.Tables[0].Rows[0]["clieRegFis"].ToString();
                        urlApiqr += "%26";
                        urlApiqr += "tt";
                        urlApiqr += "%3D";
                        urlApiqr += ds.Tables[0].Rows[0]["docRegTotImpNeto"].ToString();
                        urlApiqr += "%26";
                        urlApiqr += "id";
                        urlApiqr += "%3D";
                        urlApiqr += docCmpUUID;


                        WebClient wc = new WebClient();
                        //byte[] bytesImg = wc.DownloadData("https://api.qrserver.com/v1/create-qr-code/?size=150x150&data=QRvicZamudio");
                        byte[] bytesImg = wc.DownloadData(urlApiqr);
                        //MemoryStream ms = new MemoryStream(bytesImg);
                        //System.Drawing.Image img = System.Drawing.Image.FromStream(ms);

                        string base64StringImgQr = Convert.ToBase64String(bytesImg, 0, bytesImg.Length);


                        //Guardar Datos de Facturacion para posterior creacion de PDF


                        DataSet dsDC = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBDDC = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBDDC.NombreProcedimiento = "sp_DocumentoComprobantes";

                        ProcBDDC.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
                        ProcBDDC.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                        ProcBDDC.AgregarParametrosProcedimiento("@docRegId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(ds.Tables[0].Rows[0][0].ToString()));

                        ProcBDDC.AgregarParametrosProcedimiento("@docCmpUUID", DbType.String, 50, ParameterDirection.Input, docCmpUUID);
                        ProcBDDC.AgregarParametrosProcedimiento("@docCmpFch", DbType.String, 24, ParameterDirection.Input, docCmpFch);
                        ProcBDDC.AgregarParametrosProcedimiento("@docCmpNoCFDemi", DbType.String, 24, ParameterDirection.Input, docCmpNoCFDemi);
                        ProcBDDC.AgregarParametrosProcedimiento("@docCmpNoCFDsat", DbType.String, 24, ParameterDirection.Input, docCmpNoCFDsat);
                        ProcBDDC.AgregarParametrosProcedimiento("@docCmpSelloEmi", DbType.String, 256, ParameterDirection.Input, docCmpSelloEmi);
                        ProcBDDC.AgregarParametrosProcedimiento("@docCmpSelloSat", DbType.String, 256, ParameterDirection.Input, docCmpSelloSat);
                        ProcBDDC.AgregarParametrosProcedimiento("@docCmpCertificado", DbType.String, 2048, ParameterDirection.Input, docCmpCertificado);
                        ProcBDDC.AgregarParametrosProcedimiento("@docCmpCadena", DbType.String, 2048, ParameterDirection.Input, docCmpCadena);

                        ProcBDDC.AgregarParametrosProcedimiento("@docCmpQr", DbType.Binary, 0, ParameterDirection.Input, base64StringImgQr);

                        ProcBDDC.AgregarParametrosProcedimiento("@docCmpXML", DbType.Binary, 0, ParameterDirection.Input, docCmpXML);
                        //ProcBDDC.AgregarParametrosProcedimiento("@docCmpQr", DbType.Binary, 0, ParameterDirection.Input, base64StringImgQr);

                        try
                        {
                            dsDC = oWS.ObtenerDatasetDesdeProcedimiento(ProcBDDC.ObtenerXmlProcedimiento(), Pag_sConexionLog);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                            throw;
                        }



                    }
                    else
                    {
                        msgErrorsPB += " XML no Generado por Errores -> Envio Cancelado";
                        msgErrorsPB += saltoLinea;

                        //Grabar errores en tabla
                        DataRow rowTmp = dtLogProc.NewRow();
                        rowTmp["docRegId"] = ds.Tables[0].Rows[0]["docRegId"].ToString();
                        rowTmp["docCve"] = ds.Tables[0].Rows[0]["docCve"].ToString() + " - " + ds.Tables[0].Rows[0]["docRegFolio"].ToString();
                        rowTmp["docCmpMsgTxt"] = "Envio Cancelado -> XML no Generado por Errores ";
                        rowTmp["docCmpMsgUrlSit"] = "~/Imagenes/IcoDocProceso/imgSit5.png";
                        dtLogProc.Rows.Add(rowTmp);


                        saveErrCmp(ds.Tables[0].Rows[0]["docRegId"].ToString(), "Envio Cancelado -> XML no Generado por Errores ", Pag_sConexionLog, ds.Tables[0].Rows[0]["docCve"].ToString() + " - " + ds.Tables[0].Rows[0]["docRegFolio"].ToString(), "~/Imagenes/IcoDocProceso/imgSit5.png", docCmpTrnId.ToString());

                        return false;

                    }


                    DataSet ds1 = new DataSet();
                    MGM.ProcedimientoAlmacenado.Procedimiento serProc1 = new MGM.ProcedimientoAlmacenado.Procedimiento();
                    serProc1.NombreProcedimiento = "sp_RptDataSetDoc";
                    serProc1.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 5);
                    serProc1.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                    serProc1.AgregarParametrosProcedimiento("@docCve", DbType.String, 20, ParameterDirection.Input, spDocCve);

                    ds1 = oWS.ObtenerDatasetDesdeProcedimiento(serProc1.ObtenerXmlProcedimiento(), Pag_sConexionLog);

                    //Rellenar otra vez ds con datos de facturacion

                    ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

                    try
                    {
                        /*
                        //getds1
                        string apppathPDF = MapPath("~");
                        //string apppathPDF = "";
                        apppathPDF += "Temp\\";
                        */

                        byte[] bytes = oWSRpt.byte_FormatoDoc(ds1, ds);

                        /*
                        UrlPdf = "MGMPDF" + "-" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() +
                        "-" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + ".pdf";
                        */

                        //UrlsDocs = @"C:\Users\WINDOWS10\Documents\MGMVZam\BrykManagement\BrykManagement\FR\Docs\" + UrlPdf;
                        //UrlsDocs = @apppathPDF + UrlPdf;


                        System.IO.File.WriteAllBytes(urlSavePdf, bytes);


                        String filePDFtoSave = Convert.ToBase64String(bytes);

                        savePDF(Pag_sCompania, Pag_sConexionLog, ds.Tables[0].Rows[0]["docRegId"].ToString(), filePDFtoSave);

                        //Enviar por correo
                        if (chkDocSendStmp(Pag_sCompania, Pag_sConexionLog, ds.Tables[0].Rows[0]["docCve"].ToString()) && xmlPB != "")
                        {
                            sendDocs(ds, urlSaveXml, urlSavePdf, prmCliCveMail, ref dtLogProc, Pag_sConexionLog, docCmpTrnId);
                        }
                        else
                        {
                            delFilesCmp(urlSaveXml);
                            delFilesCmp(urlSavePdf);
                        }


                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.ToString());


                        //Grabar errores en tabla
                        DataRow rowTmp = dtLogProc.NewRow();
                        rowTmp["docRegId"] = ds.Tables[0].Rows[0]["docRegId"].ToString();
                        rowTmp["docCve"] = ds.Tables[0].Rows[0]["docCve"].ToString() + " - " + ds.Tables[0].Rows[0]["docRegFolio"].ToString();
                        rowTmp["docCmpMsgTxt"] = "Error de PDF -> " + ex.ToString();
                        rowTmp["docCmpMsgUrlSit"] = "~/Imagenes/IcoDocProceso/imgSit9.png";
                        dtLogProc.Rows.Add(rowTmp);


                        saveErrCmp(ds.Tables[0].Rows[0]["docRegId"].ToString(), "Error de PDF -> " + ex.ToString(), Pag_sConexionLog, ds.Tables[0].Rows[0]["docCve"].ToString() + " - " + ds.Tables[0].Rows[0]["docRegFolio"].ToString(), "~/Imagenes/IcoDocProceso/imgSit9.png", docCmpTrnId.ToString());



                        return false;
                        throw;
                    }


                }
                catch (WebException wex)
                {

                    if (wex.Response != null)
                    {
                        using (var errorResponse = (HttpWebResponse)wex.Response)
                        {
                            using (var readerStream = new StreamReader(errorResponse.GetResponseStream()))
                            {
                                string error = readerStream.ReadToEnd();

                                //Obtener error del arreglo error400


                                JsonTextReader reader = new JsonTextReader(new StringReader(error));

                                bool getVal = false;

                                while (reader.Read())
                                {
                                    if (reader.Value != null)
                                    {
                                        //Console.WriteLine("Token: {0}, Value: {1}", reader.TokenType, reader.Value);

                                        if (getVal)
                                        {

                                            msgErrorsPB += ds.Tables[0].Rows[0]["docCve"].ToString();
                                            msgErrorsPB += " - ";
                                            msgErrorsPB += ds.Tables[0].Rows[0]["docRegFolio"].ToString();
                                            msgErrorsPB += " (No enviado): ";
                                            msgErrorsPB += reader.Value.ToString();
                                            msgErrorsPB += " || ";
                                            msgErrorsPB += saltoLinea;


                                            //Grabar errores en tabla
                                            DataRow rowTmp = dtLogProc.NewRow();
                                            rowTmp["docRegId"] = ds.Tables[0].Rows[0]["docRegId"].ToString();
                                            rowTmp["docCve"] = ds.Tables[0].Rows[0]["docCve"].ToString() + " - " + ds.Tables[0].Rows[0]["docRegFolio"].ToString();
                                            rowTmp["docCmpMsgTxt"] = "No enviado ->" + reader.Value.ToString();
                                            rowTmp["docCmpMsgUrlSit"] = "~/Imagenes/IcoDocProceso/imgSit9.png";
                                            dtLogProc.Rows.Add(rowTmp);


                                            saveErrCmp(ds.Tables[0].Rows[0]["docRegId"].ToString(), "No enviado ->" + reader.Value.ToString(), Pag_sConexionLog, ds.Tables[0].Rows[0]["docCve"].ToString() + " - " + ds.Tables[0].Rows[0]["docRegFolio"].ToString(), "~/Imagenes/IcoDocProceso/imgSit9.png", docCmpTrnId.ToString());

                                            getVal = false;

                                        }

                                        if (reader.TokenType.ToString() == "PropertyName" && reader.Value.ToString() == "message")
                                        {
                                            getVal = true;
                                        }

                                    }
                                }


                                //MessageBox.Show(error);

                                //TODO: use JSON.net to parse this string and look at the error message
                            }
                        }
                    }

                    return false;
                    //throw;
                }



            }

        }




        logTxt = "-- LOG " + UrlTxt + " ------------";
        logTxt += saltoLinea;

        logTxt += "-- ERRORES ------";
        logTxt += saltoLinea;

        if (msgErrorsPB.Length > 0)
        {
            //ShowAlert("3", "Envio Masivo Finalizado con Errores: " + msgErrorsPB);
            //MessageBox.Show("Envio Masivo Finalizado con Errores: " + msgErrorsPB);
            logTxt += msgErrorsPB;

        }

        logTxt += saltoLinea;
        logTxt += "-- WARNINGS ------";
        logTxt += saltoLinea;

        if (msgWarningsPB.Length > 0)
        {
            logTxt += msgWarningsPB;
            //ShowAlert("2", "Advertencias: " + msgWarningsPB);
            //MessageBox.Show("Envio Masivo Finalizado con Advertencias: " + msgWarningsPB);
        }

        logTxt += saltoLinea;
        logTxt += "-- END -------------";

        writer.WriteLine(logTxt);
        writer.Close();

        delFilesCmp(urlSaveTxt);

        DataTable dtSaveErr = dtLogProc;


        //saveErrCmp(dtSaveErr, Pag_sConexionLog);

        return true;
    }

    private string bldJsonPB(string Pag_sCompania, string Pag_sConexionLog, string tokenUsrPB, DataSet ds)
    {
        string jsonToSend = "";
        string jsonFch = ds.Tables[0].Rows[0]["docRegFecReg"].ToString();

        DateTime fchjsonFch = DateTime.Now;

        jsonFch = fchjsonFch.Year + "-" + fchjsonFch.Month.ToString().PadLeft(2, '0') + "-" + fchjsonFch.Day.ToString().PadLeft(2, '0') + "T";

        jsonFch += fchjsonFch.Hour.ToString().PadLeft(2, '0') + ":" + fchjsonFch.Minute.ToString().PadLeft(2, '0') + ":" + fchjsonFch.Second.ToString().PadLeft(2, '0');

        decimal jsonDesc = Convert.ToDecimal(ds.Tables[0].Rows[0]["docRegTotImpDescDoc"].ToString());
        jsonDesc += Convert.ToDecimal(ds.Tables[0].Rows[0]["docRegTotImpDescPart"].ToString());
        decimal jsonSubTotal = Convert.ToDecimal(ds.Tables[0].Rows[0]["docRegTotImpBrut"].ToString());

        //jsonSubTotal -= jsonDesc;

        decimal jsonTotal = Convert.ToDecimal(ds.Tables[0].Rows[0]["docRegTotImpFact"].ToString());

        decimal jsonImpu = Convert.ToDecimal(ds.Tables[0].Rows[0]["docRegTotImpImpuEsp"].ToString());
        jsonImpu += Convert.ToDecimal(ds.Tables[0].Rows[0]["docRegTotImpImpuDoc"].ToString());


        string jsonTipCmb = getTipCmb(Pag_sCompania, Pag_sConexionLog, ds.Tables[0].Rows[0]["monCve"].ToString(), jsonFch);

        jsonToSend = " { \"api_key\": \"" + Pag_sKeyApi + "\", " +
                              "  \"id_user\": \"" + Pag_sUsrApi + "\", " +
                              "  \"token\": \"" + tokenUsrPB + "\", " +
                              "  \"username\": \"" + Pag_sNamApi + "\", " +
                              "  \"id_test_site\": \"56cf5728784806f72b8b4568\", " +
                              "  \"id_provider\": \"acme\", " +
                              "  \"invoice_data\": { " +
                                    " \"serie\": \"MGM\", " +
                                    " \"folio\": \"" + ds.Tables[0].Rows[0]["docRegFolio"].ToString() + "\", " +
                                    " \"fecha\": \"" + jsonFch + "\", " +
                                    " \"formaDePago\": \"" + ds.Tables[0].Rows[0]["conPagDes"].ToString() + "\", " +
                                    " \"condicionesDePago\": \"Valido\", " +
                                    " \"subTotal\": \"" + jsonSubTotal.ToString() + "\", " +
                                    " \"descuento\":\"" + jsonDesc.ToString() + "\", " +

                                    " \"motivoDescuento\":\"Descuento Promocional\", " +
                                    " \"tipoCambio\":\"1\", " +
                                    " \"moneda\":\"" + ds.Tables[0].Rows[0]["monCve"].ToString() + "\", " +
                                    " \"total\":\"" + jsonTotal.ToString() + "\", " +
                                    " \"tipoDeComprobante\":\"ingreso\", " +
                                    " \"metodoDePago\":\"" + ds.Tables[0].Rows[0]["satMetPagCve"].ToString() + "\", " +
                                    " \"lugarExpedicion\":\"" + ds.Tables[0].Rows[0]["docEncLugarExp"].ToString() + "\", " +
                                    " \"numCtaPago\":\"" + ds.Tables[0].Rows[0]["clieDatCatNoCta"].ToString() + "\", " +
                                    " \"emisor\": { " +
                                        " \"nombre\":\"" + ds.Tables[0].Rows[0]["ciaRSoc"].ToString() + "\", " +
                                        " \"rfc\":\"AAA010101AAA\", " +
                                        " \"domicilioFiscal\":{ " +
                                            " \"calle\":\"" + ds.Tables[0].Rows[0]["ciaDomCalleNo"].ToString() + "\", " +
                                            " \"municipio\":\"" + ds.Tables[0].Rows[0]["ciaCiudad"].ToString() + "\", " +
                                            " \"estado\":\"" + ds.Tables[0].Rows[0]["ciaEstado"].ToString() + "\", " +
                                            " \"pais\":\"" + ds.Tables[0].Rows[0]["ciaPais"].ToString() + "\"," +
                                            " \"codigoPostal\":\"" + ds.Tables[0].Rows[0]["ciaCodigoPostal"].ToString() + "\" " +
                                        " }, " +
                                        " \"expedidoEn\":{ " +
                                            " \"calle\":\"" + ds.Tables[0].Rows[0]["docEncLugarExp"].ToString() + "\", " +
                                            " \"municipio\":\"" + ds.Tables[0].Rows[0]["docEncLugarExp"].ToString() + "\", " +
                                            " \"estado\":\"" + ds.Tables[0].Rows[0]["docEncLugarExp"].ToString() + "\", " +
                                            " \"pais\":\"" + ds.Tables[0].Rows[0]["docEncLugarExp"].ToString() + "\", " +
                                            " \"codigoPostal\":\"" + ds.Tables[0].Rows[0]["docEncLugarExp"].ToString() + "\"" +
                                        " }, " +
                                        " \"regimenFiscal\":[{\"regimen\":\"" + Pag_sRegFisc + "\"}] " +
                                    " }, " +

                                    " \"receptor\":{ " +
                                        " \"rfc\":\"" + ds.Tables[0].Rows[0]["clieRFC"].ToString() + "\", " +
                                        " \"nombre\": \"" + ds.Tables[0].Rows[0]["clieNom"].ToString() + "\", " +
                                        " \"domicilio\":{ " +
                                            " \"calle\":\"" + ds.Tables[0].Rows[0]["clieDomCalleNo"].ToString() + "\", " +
                                            " \"municipio\":\"" + ds.Tables[0].Rows[0]["clieCiudad"].ToString() + "\", " +
                                            " \"estado\":\"" + ds.Tables[0].Rows[0]["clieEstado"].ToString() + "\", " +
                                            " \"pais\": \"" + ds.Tables[0].Rows[0]["cliePais"].ToString() + "\", " +
                                            " \"codigoPostal\":\"" + ds.Tables[0].Rows[0]["clieCodigoPostal"].ToString() + "\" " +
                                        " } " +
                                    " }, " +

                                    " \"conceptos\": [ ";

        int countCpts = 1;

        foreach (DataRow rowJS in ds.Tables[0].Rows)
        {


            jsonToSend += " { " +
                            " \"cantidad\": \"" + rowJS["docRegPartCant"].ToString() + "\", " +
                            " \"unidad\": \"" + rowJS["uniMedCve"].ToString() + "\", " +
                            " \"descripcion\": \"" + rowJS["artDes"].ToString() + "\", " +
                            " \"valorUnitario\": \"" + rowJS["docRegPartPrec"].ToString() + "\", " +
                            " \"importe\": \"" + rowJS["docRegPartImpBrut"].ToString() + "\", " +
                            " \"noIdentificacion\": \"" + rowJS["artCve"].ToString() + "\" " +
                        " } ";

            if (countCpts != ds.Tables[0].Rows.Count)
            {
                jsonToSend += " ,";
            }
            countCpts++;
        }






        jsonToSend += " ], " +


        " \"impuestos\": { " +
            " \"totalImpuestosRetenidos\":\"00.00\", " +
            " \"totalImpuestosTrasladados\":\"" + jsonImpu.ToString() + "\", " +
            " \"retenciones\":[ " +
                " { " +
                    " \"impuesto\":\"ISR\", " +
                    " \"importe\":\"00.00\" " +
                " } " +
            " ], " +
            " \"traslados\":[ ";


        jsonToSend += bldJsonImpuPB(Pag_sCompania, Pag_sConexionLog, ds);

        jsonToSend += " ] " +
      " } " +
  " } " +
" } ";

        return jsonToSend;
    }

    private string bldJsonImpuPB(string Pag_sCompania, string Pag_sConexionLog, DataSet dsDatos)
    {
        string rstrImpuGlb = "";

        string prmDocCve = dsDatos.Tables[0].Rows[0]["docCve"].ToString();

        decimal impBase = Convert.ToDecimal(dsDatos.Tables[0].Rows[0]["docRegTotImpBrut"].ToString());
        decimal impDescPart = Convert.ToDecimal(dsDatos.Tables[0].Rows[0]["docRegTotImpDescPart"].ToString());
        decimal impDescGlb = Convert.ToDecimal(dsDatos.Tables[0].Rows[0]["docRegTotImpDescDoc"].ToString());

        impBase -= impDescPart;
        impBase -= impDescGlb;

        //Añadir impuestos globales
        DataSet dsImpu = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_DocumentoImpuestos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 20, ParameterDirection.Input, prmDocCve);
        dsImpu = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        int counterOne = 0;
        int counterTwo = 0;

        if (dsImpu.Tables.Count > 0)
        {

            if (dsImpu.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow rowImpu in dsImpu.Tables[0].Rows)
                {

                    string lblTasa;
                    decimal decTasa;

                    lblTasa = rowImpu["impAbr"].ToString();
                    decTasa = Convert.ToDecimal(rowImpu["impTas"].ToString());

                    impBase *= decTasa;
                    impBase /= 100;

                    if (counterOne != 0 && counterOne < dsImpu.Tables[0].Rows.Count)
                    {
                        rstrImpuGlb += " , ";
                    }

                    rstrImpuGlb += " { " +
                                " \"impuesto\":\"" + lblTasa + "\", " +
                                " \"tasa\":\"" + decTasa.ToString() + "\", " +
                                " \"importe\":\"" + impBase.ToString("0.##") + "\" " +
                            " } ";

                    counterOne++;

                }

            }



        }


        if (dsImpu.Tables[0].Rows.Count > 0 && dsDatos.Tables[0].Rows.Count > 0)
        {
            rstrImpuGlb += " , ";
        }


        if (dsDatos.Tables[0].Rows.Count > 0)
        {

            foreach (DataRow rowImpuPart in dsDatos.Tables[0].Rows)
            {

                if (counterTwo != 0 && counterTwo < dsDatos.Tables[0].Rows.Count)
                {
                    rstrImpuGlb += " , ";
                }

                rstrImpuGlb +=

                        " { " +
                            " \"impuesto\":\"IEPS\", " +
                            " \"tasa\":\"10\", " +
                            " \"importe\":\"" + rowImpuPart["docRegPartImpBrut"].ToString() + "\" " +
                        " } ";

                counterTwo++;
            }

        }

        //Añadir impuestos Partidas****



        return rstrImpuGlb;

    }


    private bool chkDocReadyStmp(string Pag_sCompania, string Pag_sConexionLog, string docRegId)
    {
        bool response = true;

        try
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_DocumentoComprobantes";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@docRegId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(docRegId));

            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    response = false;
                }
                else
                {
                    response = true;
                }
            }
            else
            {
                response = true;
            }


        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());
            response = true;
            throw;
        }

        return response;
    }



    private static string getTokenPB(string apiKeyPB, string usrPB)
    {
        string tokenPB = "";

        string stringPB = "";

        string urlPB = "https://sync.paybook.com/v1/sessions?api_key=";
        urlPB += apiKeyPB;

        var httpWebRequest = (HttpWebRequest)WebRequest.Create(urlPB);
        httpWebRequest.ContentType = "text/json";
        httpWebRequest.Method = "POST";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            string json = "{ \"api_key\": \"" + apiKeyPB + "\", " +
                              "  \"id_user\": \"" + usrPB + "\" " +
                              "}";

            streamWriter.Write(json);
        }

        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var responseText = streamReader.ReadToEnd();
            //Now you have your response.
            //or false depending on information in the response

            JsonTextReader reader = new JsonTextReader(new StringReader(responseText));

            string token = "";
            bool getVal = false;

            while (reader.Read())
            {
                if (reader.Value != null)
                {
                    //Console.WriteLine("Token: {0}, Value: {1}", reader.TokenType, reader.Value);
                    //MessageBox.Show(System.String.Format( "Token: {0}, Value: {1}", reader.TokenType, reader.Value));

                    stringPB += System.String.Format("Token: {0}, Value: {1}", reader.TokenType, reader.Value);

                    if (getVal)
                    {
                        tokenPB = reader.Value.ToString();
                        getVal = false;
                    }

                    if (reader.TokenType.ToString() == "PropertyName" && reader.Value.ToString() == "token")
                    {
                        getVal = true;
                    }




                    stringPB += "\n";

                }
                else
                {
                    //Console.WriteLine("Token: {0}", reader.TokenType);
                    //MessageBox.Show(reader.TokenType.ToString());
                    stringPB += reader.TokenType.ToString();
                    stringPB += "\n";

                }

            }

            //MessageBox.Show(tokenPB);

            return tokenPB;
        }

    }


    private void Valores_PB(string Pag_sCompania, string Pag_sConexionLog)
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_parametros";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@parmCve", DbType.String, 10, ParameterDirection.Input, "PAYBOOK");
        ProcBD.AgregarParametrosProcedimiento("@parmSec", DbType.Int64, 0, ParameterDirection.Input, 1);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                Pag_sUrlApi = ds.Tables[0].Rows[0]["parmValStr"].ToString();
            }
        }

        DataSet dsI = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBDI = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBDI.NombreProcedimiento = "sp_parametros";
        ProcBDI.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
        ProcBDI.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBDI.AgregarParametrosProcedimiento("@parmCve", DbType.String, 10, ParameterDirection.Input, "PAYBOOK");
        ProcBDI.AgregarParametrosProcedimiento("@parmSec", DbType.Int64, 0, ParameterDirection.Input, 2);

        dsI = oWS.ObtenerDatasetDesdeProcedimiento(ProcBDI.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (dsI.Tables.Count > 0)
        {
            if (dsI.Tables[0].Rows.Count > 0)
            {
                Pag_sKeyApi = dsI.Tables[0].Rows[0]["parmValStr"].ToString();
            }
        }

        DataSet dsP = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBDP = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBDP.NombreProcedimiento = "sp_parametros";
        ProcBDP.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
        ProcBDP.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBDP.AgregarParametrosProcedimiento("@parmCve", DbType.String, 10, ParameterDirection.Input, "PAYBOOK");

        ProcBDP.AgregarParametrosProcedimiento("@parmSec", DbType.Int64, 0, ParameterDirection.Input, 3);

        dsP = oWS.ObtenerDatasetDesdeProcedimiento(ProcBDP.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (dsP.Tables.Count > 0)
        {
            if (dsP.Tables[0].Rows.Count > 0)
            {
                Pag_sUsrApi = dsP.Tables[0].Rows[0]["parmValStr"].ToString();
            }
        }


        DataSet dsN = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBDN = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBDN.NombreProcedimiento = "sp_parametros";
        ProcBDN.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
        ProcBDN.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBDN.AgregarParametrosProcedimiento("@parmCve", DbType.String, 10, ParameterDirection.Input, "PAYBOOK");
        ProcBDN.AgregarParametrosProcedimiento("@parmSec", DbType.Int64, 0, ParameterDirection.Input, 4);

        dsN = oWS.ObtenerDatasetDesdeProcedimiento(ProcBDN.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (dsN.Tables.Count > 0)
        {
            if (dsN.Tables[0].Rows.Count > 0)
            {
                Pag_sNamApi = dsP.Tables[0].Rows[0]["parmValStr"].ToString();
            }
        }


    }



    private void savePDF(string Pag_sCompania, string Pag_sConexionLog, string docRegId, string docCmpPDF)
    {

        try
        {


            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_DocumentoComprobantes";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 2);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@docRegId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(docRegId));
            ProcBD.AgregarParametrosProcedimiento("@docCmpPDF", DbType.Binary, 0, ParameterDirection.Input, docCmpPDF);
            //ProcBD.AgregarParametrosProcedimiento("@docRegId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(docRegId));

            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (true)
            {

            }
        }
        catch (Exception)
        {

            throw;
        }



    }


    private bool chkDocSendStmp(string Pag_sCompania, string Pag_sConexionLog, string docCve)
    {
        bool response = false;

        try
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_DocumentoGeneraCFDI";
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, docCve);

            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    if (ds.Tables[0].Rows[0]["docGenCFDIMail"].ToString() == "1")
                    {
                        valMailCopyServer = ds.Tables[0].Rows[0]["docGenCFDICorreo"].ToString();
                        response = true;
                    }
                    else
                    {
                        valMailCopyServer = "";
                        response = false;
                    }

                }
                else
                {
                    valMailCopyServer = "";
                    response = false;
                }
            }
            else
            {
                valMailCopyServer = "";
                response = false;
            }


        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());
            response = false;
            throw;
        }

        return response;
    }



    private void sendDocs(DataSet ds, string fileName, string UrlsDocs, string prmCliCve, ref DataTable dtLogProc, string Pag_sConexionLog, int docCmpTrnId)
    {

        if (prmCliCve != "")
        {
            try
            {
                // Command line argument must the the SMTP host.
                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.Host = valSmtpServer;
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;

                client.Credentials = new System.Net.NetworkCredential(valMailServer, valPswServer);

                string mailToMM = prmCliCve;


                MailMessage mm = new MailMessage(valMailServer, mailToMM, "Envio MGM", "Reportes Enviados por Managment");

                //Copia

                if (valMailCopyServer != "")
                {
                    MailAddress copy = new MailAddress(valMailCopyServer);
                    mm.CC.Add(copy);
                }



                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                System.Net.Mail.Attachment attachment;
                //Agrega PDF

                MemoryStream ms = new MemoryStream();
                using (Stream input = File.OpenRead(fileName))
                {
                    input.CopyTo(ms);
                }
                ms.Position = 0;
                mm.Attachments.Add(new Attachment(ms, new FileInfo(fileName).Name));

                MemoryStream msPDF = new MemoryStream();
                using (Stream input = File.OpenRead(UrlsDocs))
                {
                    input.CopyTo(msPDF);
                }
                msPDF.Position = 0;
                mm.Attachments.Add(new Attachment(msPDF, new FileInfo(UrlsDocs).Name));


                //attachment = new System.Net.Mail.Attachment(UrlsDocs);
                //mm.Attachments.Add(attachment);
                //Agregar XML
                //attachment = new System.Net.Mail.Attachment(fileName);
                //mm.Attachments.Add(attachment);
                //send

                client.Send(mm);

                //ms.Close();
                //fileStrmPDF.Close();
                //msPDF.Close();


                //Grabar errores en tabla
                DataRow rowTmp = dtLogProc.NewRow();
                rowTmp["docRegId"] = ds.Tables[0].Rows[0]["docRegId"].ToString();
                rowTmp["docCve"] = ds.Tables[0].Rows[0]["docCve"].ToString() + " - " + ds.Tables[0].Rows[0]["docRegFolio"].ToString();
                rowTmp["docCmpMsgTxt"] = "Enviado";
                rowTmp["docCmpMsgUrlSit"] = "~/Imagenes/IcoDocProceso/imgSit2.png";
                dtLogProc.Rows.Add(rowTmp);

                saveErrCmp(ds.Tables[0].Rows[0]["docRegId"].ToString(), "Enviado", Pag_sConexionLog, ds.Tables[0].Rows[0]["docCve"].ToString() + " - " + ds.Tables[0].Rows[0]["docRegFolio"].ToString(), "~/Imagenes/IcoDocProceso/imgSit2.png", docCmpTrnId.ToString());


                //Borrar xml y pdf de Directorio o Guardar

                delFilesCmp(fileName);
                delFilesCmp(UrlsDocs);

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());

                //Grabar errores en tabla
                DataRow rowTmp = dtLogProc.NewRow();
                rowTmp["docRegId"] = ds.Tables[0].Rows[0]["docRegId"].ToString();
                rowTmp["docCve"] = ds.Tables[0].Rows[0]["docCve"].ToString() + " - " + ds.Tables[0].Rows[0]["docRegFolio"].ToString();
                rowTmp["docCmpMsgTxt"] = "Error de Envio -> " + ex.Message.ToString();
                rowTmp["docCmpMsgUrlSit"] = "~/Imagenes/IcoDocProceso/imgSit9.png";
                dtLogProc.Rows.Add(rowTmp);


                saveErrCmp(ds.Tables[0].Rows[0]["docRegId"].ToString(), "Error de Envio -> " + ex.Message.ToString(), Pag_sConexionLog, ds.Tables[0].Rows[0]["docCve"].ToString() + " - " + ds.Tables[0].Rows[0]["docRegFolio"].ToString(), "~/Imagenes/IcoDocProceso/imgSit9.png", docCmpTrnId.ToString());

                delFilesCmp(fileName);
                delFilesCmp(UrlsDocs);

                throw;
            }

        }
        else
        {

            //Grabar errores en tabla
            DataRow rowTmp = dtLogProc.NewRow();
            rowTmp["docRegId"] = ds.Tables[0].Rows[0]["docRegId"].ToString();
            rowTmp["docCve"] = ds.Tables[0].Rows[0]["docCve"].ToString() + " - " + ds.Tables[0].Rows[0]["docRegFolio"].ToString();
            rowTmp["docCmpMsgTxt"] = "Error de Envio -> Correo no Definido en Contacto Cliente";
            rowTmp["docCmpMsgUrlSit"] = "~/Imagenes/IcoDocProceso/imgSit9.png";
            dtLogProc.Rows.Add(rowTmp);


            saveErrCmp(ds.Tables[0].Rows[0]["docRegId"].ToString(), "Error de Envio -> Correo no Definido en Contacto Cliente", Pag_sConexionLog, ds.Tables[0].Rows[0]["docCve"].ToString() + " - " + ds.Tables[0].Rows[0]["docRegFolio"].ToString(), "~/Imagenes/IcoDocProceso/imgSit9.png", docCmpTrnId.ToString());


        }

    }



    private void Valores_Com(string Pag_sCompania, string Pag_sConexionLog)
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_parametros";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@parmCve", DbType.String, 10, ParameterDirection.Input, "TIPOCAMBIO");
        ProcBD.AgregarParametrosProcedimiento("@parmSec", DbType.Int64, 0, ParameterDirection.Input, 1);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                Pag_sMonDef = ds.Tables[0].Rows[0]["parmValStr"].ToString();
            }
        }

    }


    private void Valores_ComSAT(string Pag_sCompania, string Pag_sConexionLog)
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_parametros";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@parmCve", DbType.String, 10, ParameterDirection.Input, "SATCFDI");
        ProcBD.AgregarParametrosProcedimiento("@parmSec", DbType.Int64, 0, ParameterDirection.Input, 2);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                Pag_sRegFisc = ds.Tables[0].Rows[0]["parmValStr"].ToString();
            }
        }

    }



    private string getTipCmb(string Pag_sCompania, string Pag_sConexionLog, string monCve, string docRegFec)
    {
        string response = "";

        if (monCve == Pag_sMonDef)
        {
            response = "1";
        }
        else
        {
            try
            {

                DataSet ds = new DataSet();
                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                ProcBD.NombreProcedimiento = "sp_Monedas";
                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
                ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                ProcBD.AgregarParametrosProcedimiento("@monCve", DbType.String, 2, ParameterDirection.Input, monCve);
                ProcBD.AgregarParametrosProcedimiento("@monTCFec", DbType.String, 100, ParameterDirection.Input, docRegFec);
                //ProcBD.AgregarParametrosProcedimiento("@docRegId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(docRegId));

                ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        response = ds.Tables[0].Rows[0]["monTCC"].ToString();
                    }
                    else
                    {
                        response = "1";
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                response = "1";
                throw;
            }


        }


        return response;
    }


    private void delFilesCmp(string pathToDel)
    {

        try
        {
            if (pathToDel != "")
            {
                File.Delete(pathToDel);
            }


        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());
            throw;
        }


    }



    private void getValServer(string Pag_sCompania, string Pag_sConexionLog)
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_parametros";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@parmCve", DbType.String, 10, ParameterDirection.Input, "ENVDOC");
        ProcBD.AgregarParametrosProcedimiento("@parmSec", DbType.Int64, 0, ParameterDirection.Input, 1);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                valSmtpServer = ds.Tables[0].Rows[0]["parmValStr"].ToString();
            }
        }

        DataSet dsI = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBDI = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBDI.NombreProcedimiento = "sp_parametros";
        ProcBDI.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
        ProcBDI.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBDI.AgregarParametrosProcedimiento("@parmCve", DbType.String, 10, ParameterDirection.Input, "ENVDOC");

        ProcBDI.AgregarParametrosProcedimiento("@parmSec", DbType.Int64, 0, ParameterDirection.Input, 2);

        dsI = oWS.ObtenerDatasetDesdeProcedimiento(ProcBDI.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (dsI.Tables.Count > 0)
        {
            if (dsI.Tables[0].Rows.Count > 0)
            {
                valMailServer = dsI.Tables[0].Rows[0]["parmValStr"].ToString();

            }
        }

        DataSet dsP = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBDP = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBDP.NombreProcedimiento = "sp_parametros";
        ProcBDP.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
        ProcBDP.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBDP.AgregarParametrosProcedimiento("@parmCve", DbType.String, 10, ParameterDirection.Input, "ENVDOC");

        ProcBDP.AgregarParametrosProcedimiento("@parmSec", DbType.Int64, 0, ParameterDirection.Input, 3);

        dsP = oWS.ObtenerDatasetDesdeProcedimiento(ProcBDP.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (dsP.Tables.Count > 0)
        {
            if (dsP.Tables[0].Rows.Count > 0)
            {
                valPswServer = dsP.Tables[0].Rows[0]["parmValStr"].ToString();
            }
        }

    }


    private void saveErrCmp(string docRegId, string docCmpErrMsg, string Pag_sConexionLog, string docCve, string docCmpMsgUrlSit, string docCmpTrnId)
    {

        try
        {

            DataSet dsP = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();

            ProcBD.NombreProcedimiento = "sp_DocumentoComprobanteMensajes";

            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
            ProcBD.AgregarParametrosProcedimiento("@docRegId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(docRegId));
            ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 1024, ParameterDirection.Input, docCve);
            ProcBD.AgregarParametrosProcedimiento("@docCmpMsgUrlSit", DbType.String, 2056, ParameterDirection.Input, docCmpMsgUrlSit);
            ProcBD.AgregarParametrosProcedimiento("@docCmpTrnId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(docCmpTrnId));
            ProcBD.AgregarParametrosProcedimiento("@docCmpMsgTxt", DbType.String, 2056, ParameterDirection.Input, docCmpErrMsg);

            dsP = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (dsP.Tables.Count > 0)
            {

            }


        }
        catch (Exception)
        {

            throw;
        }

    }


    private void docProcess(string Pag_sConexionLog, string Pag_sCompania, Int64 idocRegid, Int64 idocRegSit, string maUser, string sdocCve, int docCmpTrnId, string docRegFolio)
    {
        try
        {

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_EXPRODoc";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 2);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@docRegId", DbType.Int64, 0, ParameterDirection.Input, idocRegid);
            ProcBD.AgregarParametrosProcedimiento("@accId", DbType.Int64, 0, ParameterDirection.Input, 5);
            ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, sdocCve);
            ProcBD.AgregarParametrosProcedimiento("@docRegSitId", DbType.Int64, 0, ParameterDirection.Input, idocRegSit);
            ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, maUser);
            ProcBD.AgregarParametrosProcedimiento("@MuestraMgs", DbType.Int64, 0, ParameterDirection.Input, 1);

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);


            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string urlToSaveErr = "";

                        if (ds.Tables[0].Rows[0]["maMSGTip"].ToString().Trim() == "1")
                        {
                            urlToSaveErr = "~/Imagenes/IcoDocProceso/imgSit2.png";
                        }else
                        {
                            urlToSaveErr = "~/Imagenes/IcoDocProceso/imgSit9.png";
                        }
                        
                        saveErrCmp(idocRegid.ToString(), ds.Tables[0].Rows[0]["maMSGDes"].ToString(), Pag_sConexionLog, sdocCve + " - " + docRegFolio, urlToSaveErr, docCmpTrnId.ToString());
                    }
                }
            }


        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());
            throw;
        }

    }


}