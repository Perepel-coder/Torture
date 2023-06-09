﻿using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace Services
{
    public class DialogService
    {
        public string txtFilter = "Txt files|*.txt;*.docx";
        public string excelFilter = "Excel Files|*.xls;*.xlsx;*.xlsm";
        public string cryptoFilter = "CryptoTable files|*.ctf";
        public string pdfFilter = "PDF files|*.pdf";

        public enum FileType { TEXT, EXCEL, CTF, NAN };
        public string FilePath { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public FileType TypeFile { get; private set; } = FileType.NAN;
        public void SetFileType(FileType type) 
        { 
            this.TypeFile = type; 
        }
        public bool OpenFileDialog(string filter = "")
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (filter == string.Empty) 
            { 
                openFileDialog.Filter = txtFilter + "|" + excelFilter + "|" + cryptoFilter; 
            }
            else 
            { 
                openFileDialog.Filter = filter; 
            }
            if (openFileDialog.ShowDialog() == true)
            {
                string ext = Path.GetExtension(openFileDialog.FileName);
                if (txtFilter.Contains(ext)) 
                {
                    TypeFile = FileType.TEXT; 
                }
                else if (excelFilter.Contains(ext)) 
                { 
                    TypeFile = FileType.EXCEL; 
                }
                else if (cryptoFilter.Contains(ext)) 
                { 
                    TypeFile = FileType.CTF; 
                }
                this.FilePath = openFileDialog.FileName;
                this.FileName = Path.GetFileName(this.FilePath);
                return true;
            }
            return false;
        }
        public bool SaveFileDialog(string filter = "Txt files|*.txt|Excel Files|*.xls;*.xlsx;*.xlsm")
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (filter == "") { saveFileDialog.Filter = txtFilter + excelFilter + cryptoFilter; }
            else { saveFileDialog.Filter = filter; }
            if (saveFileDialog.ShowDialog() == true)
            {
                this.FilePath = saveFileDialog.FileName;
                this.FileName = Path.GetFileName(this.FilePath);
                return true;
            }
            return false;
        }
        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
