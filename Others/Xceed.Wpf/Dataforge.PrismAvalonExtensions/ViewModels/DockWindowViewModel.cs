﻿using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace Dataforge.PrismAvalonExtensions.ViewModels
{
    public abstract class DockWindowViewModel : BindableBase, IDisposable
    {
        #region Properties
        #region CanClose
        private bool _CanClose;
        public bool CanClose
        {
            get { return _CanClose; }
            set
            {
                if (_CanClose != value)
                {
                    _CanClose = value;
                    RaisePropertyChanged("CanClose");
                }
            }
        }
        #endregion

        #region CanFloat
        private bool _CanFloat;
        public bool CanFloat
        {
            get { return _CanFloat; }
            set
            {
                if (_CanFloat != value)
                {
                    _CanFloat = value;
                    RaisePropertyChanged("CanFloat");
                }
            }
        }
        #endregion

        #region Title
        private string _Title;
        public string Title
        {
            get { return _Title; }
            set
            {
                if (_Title != value)
                {
                    _Title = value;
                    RaisePropertyChanged("Title");
                }
            }
        }
        #endregion

        #region Glyph
        private string _Glyph;
        public string Glyph
        {
            get { return _Glyph; }
            set
            {
                if (_Glyph != value)
                {
                    _Glyph = value;
                    RaisePropertyChanged("Glyph");
                }
            }
        }
        #endregion

        #region IconSource

        private ImageSource _IconSource = null;
        public ImageSource IconSource
        {
            get { return _IconSource; }
            set
            {
                if (_IconSource != value)
                {
                    _IconSource = value;
                    RaisePropertyChanged("IconSource");
                }
            }
        }

        #endregion

        #endregion

        public DockWindowViewModel()
        {
            this.CanClose = true;
            this.CanFloat = true;
        }


        protected bool IsInitialize;
        public virtual void Initialize()
        {
            IsInitialize = true;
        }
        /// <summary>
        /// 释放标记
        /// </summary>
        private bool disposed;

        ~DockWindowViewModel()
        {
            this.Dispose(false);
        }

        public virtual void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);        
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing) //清理托管资源
            {
                //执行基本的清理代码
            }

            //清理非托管资源

            //告诉自己已经被释放
            disposed = true;
        }
    }
}
