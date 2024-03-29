using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using VSE.Properties;
using VSE.Core;

namespace VSE
{
    /// <summary>
    /// 权限类
    /// </summary>
    class Permission
    {

        /// <summary>
        /// 当前权限等级
        /// </summary>
        private static PermissionLevel currentPermission = PermissionLevel.NoPermission;
        internal static PermissionLevel CurrentPermission
        {
            get { return Permission.currentPermission; }
            set
            {
                try
                {
                    Permission.currentPermission = value;
                    string loginInfo = string.Empty;
                    switch (value)
                    {
                        case PermissionLevel.NoPermission:
                            if (Project.Instance.configuration.enablePermissionControl)
                                loginInfo = ("未登录，最低权限");
                            else
                                loginInfo = ("未登录");
                           
                            break;
                        case PermissionLevel.Operator:
                            loginInfo = ( "操作员");
                          
                            break;
                        case PermissionLevel.Admin:
                            loginInfo = ( "管理员");
                           
                            break;
                        case PermissionLevel.Developer:
                            loginInfo = ( "开发人员");
                          
                            break;
                    }

                    Win_Main.Instance.tss_permissionInfo.Text = ( "当前用户：") + loginInfo;
                }
                catch (Exception ex)
                {
                    Log.SaveError(Project.Instance.configuration.dataPath,ex);
                }
            }
        }


        /// <summary>
        /// 检查权限等级
        /// </summary>
        /// <param name="permission">能进行此操作的最低权限等级</param>
        /// <returns></returns>
        internal static bool CheckPermission(PermissionLevel permission)
        {
            if (!Project.Instance.configuration.enablePermissionControl)
                return true;
            if ((int)currentPermission < (int)permission)
            {
                Win_Main.Instance.OutputMsg("权限不足，请登录更高一级权限后重试",Win_Log.InfoType.error);
                return false;
            }
            return true;
        }

    }
    internal enum PermissionLevel
    {
        NoPermission,
        Operator,
        Admin,
        Developer,
    }
}
