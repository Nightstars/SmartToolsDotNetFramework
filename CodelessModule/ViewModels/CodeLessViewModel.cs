﻿using SmartSoft.common.Utils.solution;
using CodelessModule.Models;
using System;
using System.Collections.Generic;
using Prism.Mvvm;
using System.Windows.Input;
using Prism.Commands;
using HandyControl.Controls;
using System.Runtime.Versioning;
using CodelessModule.Services;
using System.Windows;
using System.Linq;
using System.IO;
using CodelessModule.Utils;
using System.Diagnostics;
using Prism.Events;
using CodelessModule.Events;
using CodeleSmartSoft.SmartUI.WPF.Controls;
using SmartSoft.SmartUI.WPF.Events;
using SmartSoft.SmartUI.WPF.Common.Message;
using HandyControl.Data;

namespace CodelessModule.ViewModels
{
    [SupportedOSPlatform("windows7.0")]
    public class CodeLessViewModel : BindableBase
    {
        #region 数据库列表
        /// <summary>
        /// 数据库列表
        /// </summary>
        private List<DataBaseInfo> _databaselist = new List<DataBaseInfo>();

        public List<DataBaseInfo> Databaselist
        {
            get { return _databaselist; }
            set { SetProperty(ref _databaselist, value); }
        }
        #endregion

        #region 数据库连接字符串
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        //public String connectString= "Database=CMS;Server=127.0.0.1,6666;User ID = sa; Password = Ihavenoidea@0;";
        private string _connectString = string.Empty;

        public string ConnectionString
        {
            get{ return _connectString; }
            set { SetProperty(ref _connectString, value); }
        }
        #endregion

        #region 数据库
        /// <summary>
        /// 数据库
        /// </summary>
        private string _database = string.Empty;

        public string Database
        {
            get { return _database; }
            set { SetProperty(ref _database, value); }
        }
        #endregion

        #region 命名空间
        /// <summary>
        /// 命名空间
        /// </summary>
        private string _rootnamespace = string.Empty;

        public string Rootnamespace
        {
            get { return _rootnamespace; }
            set { SetProperty(ref _rootnamespace, value); }
        }
        #endregion

        #region 数据库表
        /// <summary>
        /// 数据库表
        /// </summary>
        private string _dbTable = string.Empty;

        public string DbTable
        {
            get { return _dbTable; }
            set { SetProperty(ref _dbTable, value); }
        }
        #endregion

        #region 数据库表列表
        private List<DbTable> _dbTables = new List<DbTable>();

        public List<DbTable> DbTables
        {
            get { return _dbTables; }
            set { SetProperty(ref _dbTables, value); }
        }
        #endregion

        #region 解决方案地址
        /// <summary>
        /// 解决方案地址
        /// </summary>
        private string _slnfileaddr = string.Empty;

        public string Slnfileaddr
        {
            get { return _slnfileaddr; }
            set { SetProperty(ref _slnfileaddr, value); }
        }
        #endregion

        #region 项目列表
        /// <summary>
        /// 项目列表
        /// </summary>
        private List<SolutionInfo> _projectlist = new List<SolutionInfo>();

        public List<SolutionInfo> Projectlist
        {
            get { return _projectlist; }
            set { SetProperty(ref _projectlist, value); }
        }
        #endregion

        #region 项目
        /// <summary>
        /// 项目
        /// </summary>
        private string _project = String.Empty;

        public string Project
        {
            get { return _project; }
            set { SetProperty(ref _project, value); }
        }

        private int _selectedProjIndex;
        public int SelectedProjIndex
        {
            get
            {
                return _selectedProjIndex;
            }
            set
            {
                SetProperty(ref _selectedProjIndex, value);
            }
        }
        #endregion

        #region 项目区域
        /// <summary>
        /// 项目区域
        /// </summary>
        private string _projectArea = string.Empty;

        public string ProjectArea
        {
            get { return _projectArea; }
            set { SetProperty(ref _projectArea, value); }
        }
        #endregion

        #region 项目地址
        /// <summary>
        /// 项目地址
        /// </summary>
        private string _projectPath = string.Empty;

