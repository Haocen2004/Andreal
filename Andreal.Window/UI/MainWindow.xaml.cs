﻿using System;
using System.Collections.Concurrent;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Andreal.Core.Utils;
using Andreal.Window.UI.UserControl;

namespace Andreal.Window.UI;

internal partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
        var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
        timer.Tick += (_, _) => Status.Text = BotStatementHelper.Status;
        timer.Start();
        _controlers = new();
        _controlers.TryAdd(WindowStatus.None, new());
    }

    internal enum WindowStatus
    {
        None,
        AccountManage,
        Setting,
        ReplySetting,
        MessageLog,
        ExceptionLog
    }

    private System.Windows.Controls.UserControl GetNewUserControl(WindowStatus status)
    {
        return status switch
               {
                   WindowStatus.AccountManage => new Accounts(),
                   WindowStatus.Setting       => new Setting(),
                   WindowStatus.ReplySetting  => new ReplySetting(),
                   WindowStatus.MessageLog    => new MessageLog(),
                   WindowStatus.ExceptionLog  => new ExceptionLog(),
                   _                          => new()
               };
    }

    private readonly ConcurrentDictionary<WindowStatus, System.Windows.Controls.UserControl> _controlers;

    private WindowStatus _status = WindowStatus.None;
    
    private void ChangeUserControl(WindowStatus status)
    {
        if (_status == status) return;
        _controlers[_status].Visibility = Visibility.Collapsed;
        _status = status;
        _controlers.GetOrAdd(_status, GetNewUserControl);
        _controlers[_status].Visibility = Visibility.Visible;
        Label.Content = _controlers[_status];
    }

    private void OnMinBtnClick(object sender, RoutedEventArgs e) => Hide();

    private void OnAccountManageClick(object sender, RoutedEventArgs e)
    {
        ChangeUserControl(WindowStatus.AccountManage);
    }

    private void OnSettingClick(object sender, RoutedEventArgs e)
    {
        ChangeUserControl(WindowStatus.Setting);
    }

    private void OnMessagePushClick(object sender, MouseButtonEventArgs e)
    {
        ChangeUserControl(WindowStatus.MessageLog);
    }

    private void OnExceptionLogClick(object sender, MouseButtonEventArgs e)
    {
        ChangeUserControl(WindowStatus.ExceptionLog);
    }

    private void OnReplySettingClick(object sender, MouseButtonEventArgs e)
    {
        ChangeUserControl(WindowStatus.ReplySetting);
    }

    private void OnMainWindowClosed(object? sender, EventArgs e) { Environment.Exit(0); }

    private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        try
        {
            DragMove();
        }
        catch
        {
            //ignored
        }
    }
}
