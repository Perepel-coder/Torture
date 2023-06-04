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
            Controls.Tools.Key = "Something scientifically boring";
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
                            if (Controls.InData == null ||
                            Controls.InDataCode == null ||
                            Controls.InData.GetType() == typeof(object))
                            {
                                return;
                            }

                            Controls.IsEnabledButtonTransform = false;
                            Controls.ProgressBar_ON();

                            encrypt.Registration(Controls.Tools, Controls.InDataCode);
                            var bytes = (Controls.Tools.Way == "Зашифровать") ?
                            encrypt.Encode() :
                            encrypt.Decode();

                            switch (Controls.CurrentType)
                            {
                                case dtype.TEXT:
                                    Set_DataCodeType(trans.ToString(bytes), trans.ToInt(bytes));
                                    if (!IsVisibility(Controls.VisiProj_Text))
                                        Controls.Visi_Text();
                                    goto default;
                                case dtype.TABLE:
                                    if (DS.SaveFileDialog("CryptoTable files|*.ctf") == true)
                                    {
                                        FileStream stream = new(DS.FilePath, FileMode.Create, FileAccess.Write);
                                        StreamProcess.SaveFile_CTF(bytes, stream);
                                        Set_DataCodeType(trans.ToString(bytes), trans.ToInt(bytes));
                                        Controls.StatusBar = SaveFileMSG_GOOD(DS.FileName);
                                        Controls.CurrentType = dtype.CFT;
                                        Controls.Visi_Text();
                                    }
                                    goto default;
                                case dtype.CFT:
                                    Set_DataCodeType(trans.ToTable_CFT(bytes), trans.ToInt(bytes));
                                    Controls.CurrentType = dtype.TABLE;
                                    Controls.Visi_Table();
                                    goto default;
                                default:
                                    Controls.ProgressBar_OFF();
                                    Controls.IsEnabledButtonTransform = true;
                                    return;
                            }
                        }
                        catch (Exception ex)
                        {
                            Controls.ProgressBar_OFF();
                            Controls.IsEnabledButtonTransform = true;
                            ErrorMessage(ex.Message, "Ошибка преобразования данных.");
                        }
                    });
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
                                if (DS.TypeFile == ftype.TEXT || DS.TypeFile == ftype.CTF)
                                {
                                    IEnumerable<byte> bytes = StreamProcess.OpenFile(stream);
                                    Controls.InData = trans.ToString(bytes);
                                    Controls.Visi_Text();
                                    Controls.CurrentType = (DS.TypeFile == ftype.TEXT) ?
                                    dtype.TEXT : dtype.CFT;
                                }
                                if (DS.TypeFile == ftype.EXCEL)
                                {
                                    Controls.InData = StreamProcess.OpenFileDT(stream);
                                    Controls.InDataCode = trans.ToInt((DataTable)Controls.InData);
                                    Controls.CurrentType = dtype.TABLE;
                                    Controls.Visi_Table();
                                }
                                stream.Close();
                                Controls.StatusBar = OpenFileMSG_GOOD(DS.FileName);
                            }
                            catch (Exception ex)
                            {
                                Controls.StatusBar = OpenFileMSG_ERROR(DS.FileName);
                                ErrorFileMessage(ex.Message);
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
                                Controls.StatusBar = OpenFileMSG_GOOD(DS.FileName);
                                stream.Close();
                            }
                            catch (Exception ex)
                            {
                                Controls.StatusBar = OpenFileMSG_ERROR(DS.FileName);
                                ErrorFileMessage(ex.Message);
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
                    Controls.Visi_ProjTXT();
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
                                    Controls.StatusBar = SaveFileMSG_GOOD(DS.FileName);
                                    stream.Close();
                                }
                            }
                            else if (Controls.CurrentType == dtype.TABLE)
                            {
                                if (DS.SaveFileDialog("Excel Files|*.xls;*.xlsx;*.xlsm"))
                                {
                                    FileStream stream = new(DS.FilePath, FileMode.Create, FileAccess.Write);
                                    if(Controls.InData is DataTable table)
                                    {
                                        StreamProcess.SaveFile_EXCEL(table, stream);
                                        Controls.StatusBar = SaveFileMSG_GOOD(DS.FileName);
                                        stream.Close();
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Controls.StatusBar = SaveFileMSG_ERROR(DS.FileName);
                            ErrorFileMessage(ex.Message);
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
                        try
                        {
                            if (DS.SaveFileDialog("CryptoTable files|*.xml"))
                            {
                                FileStream stream = new(DS.FilePath, FileMode.Create, FileAccess.Write);
                                StreamProcess.SaveFile_XML(Controls.Tools, stream, rootName);
                                Controls.StatusBar = SaveFileMSG_GOOD(DS.FileName);
                                stream.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            Controls.StatusBar = SaveFileMSG_ERROR(DS.FileName);
                            ErrorFileMessage(ex.Message);
                        }  
                    });
                    thread.Start();
                });
            }
        }

        private void Set_DataCodeType<T>(T data, IEnumerable<int> code)
        {
            Controls.InData = data;
            Controls.InDataCode = code;
        }
    }
}
