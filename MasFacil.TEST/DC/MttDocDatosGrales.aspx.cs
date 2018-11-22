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
using System.Configuration;

public partial class DC_MttDocDatosGrales : System.Web.UI.Page
{

    #region VARIABLES
    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    ws.Servicio oWS = new ws.Servicio();

    MGMFnGrales.FNGrales FNGrales = new MGMFnGrales.FNGrales();
    MGMControls.FillIn FnCtlsFillIn = new MGMControls.FillIn();
    MGMControls.RadWindows FnCtrlsRadWindows = new MGMControls.RadWindows();
    MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();
    MGMFnGrales.FNDatos FNDatos = new MGMFnGrales.FNDatos();

    

    private string Pag_sCompania;
    private string Pag_sConexionLog;
    private string Pag_sSessionLog;
    private string PagLoc_ArtCve;
    private string Pag_sidM;
    string CveFormatoDoc = "";
    #endregion

    #region EVENTOS

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (SM.IsActiveSession(this.Page))
        {
            Valores_InicioPag();
            addRadWin();
            if (!IsPostBack)
            {
                InicioPagina();
                //RadImageGallery1.DataSource = GetDataTable();
                //RadImageGallery1.DataBind();
                //RadImageGallery1.ImageAreaSettings.ShowNextPrevImageButtons = false;
                
            }
        }

    }


    protected void rGdv_Documentos_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (hdfBtnAccion.Value == "2" || hdfBtnAccion.Value == "")
        {

        rCboGenDoc1.ClearSelection();
        rCboGenDoc2.ClearSelection();
        rCboGenDoc3.ClearSelection();

        rCboDocumento1.ClearSelection();
        rCboDocumento1.ClearSelection();
        rCboDocumento1.ClearSelection();


        rCboInventarios.ClearSelection();
        rCboCuentasxCobrar.ClearSelection();
        rCboContabilidad.ClearSelection();


        var dataItem = rGdv_Documentos.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_DocDatosGrales";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, dataItem["docCve"].Text);
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);




            //rad_TxtdescArt.Text = Convert.ToString(ds.Tables[0].Rows[0]["artDes"]);


            rTxtDocCve.Text = dataItem["docCve"].Text;

            //rTxtDocDes.Text = dataItem["docDes"].Text;
            rTxtDocDes.Text = Convert.ToString(ds.Tables[0].Rows[0]["docDes"]);


            //rCboManejoFolio.SelectedValue = Convert.ToString(dataItem["docFolTip"]);
            rCboManejoFolio.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["docFolTip"]);

               




            //rCboCveFolio.Text = dataItem["docFol"].Text;
            RadNumCveFolio.Text = Convert.ToString(ds.Tables[0].Rows[0]["folVal"]);


            //check valida credito

            if (Convert.ToString(ds.Tables[0].Rows[0]["docValCred"]) == "1")
            {
                rBtnValCreYes.Checked = true;
                rBtnValCreNo.Checked = false;
                rBtnValCreAut.Checked = false;
            }
            else if (Convert.ToString(ds.Tables[0].Rows[0]["docValCred"]) == "2")
            {
                rBtnValCreNo.Checked = true;
                rBtnValCreYes.Checked = false;
                rBtnValCreAut.Checked = false;

            }
            else if (Convert.ToString(ds.Tables[0].Rows[0]["docValCred"]) == "3")
            {
                rBtnValCreAut.Checked = true;
                rBtnValCreNo.Checked = false;
                rBtnValCreYes.Checked = false;
            }


            if (Convert.ToString(ds.Tables[0].Rows[0]["docTip"]) == "1")
            {
                rBtnTipDocCre.Checked = true;
                rBtnTipDocDeb.Checked = false;
            }
            else
            //if (Convert.ToString(ds.Tables[0].Rows[0]["docTip"]) == "2")
            {
                rBtnTipDocDeb.Checked = true;
                rBtnTipDocCre.Checked = false;
            }




            if (Convert.ToString(ds.Tables[0].Rows[0]["docProcParc"]) == "1")
            {
                rBtnChkProPar.Checked = true;
            }
            else if (Convert.ToString(ds.Tables[0].Rows[0]["docProcParc"]) == "2")
            {
                rBtnChkProPar.Checked = false;
            }



            if (Convert.ToString(ds.Tables[0].Rows[0]["docDescGlb"]) == "1")
            {
                rBtnChkDescGlbl.Checked = true;
            }
            else if (Convert.ToString(ds.Tables[0].Rows[0]["docDescGlb"]) == "2")
            {
                rBtnChkDescGlbl.Checked = false;
            }

                if (hdfBtnAccion.Value == "2")
                {
                   
                    if (Convert.ToString(ds.Tables[0].Rows[0]["docFolTip"]) == "2")
                    {
                        RadNumCveFolio.Enabled = false;
                    }
                    else
                    {
                        RadNumCveFolio.Enabled = true;
                    }

                    if (Convert.ToString(ds.Tables[0].Rows[0]["docActCXC"]) == "1")
                    {
                        rBtnActCxcAplica.Checked = true;
                        rBtnActCxcNo.Checked = false;
                        rBtnActCxcGenera.Checked = false;
                        rCboCuentasxCobrar.Enabled = true;
                    }
                    else if (Convert.ToString(ds.Tables[0].Rows[0]["docActCXC"]) == "2")
                    {
                        rBtnActCxcNo.Checked = true;
                        rBtnActCxcAplica.Checked = false;
                        rBtnActCxcGenera.Checked = false;
                        rCboCuentasxCobrar.Enabled = false;
                    }
                    else if (Convert.ToString(ds.Tables[0].Rows[0]["docActCXC"]) == "3")
                    {
                        rBtnActCxcGenera.Checked = true;
                        rBtnActCxcAplica.Checked = false;
                        rBtnActCxcNo.Checked = false;
                        rCboCuentasxCobrar.Enabled = true;
                    }

                    if (Convert.ToString(ds.Tables[0].Rows[0]["docActInv"]) == "1")
                    {
                        rBtnActInvAplica.Checked = true;
                        rBtnActInvNo.Checked = false;
                        rBtnActInvGenera.Checked = false;
                        rCboInventarios.Enabled = true;
                    }
                    else if (Convert.ToString(ds.Tables[0].Rows[0]["docActInv"]) == "2")
                    {
                        rBtnActInvNo.Checked = true;
                        rBtnActInvAplica.Checked = false;
                        rBtnActInvGenera.Checked = false;
                        rCboInventarios.Enabled = false;
                    }
                    else if (Convert.ToString(ds.Tables[0].Rows[0]["docActInv"]) == "3")
                    {
                        rBtnActInvGenera.Checked = true;
                        rBtnActInvNo.Checked = false;
                        rBtnActInvAplica.Checked = false;
                        rCboInventarios.Enabled = true;

                    }

                    if (Convert.ToString(ds.Tables[0].Rows[0]["docActCont"]) == "1")
                    {
                        rBtnActContAplica.Checked = true;
                        rBtnActContNo.Checked = false;
                        rBtnActContGenera.Checked = false;
                        rCboContabilidad.Enabled = true;
                    }
                    else if (Convert.ToString(ds.Tables[0].Rows[0]["docActCont"]) == "2")
                    {
                        rBtnActContNo.Checked = true;
                        rBtnActContAplica.Checked = false;
                        rBtnActContGenera.Checked = false;
                        rCboContabilidad.Enabled = false;
                    }
                    else if (Convert.ToString(ds.Tables[0].Rows[0]["docActCont"]) == "3")
                    {

                        rBtnActContGenera.Checked = true;
                        rBtnActContNo.Checked = false;
                        rBtnActContAplica.Checked = false;
                        rCboContabilidad.Enabled = true;
                    }

                    if (Convert.ToString(ds.Tables[0].Rows[0]["docGenId1"]) == "2")
                    {
                        rCboDocumento1.Enabled = false;

                    }
                    if (Convert.ToString(ds.Tables[0].Rows[0]["docGenId2"]) == "2")
                    {
                        rCboDocumento2.Enabled = false;
                    }
                    if (Convert.ToString(ds.Tables[0].Rows[0]["docGenId3"]) == "2")
                    {
                        rCboDocumento3.Enabled = false;
                    }


                    if (Convert.ToString(ds.Tables[0].Rows[0]["docCve1"]) == "")
                    {
                        rCboDocumento1.ClearSelection();
                    }
                    else
                    {
                        //rCboDocumento1.SelectedValue = dataItem["docCve1"].Text;
                        rCboDocumento1.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["docCve1"]);
                        rCboDocumento1.Enabled = true;
                    }





                    if (Convert.ToString(ds.Tables[0].Rows[0]["docCve2"]) == "")
                    {
                        rCboDocumento2.ClearSelection();
                    }
                    else
                    {
                        //rCboDocumento2.SelectedValue = dataItem["docCve2"].Text;
                        rCboDocumento2.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["docCve2"]);
                        rCboDocumento2.Enabled = true;
                    }


                    if (Convert.ToString(ds.Tables[0].Rows[0]["docCve3"]) == "")
                    {
                        rCboDocumento3.ClearSelection();
                    }
                    else
                    {
                        //rCboDocumento3.SelectedValue = dataItem["docCve3"].Text;
                        rCboDocumento3.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["docCve3"]);
                        rCboDocumento3.Enabled = true;
                    }

                }


                ///////ACTUALIZA MODULOS/////
                //INVENTARIOS
                if (Convert.ToString(ds.Tables[0].Rows[0]["docActInv"]) == "1")
            {
                rBtnActInvAplica.Checked = true;
                rBtnActInvNo.Checked = false;
                rBtnActInvGenera.Checked = false;
            }
            else if (Convert.ToString(ds.Tables[0].Rows[0]["docActInv"]) == "2")
            {
                rBtnActInvNo.Checked = true;
                rBtnActInvAplica.Checked = false;
                rBtnActInvGenera.Checked = false;
            }
            else if (Convert.ToString(ds.Tables[0].Rows[0]["docActInv"]) == "3")
            {
                rBtnActInvGenera.Checked = true;
                rBtnActInvNo.Checked = false;
                rBtnActInvAplica.Checked = false;
            }

            rCboInventarios.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["cptoIdInv"]);


            //formato documento

            setFormatInGal(Convert.ToString(ds.Tables[0].Rows[0]["docFormCve"]));





            //CUENTAS POR COBRAR//
            if (Convert.ToString(ds.Tables[0].Rows[0]["docActCXC"]) == "1")
            {
                rBtnActCxcAplica.Checked = true;
                rBtnActCxcNo.Checked = false;
                rBtnActCxcGenera.Checked = false;
            }
            else if (Convert.ToString(ds.Tables[0].Rows[0]["docActCXC"]) == "2")
            {
                rBtnActCxcNo.Checked = true;
                rBtnActCxcAplica.Checked = false;
                rBtnActCxcGenera.Checked = false;
            }
            else if (Convert.ToString(ds.Tables[0].Rows[0]["docActCXC"]) == "3")
            {
                rBtnActCxcGenera.Checked = true;
                rBtnActCxcAplica.Checked = false;
                rBtnActCxcNo.Checked = false;
            }

            rCboCuentasxCobrar.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["cptoIdCXC"]);


            //CONTABILIDAD//
            if (Convert.ToString(ds.Tables[0].Rows[0]["docActCont"]) == "1")
            {
                rBtnActContAplica.Checked = true;
                rBtnActContNo.Checked = false;
                rBtnActContGenera.Checked = false;
            }
            else if (Convert.ToString(ds.Tables[0].Rows[0]["docActCont"]) == "2")
            {
                rBtnActContNo.Checked = true;
                rBtnActContAplica.Checked = false;
                rBtnActContGenera.Checked = false;
            }
            else if (Convert.ToString(ds.Tables[0].Rows[0]["docActCont"]) == "3")
            {

                rBtnActContGenera.Checked = true;
                rBtnActContNo.Checked = false;
                rBtnActContAplica.Checked = false;
            }


            rCboContabilidad.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["cptoIdCont"]);




            rCboGenDoc1.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["docGenId1"]);
            rCboGenDoc2.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["docGenId2"]);
            rCboGenDoc3.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["docGenId3"]);

            









            if (Convert.ToString(ds.Tables[0].Rows[0]["docCve1"]) == "")
            {
                rCboDocumento1.ClearSelection();
            }
            else
            {
                //rCboDocumento1.SelectedValue = dataItem["docCve1"].Text;
                rCboDocumento1.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["docCve1"]);
                    
            }





            if (Convert.ToString(ds.Tables[0].Rows[0]["docCve2"]) == "")
            {
                rCboDocumento2.ClearSelection();
            }
            else
            {
                //rCboDocumento2.SelectedValue = dataItem["docCve2"].Text;
                rCboDocumento2.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["docCve2"]);
                    
            }


            if (Convert.ToString(ds.Tables[0].Rows[0]["docCve3"]) == "")
            {
                rCboDocumento3.ClearSelection();
            }
            else
            {
                //rCboDocumento3.SelectedValue = dataItem["docCve3"].Text;
                rCboDocumento3.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["docCve3"]);
                  
            }





            //enableUi(2);

            /////SW//////

            if (Convert.ToString(ds.Tables[0].Rows[0]["docReqAut"]) == "1")
            {
                rBtnReqAut.Checked = true;
            }
            else if (Convert.ToString(ds.Tables[0].Rows[0]["docReqAut"]) == "2")
            {
                rBtnReqAut.Checked = false;
            }


            if (Convert.ToString(ds.Tables[0].Rows[0]["docValExis"]) == "1")
            {
                rBtnValExt.Checked = true;
            }
            else if (Convert.ToString(ds.Tables[0].Rows[0]["docValExis"]) == "2")
            {
                rBtnValExt.Checked = false;
            }



            dt_referencias.DataSource = llenadatalistVarRef(1);
            dt_referencias.DataBind();

            dt_variables.DataSource = llenadatalistVarRef(2);
            dt_variables.DataBind();





            rTxtDocCve.Enabled = false;
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {

                RadImageGallery1.Enabled = false;

            }
            else
            {

                RadImageGallery1.Enabled = false;
            }


            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                DataListDocFormatoRegOpcPantalla.Enabled = true;
            }
            else
            {
                DataListDocFormatoRegOpcPantalla.Enabled = false;
            }

            this.DataListDocFormatoRegOpcPantalla.DataSource = dsDocumentoFormatoRegistoOpcPantalla(50, dataItem["docCve"].Text);
            DataListDocFormatoRegOpcPantalla.DataBind();

            if (hdfBtnAccion.Value == "")
            {
                Desa_ValRef();
            }
            else
            {
                Habil_ValRef();
            }

        }
    }
    }




    protected void rBtnNuevo_Click(object sender, ImageButtonClickEventArgs e)
    {

        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();

        RadImageGallery1.ImageAreaSettings.ShowNextPrevImageButtons = true;
        RadImageGallery1.ImageAreaSettings.ShowDescriptionBox = true;
        ControlesAccion();
    }

    protected void rBtnModificar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString();
        RadImageGallery1.ImageAreaSettings.ShowNextPrevImageButtons = true;
        RadImageGallery1.ImageAreaSettings.ShowDescriptionBox = true;
        ControlesAccion();
    }

    protected void rBtnEliminar_Clicked(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString();
        RadImageGallery1.ImageAreaSettings.ShowNextPrevImageButtons = false;
        RadImageGallery1.ImageAreaSettings.ShowDescriptionBox = false;
        ControlesAccion();
    }

    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
    {
         funcionLimpiar();


    }

    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccion();
        //limpiartBTN();
    }

    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {
        InicioPagina();
        limpiartBTN();
        btnEstilos();
        hdfBtnAccion.Value = "";
        RadImageGallery1.ImageAreaSettings.ShowNextPrevImageButtons = false;
        RadImageGallery1.ImageAreaSettings.ShowDescriptionBox = false;
       



    }

    #endregion


    #region METODOS

    public void InicioPagina()
    {
        
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnLimpiar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnLimpiar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";

        LlenaGridDocumentos();
        //galeria de documentos
        RadImageGallery1.ImageAreaSettings.ShowNextPrevImageButtons = false;

        RadImageGallery1.ImageAreaSettings.ShowDescriptionBox = false;

        

        dt_referencias.DataSource = llenadatalistVarRef(1);
        dt_referencias.DataBind();

        dt_variables.DataSource = llenadatalistVarRef(2);
        dt_variables.DataBind();


        this.DataListDocFormatoRegOpcPantalla.DataSource = dsDocumentoFormatoRegistoOpcPantalla(51,"");
        DataListDocFormatoRegOpcPantalla.DataBind();
        DataListDocFormatoRegOpcPantalla.Enabled = false;

        llenar_cboFormatos();

        enableUi(1);
        rBtnGuardar.Enabled = false;
        rBtnCancelar.Enabled = false;
        limpiar(1);
        limpiartBTN();
        Limpiar_ValRef();

        rGdv_Documentos.ClientSettings.Selecting.AllowRowSelect = true;
        rGdv_Documentos.AllowMultiRowSelection = false;
        
        RadImageGallery1.DataSource = GetDataTable();
        RadImageGallery1.DataBind();

        rCboGenDoc1.SelectedValue = "2";
        rCboGenDoc2.SelectedValue = "2";
        rCboGenDoc3.SelectedValue = "2";

        rCboDocumento1.Enabled = false;
        rCboDocumento2.Enabled = false;
        rCboDocumento3.Enabled = false;





    }

    private void addRadWin()
    {
        FnCtrlsRadWindows.cRadWindowsCompanias(Page, ref RadWindowManagerPage);
    }

    private void LlenaGridDocumentos()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_DocDatosGrales";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        //ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, PagLoc_ArtCve);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref rGdv_Documentos, ds);
    }

    public void limpiar(int opclimpiar)
    {
        if (opclimpiar == 1)
        {
            rTxtDocCve.Text = "";
            rTxtDocDes.Text = "";
            rCboManejoFolio.ClearSelection();
            RadNumCveFolio.Text = "";

            rBtnReqAut.Checked = true;
            rBtnValExt.Checked = true;

            rBtnValCreYes.Checked = true;
            rBtnTipDocCre.Checked = true;
            rBtnChkProPar.Checked = true;
            rBtnChkDescGlbl.Checked = true;

            rCboGenDoc1.SelectedValue = "2";
            rCboGenDoc2.SelectedValue = "2";
            rCboGenDoc3.SelectedValue = "2";

            rCboDocumento1.Enabled = false;
            rCboDocumento2.Enabled = false;
            rCboDocumento3.Enabled = false;

            rBtnActInvAplica.Checked = true;
            rCboInventarios.ClearSelection();
            rBtnActCxcAplica.Checked = true;
            rCboCuentasxCobrar.ClearSelection();
            rBtnActContAplica.Checked = true;
            rCboContabilidad.ClearSelection();

        } else if (opclimpiar == 2)
        {
            
            rTxtDocDes.Text = "";
            rCboManejoFolio.ClearSelection();
            RadNumCveFolio.Text = "";

            rBtnReqAut.Checked = true;
            rBtnValExt.Checked = true;

            rBtnValCreYes.Checked = true;
            rBtnTipDocCre.Checked = true;
            rBtnChkProPar.Checked = true;
            rBtnChkDescGlbl.Checked = true;

            rCboGenDoc1.SelectedValue = "2";
            rCboGenDoc2.SelectedValue = "2";
            rCboGenDoc3.SelectedValue = "2";

            rCboDocumento1.Enabled = false;
            rCboDocumento2.Enabled = false;
            rCboDocumento3.Enabled = false;

            rBtnActInvAplica.Checked = true;
            rCboInventarios.ClearSelection();
            rBtnActCxcAplica.Checked = true;
            rCboCuentasxCobrar.ClearSelection();
            rBtnActContAplica.Checked = true;
            rCboContabilidad.ClearSelection();
        }
       

        


    }

    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
        if (Request.QueryString["idM"] != null && Request.QueryString["idM"] != "")
        {
            Pag_sidM = Request.QueryString["idM"];
        }
    }
    
    public void llenar_cboFormatos()
    {
        
        FnCtlsFillIn.RabComboBox_Modulos(Pag_sConexionLog, Pag_sCompania, ref rCboInventarios, true, false);
        FnCtlsFillIn.RabComboBox_Modulos(Pag_sConexionLog, Pag_sCompania, ref rCboCuentasxCobrar, true, false);
        FnCtlsFillIn.RabComboBox_Modulos(Pag_sConexionLog, Pag_sCompania, ref rCboContabilidad, true, false);
        FnCtlsFillIn.RadComboBox_ManejoFolios(Pag_sConexionLog, Pag_sCompania, ref rCboManejoFolio, true, false);


        FnCtlsFillIn.RabComboBox_GeneraDocumentos(Pag_sConexionLog, Pag_sCompania, ref rCboGenDoc1, true, false);
        FnCtlsFillIn.RabComboBox_GeneraDocumentos(Pag_sConexionLog, Pag_sCompania, ref rCboGenDoc2, true, false);
        FnCtlsFillIn.RabComboBox_GeneraDocumentos(Pag_sConexionLog, Pag_sCompania, ref rCboGenDoc3, true, false);
  

        FnCtlsFillIn.RabComboBox_Documentos(Pag_sConexionLog, Pag_sCompania, ref rCboDocumento1, true, false);
        FnCtlsFillIn.RabComboBox_Documentos(Pag_sConexionLog, Pag_sCompania, ref rCboDocumento2, true, false);
        FnCtlsFillIn.RabComboBox_Documentos(Pag_sConexionLog, Pag_sCompania, ref rCboDocumento3, true, false);

    }
    
    public void limpiartBTN()
    {
        //rBtnTogCodArt.ClearSelection();
        //rBtnTogSerLot.ClearSelection();
        //rBtnTogCveAlm.ClearSelection();
        //rBtnTogCantidad.ClearSelection();
        //rBtnTogPreUni.ClearSelection();
        //rBtnTogFchEnt.ClearSelection();
        //rBtnTogImpBru.ClearSelection();
        //rBtnTogImpDsc.ClearSelection();
        //rBtnTogImpPar.ClearSelection();

        //rBtnTogCodArt.Value = "2";
        //rBtnTogSerLot.Value = "2";
        //rBtnTogCveAlm.Value = "2";
        //rBtnTogCantidad.Value = "2";
        //rBtnTogPreUni.Value = "2";
        //rBtnTogFchEnt.Value = "2";
        //rBtnTogImpBru.Value = "2";
        //rBtnTogImpDsc.Value = "2";
        //rBtnTogImpPar.Value = "2";

        //rBtnTogCodArt.Text = "No";
        //rBtnTogSerLot.Text = "No";
        //rBtnTogCveAlm.Text = "No";
        //rBtnTogCantidad.Text = "No";
        //rBtnTogPreUni.Text = "No";
        //rBtnTogFchEnt.Text = "No";
        //rBtnTogImpBru.Text = "No";
        //rBtnTogImpDsc.Text = "No";
        //rBtnTogImpPar.Text = "No";

        //if (rBtnTogCodArt.Checked == true)
        //{
        //    rBtnTogCodArt.Checked = false;
        //} else if (rBtnTogSerLot.Checked == true)
        //{
        //    rBtnTogSerLot.Checked = false;

        //} else if (rBtnTogCveAlm.Checked == true)
        //{
        //    rBtnTogCveAlm.Checked = false;
        //} else if (rBtnTogCantidad.Checked == true)
        //{
        //    rBtnTogCantidad.Checked = false;
        //} else if (rBtnTogPreUni.Checked == true)
        //{
        //    rBtnTogPreUni.Checked = false;
        //} else if (rBtnTogFchEnt.Checked == true)
        //{
        //    rBtnTogFchEnt.Checked = false;

        //}else if (rBtnTogImpBru.Checked == true)
        //{
        //    rBtnTogImpBru.Checked = false;
        //}else if (rBtnTogImpDsc.Checked == true)
        //{
        //    rBtnTogImpDsc.Checked = false;
        //}else if (rBtnTogImpPar.Checked==true)
        //{
        //    rBtnTogImpPar.Checked=false;

        //}



        //rBtnTogCodArt.Checked = false;
        //rBtnTogSerLot.Checked = false;
        //rBtnTogCveAlm.Checked = false;
        //rBtnTogCantidad.Checked = false;
        //rBtnTogPreUni.Checked = false;
        //rBtnTogFchEnt.Checked = false;
        //rBtnTogImpBru.Checked = false;
        //rBtnTogImpDsc.Checked = false;
        //rBtnTogImpPar.Checked = false;











    }


    private void ControlesAccion()
    {

        //===> CONTROLES POR ACCION
        // NUEVO 

        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {

            rBtnNuevo.Image.Url = "~/Images/IcoBotones/IcoBtnNuevo.png";
            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

            
            enableUi(2);
            rGdv_Documentos.ClientSettings.Selecting.AllowRowSelect = false;
            rGdv_Documentos.AllowMultiRowSelection = false;

            rBtnGuardar.Enabled = true;
            rBtnCancelar.Enabled = true;
            rTxtDocCve.Enabled = true;
            rBtnLimpiar.Enabled = true;
            limpiar(1);
            limpiartBTN();
            Limpiar_ValRef();
            LlenaGridDocumentos();
            llenar_cboFormatos();

            rCboGenDoc1.SelectedValue = "2";
            rCboGenDoc2.SelectedValue = "2";
            rCboGenDoc3.SelectedValue = "2";

            rCboDocumento1.Enabled = false;
            rCboDocumento2.Enabled = false;
            rCboDocumento3.Enabled = false;

            rTxtDocCve.CssClass = "cssTxtEnabled";
            rTxtDocDes.CssClass = "cssTxtEnabled";
            RadNumCveFolio.CssClass = "cssTxtEnabled";


            this.DataListDocFormatoRegOpcPantalla.DataSource = dsDocumentoFormatoRegistoOpcPantalla(52, "");
            DataListDocFormatoRegOpcPantalla.DataBind();
            DataListDocFormatoRegOpcPantalla.Enabled = true;
            Habil_ValRef();
            
        }



        //MODIFICAR
        else if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {

            limpiar(1);
            Limpiar_ValRef();
            Desa_ValRef();
            enableUi(2);
            LlenaGridDocumentos();
            rGdv_Documentos.ClientSettings.Selecting.AllowRowSelect = true;
            rGdv_Documentos.AllowMultiRowSelection = false;
            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
            rBtnGuardar.Enabled = true;
            rBtnCancelar.Enabled = true;
            rBtnLimpiar.Enabled = true;
            rTxtDocCve.Enabled = false;
            llenar_cboFormatos();
            limpiartBTN();
            Habil_ValRef();

            RadImageGallery1.Enabled = true;

            rTxtDocCve.CssClass = "cssTxtEnabled";
            rTxtDocDes.CssClass = "cssTxtEnabled";
            RadNumCveFolio.CssClass = "cssTxtEnabled";


        }

        //ELIMINAR
        else if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

            rGdv_Documentos.AllowMultiRowSelection = true;
            rGdv_Documentos.ClientSettings.Selecting.AllowRowSelect = true;
            LlenaGridDocumentos();
            limpiartBTN();
            enableUi(1);
            limpiar(1);
            Limpiar_ValRef();
            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminarSelected.png";
            rBtnGuardar.Enabled = true;
            rBtnCancelar.Enabled = true;
            rTxtDocCve.Enabled = false;
            DataListDocFormatoRegOpcPantalla.Enabled = false;
            rTxtDocCve.CssClass = "cssTxtEnabled";
            rTxtDocDes.CssClass = "cssTxtEnabled";
            RadNumCveFolio.CssClass = "cssTxtEnabled";


        }

        else if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString())
        {
            //Codigo boton Limpiar
            rTxtDocCve.Enabled = false;
            btnEstilos();
            

        }
        else
        {
            //===> Botones GUARDAR - CANCELAR
            if(hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
                hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() ||
                hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString()
               )
            {
            rBtnGuardar.Enabled = true;
            rBtnCancelar.Enabled = true;
            hdfBtnAccion.Value = "";

            }
            else
            {
                Desa_ValRef();
            }
        }


    }



    public void btnEstilos()
    {
        rTxtDocCve.CssClass = "cssTxtEnabled";
        rTxtDocDes.CssClass = "cssTxtEnabled";
        RadNumCveFolio.CssClass = "cssTxtEnabled";
    }




    public void Limpiar_ValRef()
    {
        foreach (DataListItem dli in dt_referencias.Items)
        {
            var references = dli.FindControl("txt_ref") as RadTextBox;
            references.Text = "";
        }

        foreach (DataListItem dli in dt_variables.Items)
        {
            var references = dli.FindControl("txt_var") as RadNumericTextBox;
            references.Text = "";
        }
    }


    public void valida_vaciosRV()
    {
        int valores=0;
        foreach (DataListItem dli in dt_referencias.Items)
        {
            var references = dli.FindControl("txt_ref") as RadTextBox;

            if (references.Text != "")
            {
                valores += 1;
            }
        }

        foreach (DataListItem dli in dt_variables.Items)
        {
            var references = dli.FindControl("txt_var") as RadNumericTextBox;
            if (references.Text != "")
            {
                valores += 1;
            }

        }
        if (valores != 0 )
        {
            //EjecutaSpRefVar();
            EjecutaSpRefVar();
        }
    }

    private void EjecutaSpRefVar()
    {



        try
        {

            foreach (DataListItem dli in dt_referencias.Items)
            {
                Int32 secRef;

                var valRef = dli.FindControl("txt_ref") as RadTextBox;
                secRef = dli.ItemIndex + 1;
                DataSet ds = new DataSet();

                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                ProcBD.NombreProcedimiento = "sp_DocDatosGralesRefVar";

                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                ProcBD.AgregarParametrosProcedimiento("@DocCve", DbType.String, 20, ParameterDirection.Input, rTxtDocCve.Text);
                ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                ProcBD.AgregarParametrosProcedimiento("@revaCve", DbType.String, 5, ParameterDirection.Input, "DOC");
                ProcBD.AgregarParametrosProcedimiento("@revaTip", DbType.Int32, 1, ParameterDirection.Input, 1);
                ProcBD.AgregarParametrosProcedimiento("@revaValRef", DbType.String, 15, ParameterDirection.Input, valRef.Text);
                ProcBD.AgregarParametrosProcedimiento("@revasec", DbType.Int32, 1, ParameterDirection.Input, secRef);

                ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            }




            foreach (DataListItem dli in dt_variables.Items)
            {
                Int32 secRef;

                var valVar = dli.FindControl("txt_var") as RadNumericTextBox;
                secRef = dli.ItemIndex + 1;

                //MessageBox.Show(references.Text);

                DataSet ds = new DataSet();

                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                ProcBD.NombreProcedimiento = "sp_DocDatosGralesRefVar";

                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                ProcBD.AgregarParametrosProcedimiento("@DocCve", DbType.String, 20, ParameterDirection.Input, rTxtDocCve.Text);
                ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                ProcBD.AgregarParametrosProcedimiento("@revaCve", DbType.String, 5, ParameterDirection.Input, "DOC");
                ProcBD.AgregarParametrosProcedimiento("@revaTip", DbType.Int32, 1, ParameterDirection.Input, 2);
                ProcBD.AgregarParametrosProcedimiento("@revasec", DbType.Int32, 1, ParameterDirection.Input, secRef);
                ///
                if (valVar.Text == "")
                {
                    valVar.Text = "0.00";
                    ProcBD.AgregarParametrosProcedimiento("@revaValVar", DbType.Decimal, 15, ParameterDirection.Input, Convert.ToDecimal(valVar.Text));
                }
                else
                {
                    ProcBD.AgregarParametrosProcedimiento("@revaValVar", DbType.Decimal, 15, ParameterDirection.Input, Convert.ToDecimal(valVar.Text));
                }

                
                

                ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            }


        }


        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());
            
        }

    }


    private void EjecutaSpFormatoRegistroOpcPantalla() {
        int Countkeyarray = 0;
        foreach (DataListItem dli in DataListDocFormatoRegOpcPantalla.Items)
        {
            var valrBtn = dli.FindControl("rBtnTog") as RadButton;
            string DocOpcPantCve = DataListDocFormatoRegOpcPantalla.DataKeys[Countkeyarray].ToString();
            bool docFormRegSit = valrBtn.Checked;
            int idocFormRegSit;
            if (docFormRegSit==true )
            {
                idocFormRegSit = 1;
            }
            else {
                idocFormRegSit = 2;
            }



            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_DocumentosFormatoRegOpcPantalla";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 2);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@DocCve", DbType.String, 10, ParameterDirection.Input, rTxtDocCve.Text);
            ProcBD.AgregarParametrosProcedimiento("@DocOpcPantCve", DbType.String, 15, ParameterDirection.Input, DocOpcPantCve);
            ProcBD.AgregarParametrosProcedimiento("@docFormRegSit", DbType.Int64, 0, ParameterDirection.Input, idocFormRegSit);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            
            Countkeyarray += 1;
        }



    }



    private void enableUi(int opc)
    {
        //Formato Nuevo - Todo Habilitado
        if (opc == 1)
        {
            rTxtDocCve.Enabled = false;
            rTxtDocDes.Enabled = false;
            rCboManejoFolio.Enabled = false;
            RadNumCveFolio.Enabled = false;
            rBtnReqAut.Enabled = false;
            rBtnValExt.Enabled = false;
            rBtnValCreYes.Enabled = false;
            rBtnValCreNo.Enabled = false;
            rBtnValCreAut.Enabled = false;
            rBtnTipDocCre.Enabled = false;
            rBtnTipDocDeb.Enabled = false;
            rBtnChkProPar.Enabled = false;
            rBtnChkDescGlbl.Enabled = false;
            //Doc Der
            rCboGenDoc1.Enabled = false;
            rCboDocumento1.Enabled = false;
            rCboGenDoc2.Enabled = false;
            rCboDocumento2.Enabled = false;
            rCboGenDoc3.Enabled = false;
            rCboDocumento3.Enabled = false;
            rBtnActInvAplica.Enabled = false;
            rBtnActInvNo.Enabled = false;
            rBtnActInvGenera.Enabled = false;
            rCboInventarios.Enabled = false;
            rBtnActCxcAplica.Enabled = false;
            rBtnActCxcNo.Enabled = false;
            rBtnActCxcGenera.Enabled = false;
            rCboCuentasxCobrar.Enabled = false;
            rBtnActContAplica.Enabled = false;
            rBtnActContNo.Enabled = false;
            rBtnActContGenera.Enabled = false;
            rCboContabilidad.Enabled = false;
            Desa_ValRef();
            //DesRefVar

        }
        else if (opc == 2)
        {
            rTxtDocCve.Enabled = true;
            rTxtDocDes.Enabled = true;
            rCboManejoFolio.Enabled = true;
            RadNumCveFolio.Enabled = true;
            rBtnReqAut.Enabled = true;
            rBtnValExt.Enabled = true;
            rBtnValExt.Enabled = true;
            rBtnValCreYes.Enabled = true;
            rBtnValCreNo.Enabled = true;
            rBtnValCreAut.Enabled = true;
            rBtnTipDocCre.Enabled = true;
            rBtnTipDocDeb.Enabled = true;
            rBtnChkProPar.Enabled = true;
            rBtnChkDescGlbl.Enabled = true;
            //Doc Der
            rCboGenDoc1.Enabled = true;
            rCboDocumento1.Enabled = true;
            rCboGenDoc2.Enabled = true;
            rCboDocumento2.Enabled = true;
            rCboGenDoc3.Enabled = true;
            rCboDocumento3.Enabled = true;

            rBtnActInvAplica.Enabled = true;
            rBtnActInvNo.Enabled = true;
            rBtnActInvGenera.Enabled = true;
            rCboInventarios.Enabled = true;
            rBtnActCxcAplica.Enabled = true;
            rBtnActCxcNo.Enabled = true;
            rBtnActCxcGenera.Enabled = true;
            rCboCuentasxCobrar.Enabled = true;
            rBtnActContAplica.Enabled = true;
            rBtnActContNo.Enabled = true;
            rBtnActContGenera.Enabled = true;
            rCboContabilidad.Enabled = true;



        }
        else if (opc == 3)
        {

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

    public void Desa_ValRef()
    {
        foreach (DataListItem dli in dt_referencias.Items)
        {
            var references = dli.FindControl("txt_ref") as RadTextBox;
            references.Enabled = false;
        }


        foreach (DataListItem dli in dt_variables.Items)
        {
            var references = dli.FindControl("txt_var") as RadNumericTextBox;
            references.Enabled = false;
        }
    }
    public void Habil_ValRef()
    {
        foreach (DataListItem dli in dt_referencias.Items)
        {
            var references = dli.FindControl("txt_ref") as RadTextBox;
            references.Enabled = true;
        }


        foreach (DataListItem dli in dt_variables.Items)
        {
            var references = dli.FindControl("txt_var") as RadNumericTextBox;
            references.Enabled = true;
        }
    }


    private void EjecutaSpAcciones()
    {
        //Variable
        CveFormatoDoc = getValueFormDoc();

        //Pasar A funcion de Lectura

        int prmActInv = 0;
        int prmActCxc = 0;
        int prmActCon = 0;
        int rBtnValCre = 0;
        int rBtnTipDoc = 0;
        int rBtnChkPro = 0;
        int rBtnChkDesc = 0;
        int rBtnValExisten = 0;
        int rBtnReqAutoriza = 0;
        
        if (rBtnActInvAplica.Checked == true)
        {
            prmActInv = 1;
        }
        else if (rBtnActInvNo.Checked == true)
        {
            prmActInv = 2;
        }
        else if (rBtnActInvGenera.Checked == true)
        {
            prmActInv = 3;
        }

        if (rBtnActCxcAplica.Checked == true)
        {
            prmActCxc = 1;
        }
        else if (rBtnActCxcNo.Checked == true)
        {
            prmActCxc = 2;
        }
        else if (rBtnActCxcGenera.Checked == true)
        {
            prmActCxc = 3;
        }

        if (rBtnActContAplica.Checked == true)
        {
            prmActCon = 1;
        }
        else if (rBtnActContNo.Checked == true)
        {
            prmActCon = 2;
        }
        else if (rBtnActContGenera.Checked == true)
        {
            prmActCon = 3;
        }
        ////rBtnTipDocCre
        if (rBtnValCreYes.Checked == true)
        {
            rBtnValCre = 1;
        }
        else if (rBtnValCreNo.Checked == true)
        {
            rBtnValCre = 2;
        }
        else if (rBtnValCreAut.Checked == true)
        {
            rBtnValCre = 3;
        }

        if (rBtnTipDocCre.Checked == true)
        {
            rBtnTipDoc = 1;
        }
        else if (rBtnTipDocDeb.Checked == true)
        {
            rBtnTipDoc = 2;
        }

        if (rBtnChkProPar.Checked == true)
        {
            rBtnChkPro = 1;
        }
        else
        {
            rBtnChkPro = 2;
        }

        if (rBtnChkDescGlbl.Checked == true)
        {
            rBtnChkDesc = 1;
        }
        else
        {
            rBtnChkDesc = 2;
        }

        ///////////////////////////////////////REQUIERE AUTORIZACION VALIDA EXITENCIAS///////////////////////////////////

        if (rBtnValExt.Checked == true)
        {
            rBtnValExisten = 1;
        }
        else
        {
            rBtnValExisten = 2;
        }

        if (rBtnReqAut.Checked == true)
        {
            rBtnReqAutoriza = 1;
        }
        else
        {
            rBtnReqAutoriza = 2;
        }



        try
        {
            DataSet ds = new DataSet();

            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_DocDatosGrales";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 1, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, rTxtDocCve.Text);
            ProcBD.AgregarParametrosProcedimiento("@docDes", DbType.String, 50, ParameterDirection.Input, rTxtDocDes.Text);

            ////AQUI VA LA CLAVE DEL DOCUMENTO////

            ProcBD.AgregarParametrosProcedimiento("@formImpCve", DbType.String, 10, ParameterDirection.Input, CveFormatoDoc);

            ProcBD.AgregarParametrosProcedimiento("@docFolTip", DbType.Int64, 1, ParameterDirection.Input, rCboManejoFolio.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@docReqAut", DbType.Int64, 1, ParameterDirection.Input, rBtnReqAutoriza);
            ProcBD.AgregarParametrosProcedimiento("@docValExis", DbType.Int64, 1, ParameterDirection.Input, rBtnValExisten);

            ////Actualiza Modulos 

            ProcBD.AgregarParametrosProcedimiento("@docActInv", DbType.Int64, 1, ParameterDirection.Input, prmActInv);
            
            ProcBD.AgregarParametrosProcedimiento("@docActCXC", DbType.Int64, 1, ParameterDirection.Input, prmActCxc);
            
            ProcBD.AgregarParametrosProcedimiento("@docActCont", DbType.Int64, 1, ParameterDirection.Input, prmActCon);
            ProcBD.AgregarParametrosProcedimiento("@docValCred", DbType.Int64, 1, ParameterDirection.Input, rBtnValCre);
            ProcBD.AgregarParametrosProcedimiento("@docTip", DbType.Int64, 1, ParameterDirection.Input, rBtnTipDoc);
            ProcBD.AgregarParametrosProcedimiento("@docProcParc", DbType.Int64, 1, ParameterDirection.Input, rBtnChkPro);
            ProcBD.AgregarParametrosProcedimiento("@docDescGlb", DbType.Int64, 1, ParameterDirection.Input, rBtnChkDesc);
            //folio marca error al ser ingresado con ""
            if (RadNumCveFolio.Text != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@docFol", DbType.String, 10, ParameterDirection.Input, RadNumCveFolio.Text);
            }

            if (rBtnActContNo.Checked == false)
            {
                if (rCboContabilidad.SelectedValue == "")
                {

                }
                else
                {
                    ProcBD.AgregarParametrosProcedimiento("@cptoIdCont", DbType.Int64, 0, ParameterDirection.Input, rCboContabilidad.SelectedValue);
                }

            }
            
            if (rBtnActCxcNo.Checked==false)
            {
                if (rCboCuentasxCobrar.SelectedValue == "")
                {

                }
                else
                {
                    ProcBD.AgregarParametrosProcedimiento("@cptoIdCXC", DbType.Int64, 0, ParameterDirection.Input, rCboCuentasxCobrar.SelectedValue);
                }

            }
            
            if (rBtnActInvNo.Checked == false)
            {
                if (rCboInventarios.SelectedValue == "")
                {

                }
                else
                {
                    ProcBD.AgregarParametrosProcedimiento("@cptoIdInv", DbType.Int64, 0, ParameterDirection.Input, rCboInventarios.SelectedValue);
                }

            }
            




            if (rCboGenDoc1.SelectedValue == "")
            {

            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@docGenId1", DbType.Int64, 1, ParameterDirection.Input, rCboGenDoc1.SelectedValue);
            }
            if (rCboGenDoc2.SelectedValue=="")
            {
                
            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@docGenId2", DbType.Int64, 1, ParameterDirection.Input, rCboGenDoc2.SelectedValue);
            }
            if (rCboGenDoc3.SelectedValue == "")
            {

            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@docGenId3", DbType.Int64, 1, ParameterDirection.Input, rCboGenDoc3.SelectedValue);
            }





            if (rCboDocumento1.SelectedValue == "")
            {

            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@docCve1", DbType.String, 10, ParameterDirection.Input, rCboDocumento1.SelectedValue);
            }
            if (rCboDocumento2.SelectedValue == "")
            {

            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@docCve2", DbType.String, 10, ParameterDirection.Input, rCboDocumento2.SelectedValue);
            }

            if (rCboDocumento3.SelectedValue == "")
            {

            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@docCve3", DbType.String, 10, ParameterDirection.Input, rCboDocumento3.SelectedValue);
            }

            
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                ShowAlert(sEjecEstatus, sEjecMSG);

                if (sEjecEstatus == "1")
                {

                    EjecutaSpFormatoRegistroOpcPantalla();
                    //EjecutaSpRefVar();
                    valida_vaciosRV();
                    InicioPagina();
                    limpiartBTN();
                    hdfBtnAccion.Value = "";
                    DataListDocFormatoRegOpcPantalla.Enabled = false;
                    RadImageGallery1.ImageAreaSettings.ShowNextPrevImageButtons = false;
                    RadImageGallery1.ImageAreaSettings.ShowDescriptionBox = false;


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
        try
        {

            int CountItems = 0;
            int CantItemsElimTrue = 0;
            int CantItemsElimFalse = 0;
            string EstatusItemsElim = "";
            string MsgItemsElim = "";
            string MsgItemsElimTrue = "";
            string MsgItemsElimFalse = "";




            foreach (GridDataItem i in rGdv_Documentos.SelectedItems)
            {

                var dataItem = rGdv_Documentos.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {


                    try
                    {

                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_DocDatosGrales";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 3);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                        ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, dataItem["docCve"].Text);
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
                    //LlenaGridAlmacenes();
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
                    //LlenaGridAlmacenes();
                    InicioPagina();
                }

            }

        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }

        hdfBtnAccion.Value = "";

    }

    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }
    #endregion

    #region FUNCIONES

    private DataSet llenadatalistVarRef(Int32 revaTip)
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Varia_Ref";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, (1), ParameterDirection.Input, 56);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 20, ParameterDirection.Input, rTxtDocCve.Text);
        ProcBD.AgregarParametrosProcedimiento("@revaCve", DbType.String, (5), ParameterDirection.Input, "DOC");
        ProcBD.AgregarParametrosProcedimiento("@revaTip", DbType.Int32, (5), ParameterDirection.Input, revaTip);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        return ds;
    }


    private DataSet dsDocumentoFormatoRegistoOpcPantalla( int opc, string sDocCve) {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_DocumentosFormatoRegOpcPantalla";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, opc);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@DocCve", DbType.String, 10, ParameterDirection.Input, sDocCve);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        return ds;
    }


    private string validaEjecutaAccion(ref string sMSGTip)
    {

        btnEstilos();
        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";
        


        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            //CLAVE DOCUMENTO
            if (rTxtDocCve.Text.Trim() == "")
            {
                rTxtDocCve.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtDocCve.CssClass = "cssTxtEnabled"; }
            //DESCRIPCION 
            if (rTxtDocDes.Text.Trim() == "")
            {
                rTxtDocDes.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtDocDes.CssClass = "cssTxtEnabled"; }
            //FOLIO
            if (rCboManejoFolio.SelectedValue == "")
            {
                rCboManejoFolio.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else if (rCboManejoFolio.SelectedValue == "1")
            {
                if (RadNumCveFolio.Text == "")
                {
                    RadNumCveFolio.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
            }


            if (rBtnActInvAplica.Checked==true)
            {
                if (rCboInventarios.SelectedValue=="")
                {
                    rCboInventarios.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
                
            }else if (rBtnActInvGenera.Checked==true)
            {
                if (rCboInventarios.SelectedValue == "")
                {
                    rCboInventarios.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
                
            }

            if (rBtnActCxcAplica.Checked ==true)
            {
                if (rCboCuentasxCobrar.SelectedValue == "")
                {
                    rCboInventarios.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }

            }else if (rBtnActCxcGenera.Checked == true)
            {
                if (rCboCuentasxCobrar.SelectedValue == "")
                {
                    rCboInventarios.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }

            }

            if (rBtnActContAplica.Checked == true)
            {
                if (rCboContabilidad.SelectedValue == "")
                {
                    rCboContabilidad.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
            }else if (rBtnActContGenera.Checked == true)
            {
                if (rCboContabilidad.SelectedValue == "")
                {
                    rCboContabilidad.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
            }


            //////////////////////////////DOCUMENTOS DERIVADOS////////////////////////////////////////
            if (rCboDocumento1.Enabled==true)
            {
                if (rCboDocumento1.SelectedValue == "")
                {
                    rCboDocumento1.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
            }
            if (rCboDocumento2.Enabled == true)
            {
                if (rCboDocumento2.SelectedValue == "")
                {
                    rCboDocumento2.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
            }

            if (rCboDocumento3.Enabled == true)
            {
                if (rCboDocumento3.SelectedValue == "")
                {
                    rCboDocumento3.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
            }






            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
               
            }
            return sResult;
        }


        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {

            if (rGdv_Documentos.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }
            //CLAVE DOCUMENTO
            if (rTxtDocCve.Text.Trim() == "")
            {
                rTxtDocCve.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtDocCve.CssClass = "cssTxtEnabled"; }
            //DESCRIPCION 
            if (rTxtDocDes.Text.Trim() == "")
            {
                rTxtDocDes.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtDocDes.CssClass = "cssTxtEnabled"; }
            //FOLIO
            if (rCboManejoFolio.SelectedValue == "")
            {
                rCboManejoFolio.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else if (rCboManejoFolio.SelectedValue == "1")
            {
                if (RadNumCveFolio.Text == "")
                {
                    RadNumCveFolio.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
            }

            

            if (rBtnActInvAplica.Checked == true)
            {
                if (rCboInventarios.SelectedValue == "")
                {
                    rCboInventarios.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }

            }
            else if (rBtnActInvGenera.Checked == true)
            {
                if (rCboInventarios.SelectedValue == "")
                {
                    rCboInventarios.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }

            }

            if (rBtnActCxcAplica.Checked == true)
            {
                if (rCboCuentasxCobrar.SelectedValue == "")
                {
                    rCboInventarios.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }

            }
            else if (rBtnActCxcGenera.Checked == true)
            {
                if (rCboCuentasxCobrar.SelectedValue == "")
                {
                    rCboInventarios.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }

            }

            if (rBtnActContAplica.Checked == true)
            {
                if (rCboContabilidad.SelectedValue == "")
                {
                    rCboContabilidad.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
            }
            else if (rBtnActContGenera.Checked == true)
            {
                if (rCboContabilidad.SelectedValue == "")
                {
                    rCboContabilidad.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
            }


            //////////////////////////////DOCUMENTOS DERIVADOS////////////////////////////////////////
            if (rCboDocumento1.Enabled == true)
            {
                if (rCboDocumento1.SelectedValue == "")
                {
                    rCboDocumento1.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
            }
            if (rCboDocumento2.Enabled == true)
            {
                if (rCboDocumento2.SelectedValue == "")
                {
                    rCboDocumento2.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
            }

            if (rCboDocumento3.Enabled == true)
            {
                if (rCboDocumento3.SelectedValue == "")
                {
                    rCboDocumento3.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
            }





            ////CLAVE DOCUMENTO
            //if (rTxtDocCve.Text.Trim() == "")
            //{
            //    rTxtDocCve.CssClass = "cssTxtInvalid";
            //    camposInc += 1;
            //}
            //else { rTxtDocCve.CssClass = "cssTxtEnabled"; }
            ////DESCRIPCION 
            //if (rTxtDocDes.Text.Trim() == "")
            //{
            //    rTxtDocDes.CssClass = "cssTxtInvalid";
            //    camposInc += 1;
            //}
            //else { rTxtDocDes.CssClass = "cssTxtEnabled"; }
            ////FOLIO
            //if (rCboManejoFolio.SelectedValue == "")
            //{
            //    rCboManejoFolio.CssClass = "cssTxtInvalid";
            //    camposInc += 1;
            //}
            //else
            //{
            //    rCboManejoFolio.CssClass = "cssTxtEnabled";

            //    if (rCboManejoFolio.SelectedValue != "2")
            //    {

            //        RadNumCveFolio.CssClass = "cssTxtInvalid";
            //        camposInc += 1;
            //    }

            //}


            if (rCboInventarios.Enabled == true)
            {
                if (rCboInventarios.SelectedValue == "")
                {
                    rCboInventarios.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
            }

            if (rCboCuentasxCobrar.Enabled == true)
            {
                if (rCboCuentasxCobrar.SelectedValue == "")
                {
                    rCboCuentasxCobrar.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
            }

            if (rCboContabilidad.Enabled == true)
            {
                if (rCboContabilidad.SelectedValue == "")
                {
                    rCboContabilidad.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
            }




            //////////////////////////////DOCUMENTOS DERIVADOS////////////////////////////////////////
            if (rCboDocumento1.Enabled == true)
            {
                if (rCboDocumento1.SelectedValue == "")
                {
                    rCboDocumento1.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
            }
            if (rCboDocumento2.Enabled == true)
            {
                if (rCboDocumento2.SelectedValue == "")
                {
                    rCboDocumento2.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
            }

            if (rCboDocumento3.Enabled == true)
            {
                if (rCboDocumento3.SelectedValue == "")
                {
                    rCboDocumento3.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
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

            if (rGdv_Documentos.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }


            return sResult;
        }




        return sResult;
    }


    public void funcionLimpiar()
    {
        if (hdfBtnAccion.Value == "")
        {
            
        }
        else if (hdfBtnAccion.Value == "1")
        {
            Limpiar_ValRef();
            limpiar(1);
            limpiartBTN();
            llenar_cboFormatos();
            btnEstilos();
            enableUi(2);
            rCboGenDoc1.SelectedValue = "2";
            rCboGenDoc2.SelectedValue = "2";
            rCboGenDoc3.SelectedValue = "2";

            rCboDocumento1.Enabled = false;
            rCboDocumento2.Enabled = false;
            rCboDocumento3.Enabled = false;


        }
        else if (hdfBtnAccion.Value == "2")
        {
            if (rTxtDocCve.Text == "")
            {
                Limpiar_ValRef();
                limpiar(2);
                limpiartBTN();
                llenar_cboFormatos();
                btnEstilos();
                enableUi(2);
                rTxtDocCve.Enabled = false;

                rCboGenDoc1.SelectedValue = "2";
                rCboGenDoc2.SelectedValue = "2";
                rCboGenDoc3.SelectedValue = "2";

                rCboDocumento1.Enabled = false;
                rCboDocumento2.Enabled = false;
                rCboDocumento3.Enabled = false;

            }
            else
            {

           
            Limpiar_ValRef();
            limpiar(2);
            limpiartBTN();
            llenar_cboFormatos();
            btnEstilos();
            enableUi(2);
            rTxtDocCve.Enabled = false;
            
            rCboGenDoc1.SelectedValue = "2";
            rCboGenDoc2.SelectedValue = "2";
            rCboGenDoc3.SelectedValue = "2";

            rCboDocumento1.Enabled = false;
            rCboDocumento2.Enabled = false;
            rCboDocumento3.Enabled = false;
            traeDatos();

            }

    }
    else if (hdfBtnAccion.Value == "3")
    {
            LlenaGridDocumentos();
    }
        

       



    }

    private string getValueFormDoc()
    {


      
        int contador = 0;


        foreach (ImageGalleryItem item in RadImageGallery1.Items)
        {



            if (RadImageGallery1.CurrentItemIndex == contador)
            {

                return item.Title;
            }

            contador++;
            }

            return "";

    }
    #endregion

    public void traeDatos()
    {


        //{


        bool rBtnReqAutS;
        bool rBtnValExtS;
        bool rBtnTogCodArtS;
        bool rBtnTogSerLotS;
        bool rBtnTogCveAlmS;
        bool rBtnTogCantidadS;
        bool rBtnTogPreUniS;
        bool rBtnTogFchEntS;
        bool rBtnTogImpBruS;
        bool rBtnTogImpDscS;
        bool rBtnTogImpParS;


        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_DocDatosGrales";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, rTxtDocCve.Text);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);



        //rTxtDocDes.Text = dataItem["docDes"].Text;
        rTxtDocDes.Text = Convert.ToString(ds.Tables[0].Rows[0]["docDes"]);
        

        //rCboManejoFolio.SelectedValue = Convert.ToString(dataItem["docFolTip"]);
        rCboManejoFolio.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["docFolTip"]);
        enableUi(2);
        if (hdfBtnAccion.Value == "2")
        {
            if (Convert.ToString(ds.Tables[0].Rows[0]["docFolTip"]) == "2")
            {
                RadNumCveFolio.Enabled = false;
            }
            else
            {
                RadNumCveFolio.Enabled = true;
            }

            if (Convert.ToString(ds.Tables[0].Rows[0]["docActCXC"]) == "1")
            {
                rBtnActCxcAplica.Checked = true;
                rBtnActCxcNo.Checked = false;
                rBtnActCxcGenera.Checked = false;
                rCboCuentasxCobrar.Enabled = true;
            }
            else if (Convert.ToString(ds.Tables[0].Rows[0]["docActCXC"]) == "2")
            {
                rBtnActCxcNo.Checked = true;
                rBtnActCxcAplica.Checked = false;
                rBtnActCxcGenera.Checked = false;
                rCboCuentasxCobrar.Enabled = false;
            }
            else if (Convert.ToString(ds.Tables[0].Rows[0]["docActCXC"]) == "3")
            {
                rBtnActCxcGenera.Checked = true;
                rBtnActCxcAplica.Checked = false;
                rBtnActCxcNo.Checked = false;
                rCboCuentasxCobrar.Enabled = true;
            }

            if (Convert.ToString(ds.Tables[0].Rows[0]["docActInv"]) == "1")
            {
                rBtnActInvAplica.Checked = true;
                rBtnActInvNo.Checked = false;
                rBtnActInvGenera.Checked = false;
                rCboInventarios.Enabled = true;
            }
            else if (Convert.ToString(ds.Tables[0].Rows[0]["docActInv"]) == "2")
            {
                rBtnActInvNo.Checked = true;
                rBtnActInvAplica.Checked = false;
                rBtnActInvGenera.Checked = false;
                rCboInventarios.Enabled = false;
            }
            else if (Convert.ToString(ds.Tables[0].Rows[0]["docActInv"]) == "3")
            {
                rBtnActInvGenera.Checked = true;
                rBtnActInvNo.Checked = false;
                rBtnActInvAplica.Checked = false;
                rCboInventarios.Enabled = true;

            }

            if (Convert.ToString(ds.Tables[0].Rows[0]["docActCont"]) == "1")
            {
                rBtnActContAplica.Checked = true;
                rBtnActContNo.Checked = false;
                rBtnActContGenera.Checked = false;
                rCboContabilidad.Enabled = true;
            }
            else if (Convert.ToString(ds.Tables[0].Rows[0]["docActCont"]) == "2")
            {
                rBtnActContNo.Checked = true;
                rBtnActContAplica.Checked = false;
                rBtnActContGenera.Checked = false;
                rCboContabilidad.Enabled = false;
            }
            else if (Convert.ToString(ds.Tables[0].Rows[0]["docActCont"]) == "3")
            {

                rBtnActContGenera.Checked = true;
                rBtnActContNo.Checked = false;
                rBtnActContAplica.Checked = false;
                rCboContabilidad.Enabled = true;
            }
            if (Convert.ToString(ds.Tables[0].Rows[0]["docGenId1"]) == "2")
            {
                rCboDocumento1.Enabled = false;

            }
            else
            {
                rCboDocumento1.Enabled = true;
            }

            if (Convert.ToString(ds.Tables[0].Rows[0]["docGenId2"]) == "2")
            {
                rCboDocumento2.Enabled = false;
            }
            else
            {
                rCboDocumento2.Enabled = true;
            }
            if (Convert.ToString(ds.Tables[0].Rows[0]["docGenId3"]) == "2")
            {
                rCboDocumento3.Enabled = false;
            }
            else
            {
                rCboDocumento3.Enabled = true;
            }

        }




        //rCboCveFolio.Text = dataItem["docFol"].Text;
        RadNumCveFolio.Text = Convert.ToString(ds.Tables[0].Rows[0]["folVal"]);


        //check valida credito

        if (Convert.ToString(ds.Tables[0].Rows[0]["docValCred"]) == "1")
        {
            rBtnValCreYes.Checked = true;
            rBtnValCreNo.Checked = false;
            rBtnValCreAut.Checked = false;
        }
        else if (Convert.ToString(ds.Tables[0].Rows[0]["docValCred"]) == "2")
        {
            rBtnValCreNo.Checked = true;
            rBtnValCreYes.Checked = false;
            rBtnValCreAut.Checked = false;

        }
        else if (Convert.ToString(ds.Tables[0].Rows[0]["docValCred"]) == "3")
        {
            rBtnValCreAut.Checked = true;
            rBtnValCreNo.Checked = false;
            rBtnValCreYes.Checked = false;
        }


        if (Convert.ToString(ds.Tables[0].Rows[0]["docTip"]) == "1")
        {
            rBtnTipDocCre.Checked = true;
            rBtnTipDocDeb.Checked = false;
        }
        else
        //if (Convert.ToString(ds.Tables[0].Rows[0]["docTip"]) == "2")
        {
            rBtnTipDocDeb.Checked = true;
            rBtnTipDocCre.Checked = false;
        }




        if (Convert.ToString(ds.Tables[0].Rows[0]["docProcParc"]) == "1")
        {
            rBtnChkProPar.Checked = true;
        }
        else if (Convert.ToString(ds.Tables[0].Rows[0]["docProcParc"]) == "2")
        {
            rBtnChkProPar.Checked = false;
        }



        if (Convert.ToString(ds.Tables[0].Rows[0]["docDescGlb"]) == "1")
        {
            rBtnChkDescGlbl.Checked = true;
        }
        else if (Convert.ToString(ds.Tables[0].Rows[0]["docDescGlb"]) == "2")
        {
            rBtnChkDescGlbl.Checked = false;
        }

        ///////ACTUALIZA MODULOS/////
        //INVENTARIOS
        if (Convert.ToString(ds.Tables[0].Rows[0]["docActInv"]) == "1")
        {
            rBtnActInvAplica.Checked = true;
            rBtnActInvNo.Checked = false;
            rBtnActInvGenera.Checked = false;
        }
        else if (Convert.ToString(ds.Tables[0].Rows[0]["docActInv"]) == "2")
        {
            rBtnActInvNo.Checked = true;
            rBtnActInvAplica.Checked = false;
            rBtnActInvGenera.Checked = false;
        }
        else if (Convert.ToString(ds.Tables[0].Rows[0]["docActInv"]) == "3")
        {
            rBtnActInvGenera.Checked = true;
            rBtnActInvNo.Checked = false;
            rBtnActInvAplica.Checked = false;

        }

        rCboInventarios.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["cptoIdInv"]);



        //CUENTAS POR COBRAR//
        if (Convert.ToString(ds.Tables[0].Rows[0]["docActCXC"]) == "1")
        {
            rBtnActCxcAplica.Checked = true;
            rBtnActCxcNo.Checked = false;
            rBtnActCxcGenera.Checked = false;
        }
        else if (Convert.ToString(ds.Tables[0].Rows[0]["docActCXC"]) == "2")
        {
            rBtnActCxcNo.Checked = true;
            rBtnActCxcAplica.Checked = false;
            rBtnActCxcGenera.Checked = false;
        }
        else if (Convert.ToString(ds.Tables[0].Rows[0]["docActCXC"]) == "3")
        {
            rBtnActCxcGenera.Checked = true;
            rBtnActCxcAplica.Checked = false;
            rBtnActCxcNo.Checked = false;
        }

        rCboCuentasxCobrar.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["cptoIdCXC"]);


        //CONTABILIDAD//
        if (Convert.ToString(ds.Tables[0].Rows[0]["docActCont"]) == "1")
        {
            rBtnActContAplica.Checked = true;
            rBtnActContNo.Checked = false;
            rBtnActContGenera.Checked = false;
        }
        else if (Convert.ToString(ds.Tables[0].Rows[0]["docActCont"]) == "2")
        {
            rBtnActContNo.Checked = true;
            rBtnActContAplica.Checked = false;
            rBtnActContGenera.Checked = false;
        }
        else if (Convert.ToString(ds.Tables[0].Rows[0]["docActCont"]) == "3")
        {

            rBtnActContGenera.Checked = true;
            rBtnActContNo.Checked = false;
            rBtnActContAplica.Checked = false;
        }

        rCboContabilidad.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["cptoIdCont"]);


        setFormatInGal(Convert.ToString(ds.Tables[0].Rows[0]["docFormCve"]));





        rCboGenDoc1.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["docGenId1"]);
        rCboGenDoc2.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["docGenId2"]);
        rCboGenDoc3.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["docGenId3"]);

       



        
        /////SW//////

        if (Convert.ToString(ds.Tables[0].Rows[0]["docReqAut"]) == "1")
        {
            rBtnReqAut.Checked = true;
        }
        else if (Convert.ToString(ds.Tables[0].Rows[0]["docReqAut"]) == "2")
        {
            rBtnReqAut.Checked = false;
        }


        if (Convert.ToString(ds.Tables[0].Rows[0]["docValExis"]) == "1")
        {
            rBtnValExt.Checked = true;
        }
        else if (Convert.ToString(ds.Tables[0].Rows[0]["docValExis"]) == "2")
        {
            rBtnValExt.Checked = false;
        }

 

        dt_referencias.DataSource = llenadatalistVarRef(1);
        dt_referencias.DataBind();

        dt_variables.DataSource = llenadatalistVarRef(2);
        dt_variables.DataBind();
        rTxtDocCve.Enabled = false;


      

        //}
    }

    private void setFormatInGal(string frmCve)
    {

        int contador = 0;


        foreach (ImageGalleryItem item in RadImageGallery1.Items)
        {

            if (item.Title == frmCve)
            {
                RadImageGallery1.CurrentItemIndex = contador;
            }
            else
            {
                //RadImageGallery1.CurrentItemIndex = 0;
            }

            contador++;
        }

        //RadImageGallery1.CurrentItemIndex = 1;
    }
 
    protected void RadImageGallery1_NeedDataSource(object sender, ImageGalleryNeedDataSourceEventArgs e)
    {
      
        
    }

    public DataView GetDataTable()
    {
        
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_DocDatosGrales";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, (1), ParameterDirection.Input, 52);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        DataView dataView = new DataView(ds.Tables[0]);
        //dataView.Sort = "docFormArch DESC";
        return dataView;

    }

    protected void rCboGenDoc1_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_DocumentoGenera";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, (1), ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@docGenId", DbType.String, 10, ParameterDirection.Input, rCboGenDoc1.SelectedValue);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        
        if (Convert.ToString(ds.Tables[0].Rows[0]["docGenSit"]) == "2")
        {
            rCboDocumento1.ClearSelection();
            rCboDocumento1.Enabled = false;

        }else
        {
            rCboDocumento1.Enabled = true;
        }




    }

    protected void rCboGenDoc2_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_DocumentoGenera";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, (1), ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@docGenId", DbType.String, 10, ParameterDirection.Input, rCboGenDoc2.SelectedValue);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (Convert.ToString(ds.Tables[0].Rows[0]["docGenSit"]) == "2")
        {
            rCboDocumento2.Enabled = false;
            rCboDocumento2.ClearSelection();
        }
        else
        {
            rCboDocumento2.Enabled = true;
        }
    }

    protected void rCboGenDoc3_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_DocumentoGenera";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, (1), ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@docGenId", DbType.String, 10, ParameterDirection.Input, rCboGenDoc3.SelectedValue);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (Convert.ToString(ds.Tables[0].Rows[0]["docGenSit"]) == "2")
        {
            rCboDocumento3.Enabled = false;
            rCboDocumento3.ClearSelection();
        }
        else
        {
            rCboDocumento3.Enabled = true;
        }
    }



    protected void rBtnActInvAplica_CheckedChanged(object sender, EventArgs e)
    {
        //rCboInventarios.Enabled = true;
        rCboInventarios_OpcChek();
    }
    protected void rBtnActInvNo_CheckedChanged(object sender, EventArgs e)
    {
        //rCboInventarios.Enabled = false;
        //rCboInventarios.ClearSelection();
        rCboInventarios_OpcChek();
    }
    protected void rBtnActInvGenera_CheckedChanged(object sender, EventArgs e)
    {
        //rCboInventarios.Enabled = true;
        rCboInventarios_OpcChek();
    }
    private void rCboInventarios_OpcChek() {

        if (rBtnActInvAplica.Checked == true && rBtnActInvNo.Checked ==false && rBtnActInvGenera.Checked== false) {
            rCboInventarios.Enabled = true;
            rCboInventarios.SelectedIndex = -1;
        }

        if (rBtnActInvAplica.Checked == false && rBtnActInvNo.Checked == true  && rBtnActInvGenera.Checked == false)
        {
            rCboInventarios.ClearSelection();
            rCboInventarios.Enabled = false;
        }


        if (rBtnActInvAplica.Checked == false  && rBtnActInvNo.Checked == false && rBtnActInvGenera.Checked == true )
        {
            rCboInventarios.Enabled = true;
            rCboInventarios.SelectedIndex = -1;
        }
    }




    protected void rBtnActCxcAplica_CheckedChanged(object sender, EventArgs e)
    {
        //rCboCuentasxCobrar.Enabled = true;
        rCboCXC_OpcChek();
    }

    protected void rBtnActCxcNo_CheckedChanged(object sender, EventArgs e)
    {
        //rCboCuentasxCobrar.Enabled = false;
        //rCboCuentasxCobrar.ClearSelection();
        rCboCXC_OpcChek();
    }

    protected void rBtnActCxcGenera_CheckedChanged(object sender, EventArgs e)
    {
        //rCboCuentasxCobrar.Enabled = true;
        rCboCXC_OpcChek();
    }

    private void rCboCXC_OpcChek()
    {

        if (rBtnActCxcAplica.Checked == true && rBtnActCxcNo.Checked == false && rBtnActCxcGenera.Checked == false)
        {
            rCboCuentasxCobrar.Enabled = true;
            rCboCuentasxCobrar.SelectedIndex = -1;
        }

        if (rBtnActCxcAplica.Checked == false && rBtnActCxcNo.Checked == true && rBtnActCxcGenera.Checked == false)
        {
            
            rCboCuentasxCobrar.ClearSelection();
            rCboCuentasxCobrar.Enabled = false;
        }


        if (rBtnActCxcAplica.Checked == false && rBtnActCxcNo.Checked == false && rBtnActCxcGenera.Checked == true)
        {
            rCboCuentasxCobrar.Enabled = true;
            rCboCuentasxCobrar.SelectedIndex = -1;
        }
    }






    protected void rBtnActContAplica_CheckedChanged(object sender, EventArgs e)
    {
        //rCboContabilidad.Enabled = true;
        rCboCont_OpcChek();
    }

    protected void rBtnActContNo_CheckedChanged(object sender, EventArgs e)
    {
        //rCboContabilidad.Enabled = false;
        //rCboContabilidad.ClearSelection();
        rCboCont_OpcChek();
    }

    protected void rBtnActContGenera_CheckedChanged(object sender, EventArgs e)
    {
        //rCboContabilidad.Enabled = true;
        rCboCont_OpcChek();
    }

    private void rCboCont_OpcChek()
    {

        if (rBtnActContAplica.Checked == true && rBtnActContNo.Checked == false && rBtnActContGenera.Checked == false)
        {
            rCboContabilidad.Enabled = true;
            rCboContabilidad.SelectedIndex = -1;
        }

        if (rBtnActContAplica.Checked == false && rBtnActContNo.Checked == true && rBtnActContGenera.Checked == false)
        {
            rCboContabilidad.ClearSelection();
            rCboContabilidad.Enabled = false;
            
        }


        if (rBtnActContAplica.Checked == false && rBtnActContNo.Checked == false && rBtnActContGenera.Checked == true)
        {
            rCboContabilidad.Enabled = true;
            rCboContabilidad.SelectedIndex = -1;
        }
    }








    protected void rCboManejoFolio_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        
        if (rCboManejoFolio.SelectedValue != "2")
        {
            RadNumCveFolio.Enabled = true; 
            RadNumCveFolio.CssClass = "cssTxtEnabled";
            RadNumCveFolio.Text = "";
        }
        else if (rCboManejoFolio.SelectedValue == "2")
        {
            RadNumCveFolio.Enabled = false;
            RadNumCveFolio.CssClass = "cssTxtEnabled";
            RadNumCveFolio.Text = "";
        }
        
    }

  
   

}