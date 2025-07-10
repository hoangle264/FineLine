using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisionDesigner.PreproMask;
using VisionDesigner.SurfaceDefectFilter;
using VisionDesigner;
using System.IO;
using Demo_VisionMaster.ParseXML;

namespace Demo_VisionMaster.Views
{
    public partial class SurfaceConfigView : Form
    {
        private Stopwatch _StopWatch = new Stopwatch();
        private CSurfaceDefectFilterTool _SurfaceDefectFilterTool = null;
        private CPreproMaskTool _MaskTool = null;
        private CMdvXmlParseTool_Surface _XmlParseTool = null;
        private CMvdImage _InputImage = null;
        private CMvdShape _ROI = null;
        private List<CMvdShape> _MaskShapeList = new List<CMvdShape>();
        public SurfaceConfigView()
        {
            InitializeComponent();
        }

        private void btnInitTool_Click(object sender, EventArgs e)
        {
            try
            {
                _SurfaceDefectFilterTool = new CSurfaceDefectFilterTool();

                byte[] fileBytes = new byte[256];
                uint nConfigDataSize = 256;
                uint nConfigDataLen = 0;
                try
                {
                    _SurfaceDefectFilterTool.SaveConfiguration(fileBytes, nConfigDataSize, ref nConfigDataLen);
                }
                catch (MvdException ex)
                {
                    if (MVD_ERROR_CODE.MVD_E_NOENOUGH_BUF == ex.ErrorCode)
                    {
                        fileBytes = new byte[nConfigDataLen];
                        nConfigDataSize = nConfigDataLen;
                        _SurfaceDefectFilterTool.SaveConfiguration(fileBytes, nConfigDataSize, ref nConfigDataLen);
                    }
                    else
                    {
                        throw ex;
                    }
                }
                UpdateParamList(fileBytes, nConfigDataLen);

                _MaskTool = new CPreproMaskTool();

                this.btnInitTool.Enabled = false;
                this.btnDeInit.Enabled = true;
                this.btnRunTool.Enabled = true;
                this.btnExpXml.Enabled = true;
                this.btnImpXml.Enabled = true;
                this.rtbInfoMessage.Text += "Init finish.\r\n";
            }
            catch (MvdException ex)
            {
                this.rtbInfoMessage.Text += "Fail to initialize the tool. ErrorCode: 0x" + ex.ErrorCode.ToString("X") + "\r\n";
            }
            catch (System.Exception ex)
            {
                this.rtbInfoMessage.Text += "Fail with error " + ex.Message + "\r\n";
            }
        }

        private void btnDeInit_Click(object sender, EventArgs e)
        {
            try
            {
                if (null != _ROI)
                {
                    mvdRenderActivexIn.MVD_DeleteShape(_ROI);
                    _ROI = null;
                }

                foreach (var item in _MaskShapeList)
                {
                    mvdRenderActivexIn.MVD_DeleteShape(item);
                }
                _MaskShapeList.Clear();

                if (null != _SurfaceDefectFilterTool)
                {
                    _SurfaceDefectFilterTool.Dispose();
                    _SurfaceDefectFilterTool = null;
                }

                dataGridView1.Rows.Clear();
                _XmlParseTool.ClearXmlBuf();

                if (null != _MaskTool)
                {
                    _MaskTool.Dispose();
                    _MaskTool = null;
                }

                this.btnDeInit.Enabled = false;
                this.btnRunTool.Enabled = false;
                this.btnExpXml.Enabled = false;
                this.btnImpXml.Enabled = false;
                this.btnInitTool.Enabled = true;
                this.rtbInfoMessage.Text += "DeInit finish.\r\n";
            }
            catch (MvdException ex)
            {
                this.rtbInfoMessage.Text += "An error occurred while releasing the resource. ErrorCode: 0x" + ex.ErrorCode.ToString("X") + "\r\n";
            }
            catch (System.Exception ex)
            {
                this.rtbInfoMessage.Text += "An error occurred while releasing the resource with ' " + ex.Message + " '\r\n";
            }
            finally
            {
                mvdRenderActivexIn.MVD_Refresh();
                mvdRenderActivexOut.MVD_Refresh();
            }
        }

