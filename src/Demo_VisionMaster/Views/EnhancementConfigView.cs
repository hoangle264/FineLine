using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisionDesigner.ImageEnhance;
using VisionDesigner.PreproMask;
using VisionDesigner;
using System.IO;
using Demo_VisionMaster.ParseXML;

namespace Demo_VisionMaster.Views
{
    public partial class EnhancementConfigView : Form
    {
        public CMvdImage Enchancement_Image { get; set; }
        private const float MVD_FLOAT_EPS = 0.0001f; // 浮点计算误差
        private CImageEnhanceTool m_stImageEnhanceToolObj = null;
        private CPreproMaskTool m_stPreproMaskToolObj = null;
        private CMvdImage m_stInputImage = null;
        private CMvdShape m_stROIShape = null;
        List<VisionDesigner.CMvdShape> m_lMaskShapes = new List<VisionDesigner.CMvdShape>();
        private ParseXML.CMvdXmlParseTool_Enhancement m_stXmlParseToolObj = null;
        public EnhancementConfigView()
        {
            InitializeComponent();
        }

        private void btnInitTool_Click(object sender, EventArgs e)
        {
            try
            {
                m_stImageEnhanceToolObj = new CImageEnhanceTool();
                //var a = test(m_stImageEnhanceToolObj);
                m_stPreproMaskToolObj = new CPreproMaskTool();
                byte[] fileBytes = new byte[256];
                uint nConfigDataSize = 256;
                uint nConfigDataLen = 0;
                try
                {
                    m_stImageEnhanceToolObj.SaveConfiguration(fileBytes, nConfigDataSize, ref nConfigDataLen);
                }
                catch (MvdException ex)
                {
                    if (MVD_ERROR_CODE.MVD_E_NOENOUGH_BUF == ex.ErrorCode)
                    {
                        fileBytes = new byte[nConfigDataLen];
                        nConfigDataSize = nConfigDataLen;
                        m_stImageEnhanceToolObj.SaveConfiguration(fileBytes, nConfigDataSize, ref nConfigDataLen);
                    }
                    else
                    {
                        throw ex;
                    }
                }
                UpdateParamList(fileBytes, nConfigDataLen);

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
        public object test(object f) 
        {
            f = new object();
            return f;
        }
        private void btnDeInit_Click(object sender, EventArgs e)
        {
            try
            {
                if (null != m_stROIShape)
                {
                    mvdRenderActivexIn.MVD_DeleteShape(m_stROIShape);
                    m_stROIShape = null;
                }
                if (null != m_stImageEnhanceToolObj)
                {
                    m_stImageEnhanceToolObj.Dispose();
                    m_stImageEnhanceToolObj = null;
                }

                if (null != m_stPreproMaskToolObj)
                {
                    m_stPreproMaskToolObj.Dispose();
                    m_stPreproMaskToolObj = null;
                }
                foreach (var item in m_lMaskShapes)
                {
                    mvdRenderActivexIn.MVD_DeleteShape(item);
                }
                m_lMaskShapes.Clear();
                mvdRenderActivexIn.MVD_Refresh();

                dataGridView1.Rows.Clear();
                m_stXmlParseToolObj.ClearXmlBuf();

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
        }

        private void btnLoadImg_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog fileDlg = null;
            try
            {
                fileDlg = new System.Windows.Forms.OpenFileDialog();
                fileDlg.Filter = @"Picture|*.bmp";
                fileDlg.RestoreDirectory = true;

                if (fileDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (null == m_stInputImage)
                    {
                        m_stInputImage = new CMvdImage();
                    }
                    m_stInputImage.InitImage(fileDlg.FileName);
                    MVD_ERROR_CODE nErrorCode = MVD_ERROR_CODE.MVD_OK;
                    if (Convert.ToInt32(nErrorCode) != mvdRenderActivexIn.MVD_LoadImageFromMvdImage(m_stInputImage))
                    {
                        throw new MvdException(MVD_MODULE_TYPE.MVD_MODUL_RENDER, nErrorCode, "MvdRenderControl error");
                    }
                    mvdRenderActivexIn.MVD_Refresh();

                    /* Shapes on the canvas are cleared by the render activeX when different images are switched */
                    m_lMaskShapes.Clear();
                    m_stROIShape = null;

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
                if ((null == m_stImageEnhanceToolObj) || (null == m_stInputImage))
                {
                    throw new MvdException(MVD_MODULE_TYPE.MVD_MODUL_TOOL, MVD_ERROR_CODE.MVD_E_CALLORDER);
                }
                m_stImageEnhanceToolObj.InputImage = m_stInputImage;

                bool bUseMaskFlag = false; // 是否启用掩膜
                if (m_lMaskShapes.Count > 0)
                {
                    bUseMaskFlag = true;
                }
                else if (null != m_stROIShape)
                {
                    if (MVD_SHAPE_TYPE.MvdShapeRectangle != m_stROIShape.ShapeType)
                    {
                        bUseMaskFlag = true;
                    }
                    else
                    {
                        var rectROI = m_stROIShape as CMvdRectangleF;
                        if (Math.Abs(rectROI.Angle) > MVD_FLOAT_EPS)
                        {
                            bUseMaskFlag = true;
                        }
                    }
                }

                if (true == bUseMaskFlag)
                {
                    m_stPreproMaskToolObj.RegionListEx.Clear();
                    m_stPreproMaskToolObj.InputImage = m_stInputImage;
                    if (null != m_stROIShape)
                    {
                        m_stPreproMaskToolObj.RegionListEx.Add(new Tuple<CMvdShape, MVD_REGION_TYPE>(m_stROIShape, MVD_REGION_TYPE.MvdRegionTypeRoi));
                    }

                    foreach (var item in m_lMaskShapes)
                    {
                        m_stPreproMaskToolObj.RegionListEx.Add(new Tuple<CMvdShape, MVD_REGION_TYPE>(item, MVD_REGION_TYPE.MvdRegionTypeMask));
                    }
                    m_stPreproMaskToolObj.Run();
                    m_stImageEnhanceToolObj.RegionImage = m_stPreproMaskToolObj.OutputImage;
                }
                else
                {
                    m_stImageEnhanceToolObj.RegionImage = null;
                    m_stImageEnhanceToolObj.ROI = m_stROIShape;
                }

                double fStartTime = GetTimeStamp();
                m_stImageEnhanceToolObj.Run();
                double fCostTime = GetTimeStamp() - fStartTime;
                this.rtbInfoMessage.Text += "Running cost: " + fCostTime.ToString() + "\r\n";
                this.rtbInfoMessage.Text += "Image enhance success." + "\r\n";

                CMvdImage stOutputImage = m_stImageEnhanceToolObj.Result.OutputImage;
                MVD_ERROR_CODE nErrorCode = MVD_ERROR_CODE.MVD_OK;
                if (Convert.ToInt32(nErrorCode) != mvdRenderActivexOut.MVD_LoadImageFromMvdImage(stOutputImage))
                {
                    throw new MvdException(MVD_MODULE_TYPE.MVD_MODUL_RENDER, nErrorCode, "MvdRenderControl error");
                }
                mvdRenderActivexOut.MVD_Refresh();
                stOutputImage.SaveImage("C:\\Users\\Dell\\Desktop\\Vinh_TestVisionPro\\VisionParket\\VisionParket\\Image\\Temp.bmp");
            }
            catch (MvdException ex)
            {
                this.rtbInfoMessage.Text += "An error occurred while running the tool. ErrorCode: 0x" + ex.ErrorCode.ToString("X") + "\r\n";
            }
            catch (System.Exception ex)
            {
                this.rtbInfoMessage.Text += "An error occurred while running the tool with ' " + ex.Message + " '\r\n";
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
                        m_stImageEnhanceToolObj.SaveConfiguration(fileBytes, nConfigDataSize, ref nConfigDataLen);
                    }
                    catch (MvdException ex)
                    {
                        if (MVD_ERROR_CODE.MVD_E_NOENOUGH_BUF == ex.ErrorCode)
                        {
                            fileBytes = new byte[nConfigDataLen];
                            nConfigDataSize = nConfigDataLen;
                            m_stImageEnhanceToolObj.SaveConfiguration(fileBytes, nConfigDataSize, ref nConfigDataLen);
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
                fileDlg.Dispose();
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
                        m_stImageEnhanceToolObj.LoadConfiguration(fileBytes, nReadLen);
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
            if (null == m_stXmlParseToolObj)
            {
                m_stXmlParseToolObj = new CMvdXmlParseTool_Enhancement(bufXml, nXmlLen);
            }
            else
            {
                m_stXmlParseToolObj.UpdateXmlBuf(bufXml, nXmlLen);
            }
            dataGridView1.Rows.Clear();
            for (int i = 0; i < m_stXmlParseToolObj.IntValueList.Count; ++i)
            {
                int nIndex = dataGridView1.Rows.Add();
                dataGridView1.Rows[nIndex].Cells[dataGridView1.Columns["ParamNameBoxCol"].Index].Value = m_stXmlParseToolObj.IntValueList[i].Name;
                dataGridView1.Rows[nIndex].Cells[dataGridView1.Columns["ParamValueBoxCol"].Index].Value = m_stXmlParseToolObj.IntValueList[i].CurValue;
            }

            for (int i = 0; i < m_stXmlParseToolObj.EnumValueList.Count; ++i)
            {
                int nIndex = dataGridView1.Rows.Add();
                DataGridViewTextBoxCell textboxcell = new DataGridViewTextBoxCell();
                textboxcell.Value = m_stXmlParseToolObj.EnumValueList[i].Name;
                dataGridView1.Rows[nIndex].Cells[dataGridView1.Columns["ParamNameBoxCol"].Index] = textboxcell;
                DataGridViewComboBoxCell comboxcell = new DataGridViewComboBoxCell();
                for (int j = 0; j < m_stXmlParseToolObj.EnumValueList[i].EnumRange.Count; ++j)
                {
                    comboxcell.Items.Add(m_stXmlParseToolObj.EnumValueList[i].EnumRange[j].Name);
                }
                comboxcell.Value = m_stXmlParseToolObj.EnumValueList[i].CurValue.Name;
                dataGridView1.Rows[nIndex].Cells[dataGridView1.Columns["ParamValueBoxCol"].Index] = comboxcell;
            }

            for (int i = 0; i < m_stXmlParseToolObj.FloatValueList.Count; ++i)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[dataGridView1.Columns["ParamNameBoxCol"].Index].Value = m_stXmlParseToolObj.FloatValueList[i].Name;
                dataGridView1.Rows[index].Cells[dataGridView1.Columns["ParamValueBoxCol"].Index].Value = m_stXmlParseToolObj.FloatValueList[i].CurValue;
            }

            for (int i = 0; i < m_stXmlParseToolObj.BooleanValueList.Count; ++i)
            {
                int index = dataGridView1.Rows.Add();

                DataGridViewTextBoxCell textboxCell = new DataGridViewTextBoxCell();
                textboxCell.Value = m_stXmlParseToolObj.BooleanValueList[i].Name;
                dataGridView1.Rows[index].Cells[dataGridView1.Columns["ParamNameBoxCol"].Index] = textboxCell;

                DataGridViewComboBoxCell comboCell = new DataGridViewComboBoxCell();
                comboCell.Items.Add("True");
                comboCell.Items.Add("False");
                comboCell.Value = m_stXmlParseToolObj.BooleanValueList[i].CurValue.ToString();
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
            for (int i = 0; i < m_stXmlParseToolObj.IntValueList.Count; ++i)
            {
                if (strName == m_stXmlParseToolObj.IntValueList[i].Name)
                {
                    strCurParamVal = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                }
            }
            for (int i = 0; i < m_stXmlParseToolObj.FloatValueList.Count; ++i)
            {
                if (strName == m_stXmlParseToolObj.FloatValueList[i].Name)
                {
                    strCurParamVal = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                }
            }
            for (int i = 0; i < m_stXmlParseToolObj.EnumValueList.Count; ++i)
            {
                if (strName == m_stXmlParseToolObj.EnumValueList[i].Name)
                {
                    String strEnumEntryName = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    strCurParamVal = strEnumEntryName;
                }
            }
            for (int i = 0; i < m_stXmlParseToolObj.BooleanValueList.Count; ++i)
            {
                if (strName == m_stXmlParseToolObj.BooleanValueList[i].Name)
                {
                    String strBooleanValueName = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    strCurParamVal = strBooleanValueName.Equals("True", StringComparison.OrdinalIgnoreCase) == true ? "1" : "0";
                }
            }
            try
            {
                double fStartTime = GetTimeStamp();
                m_stImageEnhanceToolObj.SetRunParam(strName, strCurParamVal);
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

        private void AllROIRadioButton_Click(object sender, EventArgs e)
        {
            MaskCheckBox.Checked = false;
            try
            {
                if (null != m_stROIShape)
                {
                    mvdRenderActivexIn.MVD_DeleteShape(m_stROIShape);
                    m_stROIShape = null;
                }
                foreach (var item in m_lMaskShapes)
                {
                    mvdRenderActivexIn.MVD_DeleteShape(item);
                }
                m_lMaskShapes.Clear();
                mvdRenderActivexIn.MVD_Refresh();
                this.rtbInfoMessage.Text += "Existing ROI has been cleared.\r\n";
            }
            catch (MvdException ex)
            {
                this.rtbInfoMessage.Text += "Fail to clear existing shapes. ErrorCode: 0x" + ex.ErrorCode.ToString("X") + "\r\n";
            }
            catch (System.Exception ex)
            {
                this.rtbInfoMessage.Text += "Fail to clear existing shapes with error " + ex.Message + "\r\n";
            }
            MaskCheckBox.Checked = false;
        }

        private void RectROIRadioButton_Click(object sender, EventArgs e)
        {
            MaskCheckBox.Checked = false;
        }

        private void AnnulROIRadioButton_Click(object sender, EventArgs e)
        {
            MaskCheckBox.Checked = false;
        }

        private void PolygonROIRadioButton_Click(object sender, EventArgs e)
        {
            MaskCheckBox.Checked = false;
        }

        private void MaskCheckBox_Click(object sender, EventArgs e)
        {
            AllROIRadioButton.Checked = false;
            AnnulROIRadioButton.Checked = false;
            RectROIRadioButton.Checked = false;
            PolygonROIRadioButton.Checked = false;
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

            /* ROI or result shape may be delete */
            if (VisionDesigner.Controls.MVD_SHAPE_EVENT_TYPE.MvdShapeEventDelete == enEventType)
            {
                if (m_stROIShape == cShapeObj)
                {
                    m_stROIShape = null;
                }
                foreach (var item in m_lMaskShapes)
                {
                    if (item == cShapeObj)
                    {
                        m_lMaskShapes.Remove(item);
                        break;
                    }
                }
                return;
            }

            if (MVD_SHAPE_TYPE.MvdShapePolygon == enShapeType)
            {
                if ((true != MaskCheckBox.Checked) && (true != PolygonROIRadioButton.Checked))
                {
                    MessageBox.Show("Polygon is only allowed to be added only when adding masks or polygon ROI.");
                    mvdRenderActivexIn.MVD_DeleteShape(cShapeObj);
                    mvdRenderActivexIn.MVD_Refresh();
                    return;
                }
                if (true == MaskCheckBox.Checked)
                {
                    m_lMaskShapes.Add(cShapeObj);
                    return;
                }
                if (true == PolygonROIRadioButton.Checked)
                {
                    if (null != m_stROIShape)
                    {
                        mvdRenderActivexIn.MVD_DeleteShape(m_stROIShape);
                        mvdRenderActivexIn.MVD_Refresh();
                    }
                    m_stROIShape = cShapeObj;
                    return;
                }
            }

            /* MvdShapeAdded event will be processed here */
            if (AllROIRadioButton.Checked)
            {
                MessageBox.Show("Shapes are not allowed to be added in the AllRegion Mode");
                mvdRenderActivexIn.MVD_DeleteShape(cShapeObj);
                mvdRenderActivexIn.MVD_Refresh();
                return;
            }
            if (((RectROIRadioButton.Checked) && (MVD_SHAPE_TYPE.MvdShapeRectangle == enShapeType)) ||
                ((AnnulROIRadioButton.Checked) && (MVD_SHAPE_TYPE.MvdShapeAnnularSector == enShapeType)))
            {
                if (null != m_stROIShape)
                {
                    mvdRenderActivexIn.MVD_DeleteShape(m_stROIShape);
                    mvdRenderActivexIn.MVD_Refresh();
                }
                m_stROIShape = cShapeObj;
                return;
            }

            MessageBox.Show("The shape that is inconsistent with the checked type will be deleted.");
            mvdRenderActivexIn.MVD_DeleteShape(cShapeObj);
            mvdRenderActivexIn.MVD_Refresh();
        }
    }
}
