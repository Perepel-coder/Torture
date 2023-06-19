using Services.Models;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Xceed.Words.NET;
using System.Text;
using System.Xml;

namespace Services.Crypto
{
    public static class StreamProcess
    {
        #region ReadStream
        public static IEnumerable<byte> OpenFile(FileStream stream)
        {
            if (stream == null)
            {
                throw new Exception("Поток данных не существует.");
            }
            byte[] data = new byte[stream.Length];
            stream.Read(data);
            switch (Path.GetExtension(stream.Name))
            {
                case ".ctf":
                    return data.Select(el => el ^= 0xad);
                case ".txt":
                    return data;
                case ".docx":
                    var paragraphs = DocX.Load(stream).Paragraphs;
                    string result = string.Empty;
                    foreach (var el in paragraphs)
                    {
                        result += el.Text + "\n";
                    }
                    return TransformData.ToByte(result);
            }
            throw new Exception("Не известный формат файла");
        }
        public static DataTable OpenFileDT(FileStream stream)
        {
            if (stream == null)
            {
                throw new Exception("Поток данных не существует.");
            }
            using ExcelEngine excelEngine = new();
            IApplication application = excelEngine.Excel;
            application.DefaultVersion = ExcelVersion.Excel2013;
            IWorkbook workbook = application.Workbooks.Open(stream, ExcelOpenType.Automatic);
            IWorksheet worksheet = workbook.Worksheets[0];
            DataTable table = worksheet.ExportDataTable(worksheet.UsedRange, ExcelExportDataTableOptions.ColumnNames);
            for (int i = 0; i < table.Columns.Count; i++)
            {
                while (table.Columns[i].ColumnName.IndexOf("/") != -1)
                {
                    table.Columns[i].ColumnName = Convert.ToString(table.Columns[i].ColumnName.Replace("/", "÷"));
                }
            }
            return table;
        }
        public static Dictionary<string, string> OpenFile_XML(FileStream stream, string rootName)
        {
            if (stream == null)
            {
                throw new Exception("Поток данных не существует.");
            }
            XDocument xdoc = XDocument.Load(stream);
            return TransformData.ToDictionary(xdoc, rootName);
        }
        public static string Read_XML(string fileName)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                doc.Load(stream);
                StringBuilder stringBuilder = new StringBuilder();
                XmlTextWriter textWriter = new XmlTextWriter(new StringWriter(stringBuilder));
                textWriter.Formatting = Formatting.Indented;
                doc.Save(textWriter);
                textWriter.Close();
                return stringBuilder.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        #endregion

        #region SaveStream
        public static void SaveFile_CTF(IEnumerable<byte>? data, FileStream stream)
        {
            if (stream == null)
            {
                throw new Exception("Поток данных не существует.");
            }
            if (data == null)
            {
                throw new Exception("Массив данных пуст");
            }
            byte[] array = data.Select(el => el ^= 0xad).ToArray();
            stream.Write(array, 0, array.Length);
        }
        public static void SaveFile_TXT_DOCX(IEnumerable<int>? data, FileStream stream)
        {
            if (stream == null)
            {
                throw new Exception("Поток данных не существует.");
            }
            if (data == null)
            {
                throw new Exception("Массив данных пуст");
            }
            IEnumerable<byte> dataB = TransformData.ToByte(data);
            if (Path.GetExtension(stream.Name) == ".txt")
            {
                byte[] array = dataB.ToArray();
                stream.Write(array, 0, array.Length);
            }
            if (Path.GetExtension(stream.Name) == ".docx")
            {
                var docX = DocX.Create(stream);
                string text = TransformData.ToString(dataB);
                var paragraphs = text.Split(new char[] { '\n' });
                foreach (var p in paragraphs)
                {
                    docX.InsertParagraph(p);
                }
                docX.SaveAs(stream);
            }
        }
        public static void SaveFile_EXCEL(DataTable table, FileStream stream)
        {
            using var excelEngine = new ExcelEngine();
            IApplication application = excelEngine.Excel;
            application.DefaultVersion = ExcelVersion.Excel2013;
            IWorkbook workbook = application.Workbooks.Create(1);
            IWorksheet worksheet = workbook.Worksheets[0];
            worksheet.ImportDataTable(table, true, 1, 1);
            workbook.SaveAs(stream);
        }
        public static void SaveFile_XML(CypherTools tools, FileStream stream, string rootName = "settings")
        {
            Dictionary<string, string?> Pairs = new();
            Pairs.Add(nameof(tools.Alg), tools.Alg.Class);
            Pairs.Add(nameof(tools.Mode), tools.Mode.Class);
            Pairs.Add(nameof(tools.Dchar), tools.Dchar.ToString());
            Pairs.Add(nameof(tools.Key), tools.Key);
            Pairs.Add(nameof(tools.Vec), tools.Vec.ToString());
            Pairs.Add(nameof(tools.Way), tools.Way);
            var document = TransformData.ToXML(Pairs, rootName);
            document.Save(stream);
        }

        public static void CopyFile(string path, string newPath)
        {
            File.Copy(path, newPath, true);
        }
        public static void DeleteFile(string path) 
        { 
            File.Delete(path);
        }
        #endregion
    }
}