        public string ProjectPath
        {
            get { return _projectPath; }
            set { SetProperty(ref _projectPath, value); }
        }
        #endregion

        #region XML配置地址
        /// <summary>
        /// 项目地址
        /// </summary>
        private string _xmlpath = string.Empty;

        public  string Xmlpath
        {
            get { return _xmlpath; }
            set { SetProperty(ref _xmlpath, value); }
        }
        #endregion

        #region 区域列表
        /// <summary>
        /// 项目列表
        /// </summary>
        private List<string> _arealist = new List<string>();

        public List<string> Arealist
        {
            get { return _arealist; }
            set { SetProperty(ref _arealist, value); }
        }
        #endregion

        #region 生成目录
        /// <summary>
        /// 生成目录
        /// </summary>
        private string _buildpath = string.Empty;

        public string Buildpath
        {
            get { return _buildpath; }
            set { SetProperty(ref _buildpath, value); }
        }
        #endregion

        #region 业务主键
        /// <summary>
        /// 业务主键
        /// </summary>
        private string _primarykey = string.Empty;

        public string Primarykey
        {
            get { return _primarykey; }
            set { SetProperty(ref _primarykey, value); }
        }
        #endregion

        #region 数据库字段列表
        /// <summary>
        /// 项目列表
        /// </summary>
        private List<DbTableInfo> _dbTableInfos = new List<DbTableInfo>();

        public List<DbTableInfo> DbTableInfos
        {
            get { return _dbTableInfos; }
            set { SetProperty(ref _dbTableInfos, value); }
        }
        #endregion

        #region 查询字段
        /// <summary>
        /// 查询字段
        /// </summary>
        private List<DbTableInfo> _searchParams = new List<DbTableInfo>();

        public List<DbTableInfo> SearchParams
        {
            get { return _searchParams; }
            set { SetProperty(ref _searchParams, value); }
        }
        #endregion

        #region 项目类型列表
        /// <summary>
        /// 项目列表
        /// </summary>
        private List<string> _projecttypelist = new List<string>();

        public List<string> Projecttypelist
        {
            get { return _projecttypelist; }
            set { SetProperty(ref _projecttypelist, value); }
        }
        #endregion

        #region 项目类型
        /// <summary>
        /// 数据库
        /// </summary>
        private string _projecttype = string.Empty;

        public string Projecttype
        {
            get { return _projecttype; }
            set { SetProperty(ref _projecttype, value); }
        }
        #endregion

        #region 输出类型列表
        /// <summary>
        /// 项目列表
        /// </summary>
        private List<string> _outputtypelist = new List<string>();

        public List<string> Outputtypelist
        {
            get { return _outputtypelist; }
            set { SetProperty(ref _outputtypelist, value); }
        }
        #endregion

        #region 输出类型
        /// <summary>
        /// 数据库
        /// </summary>
        private string _outputtype = string.Empty;

        public string Outputtype
        {
            get { return _outputtype; }
            set { SetProperty(ref _outputtype, value); }
        }
        #endregion

        #region 视图标题
        /// <summary>
        /// 视图标题
        /// </summary>
        private string _viewtitle = string.Empty;

        public string Viewtitle
        {
            get { return _viewtitle; }
            set { SetProperty(ref _viewtitle, value); }
        }
        #endregion

        #region initialize
        private IEventAggregator _ea;
        public CodeLessViewModel(IEventAggregator ea)
        {
            _ea = ea;
            _ea.GetEvent<SearchParamChangedEvent>().Subscribe(MessageReceived);
            _projecttypelist = new List<string> { "cms" };
            _outputtypelist = new List<string> { "项目", "文件" };
            _connectString = "Database=CMS;Server=192.168.2.52,63765;User ID = sa; Password = Longnows2021;";
        }
        #endregion

        #region 连接数据库
        public ICommand ConnectCommand { get => new DelegateCommand<object>(OnConnect); }

