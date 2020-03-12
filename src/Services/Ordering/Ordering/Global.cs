using LinFx.Modules;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Ordering.Infrastructure
{
    /// <summary>
    /// 全局配置
    /// </summary>
    public static class Global
    {
        public static string DefaultCulture => "zh-CN";

        public static string WebRootPath { get; set; }

        public static string ContentRootPath { get; set; }

        public static IConfiguration Configuration { get; set; }

        ///// <summary>
        ///// 语言集合
        ///// </summary>
        //public static IList<Culture> Cultures { get; set; } = new List<Culture>();

        /// <summary>
        /// 模块集合
        /// </summary>
        public static IList<ModuleInfo> Modules { get; set; } = new List<ModuleInfo>();
    }
}
