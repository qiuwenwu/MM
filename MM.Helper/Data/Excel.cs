using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace MM.Helper.Data
{
    /// <summary>
    /// ExcelHelper帮助类
    /// </summary>
    public class Excel
    {
        /// <summary>
        /// 错误提示
        /// </summary>
        public string Ex { get; private set; }

        /// <summary>
        /// 当前路径
        /// </summary>
        public string Dir { get; set; }

        /// <summary>  
        /// 导入Excel 
        /// </summary>  
        /// <param name="file">excel文档路径</param>  
        /// <param name="sheet">表名</param>  
        /// <returns>导入成功返回table，失败返回false</returns>
        public DataTable Load(string file, string sheet)
        {
            file = file.ToFullName(Dir);
            DataTable dataTable = null;
            if (file.EndsWith(".xlsx"))
            {
                XSSFWorkbook wk = null;

                try
                {
                    wk = new XSSFWorkbook(file);
                    if (wk == null)
                    {
                        Ex = "打开Excel文件失败！";
                    }
                    else if (wk.Count > 0)
                    {
                        if (string.IsNullOrEmpty(sheet))
                        {
                            dataTable = ToTable(wk.GetSheetAt(0));
                        }
                        else
                        {
                            dataTable = ToTable(wk.GetSheet(sheet));
                        }
                    }
                    else
                    {
                        Ex = "没有获取到表内容！";
                    }
                }
                catch (Exception ex)
                {
                    Ex = ex.Message;
                }
                finally
                {
                    if (wk != null)
                    {
                        wk.Close();
                    }
                }
            }
            else
            {
                HSSFWorkbook wk = null;
                try
                {
                    using (FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.ReadWrite))
                    {
                        wk = new HSSFWorkbook(fileStream);
                    }
                    if (wk == null)
                    {
                        Ex = "打开Excel文件失败！";
                    }
                    if (string.IsNullOrEmpty(sheet))
                    {
                        dataTable = ToTable(wk.GetSheetAt(0));
                    }
                    else
                    {
                        dataTable = ToTable(wk.GetSheet(sheet));
                    }
                }
                catch (Exception ex)
                {
                    Ex = ex.Message;
                }
                finally
                {
                    if (wk != null)
                    {
                        wk.Close();
                    }
                }
            }
            return dataTable;
        }

        /// <summary>  
        /// 导入Excel 
        /// </summary>  
        /// <param name="file">excel文档路径</param>  
        /// <param name="index">表索引（第几张表）</param>
        /// <returns>导入成功返回table，失败返回false</returns>
        public DataTable Load(string file, int index = 0)
        {
            file = file.ToFullName(Dir);
            DataTable dataTable = null;
            if (file.EndsWith(".xlsx"))
            {
                XSSFWorkbook wk = null;
                try
                {
                    wk = new XSSFWorkbook(file);
                    if (wk == null)
                    {
                        Ex = "打开Excel文件失败！";
                        return null;
                    }
                    dataTable = ToTable(wk.GetSheetAt(index));
                }
                catch (Exception ex)
                {
                    Ex = ex.Message;
                }
                finally
                {
                    if (wk != null)
                    {
                        wk.Close();
                    }
                }
            }
            else
            {
                HSSFWorkbook wk = null;
                try
                {

                    using (FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.ReadWrite))
                    {
                        wk = new HSSFWorkbook(fileStream);
                    }
                    if (wk == null)
                    {
                        Ex = "打开Excel文件失败！";
                    }
                    if (wk.Count <= index)
                    {
                        Ex = "文件中不存在该表！";
                    }
                    else
                    {
                        dataTable = ToTable(wk.GetSheetAt(0));
                    }
                }
                catch (Exception ex)
                {
                    Ex = ex.Message;
                }
                finally
                {
                    if (wk != null)
                    {
                        wk.Close();
                    }
                }
            }
            return dataTable;
        }

        /// <summary>
        /// 将Sheet对象转为DataTable对象
        /// </summary>
        /// <param name="sheet">Sheet对象</param>
        /// <returns>成功返回返回excel表对象，失败返回null</returns>
        public DataTable ToTable(ISheet sheet)
        {
            if (sheet == null)
            {
                Ex = "表不存在！";
                return null;
            }
            DataTable dt = new DataTable();
            IRow headerRow = sheet.GetRow(0);
            int count = headerRow.LastCellNum; //页头数

            for (int j = 0; j < count; j++)
            {
                ICell cell = headerRow.GetCell(j);
                dt.Columns.Add(cell.ToString());
            }
            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                DataRow dataRow = dt.NewRow();
                for (int j = row.FirstCellNum; j < count; j++)
                {
                    if (row.GetCell(j) != null)
                        dataRow[j] = row.GetCell(j).ToString();
                }
                dt.Rows.Add(dataRow);
            }
            return dt;
        }

        /// <summary>
        /// 读取为Json对象 
        /// </summary>
        /// <param name="file">文件名</param>
        /// <param name="sheet">工作表名</param>
        /// <returns>导入成功返回返回Json数组对象，失败返回null</returns>
        public JArray LoadJson(string file, string sheet = null)
        {
            try
            {
                var table = Load(file.ToFullName(Dir), sheet);
                if (table != null)
                {
                    return JArray.FromObject(table);
                }
            }
            catch (Exception ex)
            {
                Ex = ex.Message;
            }
            return null;
        }

        /// <summary>
        /// 新建字段模型
        /// </summary>
        /// <returns>返回字段模型实例</returns>
        public FieldModel NewField()
        {
            return new FieldModel();
        }

        /// <summary>  
        /// DataTable导出到Excel文件
        /// </summary>  
        /// <param name="file">文件名</param>  
        /// <param name="dataTable">DataTable数据源</param>
        /// <param name="field">字段模型</param>
        /// <param name="header">Excel表头文本（例如：车辆列表）</param>
        /// <param name="sheet">文件名</param>
        /// <returns>导出成功返回true，失败返回false</returns>
        public bool Save(string file, DataTable dataTable, List<FieldModel> field = null, string header = "", string sheet = "Sheet1")
        {
            if (file.EndsWith(".xlsx"))
            {
                return SaveXlsx(file, dataTable, field, header, sheet);
            }
            else
            {
                return SaveXls(file, dataTable, field, header, sheet);
            }
        }

        /// <summary>  
        /// DataTable导出到Excel文件(*.xls)
        /// </summary>  
        /// <param name="file">文件名</param>  
        /// <param name="dataTable">DataTable数据源</param>
        /// <param name="fields">字段模型</param>
        /// <param name="header">Excel表头文本（例如：车辆列表）</param>
        /// <param name="sheet">文件名</param>
        /// <returns>导出成功返回true，失败返回false</returns>
        private bool SaveXls(string file, DataTable dataTable, List<FieldModel> fields = null, string header = "", string sheet = "Sheet1")
        {
            if (dataTable == null)
            {
                return false;
            }
            file = file.ToFullName(Dir);
            bool bl = false;
            HSSFWorkbook wk = new HSSFWorkbook();
            try
            {
                ISheet isheet = wk.CreateSheet(sheet);

                #region 右击文件 属性信息
                {
                    DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                    dsi.Company = "MM";
                    wk.DocumentSummaryInformation = dsi;

                    SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                    si.Author = "超级美眉工作室"; //填加xls文件作者信息
                    si.ApplicationName = "MM"; //填加xls文件创建程序信息
                    si.LastAuthor = "邱文武"; //填加xls文件最后保存者信息
                    si.Comments = "这是使用超级美眉的类库导出的Excel文件"; //填加xls文件作者信息
                    si.Title = "超级美眉Excel"; //填加xls文件标题信息
                    si.Subject = "超级美眉专用主题";//填加文件主题信息
                    si.CreateDateTime = DateTime.Now;
                    wk.SummaryInformation = si;
                }
                #endregion


                ICellStyle dateStyle = wk.CreateCellStyle();
                //dateStyle.BorderBottom = BorderStyle.Thin;
                //dateStyle.BorderLeft = BorderStyle.Thin;
                //dateStyle.BorderRight = BorderStyle.Thin;
                //dateStyle.BorderTop = BorderStyle.Thin;

                IDataFormat format = wk.CreateDataFormat();
                dateStyle.DataFormat = format.GetFormat("yyyy-MM-dd hh:mm:ss");

                //取得列宽
                int[] arrColWidth = new int[dataTable.Columns.Count];
                foreach (DataColumn item in dataTable.Columns)
                {
                    arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
                }
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                    {
                        int intTemp = Encoding.GetEncoding(936).GetBytes(dataTable.Rows[i][j].ToString()).Length;
                        if (intTemp > arrColWidth[j])
                        {
                            arrColWidth[j] = intTemp;
                        }
                    }
                }
                int rowIndex = 0;
                foreach (DataRow row in dataTable.Rows)
                {
                    #region 新建表，填充表头，填充列头，样式
                    if (rowIndex == 65535 || rowIndex == 0)
                    {
                        if (rowIndex != 0)
                        {
                            isheet = wk.CreateSheet(sheet);
                        }
                        // 表头及样式
                        if (!string.IsNullOrEmpty(header))
                        {
                            IRow headerRow = isheet.CreateRow(0);
                            headerRow.HeightInPoints = 25;
                            headerRow.CreateCell(0).SetCellValue(header);

                            ICellStyle headStyle = wk.CreateCellStyle();
                            headStyle.Alignment = HorizontalAlignment.Center;
                            IFont font = wk.CreateFont();
                            font.FontHeightInPoints = 20;
                            font.Boldweight = 700;
                            headStyle.SetFont(font);
                            headerRow.GetCell(0).CellStyle = headStyle;
                            isheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, dataTable.Columns.Count - 1));
                            rowIndex++;
                        }
                        //设置页头
                        IRow twoRow = isheet.CreateRow(rowIndex);
                        ICellStyle style = wk.CreateCellStyle();
                        IFont fontB = wk.CreateFont();
                        //fontB.FontHeightInPoints = 12;
                        fontB.Boldweight = 700;
                        style.SetFont(fontB);
                        style.Alignment = HorizontalAlignment.Center;
                        twoRow.RowStyle = style;
                        foreach (DataColumn column in dataTable.Columns)
                        {
                            ICell cell = twoRow.CreateCell(column.Ordinal);
                            if (fields == null)
                            {
                                cell.SetCellValue(column.ColumnName);
                            }
                            else
                            {
                                var colName = GetColName(fields, column.ColumnName);
                                cell.SetCellValue(colName);
                            }
                            cell.CellStyle = style;

                            //设置列宽
                            isheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1) * 256);
                        }
                        rowIndex++;
                    }
                    #endregion

                    #region 填充内容
                    IRow dataRow = isheet.CreateRow(rowIndex);
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        ICell newCell = dataRow.CreateCell(column.Ordinal);
                        string drValue = row[column].ToString();
                        switch (column.DataType.ToString())
                        {
                            case "System.String"://字符串类型  
                                newCell.SetCellValue(drValue);
                                break;
                            case "System.DateTime"://日期类型  
                                DateTime.TryParse(drValue, out DateTime dateV);
                                newCell.SetCellValue(dateV);
                                newCell.CellStyle = dateStyle;
                                break;
                            case "System.Boolean"://布尔型  
                                bool.TryParse(drValue, out bool boolV);
                                newCell.SetCellValue(boolV);
                                break;
                            case "System.Int16"://整型  
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Byte":
                                int.TryParse(drValue, out int intV);
                                newCell.SetCellValue(intV);
                                break;
                            case "System.Decimal"://浮点型  
                            case "System.Double":
                                double.TryParse(drValue, out double doubV);
                                newCell.SetCellValue(doubV);
                                break;
                            case "System.DBNull"://空值处理  
                                newCell.SetCellValue("");
                                break;
                            default:
                                newCell.SetCellValue("");
                                break;
                        }
                    }
                    #endregion

                    rowIndex++;
                }

                using (FileStream sw = File.Create(file))
                {
                    wk.Write(sw);
                    sheet = null;
                    bl = true;
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                Ex = ex.Message;
            }
            finally
            {
                if (wk != null)
                {
                    wk.Close();
                }
            }

            return bl;
        }

        /// <summary>  
        /// DataTable导出到Excel文件(*.xlsx)
        /// </summary>  
        /// <param name="file">文件名</param>  
        /// <param name="dataTable">DataTable数据源</param>
        /// <param name="fields">字段模型</param>
        /// <param name="header">Excel表头文本（例如：车辆列表）</param>
        /// <param name="sheet">文件名</param>
        /// <returns>导出成功返回true，失败返回false</returns>
        private bool SaveXlsx(string file, DataTable dataTable, List<FieldModel> fields = null, string header = "", string sheet = "Sheet1")
        {
            if (dataTable == null)
            {
                return false;
            }
            file = file.ToFullName(Dir);
            bool bl = false;
            XSSFWorkbook wk = new XSSFWorkbook();
            try
            {
                ISheet isheet = wk.CreateSheet(sheet);

                #region 右击文件 属性信息
                {
                    DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                    dsi.Company = "MM";
                    SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                    si.Author = "超级美眉工作室"; //填加xls文件作者信息
                    si.ApplicationName = "MM"; //填加xls文件创建程序信息
                    si.LastAuthor = "邱文武"; //填加xls文件最后保存者信息
                    si.Comments = "这是使用超级美眉的类库导出的Excel文件"; //填加xls文件作者信
                    si.Title = "超级美眉Excel"; //填加xls文件标题信息
                    si.Subject = "超级美眉专用主题";//填加文件主题信息
                    si.CreateDateTime = DateTime.Now;
                }
                #endregion

                ICellStyle dateStyle = wk.CreateCellStyle();
                IDataFormat format = wk.CreateDataFormat();
                dateStyle.DataFormat = format.GetFormat("yyyy-MM-dd hh:mm:ss");

                //取得列宽
                int[] arrColWidth = new int[dataTable.Columns.Count];
                foreach (DataColumn item in dataTable.Columns)
                {
                    arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
                }
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                    {
                        int intTemp = Encoding.GetEncoding(936).GetBytes(dataTable.Rows[i][j].ToString()).Length;
                        if (intTemp > arrColWidth[j])
                        {
                            arrColWidth[j] = intTemp;
                        }
                    }
                }
                int rowIndex = 0;
                foreach (DataRow row in dataTable.Rows)
                {
                    #region 新建表，填充表头，填充列头，样式
                    if (rowIndex == 65535 || rowIndex == 0)
                    {
                        if (rowIndex != 0)
                        {
                            isheet = wk.CreateSheet(sheet);
                            isheet.IsPrintGridlines = false;
                        }
                        // 表头及样式
                        if (!string.IsNullOrEmpty(header))
                        {
                            IRow headerRow = isheet.CreateRow(0);
                            headerRow.HeightInPoints = 25;
                            headerRow.CreateCell(0).SetCellValue(header);

                            ICellStyle headStyle = wk.CreateCellStyle();
                            headStyle.Alignment = HorizontalAlignment.Center;
                            IFont font = wk.CreateFont();
                            font.FontHeightInPoints = 20;
                            font.Boldweight = 700;
                            headStyle.SetFont(font);
                            headerRow.GetCell(0).CellStyle = headStyle;
                            isheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, dataTable.Columns.Count - 1));
                            rowIndex++;
                        }
                        //设置页头
                        IRow twoRow = isheet.CreateRow(rowIndex);
                        ICellStyle style = wk.CreateCellStyle();
                        IFont fontB = wk.CreateFont();
                        //fontB.FontHeightInPoints = 12;
                        fontB.Boldweight = 700;
                        style.SetFont(fontB);
                        style.Alignment = HorizontalAlignment.Center;
                        twoRow.RowStyle = style;
                        foreach (DataColumn column in dataTable.Columns)
                        {
                            ICell cell = twoRow.CreateCell(column.Ordinal);
                            if (fields == null)
                            {
                                cell.SetCellValue(column.ColumnName);
                            }
                            else
                            {
                                var colName = GetColName(fields, column.ColumnName);
                                cell.SetCellValue(colName);
                            }
                            cell.CellStyle = style;

                            //设置列宽
                            isheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1) * 256);
                        }

                        rowIndex++;
                    }
                    #endregion

                    #region 填充内容
                    IRow dataRow = isheet.CreateRow(rowIndex);
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        ICell newCell = dataRow.CreateCell(column.Ordinal);
                        string drValue = row[column].ToString();
                        switch (column.DataType.ToString())
                        {
                            case "System.String"://字符串类型  
                                newCell.SetCellValue(drValue);
                                break;
                            case "System.DateTime"://日期类型
                                DateTime.TryParse(drValue, out DateTime dateV);
                                newCell.SetCellValue(dateV);
                                newCell.CellStyle = dateStyle;
                                break;
                            case "System.Boolean"://布尔型
                                bool.TryParse(drValue, out bool boolV);
                                newCell.SetCellValue(boolV);
                                break;
                            case "System.Int16"://整型
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Byte":
                                int.TryParse(drValue, out int intV);
                                newCell.SetCellValue(intV);
                                break;
                            case "System.Decimal"://浮点型  
                            case "System.Double":
                                double.TryParse(drValue, out double doubV);
                                newCell.SetCellValue(doubV);
                                break;
                            case "System.DBNull"://空值处理  
                                newCell.SetCellValue("");
                                break;
                            default:
                                newCell.SetCellValue("");
                                break;
                        }
                    }
                    #endregion

                    rowIndex++;
                }

                using (FileStream sw = File.Create(file))
                {
                    wk.Write(sw);
                    sheet = null;
                    bl = true;
                }
            }
            catch (Exception ex)
            {
                Ex = ex.Message;
            }
            finally
            {
                if (wk != null)
                {
                    wk.Close();
                }
            }
            return bl;
        }

        /// <summary>
        /// 设置名称
        /// </summary>
        /// <param name="fields">字段模型</param>
        /// <param name="field">字段</param>
        /// <returns>返回字段名</returns>
        private string GetColName(List<FieldModel> fields, string field)
        {
            string name = field;
            foreach (var o in fields)
            {
                if (o.Field == field)
                {
                    name = o.Name;
                    break;
                }
            }
            return name;
        }

        /// <summary>
        /// 将Json保存为Excel
        /// </summary>
        /// <param name="file">文件名</param>
        /// <param name="jarr">json对象数组</param>
        /// <param name="fields">字段模型</param>
        /// <param name="header">表头</param>
        /// <param name="sheet">工作表名</param>
        /// <returns>保存成功返回true，失败返回false</returns>
        public bool Save(string file, JArray jarr, List<FieldModel> fields = null, string header = "", string sheet = "Sheet1")
        {
            if (jarr == null)
            {
                return false;
            }
            else
            {
                return Save(file, jarr.ToString(), fields, header, sheet);
            }
        }

        /// <summary>
        /// 将Json保存为Excel
        /// </summary>
        /// <param name="file">文件名</param>
        /// <param name="jarrStr">json字符串</param>
        /// <param name="fields">字段模型</param>
        /// <param name="header">是否有标题</param>
        /// <param name="sheet">工作表名</param>
        /// <returns>保存成功返回true，失败返回false</returns>
        public bool Save(string file, string jarrStr, List<FieldModel> fields = null, string header = "", string sheet = "Sheet1")
        {
            if (!string.IsNullOrEmpty(jarrStr))
            {
                try
                {
                    DataTable dataTable = JsonConvert.DeserializeObject<DataTable>(jarrStr);
                    return Save(file, dataTable, fields, header, sheet);
                }
                catch (Exception ex)
                {
                    Ex = ex.Message;
                }
            }
            return false;
        }
    }

    /// <summary>
    /// 字段模型
    /// </summary>
    public class FieldModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 字段
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}
