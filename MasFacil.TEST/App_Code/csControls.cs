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
/// Summary description for csControls
/// </summary>
namespace MGMControls
{

    public class FillIn
    {
        ws.Servicio oWS = new ws.Servicio();
        MGMFnGrales.FNDatos  FNDatos = new MGMFnGrales.FNDatos();
        MGMFnGrales.ObjAdoNet FNAdoNet = new MGMFnGrales.ObjAdoNet();
        MGMFnGrales.FNPeriodosCalendario FNPeriodo = new MGMFnGrales.FNPeriodosCalendario();

    //MASH
        public bool RadComboBox_FormaPago(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            ds = FNDatos.dsCatalogos_2(sPag_sConexionLog, "tb_SATFormaPago", "satFormaPagCve", "satFormaPagDes", sCiaCve);
            RadComboBox(ref objRadComboBox, ds, "Clave", "Descripcion", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }
        //MASH

        public bool RabComboBox_Incoterm(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            ds = FNDatos.dsCatalogos(sPag_sConexionLog, "tb_Incoterms", "incoCve", "incoDes", sCiaCve);
            RadComboBox(ref objRadComboBox, ds, "Clave", "Descripcion", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }

        //Store Procedure
        public bool RadComboBox(ref RadComboBox objRadComboBox, DataSet ds, string sDataValueField, string sDataTextField, bool Filtro, bool selected,  string SelectedValue ="")
        {
            if (Filtro == true)
            { objRadComboBox.Filter = RadComboBoxFilter.Contains; }

            if (FNAdoNet.bDSIsFill(ds))
            {

                objRadComboBox.EmptyMessage = "Seleccionar";
                objRadComboBox.DataTextField = sDataTextField;
                objRadComboBox.DataValueField = sDataValueField;
                objRadComboBox.DataSource = ds.Tables[0];
                objRadComboBox.DataBind();
                objRadComboBox.ClearSelection();
                objRadComboBox.Text = string.Empty;
 
                if (selected == true)
                {
                    if (SelectedValue == "")
                    {
                        objRadComboBox.SelectedIndex = 0;
                    }
                    else
                    {
                        objRadComboBox.SelectedValue = SelectedValue;
                    }

                }
                return true;
            }
            else {
                return false;
            }

            



            return true;
        }

        //Se agrega metodo para llenado de datos para combo Beneficiario

        public bool RadComboBoxBeneficiario(ref RadComboBox objRadComboBox, DataSet ds,string sDataValueField, string sDataTextField, bool Filtro, bool selected, string SelectedValue = "")
        {
            if (Filtro == true)
            { objRadComboBox.Filter = RadComboBoxFilter.Contains; }

            if (FNAdoNet.bDSIsFill(ds))
            {

                objRadComboBox.EmptyMessage = "Seleccionar";
                objRadComboBox.DataTextField = sDataTextField;
                objRadComboBox.DataValueField = sDataValueField;
                //objRadComboBox.DataTextField = sDataValueField; 
                //objRadComboBox.DataValueField = sDataTextField;
                objRadComboBox.DataSource = ds.Tables[0];
                objRadComboBox.DataBind();
                objRadComboBox.ClearSelection();
                objRadComboBox.Text = string.Empty;

                if (selected == true)
                {
                    if (SelectedValue == "")
                    {
                        objRadComboBox.SelectedIndex = 0;
                    }
                    else
                    {
                        objRadComboBox.SelectedValue = SelectedValue;
                    }

                }
                return true;
            }
            else
            {
                return false;
            }
            
            return true;
        }

        //
        public bool RadComboBox_Paises(string sPag_sConexionLog, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_paises";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.String, 2, ParameterDirection.Input, 51);
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);
            RadComboBox(ref objRadComboBox, ds, "paisCve", "paisDes", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }

        public bool RadComboBox_EntidadesFed(string sPag_sConexionLog, string sPaisId, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_EntidadesFed";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
            ProcBD.AgregarParametrosProcedimiento("@paisCve", DbType.String, 2, ParameterDirection.Input, sPaisId);
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);
            RadComboBox(ref objRadComboBox, ds, "entFCve", "entFDes", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }

        public bool RadComboBox_Provincias(string sPag_sConexionLog, string sPaisId, string sEntidadFedId, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_Provincias";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
            ProcBD.AgregarParametrosProcedimiento("@paisCve", DbType.String, 2, ParameterDirection.Input, sPaisId);
            ProcBD.AgregarParametrosProcedimiento("@entFCve", DbType.String, 2, ParameterDirection.Input, sEntidadFedId);
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);
            RadComboBox(ref objRadComboBox, ds, "provCve", "provDes", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }

        public bool RadComboBox_Trabajadores(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "", int estatus = 0)
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            int opc = 50;
            opc += estatus;
            ProcBD.NombreProcedimiento = "sp_empleados";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, opc);
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);
            RadComboBox(ref objRadComboBox, ds, "empleado", "nombre", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }

        public bool RadComboBox_Foliadores(string sPag_sConexionLog, string sCiaCve, Int64  TipoFoliador, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            int opc = 50;
            ProcBD.NombreProcedimiento = "sp_Foliadores";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, opc);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
            ProcBD.AgregarParametrosProcedimiento("@tipoFol", DbType.Int64, 0, ParameterDirection.Input, TipoFoliador);
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);
            RadComboBox(ref objRadComboBox, ds, "FolioCve", "FolioDescripcion", Filtro, selected, SelectedValue);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);

 
            if (TipoFoliador == 2) {

            foreach (RadComboBoxItem item in objRadComboBox.Items)
            {
                DataRow[] foundRows;
                foundRows = ds.Tables[0].Select("folioSit = 1 and  FolioCve = '" + item.Value.ToString() + "' ");

                    if (SelectedValue != item.Value.ToString()) {

                  
                        if (foundRows.Length > 0) {
                            item.Enabled = false;
                        }
                    }



                }

            }




            return true;
        }
        public bool RadComboBox_FoliadoresSinDeshabilitar(string sPag_sConexionLog, string sCiaCve, Int64 TipoFoliador, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            int opc = 50;
            ProcBD.NombreProcedimiento = "sp_Foliadores";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, opc);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
            ProcBD.AgregarParametrosProcedimiento("@tipoFol", DbType.Int64, 0, ParameterDirection.Input, TipoFoliador);
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);
            RadComboBox(ref objRadComboBox, ds, "FolioCve", "FolioDescripcion", Filtro, selected, SelectedValue);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }


        public bool RadComboBox_FormatosImpresion(string sPag_sConexionLog, string sCiaCve, Int64 TipoFormato, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_FormatosImpresion";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
            ProcBD.AgregarParametrosProcedimiento("@formImpTip", DbType.Int64, 0, ParameterDirection.Input, TipoFormato);
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);

            RadComboBox(ref objRadComboBox, ds, "formImpCve", "formImpDes", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }

        public bool RadComboBox_SituacionDocumento(string sPag_sConexionLog, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_DocumentoProcesos";
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);

            RadComboBox(ref objRadComboBox, ds, "docProcID", "docProcDes", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;

            //DataSet ds = new DataSet();
            //ds = FNDatos.dsCatalogos(sPag_sConexionLog, "tb_DocumentoProcesos", "docProcID", "docProcDes", sCiaCve);
            //RadComboBox(ref objRadComboBox, ds, "Clave", "Descripcion", Filtro, selected);
            //((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            //return true;


        }

        public bool RadComboBox_Situaciones(string sPag_sConexionLog, string sCiaCve, int Opc, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {


            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_Situacion";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, Opc);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);

            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);

            RadComboBox(ref objRadComboBox, ds, "Clave", "Descripcion", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
         }
        //Se agrega metodo para llenar combo de descripcion
        public bool RadComboBox_Descripcion(string sPag_sConexionLog, string sCiaCve, int Opc, String tipoConcepto, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_RegistroOperaciones";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, Opc);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
            ProcBD.AgregarParametrosProcedimiento("@contRefTipCve", DbType.String, 2, ParameterDirection.Input, tipoConcepto);

            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);

            RadComboBox(ref objRadComboBox, ds, "transFolio", "transDes", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }
        //

        public bool RadComboBox_Sucursales(string sPag_sConexionLog, string sCiaCve, int Opc, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_Sucursales";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, Opc);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);

            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);

            RadComboBox(ref objRadComboBox, ds, "sucCve", "sucDes", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }


        public bool RabComboBox_UsuariosSegDoc(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_MAUsuarios";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);

            RadComboBox(ref objRadComboBox, ds, "maUsuCve", "maUsuNom", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }

        public bool RabComboBox_DocumentosSegDoc(string sPag_sConexionLog, string sCiaCve, string maUsuCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_SeguridadDocumentos";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 54);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
            ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, maUsuCve);

            
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);

            RadComboBox(ref objRadComboBox, ds, "docCve", "docDes", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        
        }

        public bool RabComboBox_Modulo_IN(string sPag_sConexionLog, string sPag_sComp, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ConceptoDefinicion";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 60);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sPag_sComp);

            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);

            RadComboBox(ref objRadComboBox, ds, "cptoId", "cptoDes", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;

        }

        public bool RabComboBox_Modulo_CXC(string sPag_sConexionLog, string sPag_sComp, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ConceptoDefinicion";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 61);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sPag_sComp);

            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);

            RadComboBox(ref objRadComboBox, ds, "cptoId", "cptoDes", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;

        }

        public bool RabComboBox_Modulo_CG(string sPag_sConexionLog, string sPag_sComp, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ConceptoDefinicion";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 62);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sPag_sComp);

            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);

            RadComboBox(ref objRadComboBox, ds, "cptoId", "cptoDes", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;

        }

        public bool RabComboBox_ValorDic(string sPag_sConexionLog, string sCiaCve, string listTipDatoCptoCve,int cptoId, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_TransaccionesDiccionarioCptoTipDato";
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
            ProcBD.AgregarParametrosProcedimiento("@listTipDatoCptoCve", DbType.String, 10, ParameterDirection.Input, listTipDatoCptoCve);
            ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, cptoId);

            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);

            RadComboBox(ref objRadComboBox, ds, "Clave", "Descripcion", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;


            //DataSet ds = new DataSet();
            //ds = FNDatos.dsCatalogos(sPag_sConexionLog, "tb_TransaccionesDiccionario", "transDicCve", "transDicDes", sCiaCve);
            //RadComboBox(ref objRadComboBox, ds, "Clave", "Descripcion", Filtro, selected);
            //((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            //return true;
        }
        
        public bool RadComboBox_MapeoConfiguracion(string sPag_sConexionLog, string sCiaCve, string cptoid, string listTipDatoCptoCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ConceptoConfiguracion";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 56);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
            ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, cptoid);
            ProcBD.AgregarParametrosProcedimiento("@listTipDatoCptoCve", DbType.String, 10, ParameterDirection.Input, listTipDatoCptoCve);

            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);

            RadComboBox(ref objRadComboBox, ds, "cptoConfId", "cptoConfDes", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }
        public bool RabComboBox_ConceptoReferenciaTipo(string sPag_sConexionLog, string sCiaCve, string listTipDatoCptoCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_RegistroOperaciones";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 100);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
            ProcBD.AgregarParametrosProcedimiento("@contRefTipCve", DbType.String, 2, ParameterDirection.Input, listTipDatoCptoCve);

            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);

            RadComboBox(ref objRadComboBox, ds, "cptoId", "cptoDes", true, false, "");
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }

        public bool RabComboBox_ConceptoReferenciaTipo_SegUsuario(string sPag_sConexionLog, string sCiaCve, int segCptoAut, string maUsuCve, string contRefTipCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "", int cptoTip = 0)
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_RegistroOperaciones";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 101);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
            ProcBD.AgregarParametrosProcedimiento("@segCptoAut", DbType.Int64, 0, ParameterDirection.Input, segCptoAut);
            ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, maUsuCve);
            ProcBD.AgregarParametrosProcedimiento("@contRefTipCve", DbType.String, 2, ParameterDirection.Input, contRefTipCve);

            //Se comenta if para que filtre por concepto crea
            //if (cptoTip > 0) {
            //ProcBD.AgregarParametrosProcedimiento("@cptoTip", DbType.Int64, 0, ParameterDirection.Input, cptoTip);
            //}
            ProcBD.AgregarParametrosProcedimiento("@cptoTip", DbType.Int64, 0, ParameterDirection.Input, 1);

            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);

            RadComboBox(ref objRadComboBox, ds, "cptoId", "cptoDes", Filtro, selected, SelectedValue);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }

        //GRET,se agregan metodos para llenado de combo, Prefijo,A.Contable,Descripcion,Concepto

        public bool RabComboBox_Prefijo(string sPag_sConexionLog, string Pag_sCompania, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ACUMAsientoContable";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 55);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);

            RadComboBox(ref objRadComboBox, ds, "PrefCpto", "PrefCpto", Filtro, selected, SelectedValue);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }
        public bool RabComboBox_AContable(string sPag_sConexionLog, string Pag_sCompania, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ACUMAsientoContable";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);

            RadComboBox(ref objRadComboBox, ds, "PolCve", "PolCve", Filtro, selected, SelectedValue);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }

        public bool RabComboBox_Descripcion(string sPag_sConexionLog, string Pag_sCompania, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ACUMAsientoContable";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 53);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);

            RadComboBox(ref objRadComboBox, ds, "PolDes", "PolDes", Filtro, selected, SelectedValue);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }
        public bool RabComboBox_ConceptoReferenciaTipo_CG(string sPag_sConexionLog, string Pag_sCompania, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ACUMAsientoContable";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);

            RadComboBox(ref objRadComboBox, ds, "cptoDes", "cptoDes", Filtro, selected, SelectedValue);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }

        //
        public bool RabComboBox_SatMetodoPago(string sPag_sConexionLog, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_SATMetodosPago";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);

            RadComboBox(ref objRadComboBox, ds, "satMetPagCve", "satMetPagDes", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }
        
        public bool RabComboBox_DireccionesEntrega(string sPag_sConexionLog, string sCiaCve, string sCliCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ClienteDireccionesEntrega";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 55);
            ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, 20, ParameterDirection.Input, sCliCve);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);

            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);

            RadComboBox(ref objRadComboBox, ds, "domId", "cliDirEntDom", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }

        public bool RabComboBox_PeriodoAnios(string sPag_sConexionLog, string sCiaCve,  ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet(); 
            ds = FNPeriodo.dsAniosPeriodos(sPag_sConexionLog, sCiaCve);
            RadComboBox(ref objRadComboBox, ds, "perAnio", "perAnio", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }

        public bool RabComboBox_PeriodoNum(string sPag_sConexionLog, string sCiaCve, int iPerAnio, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            ds = FNPeriodo.dsNumeroPeriodos(sPag_sConexionLog, sCiaCve, iPerAnio);
            RadComboBox(ref objRadComboBox, ds, "perNum", "perDes", Filtro, selected, SelectedValue);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }

        public bool RadComboBox_AgrupacionAgrTipId(string sPag_sConexionLog, string sCiaCve, Int64 agrTipId, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_AgrupacionesDato";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
            ProcBD.AgregarParametrosProcedimiento("@agrTipId", DbType.Int64, 0, ParameterDirection.Input, agrTipId);
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);

            RadComboBox(ref objRadComboBox, ds, "agrCve", "agrDes", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }

        public bool RadComboBox_AgrupaDatos(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, string agrCve, int agrTipId, bool Filtro, bool selected, string SelectedValue = "", string sDataValueField ="", string sDataTextField ="")
        {
            if (sDataValueField == "")
            {
                sDataValueField = "agrDatoCve";
            }
            if (sDataTextField == "")
            {
                sDataTextField = "agrDatoDes";
            }

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_AgrupacionesDato";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
            ProcBD.AgregarParametrosProcedimiento("@agrCve", DbType.String, 3, ParameterDirection.Input, agrCve);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
            ProcBD.AgregarParametrosProcedimiento("@agrTipId", DbType.Int32, 0, ParameterDirection.Input, agrTipId);
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);
            RadComboBox(ref objRadComboBox, ds, sDataValueField, sDataTextField, Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }

        public bool RadComboBox_Agrupa(string sPag_sConexionLog, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_selAgrupacionTipos";
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);
            RadComboBox(ref objRadComboBox, ds, "agrTipId", "agrTipDes", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }

        public bool RadComboBox_ConceptosSeguridad(string sPag_sConexionLog, string sCiaCve,string sContRefCve, string sMaUsuCve, int iOpcSeg , ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_SeguridadConceptos";
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
            ProcBD.AgregarParametrosProcedimiento("@contRefCve", DbType.String, 2, ParameterDirection.Input, sContRefCve);
            ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, sMaUsuCve);
            if (iOpcSeg > 0) {
                ProcBD.AgregarParametrosProcedimiento("@opcSeg", DbType.Int64, 0, ParameterDirection.Input, iOpcSeg);
            }
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);

            RadComboBox(ref objRadComboBox, ds, "cptoId", "cptoDes", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }


        public bool RadComboBox_RptNiveles_AgrTipo(string sPag_sConexionLog, string sRptCve, string sFiltro, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_RptNivelesAgr";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
            ProcBD.AgregarParametrosProcedimiento("@rptCve", DbType.String, 20, ParameterDirection.Input, sRptCve);
            ProcBD.AgregarParametrosProcedimiento("@filtro", DbType.String,1000, ParameterDirection.Input, sFiltro);
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);
            RadComboBox(ref objRadComboBox, ds, "agrTipId", "agrTipDes", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }


        public bool RadComboBox_ReferenciasVariablesTipo(string sPag_sConexionLog, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ReferenciasVariablesTipo";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);
            RadComboBox(ref objRadComboBox, ds, "revaCve", "revaDes", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }


        //Catalogos
        public bool RadComboBox_MetVal(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            ds = FNDatos.dsCatalogos(sPag_sConexionLog, "tb_MedotosValuacion", "metValId", "metValDes", sCiaCve);

            RadComboBox(ref objRadComboBox, ds, "Clave", "Descripcion", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }
        public bool RadComboBox_Monedas(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            ds = FNDatos.dsCatalogos(sPag_sConexionLog, "tb_Monedas", "monCve", "monDes", sCiaCve);
            RadComboBox(ref objRadComboBox, ds, "Clave", "Descripcion", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }
        public bool RabComboBox_CondicionesPago(string sPag_sConexionLog,string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "") {
            DataSet ds = new DataSet();
            ds = FNDatos.dsCatalogos(sPag_sConexionLog, "tb_CondicionPagos", "conPagCve", "conPagDes", sCiaCve);
            RadComboBox(ref objRadComboBox, ds, "Clave", "Descripcion", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }
        public bool RadComboBox_UnidadesMedida(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "") {
            DataSet ds = new DataSet();
            ds = FNDatos.dsCatalogos(sPag_sConexionLog, "tb_UnidadMedidas", "uniMedCve", "uniMedDes", sCiaCve);
            RadComboBox(ref objRadComboBox, ds, "Clave", "Descripcion", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }
        public bool RadComboBox_InstitucionesDeposito(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            ds = FNDatos.dsCatalogos(sPag_sConexionLog, "tb_InstitucionesDeposito", "insDepCve", "insDepDes", sCiaCve);
            RadComboBox(ref objRadComboBox, ds, "Clave", "Descripcion", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }
        public bool RabComboBox_Articulos(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            ds = FNDatos.dsCatalogos(sPag_sConexionLog, "tb_Articulos", "artCve", "artDes", sCiaCve);
            RadComboBox(ref objRadComboBox, ds, "Clave", "Descripcion", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }

        public bool RabComboBox_Modulos(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            ds = FNDatos.dsCatalogos(sPag_sConexionLog, "tb_Conceptos", "cptoId", "cptoDes", sCiaCve);
            RadComboBox(ref objRadComboBox, ds, "Clave", "Descripcion", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }

        public bool RabComboBox_Documentos(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            ds = FNDatos.dsCatalogos(sPag_sConexionLog, "tb_Documentos", "docCve", "docDes", sCiaCve);
            RadComboBox(ref objRadComboBox, ds, "Clave", "Descripcion", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }
        public bool RabComboBox_GeneraDocumentos(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            ds = FNDatos.dsCatalogos(sPag_sConexionLog, "tb_DocumentoGenera", "docGenId", "docGenDes", sCiaCve);
            if (FNAdoNet.bDSRowsIsFill(ds))
            {
                RadComboBox(ref objRadComboBox, ds, "Clave", "Descripcion", Filtro, selected, SelectedValue);
            }
              ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }
        public bool RadComboBox_ReferenciaBase(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            ds = FNDatos.dsCatalogos(sPag_sConexionLog, "tb_ContabilidadReferencia", "contRefCve", "contRefDes", sCiaCve);
            RadComboBox(ref objRadComboBox, ds, "Clave", "Descripcion", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }

        public bool RabComboBox_ViasEmbarque(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            ds = FNDatos.dsCatalogos(sPag_sConexionLog, "tb_ViaEmbarque", "viaEmbCve", "viaEmbDes", sCiaCve);
            RadComboBox(ref objRadComboBox, ds, "Clave", "Descripcion", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }
        public bool RabComboBox_Clientes(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            ds = FNDatos.dsCatalogos(sPag_sConexionLog, "tb_Clientes", "cliCve", "clieNom", sCiaCve);
            RadComboBox(ref objRadComboBox, ds, "Clave", "Descripcion", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }

        public bool RabComboBox_ClientesVW(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            ds = FNDatos.dsCatalogos(sPag_sConexionLog, "vw_Clientes", "cliCve", "cliCveNom", sCiaCve);
            RadComboBox(ref objRadComboBox, ds, "Clave", "cliCveNom", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }

        public bool RabComboBox_ConceptosEstadisticos(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            ds = FNDatos.dsCatalogos(sPag_sConexionLog, "tb_ConceptosEstadisticos", "cptoEstId", "cptoEstDes", sCiaCve);
            RadComboBox(ref objRadComboBox, ds, "Clave", "Descripcion", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }

        public bool RadComboBox_ContabilidadReferenciaTipo(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            ds = FNDatos.dsCatalogos(sPag_sConexionLog, "tb_ContabilidadReferenciaTipo", "contRefTipCve", "contRefDes", sCiaCve);
            RadComboBox(ref objRadComboBox, ds, "Clave", "Descripcion", Filtro, selected, SelectedValue);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }

        public bool RabComboBox_Proveedores(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {

            DataSet ds = new DataSet();
            ds = FNDatos.dsCatalogos(sPag_sConexionLog, "tb_Proveedores", "provCve", "provNom", sCiaCve);

            RadComboBox(ref objRadComboBox, ds, "Clave", "Descripcion", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;

        }

        public bool RadComboBox_CodificacionCuentas(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            ds = FNDatos.dsCatalogos(sPag_sConexionLog, "vw_CuentasContables", "ctaContCve", "ctaContNom", sCiaCve);
            RadComboBox(ref objRadComboBox, ds, "Clave", "Descripcion", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }

        public bool RadComboBox_ListaPrecios(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            ds = FNDatos.dsCatalogos(sPag_sConexionLog, "tb_ListaPrecios", "lisPreCve", "lisPreDes", sCiaCve);
            RadComboBox(ref objRadComboBox, ds, "Clave", "Descripcion", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }

        public bool RadComboBox_Impuestos(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            ds = FNDatos.dsCatalogos(sPag_sConexionLog, "tb_Impuestos", "impCve", "impDes", sCiaCve);
            RadComboBox(ref objRadComboBox, ds, "Clave", "Descripcion", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }

        public bool RabComboBox_ListaPrecios(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            ds = FNDatos.dsCatalogos(sPag_sConexionLog, "tb_ListaPrecios", "lisPreCve", "lisPreDes", sCiaCve);
            RadComboBox(ref objRadComboBox, ds, "Clave", "Descripcion", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }

        public bool RabComboBox_Almacen(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            ds = FNDatos.dsCatalogos(sPag_sConexionLog, "tb_Almacenes", "almCve", "almDes", sCiaCve);
            RadComboBox(ref objRadComboBox, ds, "Clave", "Descripcion", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }



        //Listas Precargadas

        public bool RadComboBox_TiposProveedor(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "") {
            DataSet ds = new DataSet();
            ds = FNDatos.dsListasPrecargadas(sPag_sConexionLog, sCiaCve, "TipoProv", "int");
            RadComboBox(ref objRadComboBox, ds, "listPreVal", "listPreValDes", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }

        public bool RadComboBox_Clasificacion(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            ds = FNDatos.dsListasPrecargadas(sPag_sConexionLog, sCiaCve, "clasRot", "Str");
            RadComboBox(ref objRadComboBox, ds, "listPreVal", "listPreValDes", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }

        public bool RadComboBox_Existencias(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            ds = FNDatos.dsListasPrecargadas(sPag_sConexionLog, sCiaCve, "manExi", "Str");
            RadComboBox(ref objRadComboBox, ds, "listPreVal", "listPreValDes", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }

        public bool RadComboBox_TiposAgentes(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            ds = FNDatos.dsListasPrecargadas(sPag_sConexionLog, sCiaCve, "TipoAgente", "int");
            RadComboBox(ref objRadComboBox, ds, "listPreVal", "listPreValDes", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }

        public bool RadComboBox_FormatoCheque(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            ds = FNDatos.dsListasPrecargadas(sPag_sConexionLog, sCiaCve, "CDepFCheq", "Int");
            RadComboBox(ref objRadComboBox, ds, "listPreVal", "listPreValDes", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }
        public bool RadComboBox_ManejoFolios(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            ds = FNDatos.dsListasPrecargadas(sPag_sConexionLog, sCiaCve, "ManejoFol", "Int");
            RadComboBox(ref objRadComboBox, ds, "listPreVal", "listPreValDes", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }


        public bool RadComboBox_FormaAplicacion(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            ds = FNDatos.dsListasPrecargadas(sPag_sConexionLog, sCiaCve, "ForAplic", "Str");
            RadComboBox(ref objRadComboBox, ds, "listPreValStr", "listPreValDes", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }

        public bool RadComboBox_ManejaDescuentos(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            ds = FNDatos.dsListasPrecargadas(sPag_sConexionLog, sCiaCve, "ManDes", "Int");
            RadComboBox(ref objRadComboBox, ds, "listPreValInt", "listPreValDes", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }

        public bool RadComboBox_ManejaListaDePrecios(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            ds = FNDatos.dsListasPrecargadas(sPag_sConexionLog, sCiaCve, "ManListPre", "Int");
            RadComboBox(ref objRadComboBox, ds, "listPreValInt", "listPreValDes", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }

        public bool RadComboBox_TipoSusCont(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            ds = FNDatos.dsListasPrecargadas(sPag_sConexionLog, sCiaCve, "TipoSusCon", "Int");
            RadComboBox(ref objRadComboBox, ds, "listPreValInt", "listPreValDes", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }

        public bool RadComboBox_FormatoFolios(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
        {
            DataSet ds = new DataSet();
            ds = FNDatos.dsCatalogos(sPag_sConexionLog, "tb_FormatosFolio", "formFolCve", "formFolDes", sCiaCve);

            RadComboBox(ref objRadComboBox, ds, "Clave", "Descripcion", Filtro, selected);
            ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
            return true;
        }


        public bool RadGrid(ref RadGrid objRadGrid, DataSet ds)
        {
            objRadGrid.DataSource = ds.Tables[0];
            objRadGrid.DataBind();
            return true;
        }

        public bool cRadMenu(ref RadMenu objRadMenu, DataSet ds,string sDataFieldID, string sDataFieldParentID, string sDataTextField, string sDataVavigateUrlField)
        {
            objRadMenu.DataSource = ds.Tables[0] ;
            objRadMenu.DataFieldID = sDataFieldID;
            objRadMenu.DataFieldParentID = sDataFieldParentID;

            objRadMenu.DataTextField = sDataTextField;
            objRadMenu.DataValueField = sDataFieldID;
            objRadMenu.DataNavigateUrlField = sDataVavigateUrlField;
            objRadMenu.DataBind();
            return true;
        }



    }
    
    public class Clean
    {
        public bool RadComboBox(ref RadComboBox objRadComboBox) {

            objRadComboBox.DataSource = null;
            objRadComboBox.DataBind();
            objRadComboBox.ClearSelection();
            objRadComboBox.Text = string.Empty;

            return true;
        }
    }

    public class RadWindows
    {

        public bool cRadWindowsCompanias(System.Web.UI.Page i_Page, ref RadWindowManager objRadWindowManagerPage)
        {

            Panel pnlCompanias;
            pnlCompanias = ((Panel)i_Page.Master.FindControl("pnlCompanias"));

            RadWindow rwindows = new RadWindow();
            rwindows.ID = "RADCIA";
            rwindows.NavigateUrl = "";
            rwindows.Title = "Companias";
            rwindows.ContentContainer.Controls.Add(pnlCompanias);

            objRadWindowManagerPage.Windows.Add(rwindows);

            return true;
        }

        public bool cRadWindowsArticulos(System.Web.UI.Page i_Page, ref RadWindowManager objRadWindowManagerPage) {

            Panel pnlArticulos = new Panel();
            RadButton btn = new RadButton();
            btn.Text = "mI PRUEBA BOTON";



            pnlArticulos.Controls.Add(btn);


            RadWindow rwindows = new RadWindow();
            rwindows.ID = "FNTempo1";
            rwindows.NavigateUrl = "";
            rwindows.Title = "prueba";
            rwindows.ContentContainer.Controls.Add(pnlArticulos);

            objRadWindowManagerPage.Windows.Add(rwindows);
            return true;
        }

    }

    public class PermisosBTN
    {
        ws.Servicio oWS = new ws.Servicio();
        MGMFnGrales.FNDatos FNDatos = new MGMFnGrales.FNDatos();
        MGMFnGrales.ObjAdoNet FNAdoNet = new MGMFnGrales.ObjAdoNet();
 
        public bool MAPerfiles_Operacion_Acciones(System.Web.UI.Page i_Page, string sPag_sConexionLog, string sCiaCve, string sMAUsuCve, Int64 iMAMenuId)
        {

            Int64 iCountVisible = 0;
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_MAPerfiles_Operacion_Acciones";
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
            ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 10, ParameterDirection.Input, sMAUsuCve);
            ProcBD.AgregarParametrosProcedimiento("@maMenuId", DbType.Int64, 0, ParameterDirection.Input, iMAMenuId);
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);

            if (FNAdoNet.bDSRowsIsFill(ds))
            {
                foreach (DataRow etiqueta in ds.Tables[0].Rows)
                {

                    try
                    {

                        Int64 iPermiso = Convert.ToInt64(etiqueta["Permiso"]);
                        Boolean bPermiso = false;
                        if (iPermiso == 1)
                        {
                            bPermiso = true;
                            iCountVisible += 1;
                        }

                        RadImageButton btn = new RadImageButton();
                        btn = ((RadImageButton)i_Page.FindControl(etiqueta["accIdContrl"].ToString()));
                        btn.Visible = bPermiso;


                    }
                    catch (Exception ex)
                    {

                    }
                }
            }


            if(iCountVisible == 0) {

                try
                {
                    Panel pnl = new Panel();
                    pnl = ((Panel)i_Page.FindControl("pnlBtnsAplicaAccion"));
                    pnl.Visible = false;
                }
                catch (Exception ex)
                {

                }

   
            }

           return true;
        }


        public bool MAPerfiles_Operacion_Acciones(Panel i_Panel, string sPag_sConexionLog, string sCiaCve, string sMAUsuCve, Int64 iMAMenuId)
        {

            Int64 iCountVisible = 0;
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_MAPerfiles_Operacion_Acciones";
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
            ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 10, ParameterDirection.Input, sMAUsuCve);
            ProcBD.AgregarParametrosProcedimiento("@maMenuId", DbType.Int64, 0, ParameterDirection.Input, iMAMenuId);
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);

            if (FNAdoNet.bDSRowsIsFill(ds))
            {
                foreach (DataRow etiqueta in ds.Tables[0].Rows)
                {

                    try
                    {

                        Int64 iPermiso = Convert.ToInt64(etiqueta["Permiso"]);
                        Boolean bPermiso = false;
                        if (iPermiso == 1)
                        {
                            bPermiso = true;
                            iCountVisible += 1;
                        }

                        RadImageButton btn = new RadImageButton();
                        btn = ((RadImageButton)i_Panel.FindControl(etiqueta["accIdContrl"].ToString()));
                        btn.Visible = bPermiso;


                    }
                    catch (Exception ex)
                    {

                    }
                }
            }

            return true;
        }


        public RadButton FindControlRecursive(RadButton rootControl, string controlID) {
            if (rootControl.ID == controlID)
            {
                return rootControl;
            }

            foreach (RadButton controlToSearch in rootControl.Controls)
            {
                RadButton controlToReturn = FindControlRecursive(controlToSearch, controlID);
                if (controlToReturn != null)
                {
                    return controlToReturn;
                }
            }
            return null;

        }

    }


}