        private void OnConnect(object obj)
        {

            Dialog? logading = null;

            if (string.IsNullOrWhiteSpace(_connectString))
            {
                HandyControl.Controls.MessageBox.Error("数据库连接字符串不能为空!");
                return;
            }

            try
            {
                logading = Dialog.Show(new Loading());
                Databaselist = new CodeLessService(_connectString).GetDatabase();

                Database = Databaselist.FirstOrDefault()?.name;
                Growl.Success(new GrowlInfo { Message = "连接成功", WaitTime = 1, ShowDateTime = false });
            }
            catch (Exception ex)
            {
                HandyControl.Controls.MessageBox.Error(ex.Message);
            }
            finally
            {
                logading?.Close();
            }

        }
        #endregion

        #region 选择数据库
        public ICommand DatabaseSelChangedCommand => new DelegateCommand<object>(OnDatabaseChange);

        private void OnDatabaseChange(object obj)
        {
            var logading = Dialog.Show(new Loading());

            if (string.IsNullOrWhiteSpace(_connectString))
            {
                HandyControl.Controls.MessageBox.Error("数据库连接字符串不能为空!");
                return;
            }

            try
            {
                if (!string.IsNullOrWhiteSpace(_database))
                {
                    DbTables = new CodeLessService(_connectString).GetDbTable(_database);
                    SearchParams = new List<DbTableInfo>();
                }
                else
                {
                    DbTable = String.Empty;
                }
            }
            catch (Exception ex)
            {
                HandyControl.Controls.MessageBox.Error(ex.Message);
            }
            finally
            {
                logading.Close();
            }
        }
        #endregion

        #region 选择表
        public ICommand DbSelChangeCommand => new DelegateCommand<object>(OnDbSelChange);

        private void OnDbSelChange(object obj)
        {
            if (!string.IsNullOrWhiteSpace(_dbTable))
                DbTableInfos = new CodeLessService(_connectString).GetDbTableInfo(_database, _dbTable);
            else
                DbTableInfos = new List<DbTableInfo>();

            SearchParams = new List<DbTableInfo>();
        }
        #endregion

        #region 查询字段
        public ICommand SearchparamSelChangeCommand => new DelegateCommand<IEnumerable<DbTableInfo>> (OnSearchparamSelChange);

        private void OnSearchparamSelChange(IEnumerable<DbTableInfo> obj)
        {
            var temp = obj;
        }
        #endregion

        #region 选择解决方案
        public ICommand ChooseSlnCommand => new DelegateCommand<object>(OnChooseSln);

