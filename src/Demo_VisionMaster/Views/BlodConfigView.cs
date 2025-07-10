using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisionDesigner.BlobFind;
using VisionDesigner;
using System.IO;
using Demo_VisionMaster.ParseXML;
using System.Windows.Navigation;
using Demo_VisionMaster.Helpers;

namespace Demo_VisionMaster.Views
{
    public partial class BlodConfigView : Form
    {
        private CBlobFindTool m_stBlobFindToolObj = null;
        private CMvdImage m_stInputImage = null;
        private CMvdImage m_stBinaryImage = null;
        private CMvdImage m_stBlobImage = null;
        private CMvdShape m_stROIShape = null;
        List<CMvdShape> m_lMaskShapes = new List<CMvdShape>();
        List<CMvdRectangleF> m_lBlobBoxRender1 = new List<CMvdRectangleF>();
        List<CMvdRectangleF> m_lBlobBoxRender2 = new List<CMvdRectangleF>();
        private CMvdXmlParseTool_Blod m_stXmlParseToolObj = null;
        private CMvdShape m_stBlobOutline = null;
        public BlodConfigView()
        {
            InitializeComponent();
        }

        private void BlodConfigView_Load(object sender, EventArgs e)
        {
            this.comboBoxImage.Items.Add("Binary Image");
            this.comboBoxImage.Items.Add("Blob Image");
            this.comboBoxImage.SelectedIndex = 0;

            this.radioROIAll.Checked = true;
        }
        private double GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return ts.TotalMilliseconds;
        }
        private void Init_Click(object sender, EventArgs e)
        {
            try
            {
                m_stBlobFindToolObj = new CBlobFindTool();
                byte[] fileBytes = new byte[256];
                uint nConfigDataSize = 256;
                uint nConfigDataLen = 0;
                try
                {
                    m_stBlobFindToolObj.SaveConfiguration(fileBytes, nConfigDataSize, ref nConfigDataLen);
                }
                catch (MvdException ex)
                {
                    if (MVD_ERROR_CODE.MVD_E_NOENOUGH_BUF == ex.ErrorCode)
                    {
                        fileBytes = new byte[nConfigDataLen];
                        nConfigDataSize = nConfigDataLen;
                        m_stBlobFindToolObj.SaveConfiguration(fileBytes, nConfigDataSize, ref nConfigDataLen);
                    }
                    else
                    {
                        throw ex;
                    }
                }
                UpdateParamList(fileBytes, nConfigDataLen);

                this.Init.Enabled = false;
                this.DeInit.Enabled = true;
                this.Run.Enabled = true;
                this.ExportXml.Enabled = true;
                this.ImportXml.Enabled = true;

                this.richTextBox1.Text += "Init finish.\r\n";
            }
            catch (MvdException ex)
            {
                this.richTextBox1.Text += "Fail to initialize the tool. ErrorCode: 0x" + ex.ErrorCode.ToString("X") + "\r\n";
            }
            catch (System.Exception ex)
            {
                this.richTextBox1.Text += "Fail with error " + ex.Message + "\r\n";
            }
        }

        private void DeInit_Click(object sender, EventArgs e)
        {
            try
            {
                if (null != m_stROIShape)
                {
                    mvdRenderActivex1.MVD_DeleteShape(m_stROIShape);
                    m_stROIShape = null;
                }
                foreach (var item in m_lMaskShapes)
                {
                    mvdRenderActivex1.MVD_DeleteShape(item);
                }
                m_lMaskShapes.Clear();
                foreach (var item in m_lBlobBoxRender1)
                {
                    mvdRenderActivex1.MVD_DeleteShape(item);
                }
                m_lBlobBoxRender1.Clear();
                foreach (var item in m_lBlobBoxRender2)
                {
                    mvdRenderActivex2.MVD_DeleteShape(item);
                }

                if (null != m_stBlobFindToolObj)
                {
                    m_stBlobFindToolObj.Dispose();
                    m_stBlobFindToolObj = null;
                }
                if (null != m_stBlobOutline)
                {
                    mvdRenderActivex2.MVD_DeleteShape(m_stBlobOutline);
                    m_stBlobOutline = null;
                }

                m_lBlobBoxRender2.Clear();
                mvdRenderActivex1.MVD_Refresh();
                mvdRenderActivex2.MVD_Refresh();

                dataGridView1.Rows.Clear();
                m_stXmlParseToolObj.ClearXmlBuf();

                this.DeInit.Enabled = false;
                this.Run.Enabled = false;
                this.ExportXml.Enabled = false;
                this.ImportXml.Enabled = false;
                this.Init.Enabled = true;

                this.richTextBox1.Text += "DeInit finish.\r\n";
            }
            catch (MvdException ex)
            {
                this.richTextBox1.Text += "An error occurred while releasing the resource. ErrorCode: 0x" + ex.ErrorCode.ToString("X") + "\r\n";
            }
            catch (System.Exception ex)
            {
                this.richTextBox1.Text += "An error occurred while releasing the resource with ' " + ex.Message + " '\r\n";
            }
        }

