using System;

namespace GrowDT.MvcHelper.Authorization
{
    [Flags]
    public enum UserRole
    {
        /// <summary>
        /// 无法获取用户类别
        /// </summary>
        None = 0,
        /// <summary>
        /// 超级管理员
        /// </summary>
        SuperAdmin = 1,
        /// <summary>
        /// 商业流管理员
        /// </summary>
        BusinessFlowManager = 2,
        /// <summary>
        /// 信息流管理员
        /// </summary>
        InformationFlowManager = 4,
        /// <summary>
        /// 资金流管理员
        /// </summary>
        CashFlowManager = 8,
        /// <summary>
        /// 产品流管理员
        /// </summary>
        ProductFlowManager = 16,
        /// <summary>
        /// CBP
        /// </summary>
        CBP = 32,
        /// <summary>
        /// 销售经理
        /// </summary>
        SalesManager = 64,
    }
}