        private void btnLoadImg_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog fileDlg = null;
            try
            {
                fileDlg = new System.Windows.Forms.OpenFileDialog();
                fileDlg.Filter = @"image(*.bmp;*.jpeg;*.jpg;*.png)|*.bmp;*.jpeg;*.jpg;*.png||";
                fileDlg.RestoreDirectory = true;

                if (fileDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (null == _InputImage)
                    {
                        _InputImage = new CMvdImage();

                    }
                    _InputImage.InitImage(fileDlg.FileName);
                    MVD_ERROR_CODE nErrorCode = MVD_ERROR_CODE.MVD_OK;
                    if (Convert.ToInt32(nErrorCode) != mvdRenderActivexIn.MVD_LoadImageFromMvdImage(_InputImage))
                    {
                        throw new MvdException(MVD_MODULE_TYPE.MVD_MODUL_RENDER, nErrorCode, "MvdRenderControl error");
                    }
                    mvdRenderActivexIn.MVD_Refresh();

                    /* Shapes on the canvas are cleared by the render activeX when different images are switched */
                    _MaskShapeList.Clear();
                    _ROI = null;

                    this.rtbInfoMessage.Text += "Finish loading image from [" + fileDlg.FileName + "].\r\n";
                }
                fileDlg.Dispose();
            }
            catch (MvdException ex)
            {
                this.rtbInfoMessage.Text += "Fail to load image from [" + fileDlg.FileName + "]. ErrorCode: 0x" + ex.ErrorCode.ToString("X") + "\r\n";
            }
            catch (System.Exception ex)
            {
                this.rtbInfoMessage.Text += "Fail to load image from [" + fileDlg.FileName + "]. Error: " + ex.Message + "\r\n";
            }
            finally
            {
                if (null != fileDlg)
                {
                    fileDlg.Dispose();
                }
            }
        }

