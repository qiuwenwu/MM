using System.Collections.Generic;

namespace MM.Task
{
    /// <summary>
    /// 任务驱动
    /// </summary>
    public class Drive : Common.Drive
    {
        /// <summary>
        /// 任务字典
        /// </summary>
        public Dictionary<string, Helper> Dict { get; set; } = new Dictionary<string, Helper>();

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns>删除成功返回true，失败返回false</returns>
        public bool Del(string key) {
            return Dict.Remove(key);
        }

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="cg">配置</param>
        public void Set(Config cg) {
            if (cg.Info != null)
            {
                cg.Change();
                var v = (Helper)cg;
                var k = cg.Info.Name;
                if (Dict.ContainsKey(k))
                {
                    Dict[k].End();
                    Dict[k] = v;
                }
                else
                {
                    v.Init();
                    Dict.Add(k, v);
                }
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="path">搜索路径</param>
        public void Update(string path) {
            if (path != null) {
                Dir = path;
            }
            var list = EachLoad<Config>();
            foreach (var o in list) {
                Set(o);
            }
        }

        /// <summary>
        /// 新建配置
        /// </summary>
        /// <returns>返回配置模型</returns>
        public Config New() {
            return new Config();
        }
    }
}
