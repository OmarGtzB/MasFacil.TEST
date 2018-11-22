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
using System.Windows.Forms;

public partial class DC_MttoCptoDerivados : System.Web.UI.Page
{

    #region VARIABLES
    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    MGMSecurity.LoginManeger LM = new MGMSecurity.LoginManeger();
    ws.Servicio oWS = new ws.Servicio();

    MGMFnGrales.FNGrales FNGrales = new MGMFnGrales.FNGrales();
    MGMControls.FillIn FnCtlsFillIn = new MGMControls.FillIn();
    MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();
    MGMFnGrales.FNDatos FNDatos = new MGMFnGrales.FNDatos();
    MGMFnGrales.FnValidaciones FNValida = new MGMFnGrales.FnValidaciones();
    MGMControls.PermisosBTN FNBtn = new MGMControls.PermisosBTN();

    private string Pag_sCompania;
    private string Pag_sConexionLog;
    private string Pag_sSessionLog;
    private string PagLoc_Concepto;

    private string SecValDerivado;
    private string TipoCve;

    //private static int CveEliminar;
    //private static string ValEliminar;
    //private static int ValDS ;

    private int CveEliminar;
    private string ValEliminar;
    private int ValDS;

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
        //ControlesAccion();
        if (PagLoc_Concepto != "")
        {
            rGdvDerivados.ClientSettings.Selecting.AllowRowSelect = false;

            ControlesAccion();
            NuevoLLenaGridConceptos("");
            rGdvDerivados.DataBind();

            rCboOrigen.ClearSelection();
            rCboDerivado.ClearSelection();

            rCboOrigen.Enabled = false;
            rCboDerivado.Enabled = false;

            limpiarcss();
        }
        RadtxtBuscar.Text = "";


    }

    protected void rBtnModificar_Click(object sender, ImageButtonClickEventArgs e)

    {

        //if (PagLoc_Concepto != "")
        //{
        //    hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString();
        //    limpiarcss();
        //    ControlesAccion();
        //    LlenaGridConceptos("");
        //    rCboOrigen.Enabled = false;
        //    rCboDerivado.Enabled = false;
        //    rCboOrigen.ClearSelection();
        //    rCboDerivado.ClearSelection();
        //    rGdvDerivados.ClientSettings.Selecting.AllowRowSelect = true;
        //    rGdvDerivados.DataBind();
        //}
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString();
        ControlesAccion();
        RadtxtBuscar.Text = "";
    }

    protected void rBtnEliminar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString();
        ControlesAccion();

        //if (PagLoc_Concepto != "")
        //{
        //    hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString();
        //    LlenaGridConceptos("");
        //    limpiarcss();
        //    ControlesAccion();
        //    rCboOrigen.Enabled = false;
        //    rCboDerivado.Enabled = false;
        //    rCboOrigen.ClearSelection();
        //    rCboDerivado.ClearSelection();
        //    rGdvConceptos.ClientSettings.Selecting.AllowRowSelect = true;
        //    rGdvDerivados.ClientSettings.Selecting.AllowRowSelect = true;
        //    rGdvDerivados.DataBind();
        //}

        //RadtxtBuscar.Text = "";


    }

    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
    {
        // ControlesAccion();
        //btnLimpiar();
        EjecutaAccionLimpiar();


    }

    protected void rbtBuscar_Click(object sender, EventArgs e)
    {
        txtBuscar();
    }

    protected void rGdvConceptos_SelectedIndexChanged(object sender, EventArgs e)
    {

        rCboDerivado.Enabled = false;
        rCboOrigen.Enabled=false;
        //if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        //{
        //    CveEliminar = 1;
        //    rGdvDerivados.AllowMultiRowSelection = true;
        //    rGdvDerivados.ClientSettings.Selecting.AllowRowSelect = true;
        //}

        //if (hdfBtnAccion.Value == "")
        //{
        //    rGdvDerivados.ClientSettings.Selecting.AllowRowSelect = false;
        //}

        rGdvConceptos.ClientSettings.Selecting.AllowRowSelect = true;
        rCboDerivado.ClearSelection();
        rCboOrigen.ClearSelection();

        var dataItem = rGdvConceptos.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {
            //if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() || hdfBtnAccion.Value == "")
            //{

                DataSet ds = new DataSet();
                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                ProcBD.NombreProcedimiento = "sp_MttoCptoDerivados";
                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 53);
                ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.String, 20, ParameterDirection.Input, dataItem["cptoDerCptoID"].Text);
                ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

                FnCtlsFillIn.RadComboBox(ref rCboDerivado, ds, "unica", "cptoConfDes", true, false, "");
                ((Literal)rCboDerivado.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboDerivado.Items.Count);

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {
                rCboDerivado.Enabled = true;
            }
            llenarGridDerivados();
            //}

            //else if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() ||
            //         hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString() || hdfBtnAccion.Value == "")
            //{

            //    llenarGridDerivados();

            //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            //    {
            //        rGdvDerivados.ClientSettings.Selecting.AllowRowSelect = true;
            //    }


            //}

        }


    }

    protected void rCboDerivado_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        string cadena = "";
        cadena = rCboDerivado.SelectedValue;
        TipoCve = cadena.Substring(0, 7);

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MttoCptoDerivados";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 55);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.String, 20, ParameterDirection.Input, PagLoc_Concepto);
        ProcBD.AgregarParametrosProcedimiento("@listTipDatoCptoCve", DbType.String, 10, ParameterDirection.Input, TipoCve);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        FnCtlsFillIn.RadComboBox(ref rCboOrigen, ds, "cptoConfSec", "cptoConfDes", true, false, "");
        ((Literal)rCboOrigen.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboOrigen.Items.Count);

        if (ds.Tables[0].Rows.Count > 0)
        {
            TipoDato.Value = ds.Tables[0].Rows[0]["listTipDatoCptoCve"].ToString();
        }
        // MessageBox.Show(rCboDerivado..ToString());
        rCboOrigen.Enabled = true;
    }

    protected void rGdvDerivados_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        //{
        //    CveEliminar = 2;

        //}
        if (rBtnModificar.Image.Url == "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png")
        {
            rCboOrigen.Enabled=true;
           
        }
        combosds();
    }

    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccion();
    }

    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {
        ControlesAccion();
        InicioPagina();
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnGuardar.Enabled = false;
        rBtnCancelar.Enabled = false;
        rGdvConceptos.DataBind();
        rGdvDerivados.DataBind();
        rCboOrigen.ClearSelection();
        rCboOrigen.Enabled = false;
        rCboDerivado.ClearSelection();
        rCboDerivado.Enabled = false;
        rGdvConceptos.ClientSettings.Selecting.AllowRowSelect = true;
        hdfBtnAccion.Value = "";
        CveEliminar = 0;
        RadtxtBuscar.Text = "";
        RadtxtBuscar.CssClass = "cssTxtEnabled";
        rCboDerivado.BorderColor = System.Drawing.Color.Transparent;
        rCboOrigen.BorderColor = System.Drawing.Color.Transparent;

    }

    #endregion
    
    #region FUNCIONES

    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
        PagLoc_Concepto = Convert.ToString(Session["folio_Selection"]);
    }

    private void InicioPagina()
    {
        if (PagLoc_Concepto == "")
        {
            RadlabelOrigenID.Text = "";
            RadlabelOrigenDes.Text = "";
        }
        else
        {
            concepto();
        }

        rGdvConceptos.ClientSettings.Selecting.AllowRowSelect = true;
        
        LlenaGridConceptos("");
        ControlesAccion();
        
        rCboOrigen.EmptyMessage = "Seleccionar";
        rCboDerivado.EmptyMessage = "Seleccionar";
        hdfBtnAccion.Value = "";
        
        rGdvConceptos.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvConceptos.AllowMultiRowSelection = true;

        rGdvDerivados.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvDerivados.AllowMultiRowSelection = true;


        PermisoBotones();
    }


    private void PermisoBotones()
    {
        Int64 Pag_sidM = 0;
        if (Request.QueryString["idM"] != null && Request.QueryString["idM"] != "")
        {
            Pag_sidM = Convert.ToInt64(Request.QueryString["idM"]);
        }
        string maUser = LM.sValSess(this.Page, 1);
        FNBtn.MAPerfiles_Operacion_Acciones(Page, Pag_sConexionLog, Pag_sCompania, maUser, Pag_sidM);
    }



    private void EjecutaSpAcciones()
    {

        string cadena = "";
        cadena = rCboDerivado.SelectedValue;
        TipoCve = cadena.Substring(0, 7);


        String var1 = rCboDerivado.SelectedValue.ToString(); ;
        int tam_var = var1.Length;
        String SecValDerivado = var1.Substring((tam_var - 4), 4);

        var dataItem1 = rGdvConceptos.SelectedItems[0] as GridDataItem;

        DataSet dsDerivado = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBDDerivado = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBDDerivado.NombreProcedimiento = "sp_MttoCptoDerivados";
        ProcBDDerivado.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 57);
        ProcBDDerivado.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 20, ParameterDirection.Input, Pag_sCompania);
        ProcBDDerivado.AgregarParametrosProcedimiento("@cptoId", DbType.String, 10, ParameterDirection.Input, dataItem1["cptoDerCptoID"].Text);
        ProcBDDerivado.AgregarParametrosProcedimiento("@listTipDatoCptoCve", DbType.String, 10, ParameterDirection.Input, TipoCve.Trim());
        ProcBDDerivado.AgregarParametrosProcedimiento("@cptoConfSec", DbType.Int64, 0, ParameterDirection.Input, SecValDerivado);
        dsDerivado = oWS.ObtenerDatasetDesdeProcedimiento(ProcBDDerivado.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        String cptoDerivado = "";

        String SecDerivado = "";

        cptoDerivado = dataItem1["cptoDerCptoID"].Text;
        SecDerivado = dsDerivado.Tables[0].Rows[0]["cptoConfSec"].ToString();

        
        try
        {
            var dataItem = rGdvConceptos.SelectedItems[0] as GridDataItem;


            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_MttoCptoDerivados";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 20, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 10, ParameterDirection.Input, PagLoc_Concepto);
            ProcBD.AgregarParametrosProcedimiento("@listTipDatoCptoCve", DbType.String, 10, ParameterDirection.Input, TipoCve.Trim());
            ProcBD.AgregarParametrosProcedimiento("@cptoConfSec", DbType.Int64, 5, ParameterDirection.Input, rCboOrigen.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@cptoDerCptoID", DbType.Int64, 10, ParameterDirection.Input, dataItem["cptoDerCptoID"].Text);
            ProcBD.AgregarParametrosProcedimiento("@cptoDerListTipDatoCptoCve", DbType.String, 10, ParameterDirection.Input, TipoCve.Trim());
            ProcBD.AgregarParametrosProcedimiento("@cptoDerCptoConfSec", DbType.Int64, 5, ParameterDirection.Input, SecValDerivado.Trim());
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                var dataItemDeriv = rGdvDerivados.SelectedItems[0] as GridDataItem;
                string MapId = dataItemDeriv["cptoDerMapId"].Text;
                ProcBD.AgregarParametrosProcedimiento("@cptoDerMapId", DbType.Int64, 10, ParameterDirection.Input, Convert.ToInt32(MapId));
            }




            // MessageBox.Show("ER");

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjecMSG;
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                //if (sEjecEstatus == "4")
                //{
                //    sEjecEstatus = "2";
                //    sEjecMSG = "No se puede duplicar la Conf. Derivado";
                //}

                ShowAlert(sEjecEstatus, sEjecMSG);

                if (sEjecEstatus == "1")
                {

                    llenarGridDerivados();
                    rCboOrigen.ClearSelection();
                    rCboDerivado.ClearSelection();
                    rCboOrigen.Enabled = false;
                    hdfBtnAccion.Value = "";
                    LimpiaGrid();
                    InicioPagina();
                }



            }


        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }


    }

    private void EjecutaSpAccionEliminar()
    {
        int CountItems = 0;
        int CantItemsElimTrue = 0;
        int CantItemsElimFalse = 0;
        string EstatusItemsElim = "";
        string MsgItemsElim = "";
        string MsgItemsElimTrue = "";
        string MsgItemsElimFalse = "";

        try
        {
            ObtieneValElimnar();

            if (CveEliminar == 1)
            {
                try
                {
                    var dataItem = rGdvConceptos.SelectedItems[0] as GridDataItem;
                    DataSet ds = new DataSet();
                    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                    ProcBD.NombreProcedimiento = "sp_MttoCptoDerivados";
                    ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 4);
                    ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 20, ParameterDirection.Input, Pag_sCompania);
                    ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 10, ParameterDirection.Input, PagLoc_Concepto);

                    ProcBD.AgregarParametrosProcedimiento("@cptoDerCptoID", DbType.Int64, 10, ParameterDirection.Input, dataItem["cptoDerCptoID"].Text);

                    ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
                    if (FnValAdoNet.bDSIsFill(ds))
                    {
                        string sEjecEstatus, sEjecMSG = "";
                        sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                        sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                        ShowAlert(sEjecEstatus, sEjecMSG);

                        if (sEjecEstatus == "1")
                        {
                            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
                            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
                            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

                            rGdvConceptos.DataBind();
                            rGdvDerivados.DataBind();
                            rBtnGuardar.Enabled = false;
                            rBtnCancelar.Enabled = false;
                            LlenaGridConceptos("");

                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }


            }

            ////////////////////////////////////////Eliminar derivados por filas///////////////////////////////////////////////////
            if (CveEliminar == 2)
            {

                ValEliminar = "";

                rGdvDerivados.ClientSettings.Selecting.AllowRowSelect = true;
                var dataItemConceptos = rGdvConceptos.SelectedItems[0] as GridDataItem;
                string IdConcepto = dataItemConceptos["cptoDerCptoID"].Text;

                foreach (GridDataItem i in rGdvDerivados.SelectedItems)
                {
                    var dataItem = rGdvDerivados.SelectedItems[CountItems] as GridDataItem;
                    if (dataItem != null)
                    {

                        string MapId = dataItem["cptoDerMapId"].Text;

                        try
                        {

                            DataSet ds = new DataSet();
                            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                            ProcBD.NombreProcedimiento = "sp_MttoCptoDerivados";
                            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                            ProcBD.AgregarParametrosProcedimiento("@cptoDerMapId", DbType.Int64, 10, ParameterDirection.Input, Convert.ToInt32(MapId));
                            ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 10, ParameterDirection.Input, PagLoc_Concepto);
                            ProcBD.AgregarParametrosProcedimiento("@cptoDerCptoID", DbType.Int64, 10, ParameterDirection.Input, IdConcepto);

                            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);


                            if (FnValAdoNet.bDSIsFill(ds))
                            {
                                EstatusItemsElim = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                                if (EstatusItemsElim == "1")
                                {
                                    CantItemsElimTrue += 1;
                                    MsgItemsElimTrue = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                                    rGdvDerivados.ClientSettings.Selecting.AllowRowSelect = true;

                                    CveEliminar = 0;
                                }
                                else
                                {
                                    CantItemsElimFalse += 1;
                                    MsgItemsElimFalse = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                                }

                                MsgItemsElim = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                                ValEliminar = ds.Tables[1].Rows[0]["NFil"].ToString();

                                if (ValEliminar == "0")
                                {
                                    llenarGridDerivados();
                                    LlenaGridConceptos("");
                                }

                            }

                        }
                        catch (Exception ex)
                        {
                            //string MsgError = ex.Message.Trim();
                        }


                    }

                    CountItems += 1;
                }

                if (ValEliminar != "0")
                {
                    llenarGridDerivados();

                }

                //

                //if (ValEliminar == "0")
                //{

                //}

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
                    //InicioPagina();
                }
                else
                {
                    //LlenaGridAlmacenes();
                    //InicioPagina();
                }

            }

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());
        }

    }



    private void ObtieneValElimnar() 
    {
        if (rGdvConceptos.SelectedItems.Count > 0)
        {
            CveEliminar = 1;
        }
        
        if (rGdvDerivados.SelectedItems.Count > 0) {
            CveEliminar = 2;
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

            if (rCboDerivado.SelectedValue == "")
            {
                rCboDerivado.CssClass = "cssTxtInvalid";
                rCboDerivado.BorderWidth = Unit.Pixel(1);
                rCboDerivado.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {
                rCboDerivado.BorderWidth = Unit.Pixel(1);
                rCboDerivado.BorderColor = System.Drawing.Color.Transparent;
            }


            if (rCboOrigen.SelectedValue == "")
            {
                rCboOrigen.CssClass = "cssTxtInvalid";
                rCboOrigen.BorderWidth = Unit.Pixel(1);
                rCboOrigen.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {
                rCboOrigen.BorderWidth = Unit.Pixel(1);
                rCboOrigen.BorderColor = System.Drawing.Color.Transparent;
            }

            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
                rGdvDerivados.DataBind();
            }
            return sResult;
        }


        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            if (rGdvConceptos.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }
            if (rGdvDerivados.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            if (rCboDerivado.SelectedValue == "")
            {
                rCboDerivado.CssClass = "cssTxtInvalid";
                rCboDerivado.BorderWidth = Unit.Pixel(1);
                rCboDerivado.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {
                rCboDerivado.BorderWidth = Unit.Pixel(1);
                rCboDerivado.BorderColor = System.Drawing.Color.Transparent;
            }


            if (rCboOrigen.SelectedValue == "")
            {
                rCboOrigen.CssClass = "cssTxtInvalid";
                rCboOrigen.BorderWidth = Unit.Pixel(1);
                rCboOrigen.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {
                rCboOrigen.BorderWidth = Unit.Pixel(1);
                rCboOrigen.BorderColor = System.Drawing.Color.Transparent;
            }



            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

            if (rGdvConceptos.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }


            return sResult;
        }




        return sResult;
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
                //InicioPagina();
            }

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaSpAccionEliminar();
                rCboOrigen.ClearSelection();
                rCboDerivado.ClearSelection();
            }

        }
        else
        {
            ShowAlert(sMSGTip, msgValidacion);
        }
    }

    //private void ControlesAccion()
    //{

    //    rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
    //    rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
    //    rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";


    //    //===> CONTROLES POR ACCION
    //    //NUEVO
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
    //    {
    //        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
    //        this.rGdvConceptos.ClientSettings.Selecting.AllowRowSelect = true;

    //    }

    //    //MODIFICAR
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
    //    {
    //        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
    //        rGdvConceptos.ClientSettings.Selecting.AllowRowSelect = true;
    //    }

    //    //ELIMINAR
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
    //    {
    //        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminarSelected.png";
    //    }


    //    //INICIO / CANCELAR
    //    if (hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() &&
    //        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() &&
    //        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString() &&
    //           hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString()
    //          )
    //    {
    //    }


    //    //===> Botones GUARDAR - CANCELAR
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
    //        hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() ||
    //        hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString()
    //       )
    //    {
    //        rBtnGuardar.Enabled = true;
    //        rBtnCancelar.Enabled = true;
    //    }
    //    else
    //    {
    //        rBtnGuardar.Enabled = false;
    //        rBtnCancelar.Enabled = false;
    //    }
    //}

    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }
    
    #endregion
    
    #region METODOS

    private int LlenaGridConceptos(string filtroEditElim)
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MttoCptoDerivados";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.String, 20, ParameterDirection.Input, PagLoc_Concepto);
        ProcBD.AgregarParametrosProcedimiento("@FiltroCptoDer", DbType.String, 20, ParameterDirection.Input, filtroEditElim);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref rGdvConceptos, ds);

  
        if (ds.Tables.Count > 0)
        {
          return  ValDS = 0;
        }
        else
        {
            return ValDS = 1;
        }

    }

    private void btnLimpiar()
    {
        if (hdfBtnAccion.Value != "")
        {
            limpiarcss();
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {
                if (rCboDerivado.Enabled == true)
                {
                    rCboDerivado.ClearSelection();
                }
                if (rCboOrigen.Enabled == true)
                {
                    rCboOrigen.ClearSelection();
                    rCboOrigen.Enabled = false;
                }

                RadtxtBuscar.Text = "";
                txtBuscar();

            }
            else if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                
                if (LlenaGridConceptos(RadtxtBuscar.Text.Trim()) == 1)
                {
                    if (rCboOrigen.Enabled == true && rCboDerivado.Enabled == false)
                    {
                        combosds();
                    }
                }
                else
                {
                    //llenarGridDerivados();
                    rGdvDerivados.DataBind();
                    rCboDerivado.ClearSelection();

                    if (rCboOrigen.Enabled == true)
                    {
                        rCboOrigen.ClearSelection();
                        rCboOrigen.Enabled = false;
                    }

                    RadtxtBuscar.Text = "";
                    txtBuscar();
                }


            }
        }
        else
        {
            rGdvDerivados.DataBind();
            RadtxtBuscar.Text = "";
            txtBuscar();

        }
        
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            //llenarGridDerivados();
            //LlenaGridConceptos("");

            rCboOrigen.ClearSelection();
            rCboDerivado.ClearSelection();
            rCboOrigen.Enabled = false;
            rCboDerivado.Enabled = false;

            RadtxtBuscar.Text = "";
            txtBuscar();


            rGdvDerivados.ClientSettings.Selecting.AllowRowSelect = false;
        }





    }

    private void NuevoLLenaGridConceptos(string filtro)
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MttoCptoDerivados";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.String, 20, ParameterDirection.Input, PagLoc_Concepto);
        ProcBD.AgregarParametrosProcedimiento("@FiltroCptoDer", DbType.String, 20, ParameterDirection.Input, filtro);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref rGdvConceptos, ds);

    }

    public void llenarGridDerivados()
    {
        var dataItem = rGdvConceptos.SelectedItems[0] as GridDataItem;
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MttoCptoDerivados";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 54);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.String, 20, ParameterDirection.Input, PagLoc_Concepto);
        ProcBD.AgregarParametrosProcedimiento("@cptoDerCptoID", DbType.String, 20, ParameterDirection.Input, dataItem["cptoDerCptoID"].Text);

        //ProcBD.AgregarParametrosProcedimiento("@listTipDatoCptoCve", DbType.String, 10, ParameterDirection.Input, dataItem["listTipDatoCptoCve"].Text);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref rGdvDerivados, ds);
    }
    public void LimpiaGrid()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MttoCptoDerivados";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 54);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.String, 20, ParameterDirection.Input, PagLoc_Concepto);
        ProcBD.AgregarParametrosProcedimiento("@cptoDerCptoID", DbType.String, 20, ParameterDirection.Input, "");

        //ProcBD.AgregarParametrosProcedimiento("@listTipDatoCptoCve", DbType.String, 10, ParameterDirection.Input, dataItem["listTipDatoCptoCve"].Text);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref rGdvDerivados, ds);
    }
    public void combosds()
    {

        var dataItem = rGdvConceptos.SelectedItems[0] as GridDataItem;
        DataSet ds1 = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD1 = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD1.NombreProcedimiento = "sp_MttoCptoDerivados";
        ProcBD1.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 53);
        ProcBD1.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD1.AgregarParametrosProcedimiento("@cptoId", DbType.String, 20, ParameterDirection.Input, dataItem["cptoDerCptoID"].Text);
        ds1 = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD1.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        FnCtlsFillIn.RadComboBox(ref rCboDerivado, ds1, "unica", "cptoConfDes", true, false, "");
        ((Literal)rCboDerivado.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboDerivado.Items.Count);

        var dataItemDeri = rGdvDerivados.SelectedItems[0] as GridDataItem;
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MttoCptoDerivados";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 56);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.String, 20, ParameterDirection.Input, PagLoc_Concepto);
        ProcBD.AgregarParametrosProcedimiento("@cptoDerMapId", DbType.Int64, 10, ParameterDirection.Input, dataItemDeri["cptoDerMapId"].Text);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        
        rCboDerivado.SelectedValue = ds.Tables[0].Rows[0]["unica"].ToString();

        string tempo;
        string cadena = "";
        cadena = ds.Tables[0].Rows[0]["unica"].ToString();
        tempo = cadena.Substring(0, 7);

        DataSet ds2 = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD2 = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD2.NombreProcedimiento = "sp_MttoCptoDerivados";
        ProcBD2.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 55);
        ProcBD2.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD2.AgregarParametrosProcedimiento("@cptoId", DbType.String, 20, ParameterDirection.Input, PagLoc_Concepto);
        ProcBD2.AgregarParametrosProcedimiento("@listTipDatoCptoCve", DbType.String, 10, ParameterDirection.Input, tempo.Trim());
        ds2 = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD2.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        FnCtlsFillIn.RadComboBox(ref rCboOrigen, ds2, "cptoConfSec", "cptoConfDes", true, false, "");
        ((Literal)rCboOrigen.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboOrigen.Items.Count);

        rCboOrigen.SelectedValue = ds.Tables[0].Rows[0]["cptoConfSec"].ToString();

       // rCboOrigen.Enabled = true;
        //rCboDerivado.Enabled = true;
    }

    public void concepto()
    {


        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MttoCptoDerivados";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.String, 20, ParameterDirection.Input, PagLoc_Concepto);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        RadlabelOrigenID.Text = ds.Tables[0].Rows[0]["cptoid"].ToString();
        RadlabelOrigenDes.Text = ds.Tables[0].Rows[0]["cptoDes"].ToString();

    }

    public void limpiarcss()
    {
        rCboDerivado.BorderWidth = Unit.Pixel(1);
        rCboDerivado.BorderColor = System.Drawing.Color.Transparent;

        rCboOrigen.BorderWidth = Unit.Pixel(1);
        rCboOrigen.BorderColor = System.Drawing.Color.Transparent;

        RadtxtBuscar.CssClass = "cssTxtEnabled";
    }

    protected void txtBuscar()
    {
        rCboOrigen.CssClass = "cssTxtEnabled";

        rCboDerivado.Enabled = false;

        rGdvDerivados.DataBind();

        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString()
           || hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString()
           || hdfBtnAccion.Value == "")
        {
            LlenaGridConceptos(RadtxtBuscar.Text);
        }
        else if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            NuevoLLenaGridConceptos(RadtxtBuscar.Text);
        }

    }
    #endregion


    private void ControlesAccion()
    {
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL
        this.rGdvConceptos.ClientSettings.Selecting.AllowRowSelect = true;
        this.rGdvDerivados.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        rCboDerivado.CssClass = "cssTxtInvalid";
        rCboDerivado.BorderWidth = Unit.Pixel(1);
        rCboDerivado.BorderColor = System.Drawing.Color.Transparent;

        rCboDerivado.CssClass = "cssTxtInvalid";
        rCboOrigen.BorderWidth = Unit.Pixel(1);
        rCboOrigen.BorderColor = System.Drawing.Color.Transparent;


        this.rCboDerivado.Enabled = false;
        this.rCboOrigen.Enabled = false;

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
            this.rCboDerivado.Enabled = false;
            this.rCboOrigen.Enabled = false;
            
            this.rCboDerivado.ClearSelection();
            this.rCboOrigen.ClearSelection();

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
                this.rGdvConceptos.ClientSettings.Selecting.AllowRowSelect = true;
                rGdvConceptos.ClientSettings.Selecting.AllowRowSelect = true;
                rGdvConceptos.AllowMultiRowSelection = false;
                this.rGdvDerivados.ClientSettings.Selecting.AllowRowSelect = false;
                rGdvDerivados.MasterTableView.ClearSelectedItems();

                this.rCboDerivado.Enabled = true;
                this.rCboOrigen.Enabled = true;

                this.rCboDerivado.ClearSelection();
                this.rCboOrigen.ClearSelection();

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
                rGdvConceptos.AllowMultiRowSelection = false;
                rGdvDerivados.AllowMultiRowSelection = false;

                this.rCboDerivado.Enabled = false;
                this.rCboOrigen.Enabled = true;

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
                rGdvConceptos.ClientSettings.Selecting.AllowRowSelect = true;
                rGdvConceptos.AllowMultiRowSelection = true;
                rGdvConceptos.MasterTableView.ClearSelectedItems();

                rGdvDerivados.ClientSettings.Selecting.AllowRowSelect = true;
                rGdvDerivados.AllowMultiRowSelection = true;
                rGdvDerivados.MasterTableView.ClearSelectedItems();

                this.rCboDerivado.Enabled = false;
                this.rCboOrigen.Enabled = false;

                this.rCboOrigen.ClearSelection();
                this.rCboDerivado.ClearSelection();
            }
        }


        if (Result == false)
        {
            this.rCboDerivado.Enabled = false;
            this.rCboDerivado.Enabled = false;
            this.rCboDerivado.Enabled = false;

            this.rCboOrigen.Text = "";
            this.rCboOrigen.Text = "";
            this.rCboOrigen.Text = "";

            rGdvConceptos.ClientSettings.Selecting.AllowRowSelect = true;
            rGdvConceptos.AllowMultiRowSelection = true;
            rGdvConceptos.MasterTableView.ClearSelectedItems();


            rGdvDerivados.ClientSettings.Selecting.AllowRowSelect = true;
            rGdvDerivados.AllowMultiRowSelection = true;
            rGdvDerivados.MasterTableView.ClearSelectedItems();
        }


    }

    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = rGdvDerivados.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvDerivados, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvDerivados, GvVAS, ref sMSGTip, ref sResult) == false)
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
            this.rCboDerivado.ClearSelection();
            this.rCboOrigen.ClearSelection();
            rCboOrigen.Enabled = false;
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rGdvConceptos.ClientSettings.Selecting.AllowRowSelect = true;
            rGdvConceptos.MasterTableView.ClearSelectedItems();

            rGdvDerivados.ClientSettings.Selecting.AllowRowSelect = true;
            rGdvDerivados.MasterTableView.ClearSelectedItems();

            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

            rCboDerivado.CssClass = "cssTxtInvalid";
            rCboDerivado.BorderWidth = Unit.Pixel(1);
            rCboDerivado.BorderColor = System.Drawing.Color.Transparent;

            rCboOrigen.CssClass = "cssTxtInvalid";
            rCboOrigen.BorderWidth = Unit.Pixel(1);
            rCboOrigen.BorderColor = System.Drawing.Color.Transparent;

            this.rCboDerivado.Enabled = false;
            this.rCboOrigen.Enabled = false;

            rCboDerivado.ClearSelection();
            rCboOrigen.ClearSelection();

            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

        }

    }


    

    //private void ControlesAccion()
    //{
    //    string sMSGTip = "";
    //    string msgValidacion = "";

    //    //===> CONTROLES GENERAL
    //    this.rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
    //    rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
    //    rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
    //    rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

    //    rTxtCve.CssClass = "cssTxtEnabled";
    //    rTxtDes.CssClass = "cssTxtEnabled";
    //    rTxtAbr.CssClass = "cssTxtEnabled";
    //    this.rTxtCve.Enabled = false;
    //    this.rTxtDes.Enabled = false;
    //    this.rTxtAbr.Enabled = false;

    //    rBtnGuardar.Enabled = false;
    //    rBtnCancelar.Enabled = false;


    //    /*>>>>>>>>>>>>>>>>> CONTROLES POR ACCION <<<<<<<<<<<<<<<<<<  */
    //    //Validacion
    //    msgValidacion = ValidaControlesAccion_SelectRowGrid(ref sMSGTip);
    //    if (msgValidacion == "")
    //    {
    //        ControlesAccionEjecucion(true);
    //    }
    //    else
    //    {
    //        ControlesAccionEjecucion(false);
    //        ShowAlert(sMSGTip, msgValidacion);
    //    }

    //    //INICIO / CANCELAR
    //    if (hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() &&
    //    hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() &&
    //    hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString() &&
    //    hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString()
    //      )
    //    {
    //        this.rTxtCve.Enabled = false;
    //        this.rTxtDes.Enabled = false;
    //        this.rTxtAbr.Enabled = false;
    //        this.rTxtCve.Text = "";
    //        this.rTxtDes.Text = "";
    //        this.rTxtAbr.Text = "";
    //    }
    //}

    //private void ControlesAccionEjecucion(bool Result)
    //{
    //    if (Result == true)
    //    {
    //        //NUEVO
    //        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
    //        {
    //            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
    //            this.rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = false;
    //            rGdvInformacion.MasterTableView.ClearSelectedItems();
    //            this.rTxtCve.Enabled = true;
    //            this.rTxtDes.Enabled = true;
    //            this.rTxtAbr.Enabled = true;
    //            this.rTxtCve.Text = "";
    //            this.rTxtDes.Text = "";
    //            this.rTxtAbr.Text = "";
    //            rBtnGuardar.Enabled = true;
    //            rBtnCancelar.Enabled = true;
    //        }

    //        //MODIFICAR
    //        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
    //        {
    //            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
    //            rGdvInformacion.AllowMultiRowSelection = false;
    //            this.rTxtCve.Enabled = false;
    //            this.rTxtDes.Enabled = true;
    //            this.rTxtAbr.Enabled = true;
    //            rBtnGuardar.Enabled = true;
    //            rBtnCancelar.Enabled = true;
    //        }

    //        //ELIMIAR
    //        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
    //        {
    //            EjecutaAccion();
    //        }

    //        //LIMPIAR
    //        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString())
    //        {
    //            rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
    //            rGdvInformacion.AllowMultiRowSelection = true;
    //            rGdvInformacion.MasterTableView.ClearSelectedItems();
    //            this.rTxtCve.Enabled = false;
    //            this.rTxtDes.Enabled = false;
    //            this.rTxtAbr.Enabled = false;
    //            this.rTxtCve.Text = "";
    //            this.rTxtDes.Text = "";
    //            this.rTxtAbr.Text = "";
    //        }
    //    }


    //    if (Result == false)
    //    {
    //        this.rTxtCve.Enabled = false;
    //        this.rTxtDes.Enabled = false;
    //        this.rTxtAbr.Enabled = false;
    //        this.rTxtCve.Text = "";
    //        this.rTxtDes.Text = "";
    //        this.rTxtAbr.Text = "";
    //    }


    //}

    //private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    //{
    //    string sResult = "";
    //    int GvSelectItem = rGdvInformacion.SelectedItems.Count;
    //    string[] GvVAS;

    //    //NUEVO
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
    //    {
    //    }

    //    //MODIFICAR
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
    //    {
    //        GvVAS = new string[] { "VAL0003", "VAL0008" };
    //        if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvInformacion, GvVAS, ref sMSGTip, ref sResult) == false)
    //        {
    //            return sResult;
    //        }
    //    }

    //    //ELIMINAR
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
    //    {
    //        GvVAS = new string[] { "VAL0003" };
    //        if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvInformacion, GvVAS, ref sMSGTip, ref sResult) == false)
    //        {
    //            return sResult;
    //        }
    //    }
    //    return sResult;
    //}

    //private void EjecutaAccionLimpiar()
    //{
    //    //NUEVO
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
    //    {
    //        this.rTxtCve.Text = "";
    //        this.rTxtDes.Text = "";
    //        this.rTxtAbr.Text = "";
    //    }

    //    //MODIFICAR
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
    //    {
    //        rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
    //        rGdvInformacion.MasterTableView.ClearSelectedItems();

    //        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
    //        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
    //        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

    //        rTxtCve.CssClass = "cssTxtEnabled";
    //        rTxtDes.CssClass = "cssTxtEnabled";
    //        rTxtAbr.CssClass = "cssTxtEnabled";

    //        rTxtCve.Enabled = false;
    //        rTxtDes.Enabled = false;
    //        rTxtAbr.Enabled = false;

    //        rTxtCve.Text = "";
    //        rTxtDes.Text = "";
    //        rTxtAbr.Text = "";

    //        rBtnGuardar.Enabled = false;
    //        rBtnCancelar.Enabled = false;
    //    }

    //    //ELIMINAR
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
    //    {

    //    }
    //}
}