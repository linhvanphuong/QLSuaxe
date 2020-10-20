using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Portal.Utils
{
    public enum StatusEnum
    {
        [Description("Sử dụng")]
        Active = 1,
        [Description("Không sử dụng")]
        Unactive = 2,
        [Description("Tất cả trạng thái")]
        All = 3
    }
    public enum SexEnum
    {
        [Description("Nam")]
        Male = 1,
        [Description("Nữ")]
        Female = 0
    }
    public enum PermissionEnum
    {
        [Description("Thêm mới")]
        Create,
        [Description("Sửa")]
        Update,
        [Description("Xóa")]
        Delete
    }
}