        private void btnRunTool_Click(object sender, EventArgs e)
        {
            try
            {
                if ((null == _SurfaceDefectFilterTool) || (null == _InputImage))
                {
                    throw new MvdException(MVD_MODULE_TYPE.MVD_MODUL_TOOL, MVD_ERROR_CODE.MVD_E_CALLORDER);
                }

                _SurfaceDefectFilterTool.InputImage = _InputImage;
                _SurfaceDefectFilterTool.InputROI = _ROI;

                _SurfaceDefectFilterTool.RegionImage = null;
                _SurfaceDefectFilterTool.RegionImageAssistant.ROIList.Clear();

                if (_MaskShapeList.Count > 0)
                {
                    _MaskTool.RegionListEx.Clear();
                    _MaskTool.InputImage = _InputImage;
                    if (null != _ROI)
                    {
                        _MaskTool.RegionListEx.Add(new Tuple<CMvdShape, MVD_REGION_TYPE>(_ROI, MVD_REGION_TYPE.MvdRegionTypeRoi));
                        _SurfaceDefectFilterTool.RegionImageAssistant.ROIList.Add(_ROI);
                    }
                    foreach (var shape in _MaskShapeList)
                    {
                        _MaskTool.RegionListEx.Add(new Tuple<CMvdShape, MVD_REGION_TYPE>(shape, MVD_REGION_TYPE.MvdRegionTypeMask));
                    }
                    _MaskTool.Run();
                    _SurfaceDefectFilterTool.RegionImage = _MaskTool.OutputImage;
                }

                float[] weight = new float[] { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f };
                weight[0] = Convert.ToSingle(this.txtWeight0.Text.ToString());
                weight[1] = Convert.ToSingle(this.txtWeight30.Text.ToString());
                weight[2] = Convert.ToSingle(this.txtWeight60.Text.ToString());
                weight[3] = Convert.ToSingle(this.txtWeight90.Text.ToString());
                weight[4] = Convert.ToSingle(this.txtWeight120.Text.ToString());
                weight[5] = Convert.ToSingle(this.txtWeight150.Text.ToString());
                _SurfaceDefectFilterTool.BasicParam.WeightData = weight;
                _SurfaceDefectFilterTool.BasicParam.Offset = Convert.ToInt32(this.txtOffset.Text.ToString());

                _StopWatch.Reset();
                _StopWatch.Start();
                _SurfaceDefectFilterTool.Run();
                _StopWatch.Stop();
                this.rtbInfoMessage.Text += "Running cost: " + _StopWatch.ElapsedMilliseconds.ToString("#0.000") + "\r\n";
                this.rtbInfoMessage.Text += "Image binary success." + "\r\n";

                var outputImage = _SurfaceDefectFilterTool.Result.FilterImage;
                MVD_ERROR_CODE nErrorCode = MVD_ERROR_CODE.MVD_OK;
                var content = mvdRenderActivexOut.MVD_LoadImageFromMvdImage(outputImage);
                if (Convert.ToInt32(nErrorCode) != mvdRenderActivexOut.MVD_LoadImageFromMvdImage(outputImage))
                {
                    throw new MvdException(MVD_MODULE_TYPE.MVD_MODUL_RENDER, nErrorCode, "MvdRenderControl error");
                }
                outputImage.SaveImage("C:\\Users\\Dell\\Desktop\\VisionParket\\VisionParket\\Image\\Temp.bmp");

            }
            catch (MvdException ex)
            {
                this.rtbInfoMessage.Text += "An error occurred while running the tool. ErrorCode: 0x" + ex.ErrorCode.ToString("X") + "\r\n";
            }
            catch (System.Exception ex)
            {
                this.rtbInfoMessage.Text += "An error occurred while running the tool with ' " + ex.Message + " '\r\n";
            }
            finally
            {
                mvdRenderActivexOut.MVD_Refresh();
            }
        }

