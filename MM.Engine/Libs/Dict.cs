using System.Collections.Generic;
using System.Dynamic;

namespace MM.Engine.Libs
{
    /// <summary>
    /// 字典拓展dynamic
    /// </summary>
    public class DynamicDictionary : DynamicObject
    {
        /// <summary>
        /// 内部字典
        /// </summary>
        readonly Dictionary<string, object> dictionary = new Dictionary<string, object>();

        /// <summary>
        /// 这个属性返回字典成员数量
        /// </summary>
        public int Count
        {
            get
            {
                return dictionary.Count;
            }
        }

        /// <summary>
        /// 如果你试图让一个类中没有定义,属性值,调用此方法。
        /// </summary>
        /// <param name="binder">绑定器</param>
        /// <param name="result">输出结果</param>
        /// <returns>成功返回true,失败返回false</returns>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            // 将属性名转换为小写,属性名不区分大小写。
            string name = binder.Name.ToLower();

            // 如果属性名是在字典中找到,结果参数设置属性值,返回true。否则,返回false。
            return dictionary.TryGetValue(name, out result);
        }

        /// 如果你想设置一个值的属性中没有定义类,调用此方法。
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            // 将属性名转换为小写,属性名不区分大小写。
            dictionary[binder.Name.ToLower()] = value;

            // 你可以添加一个值字典,所以这个方法总是返回true。
            return true;
        }
    }
}