        private void ImportXml_Click(object sender, EventArgs e)
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
                        m_stBlobFindToolObj.LoadConfiguration(fileBytes, nReadLen);
                        UpdateParamList(fileBytes, nReadLen);
                        this.richTextBox1.Text += "Finish importing xml file.\r\n";
                    }
                }
                fileDlg.Dispose();
            }
            catch (MvdException ex)
            {
                this.richTextBox1.Text += "Fail to import xml file. ErrorCode: 0x" + ex.ErrorCode.ToString("X") + "\r\n";
            }
            catch (System.Exception ex)
            {
                this.richTextBox1.Text += "Fail to import xml file with error ' " + ex.Message + " '\r\n";
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

        private void ExportXml_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.SaveFileDialog fileDlg = null;
            FileStream fileStr = null;
            try
            {
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
                        m_stBlobFindToolObj.SaveConfiguration(fileBytes, nConfigDataSize, ref nConfigDataLen);
                    }
                    catch (MvdException ex)
                    {
                        if (MVD_ERROR_CODE.MVD_E_NOENOUGH_BUF == ex.ErrorCode)
                        {
                            fileBytes = new byte[nConfigDataLen];
                            nConfigDataSize = nConfigDataLen;
                            m_stBlobFindToolObj.SaveConfiguration(fileBytes, nConfigDataSize, ref nConfigDataLen);
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
                    this.richTextBox1.Text += "Finish exporting xml file.\r\n";
                }
                fileDlg.Dispose();
            }
            catch (MvdException ex)
            {
                this.richTextBox1.Text += "Fail to export xml file. ErrorCode: 0x" + ex.ErrorCode.ToString("X") + "\r\n";
            }
            catch (System.Exception ex)
            {
                this.richTextBox1.Text += "Fail to export xml file with error ' " + ex.Message + " '\r\n";
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

        private void LoadImg_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog fileDlg = null;
            try
            {
                fileDlg = new System.Windows.Forms.OpenFileDialog();
                fileDlg.Filter = @"image(*.bmp;*.jpeg;*.jpg;*.png)|*.bmp;*.jpeg;*.jpg;*.png||";
                fileDlg.RestoreDirectory = true;

                if (fileDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (null == m_stInputImage)
                    {
                        m_stInputImage = new CMvdImage();
                    }
                    m_stInputImage.InitImage(fileDlg.FileName);
                    //m_stInputImage = MainView.Instance.TempImage;
                    MVD_ERROR_CODE nErrorCode = MVD_ERROR_CODE.MVD_OK;
                    if (Convert.ToInt32(nErrorCode) != mvdRenderActivex1.MVD_LoadImageFromMvdImage(m_stInputImage))
                    {
                        throw new MvdException(MVD_MODULE_TYPE.MVD_MODUL_RENDER, nErrorCode, "MvdRenderControl error");
                    }
                    mvdRenderActivex1.MVD_Refresh();

                    /* Shapes on the canvas are cleared by the render activeX when different images are switched */
                    m_lMaskShapes.Clear();
                    m_stROIShape = null;
                    m_lBlobBoxRender1.Clear();

                    this.richTextBox1.Text += "Finish loading image from [" + fileDlg.FileName + "].\r\n";
                }
                fileDlg.Dispose();

            }
            catch (MvdException ex)
            {
                this.richTextBox1.Text += "Fail to load image from [" + fileDlg.FileName + "]. ErrorCode: 0x" + ex.ErrorCode.ToString("X") + "\r\n";
            }
            catch (System.Exception ex)
            {
                this.richTextBox1.Text += "Fail to load image from [" + fileDlg.FileName + "]. Error: " + ex.Message + "\r\n";
            }
            finally
            {
                if (null != fileDlg)
                {
                    fileDlg.Dispose();
                }
            }
        }

        private void Run_Click(object sender, EventArgs e)
        {
            try
            {
              
                if ((null == m_stBlobFindToolObj) || (null == this.m_stInputImage))
                {
                    throw new MvdException(MVD_MODULE_TYPE.MVD_MODUL_APP, MVD_ERROR_CODE.MVD_E_CALLORDER);
                }

                
                m_stBlobFindToolObj.InputImage = m_stInputImage;
                if (comboBoxImage.SelectedIndex == 1)
                {
                    m_stBlobFindToolObj.BasicParam.ShowBlobImageStatus = true;
                    m_stBlobFindToolObj.BasicParam.ShowBinaryImageStatus = false;
                }
                else
                {
                    m_stBlobFindToolObj.BasicParam.ShowBlobImageStatus = false;
                    m_stBlobFindToolObj.BasicParam.ShowBinaryImageStatus = true;
                }

                if (null == m_stROIShape)
                {
                    m_stBlobFindToolObj.ROI = new VisionDesigner.CMvdRectangleF(m_stInputImage.Width / 2, m_stInputImage.Height / 2, m_stInputImage.Width, m_stInputImage.Height);
                }
                else
                {
                    m_stBlobFindToolObj.ROI = m_stROIShape;
                }

                m_stBlobFindToolObj.ClearMasks();
                foreach (var item in m_lMaskShapes)
                {
                    m_stBlobFindToolObj.AddMask(item);
                }
                double fStartTime = GetTimeStamp();
                m_stBlobFindToolObj.Run();
                double fCostTime = GetTimeStamp() - fStartTime;
                this.richTextBox1.Text += "Running cost: " + fCostTime.ToString() + "\r\n";

                if (0 != m_lBlobBoxRender1.Count)
                {
                    foreach (var item in m_lBlobBoxRender1)
                    {
                        mvdRenderActivex1.MVD_DeleteShape(item);
                    }
                    m_lBlobBoxRender1.Clear();
                }

                if (null != m_stBlobOutline)
                {
                    mvdRenderActivex2.MVD_DeleteShape(m_stBlobOutline);
                    m_stBlobOutline = null;
                }

                this.richTextBox1.Text += "Blob Total Area : " + m_stBlobFindToolObj.Result.BlobTotalArea.ToString("0.000") + "\r\n";
                this.richTextBox1.Text += "Blob Num : " + m_stBlobFindToolObj.Result.BlobInfo.Count + "\r\n";

                var BlobOutline = new CMvdPointSetF();
                BlobOutline.BorderColor = new MVD_COLOR(0, 255, 0, 255);
                foreach (var item in m_stBlobFindToolObj.Result.BlobInfo)
                {
                    CMvdRectangleF blobBox = new CMvdRectangleF(item.BoxInfo.CenterX, item.BoxInfo.CenterY, item.BoxInfo.Width, item.BoxInfo.Height);
                    blobBox.Angle = item.BoxInfo.Angle;
                    blobBox.BorderColor = new MVD_COLOR(255, 0, 0, 255);
                    mvdRenderActivex1.MVD_AddMvdShape(blobBox);
                    m_lBlobBoxRender1.Add(blobBox);

                    this.richTextBox1.Text += "Index : " + item.DomainIndex + "\r\n";
                    this.richTextBox1.Text += string.Format("Angle :{0}, Perimeter: {1}, Area:{2}, Circularity :{3}\r\n"
                                                            , item.BoxInfo.Angle.ToString("#0.000")
                                                            , item.Perimeter.ToString("#0.000")
                                                            , item.Area
                                                            , item.Circularity.ToString("#0.000"));

                    this.richTextBox1.Text += string.Format("Rectangularity: {0}, Centroid:({1}, {2}), Min gray value: {3}, Max gray value: {4}, gray contrast: {5}\r\n"
                                                            , item.Rectangularity.ToString("#0.000")
                                                            , item.Centroid.fX.ToString("#0.000")
                                                            , item.Centroid.fY.ToString("#0.000")
                                                            , item.MinGrayValue
                                                            , item.MaxGrayValue
                                                            , item.GrayContrast.ToString("#0.000"));
                    this.richTextBox1.Text += "The number of contour point: " + item.ContourPoints.Count() + "\r\n\r\n";

                    foreach (var Item in item.ContourPoints)
                    {
                        BlobOutline.AddPoint(Item.sX, Item.sY);
                    }

                    this.richTextBox1.Text += "The number sequential of contour : " + item.SequentialContours.Count() + "\r\n\r\n";

                    for (int j = 0; j < item.SequentialContours.Count(); j++)
                    {
                        this.richTextBox1.Text += "The point number of each sequential contour : " + item.SequentialContours[j].ContourPoints.Count + "\r\n\r\n";
                    }

                }
                if (0 != BlobOutline.PointsList.Count)
                {
                    m_stBlobOutline = BlobOutline;
                }

                mvdRenderActivex1.MVD_Refresh();

                m_stBinaryImage = m_stBlobFindToolObj.Result.BinaryImage;
                m_stBlobImage = m_stBlobFindToolObj.Result.BlobImage;


                int nCurIndex = this.comboBoxImage.SelectedIndex;
                this.comboBoxImage.SelectedIndex = -1;
                this.comboBoxImage.SelectedIndex = nCurIndex;
 
            }
            catch (MvdException ex)
            {

                this.richTextBox1.Text += "Fail to execute algorithm tool. nRet = 0x" + ex.ErrorCode.ToString("X") + "\r\n" + ex.Message + ex.StackTrace;
            }
            catch (System.Exception ex)
            {
                this.richTextBox1.Text += ex.Message + ex.Source + ex.StackTrace + "\r\n";
            }
        }
        private void UpdateParamList(Byte[] bufXml, uint nXmlLen)
        {
            if (null == m_stXmlParseToolObj)
            {
                m_stXmlParseToolObj = new CMvdXmlParseTool_Blod(bufXml, nXmlLen);
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
                int nIndex = dataGridView1.Rows.Add();
                dataGridView1.Rows[nIndex].Cells[dataGridView1.Columns["ParamNameBoxCol"].Index].Value = m_stXmlParseToolObj.FloatValueList[i].Name;
                dataGridView1.Rows[nIndex].Cells[dataGridView1.Columns["ParamValueBoxCol"].Index].Value = m_stXmlParseToolObj.FloatValueList[i].CurValue;
            }

            for (int i = 0; i < m_stXmlParseToolObj.BooleanValueList.Count; ++i)
            {
                int nIndex = dataGridView1.Rows.Add();

                DataGridViewTextBoxCell textboxcell = new DataGridViewTextBoxCell();
                textboxcell.Value = m_stXmlParseToolObj.BooleanValueList[i].Name;
                dataGridView1.Rows[nIndex].Cells[dataGridView1.Columns["ParamNameBoxCol"].Index] = textboxcell;

                DataGridViewComboBoxCell comboxcell = new DataGridViewComboBoxCell();
                comboxcell.Items.Add("True");
                comboxcell.Items.Add("False");
                comboxcell.Value = m_stXmlParseToolObj.BooleanValueList[i].CurValue.ToString();
                dataGridView1.Rows[nIndex].Cells[dataGridView1.Columns["ParamValueBoxCol"].Index] = comboxcell;
            }
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
            for (int i = 0; i < m_stXmlParseToolObj.EnumValueList.Count; ++i)
            {
                if (strName == m_stXmlParseToolObj.EnumValueList[i].Name)
                {
                    String strEnumEntryName = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    strCurParamVal = strEnumEntryName;
                }
            }
            for (int i = 0; i < m_stXmlParseToolObj.FloatValueList.Count; ++i)
            {
                if (strName == m_stXmlParseToolObj.FloatValueList[i].Name)
                {
                    strCurParamVal = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
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
                m_stBlobFindToolObj.SetRunParam(strName, strCurParamVal);
                double fCostTime = GetTimeStamp() - fStartTime;
                this.richTextBox1.Text += "SetRunParam success. Cost: " + fCostTime.ToString() + "\r\n";
            }
            catch (MvdException ex)
            {
                this.richTextBox1.Text += "Fail to set run param. ErrorCode: 0x" + ex.ErrorCode.ToString("X") + "\r\n";
            }
            catch (System.Exception ex)
            {
                this.richTextBox1.Text += "Fail to set run param with error " + ex.Message + "\r\n";
            }
        }

        private void comboBoxImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (0 == this.comboBoxImage.SelectedIndex)
            {
                if (null == m_stBinaryImage)
                {
                    return;
                }

                mvdRenderActivex2.MVD_LoadImageFromData(m_stBinaryImage.GetImageData(0).arrDataBytes
                                                  , m_stBinaryImage.GetImageData(0).nLen
                                                  , m_stBinaryImage.Width
                                                  , m_stBinaryImage.Height
                                                  , System.Convert.ToInt16(m_stBinaryImage.PixelFormat));
            }
            else if (1 == this.comboBoxImage.SelectedIndex)
            {
                if (null == m_stBlobImage)
                {
                    return;
                }

                mvdRenderActivex2.MVD_LoadImageFromData(m_stBlobImage.GetImageData(0).arrDataBytes
                                                  , m_stBlobImage.GetImageData(0).nLen
                                                  , m_stBlobImage.Width
                                                  , m_stBlobImage.Height
                                                  , System.Convert.ToInt16(m_stBlobImage.PixelFormat));
            }
            else
            {
                return; // 非0和1的Index直接返回
            }

            // The shapes will be cleaned up as the image switch
            mvdRenderActivex2.MVD_Refresh();
            m_lBlobBoxRender2.Clear();
            foreach (var item in m_lBlobBoxRender1)
            {
                CMvdRectangleF boxRender2 = item.Clone() as CMvdRectangleF;
                mvdRenderActivex2.MVD_AddMvdShape(boxRender2);
                m_lBlobBoxRender2.Add(boxRender2);
            }
            if (null != m_stBlobOutline)
            {
                CMvdPointSetF BlobOutline2 = m_stBlobOutline as CMvdPointSetF;
                mvdRenderActivex2.MVD_AddMvdShape(BlobOutline2);
            }
            mvdRenderActivex2.MVD_Refresh();
        }

        private void richTextBox1_DoubleClick(object sender, EventArgs e)
        {
            this.richTextBox1.Clear();
        }

        private void radioROIAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (null != m_stROIShape)
                {
                    mvdRenderActivex1.MVD_DeleteShape(m_stROIShape);
                    m_stROIShape = null;
                }
                foreach (var item in m_lMaskShapes)
                {
                    mvdRenderActivex1.MVD_DeleteShape(item);
                }
                m_lMaskShapes.Clear();

                mvdRenderActivex1.MVD_Refresh();
                this.richTextBox1.Text += "Existing ROI has been cleared.\r\n";
            }
            catch (MvdException ex)
            {
                this.richTextBox1.Text += "Fail to clear existing shapes. ErrorCode: 0x" + ex.ErrorCode.ToString("X") + "\r\n";
            }
            catch (System.Exception ex)
            {
                this.richTextBox1.Text += "Fail to clear existing shapes with error " + ex.Message + "\r\n";
            }
            finally
            {
                this.radioMaskPolygon.Checked = false;
            }
        }

        private void radioROIRect_Click(object sender, EventArgs e)
        {
            this.radioMaskPolygon.Checked = false;
        }

        private void radioROIAnnulus_Click(object sender, EventArgs e)
        {
            this.radioMaskPolygon.Checked = false;
        }

        private void radioROIPolygon_Click(object sender, EventArgs e)
        {
            this.radioMaskPolygon.Checked = false;
        }

        private void radioMaskPolygon_Click(object sender, EventArgs e)
        {
            this.radioROIAll.Checked = false;
            this.radioROIRect.Checked = false;
            this.radioROIAnnulus.Checked = false;
            this.radioROIPolygon.Checked = false;
        }

        private void mvdRenderActivex1_MVD_ShapeChangeEvent(VisionDesigner.Controls.MVD_SHAPE_EVENT_TYPE enEventType, MVD_SHAPE_TYPE enShapeType, CMvdShape cShapeObj)
        {
            if (VisionDesigner.Controls.MVD_SHAPE_EVENT_TYPE.MvdShapeEventSelect == enEventType)
            {
                return;
            }
            if (VisionDesigner.Controls.MVD_SHAPE_EVENT_TYPE.MvdShapeEventEdit == enEventType)
            {
                return;
            }

            // 图形删除事件
            if (VisionDesigner.Controls.MVD_SHAPE_EVENT_TYPE.MvdShapeEventDelete == enEventType)
            {
                // 删除ROI
                if (m_stROIShape == cShapeObj)
                {
                    m_stROIShape = null;
                }
                else // 删除绘制的blob包围框和屏蔽区
                {
                    foreach (var item in m_lMaskShapes)
                    {
                        if (cShapeObj == item)
                        {
                            m_lMaskShapes.Remove(item);
                            break;
                        }
                    }

                    foreach (var item in m_lBlobBoxRender1)
                    {
                        if (cShapeObj == item)
                        {
                            m_lBlobBoxRender1.Remove(item);
                            break;
                        }
                    }
                }
                return;
            }

            if (MVD_SHAPE_TYPE.MvdShapePolygon == enShapeType)
            {
                if (true == this.radioROIPolygon.Checked) // 多边形ROI
                {
                    if (VisionDesigner.Controls.MVD_SHAPE_EVENT_TYPE.MvdShapeEventAdd == enEventType)
                    {
                        if (null != m_stROIShape)
                        {
                            mvdRenderActivex1.MVD_DeleteShape(m_stROIShape);
                            mvdRenderActivex1.MVD_Refresh();
                        }
                        m_stROIShape = cShapeObj;
                    }
                }
                else if (true == this.radioMaskPolygon.Checked) // 多边形屏蔽区
                {
                    if (VisionDesigner.Controls.MVD_SHAPE_EVENT_TYPE.MvdShapeEventAdd == enEventType)
                    {
                        m_lMaskShapes.Add(cShapeObj);
                    }
                }
                else // 非Polygon按钮绘制了多边形
                {
                    if (VisionDesigner.Controls.MVD_SHAPE_EVENT_TYPE.MvdShapeEventAdd == enEventType)
                    {
                        MessageBox.Show("The shape that is inconsistent with the checked type will be deleted.\r\n");
                        mvdRenderActivex1.MVD_DeleteShape(cShapeObj);
                        mvdRenderActivex1.MVD_Refresh();
                    }
                }
                return;
            }

            if (this.radioROIAll.Checked)
            {
                MessageBox.Show("Shapes are not allowed to be added in the AllRegion Mode\r\n");
                m_stROIShape = null;
            }

            if (((this.radioROIRect.Checked) && (MVD_SHAPE_TYPE.MvdShapeRectangle == enShapeType))
              || ((this.radioROIAnnulus.Checked) && (MVD_SHAPE_TYPE.MvdShapeAnnularSector == enShapeType)))
            {
                if (null != m_stROIShape)
                {
                    mvdRenderActivex1.MVD_DeleteShape(m_stROIShape);
                    mvdRenderActivex1.MVD_Refresh();
                }
                m_stROIShape = cShapeObj;
                return;
            }

            MessageBox.Show("The shape that is inconsistent with the checked type will be deleted.\r\n");
            mvdRenderActivex1.MVD_DeleteShape(cShapeObj);
            mvdRenderActivex1.MVD_Refresh();
        }

        private void mvdRenderActivex2_MVD_ShapeChangeEvent(VisionDesigner.Controls.MVD_SHAPE_EVENT_TYPE enEventType, MVD_SHAPE_TYPE enShapeType, CMvdShape cShapeObj)
        {
            if ((VisionDesigner.Controls.MVD_SHAPE_EVENT_TYPE.MvdShapeEventSelect == enEventType)
            || (VisionDesigner.Controls.MVD_SHAPE_EVENT_TYPE.MvdShapeEventEdit == enEventType)
            || (VisionDesigner.Controls.MVD_SHAPE_EVENT_TYPE.MvdShapeEventAdd == enEventType))
            {
                return;
            }

            if (VisionDesigner.Controls.MVD_SHAPE_EVENT_TYPE.MvdShapeEventDelete == enEventType)
            {
                foreach (var item in m_lBlobBoxRender2)
                {
                    if (cShapeObj == item)
                    {
                        m_lBlobBoxRender2.Remove(item);
                        break;
                    }
                }
                return;
            }
        }
    }
}
