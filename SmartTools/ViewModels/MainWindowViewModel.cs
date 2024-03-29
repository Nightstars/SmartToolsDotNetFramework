﻿using GeneralUpdate.ClientCore;
using GeneralUpdate.ClientCore.Hubs;
using GeneralUpdate.ClientCore.Strategys;
using GeneralUpdate.Core.Models;
using GeneralUpdate.Core.Update;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SmartSoft.common.Utils.update;
using SmartTools.Views;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace SmartToolsDotNet.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region initialize
        private Update _update = new();
        private const string baseUrl = @"http://127.0.0.1:5004/api/AppInfo", hubName = "versionhub";
        //private const string baseUrl = @"http://api.smartcloud.fun:3/api/AppInfo", hubName = "versionhub";
        private ClientParameter clientParameter;
        public MainWindowViewModel(IRegionManager regionManager)
        {
            //CheckUpdate();
            //InitVersionHub();
        }
        #endregion

        #region VersionHub
        /// <summary>
        /// Subscription server push version message.
        /// </summary>
        private void InitVersionHub()
        {
            VersionHub<string>.Instance.Subscribe($"{ baseUrl }/{ hubName }", "TESTNAME", new Action<string>(GetMessage));
        }

        private void GetMessage(string msg)
        {
            MessageBox.Show(msg);
        }

        #endregion VersionHub

        #region Check Update
        private void CheckUpdate()
        {
            Task.Run(async () =>
            {
                //主程序信息
                var mainVersion = FileVersionInfo.GetVersionInfo(@"./SmartTools.exe").FileVersion;

                //该对象用于主程序客户端与更新组件进程之间交互用的对象
                clientParameter = new ClientParameter();

                //本机的客户端程序应用地址
                clientParameter.InstallPath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
                //更新公告网页
                //clientParameter.UpdateLogUrl = "https://www.baidu.com/";

                #region update app.
                //更新组件的版本号
                //clientParameter.ClientVersion = FileVersionInfo.GetVersionInfo(@"./Update.exe").FileVersion;
                clientParameter.ClientVersion = "1.2";
                //客户端类型：1.主程序客户端 2.更新组件
                clientParameter.AppType = (int)AppType.UpdateApp;
                //更新组件请求验证更新的服务端地址
                clientParameter.ValidateUrl = $"{baseUrl}/validate/{ clientParameter.AppType }/{ clientParameter.ClientVersion }";
                //更新组件更新包下载地址
                clientParameter.UpdateUrl = $"{baseUrl}/versions/{ clientParameter.AppType }/{ clientParameter.ClientVersion }";
                //更新程序exe名称
                clientParameter.AppName = "Update";
                //指定应用密钥，用于区分客户端应用
                clientParameter.AppSecretKey = "41A54379-C7D6-4920-8768-21A3468572E5";
                #endregion update app.

                #region main app.
                //主程序客户端exe名称
                clientParameter.MainAppName = "SmartTools";
                //主程序客户端请求验证更新的服务端地址
                clientParameter.MainValidateUrl = $"{baseUrl}/validate/{ (int)AppType.ClientApp }/{ mainVersion }";
                //主程序客户端更新包下载地址
                clientParameter.MainUpdateUrl = $"{baseUrl}/versions/{ (int)AppType.ClientApp }/{ mainVersion }";
                #endregion main app.

                var generalClientBootstrap = new GeneralClientBootstrap();
                //单个或多个更新包下载通知事件
                generalClientBootstrap.MutiDownloadProgressChanged += OnMutiDownloadProgressChanged;
                //单个或多个更新包下载速度、剩余下载事件、当前下载版本信息通知事件
                generalClientBootstrap.MutiDownloadStatistics += OnMutiDownloadStatistics;
                //单个或多个更新包下载完成
                generalClientBootstrap.MutiDownloadCompleted += OnMutiDownloadCompleted;
                //完成所有的下载任务通知
                generalClientBootstrap.MutiAllDownloadCompleted += OnMutiAllDownloadCompleted;
                //下载过程出现的异常通知
                generalClientBootstrap.MutiDownloadError += OnMutiDownloadError;
                //整个更新过程出现的任何问题都会通过这个事件通知
                generalClientBootstrap.Exception += OnException;
                //ClientStrategy该更新策略将完成1.自动升级组件自更新 2.启动更新组件 3.配置好ClientParameter无需再像之前的版本写args数组进程通讯了。
                //generalClientBootstrap.Config(clientParameter).
                generalClientBootstrap.Config(baseUrl, clientParameter.AppSecretKey, "Update").
                Option(UpdateOption.DownloadTimeOut, 60).
                Option(UpdateOption.Encoding, Encoding.Default).
                Option(UpdateOption.Format, "zip").
                //注入一个func让用户决定是否跳过本次更新，如果是强制更新则不生效
                SetCustomOption(ConfirmUpdate).
                Strategy<ClientStrategy>();
                StartLoading();
                await generalClientBootstrap.LaunchTaskAsync();
            });
        }
        #endregion

        #region ConfirmUpdate
        /// <summary>
        /// ConfirmUpdate
        /// </summary>
        /// <returns></returns>
        private bool ConfirmUpdate()
        {
            var messageBoxResult = MessageBox.Show("检测到新版本，是否更新？", "click", MessageBoxButton.YesNoCancel);
            return messageBoxResult == MessageBoxResult.Yes;
        }
        #endregion

        #region OnMutiDownloadStatistics
        /// <summary>
        /// OnMutiDownloadStatistics
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMutiDownloadStatistics(object sender, MutiDownloadStatisticsEventArgs e)
        {
            //e.Remaining 剩余下载时间
            //e.Speed 下载速度
            //e.Version 当前下载的版本信息
            Debug.WriteLine($"{e.Remaining} {e.Speed} {e.Version}");
        }
        #endregion

        #region OnMutiDownloadProgressChanged
        /// <summary>
        /// OnMutiDownloadProgressChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMutiDownloadProgressChanged(object sender, MutiDownloadProgressChangedEventArgs e)
        {
            //e.TotalBytesToReceive 当前更新包需要下载的总大小
            //e.ProgressValue 当前进度值
            //e.ProgressPercentage 当前进度的百分比
            //e.Version 当前下载的版本信息
            //e.Type 当前正在执行的操作  1.ProgressType.Check 检查版本信息中 2.ProgressType.Donwload 正在下载当前版本 3. ProgressType.Updatefile 更新当前版本 4. ProgressType.Done更新完成 5.ProgressType.Fail 更新失败
            //e.BytesReceived 已下载大小
        }
        #endregion

        #region OnException
        /// <summary>
        /// OnException
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnException(object sender, ExceptionEventArgs e)
        {
            Debug.WriteLine(e.Exception.Message);
        }
        #endregion

        #region OnMutiAllDownloadCompleted
        /// <summary>
        /// OnMutiAllDownloadCompleted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMutiAllDownloadCompleted(object sender, MutiAllDownloadCompletedEventArgs e)
        {
            //e.FailedVersions; 如果出现下载失败则会把下载错误的版本、错误原因统计到该集合当中。
            Debug.WriteLine($"Is all download completed {e.IsAllDownloadCompleted}.");
            StopLoading();
        }
        #endregion

        #region OnMutiDownloadCompleted
        /// <summary>
        /// OnMutiDownloadCompleted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMutiDownloadCompleted(object sender, MutiDownloadCompletedEventArgs e)
        {
            //Debug.WriteLine($"{ e.Version.Name } download completed.");
        }
        #endregion

        #region OnMutiDownloadError
        /// <summary>
        /// OnMutiDownloadError
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMutiDownloadError(object sender, MutiDownloadErrorEventArgs e)
        {
            //Debug.WriteLine($"{ e.Version.Name } error!");
        }
        #endregion

        #region 显示更新弹层
        private void StartLoading()
        {
            //_update.Owner = Application.Current.MainWindow;//设置弹出层在主窗体上方
            Application.Current.Dispatcher.Invoke(() =>
            {
                //_update.Owner = ;
                _update.Topmost = true;
                _update.Show();//打开弹出层
            });
            //IsEnabled = false;//设置主窗体不可用
        }
        #endregion

        #region 关闭更新弹层
        private void StopLoading()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _update.Hide();//弹出层隐藏（如果直接Close的话，只能打开弹出层一次）
            });
            //IsEnabled = true;//主窗体可用
        }
        #endregion
    }
}