        private void btnExpXml_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.SaveFileDialog fileDlg = null;
            FileStream fileStr = null;
            try
            {
                // 启动文件另存对话框
                fileDlg = new System.Windows.Forms.SaveFileDialog();
                fileDlg.Filter = @"XMl Files(*.xml)|*.xml";
                fileDlg.RestoreDirectory = true;
                if (fileDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string filePath = fileDlg.FileName;
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                    fileStr = new FileStream(filePath, FileMode.Create);

                    /* Save parameters in local file as XML. */
                    byte[] fileBytes = new byte[256];
                    uint nConfigDataSize = 256;
                    uint nConfigDataLen = 0;
                    try
                    {
                        _SurfaceDefectFilterTool.SaveConfiguration(fileBytes, nConfigDataSize, ref nConfigDataLen);
                    }
                    catch (MvdException ex)
                    {
                        if (MVD_ERROR_CODE.MVD_E_NOENOUGH_BUF == ex.ErrorCode)
                        {
                            fileBytes = new byte[nConfigDataLen];
                            nConfigDataSize = nConfigDataLen;
                            _SurfaceDefectFilterTool.SaveConfiguration(fileBytes, nConfigDataSize, ref nConfigDataLen);
                        }
                        else
                        {
                            throw ex;
                        }
                    }

                    fileStr.Write(fileBytes, 0, Convert.ToInt32(nConfigDataLen));
                    fileStr.Flush();
                    fileStr.Close();
                    fileStr.Dispose();
                    this.rtbInfoMessage.Text += "Finish exporting xml file.\r\n";
                }
            }
            catch (MvdException ex)
            {
                this.rtbInfoMessage.Text += "Fail to export xml file. ErrorCode: 0x" + ex.ErrorCode.ToString("X") + "\r\n";
            }
            catch (System.Exception ex)
            {
                this.rtbInfoMessage.Text += "Fail to export xml file with error ' " + ex.Message + " '\r\n";
            }
            finally
            {
                if (null != fileDlg)
                {
                    fileDlg.Dispose();
                }
                if (null != fileStr)
                {
                    fileStr.Close();
                    fileStr.Dispose();
                }
            }
        }

        private void btnImpXml_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog fileDlg = null;
            FileStream fileStr = null;
            try
            {
                fileDlg = new System.Windows.Forms.OpenFileDialog();
                fileDlg.Filter = @"XMl Files(*.xml)|*.xml";
                fileDlg.FilterIndex = 2;
                fileDlg.RestoreDirectory = true;
                if (fileDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string filePath = fileDlg.FileName;
                    if (File.Exists(filePath))
                    {
                        fileStr = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                        Int64 nFileLen = fileStr.Length;
                        byte[] fileBytes = new byte[nFileLen];
                        uint nReadLen = Convert.ToUInt32(fileStr.Read(fileBytes, 0, fileBytes.Length));
                        fileStr.Close();
                        fileStr.Dispose();
                        _SurfaceDefectFilterTool.LoadConfiguration(fileBytes, nReadLen);
                        UpdateParamList(fileBytes, nReadLen);
                        this.rtbInfoMessage.Text += "Finish importing xml file.\r\n";
                    }
                }
                fileDlg.Dispose();
            }
            catch (MvdException ex)
            {
                this.rtbInfoMessage.Text += "Fail to import xml file. ErrorCode: 0x" + ex.ErrorCode.ToString("X") + "\r\n";
            }
            catch (System.Exception ex)
            {
                this.rtbInfoMessage.Text += "Fail to import xml file with error ' " + ex.Message + " '\r\n";
            }
            finally
            {
                if (null != fileDlg)
                {
                    fileDlg.Dispose();
                }
                if (null != fileStr)
                {
                    fileStr.Close();
                    fileStr.Dispose();
                }
            }
        }
        private void UpdateParamList(Byte[] bufXml, uint nXmlLen)
        {
            if (null == _XmlParseTool)
            {
                _XmlParseTool = new CMdvXmlParseTool_Surface(bufXml, nXmlLen);
            }
            else
            {
                _XmlParseTool.UpdateXmlBuf(bufXml, nXmlLen);
            }
            dataGridView1.Rows.Clear();
            for (int i = 0; i < _XmlParseTool.IntValueList.Count; ++i)
            {
                int nIndex = dataGridView1.Rows.Add();
                dataGridView1.Rows[nIndex].Cells[dataGridView1.Columns["ParamNameBoxCol"].Index].Value = _XmlParseTool.IntValueList[i].Name;
                dataGridView1.Rows[nIndex].Cells[dataGridView1.Columns["ParamValueBoxCol"].Index].Value = _XmlParseTool.IntValueList[i].CurValue;
            }

            for (int i = 0; i < _XmlParseTool.EnumValueList.Count; ++i)
            {
                int nIndex = dataGridView1.Rows.Add();
                DataGridViewTextBoxCell textboxcell = new DataGridViewTextBoxCell();
                textboxcell.Value = _XmlParseTool.EnumValueList[i].Name;
                dataGridView1.Rows[nIndex].Cells[dataGridView1.Columns["ParamNameBoxCol"].Index] = textboxcell;
                DataGridViewComboBoxCell comboxcell = new DataGridViewComboBoxCell();
                for (int j = 0; j < _XmlParseTool.EnumValueList[i].EnumRange.Count; ++j)
                {
                    comboxcell.Items.Add(_XmlParseTool.EnumValueList[i].EnumRange[j].Name);
                }
                comboxcell.Value = _XmlParseTool.EnumValueList[i].CurValue.Name;
                dataGridView1.Rows[nIndex].Cells[dataGridView1.Columns["ParamValueBoxCol"].Index] = comboxcell;
            }

            for (int i = 0; i < _XmlParseTool.FloatValueList.Count; ++i)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[dataGridView1.Columns["ParamNameBoxCol"].Index].Value = _XmlParseTool.FloatValueList[i].Name;
                dataGridView1.Rows[index].Cells[dataGridView1.Columns["ParamValueBoxCol"].Index].Value = _XmlParseTool.FloatValueList[i].CurValue;
            }

            for (int i = 0; i < _XmlParseTool.BooleanValueList.Count; ++i)
            {
                int index = dataGridView1.Rows.Add();

                DataGridViewTextBoxCell textboxCell = new DataGridViewTextBoxCell();
                textboxCell.Value = _XmlParseTool.BooleanValueList[i].Name;
                dataGridView1.Rows[index].Cells[dataGridView1.Columns["ParamNameBoxCol"].Index] = textboxCell;

                DataGridViewComboBoxCell comboCell = new DataGridViewComboBoxCell();
                comboCell.Items.Add("True");
                comboCell.Items.Add("False");
                comboCell.Value = _XmlParseTool.BooleanValueList[i].CurValue.ToString();
                dataGridView1.Rows[index].Cells[dataGridView1.Columns["ParamValueBoxCol"].Index] = comboCell;
            }
        }

        public double GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return ts.TotalMilliseconds;
        }

        private void rtbInfoMessage_DoubleClick(object sender, EventArgs e)
        {
            this.rtbInfoMessage.Clear();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            String strName = dataGridView1.Rows[e.RowIndex].Cells[dataGridView1.Columns["ParamNameBoxCol"].Index].Value.ToString();
            string strCurParamVal = null;
            for (int i = 0; i < _XmlParseTool.IntValueList.Count; ++i)
            {
                if (strName == _XmlParseTool.IntValueList[i].Name)
                {
                    strCurParamVal = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                }
            }
            for (int i = 0; i < _XmlParseTool.EnumValueList.Count; ++i)
            {
                if (strName == _XmlParseTool.EnumValueList[i].Name)
                {
                    String strEnumEntryName = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    strCurParamVal = strEnumEntryName;
                }
            }
            for (int i = 0; i < _XmlParseTool.FloatValueList.Count; ++i)
            {
                if (strName == _XmlParseTool.FloatValueList[i].Name)
                {
                    strCurParamVal = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                }
            }
            for (int i = 0; i < _XmlParseTool.BooleanValueList.Count; ++i)
            {
                if (strName == _XmlParseTool.BooleanValueList[i].Name)
                {
                    String strBooleanValueName = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    strCurParamVal = strBooleanValueName.Equals("True", StringComparison.OrdinalIgnoreCase) == true ? "1" : "0";
                }
            }

            try
            {
                double fStartTime = GetTimeStamp();
                _SurfaceDefectFilterTool.SetRunParam(strName, strCurParamVal);
                double fCostTime = GetTimeStamp() - fStartTime;
                this.rtbInfoMessage.Text += "SetRunParam success. Cost: " + fCostTime.ToString() + "\r\n";
            }
            catch (MvdException ex)
            {
                this.rtbInfoMessage.Text += "Fail to set run param. ErrorCode: 0x" + ex.ErrorCode.ToString("X") + "\r\n";
            }
            catch (System.Exception ex)
            {
                this.rtbInfoMessage.Text += "Fail to set run param with error " + ex.Message + "\r\n";
            }
        }

        private void radioButtonAllROI_Click(object sender, EventArgs e)
        {
            try
            {
                bool cleanState = false;

                if (null != _ROI)
                {
                    mvdRenderActivexIn.MVD_DeleteShape(_ROI);
                    _ROI = null;
                    cleanState = true;
                }
                foreach (var item in _MaskShapeList)
                {
                    mvdRenderActivexIn.MVD_DeleteShape(item);
                    cleanState = true;
                }
                _MaskShapeList.Clear();

                if (cleanState)
                {
                    this.rtbInfoMessage.Text += "Existing ROI has been cleared.\r\n";
                }
            }
            catch (MvdException ex)
            {
                this.rtbInfoMessage.Text += "Fail to clear existing shapes. ErrorCode: 0x" + ex.ErrorCode.ToString("X") + "\r\n";
            }
            catch (System.Exception ex)
            {
                this.rtbInfoMessage.Text += "Fail to clear existing shapes with error " + ex.Message + "\r\n";
            }
            finally
            {
                mvdRenderActivexIn.MVD_Refresh();
            }
        }

        private void radioButtonRectROI_Click(object sender, EventArgs e)
        {
            this.radioButtonPolygonMask.Checked = false;

        }

        private void radioButtonCircleROI_Click(object sender, EventArgs e)
        {
            this.radioButtonPolygonMask.Checked = false;

        }

        private void radioButtonAunulusROI_Click(object sender, EventArgs e)
        {
            this.radioButtonPolygonMask.Checked = false;

        }

        private void radioButtonPolygonMask_Click(object sender, EventArgs e)
        {
            this.radioButtonAllROI.Checked = false;
            this.radioButtonRectROI.Checked = false;
            this.radioButtonCircleROI.Checked = false;
            this.radioButtonAunulusROI.Checked = false;
        }

        private void mvdRenderActivexIn_MVD_ShapeChangeEvent(VisionDesigner.Controls.MVD_SHAPE_EVENT_TYPE enEventType, MVD_SHAPE_TYPE enShapeType, CMvdShape cShapeObj)
        {
            if (VisionDesigner.Controls.MVD_SHAPE_EVENT_TYPE.MvdShapeEventSelect == enEventType)
            {
                return;
            }
            if (VisionDesigner.Controls.MVD_SHAPE_EVENT_TYPE.MvdShapeEventEdit == enEventType)
            {
                return;
            }

            if (VisionDesigner.Controls.MVD_SHAPE_EVENT_TYPE.MvdShapeEventAdd == enEventType)
            {
                // 全区模式下不允许添加图形
                if (radioButtonAllROI.Checked)
                {
                    MessageBox.Show("Shapes are not allowed to be added in the AllRegion Mode");
                    mvdRenderActivexIn.MVD_DeleteShape(cShapeObj);
                    mvdRenderActivexIn.MVD_Refresh();
                    return;
                }

                // 矩形、圆、扇环ROI
                if ((MVD_SHAPE_TYPE.MvdShapeRectangle == enShapeType && radioButtonRectROI.Checked)
                    || (MVD_SHAPE_TYPE.MvdShapeCircle == enShapeType && radioButtonCircleROI.Checked)
                    || (MVD_SHAPE_TYPE.MvdShapeAnnularSector == enShapeType && radioButtonAunulusROI.Checked)
                    )
                {
                    if (null != _ROI)
                    {
                        mvdRenderActivexIn.MVD_DeleteShape(_ROI);
                        mvdRenderActivexIn.MVD_Refresh();
                    }
                    _ROI = cShapeObj;
                    return;
                }

                // 多边形屏蔽区
                if (MVD_SHAPE_TYPE.MvdShapePolygon == enShapeType && radioButtonPolygonMask.Checked)
                {
                    _MaskShapeList.Add(cShapeObj);
                    return;
                }
            }

            // 删除图形
            if (VisionDesigner.Controls.MVD_SHAPE_EVENT_TYPE.MvdShapeEventDelete == enEventType)
            {
                if (_ROI == cShapeObj)
                {
                    _ROI = null;
                }

                foreach (var item in _MaskShapeList)
                {
                    if (item == cShapeObj)
                    {
                        _MaskShapeList.Remove(item);
                        break;
                    }
                }
                return;
            }

            MessageBox.Show("The shape that is inconsistent with the checked type will be deleted.");
            mvdRenderActivexIn.MVD_DeleteShape(cShapeObj);
            mvdRenderActivexIn.MVD_Refresh();
        }
    }
}
