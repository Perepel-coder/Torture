using Services;
using Services.Crypto;
using Services.RequestDB.InterfaceDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;
using System.Windows.Input;
using ViewModels.Models;
using trans = Services.Crypto.TransformData;
using dtype = ViewModels.Models.CypherData_Controls.DataType;
using ftype = Services.DialogService.FileType;
using v = System.Windows.Visibility;
using System.Linq;
using ViewModels.Interfaces;

namespace ViewModels.Researcher
{
    public class CypherData_VM : BaseViewModel
    {
        public override string Name { get; set; }
        public CypherData_Controls Controls { get; set; }
        private readonly DialogService DS;
        private readonly BlockEncryption encrypt;
        private readonly string rootName = "settings";
        public CypherData_VM(
            CypherData_Controls controls,
            DialogService dialogService,
            BlockEncryption encrypt,
            ICryptoAlgService CAS)
        {
            Name = typeof(CypherData_VM).Name;
            Controls = controls;
            Controls.ListOfAlg = new(CAS.GetAlgs("Одноключевой"));
            Controls.ListOfAlgMode = new(CAS.GetModes());
            Controls.Tools.Dchar = '~';
            Controls.Tools.Key = "Моя соседка крутая.";
            Controls.Tools.Vec = 1234;
            Controls.Tools.Way = "Зашифровать";
            Controls.Tools.Alg = Controls.ListOfAlg.FirstOrDefault();
            Controls.Tools.Mode = Controls.ListOfAlgMode.FirstOrDefault();
            DS = dialogService;
            this.encrypt = encrypt;
        }

