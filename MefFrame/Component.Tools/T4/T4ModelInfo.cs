﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Component.Tools.T4
{
    /// <summary>
    ///     T4实体模型信息类
    /// </summary>
    public class T4ModelInfo
    {
        public T4ModelInfo(Type modelType, bool useModuleDir = false)
        {
            string @namespace = modelType.Namespace;
            if (@namespace == null)
            {
                return;
            }
            UseModuleDir = useModuleDir;
            if (UseModuleDir)
            {
                int index = @namespace.LastIndexOf('.') + 1;
                ModuleName = @namespace.Substring(index, @namespace.Length - index);
            }

            Name = modelType.Name;
            object[] descAttributes = modelType.GetCustomAttributes(typeof (DescriptionAttribute), true);
            Description = descAttributes.Length == 1 ? ((DescriptionAttribute) descAttributes[0]).Description : Name;
            Properties = modelType.GetProperties();
        }

        /// <summary>
        ///     获取 是否使用模块文件夹
        /// </summary>
        public bool UseModuleDir { get; private set; }

        /// <summary>
        ///     获取 模型所在模块名称
        /// </summary>
        public string ModuleName { get; private set; }

        /// <summary>
        ///     获取 模型名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        ///     获取 模型描述
        /// </summary>
        public string Description { get; private set; }

        public IEnumerable<PropertyInfo> Properties { get; private set; }
    }
}