        public void OnChooseSln(object ovj)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "Solution Files (*.sln)|*.sln",
                Title = "选择解决方案"

            };
            var result = openFileDialog.ShowDialog();
            if (result == true)
            {
                Arealist = new List<string>();
                Rootnamespace = String.Empty;
                Slnfileaddr = openFileDialog.FileName;
                Projectlist = new SolutionUtil(_slnfileaddr).SlnParse();

            }
            else
            {
                HandyControl.Controls.MessageBox.Error("未选择任何解决方案!");
            }
        }
        #endregion

        #region 选择项目
        public ICommand ProjSelChangeCommand => new DelegateCommand<string>(OnProjSelChange);

        private void OnProjSelChange(string obj)
        {
            if (obj != null)
            {
                var path = _projectlist?.Where(x => x.projName == obj)?.FirstOrDefault()?.projFullName;
                _projectPath = $"{path?.Substring(0, path.LastIndexOf("/"))}/Areas";
                _xmlpath = "App_Data";

                if (Directory.Exists(@$"{_slnfileaddr.Substring(0, _slnfileaddr.LastIndexOf("\\"))}\{_projectPath}"))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(@$"{_slnfileaddr.Substring(0, _slnfileaddr.LastIndexOf("\\"))}\{_projectPath}");
                    var files = directoryInfo.GetDirectories();
                    List<string> areas = new List<string>();
                    foreach (var file in files)
                    {
                        areas.Add(file.Name);
                    }
                    Arealist = areas;

                }
            }
        }
        #endregion

        #region 选择区域
        public ICommand ProjAreaSelChangeCommand => new DelegateCommand<Object>(OnProjAreaSelChange);

        private void OnProjAreaSelChange(Object obj)
        {
            if (Directory.Exists(@$"{_slnfileaddr.Substring(0, _slnfileaddr.LastIndexOf("\\"))}\{_projectPath}"))
            {
                _buildpath = @$"{_slnfileaddr.Substring(0, _slnfileaddr.LastIndexOf("\\"))}\{_projectPath}\{_projectArea}".Replace("\\", "/");
                var path = _projectlist?.Where(x => x.projName == _project).FirstOrDefault()?.projName;
                Rootnamespace = $"{path?.Substring(0, path.LastIndexOf("."))}.Areas.{_projectArea}";
            }
        }
        #endregion

        #region  生成代码
        public ICommand GenCodeCommand => new DelegateCommand<Object>(OnGenCode);

        private void OnGenCode(Object obj)
        {
            var logading = Dialog.Show(new Loading());



            if (string.IsNullOrWhiteSpace(_connectString))
            {
                HandyControl.Controls.MessageBox.Error("连接字符串不能为空!");
                logading.Close();
                return;
            }

            if (string.IsNullOrWhiteSpace(_database))
            {
                HandyControl.Controls.MessageBox.Error("请选择数据库!");
                logading.Close();
                return;
            }

            if (string.IsNullOrWhiteSpace(_dbTable))
            {
                HandyControl.Controls.MessageBox.Error("请选择数据库表!");
                logading.Close();
                return;
            }

            if (string.IsNullOrWhiteSpace(_primarykey) && _projecttype == "wechat api")
            {
                HandyControl.Controls.MessageBox.Error("请选择业务主键!");
                logading.Close();
                return;
            }

            if (string.IsNullOrWhiteSpace(_rootnamespace))
            {
                HandyControl.Controls.MessageBox.Error("请填写命名空间!");
                logading.Close();
                return;
            }

            if (string.IsNullOrWhiteSpace(_projecttype))
            {
                HandyControl.Controls.MessageBox.Error("请选择项目类型!");
                logading.Close();
                return;
            }

            if (string.IsNullOrWhiteSpace(_outputtype))
            {
                HandyControl.Controls.MessageBox.Error("请选择生成类型!");
                logading.Close();
                return;
            }


            try
            {
                var projname = $"{_project.Split(".").Reverse().Skip(1).Take(1).FirstOrDefault()}";
                var tbname = $"{ FiledUtil.GetModelName(_dbTable) }";

                var temp = _buildpath;

                //生成代码
                new CodeBuilder
                (
                    DbTableInfos,
                    _rootnamespace,
                    tbname.Contains(projname) ? $"{tbname}" : $"{projname}{tbname}",
                    _database,
                    _dbTable,
                    _buildpath,
                    _projecttype,
                    _outputtype,
                    _primarykey,
                    _searchParams,
                    _projectArea,
                    $"{_buildpath.Split("Areas").FirstOrDefault()}{_xmlpath}",
                    _viewtitle
                )
                .BuildModel()
                .BuildSearchModel()
                .BuildService()
                .BuildController()
                .BuildViews()
                .BuildDbinfo();

                //show result
                if (_outputtype == "项目")
                    HandyControl.Controls.MessageBox.Success("生成成功");
                else
                {
                    var result = HandyControl.Controls.MessageBox.Show("生成成功,是否打开输出文件夹？", "温馨提示", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.OK);
                    if (result == MessageBoxResult.OK)
                    {
                        logading.Close();
                        Process.Start("explorer.exe", $@"{AppDomain.CurrentDomain.BaseDirectory}Oupput\");
                    }
                    else
                        logading.Close();
                }
                logading.Close();
            }
            catch (Exception ex)
            {
                HandyControl.Controls.MessageBox.Error(ex.Message);
                logading.Close();
            }
            finally
            {
                logading.Close();
            }
        }
        #endregion

        #region MessageReceived
        private void MessageReceived(List<DbTableInfo> message)
        {
            _searchParams = message ?? new List<DbTableInfo>();
        }
        #endregion
    }
}
