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

public partial class SG_SGPerfil : System.Web.UI.Page
{

    #region VARIABLES
    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    ws.Servicio oWS = new ws.Servicio();

    MGMFnGrales.FNGrales FNGrales = new MGMFnGrales.FNGrales();
    MGMControls.FillIn FnCtlsFillIn = new MGMControls.FillIn();
    MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();
    MGMFnGrales.FNDatos FNDatos = new MGMFnGrales.FNDatos();
    MGMFnGrales.FnValidaciones FNValida = new MGMFnGrales.FnValidaciones();

    private string Pag_sCompania;
    private string Pag_sConexionLog;
    private string Pag_sSessionLog;
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

    protected void rBtnNuevo_Click(object sender, ImageButtonClickEventArgs e)
    {

        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();
        ControlesAccion();
    }

    protected void rBtnModificar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString();
        ControlesAccion();
    }

    protected void rBtnEliminar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString();
        ControlesAccion();
    }

    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccionLimpiar();
    }

    protected void rGdvImpuestos_SelectedIndexChanged(object sender, EventArgs e)
    {
        var dataItem = rGdvPerfiles.SelectedItems[0] as GridDataItem;
        string val = dataItem["maPerfSts"].Text;

            if (dataItem != null)
            {
                rtxtClave.Text = dataItem["maPerfCve"].Text;
                rTxtDescripcion.Text = dataItem["maPerfDes"].Text;
            }

            if (val == "1")
            {
                CheckReg.Checked = true;
            }
            else
            {
                CheckReg.Checked = false;
            }

        //if (dataItem != null)
        //{
        //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() ||
        //        hdfBtnAccion.Value == "")
        //    {

        //        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        //        {
        //            this.rtxtClave.Enabled = false;
        //            this.rTxtDescripcion.Enabled = true;
        //            CheckReg.Enabled = true;
        //        }
        //        string skey = dataItem.GetDataKeyValue("maPerfCve").ToString();


        //        rtxtClave.Text = dataItem["maPerfCve"].Text;
        //        rTxtDescripcion.Text = dataItem["maPerfDes"].Text;

        //        if (val == "1")
        //        {
        //            CheckReg.Checked = true;
        //        }
        //        else
        //        {
        //            CheckReg.Checked = false;
        //        }
        //    }
        //}
    }

    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccion();
    }

    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {
        InicioPagina();
    }

    #endregion

    #region METODOS
    private void InicioPagina()
    {
        LlenaGrid();
        hdfBtnAccion.Value = "";
        ControlesAccion();
        rGdvPerfiles.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvPerfiles.AllowMultiRowSelection = true;
    }

    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
    }

    private void LlenaGrid()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MAPerfiles";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref rGdvPerfiles, ds);
    }

    private int ValCheck()
    {
        if (CheckReg.Checked == true)
        {
            return 1;
        }
        else
        {

            return 2;
        }
    }

    private void EjecutaAccion()
    {
        string sMSGTip = "";
        string msgValidacion = validaEjecutaAccion(ref sMSGTip);

        if (msgValidacion == "")
        {

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
                  hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                EjecutaSpAcciones();
                InicioPagina();
            }

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaSpAccionEliminar();
            }

        }
        else
        {
            ShowAlert("2", msgValidacion);
        }
    }

    private string validaEjecutaAccion(ref string sMSGTip)
    {

        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            if (rtxtClave.Text.Trim() == "")
            {
                rtxtClave.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rtxtClave.CssClass = "cssTxtEnabled"; }

            if (rTxtDescripcion.Text.Trim() == "")
            {
                rTxtDescripcion.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtDescripcion.CssClass = "cssTxtEnabled"; }

           

            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }


        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {

            if (rGdvPerfiles.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            if (rtxtClave.Text.Trim() == "")
            {
                rtxtClave.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rtxtClave.CssClass = "cssTxtEnabled"; }

            if (rTxtDescripcion.Text.Trim() == "")
            {
                rTxtDescripcion.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtDescripcion.CssClass = "cssTxtEnabled"; }          


            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;

        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

            if (rGdvPerfiles.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            return sResult;
        }




        return sResult;
    }

    private void EjecutaSpAcciones()
    {
        DataSet ds = new DataSet();

        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MAPerfiles";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@maPerfCve", DbType.String, 10, ParameterDirection.Input, rtxtClave.Text);
        ProcBD.AgregarParametrosProcedimiento("@maPerfDes", DbType.String, 100, ParameterDirection.Input, rTxtDescripcion.Text);
        ProcBD.AgregarParametrosProcedimiento("@maPerfSts", DbType.Int32, 0, ParameterDirection.Input, ValCheck());

        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSIsFill(ds))
        {
            string sEjecEstatus, sEjecMSG = "";
            sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
            sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

            ShowAlert(sEjecEstatus, sEjecMSG);
            if (sEjecEstatus == "1")
            {
                InicioPagina();
            }
            
        }


    }

    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }

    private void EjecutaSpAccionEliminar()
    {
        try
        {
            int CountItems = 0;
            int CantItemsElimTrue = 0;
            int CantItemsElimFalse = 0;
            string EstatusItemsElim = "";
            string MsgItemsElim = "";
            string MsgItemsElimTrue = "";
            string MsgItemsElimFalse = "";


            foreach (GridDataItem i in rGdvPerfiles.SelectedItems)
            {

                var dataItem = rGdvPerfiles.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {

                    string maPerfCve = dataItem.GetDataKeyValue("maPerfCve").ToString();
                    try
                    {

                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_MAPerfiles";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                        ProcBD.AgregarParametrosProcedimiento("@maPerfCve", DbType.String, 10, ParameterDirection.Input, maPerfCve);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);


                        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

                        if (FnValAdoNet.bDSIsFill(ds))
                        {

                            EstatusItemsElim = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                            if (EstatusItemsElim == "1")
                            {
                                CantItemsElimTrue += 1;
                                MsgItemsElimTrue = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                            }
                            else
                            {
                                CantItemsElimFalse += 1;
                                MsgItemsElimFalse = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                            }

                            MsgItemsElim = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                        }


                    }
                    catch (Exception ex)
                    {
                        string MsgError = ex.Message.Trim();
                    }


                }

                CountItems += 1;
            }





            string sEstatusAlert = "2";
            string sMsgAlert = "";

            if (CountItems == 1)
            {

                sEstatusAlert = EstatusItemsElim;
                if (sEstatusAlert == "1")
                {
                    sMsgAlert = MsgItemsElim + " " + CountItems.ToString();
                }
                else
                {
                    sMsgAlert = MsgItemsElim;
                }


                ShowAlert(sEstatusAlert, sMsgAlert);

                if (sEstatusAlert == "1")
                {
                    InicioPagina();
                }
                else
                {
                    //LlenaGrid();
                    InicioPagina();
                }


            }
            else if (CountItems > 1)
            {


                if (CantItemsElimTrue > 0)
                {
                    sEstatusAlert = "1";
                }

                if (CantItemsElimTrue > 0)
                {
                    string sMSGTip = "";
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "ABC0003", ref sMSGTip, ref sMsgAlert);
                    sMsgAlert += " " + CantItemsElimTrue.ToString();
                }

                if (CantItemsElimFalse > 0)
                {
                    if (sMsgAlert != "")
                    {
                        sMsgAlert = sMsgAlert + "</br>";
                    }

                    string sMSGTip = "";
                    string sMSGRegNoElim = "";
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "ABC0006", ref sMSGTip, ref sMSGRegNoElim);
                    sMsgAlert = sMsgAlert + sMSGRegNoElim + " " + CantItemsElimFalse.ToString();
                }


                ShowAlert(sEstatusAlert, sMsgAlert);
                if (CountItems == CantItemsElimTrue)
                {
                    InicioPagina();
                }
                else
                {
                    InicioPagina();
                }

            }

        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }


    }
    #endregion

    #region FUNCIONES

    private void ControlesAccion()
    {

        //===> CONTROLES GENERAL
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL
        this.rGdvPerfiles.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        rtxtClave.CssClass = "cssTxtEnabled";
        rTxtDescripcion.CssClass = "cssTxtEnabled";

        this.rtxtClave.Enabled = false;
        this.rTxtDescripcion.Enabled = false;
        CheckReg.Enabled = false;

        rBtnGuardar.Enabled = false;
        rBtnCancelar.Enabled = false;

        /*>>>>>>>>>>>>>>>>> CONTROLES POR ACCION <<<<<<<<<<<<<<<<<<  */
        //Validacion
        msgValidacion = ValidaControlesAccion_SelectRowGrid(ref sMSGTip);
        if (msgValidacion == "")
        {
            ControlesAccionEjecucion(true);
        }
        else
        {
            ControlesAccionEjecucion(false);
            ShowAlert(sMSGTip, msgValidacion);
        }

        //INICIO / CANCELAR
        if (hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() &&
        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() &&
        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString() &&
        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString()
          )
        {
            this.rtxtClave.Enabled = false;
            this.rTxtDescripcion.Enabled = false;
            this.rtxtClave.Text = "";
            this.rTxtDescripcion.Text = "";
            CheckReg.Enabled = false;
        }
    }
    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = rGdvPerfiles.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvPerfiles, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvPerfiles, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }
        return sResult;
    }
    private void EjecutaAccionLimpiar()
    {
        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            this.rtxtClave.Text = "";
            this.rTxtDescripcion.Text = "";

        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rGdvPerfiles.ClientSettings.Selecting.AllowRowSelect = true;
            rGdvPerfiles.AllowMultiRowSelection = true;
            rGdvPerfiles.MasterTableView.ClearSelectedItems();

            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";


            rtxtClave.CssClass = "cssTxtEnabled";
            rTxtDescripcion.CssClass = "cssTxtEnabled";

            rtxtClave.Enabled = false;
            rTxtDescripcion.Enabled = false;
            CheckReg.Enabled = false;

            rtxtClave.Text = "";
            rTxtDescripcion.Text = "";
            CheckReg.Checked = false;

            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;
        }

        //ELIMIAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

        }
    }
    private void ControlesAccionEjecucion(bool Result)
    {
        if (Result == true)
        {
            //NUEVO
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {
                rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
                this.rGdvPerfiles.ClientSettings.Selecting.AllowRowSelect = false;
                rGdvPerfiles.MasterTableView.ClearSelectedItems();

                this.rtxtClave.Enabled = true;
                this.rTxtDescripcion.Enabled = true;
                this.rtxtClave.Text = "";
                this.rTxtDescripcion.Text = "";
                CheckReg.Enabled = true;

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
                rGdvPerfiles.AllowMultiRowSelection = false;

                this.rtxtClave.Enabled = false;
                this.rTxtDescripcion.Enabled = true;
                CheckReg.Enabled = true;

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //ELIMIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaAccion();
            }

            //LIMPIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString())
            {
                rGdvPerfiles.ClientSettings.Selecting.AllowRowSelect = true;
                rGdvPerfiles.AllowMultiRowSelection = true;
                rGdvPerfiles.MasterTableView.ClearSelectedItems();

                this.rtxtClave.Enabled = false;
                this.rTxtDescripcion.Enabled = false;
                CheckReg.Enabled = false;

                this.rtxtClave.Text = "";
                this.rTxtDescripcion.Text = "";
                CheckReg.Checked = false;

            }
        }


        if (Result == false)
        {
            this.rtxtClave.Enabled = false;
            this.rTxtDescripcion.Enabled = false;
            CheckReg.Enabled = false;

            this.rtxtClave.Text = "";
            this.rTxtDescripcion.Text = "";
            CheckReg.Checked = false;
        }


    }

    #endregion
}