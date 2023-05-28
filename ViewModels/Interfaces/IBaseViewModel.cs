﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Windows;
using Autofac;
using Services;
using Services.Models;
using ViewModels.Models.Tasks;

namespace ViewModels.Interfaces
{
    public interface IAuthorization<T>
    {
        public BaseViewModel? CurrentVM { get; set; }
        public bool Authorization(string login, T password);
        public bool Registration(string login, T password);
    }
    public abstract class BaseViewModel
    {
        public static User_S? User { get; set; }
        public static IContainer Builder { get; set; } = null!;
        public virtual string Name { get; set; } = typeof(BaseViewModel).Name;
        public static void ErrorMessage(string msg, string? title = "Ошибка операции")
        {
            MessageBox.Show(msg, title,
                 MessageBoxButton.OK,
                 MessageBoxImage.Error);
        }
        public static void InfoMessage(string msg, string? title = "Успешная операция")
        {
            MessageBox.Show(msg, title,
                 MessageBoxButton.OK,
                 MessageBoxImage.Information);
        }
    }
    public abstract class TaskVM: BaseViewModel
    {
        public virtual Counter Counter { get; set; } = new(0, 3, 1);
        public virtual bool TaskCompleted { get; set; } = false;
    }
    public static class AppWindows
    {
        private static Dictionary<string, Type> Windows { get; set; } = new();

        public static void Add(string name, Type window)
        {
            Windows.Add(name, window);
        }

        public static void Remove(string name, Type window)
        {
            Windows.Remove(name);
        }

        public static void Clear()
        {
            Windows.Clear(); 
        }

        public static void ShowDialog(Window? window)
        {
            if (window != null)
            {
                window.ShowDialog();
            }
        }
        public static void Show(Window? window)
        {
            if (window != null)
            {
                window.Show();
            }
        }
        public static Window? SetDataContext<T>(string name, T? context = null) where T: class
        {
            var window = GetWindow(name);
            if (window != null && context != null)
            {
                window.DataContext = context;
            }
            return window;
        }
        private static Window? GetWindow(string name)
        {
            Type type = Windows.Single(w => w.Key == name).Value;
            var window = BaseViewModel.Builder.Resolve(type) as Window;
            if (window != null)
            {
                return window;
            }
            else
            {
                return null;
            }
        }
    }
}