        private RelayCommand? code;
        private RelayCommand? openFile;
        private RelayCommand? openSettings;
        private RelayCommand? openProjectText;
        private RelayCommand? saveFile;
        private RelayCommand? saveSettings;
        public ICommand Code
        {
            get
            {
                return code ??= new RelayCommand(obj =>
                {
                    Thread thread = new(() =>
                    {
                        try
                        {
                            var table = v.Collapsed;
                            var image = v.Collapsed;
                            var text = v.Collapsed;
                            Controls.VisiProgressBar = v.Visible;
                            Controls.IsEnabledButtonTransform = false;

                            encrypt.Registration(Controls.Tools, Controls.InDataCode);
                            var bytes = (Controls.Tools.Way == "Зашифровать") ?
                            encrypt.Encode() :
                            encrypt.Decode();

                            if (Controls.CurrentType == dtype.TEXT)
                            {
                                if (Controls.VisiProj_Text != v.Collapsed)
                                {
                                    Set_DataCodeType(trans.ToString(bytes), trans.ToInt(bytes));
                                }
                                else
                                {
                                    Set_DataCodeType(trans.ToString(bytes), trans.ToInt(bytes), out text);
                                }
                            }
                            else if (Controls.CurrentType == dtype.TABLE)
                            {
                                if (DS.SaveFileDialog("CryptoTable files|*.ctf") == true)
                                {
                                    FileStream stream = new(DS.FilePath, FileMode.Create, FileAccess.Write);
                                    StreamProcess.SaveFile_CTF(bytes, stream);
                                    Set_DataCodeType(trans.ToString(bytes), trans.ToInt(bytes), out text);
                                    Controls.CurrentType = dtype.CFT;
                                    Controls.StatusBar = $"Файл {DS.FileName} успешно сохранен.";
                                }
                                else
                                {
                                    table = v.Visible;
                                }
                            }
                            else if (Controls.CurrentType == dtype.CFT)
                            {
                                Set_DataCodeType(
                                    trans.ToDataTableFromCFT(bytes),
                                    trans.ToInt(bytes),
                                    out table);
                                Controls.CurrentType = dtype.TABLE;
                            }
                            Controls.Visi_TableImageText(table, image, text);
                            Controls.VisiProgressBar = v.Collapsed;
                            Controls.IsEnabledButtonTransform = true;
                        }
                        catch (Exception ex)
                        {
                            Controls.VisiProgressBar = v.Collapsed;
                            Controls.IsEnabledButtonTransform = true;
                            ErrorMessage(ex.Message, "Ошибка преобразования данных.");
                        }
                    });
                    if (Controls.InData == null ||
                    Controls.InDataCode == null ||
                    Controls.InData.GetType() == typeof(object))
                    {
                        return;
                    }
                    thread.Start();
                });
            }
        }
        public ICommand OpenFile
        {
            get
            {
                return openFile ??= new RelayCommand(obj =>
                {
                    if (DS.OpenFileDialog() == true)
                    {
                        Thread thread = new(() =>
                        {
                            try
                            {
                                FileStream stream = new(DS.FilePath, FileMode.Open);
                                var table = v.Collapsed;
                                var image = v.Collapsed;
                                var text = v.Collapsed;
                                if (DS.TypeFile == ftype.TEXT || DS.TypeFile == ftype.CTF)
                                {
                                    IEnumerable<byte> bytes = StreamProcess.OpenFile(stream);
                                    Controls.InData = trans.ToString(bytes);
                                    text = v.Visible;
                                    Controls.CurrentType = (DS.TypeFile == ftype.TEXT) ?
                                    dtype.TEXT : dtype.CFT;
                                }
                                if (DS.TypeFile == ftype.EXCEL)
                                {
                                    Controls.InData = StreamProcess.OpenFileDT(stream);
                                    Controls.InDataCode = trans.ToInt((DataTable)Controls.InData);
                                    Controls.CurrentType = dtype.TABLE;
                                    table = v.Visible;
                                }
                                Controls.Visi_TableImageText(table, image, text);
                                Controls.VisiProj_Text = v.Collapsed;
                                stream.Close();
                                Controls.StatusBar = $"Файл {DS.FileName} успешно открыт.";
                            }
                            catch (Exception ex)
                            {
                                Controls.StatusBar = $"Файл {DS.FileName} не может быть открыт";
                                ErrorMessage(ex.Message, "Ошибка при открытии файла");
                            }
                            
                        });
                        thread.Start();
                    }
                });
            }
        }
        public ICommand OpenSettings
        {
            get
            {
                return openSettings ??= new RelayCommand(obj =>
                {
                    if (DS.OpenFileDialog() == true)
                    {
                        Thread thread = new(() =>
                        {
                            try
                            {
                                FileStream stream = new(DS.FilePath, FileMode.Open, FileAccess.Read);
                                var data = StreamProcess.OpenFile_XML(stream, rootName);

                                foreach (var el in data)
                                {
                                    switch (el.Key)
                                    {
                                        case nameof(Controls.Tools.Alg):
                                            Controls.Tools.Alg = Controls.ListOfAlg
                                            .Single(a => a.Class == el.Value);
                                            break;
                                        case nameof(Controls.Tools.Mode):
                                            Controls.Tools.Mode = Controls.ListOfAlgMode
                                            .Single(m => m.Class == el.Value);
                                            break;
                                        case nameof(Controls.Tools.Dchar):
                                            Controls.Tools.Dchar = Convert.ToChar(el.Value);
                                            break;
                                        case nameof(Controls.Tools.Key):
                                            Controls.Tools.Key = el.Value;
                                            break;
                                        case nameof(Controls.Tools.Vec):
                                            Controls.Tools.Vec = int.Parse(el.Value);
                                            break;
                                        case nameof(Controls.Tools.Way):
                                            Controls.Tools.Way = el.Value;
                                            break;
                                    }
                                }
                                Controls.StatusBar = $"Файл {DS.FileName} успешно открыт.";
                                stream.Close();
                            }
                            catch (Exception ex)
                            {
                                Controls.StatusBar = $"Файл {DS.FileName} не может быть открыт";
                                ErrorMessage(ex.Message, "Ошибка при открытии файла");
                            }
                        });
                        thread.Start();
                    }
                });
            }
        }
        public ICommand OpenProjectText
        {
            get
            {
                return openProjectText ??= new RelayCommand(obj =>
                {
                    Controls.CurrentType = dtype.TEXT;
                    Controls.InData = "Ваш новый проект!";
                    Controls.Visi_TableImageText(v.Collapsed, v.Collapsed, v.Collapsed);
                    Controls.VisiProj_Text = v.Visible;
                });
            }
        }
        public ICommand SaveFile
        {
            get
            {
                return saveFile ??= new RelayCommand(obj =>
                {
                    Thread thread = new(() =>
                    {
                        try
                        {
                            if (Controls.CurrentType == dtype.TEXT)
                            {
                                if (DS.SaveFileDialog("Txt files|*.txt;*.docx"))
                                {
                                    FileStream stream = new(DS.FilePath, FileMode.Create, FileAccess.Write);
                                    StreamProcess.SaveFile_TXT_DOCX(Controls.InDataCode, stream);
                                    Controls.StatusBar = $"Файл {DS.FileName} успешно сохранен.";
                                    stream.Close();
                                }
                            }
                            else if (Controls.CurrentType == dtype.TABLE)
                            {
                                if (DS.SaveFileDialog("Excel Files|*.xls;*.xlsx;*.xlsm"))
                                {
                                    FileStream stream = new(DS.FilePath, FileMode.Create, FileAccess.Write);
                                    DataTable? table = Controls.InData as DataTable;
                                    if (table != null)
                                    {
                                        StreamProcess.SaveFile_EXCEL(table, stream);
                                        Controls.StatusBar = $"Файл {DS.FileName} успешно сохранен.";
                                        stream.Close();
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Controls.StatusBar = $"Файл {DS.FileName} не может быть создан/изменен";
                            ErrorMessage(ex.Message, "Ошибка при сохранении файла");
                        }
                        
                    });
                    thread.Start();
                });
            }
        }
        public ICommand SaveSettings
        {
            get
            {
                return saveSettings ??= new RelayCommand(obj =>
                {
                    Thread thread = new(() =>
                    {
                        if (DS.SaveFileDialog("CryptoTable files|*.xml"))
                        {
                            FileStream stream = new (DS.FilePath, FileMode.Create, FileAccess.Write);
                            StreamProcess.SaveFile_XML(Controls.Tools, stream, rootName);
                            Controls.StatusBar = $"Файл {DS.FileName} успешно сохранен.";
                            stream.Close();
                        }
                    });
                    try
                    {
                        thread.Start();
                    }
                    catch (Exception ex)
                    {
                        Controls.StatusBar = 
                        $"Ошибка при сохранении файла {DS.FileName}! " +
                        $"Файл не был сохранен.";
                        ErrorMessage(ex.Message, "Ошибка при сохранении файла настроек");
                    }
                });
            }
        }

        public void Set_DataCodeType<T>(T data, IEnumerable<int> code, out v visi)
        {
            Controls.InData = data;
            Controls.InDataCode = code;
            visi = v.Visible;
        }
        public void Set_DataCodeType<T>(T data, IEnumerable<int> code)
        {
            Controls.InData = data;
            Controls.InDataCode = code;
        }
    }
}
