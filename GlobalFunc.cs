using System;
using System.Collections.Generic;
using System.Text;

namespace learnWinForms
{
    internal class GlobalFunc
    {
        // 单例模式
        /*
            饿汉式
            普通懒汉
            双重检查锁
            Lazy<T>
         */

        private static GlobalFunc _Instance = null;

        private GlobalFunc() { }
        public static GlobalFunc Instance
        {
            get
            {
                if( _Instance == null )
                {
                    _Instance = new GlobalFunc();
                }
                return _Instance;
            }
        }

        //public string name = "123";

        // 初始化登录窗口 form1
        public Form1 formLogin = null;
        public string? account = null;
    }
}
