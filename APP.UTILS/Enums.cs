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
    public enum MotorLiftEnum
    {
        [Description("Chờ")]
        Active = 1,
        [Description("Đang sử dụng")]
        Acting = 2,
        [Description("Khóa")]
        Unactive = 3,
        [Description("Tất cả trạng thái")]
        All = 4
    }
    public enum BillStatus
    {
        [Description("Phiếu tạm tính")]
        TemporaryLT = 1,
        [Description("Phiếu tạm tính - ktv")]
        TemporaryKTV = 2,
        [Description("Hóa đơn")]
        Bill = 3
    }
    public enum AccountStatusEnum
    {
        [Description("Rảnh")]
        Active = 1,
        [Description("Bận")]
        Acting = 2,
    }
}



