using System;
using System.Collections.Generic;
using System.Text;

namespace MM.Common.Models
{
    public class PowerModel
    {
        /// <summary>
        /// 是否需含Token
        /// </summary>
        public bool Token         { get; set; } = false;

        /// <summary>
        /// 是否需登录
        /// </summary>
        public bool Login         { get; set; } = false;

        /// <summary>
        /// 允许访问的用户组，填写用户组ID，多个组用“,”分隔，为空则允许所有人访问
        /// </summary>
        public string User_group  { get; set; }

        /// <summary>
        /// 允许访问的管理组，填写管理组ID，多个组用“,”分隔，为空则允许所有人访问
        /// </summary>
        public string Admin_group { get; set; }

        /// <summary>
        /// 限定访问的用户，填写用户ID，多个用户用“,”分隔，为空则允许所有人访问
        /// </summary>
        public string Users       { get; set; }
    }
}

/*
    "scope": true,
    "login": true,
    "user_group": "1|6",
    "admin_group": "1",
    "user": "1|2|3"
*/
